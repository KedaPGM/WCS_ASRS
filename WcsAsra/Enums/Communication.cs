using System;
using System.Collections.Generic;
using System.Text;

namespace WcsAsra.Enums
{
    public enum SocketMsgTypeE
    {
        Connection,
        DataReiceive,
    }

    /// <summary>
    /// 通信状态
    /// </summary>
    public enum SocketConnectStatusE
    {
        初始化,
        连接中,
        连接成功,
        握手成功,
        通信正常,
        连接断开,
        主动断开
    }
}
