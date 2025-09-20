# Kernel Instrumental Quirúrgico
**Sistema de Gestión de Instrumental Quirúrgico — Trabajo Final**

## Descripción
Proyecto correspondiente al **Trabajo Final de la Tecnicatura Superior en Análisis de Sistemas**.  
Objetivo: desarrollar una aplicación interna para optimizar la **gestión del instrumental quirúrgico** del **Sanatorio Finochietto**, reemplazando planillas en papel por un sistema digital confiable y escalable.

## Alcance del MVP
- Inventario digital de instrumentos y cajas.  
- Búsqueda rápida de instrumental por nombre, tipo o código interno.  
- Consulta detallada del contenido de cada caja.  
- Checklists digitales pre y post cirugía.  
- Gestión de usuarios y **roles** (Circulante, Coordinación, Jefatura).  
- Exportación e impresión de listados (CSV / vista preparada para impresión).

## Mejoras previstas (fuera del MVP)
- **AuditLog** (trazabilidad detallada de cambios, con usuario, acción y marca temporal).  
- **Importación masiva** desde CSV/XLSX con previsualización.  
- **Reportes y dashboards** (uso de instrumental, indicadores operativos).  
- **Actualización en tiempo real** mediante SignalR.

---

## Stack tecnológico
- **C# con Blazor Server (.NET 8)**: frontend y backend en un mismo proyecto.  
- **Entity Framework Core**: acceso a datos con LINQ y migraciones.  
- **SQLite** (MVP): base liviana y de rápida configuración.  
- **ASP.NET Identity**: autenticación y autorización basada en roles.  
- **Logging nativo .NET**: registro básico de eventos y errores.

> Alternativa futura de base de datos: **SQL Server Express**, sin cambios sustanciales en la lógica de negocio.

---

## Arquitectura (resumen)
- **Blazor Components** para las pantallas principales: Búsqueda, Detalle de Caja, Checklist, Administración (ABM).  
- **EF Core** para el mapeo de entidades y consultas: *Instrument*, *Box*, *BoxItem* (relación M:N), *Checklist*.  
- **ASP.NET Identity** para control de acceso y autorización por roles.  
- Endpoints mínimos para **exportación CSV** y **descargas**.

---

## Estructura del proyecto (sugerida)
- KernelInstrumental/ (solución)
  - README.md  
  - KernelInstrumental.sln  
  - KernelInstrumental/ (proyecto Blazor Server)  
    - Pages/ (componentes .razor)  
    - Data/ (DbContext, configuración EF, migraciones)  
    - Models/ (entidades de dominio)  
    - Services/ (lógica de aplicación)  
    - Areas/Identity/ (autenticación y roles)  
    - wwwroot/ (recursos estáticos)  
    - Program.cs  
    - appsettings.json (cadena de conexión a SQLite)

---

## Funcionalidades
1. **Autenticación y roles**  
   - *Circulante*: acceso de solo lectura.  
   - *Coordinación* y *Jefatura*: edición y gestión del inventario y relaciones.

2. **Búsqueda de instrumental**  
   - Filtros por nombre, tipo o código interno.  
   - Listado de **cajas** que contienen el instrumento.

3. **Detalle de caja**  
   - Visualización completa del contenido.  
   - Exportación a **CSV** y vista preparada para **impresión**.

4. **Checklist pre/post**  
   - Registro digital del chequeo de instrumentos (presentes/faltantes/sobrantes).  
   - Asociación del registro al usuario autenticado y marca temporal.

5. **ABM (Alta/Baja/Modificación)**  
   - Instrumentos, cajas y relaciones *BoxItem*.  
   - Acceso restringido a Coordinación/Jefatura.

---

## Puesta en marcha (desarrollo local)

**Requisitos**  
- .NET 8 SDK  
- Git

**Pasos**
1. Clonar el repositorio (`git clone <URL_DEL_REPOSITORIO>`) y acceder al proyecto (`cd KernelInstrumental/KernelInstrumental`).  
2. Configurar la base de datos (SQLite) en `appsettings.json` si corresponde.  
3. Compilar y, en caso de existir migraciones, actualizar la base (`dotnet build` y `dotnet ef database update`).  
4. Ejecutar la aplicación (`dotnet run`) y acceder a la URL indicada por la consola.

---

## Políticas de acceso
- El acceso a funcionalidades de edición se controla mediante **roles** en ASP.NET Identity.  
- Las páginas y componentes sensibles se protegen con autorizaciones específicas.  
- Se recomienda crear al menos un usuario con rol **Jefatura** para la administración inicial.

---

## Plan de trabajo (MVP)
1. Semana 1: proyecto base, modelos y configuración de SQLite.  
2. Semana 2: pantallas de Búsqueda y Detalle de Caja.  
3. Semana 3: Checklist y ABM.  
4. Semana 4: exportación/impresión y pruebas internas.  
5. Semana 5: validación con usuarios, capacitación breve y demostración.

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
Uso académico.
