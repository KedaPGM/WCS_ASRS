using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;

namespace WcsAsra.Model
{
    public class PageView :ViewModelBase
    {
        #region[字段]

        private uint row;
        private uint column;
        private bool ishave;

        #endregion

        public uint Row
        {
            get => row;
            set => Set(ref row, value);
        }
        public uint Column
        {
            get => column;
            set => Set(ref column, value);
        }

        public bool IsHave
        {
            get => ishave;
            set => Set(ref ishave, value);
        }


    }
}
