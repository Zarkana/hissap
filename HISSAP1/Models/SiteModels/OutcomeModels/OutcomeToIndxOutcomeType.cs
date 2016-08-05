using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HISSAP1.Models.SiteModels.OutcomeModels
{
  public class OutcomeToIndxOutcomeType
  {
    public int OutcomeToIndxOutcomeTypeID { get; set; }
    public int OutcomeID { get; set; }
    public int IndxOutcomeTypeID { get; set; }

    public virtual Outcome Outcome { get; set; }
    public virtual IndxOutcomeType IndxOutcomeType { get; set; }
  }
}