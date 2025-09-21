
namespace WebApp.Models;

public class Box
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Especialidad { get; set; } = "";
    public bool Activo { get; set; } = true;

    public ICollection<BoxItem> Items { get; set; } = new List<BoxItem>();
    public ICollection<Checklist> Checklists { get; set; } = new List<Checklist>();
}
