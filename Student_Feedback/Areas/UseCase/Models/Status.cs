using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gios_mvcSolution.Areas.UseCase.Models
{
    public class Status
    {
        [Key]
        [Display(Name = "Status")]
        public string strStatus { get; set; }

        [Display(Name ="Satus Description")]
        public string strDescription { get; set; }

        [Display(Name ="Is Active")]
        public bool yesActive { get; set; }

        [Display(Name = "Date Created")]
        public Nullable<System.DateTime> dtmCreated { get; set; }
        [Display(Name = "Created By")]
        public string strCreatorID { get; set; }
        [Display(Name = "Last Modified")]
        public Nullable<System.DateTime> dtmModified { get; set; }
        [Display(Name = "Modified By")]
        public string strModifierID { get; set; }

        public IList StatusList { get; set; }
    }
}