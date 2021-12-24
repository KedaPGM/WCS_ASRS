using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using WcsAsra.Enums;
using WcsAsra.Model;

namespace WcsAsra.Communication
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    /// <summary>
    /// 数据结构
    /// </summary>
    public struct DeviceStatusStruct
    {
        public byte Head;           //字头  【0x99】
        public byte direction;      //方向    0是左，1是右
        public byte row;            //层
        public byte column;         //位
        public byte ishave;         //状态
        public ushort Tail;          //命令字尾【0xFF】
    }

    public class DeviceProcesser : ProcesserBase
    {
        Device device;

        public DeviceProcesser()
        {
            device = new Device();
        }

        public Device GetStatus(byte[] data)
        {
            DeviceStatusStruct st = BufferToStruct<DeviceStatusStruct>(data);

            device.Row = st.row;
            device.Column = st.column;
            device.IsHave = (DeviceHaveGoodsStatuE)st.ishave;

            return device;
        }

        ///// <summary>
        ///// 获取指令  
        ///// </summary>
        ///// <param name="devid"></param>
        ///// <param name="type"></param>
        ///// <param name="mark"></param>
        ///// <returns></returns>
        //internal byte[] GetCmd(string devid, byte mark)
        //{
        //    DeviceStatusStruct cmd = new DeviceStatusStruct
        //    {
        //        Head = ShiftBytes(SocketConst.CARRIER_CMD_HEAD_KEY),
        //        DeviceID = byte.Parse(devid),
        //        Command = (byte)type,
        //        Value13 = mark,
        //        Tail = ShiftBytes(SocketConst.TAIL_KEY)
        //    };

        //    return StructToBuffer(cmd);
        //}


    }

}
