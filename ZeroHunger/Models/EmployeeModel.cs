using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZeroHunger.Models
{
    public class EmployeeModel
    {
        public int Empid { get; set; }
        [Required]
        public string EmpName { get; set; }
        [Required]
        public string EmpAge { get; set; }
        [Required]
        public string EmpAdd { get; set; }
        [Required]
        public string EmpPhone { get; set; }
        [Required]
        public string EmpUserName { get; set; }
       
        public string EmpPassword { get; set; }
        
        public Nullable<int> EmpStatus { get; set; }
    }
}