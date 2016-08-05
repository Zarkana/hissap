using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HISSAP1.Models.SiteModels.OutcomeModels
{
  public class IndxOutcomeType
  {
    public int IndxOutcomeTypeID { get; set; }
    public string Name { get; set; }

    //Navigation property
    public virtual ICollection<OutcomeToIndxOutcomeType> OutcomeToIndxOutcomeTypes { get; set; }
  }
}