using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gios_mvcSolution.Areas.UseCase.Models
{
    public class ImpactKPI
    {
        [Display(Name = "Support Type ID")]
        [Key]
        public int intImpactKPIID { get; set; }

        [Display(Name = "KPI Name")]
        public string strKPI { get; set; }

        [Display(Name = "KPI Description")]
        public string strKPIDescr { get; set; }

        [Display(Name = "Date Created")]
        public Nullable<System.DateTime> dtmCreated { get; set; }
        [Display(Name = "Created By")]
        public string strCreatorID { get; set; }
        [Display(Name = "Last Modified")]
        public Nullable<System.DateTime> dtmModified { get; set; }
        [Display(Name = "Modified By")]
        public string strModifierID { get; set; }

        public bool isSelected { get; set; }

        [NotMapped]
        public IList impactKPIList { get; set; }
    }

    public class UseCaseImpactKPI
    {
        public string strKPI { get; set; }
        public int intUseCaseID { get; set; }

        [Display(Name = "Date Created")]
        public Nullable<System.DateTime> dtmCreated { get; set; }
        [Display(Name = "Created By")]
        public string strCreatorID { get; set; }
        [Display(Name = "Last Modified")]
        public Nullable<System.DateTime> dtmModified { get; set; }
        [Display(Name = "Modified By")]
        public string strModifierID { get; set; }
    }

}