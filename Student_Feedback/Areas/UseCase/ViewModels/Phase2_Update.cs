using Gios_mvcSolution.Areas.UseCase.Models;
using Gios_mvcSolution.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace Gios_mvcSolution.Areas.UseCase.ViewModels
{
    public class Phase2_Update
    {
        [Display(Name = "Use Case ID")]
        public int intUseCaseID { get; set; }

        [Display(Name = "Automation Category")]
        public string strAutomationCategory { get; set; }

        [Display(Name = "Last Modified")]
        public Nullable<System.DateTime> dtmModified { get; set; }
        [Display(Name = "Modified By")]
        public string strModifierID { get; set; }

        [Display(Name = "Current Status of Use Case")]
        public string status { get; set; }

        [Display(Name = "DI Number")]
        public int? intDINumber { get; set; }

        [Display(Name = " Use Case SPA")]
        public string strUseCaseSPA { get; set; }

        [Display(Name = "Use Case Title")]
        public string strTitle { get; set; }

        [Display(Name = "Problem Description")]
        public string strDescription { get; set; }

        [Display(Name = "Solution / Hypothosis")]
        public string strHypothesis { get; set; }


        public string strBUID { get; set; }

        public string strSegmentID { get; set; }

        [Display(Name = "Plant ID")]
        [Required]
        public int intPlantID { get; set; }

        public string strPlantName { get; set; }

        [Display(Name = "Product Process ID")]
        [Required]
        public int ProductProcessID { get; set; }

        public string strLineID { get; set; }

        [Display(Name = "Assests")]
        public string strAssests { get; set; }

        [Display(Name = "Team")]
        public string strTeam { get; set; }

        [Display(Name = "Idea Estimate in Thousands of US $")]
        public int? fltIdeaEstimate { get; set; }

        [Display(Name = "Impact Calculation Methodology")]
        public string strImpactCalculation { get; set; }

        [Display(Name = "ITAR Protected?")]
        public bool ysnITAR { get; set; }

        [Display(Name = "File Extension")]
        public string strFileType { get; set; }

        public byte[] Attachment { get; set; }

        [Display(Name = "Created By")]
        public string strCreatorID { get; set; }

        [Display(Name = "Required Support Needed")]
        public IList<RequiredSupportTypes> RequiredSupportTypes { get; set; }

        public IList<string> SelectedSupport { get; set; }

        [Display(Name = "Compass Category")]
        public IList<CompassCat> compassCats { get; set; }
        [Display(Name = "Compass Category")]
        public string strCategoryID { get; set; }

        public string strReqSupportOther { get; set; }

        [Display(Name = "Estimate in Thousands of US$")]
        public int? intCAPEXEstimate { get; set; }

        public IList<ImpactKPI> ImpactKPIs { get; set; }

        public IList<string> SelectedImpactKPIs { get; set; }

        public IList<FileTypes> supportedFileTypes { get; set; }

        public IList<BusinessUnit> businessUnits { get; set; }

        public IList<UseCaseAttachments> useCaseAttachments { get; set; }
        public IList<string> RemovedAttachments { get; set; }

        //USED FOR PHASE 4 VIEW ONLY
        [Display(Name = "Lessons Learned")]
        public string strLessonsLearned { get; set; }

        [Display(Name = "Total Investment")]
        [DataType(DataType.Currency)]
        public int? fltTotInvestment { get; set; }

        [Display(Name = "Technology Provider")]
        public string strTechnologyProvider { get; set; }

        //END PHASE 4 CHECKS

        public Phase2_Update()
        {
            RequiredSupportTypes = new List<RequiredSupportTypes>();
            compassCats = new List<CompassCat>();
            supportedFileTypes = new List<FileTypes>();
            businessUnits = new List<BusinessUnit>();
            useCaseAttachments = new List<UseCaseAttachments>();
            ImpactKPIs = new List<ImpactKPI>();
            SelectedSupport = new List<string>();
            SelectedImpactKPIs = new List<string>();
            RemovedAttachments = new List<string>();
        }


    }
}