using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gios_mvcSolution.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Competency> CompetencyList { get; set; }
    }
}