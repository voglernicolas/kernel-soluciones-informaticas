using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Services;

public record BoxSummary(int BoxId, string Nombre, string Especialidad, int Cantidad);
public record InstrumentSearchResult(int InstrumentId, string Nombre, string Tipo, string CodigoInterno, List<BoxSummary> Cajas);

public class SearchService
{
    private readonly ApplicationDbContext _db;
    public SearchService(ApplicationDbContext db) => _db = db;

    public async Task<List<InstrumentSearchResult>> SearchAsync(string? term, CancellationToken ct = default)
    {
        term = (term ?? string.Empty).Trim();
        var instrumentsQuery = _db.Instruments.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(term))
        {
            var like = $"%{term}%";
            instrumentsQuery = instrumentsQuery.Where(i =>
                EF.Functions.Like(i.Nombre, like) ||
                EF.Functions.Like(i.Tipo, like) ||
                i.CodigoInterno == term);
        }

        var instruments = await instrumentsQuery
            .OrderBy(i => i.Nombre)
            .Take(100)
            .ToListAsync(ct);

        if (instruments.Count == 0) return new();

        var ids = instruments.Select(i => i.Id).ToList();

        var links = await _db.BoxItems
            .AsNoTracking()
            .Where(bi => ids.Contains(bi.InstrumentId))
            .Include(bi => bi.Box)
            .ToListAsync(ct);

        var linkLookup = links.GroupBy(bi => bi.InstrumentId)
                              .ToDictionary(g => g.Key, g => g.Select(bi =>
                                new BoxSummary(bi.BoxId, bi.Box!.Nombre, bi.Box!.Especialidad, bi.Cantidad)).ToList());

        var results = instruments.Select(i =>
            new InstrumentSearchResult(
                i.Id,
                i.Nombre,
                i.Tipo,
                i.CodigoInterno,
                linkLookup.TryGetValue(i.Id, out var cajas) ? cajas : new List<BoxSummary>())
        ).ToList();

        return results;
    }
}