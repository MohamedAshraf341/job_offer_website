using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobs_offer.Models
{
    public class JobsViewModel
    {
        public string JobTitle { get; set; }
        public IEnumerable<Applyforjob> Items { get; set; }
    }
}