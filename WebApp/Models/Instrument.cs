
namespace WebApp.Models;

public class Instrument
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Tipo { get; set; } = "";
    public string CodigoInterno { get; set; } = "";
    public string Estado { get; set; } = "Activo";

    public ICollection<BoxItem> Boxes { get; set; } = new List<BoxItem>();
    public ICollection<ChecklistItem> ChecklistItems { get; set; } = new List<ChecklistItem>();
}
