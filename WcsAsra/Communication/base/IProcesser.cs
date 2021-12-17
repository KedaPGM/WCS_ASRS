using System;

namespace WcsAsra.Communication
{
    public interface IProcesser
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreatedUTC
        {
            get;
        }

        MsgBuffer ToAciMsgBuffer();
    }
}
