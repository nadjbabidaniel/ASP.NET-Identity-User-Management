﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Users.Models
{
    public class RoleEditModel
    {
        public AppRole Role { get; set; }
        public IEnumerable<ApplicationUser> Members { get; set; }
        public IEnumerable<ApplicationUser> NonMembers { get; set; }
    }
}