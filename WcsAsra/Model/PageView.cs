using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using WcsAsra.Enums;

namespace WcsAsra.Model
{
    public class PageView :ViewModelBase
    {
        #region[字段]

        //private bool 
        private bool direction = false;
        private int row;
        private int column;
        private bool ishave = false;

        #endregion
        
        public int Direction
        {
            get
            {
                if (direction)
                {
                    return 2;
                }
                return 1;
            }
            set
            {
                if (value == 2)
                {
                    direction = true;
                }
                else
                {
                    direction = false;
                }
            }
        }

        public int Row
        {
            get => row;
            set => Set(ref row, value);
        }
        public int Column
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
