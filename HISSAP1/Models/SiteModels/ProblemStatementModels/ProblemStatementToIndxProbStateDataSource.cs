using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HISSAP1.Models;
using HISSAP1.Models.SiteModels;

namespace HISSAP1.Models.SiteModels.ProblemStatementModels
{
  public class ProblemStatementToIndxProbStateDataSource
  {
    public int ProblemStatementToIndxProbStateDataSourceId { get; set; }
    public int ProblemStatementId { get; set; }
    public int IndxProbStateDataSourceId { get; set; }

    public virtual ProblemStatement ProblemStatement { get; set; }
    public virtual IndxProbStateDataSource IndxProbStateDataSource { get; set; }
  }
}