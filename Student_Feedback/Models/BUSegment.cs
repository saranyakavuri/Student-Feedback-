using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Gios_mvcSolution.Models
{
    public class BUSegment
    {
        [Display(Name = "Segment")]
        public string strSegmentID { get; set; }
        [Display(Name = "Segment Description")]
        public string strSegmentDescr { get; set; }
        [Display(Name = "BU")]
        public string strBUID { get; set; }
        [Display(Name = "Date Created")]
        public Nullable<System.DateTime> dtmCreated { get; set; }
        [Display(Name = "Created By")]
        public string strCreatorID { get; set; }
        [Display(Name = "Last Modified")]
        public Nullable<System.DateTime> dtmModified { get; set; }
        [Display(Name = "Modified By")]
        public string strModifierID { get; set; }
        [NotMapped]
        public SelectList SegmentSelectList { get; set; }

        public IList Segments { get; set; }

    }
}