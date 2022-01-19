using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using WcsAsra.Enums;

namespace WcsAsra.Model
{
    public class Device : ViewModelBase
    {
        #region[字段]

        //private byte deviceid;    //设备号
        //private byte devicestatus;   //设备状态

        private byte direction;      //方向    0是左，1是右
        private byte column;         //排
        private byte row;            //层
        private DeviceHaveGoodsStatuE ishave;         //状态

        #endregion

        public byte Direction
        {
            get => direction;
            set => Set(ref direction, value);
        }


        public byte Row
        {
            get => row;
            set => Set(ref row, value);
        }
        public byte Column
        {
            get => column;
            set => Set(ref column, value);
        }

        public DeviceHaveGoodsStatuE IsHave
        {
            get => ishave;
            set => Set(ref ishave, value);
        }

        public SocketMsgTypeE MsgType { set; get; }

        public SocketConnectStatusE ConnStatus { set; get; }
    }
}
