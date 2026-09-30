namespace Servicios
{
    // Hoja de estilo CSS para ver el XML del maestro de usuarios con el diseño del PDF de
    // "Imprimir bitácora" (título en negrita, subtítulo gris con fecha y total, encabezados en
    // negrita entre líneas grises y filas separadas por líneas claras).
    //
    // Es solo presentación: se guarda como archivo aparte (NOMBRE_ARCHIVO) junto al XML, que la
    // referencia con <?xml-stylesheet type="text/css" href="estilo.css"?>. El XML no se modifica.
    public static class EstiloXmlUsuarios_GV42
    {
        public const string NOMBRE_ARCHIVO = "estilo.css";

        public static string CSS
        {
            get { return CONTENIDO_CSS; }
        }

        private const string CONTENIDO_CSS =
@"/* Maestro de Usuarios - estilo de visualización del XML serializado */
MaestroUsuarios {
    display: table;
    position: relative;
    width: calc(100% - 100px);
    margin: 110px 50px 40px 50px;
    border-collapse: collapse;
    font-family: Helvetica, Arial, sans-serif;
    font-size: 9pt;
    color: #000;
    background: #fff;
}

/* Título y subtítulo (con los atributos de la raíz) */
MaestroUsuarios::before {
    content: ""Maestro de Usuarios"";
    position: absolute; top: -100px; left: 0;
    font-size: 16pt; font-weight: bold;
}
MaestroUsuarios::after {
    content: ""Generado el "" attr(FechaGeneracion) "" por "" attr(GeneradoPor) "" - Total de registros: "" attr(Cantidad);
    position: absolute; top: -70px; left: 0;
    font-size: 9pt; color: #737373;
}

/* Cada usuario es una fila y cada dato una celda */
Usuario { display: table-row; }
Usuario > * {
    display: table-cell;
    position: relative;
    padding: 5px 6px 4px 0;
    border-bottom: 1px solid #e0e0e0;
    vertical-align: top;
}

DNI       { width: 10%; }
Apellido  { width: 13%; }
Nombre    { width: 13%; }
Email     { width: 22%; }
UserName  { width: 12%; }
Rol       { width: 14%; }
Bloqueado { width: 8%; }
Activo    { width: 8%; }

/* Encabezados de columna: se dibujan sobre las celdas de la primera fila */
Usuario:first-of-type > *::before {
    position: absolute; top: -30px; left: 0; right: 0;
    padding: 6px 6px 5px 0;
    font-size: 10pt; font-weight: bold;
    border-top: 1px solid #b3b3b3;
    border-bottom: 1px solid #808080;
}
Usuario:first-of-type > DNI::before       { content: ""DNI""; }
Usuario:first-of-type > Apellido::before  { content: ""Apellido""; }
Usuario:first-of-type > Nombre::before    { content: ""Nombre""; }
Usuario:first-of-type > Email::before     { content: ""Email""; }
Usuario:first-of-type > UserName::before  { content: ""Usuario""; }
Usuario:first-of-type > Rol::before       { content: ""Rol""; }
Usuario:first-of-type > Bloqueado::before { content: ""Bloqueado""; }
Usuario:first-of-type > Activo::before    { content: ""Activo""; }

@page { size: A4 landscape; margin: 18mm; }
";
    }
}
