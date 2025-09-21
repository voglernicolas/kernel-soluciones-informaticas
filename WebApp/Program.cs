using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Components;

var builder = WebApplication.CreateBuilder(args);

// DB (SQLite)
var cs = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=kernel_simple.db";
builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseSqlite(cs));

// Identity (sin roles)
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

// no funcionaba con https - cambiar
//app.UseHttpsRedirection();
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

// Endpoints utilitarios
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

app.MapRazorPages();
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
