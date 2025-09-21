
namespace WebApp.Models;

public enum ItemEstado { Presente = 0, Faltante = 1, Sobrante = 2 }

public class ChecklistItem
{
    public int Id { get; set; }
    public int ChecklistId { get; set; }
    public int InstrumentId { get; set; }
    public ItemEstado Estado { get; set; }

    public Checklist? Checklist { get; set; }
    public Instrument? Instrument { get; set; }
}
