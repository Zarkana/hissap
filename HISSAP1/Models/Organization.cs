﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HISSAP1.Models
{
  public class Organization
  {
    public int ID { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    [Display(Name = "Address 1st Line")]
    public string Line1 { get; set; }
    [Display(Name = "Address 2nd Line")]
    public string Line2 { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string State { get; set; }
    [Required]
    [RegularExpression(@"^\d{5}(-\d{4})?", ErrorMessage = "Please enter a valid US Zip")]
    public string Zip { get; set; }
    [Display(Name = "Contact Name")]
    public string ContactPerson { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [Phone]    
    [RegularExpression(@"[\(]\d{3}[\)]-?\d{3}[\-]\d{4}", ErrorMessage= "Format: (999)-999-9999")]
    public string Phone { get; set; }
    [Url]
    public string Website { get; set; }
  }
}