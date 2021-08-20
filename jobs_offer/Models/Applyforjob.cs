using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobs_offer.Models
{
    public class Applyforjob
    {
        public int id { get; set; }
        public string message { get; set; }
        public DateTime ApplyDate { get; set; }
        public int jobid { get; set; }
        public string userid { get; set;}
        public virtual job jobs { get; set; }
        public virtual ApplicationUser users { get; set; }
    }
}