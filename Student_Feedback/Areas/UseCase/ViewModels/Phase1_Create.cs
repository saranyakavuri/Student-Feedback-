using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Gios_mvcSolution.Areas.UseCase.Models;
using Gios_mvcSolution.Models;

namespace Gios_mvcSolution.Areas.UseCase.ViewModels
{
    public class Phase1_Create
    {
        public string status { get; set; }

        [Display(Name = "SM Number")]
        public string strSMNumber { get; set; }

        [Display(Name = " Use Case SPA")]
        public string strUseCaseSPA { get; set; }

        [Display(Name = "Use Case Title")]
        public string strTitle { get; set; }

        [Display(Name = "Problem Description")]
        public string strDescription { get; set; }

        [Display(Name = "Solution / Hypothesis")]
        public string strHypothesis { get; set; }


        public string strBUID { get; set; }

        public string strSegmentID { get; set; }

        [Display(Name = "Plant ID")]
        [Required]
        public int intPlantID { get; set; }

        [Display(Name = "Product Process ID")]
        [Required]
        public int ProductProcessID { get; set; }

        [Display(Name = "Assests")]
        public string strAssests { get; set; }

        [Display(Name = "Team")]
        public string strTeam { get; set; }

        [Display(Name = "Idea Estimate in Thousands of US $")]
        public int? fltIdeaEstimate { get; set; }

        [Display(Name = "Impact Calculation Methodology")]
        public string strImpactCalculation { get; set; }

        [Display(Name = "Does this Use Case contain Sensitive or Protected Data?")]
        public bool ysnITAR { get; set; }

        [Display(Name = "File Extension")]
        public string strFileType { get; set; }

        public byte[] Attachment { get; set; }

        [Display(Name = "Created By")]
        public string strCreatorID { get; set; }

        [Display(Name ="Required Support Needed")]
        public IList<RequiredSupportTypes> RequiredSupportTypes { get; set; }

        public IList<string> SelectedSupport { get; set; }

        [Display(Name = "Compass Category")]
        public IList<CompassCat> compassCats { get; set; }
        [Display(Name = "Compass Category")]
        public string strCategoryID { get; set; }

        public string strReqSupportOther { get; set; }

        [Display(Name ="Estimate in Thousands of US$")]
        public int intCAPEXEstimate { get; set; }

        public IList<ImpactKPI> ImpactKPIs { get; set; }

        public IList<string> SelectedImpactKPIs { get; set; }

        public IList<FileTypes> supportedFileTypes { get; set; }

        public IList<BusinessUnit> businessUnits { get; set; }

        public Phase1_Create()
        {
            RequiredSupportTypes = new List<RequiredSupportTypes>();
            compassCats = new List<CompassCat>();
            supportedFileTypes = new List<FileTypes>();
            businessUnits = new List<BusinessUnit>();
            ImpactKPIs = new List<ImpactKPI>();
            SelectedSupport = new List<string>();
            SelectedImpactKPIs = new List<string>();
            

        }
    }
}