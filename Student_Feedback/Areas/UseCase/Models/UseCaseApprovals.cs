using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gios_mvcSolution.Areas.UseCase.Models
{
    public class UseCaseApprovals
    {
        [Key]
        public int lngRecordID { get; set; }

        [Display(Name ="Use Case ID")]
        public int intUseCaseID { get; set; }

        [Display(Name ="Use Case Title")]
        public string strUseCaseTitle { get; set; }

        [Display(Name = "Problem Description")]
        public string strUseCaseDescr { get; set; }

        [Display(Name ="BU")]
        public string strBUID { get; set; }

        [Display(Name ="Approved?")]
        public string ysnApproved { get; set; }

        [Display(Name ="Comments on Approval / Rejection")]
        public string strComments { get; set; }

        [Display(Name = "Date Created")]
        public Nullable<System.DateTime> dtmCreated { get; set; }
        [Display(Name = "Created By")]
        public string strCreatorID { get; set; }

        [Display(Name = "Date Approved")]
        public Nullable<System.DateTime> dtmApproved { get; set; }

        [Display(Name = "Approved By")]
        public string strApproverID { get; set; }

        public IList lstUseCaseApprovals { get;  set; }
    }
}