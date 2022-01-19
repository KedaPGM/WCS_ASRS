using System;
using System.Collections.Generic;
using System.Text;
using WcsAsra.Model;
using WcsAsra;
using WcsAsra.ViewModel;

namespace WcsAsra.PubMaster
{
    public class FindMaster
    {

        public FindMaster()
        {

        }

        public bool GetIsHave(uint row, uint col)
        {
            return MainViewModel.PageViewList.Find(c => c.Row == row && c.Column == col).IsHave;
        }

        public static void SetView(Device item)
        {
            PageView pageView = null;

            if (item.IsHave == Enums.DeviceHaveGoodsStatuE.有货)
            {
                pageView = MainViewModel.PageViewList.Find(c => c.Column == item.Column && c.Row == item.Row && c.Direction == item.Direction);

                if (pageView != null)
                {
                    pageView.IsHave = true;
                }

            }
            else
            {
                pageView = MainViewModel.PageViewList.Find(c => c.Column == item.Column && c.Row == item.Row && c.Direction == item.Direction);

                if (pageView != null)
                {
                    pageView.IsHave = false;
                }
            }

            //PageViewList.Add(new PageView
            //{
            //    Column = item.Column,
            //    IsHave = item.IsHave == Enums.DeviceHaveGoodsStatuE.有货 ,
            //    Row = item.Row
            //});
        }

    }
}
