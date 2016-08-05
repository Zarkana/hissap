using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HISSAP1.Models.SiteModels.ProblemStatementModels
{
  public class IndxProbStateDataSource
  {
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    //Navigation property
    public virtual ICollection<ProblemStatementToIndxProbStateDataSource> ProblemStatementToIndxProbStateDataSources { get; set; }
  }
}