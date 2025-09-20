# Kernel Instrumental Quirúrgico
**Sistema de Gestión de Instrumental Quirúrgico — Trabajo Final**

---

## Resumen ejecutivo
Aplicación interna destinada a optimizar la **gestión del instrumental quirúrgico** en el **Sanatorio Finochietto**, sustituyendo planillas en papel por un sistema digital confiable, trazable y escalable. Este repositorio implementa un **MVP** funcional sobre **.NET 8 (Blazor Server)** con **Entity Framework Core** y **SQLite**.

---

## Contenidos
- [Alcance del MVP](#alcance-del-mvp)
- [Mejoras previstas (post‑MVP)](#mejoras-previstas-postmvp)
- [Tecnologías](#tecnologías)
- [Arquitectura funcional](#arquitectura-funcional)
- [Estructura del repositorio](#estructura-del-repositorio)
- [Funcionalidades clave](#funcionalidades-clave)
- [Roles y permisos](#roles-y-permisos)
- [Puesta en marcha (desarrollo local)](#puesta-en-marcha-desarrollo-local)
- [Plan de trabajo](#plan-de-trabajo)
- [Equipo](#equipo)
- [Licencia](#licencia)

---

## Alcance del MVP
- Inventario digital de **instrumentos** y **cajas**.
- Búsqueda rápida por **nombre**, **tipo** o **código interno**.
- Consulta detallada del contenido de cada caja.
- **Checklists** digitales **pre** y **post** cirugía.
- Gestión de **usuarios** y **roles** (Circulante, Coordinación, Jefatura).
- Exportación de listados (**CSV**) y **vista preparada para impresión**.

## Mejoras previstas (post‑MVP)
- **AuditLog**: trazabilidad detallada de cambios (usuario, acción, marca temporal).
- **Importación masiva** desde **CSV/XLSX** con previsualización.
- **Reportes y dashboards** (indicadores de uso y eficiencia).
- **Actualización en tiempo real** mediante **SignalR**.

---

## Tecnologías
- **C# + Blazor Server (.NET 8)** — frontend y backend unificados.
- **Entity Framework Core** — acceso a datos con LINQ y migraciones.
- **SQLite** — base de datos para el MVP (simple, sin configuración adicional).
- **ASP.NET Identity** — autenticación y autorización basada en roles.
- **Logging nativo .NET** — registro básico de eventos y errores.
> Alternativa futura: **SQL Server Express** (mayor concurrencia sin cambios significativos de lógica).

---

## Arquitectura funcional
- **Blazor Components** para las pantallas principales: Búsqueda, Detalle de Caja, Checklist, Administración (ABM).
- **EF Core** para el mapeo y consultas de entidades: *Instrument*, *Box*, *BoxItem* (M:N), *Checklist*.
- **Identity** para control de acceso y autorización por roles.
- Endpoints mínimos para **exportación CSV** y descargas.
- Separación lógica por capas: **Presentación** (Blazor), **Aplicación** (Servicios), **Persistencia** (EF Core).

---

## Estructura del repositorio
- **KernelInstrumental/** (solución)
  - **README.md**
  - **KernelInstrumental.sln**
  - **KernelInstrumental/** (proyecto Blazor Server)
    - **Pages/** (componentes *.razor*)
    - **Data/** (*DbContext*, configuración EF, migraciones)
    - **Models/** (entidades de dominio)
    - **Services/** (lógica de aplicación)
    - **Areas/Identity/** (autenticación y roles)
    - **wwwroot/** (recursos estáticos)
    - **Program.cs**
    - **appsettings.json** (cadena de conexión SQLite)

---

## Funcionalidades clave
1. **Autenticación y roles**
   - Circulante: acceso de solo lectura.
   - Coordinación y Jefatura: edición y gestión del inventario y relaciones.
2. **Búsqueda de instrumental**
   - Filtros por nombre, tipo o código interno.
   - Listado de **cajas** donde se encuentra el instrumento.
3. **Detalle de caja**
   - Contenido completo y acciones de **exportación CSV** y **impresión**.
4. **Checklist pre/post**
   - Registro digital de presentes/faltantes/sobrantes asociado al usuario autenticado.
5. **ABM (Alta/Baja/Modificación)**
   - Instrumentos, cajas y relaciones **BoxItem** (acceso restringido).

---

## Roles y permisos
| Rol           | Lectura | Edición/ABM | Checklists | Administración de usuarios |
|---------------|:------:|:-----------:|:----------:|:--------------------------:|
| Circulante    |   ✔    |      ✖      |     ✔      |             ✖              |
| Coordinación  |   ✔    |      ✔      |     ✔      |             ✖              |
| Jefatura      |   ✔    |      ✔      |     ✔      |             ✔              |

---

## Puesta en marcha (desarrollo local)
**Requisitos**
- .NET 8 SDK
- Git

**Procedimiento**
1. Clonar el repositorio y acceder al proyecto de la aplicación.
2. Revisar la cadena de conexión a **SQLite** en `appsettings.json` si corresponde.
3. Compilar y, en caso de existir migraciones, actualizar la base de datos.
4. Ejecutar la aplicación y acceder a la URL indicada por la consola.

Comandos de referencia:
- `git clone <URL_DEL_REPOSITORIO>`  
- `cd KernelInstrumental/KernelInstrumental`  
- `dotnet build`  
- `dotnet ef database update` *(si existen migraciones)*  
- `dotnet run`

---

## Plan de trabajo
1. **Semana 1** — Proyecto base, modelos y configuración de SQLite.  
2. **Semana 2** — Pantallas de Búsqueda y Detalle de Caja.  
3. **Semana 3** — Checklist y ABM.  
4. **Semana 4** — Exportación/Impresión y pruebas internas.  
5. **Semana 5** — Validación con usuarios, capacitación breve y demostración.

---

## Equipo
**Kernel Soluciones Informáticas**  
- Ahmed Camila  
- Aldecoa Florencia  
- Bravo Alejandro  
- Olivera Alejandro  
- Vogler Nicolás

---

## Licencia
Uso académico — Proyecto Final Tecnicatura Superior en Análisis de Sistemas.
