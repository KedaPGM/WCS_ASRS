using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using WcsAsra.Enums;
using WcsAsra.Model;

namespace WcsAsra.Communication
{
    public class DeviceTcp : TcpBase
    {
        private DeviceProcesser DeviceProcesser;

        public DeviceTcp(DeviceBase dev) : base(dev)
        {
            DeviceProcesser = new DeviceProcesser();
            mMinProtLength = Const.BUFFER_SIZE;
        }


        internal override void SendMsg(SocketMsgTypeE type, SocketConnectStatusE status, Device device)
        {
            //if (Monitor.TryEnter( , TimeSpan.FromMilliseconds(500)))
            //{
            //    try
            //    {
            //        mdevMsgMod.MsgType = type;
            //        mdevMsgMod.ConnStatus = status;
            //        mdevMsgMod.IDevice = device;
            //        CarrierMsgUpdate(mdevMsgMod);

            //    }
            //    finally
            //    {
            //        Monitor.Exit(mdevMsgMod);
            //    }
            //}
        }



        /// <summary>
        /// 成功连接
        ///     1.接收数据
        ///     2.处理数据
        ///     3.发送数据
        /// </summary>
        /// <param name="ar"></param>
        internal override void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                if (m_Client != null)
                {
                    m_Client.EndConnect(ar);

                    m_Stream = m_Client.GetStream();

                    m_StopSignal = new ManualResetEvent(false);
                    m_Connected = true;

                    m_ReaderThread = new Thread(new ThreadStart(ReceiverHandler));
                    m_ReaderThread.Name = "ClientBaseReceiver";
                    m_ReaderThread.Start();

                    //SendMsg(SocketMsgTypeE.Connection, SocketConnectStatusE.连接成功, null);
                    Console.WriteLine("连接成功:" + DateTime.Now.ToString());

                    //_mLog.Status(true, "连接成功");
                }
                else
                {
                    m_RetryTimer = new Timer(delegate (object state)
                    {
                        m_RetryTimer = null;
                        Connect();
                        //Console.WriteLine("连接失败重连:" + DateTime.Now.ToString());
                    }, null, CONNECTION_RETRY_TIMEOUT, 0);
                }
            }
            catch
            {
                m_RetryTimer = new Timer(delegate (object state)
                {
                    m_RetryTimer = null;
                    Connect();
                    //Console.WriteLine("连接失败重连:" + DateTime.Now.ToString());
                }, null, CONNECTION_RETRY_TIMEOUT, 0);
            }
        }

        /// <summary>
        /// 接收到数据
        /// </summary>
        private void ReceiverHandler()
        {
            if (!IsConnected)
            {
                string logMessage = "Cannot start receiver - client not started";
                //throw new InvalidOperationException(logMessage);
                //_mLog.Error(true, logMessage);
                Reconnect();
                return;
            }

            byte[] bufferData = null;

            byte[] buffer = new byte[mMinProtLength];

            while (!m_StopSignal.WaitOne(0, false))
            {
                try
                {

                    int bytesRead = m_Stream.Read(buffer, 0, mMinProtLength);
                    if (bytesRead == 0)
                    {
                        //Reconnect();
                        continue;
                    }

                    byte[] readData = buffer.Take(bytesRead).ToArray();

                    if (bufferData != null && bufferData.Length > 0)
                    {
                        readData = bufferData.Concat(readData).ToArray();
                    }


                    if (!ProcessData(ref readData))
                    {
                        Reconnect();

                        //Console.WriteLine("处理数据重连:" + DateTime.Now.ToString());
                        break;
                    }

                    // save until next round
                    bufferData = readData;
                }
                catch (IOException)
                {
                    // unclean disconnect from service
                    Reconnect();
                    //Console.WriteLine("处理数据报错:" + DateTime.Now.ToString()+e.StackTrace);
                    break;
                }
                catch
                {
                    // don't handle error, just wait for end signal
                }
            }
        }

        /// <summary>
        /// 数据处理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool ProcessData(ref byte[] data)
        {
            while (data.Length >= mMinProtLength)
            {
                if (!MatchWithProtocol(ref data))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 循环匹配所有协议
        /// 寻找合适的协议
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool MatchWithProtocol(ref byte[] data)
        {
            //ushort tail = BitConverter.ToUInt16(ShiftBytes(data, mMinProtLength - 2, 2), 0);

            ushort head = BitConverter.ToUInt16(ShiftBytes(data, 0, 2), 0);
            ushort tail = BitConverter.ToUInt16(ShiftBytes(data, mMinProtLength - 2, 2), 0);

            if (head == Const.HEAD_KEY && tail == Const.TAIL_KEY)
            { 
                byte[] pdata = new byte[Const.STATUS_SIZE];
                Array.Copy(data, 0, pdata, 0, Const.STATUS_SIZE);
                Device device = DeviceProcesser.GetStatus(pdata);

                //SendMsg(SocketMsgTypeE.DataReiceive, SocketConnectStatusE.通信正常, device);

                PubMaster.FindMaster.SetView(device);
                //截取后释放剩余值
                data = data.Skip(Const.STATUS_SIZE).ToArray();
                return true;
            }
            return false;
        }

        public ushort byteToHexStr(byte[] bytes)
        {
            ushort returnStr = 0;
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i];
                }
            }
            return returnStr;
        }





    }
}
