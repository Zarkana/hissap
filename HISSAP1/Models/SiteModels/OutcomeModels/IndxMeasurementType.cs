using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HISSAP1.Models.SiteModels.OutcomeModels
{
  public class IndxMeasurementType
  {
    public int IndxMeasurementTypeID { get; set; }
    public string Name { get; set; }

    //Navigation property
    public virtual ICollection<OutcomeToIndxMeasurementType> OutcomeToIndxMeasurementTypes { get; set; }
  }
}