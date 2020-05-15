using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gios_mvcSolution.Areas.UseCase.Models
{
    public class RequiredSupportTypes
    {
        [Display(Name = "Support Type ID")]
        [Key]
        public int intSupportTypeID { get; set; }

        [Display(Name = "Support Type")]
        public string strSupportType { get; set; }

        [Display(Name ="Support Type Description")]
        public string strSupportTypeDescr { get; set; }

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
        public IList RequiredSupportList { get; set; }
    }

    public class UseCaseExtraSupport
    {
        [Key]
        public int lngRecordID { get; set; }

        public int intUseCaseID { get; set; }

        public string strSupportType { get; set; }

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