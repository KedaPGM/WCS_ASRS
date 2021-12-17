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
        public MainViewModel()
        {
            PageView pageView = new PageView()
            {
                Row = 1,
                Column = 1,
                IsHave = true
            };
            
        }

    }
}
