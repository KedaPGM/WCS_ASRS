using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace WcsAsra.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
        }

        public static ViewModelLocator Instance => new Lazy<ViewModelLocator>(() => Application.Current.TryFindResource("Locator") as ViewModelLocator).Value;

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
    }
}
