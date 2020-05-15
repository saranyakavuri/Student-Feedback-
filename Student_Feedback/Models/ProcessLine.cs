using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gios_mvcSolution.Models
{
    public class ProcessLine
    {
        [Display(Name="Product Process Num")]
        public int ProductProcessID { get; set; }

        [Display(Name = "Line Name")]
        public string strLineID { get; set; }

        [Display(Name ="Product Num")]
        public int IntProductID { get; set; }

        [Display(Name = "Date Created")]
        public Nullable<System.DateTime> dtmCreated { get; set; }

        [Display(Name = "Created By")]
        public string strCreatorID { get; set; }

        [Display(Name = "Last Modified")]
        public Nullable<System.DateTime> dtmModified { get; set; }

        [Display(Name = "Last Modified By")]
        public string strModifierID { get; set; }

        [NotMapped]
        public SelectList ProcessLineList { get; set; }
    }
}