using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gios_mvcSolution.Models
{
    public class ProcessDepartment
    {
        [Display(Name ="Process / Department")]
        public string strProcessID { get; set; }

        [Display(Name = "Process Description")]
        public string strProcessDescr { get; set; }

        [Display(Name = "Int Sort Order")]
        public int? intSortOrder { get; set; }

        [Display(Name = "Active")]
        public int ysnActive { get; set; }

        [Display(Name = "Date Created")]
        public Nullable<System.DateTime> dtmCreated { get; set; }

        [Display(Name = "Created By")]
        public string strCreatorID { get; set; }

        [Display(Name = "Last Modified")]
        public Nullable<System.DateTime> dtmModified { get; set; }

        [Display(Name = "Last Modified By")]
        public string strModifierID { get; set; }

        [NotMapped]
        public SelectList ProccessList { get; set; }

        [NotMapped]
        public IList Processes { get; set; }
    }
}