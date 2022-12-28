﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.LoggedInSeller
{
    public class SellerSideBarFlyoutMenuItem
    {
        public SellerSideBarFlyoutMenuItem()
        {
            TargetType = typeof(SellerSideBarFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}