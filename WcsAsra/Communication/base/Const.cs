namespace WcsAsra.Communication
{
    public class Const
    {
        #region[公共]

        /// <summary>
        /// 读取长度
        /// </summary>
        internal const int BUFFER_SIZE = 8;
        internal const ushort STATUS_SIZE = 8;

        /// <summary>
        /// 头尾部值
        /// </summary>
        internal const ushort HEAD_KEY = 0x9901;
        internal const ushort TAIL_KEY = 0xEEFF;
        #endregion

    }
}