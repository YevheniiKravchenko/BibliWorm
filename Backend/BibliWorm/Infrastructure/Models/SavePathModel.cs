using System.ComponentModel.DataAnnotations;

namespace BibliWorm.Infrastructure.Models;

public class SavePathModel
{
    [Required(ErrorMessage = "FIELD_IS_REQUIRED")]
    public string SavePath { get; set; }
}
