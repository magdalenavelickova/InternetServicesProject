using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.DTOs
{
    public class NationalParkDto
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]      
        public string state { get; set; }

        public DateTime created { get; set; }
        public DateTime established { get; set; }
    }
}
