
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class Box
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    public string Nombre { get; set; } = "";

    [Required, StringLength(120)]
    public string Especialidad { get; set; } = "";

    public bool Activo { get; set; } = true;

    public ICollection<BoxItem> Items { get; set; } = new List<BoxItem>();
    public ICollection<Checklist> Checklists { get; set; } = new List<Checklist>();
}
