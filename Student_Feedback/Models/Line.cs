using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Gios_mvcSolution.Models
{
    public class Line
	{


        public Nullable<System.DateTime> dtmCreated { get; set; }
        public string strCreatorID { get; set; }
        public Nullable<System.DateTime> dtmModified { get; set; }
        public string strModifierID { get; set; }
        [NotMapped]
        public SelectList LineList { get; set; }
    }
}