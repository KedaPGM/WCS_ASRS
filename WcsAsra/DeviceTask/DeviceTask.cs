using System;
using System.Collections.Generic;
using System.Text;
using WcsAsra.Communication;
using WcsAsra.Model;

namespace WcsAsra.DeviceTack
{
    public class DeviceTask
    {
        public  DeviceTask()
        {


        }

        public DeviceBase Device { set; get; } = new DeviceBase();
        public DeviceTcp DevTcp { set; get; }

        //public bool IsEnable
        //{
        //    get => Device?.enable ?? false;
        //}

        public void Start(string memo = "开始连接")
        {
            //if (!IsEnable) return;

            if (DevTcp == null)
            {
                DevTcp = new DeviceTcp(Device);    
            }
            DevTcp.Start(memo);
            //if (!DevTcp.m_Working)
            //{
            //    DevTcp.Start(memo);
            //}

        }

        /// <summary>
        /// 关闭通讯
        /// </summary>
        /// <param name="memo"></param>
        public void Stop(string memo)
        {
            DevTcp?.Stop(memo);
        }

    }

}
