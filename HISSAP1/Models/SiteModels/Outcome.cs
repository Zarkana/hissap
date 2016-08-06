using HISSAP1.Models.SiteModels.OutcomeModels;
using HISSAP1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HISSAP1.Models.SiteModels
{
  public class Outcome
  {
    //BASIC INFORMATION
    public int Id { get; set; }
    public string DesiredOutcome { get; set; }
    public string OutcomeDescription { get; set; }
    //Outcome Type Dropdown index
    public List<SelectListItem> OutcomeTypes { set; get; }
    public int SelectedOutcomeTypeId { set; get; }

    public float BaselinePercentage { get; set; }
    public float DesiredPercentage { get; set; }

    //DETAILS
    //Measurement Tool Dropdown index
    public List<SelectListItem> MeasurementTools { set; get; }
    public int SelectedMeasurementToolId { set; get; }

    //Measurement Type Dropdown index
    public List<SelectListItem> MeasurementTypes { set; get; }
    public int SelectedMeasurementTypeId { set; get; }
    //Staff checkbox list
    //Outcome Files

    //Navigation property
    public virtual ICollection<OutcomeToIndxOutcomeType> OutcomeToIndxOutcomeTypes { get; set; }
  }
}