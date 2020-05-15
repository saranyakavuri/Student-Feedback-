using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Gios_mvcSolution.Models
{
    public class Location
    {
        public int Location_Id { get; set; }
        public string strLocationID { get; set; }
        public string strSegmentID { get; set; }
        public string strLocationDescr { get; set; }
        public Nullable<System.DateTime> dtmCreated { get; set; }
        public string strCreatorID { get; set; }
        public Nullable<System.DateTime> dtmModified { get; set; }
        public string strModifierID { get; set; }
        [NotMapped]
        public SelectList LocList { get; set; }
    }
}