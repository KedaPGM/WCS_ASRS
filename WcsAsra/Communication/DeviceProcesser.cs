using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using WcsAsra.Model;

namespace WcsAsra.Communication
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    /// <summary>
    /// 数据结构
    /// </summary>
    public struct DeviceStatusStruct
    {
        private byte Head;           //字头  【0x99】
        private byte direction;      //方向    0是左，1是右
        private byte row;            //层
        private byte column;         //位
        private byte ishave;         //状态
        public ushort Tail;          //命令字尾【0xFF】
    }

    public class DeviceProcesser : ProcesserBase
    {
        Device device;

        public DeviceProcesser()
        {
            device = new Device();
        }

    }

}
