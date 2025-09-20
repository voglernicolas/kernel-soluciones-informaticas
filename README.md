# Kernel Instrumental Quirúrgico
**Sistema de Gestión de Instrumental Quirúrgico – Trabajo Final**

## 📌 Descripción
Este proyecto corresponde al **Trabajo Final de la Tecnicatura Superior en Análisis de Sistemas**.  
El objetivo es desarrollar una aplicación interna que optimiza la **gestión de instrumental quirúrgico** en el **Sanatorio Finochietto**, reemplazando planillas en papel por un sistema digital confiable y escalable.

### Alcance del MVP
- Inventario digital de instrumentos y cajas.  
- Búsqueda rápida de instrumental.  
- Consulta detallada de cajas.  
- Checklists digitales pre y post cirugía.  
- Gestión de roles y permisos por usuario.  
- Exportación/impresión de listados.

### Mejoras futuras (fuera del MVP)
- Auditoría detallada de cambios (AuditLog).  
- Importación masiva desde CSV/XLSX.  
- Reportes y dashboards analíticos.  
- Notificaciones en tiempo real (SignalR).

---

## 🛠️ Stack Tecnológico
- **C# con Blazor Server (.NET 8)** → Frontend y backend en un mismo proyecto.  
- **Entity Framework Core** → Acceso a datos con LINQ y migraciones.  
- **SQLite** (MVP) → Base liviana y simple de desplegar.  
- **ASP.NET Identity** → Autenticación y roles (Circulante, Coordinación, Jefatura).  
- **Logging nativo .NET** → Registro básico de eventos/errores.  

> Futuras opciones: **SQL Server Express**, **SignalR**, importación **CSV/XLSX**, **AuditLog** con interceptores EF.

---

## ⚙️ Arquitectura (resumen)
- **Blazor Components** para pantallas: Búsqueda, Detalle de Caja, Checklist, Admin.  
- **EF Core** para entidades: `Instrument`, `Box`, `BoxItem`, `Checklist`.  
- **Identity** para control de acceso por roles.  

---

## 📂 Estructura del Proyecto (sugerida)
- `KernelInstrumental/` (solución)
  - `README.md`
  - `KernelInstrumental.sln`
  - `KernelInstrumental/` (proyecto Blazor Server)
    - `Pages/` (componentes .razor)
    - `Data/` (DbContext, configuraciones EF)
    - `Models/` (entidades de dominio)
    - `Services/` (lógica de negocio)
    - `Areas/Identity/` (autenticación y roles)
    - `wwwroot/` (css/js/img)
    - `Program.cs`
    - `appsettings.json` (cadena de conexión SQLite)

---

## 🚀 Funcionalidades MVP

1) **Login y Roles**  
   - *Circulante*: solo lectura.  
   - *Coordinación/Jefatura*: edición y gestión.

2) **Búsqueda de instrumental**  
   - Filtros por nombre, tipo o código.  
   - Devuelve las **cajas donde se encuentra** cada instrumento.

3) **Detalle de caja**  
   - Listado completo de instrumentos.  
   - Exportación a CSV / impresión simple.

4) **Checklist digital**  
   - Formularios pre y post cirugía.  
   - Validación de faltantes/sobrantes y guardado.

5) **ABM (Alta/Baja/Modificación)**  
   - Gestión de instrumentos, cajas y relaciones (BoxItem).  
   - Restringido a Coordinación/Jefatura.

---

## 📊 Ejemplos de Uso

**Búsqueda de instrumental**  
- Ingresar “tijera de Metzembaun” → el sistema lista cajas donde aparece (p. ej. Caja #3, Caja #7).

**Checklist preoperatorio**  
- Seleccionar caja → mostrar lista digitalizada → marcar presentes/faltantes → guardar.

---

## ▶️ Cómo ejecutar (desarrollo)
1. Requisitos:
   - .NET 8 SDK  
   - Git

2. Clonar y entrar al proyecto:
   - `git clone <URL_DE_TU_REPO>`
   - `cd KernelInstrumental/KernelInstrumental`

3. Configurar cadena de conexión (SQLite por defecto):
   - Editar `appsettings.json` si querés cambiar la ruta del archivo `.db`.

4. Crear base y correr migraciones (si aplica):
   - `dotnet build`
   - `dotnet ef database update` *(si tenés migraciones ya creadas)*

5. Ejecutar:
   - `dotnet run`
   - Abrir el navegador en la URL que indique la consola (p. ej. http://localhost:5000)

> Para un demo rápido podés usar datos semilla (se recomienda crear un usuario admin y los roles *Circulante*, *Coordinación*, *Jefatura*).

---

## 📅 Plan de Trabajo (MVP)
1. **Semana 1** → Proyecto base, modelos y SQLite.  
2. **Semana 2** → Búsqueda y Detalle de Caja.  
3. **Semana 3** → Checklist + ABM.  
4. **Semana 4** → Export/Impresión + pruebas internas.  
5. **Semana 5** → Capacitación breve y demo final.

---

## 👥 Equipo
**Kernel Soluciones Informáticas**  
- Ahmed Camila  
- Aldecoa Florencia  
- Bravo Alejandro  
- Olivera Alejandro  
- Vogler Nicolás

---

## 📖 Licencia
Uso académico – Proyecto Final Tecnicatura Superior en Análisis de Sistemas.
