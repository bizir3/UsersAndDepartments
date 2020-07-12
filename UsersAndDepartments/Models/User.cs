using System;
using Newtonsoft.Json;

namespace UsersAndDepartments.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FIO { get; set; }
        public int DepId { get; set; }
        public DateTime DateAdd { get; set; }
     
        [JsonIgnore]
        public virtual Department Department { get; set; }
    }
}