﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
namespace ApiExample.Models
{
  public class Book
  {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] // BookId --> is a Primary Key, is Identity (auto incrementing by 1).
    public int BookId { get; set; }
    [Required, Column(TypeName = "varchar"), MaxLength(50)] // Title --> is Required, Datatype is varchar, max length is 50 char.
    public string Title { get; set; } = string.Empty;
  }

  // [ForeignKey("Category")]
  // public int CatId { get; set; } // By Convention --> Foreign Key. 'ClassNameId'
  // [InverseProperty("Books"), AllowNull, DeleteBehavior(DeleteBehavior.Restrict)]
  // public virtual Category Category { get; set; } // Navegational Property: 1.
}