using HISSAP1.Models.SiteModels.OutcomeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HISSAP1.Models.SiteModels
{
  public class Outcome
  {
    //BASIC INFORMATION
    public int Id { get; set; }
    public string DesiredOutcome { get; set; }
    public string OutcomeDescription { get; set; }
    //Outcome Type Dropdown index
    public float BaselinePercentage { get; set; }
    public float DesiredPercentage { get; set; }

    //DETAILS
    //Measurement Tool Dropdown index
    //Measurement Type Dropdown index

    //Staff checkbox list
    //Outcome Files

    //Navigation property
    public virtual ICollection<OutcomeToIndxOutcomeType> OutcomeToIndxOutcomeTypes { get; set; }
  }
}