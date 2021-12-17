using System;
using System.Collections.Generic;
using System.Text;
using WcsAsra.Model;

namespace WcsAsra.PubMaster
{
    public class FindMaster
    {
        public static List<PageView> PageViewList = new List<PageView>();

        public FindMaster()
        {

        }

        public bool GetIsHave(uint row, uint col)
        {
            return PageViewList.Find(c => c.Row == row && c.Column == col).IsHave;
        }

    }
}
