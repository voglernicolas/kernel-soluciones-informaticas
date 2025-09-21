
namespace WebApp.Models;

public class BoxItem
{
    public int Id { get; set; }
    public int BoxId { get; set; }
    public int InstrumentId { get; set; }
    public int Cantidad { get; set; } = 1;

    public Box? Box { get; set; }
    public Instrument? Instrument { get; set; }
}
