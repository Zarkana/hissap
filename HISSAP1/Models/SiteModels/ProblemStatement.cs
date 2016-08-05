using HISSAP1.Models.SiteModels.ProblemStatementModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HISSAP1.Models.SiteModels
{
  public class ProblemStatement
  {
    /* PROBLEM STATEMENT INFORMATION */
    public int Id { get; set; }

    [Required]
    public int ProblemStatementsSiteId { get; set; }

    [ForeignKey("ProblemStatementsSiteId")]//Foreign Key to the parent Site
    public virtual Site ProblemStatementsSite { get; set; }

    //TODO: Make a function for setting date created in controller
    //[Display(Name = "Date Created")]
    //public DateTime DateCreated { get; set; }

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

    //Navigation property
    public virtual ICollection<ProblemStatementToIndxProbStateDataSource> ProblemStatementToIndxProbStateDataSources { get; set; }
  }

  //public class ProblemStatementFile
  //{
  //    public Guid Id { get; set; }
  //    public string BudgetFileName { get; set; }
  //    public string Extension { get; set; }
  //    public int RecepitId { get; set; }
  //    //TODO: implement
  //    //public string title { get; set; }
  //    //public string note { get; set; }
  //    public virtual Budget Budget { get; set; }
  //}
}