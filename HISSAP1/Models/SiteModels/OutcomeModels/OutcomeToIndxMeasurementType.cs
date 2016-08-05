using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HISSAP1.Models.SiteModels.OutcomeModels
{
  public class OutcomeToIndxMeasurementType
  {
    public int OutcomeToIndxMeasurementTypeID { get; set; }
    public int OutcomeID { get; set; }
    public int IndxMeasurementTypeID { get; set; }

    public virtual Outcome Outcome { get; set; }
    public virtual IndxMeasurementType IndxMeasurementType { get; set; }
  }
}