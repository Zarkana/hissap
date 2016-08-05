using HISSAP1.Models;
using HISSAP1.Models.SiteModels;
using HISSAP1.Models.SiteModels.ProblemStatementModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HISSAP1.ViewModels
{
  public class ProblemStatementViewModel
  {
    /* PROBLEM STATEMENT INFORMATION */

    public int ProblemStatementID { get; set; }
    public string Name { get; set; }

    [Required]
    [Display(Name = "Problem Statement")]
    public string ProblemStatementDescription { get; set; }

    /* DETAILS */

    [Required]
    public string Consequences { get; set; }

    [Required]
    public string Resources { get; set; }

    [Required]
    public string Gaps { get; set; }

    public List<CheckBoxViewModel> DataSources { get; set; }
  }
}