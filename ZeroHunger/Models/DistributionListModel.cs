using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZeroHunger.Models
{
    public class DistributionListModel
    {
        public int DisId { get; set; }
       
        public string FoodType { get; set; }
        [Required] 
        public string Place { get; set; }
        [Required] 
        public Nullable<System.DateTime> Date { get; set; }
    }
}