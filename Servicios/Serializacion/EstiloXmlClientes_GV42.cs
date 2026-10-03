namespace Servicios
{
    // Hoja de estilo CSS para ver el XML del maestro de clientes como una tabla (título, subtítulo
    // con fecha y total, encabezados en negrita y filas separadas por líneas claras).
    //
    // Es solo presentación: se guarda como archivo aparte (NOMBRE_ARCHIVO) junto al XML, que la
    // referencia con <?xml-stylesheet type="text/css" href="estilo.css"?>. El XML no se modifica.
    public static class EstiloXmlClientes_GV42
    {
        #region Campos

        public const string NOMBRE_ARCHIVO = "estilo.css";

        private const string CONTENIDO_CSS =
@"/* Maestro de Clientes - estilo de visualización del XML serializado */
MaestroClientes {
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
MaestroClientes::before {
    content: ""Maestro de Clientes"";
    position: absolute; top: -100px; left: 0;
    font-size: 16pt; font-weight: bold;
}
MaestroClientes::after {
    content: ""Generado el "" attr(FechaGeneracion) "" por "" attr(GeneradoPor) "" - Total de registros: "" attr(Cantidad);
    position: absolute; top: -70px; left: 0;
    font-size: 9pt; color: #737373;
}

/* Cada cliente es una fila y cada dato una celda */
Cliente { display: table-row; }
Cliente > * {
    display: table-cell;
    position: relative;
    padding: 5px 6px 4px 0;
    border-bottom: 1px solid #e0e0e0;
    vertical-align: top;
}

DNI      { width: 14%; }
Apellido { width: 20%; }
Nombre   { width: 20%; }
Email    { width: 28%; }
Telefono { width: 18%; }

/* Encabezados de columna: se dibujan sobre las celdas de la primera fila */
Cliente:first-of-type > *::before {
    position: absolute; top: -30px; left: 0; right: 0;
    padding: 6px 6px 5px 0;
    font-size: 10pt; font-weight: bold;
    border-top: 1px solid #b3b3b3;
    border-bottom: 1px solid #808080;
}
Cliente:first-of-type > DNI::before      { content: ""DNI""; }
Cliente:first-of-type > Apellido::before { content: ""Apellido""; }
Cliente:first-of-type > Nombre::before   { content: ""Nombre""; }
Cliente:first-of-type > Email::before    { content: ""Email""; }
Cliente:first-of-type > Telefono::before { content: ""Teléfono""; }

@page { size: A4 landscape; margin: 18mm; }
";

        #endregion

        #region Propiedades

        public static string CSS
        {
            get { return CONTENIDO_CSS; }
        }

        #endregion
    }
}
