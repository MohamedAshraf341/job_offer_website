using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Web.Mvc;

namespace jobs_offer.Models
{
    public class category
    {
        public int id { get; set; }
        [Required]
        [Display(Name ="نوع الوظيفه")]
        public string categoryname { get; set; }
        [Required]
        [Display(Name ="وصف الوظيفه")]
        public string categorydescription { get; set; }
        //كود many
        public virtual ICollection<job> jobs { get; set; }
    }
}