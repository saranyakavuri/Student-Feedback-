using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gios_mvcSolution.Models
{
    public class Survey
    {
        public int Id { get; set; }
        public string TakenBy { get; set; }
        public int RoleId { get; set; }//any positive value will indicate role was selected.<=0 will indicate it is null
        public int CompetencyId { get; set; }
        public List<Competency> CompetencyList { get; set; }
        public bool TakeAsAGuest { get; set; }
        public string TakeOn { get; set; }
        public int LevelId { get; set; }
        public string LevelName { get; set; }
    }
}