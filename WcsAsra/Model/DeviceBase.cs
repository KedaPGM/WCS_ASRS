using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;

namespace WcsAsra.Model
{
    public class DeviceBase : ViewModelBase
    {
        #region[字段]

        public string Ip = "192.168.0.5";              //地址
        public int Port = 2021;            //端口

        #endregion

    }
}
