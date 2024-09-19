﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.DAL.Models
{
    public  class AppUser : IdentityUser
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
    }
}
