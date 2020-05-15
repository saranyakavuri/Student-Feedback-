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
    public class Plant
    {
        [Display(Name = "Plant Num")]
        public int intPlantID { get; set; }

        [Display(Name = "Plant Name")]
        public string strPlantName { get; set; }

        [Display(Name = "Segment Name")]
        public string strSegmentID { get; set; }

        [Display(Name = "Plant Description")]
        public string strPlantDescr { get; set; }

        [Display(Name ="Location ID")]
        public string strLocationID { get; set; }

        [Display(Name ="Date Created")]
        public Nullable<System.DateTime> dtmCreated { get; set; }

        [Display(Name = "Created By")]
        public string strCreatorID { get; set; }

        [Display(Name = "Last Modified")]
        public Nullable<System.DateTime> dtmModified { get; set; }

        [Display(Name = "Last Modified By")]
        public string strModifierID { get; set; }

        [NotMapped]
        public SelectList PlantList { get; set; }

        [NotMapped]
        public IList Plants { get; set; }
    }
}