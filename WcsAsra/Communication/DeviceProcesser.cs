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
        public ushort Head;           //字头  【0x99 0x01】
        public byte direction;      //方向    0是左，1是右
        public byte column;            //层
        public byte row;         //位
        public byte ishave;         //状态
        public ushort Tail;          //命令字尾【0xEE 0xFF】
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
            device.Direction = st.direction;
            device.Row = st.row;
            device.Column = st.column;
            device.IsHave = (DeviceHaveGoodsStatuE)st.ishave;

            return device;
        }
    }

}
