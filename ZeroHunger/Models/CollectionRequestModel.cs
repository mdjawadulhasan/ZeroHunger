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
        [Required]
        public Nullable<int> CrId { get; set; }
        [Required]
        public string FoodType { get; set; }
        [Required]
        public string MaxTime { get; set; }
        [Required]
        public Nullable<System.DateTime> Date { get; set; }
        [Required]
        public Nullable<int> CempId { get; set; }
        [Required]
        public Nullable<int> Status { get; set; }
    }
}