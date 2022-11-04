using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZeroHunger.Models
{
    public class RestaurantModel
    {
        public int Rid { get; set; }
        [Required]
        public string ResName { get; set; }
        [Required]
        public string ResLocation { get; set; }
        [Required]
        public string ResUname { get; set; }
        
        public string ResPass { get; set; }
    }
}