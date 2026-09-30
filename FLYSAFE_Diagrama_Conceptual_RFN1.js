/*
 * FLY SAFE - Diagrama Conceptual (Punto D) del RFN 1 - Reserva de vuelo
 * ------------------------------------------------------------------------------------------------
 * Genera SOLO este diagrama (no toca los demás diagramas).
 * Conceptos del negocio que nombra el PN1 del RFN 1, con sus atributos y relaciones.
 * Sin métodos, capas ni claves técnicas. La Reserva es el documento central (como la Factura del
 * ejemplo): repite los datos del cliente y del vuelo, y los demás conceptos se agregan a ella
 * (rombo del lado de la Reserva).
 *
 * Compatible con JScript y JavaScript (los dos motores de scripting de EA).
 *
 * Cómo usarlo (EA 13 o superior):
 *   1. Seleccioná en el Project Browser el paquete donde querés el diagrama.
 *   2. Specialize/Tools > Scripting > (grupo Normal) > New JavaScript script.
 *   3. Pegá este archivo completo, guardá y ejecutalo (Run). El avance se ve en Output > Script.
 * Si lo volvés a ejecutar, borra el paquete anterior con el mismo nombre y lo regenera.
 */

var PAQUETE = "FLY SAFE - Diagrama conceptual RFN1";
var DIAGRAMA = "Diagrama Conceptual - RFN1 Reserva de vuelo";

// ------------------------------------------------------------------ conceptos: nombre, x, y, atributos
var CONCEPTOS = [
    { nombre: "Reserva", x: 420, y: 40, atributos: [
        ["Nro_Reserva", "string"], ["Fecha_Realizacion", "DateTime"], ["Tipo_Viaje", "string"],
        ["Fecha_Regreso", "DateTime"], ["Cod_Vuelo", "string"], ["Origen", "string"], ["Destino", "string"],
        ["Clase", "string"], ["Cantidad_Pasajeros", "int"], ["DNI_Cliente", "int"], ["Nombre_Cliente", "string"],
        ["Apellido_Cliente", "string"], ["Importe_Base", "decimal"], ["Subtotal_Adicionales", "decimal"],
        ["Impuestos", "decimal"], ["Importe_Total", "decimal"], ["Estado", "string"]] },

    { nombre: "Cliente", x: 60, y: 40, atributos: [
        ["DNI", "int"], ["Nombre", "string"], ["Apellido", "string"], ["Correo", "string"], ["Telefono", "string"]] },

    { nombre: "Vuelo", x: 60, y: 215, atributos: [
        ["Cod_Vuelo", "string"], ["Aerolinea", "string"], ["Origen", "string"], ["Destino", "string"],
        ["FechaHora_Salida", "DateTime"], ["FechaHora_Llegada", "DateTime"], ["Clase_Disponible", "string"],
        ["Cantidad_Asientos", "int"]] },

    { nombre: "Servicio_Adicional", x: 60, y: 440, atributos: [
        ["Tipo", "string"], ["Cantidad", "int"], ["Costo_Unitario", "decimal"]] },

    { nombre: "Pasajero", x: 840, y: 40, atributos: [
        ["DNI", "int"], ["Nombre", "string"], ["Apellido", "string"], ["Correo", "string"], ["Telefono", "string"]] },

    { nombre: "Boleto", x: 840, y: 260, atributos: [
        ["Nro_Boleto", "string"], ["Nro_Reserva", "string"], ["DNI_Pasajero", "int"], ["Nombre_Pasajero", "string"],
        ["Apellido_Pasajero", "string"], ["Cod_Vuelo", "string"]] },

    { nombre: "Pago", x: 420, y: 440, atributos: [
        ["Nro_Reserva", "string"], ["Importe_Total_Abonado", "decimal"], ["Medio_Pago", "string"],
        ["Nro_Transaccion", "string"], ["FechaHora_Pago", "DateTime"]] }
];
var ANCHO = 230;

