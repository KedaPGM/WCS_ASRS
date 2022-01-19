using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using WcsAsra.Model;

namespace WcsAsra.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        public static List<PageView> _PageViewList;

        public static List<PageView> PageViewList
        {
            get
            {
                if (_PageViewList == null || _PageViewList.Count == 0)
                {
                    _PageViewList = new List<PageView>();

                    for (int r = 1; r <= 4; r++)
                    {
                        for (int c = 1; c <= 6; c++)
                        {
                            _PageViewList.Add(new PageView() { Row = r, Column = c, IsHave = false, Direction = 1 });
                        }
                    }
                    for (int r = 1; r <= 4; r++)
                    {
                        for (int c = 1; c <= 6; c++)
                        {
                            _PageViewList.Add(new PageView() { Row = r, Column = c, IsHave = false, Direction = 2 });
                        }
                    }
                }
                return _PageViewList;
            }
            set => _PageViewList = value;
        }


        public MainViewModel()
        {
            //for (int r = 0; r < 4; r++)
            //{
            //    for (int c = 0; c < 6; c++)
            //    {
            //        PageViewList.Add(new PageView() { Row = r, Column = c, IsHave = false });
            //    }

            //}
        }

    }
}
