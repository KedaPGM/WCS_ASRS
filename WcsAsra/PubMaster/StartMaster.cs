using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using WcsAsra.DeviceTack;

namespace WcsAsra.PubMaster
{
    public class StartMaster
    {
        public static DeviceTask Device { set; get; }
        public static FindMaster Find { set; get; }

        public static bool IsReady = false;


        //public static StartMaster()
        //{

        //}
        
        /// <summary>
        /// 启动调度时开始启动后台逻辑
        /// </summary>
        public static void Start()
        {
            IsReady = false;
            CreateInstance();

            new Thread(StartTcp)
            {
                IsBackground = true,
                Name = "启动Tcp连接"
            }.Start();
        }

        public static void Stop()
        {
            StopTcp();
        }




        /// <summary>
        /// 创建实例
        /// </summary>
        private static void CreateInstance()
        {

            Find = new FindMaster();
            Device = new DeviceTask();

            //检测数据加载完成后启动
            while (IsReady)
            {
                Thread.Sleep(2000);
            }
        }

        private static void StartTcp()
        {
            Device?.Start();
        }

        private static void StopTcp()
        {
            Device?.Stop("关闭");
        }


    }
}
