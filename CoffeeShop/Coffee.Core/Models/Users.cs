﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Core.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Person { get; set; }
        public string Password { get; set; }
        public string AdminBy { get; set; }

    }
}
