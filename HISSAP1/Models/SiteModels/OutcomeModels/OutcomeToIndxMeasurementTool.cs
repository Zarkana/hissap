using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HISSAP1.Models.SiteModels.OutcomeModels
{
  public class OutcomeToIndxMeasurementTool
  {
    public int OutcomeToIndxMeasurementToolID { get; set; }
    public int OutcomeID { get; set; }
    public int IndxMeasurementToolID { get; set; }

    public virtual Outcome Outcome { get; set; }
    public virtual IndxMeasurementTool IndxMeasurementTool { get; set; }
  }
}