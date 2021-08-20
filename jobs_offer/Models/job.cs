using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace jobs_offer.Models
{
    public class job
    {
        public int id { get; set; }
        [Display(Name ="اسم الوظيفه")]
        public string jobtitle { get; set; }
        [Display(Name = "وصف الوظيفه")]
        [AllowHtml]
        public string jobcontent { get; set; }
        //[Required]
        [Display(Name = "صورة الوظيفه")]  
        public string jobimage { get; set; }
        [Display(Name = "نوع الوظيفه")]
        public int categoryid { get; set; }
        public string UserId { get; set; }
       //علاقة one
        public virtual category cat { get; set; }
        public virtual ApplicationUser User { get; set; }


    }
}