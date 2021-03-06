using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using WcsAsra.Enums;
using WcsAsra.Model;

namespace WcsAsra.Communication
{
    public abstract class TcpBase : IDisposable
    {
        #region[参数定义]

        internal const int CONNECTION_TIMEOUT = 10 * 1000;
        internal const int CONNECTION_RETRY_TIMEOUT = 5 * 1000;
        internal const int MESSAGE_RESEND_TIMEOUT = 5 * 1000;

        internal TcpClient m_Client;
        internal bool m_Connected;
        public bool m_Working;

        internal NetworkStream m_Stream;

        internal ManualResetEvent m_StopSignal;
        internal AutoResetEvent m_QueueHandle;

        internal Thread m_ReaderThread;
        internal Timer m_RetryTimer;


        //internal DateTime? LastSendTime;
      //  internal MTimer mTimer;
        internal DateTime lastfunctime = DateTime.Now;

        internal object _senobj;
        #endregion

        #region[其他属性]

      //  internal SocketMsgMod mMsgMod { set; get; }
        public string mLogname { set; get; }
        internal Device mdevMsgMod { set; get; }
        protected DeviceBase mDev { set; get; }
        protected int mMinProtLength { set; get; }
        protected bool mSystemStop { set; get; }

        protected uint DevID;

        #endregion

        #region[构造方法]

        protected TcpBase(DeviceBase dev)
        {
            mDev = dev;//+"-"+dev.id

            _senobj = new object();
            //mMsgMod = new SocketMsgMod();
            //mMsgMod.ID = dev.id;
            //DevID = dev.id;
            //mTimer = new MTimer();
        }

        public void Start(string memo)
        {
            Open(memo);
        }

        #endregion

        #region[连接断开]

        public bool IsConnected
        {
            get
            {
                return m_Client != null && m_Client.Connected && m_Connected;
            }
        }

        public bool Open(string memo)
        {
            try
            {
                if (IsConnected)
                {
                    string logMessage = "VCP9412 client already started";
                    throw new InvalidOperationException(logMessage);
                }

                if (string.IsNullOrEmpty(mDev.Ip))
                {
                    string logMessage = string.Format("VCP9412 server ip not valid: '{0}'", mDev.Ip);
                    throw new ArgumentNullException(logMessage);
                }

                if (mDev.Port <= 0
                    || mDev.Port < IPEndPoint.MinPort
                    || mDev.Port > IPEndPoint.MaxPort)
                {
                    string logMessage = string.Format("VCP9412 server port not valid: '{0}'", mDev.Port);
                    throw new ArgumentOutOfRangeException(logMessage);
                }
                m_QueueHandle = new AutoResetEvent(false);

                m_Working = true;
                mSystemStop = false;
                // connect on new thread
                new Thread(Connect)
                {
                    IsBackground = true
                }.Start();
            }
            catch
            {
                Close();
                throw;
            }

            return true;
        }


        internal void Connect()
        {
            try
            {
                if (mSystemStop) return;
                //if (IsConnected) return;
                if (m_Client != null)
                {
                    m_Client.Close();
                    m_Client.Dispose();
                    m_Client = null;
                }
                m_Client = new TcpClient();
                m_Client.BeginConnect(mDev.Ip, mDev.Port, new AsyncCallback(ConnectCallback), null);

                //SendMsg(SocketMsgTypeE.Connection, SocketConnectStatusE.连接中, null);

                //Console.WriteLine("连接成功" + DateTime.Now.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("连接失败" + DateTime.Now.ToString() + e.StackTrace);
            }
        }

        internal void Reconnect()
        {
            if (mSystemStop)
            {
                return;
            }
            // important to disconnect within a timer (different thread) since the call came from
            // one of the threads being closed in the Disconnect() method
            m_RetryTimer = new Timer(delegate (object state)
            {
                if (IsConnected)
                {
                    if (m_RetryTimer != null)
                        m_RetryTimer.Change(Timeout.Infinite, Timeout.Infinite);
                    m_RetryTimer = null;
                    return;
                }
                m_RetryTimer = null;
                Disconnect();
                Connect();
            }, null, CONNECTION_RETRY_TIMEOUT, 0);
        }


        private void Disconnect(string memo = "连接断开")
        {
            if (m_Client != null)
            {
                m_Client.Close();
                m_Client = null;
            }

            if (m_StopSignal != null)
            {
                m_StopSignal.Set();
            }

            if (m_ReaderThread != null)
            {
                try
                {
                    if (!m_ReaderThread.Join(TimeSpan.FromMilliseconds(100)))
                    {
                        m_ReaderThread.Abort(TimeSpan.FromMilliseconds(100));
                    }
                }
                catch (Exception)
                {
                    //Console.WriteLine("中止读取线程" + DateTime.Now.ToString() + e.StackTrace);
                }
                m_ReaderThread = null;
            }


            if (m_Stream != null)
            {
                m_Stream.Close();
                m_Stream = null;
            }

            m_Connected = false;

            //SendMsg(SocketMsgTypeE.Connection, SocketConnectStatusE.连接断开, null);
        }

        public void Close(string memo = "连接断开")
        {
            // stop timer
            if (m_RetryTimer != null)
            {
                m_RetryTimer.Change(Timeout.Infinite, Timeout.Infinite);
            }

            Disconnect(memo);
        }

        public void Stop(string memo = "连接断开")
        {
            m_Working = false;
            //Disconnect();
            mSystemStop = true;
            Close(memo);
        }

        #endregion


        internal byte[] ShiftBytes(byte[] buffer, int offset, int size)
        {
            return buffer.Skip(offset).Take(size).Reverse().ToArray();
        }

        public void Dispose()
        {
            Close();
        }

        #region[抽象方法]

        internal abstract void SendMsg(SocketMsgTypeE type, SocketConnectStatusE status, Device device);

        internal abstract void ConnectCallback(IAsyncResult ar);
        #endregion

        #region[添加其他日志]

        public void AddStatusLog(string log)
        {

        }

        #endregion
    }
}
