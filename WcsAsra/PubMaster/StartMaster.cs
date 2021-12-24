using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace WcsAsra.PubMaster
{
    public class StartMaster
    {
        public StartMaster()
        {

        }
        
        /// <summary>
        /// 启动调度时开始启动后台逻辑
        /// </summary>
        public static void Start()
        {

            CreateInstance();

            new Thread(StartAllBackStage)
            {
                IsBackground = true,
                Name = "启动任务逻辑"
            }.Start();
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        private static void CreateInstance()
        {


        }

        private static void StartAllBackStage()
        {
            //Communication.Start();


        }



    }
}