// ------------------------------------------------------------------ relaciones
//   [parte, todo, nombre, multiplicidad del lado "parte", multiplicidad del lado "todo", agregación]
//   agregación = true: rombo vacío del lado del "todo" (como Factura en el ejemplo).
var RELACIONES = [
    ["Cliente", "Reserva", "Realiza", "1", "0..*", true],
    ["Vuelo", "Reserva", "Es reservado en", "1", "0..*", true],
    ["Pasajero", "Reserva", "Viaja en", "1..*", "0..*", true],
    ["Servicio_Adicional", "Reserva", "Incluye", "0..*", "1", true],
    ["Pago", "Reserva", "Se abona con", "0..1", "1", true],
    ["Boleto", "Reserva", "Emite", "0..*", "1", true],
    ["Boleto", "Pasajero", "Pertenece a", "0..*", "1", false]
];

// ------------------------------------------------------------------ utilidades (JScript, sin ES5)

function log(msg) { Session.Output(msg); }

function ponerEnDiagrama(diagrama, el, x, y, w, h) {
    var pos = "l=" + x + ";r=" + (x + w) + ";t=" + y + ";b=" + (y + h) + ";";
    var o = diagrama.DiagramObjects.AddNew(pos, "");
    o.ElementID = el.ElementID;
    o.Update();
    return o;
}

function main() {
    Repository.EnsureOutputVisible("Script");
    Repository.ClearOutput("Script");

    var destino = Repository.GetTreeSelectedPackage();
    if (destino == null) {
        Session.Prompt("Seleccioná un paquete en el Project Browser antes de ejecutar el script.", 1);
        return;
    }

    var i, j;
    for (i = destino.Packages.Count - 1; i >= 0; i--) {
        if (destino.Packages.GetAt(i).Name == PAQUETE) {
            destino.Packages.DeleteAt(i, false);
            log("Se borró la versión anterior de '" + PAQUETE + "'.");
        }
    }
    destino.Packages.Refresh();

    var paquete = destino.Packages.AddNew(PAQUETE, "Package");
    paquete.Update();
    destino.Packages.Refresh();

    var d = paquete.Diagrams.AddNew(DIAGRAMA, "Logical");
    d.Update();

    // Conceptos (clases solo con atributos)
    var elementos = {};
    for (i = 0; i < CONCEPTOS.length; i++) {
        var c = CONCEPTOS[i];
        var el = paquete.Elements.AddNew(c.nombre, "Class");
        el.Update();
        for (j = 0; j < c.atributos.length; j++) {
            var a = el.Attributes.AddNew(c.atributos[j][0], c.atributos[j][1]);
            a.Visibility = "Private";
            a.Pos = j;
            a.Update();
        }
        el.Attributes.Refresh();
        el.Update();
        elementos[c.nombre] = el;
        ponerEnDiagrama(d, el, c.x, c.y, ANCHO, 36 + 15 * c.atributos.length);
    }
    paquete.Elements.Refresh();

    // Relaciones
    for (i = 0; i < RELACIONES.length; i++) {
        var r = RELACIONES[i];
        var parte = elementos[r[0]], todo = elementos[r[1]];
        var con = parte.Connectors.AddNew(r[2], "Association");
        con.SupplierID = todo.ElementID;
        con.ClientEnd.Cardinality = r[3];
        con.SupplierEnd.Cardinality = r[4];
        if (r[5]) {
            try { con.SupplierEnd.Aggregation = 1; } catch (e) { }   // 1 = compartida: rombo vacío en el extremo del "todo"
        }
        con.Update();
        parte.Connectors.Refresh();
    }

    d.DiagramObjects.Refresh();
    try { Repository.ReloadDiagram(d.DiagramID); } catch (e2) { }
    Repository.RefreshModelView(destino.PackageID);
    try { Repository.OpenDiagram(d.DiagramID); } catch (e3) { }

    log("Listo: '" + DIAGRAMA + "' (" + CONCEPTOS.length + " conceptos, " + RELACIONES.length +
        " relaciones) en el paquete '" + PAQUETE + "'.");
}

main();
