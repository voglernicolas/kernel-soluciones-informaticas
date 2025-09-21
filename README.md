
# Kernel Instrumental — SIMPLE (sin estilos)

- Blazor Web App (.NET 9)
- Identity (Login/Registro) por librería
- SQLite (`kernel_simple.db`) sin migraciones (EnsureCreated)
- Páginas: ABM Instrumentos, Cajas, Contenido; Búsqueda; Detalle + Export CSV
- Sin CSS/clases de estilos

## Ejecutar
```powershell
cd KernelInstrumental_Simple_NoStyles
dotnet restore KernelInstrumental.sln
dotnet build   KernelInstrumental.sln
dotnet run --project WebApp/WebApp.csproj
```
Se levanta en `http://127.0.0.1:5007` (perfil de launch incluido).

## Rutas
- Registro/Login: `/Identity/Account/Register` y `/Identity/Account/Login`
- Inicio: `/`
- Instrumentos: `/admin/instruments`
- Cajas: `/admin/boxes`
- Contenido: `/admin/boxitems`
- Búsqueda: `/search`
- Detalle: `/boxes/{id}` y CSV: `/boxes/{id}/export`
