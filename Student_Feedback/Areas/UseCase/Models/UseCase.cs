using Gios_mvcSolution.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gios_mvcSolution.Areas.UseCase.Models
{
    public class UseCase
    {
        [Key]
        [Display(Name ="Use Case Number")]
        [Required]
        public int intUseCaseID { get; set; }

        [Display(Name ="SM Number")]
        public string strSMNumber { get; set; }

        [Display(Name ="Plant ID")]
        public int intPlantID { get; set; }

        [Display(Name = "Product Process ID")]
        public int ProductProcessID { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Category ID")]
        public string strCategoryID { get; set; }

        [Display(Name = " Use Case SPA")]
        public string strUseCaseSPA { get; set; }

        [Display(Name = "Use Case Title")]
        public string strTitle { get; set; }

        [Display(Name = "Use Case Description")]
        public string strDescription { get; set; }

        [Display(Name = "Use Case Hypothosis")]
        public string strHypothesis { get; set; }

        [Display(Name = "Assests")]
        public string strAssests { get; set; }

        [Display(Name = "Team")]
        public string strTeam { get; set; }

        [Display(Name = "Idea Estimate")]
        [DataType(DataType.Currency)]
        public int? fltIdeaEstimate { get; set; }

        [Display(Name = "Impact Calculation Methodology")]
        public string strImpactCalculation { get; set; }

        public string strReqSupportOther { get; set; }

        public int? intCAPEXEstimate { get; set; }

        [Display(Name = "ITAR Protected?")]
        public bool ysnITAR { get; set; }

        [Display(Name = "Automation Category")]
        public string strAutomationCategory { get; set; }

        [Display(Name ="DI Number")]
        public int? intDINumber { get; set; }

        [Display(Name = "Lessons Learned")]
        public string strLessonsLearned { get; set; }

        [Display(Name = "Total Investment")]
        [DataType(DataType.Currency)]
        public int? fltTotInvestment { get; set; }
        
        [Display(Name = "Technology Provider")]
        public string strTechnologyProvider { get; set; }

        [Display(Name = "Date Created")]
        public Nullable<System.DateTime> dtmCreated { get; set; }
        [Display(Name = "Created By")]
        public string strCreatorID { get; set; }
        [Display(Name = "Last Modified")]
        public Nullable<System.DateTime> dtmModified { get; set; }
        [Display(Name = "Modified By")]
        public string strModifierID { get; set; }

        public IList lstUseCases { get; set; }

    }
}