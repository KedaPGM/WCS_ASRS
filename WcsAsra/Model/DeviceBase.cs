using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using WcsAsra.Enums;

namespace WcsAsra.Model
{
    public class DeviceBase : ViewModelBase
    {
        public string Ip = "192.168.3.1";              //地址
        public int Port = 2002;                       //端口

        public Device device;
    }
}
