using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Gios_mvcSolution.Models
{
    public class BusinessUnit
    {
        [Display(Name = "BU")]
        [Key]
        [Required(ErrorMessage = "BU is required.")]
        public string strBUID { get; set; }
        [Display(Name = "BU Description")]
        [Required(ErrorMessage = "BU Description is required.")]
        public string strBUDescr { get; set; }
        [Required(ErrorMessage = "Group is required.")]
        [Display(Name = "Group")]
        public string strGroupID { get; set; }
        [Display(Name = "Date Created")]
        public Nullable<System.DateTime> dtmCreated { get; set; }
        [Display(Name = "Created By")]
        public string strCreatorID { get; set; }
        [Display(Name = "Last Modified")]
        public Nullable<System.DateTime> dtmModified { get; set; }
        [Display(Name = "Last Modified By")]
        public string strModifierID { get; set; }
        [NotMapped]
        public SelectList BuList { get; set; }
        [NotMapped]
        public IList BUs { get; set; }

        public bool isSelected { get; set; }

    }
}