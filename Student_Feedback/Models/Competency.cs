using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gios_mvcSolution.Models
{
    public class Competency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LevelId { get; set; }
        public List<Question> QuestionList { get; set; }
    }
}