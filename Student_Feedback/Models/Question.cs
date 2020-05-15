using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gios_mvcSolution.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionStatement {get; set; }
        public bool? Answer { get; set; }
        public bool? Relevant { get; set; }
    }
}