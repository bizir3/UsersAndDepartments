using System;
using System.Collections;
using System.Collections.Generic;

namespace UsersAndDepartments.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FIO { get; set; }
        public int DepId { get; set; }
        public DateTime DateAdd { get; set; }
        
        public virtual Department Department { get; set; }
    }
}