using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using WcsAsra.Enums;

namespace WcsAsra.Model
{
    public class DeviceBase : ViewModelBase
    {
        #region[字段]

        public string Ip = "10.9.30.26";              //地址
        public int Port = 2002;                         //端口

        private byte deviceid;                   //设备号
        private byte devicestatus;   //设备状态
        private byte direction;      //方向    0是左，1是右
        private byte row;            //层
        private byte column;         //位
        private DeviceHaveGoodsStatuE ishave;         //状态

        #endregion

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

    }
}
