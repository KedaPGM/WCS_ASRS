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
            try
            {
                if (item.IsHave == Enums.DeviceHaveGoodsStatuE.有货)
                {
                    MainViewModel.PageViewList.Find(c => c.Column == item.Column && c.Row == item.Row && c.Direction == item.Direction).IsHave = true;
                }
                else
                {
                    MainViewModel.PageViewList.Find(c => c.Column == item.Column && c.Row == item.Row && c.Direction == item.Direction).IsHave = false;
                }
            }
            catch
            {

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
