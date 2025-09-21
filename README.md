# Kernel Instrumental — README (MVP)

Gestión simple de **Instrumentos**, **Cajas** y **Contenido de Cajas** con autenticación.  
Arquitectura minimalista para un MVP rápido y entendible por todo el equipo.

---

## Stack / Arquitectura

- **.NET 9 (ASP.NET Core + Blazor SSR)** — renderizado del lado del servidor (sin modo interactivo).
- **Formularios HTML** → **Minimal APIs (POST)** → **EF Core** → **SQLite**.
- **Identity** para registro/login/logout.
- **SQLite** con **EnsureCreated** (sin migraciones por ahora).
- **Sin estilos/CSS** (intencional, para simplicidad del MVP).

Diagrama breve:
```
[ UI (Razor/SSR) ]
   └─ (HTML form POST) → [ Minimal APIs ]
                           └─ EF Core → [ SQLite (kernel_simple.db) ]
```

---

## Requerimientos

- **.NET SDK 9.x**
- **Editor**: Visual Studio / VS Code / Rider (a elección)
- **Git** (opcional, si vas a clonar)
- **DB Browser for SQLite** (opcional, para inspeccionar la base)

---

## Configuración

- Cadena de conexión en `WebApp/appsettings.json`:
  ```json
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=kernel_simple.db"
  }
  ```
- La primera ejecución crea la base **`kernel_simple.db`** automáticamente (EnsureCreated).
---

## Ejecución

```bash
dotnet restore KernelInstrumental.sln
dotnet build   KernelInstrumental.sln
dotnet run --project WebApp/WebApp.csproj
```
- Perfil dev por defecto: **http://127.0.0.1:5007**

---

## Rutas

- **Registro / Login**  
  `/Identity/Account/Register`, `/Identity/Account/Login`
- **Inicio**  
  `/`
- **Instrumentos (Alta/Baja + listado)**  
  `/admin/instruments`
- **Cajas (Alta/Baja + listado)**  
  `/admin/boxes`
- **Contenido de Cajas** (elegir caja, agregar/quitar instrumentos; consolida cantidades)  
  `/admin/boxitems`
- **Búsqueda** (por nombre/tipo/código con links al detalle)  
  `/search`
- **Detalle de Caja**  
  `/boxes/{id}`

> Las rutas de administración requieren **usuario autenticado**.

---

## Cómo funciona (resumen)

1. Las páginas SSR muestran **formularios HTML**.
2. Cada **POST** va a una **Minimal API** (por ejemplo, `/admin/instruments/create`).
3. El endpoint usa `ApplicationDbContext` (EF Core) para guardar y hace **Redirect** al listado (patrón **POST-Redirect-GET**).
4. La página se vuelve a renderizar con los datos actualizados.

---

## Base de datos (SQLite)

- Tablas esperables en el MVP:  
  **`Instruments`**, **`Boxes`**, **`BoxItems`**, tablas de **Identity** (`AspNet*`) y `sqlite_sequence`.
- Esquema mínimo (lógico):
  ```
  Instrument ──< BoxItem >── Box
                (Cantidad)
  ```

---

## Seguridad

- Páginas de ABM protegidas con **[Authorize]** (requiere login).
- **Antiforgery** (CSRF): activo a nivel global; en el MVP algunos endpoints de formularios lo tienen deshabilitado para simplificar. En la siguiente iteración se añadirá el **token** a los formularios y se reactivará.

---

## Estado actual

- Autenticación (registro/login/logout) lista.
- Instrumentos y Cajas: **crear / eliminar** y listar.
- Contenido de Cajas: **agregar / quitar**; si agregás el mismo instrumento, **suma** cantidad.
- Búsqueda y **detalle** de caja.
- Sin dependencias externas (SQLite embebido).

---

## Roadmap corto

1. **Antiforgery**: agregar token en formularios y reactivar en endpoints.
2. **Editar (Update)** en Instrumentos y Cajas (CRUD completo).
3. **Checkbox “Activa”**:
   - Cajas: parseo robusto del checkbox en el endpoint.
   - Instrumentos: mismo checkbox mapeado a `Estado` (“Activo”/“Inactivo”).
4. **Roles** (Admin/Operador) y **estilos** mínimos.
5. Cuando el esquema se estabilice: pasar de **EnsureCreated** a **Migraciones EF**.

---

## Prueba rápida

1. Registrate y logueate.  
2. Crea 2 **Instrumentos** y 1–2 **Cajas**.  
3. En **Contenido**, elegí una caja y agregá/quita instrumentos (probá repetir uno para ver la **consolidación**).  
4. Buscá por nombre/tipo/código y navegá al **detalle** de la caja.  
