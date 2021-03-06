﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UsersAndDepartments.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public DateTime DateAdd { get; set; }
        public DateTime DateUpdate { get; set; }
        
        [JsonIgnore]
        public virtual IEnumerable<User> Users { get; set; }
    }
}