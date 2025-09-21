
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class Instrument
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    public string Nombre { get; set; } = "";

    [Required, StringLength(80)]
    public string Tipo { get; set; } = "";

    [Required, StringLength(80)]
    public string CodigoInterno { get; set; } = "";

    [StringLength(40)]
    public string Estado { get; set; } = "Activo";

    public ICollection<BoxItem> Boxes { get; set; } = new List<BoxItem>();
    public ICollection<ChecklistItem> ChecklistItems { get; set; } = new List<ChecklistItem>();
}
