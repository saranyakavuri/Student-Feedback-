using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gios_mvcSolution.Areas.UseCase.ViewModels
{
    public class Create_ViewModel
    {
        [Display(Name = "Title")]
        public string title { get; set; }
        [Display(Name = "Use Case Number")]
        public int UCNumber { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "Description")]
        public string UCDescr { get; set; }
        [Display(Name = "Business Unit")]
        public string BUID { get; set; }
        [Display(Name = "Location")]
        public string LocID { get; set; }
        [Display(Name = "Process")]
        public string ProcID { get; set; }
        [Display(Name = "DI Number")]
        public int? DINumber { get; set; }
        [Display(Name = "Sensitive")]
        public string Sensitive { get; set; }
        [Display(Name = "Leassons Learned")]
        public string LessonsLearned { get; set; }
    }
}