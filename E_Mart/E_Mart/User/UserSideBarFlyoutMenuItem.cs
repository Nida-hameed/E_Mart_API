﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.User
{
    public class UserSideBarFlyoutMenuItem
    {
        public UserSideBarFlyoutMenuItem()
        {
            TargetType = typeof(UserSideBarFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}