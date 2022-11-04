using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZeroHunger.Models
{
    public class CollectionRequestModel
    {
        public int ColId { get; set; }
       
        public Nullable<int> CrId { get; set; }
        [Required]
        public string FoodType { get; set; }
       [Required]
        public int MaxTime { get; set; }
        
        public Nullable<System.DateTime> Date { get; set; }
        
        public Nullable<int> CempId { get; set; }
       
        public Nullable<int> Status { get; set; }
    }
}