﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Users.Models
{
    //[NotMapped]
    public class AppRole:IdentityRole
    {
        public AppRole() : base() { }

        public AppRole(string name) : base(name) { }
        // 在此添加额外属性


    }
}