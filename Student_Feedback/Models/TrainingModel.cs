using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gios_mvcSolution.Models
{
    public class TrainingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Competency competency{ get; set; }
    }
}