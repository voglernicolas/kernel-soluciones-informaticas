
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Components;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

// DB (SQLite)
var cs = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=kernel_simple.db";
builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseSqlite(cs));

// Identity (simple)
builder.Services
    .AddDefaultIdentity<IdentityUser>(opt =>
    {
        opt.SignIn.RequireConfirmedAccount = false;
        opt.Password.RequiredLength = 6;
        opt.Password.RequireNonAlphanumeric = false;
        opt.Password.RequireUppercase = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddScoped<WebApp.Services.SearchService>();

var app = builder.Build();










// ===== Instruments =====
app.MapPost("/admin/instruments/create", async (HttpContext ctx, ApplicationDbContext db) =>
{
    var f = await ctx.Request.ReadFormAsync();
    var nombre = (f["Nombre"].ToString() ?? "").Trim();
    var tipo = (f["Tipo"].ToString() ?? "").Trim();
    var codigo = (f["CodigoInterno"].ToString() ?? "").Trim();
    var estado = (f["Estado"].ToString() ?? "Activo").Trim();

    if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(tipo) || string.IsNullOrWhiteSpace(codigo))
        return Results.Redirect("/admin/instruments?error=Campos");

    if (await db.Instruments.AnyAsync(i => i.CodigoInterno == codigo))
        return Results.Redirect("/admin/instruments?error=Duplicado");

    db.Instruments.Add(new Instrument { Nombre = nombre, Tipo = tipo, CodigoInterno = codigo, Estado = estado });
    await db.SaveChangesAsync();
    return Results.Redirect("/admin/instruments");
}).RequireAuthorization().DisableAntiforgery();

app.MapPost("/admin/instruments/delete", async (HttpContext ctx, ApplicationDbContext db) =>
{
    var f = await ctx.Request.ReadFormAsync();
    if (int.TryParse(f["Id"], out var id))
    {
        var e = await db.Instruments.FindAsync(id);
        if (e is not null) { db.Remove(e); await db.SaveChangesAsync(); }
    }
    return Results.Redirect("/admin/instruments");
}).RequireAuthorization().DisableAntiforgery();

// ===== Boxes =====
app.MapPost("/admin/boxes/create", async (HttpContext ctx, ApplicationDbContext db) =>
{
    var f = await ctx.Request.ReadFormAsync();
    var nombre = (f["Nombre"].ToString() ?? "").Trim();
    var esp = (f["Especialidad"].ToString() ?? "").Trim();
    var activo = (f["Activo"].ToString() ?? "false") == "true";

    if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(esp))
        return Results.Redirect("/admin/boxes?error=Campos");

    db.Boxes.Add(new Box { Nombre = nombre, Especialidad = esp, Activo = activo });
    await db.SaveChangesAsync();
    return Results.Redirect("/admin/boxes");
}).RequireAuthorization().DisableAntiforgery();

app.MapPost("/admin/boxes/delete", async (HttpContext ctx, ApplicationDbContext db) =>
{
    var f = await ctx.Request.ReadFormAsync();
    if (int.TryParse(f["Id"], out var id))
    {
        var e = await db.Boxes.FindAsync(id);
        if (e is not null) { db.Remove(e); await db.SaveChangesAsync(); }
    }
    return Results.Redirect("/admin/boxes");
}).RequireAuthorization().DisableAntiforgery();

// ===== BoxItems (agregar/quitar) =====
app.MapPost("/admin/boxitems/add", async (HttpContext ctx, ApplicationDbContext db) =>
{
    var f = await ctx.Request.ReadFormAsync();
    if (!int.TryParse(f["BoxId"], out var boxId)) return Results.Redirect("/admin/boxitems");
    if (!int.TryParse(f["InstrumentId"], out var instId)) return Results.Redirect($"/admin/boxitems?boxId={boxId}");
    if (!int.TryParse(f["Cantidad"], out var cant)) cant = 1;

    // ver si ya existe el par Box–Instrument
    var existing = await db.BoxItems.FirstOrDefaultAsync(x => x.BoxId == boxId && x.InstrumentId == instId);
    if (existing is null)
        db.BoxItems.Add(new BoxItem { BoxId = boxId, InstrumentId = instId, Cantidad = Math.Max(1, cant) });
    else
        existing.Cantidad += Math.Max(1, cant);

    await db.SaveChangesAsync();
    return Results.Redirect($"/admin/boxitems?boxId={boxId}");
}).RequireAuthorization().DisableAntiforgery();

app.MapPost("/admin/boxitems/remove", async (HttpContext ctx, ApplicationDbContext db) =>
{
    var f = await ctx.Request.ReadFormAsync();
    int.TryParse(f["BoxId"], out var boxId);
    if (int.TryParse(f["Id"], out var id))
    {
        var e = await db.BoxItems.FindAsync(id);
        if (e is not null) { db.Remove(e); await db.SaveChangesAsync(); }
    }
    return Results.Redirect($"/admin/boxitems?boxId={boxId}");
}).RequireAuthorization().DisableAntiforgery();









if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

// Asegurar DB creada
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await db.Database.EnsureCreatedAsync();
}

app.MapRazorPages();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

// Export CSV
app.MapGet("/boxes/{id:int}/export", async (int id, ApplicationDbContext db) =>
{
    var box = await db.Boxes.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
    if (box is null) return Results.NotFound();

    var items = await db.BoxItems
        .AsNoTracking()
        .Where(x => x.BoxId == id)
        .Include(x => x.Instrument)
        .OrderBy(x => x.Instrument!.Nombre)
        .ToListAsync();

    var sb = new System.Text.StringBuilder();
    sb.AppendLine("Caja,Especialidad,Instrumento,Tipo,Codigo,Cantidad");
    foreach (var it in items)
        sb.AppendLine($"{box.Nombre},{box.Especialidad},{it.Instrument!.Nombre},{it.Instrument!.Tipo},{it.Instrument!.CodigoInterno},{it.Cantidad}");
    var bytes = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
    return Results.File(bytes, "text/csv", $"box_{id}.csv");
}).RequireAuthorization();


app.Run();
