using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gios_mvcSolution.Areas.UseCase.Models
{
    public class CompassCat
    {
        [Key]
        [Display(Name="Category Name")]
        [Required]
        public string strCategoryID { get; set; }

        [Display(Name ="Category Description")]
        [Required]
        public string strCategoryDescr { get; set; }
        
        [Display(Name = "Date Created")]
        public Nullable<System.DateTime> dtmCreated { get; set; }
        [Display(Name = "Created By")]
        public string strCreatorID { get; set; }
        [Display(Name = "Last Modified")]
        public Nullable<System.DateTime> dtmModified { get; set; }
        [Display(Name = "Modified By")]
        public string strModifierID { get; set; }

        [NotMapped]
        public SelectList CategoryDDL { get; set; }

        public IList CategoryList { get; set; }
    }
}