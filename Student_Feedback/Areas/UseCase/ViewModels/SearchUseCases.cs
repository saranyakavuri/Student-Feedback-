using Gios_mvcSolution.Areas.UseCase.Models;
using Gios_mvcSolution.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gios_mvcSolution.Areas.UseCase.ViewModels
{
    public class SearchUseCases
    {

        [Display(Name = "Required Support Needed")]
        public IList<RequiredSupportTypes> RequiredSupportTypes { get; set; }

        public IList<string> SelectedSupport { get; set; }

        public IList<ImpactKPI> ImpactKPIs { get; set; }

        public IList<string> SelectedImpactKPIs { get; set; }

        public IList<BusinessUnit> lstBusinessUnits { get; set; }
        public IList<BUSegment> lstSegments { get; set; }
        public IList<Plant> lstPlants { get; set; }
        public IList<Line> lstLines { get; set; }

        public IList<string> selectedBU { get; set; }
        public IList<string> selectedSegment { get; set; }
        public IList<string> selectedPlants { get; set; }


        public SearchUseCases()
        {
            RequiredSupportTypes = new List<RequiredSupportTypes>();
            SelectedSupport = new List<string>();
            ImpactKPIs = new List<ImpactKPI>();
            SelectedImpactKPIs = new List<string>();

            lstBusinessUnits = new List<BusinessUnit>();
            lstSegments = new List<BUSegment>();
            lstPlants = new List<Plant>();
            lstLines = new List<Line>();

        }
    }
}