
namespace WebApp.Models;

public enum ChecklistTipo { Pre = 0, Post = 1 }
public enum ChecklistResultado { OK = 0, Faltante = 1, Sobrante = 2 }

public class Checklist
{
    public int Id { get; set; }
    public int BoxId { get; set; }
    public string UsuarioId { get; set; } = "";
    public ChecklistTipo Tipo { get; set; }
    public ChecklistResultado Resultado { get; set; }
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public string? Notas { get; set; }

    public Box? Box { get; set; }
    public ICollection<ChecklistItem> Items { get; set; } = new List<ChecklistItem>();
}
