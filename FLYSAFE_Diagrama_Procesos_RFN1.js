/*
 * FLY SAFE - Diagrama de Procesos (basado en Diagrama de Actividad) del RFN 1 - Reserva de vuelo
 * ------------------------------------------------------------------------------------------------
 * Genera SOLO este diagrama (no toca los diagramas de CUN01 a CUN06).
 *
 * Contenido, según el PN1 del RFN 1 (pasos 1 a 13) y los roles del Punto A:
 *   Carriles (particiones): Cliente | Vendedor | Entidad Bancaria
 *   - Cliente: datos que entrega (señales de envío) y lo que recibe (vuelos disponibles, reserva, boletos).
 *   - Vendedor: actividades del proceso, decisiones y almacenes de datos («datastore»).
 *   - Entidad Bancaria: procesa el pago con tarjeta / transferencia (el efectivo no pasa por el banco).
 *   Si no querés el carril del banco, poné INCLUIR_BANCO = false (el pago queda en el Vendedor).
 *
 * Compatible con JScript y JavaScript (los dos motores de scripting de EA).
 *
 * Cómo usarlo (EA 13 o superior):
 *   1. Seleccioná en el Project Browser el paquete donde querés el diagrama.
 *   2. Specialize/Tools > Scripting > (grupo Normal) > New JavaScript script.
 *   3. Pegá este archivo completo, guardá y ejecutalo (Run). El avance se ve en Output > Script.
 * Si lo volvés a ejecutar, borra el paquete anterior con el mismo nombre y lo regenera.
 */

var PAQUETE = "FLY SAFE - Diagrama de procesos RFN1";
var DIAGRAMA = "Diagrama de Procesos - RFN1 Reserva de vuelo";
var INCLUIR_BANCO = true;

// ------------------------------------------------------------------ carriles (x, ancho, color)
var CARRILES = [
    { id: "C", nombre: "Cliente",          x: 20,  ancho: 300, color: [0, 230, 0] },
    { id: "V", nombre: "Vendedor",         x: 320, ancho: 560, color: [192, 255, 255] },
    { id: "B", nombre: "Entidad Bancaria", x: 880, ancho: 300, color: [255, 255, 200] }
];
var ALTO_CARRIL = 1660;
var Y_CARRIL = 20;

// Columnas (centro x) dentro de los carriles
var COL_CLIENTE = 170, COL_VEND = 625, COL_ALT = 420, COL_DS = 812, COL_BANCO = 1030;

// ------------------------------------------------------------------ estilos por tipo de nodo
//   ini = nodo inicial, fin = nodo final, env = señal de envío (dato que entrega el cliente),
//   acc = actividad, dec = decisión, mer = unión (merge), ds = almacén de datos, art = documento
var ESTILO = {
    ini: { w: 22,  h: 22, color: null },
    fin: { w: 24,  h: 24, color: null },
    env: { w: 150, h: 44, color: [244, 182, 182] },
    acc: { w: 170, h: 44, color: [250, 215, 175] },
    dec: { w: 38,  h: 38, color: [198, 217, 138] },
    mer: { w: 38,  h: 38, color: [198, 217, 138] },
    ds:  { w: 120, h: 46, color: [214, 228, 255] },
    art: { w: 140, h: 52, color: [215, 245, 225] }
};

// ------------------------------------------------------------------ nodos: [id, tipo, nombre, x centro, y]
var NODOS = [
    ["ini",          "ini", "Inicio",                                   COL_CLIENTE, 70],
    ["datosVuelo",   "env", "Datos del vuelo",                          COL_CLIENTE, 120],
    ["regVuelo",     "acc", "Registrar datos del vuelo",                COL_VEND,    120],
    ["tipoViaje",    "dec", "¿Tipo de viaje?",                          COL_VEND,    200],
    ["fechaRegreso", "acc", "Registrar fecha de regreso",               COL_ALT,     255],
    ["unionViaje",   "mer", "",                                         COL_VEND,    315],
    ["buscar",       "acc", "Buscar vuelos disponibles",                COL_VEND,    375],
    ["dsVuelos",     "ds",  "Vuelos",                                   COL_DS,      375],
    ["artVuelos",    "art", "Vuelos disponibles",                       COL_CLIENTE, 440],

    ["eleccion",     "env", "Vuelo, clase y cantidad de pasajeros",     COL_CLIENTE, 525],
    ["seleccionar",  "acc", "Seleccionar vuelo y clase",                COL_VEND,    525],
    ["dni",          "env", "DNI del cliente",                          COL_CLIENTE, 605],
    ["verificar",    "acc", "Verificar cliente",                        COL_VEND,    605],
    ["dsClientes",   "ds",  "Clientes",                                 COL_ALT,     668],
    ["registrado",   "dec", "¿Cliente registrado?",                     COL_VEND,    680],
    ["datosPers",    "env", "Datos personales",                         COL_CLIENTE, 740],
    ["regCliente",   "acc", "Registrar cliente",                        COL_ALT,     740],
    ["unionCliente", "mer", "",                                         COL_VEND,    805],

    ["pasajeros",    "env", "Datos de los pasajeros",                   COL_CLIENTE, 865],
    ["regPasajeros", "acc", "Registrar pasajeros del vuelo",            COL_VEND,    865],
    ["dsPasajeros",  "ds",  "Pasajeros",                                COL_DS,      865],
    ["adicionales",  "dec", "¿Desea servicios adicionales?",            COL_VEND,    940],
    ["servicios",    "env", "Servicios adicionales",                    COL_CLIENTE, 1000],
    ["regServicios", "acc", "Registrar servicios adicionales",          COL_ALT,     1000],
    ["unionAdic",    "mer", "",                                         COL_VEND,    1065],

    ["generar",      "acc", "Generar reserva (Pendiente de pago)",      COL_VEND,    1125],
    ["dsReservas",   "ds",  "Reservas",                                 COL_DS,      1125],
    ["artReserva",   "art", "Reserva pendiente de pago",                COL_CLIENTE, 1120],

    ["metodo",       "env", "Método de pago",                           COL_CLIENTE, 1205],
    ["regMetodo",    "acc", "Registrar método de pago",                 COL_VEND,    1205],
    ["medio",        "dec", "¿Medio de pago?",                          COL_VEND,    1280],
    ["procesar",     "acc", "Procesar pago",                            COL_BANCO,   1280],
    ["aprobado",     "dec", "¿Pago aprobado?",                          COL_BANCO,   1355],
    ["finRechazo",   "fin", "Pago rechazado",                           COL_BANCO,   1430],
    ["unionPago",    "mer", "",                                         COL_VEND,    1355],

    ["regPago",      "acc", "Registrar pago y confirmar reserva",       COL_VEND,    1415],
    ["dsPagos",      "ds",  "Pagos",                                    COL_DS,      1415],
    ["emitir",       "acc", "Emitir boletos",                           COL_VEND,    1495],
    ["dsBoletos",    "ds",  "Boletos",                                  COL_DS,      1495],
    ["artBoletos",   "art", "Boletos",                                  COL_CLIENTE, 1490],
    ["fin",          "fin", "Fin",                                      COL_CLIENTE, 1585]
];

// ------------------------------------------------------------------ flujos: [origen, destino, tipo, etiqueta]
//   cf = flujo de control, of = flujo de objeto, ds = acceso a almacén (línea punteada, como el ejemplo)
var FLUJOS = [
    ["ini", "datosVuelo", "cf", ""],
    ["datosVuelo", "regVuelo", "of", ""],
    ["regVuelo", "tipoViaje", "cf", ""],
    ["tipoViaje", "fechaRegreso", "cf", "ida y vuelta"],
    ["tipoViaje", "unionViaje", "cf", "ida"],
    ["fechaRegreso", "unionViaje", "cf", ""],
    ["unionViaje", "buscar", "cf", ""],
    ["buscar", "dsVuelos", "ds", ""],
    ["buscar", "artVuelos", "of", ""],
    ["artVuelos", "eleccion", "cf", ""],

    ["eleccion", "seleccionar", "of", ""],
    ["seleccionar", "verificar", "cf", ""],
    ["dni", "verificar", "of", ""],
    ["verificar", "dsClientes", "ds", ""],
    ["verificar", "registrado", "cf", ""],
    ["registrado", "regCliente", "cf", "no"],
    ["registrado", "unionCliente", "cf", "sí"],
    ["datosPers", "regCliente", "of", ""],
    ["regCliente", "dsClientes", "ds", ""],
    ["regCliente", "unionCliente", "cf", ""],

    ["unionCliente", "regPasajeros", "cf", ""],
    ["pasajeros", "regPasajeros", "of", ""],
    ["regPasajeros", "dsPasajeros", "ds", ""],
    ["regPasajeros", "adicionales", "cf", ""],
    ["adicionales", "regServicios", "cf", "sí"],
    ["adicionales", "unionAdic", "cf", "no"],
    ["servicios", "regServicios", "of", ""],
    ["regServicios", "unionAdic", "cf", ""],

    ["unionAdic", "generar", "cf", ""],
    ["generar", "dsReservas", "ds", ""],
    ["generar", "artReserva", "of", ""],
    ["generar", "regMetodo", "cf", ""],
    ["metodo", "regMetodo", "of", ""],
    ["regMetodo", "medio", "cf", ""],
    ["medio", "procesar", "cf", "tarjeta / transferencia"],
    ["medio", "unionPago", "cf", "efectivo"],
    ["procesar", "aprobado", "cf", ""],
    ["aprobado", "finRechazo", "cf", "no"],
    ["aprobado", "unionPago", "cf", "sí"],

    ["unionPago", "regPago", "cf", ""],
    ["regPago", "dsPagos", "ds", ""],
    ["regPago", "dsReservas", "ds", ""],
    ["regPago", "emitir", "cf", ""],
    ["emitir", "dsBoletos", "ds", ""],
    ["emitir", "artBoletos", "of", ""],
    ["artBoletos", "fin", "cf", ""]
];

// ------------------------------------------------------------------ utilidades (JScript, sin ES5)

function log(msg) { Session.Output(msg); }

// EA guarda los colores como BGR en un entero.
function colorEA(rgb) { return rgb[0] + rgb[1] * 256 + rgb[2] * 65536; }

function pintar(el, rgb) {
    if (!rgb) return;
    try { el.SetAppearance(1, 0, colorEA(rgb)); } catch (e) { }   // 1 = local, 0 = fondo
}

function nuevoElemento(paquete, nombre, tipo, alternativo) {
    var el;
    try {
        el = paquete.Elements.AddNew(nombre, tipo);
    } catch (e) {
        el = paquete.Elements.AddNew(nombre, alternativo);
    }
    return el;
}

// Pentágono de "señal de envío": acción con Kind = SendSignal (propiedad personalizada de EA).
function comoSenalDeEnvio(el) {
    try {
        var props = el.CustomProperties;
        for (var i = 0; i < props.Count; i++) {
            var p = props.GetAt(i);
            if (("" + p.Name).toLowerCase() == "kind") { p.Value = "SendSignal"; el.Update(); return true; }
        }
    } catch (e) { }
    return false;
}

function ponerEnDiagrama(diagrama, el, x, y, w, h, orden) {
    var pos = "l=" + Math.round(x) + ";r=" + Math.round(x + w) + ";t=" + Math.round(y) + ";b=" + Math.round(y + h) + ";";
    var o = diagrama.DiagramObjects.AddNew(pos, "");
    o.ElementID = el.ElementID;
    try { o.Sequence = orden; } catch (e) { }   // orden Z: 1 = adelante; los carriles van al fondo
    o.Update();
    return o;
}

function buscarCarril(id) {
    for (var i = 0; i < CARRILES.length; i++) if (CARRILES[i].id == id) return CARRILES[i];
    return null;
}

// Sin carril del banco: el pago lo procesa el Vendedor (columna alternativa).
function ajustarSinBanco() {
    var carriles = [], i;
    for (i = 0; i < CARRILES.length; i++) if (CARRILES[i].id != "B") carriles.push(CARRILES[i]);
    CARRILES = carriles;
    for (i = 0; i < NODOS.length; i++) if (NODOS[i][3] == COL_BANCO) NODOS[i][3] = COL_ALT;
}

// ------------------------------------------------------------------ principal

function main() {
    Repository.EnsureOutputVisible("Script");
    Repository.ClearOutput("Script");

    var destino = Repository.GetTreeSelectedPackage();
    if (destino == null) {
        Session.Prompt("Seleccioná un paquete en el Project Browser antes de ejecutar el script.", 1);
        return;
    }
    if (!INCLUIR_BANCO) ajustarSinBanco();

    // Regenerar: borrar la versión anterior del paquete
    for (var i = destino.Packages.Count - 1; i >= 0; i--) {
        if (destino.Packages.GetAt(i).Name == PAQUETE) {
            destino.Packages.DeleteAt(i, false);
            log("Se borró la versión anterior de '" + PAQUETE + "'.");
        }
    }
    destino.Packages.Refresh();

    var paquete = destino.Packages.AddNew(PAQUETE, "Package");
    paquete.Update();
    destino.Packages.Refresh();

    var d = paquete.Diagrams.AddNew(DIAGRAMA, "Activity");
    d.Update();

    // Carriles (particiones de actividad), al fondo del diagrama
    for (i = 0; i < CARRILES.length; i++) {
        var c = CARRILES[i];
        var part = nuevoElemento(paquete, c.nombre, "ActivityPartition", "ActivityPartition");
        part.Update();
        pintar(part, c.color);
        part.Update();
        ponerEnDiagrama(d, part, c.x, Y_CARRIL, c.ancho, ALTO_CARRIL, 100 + i);
    }

    // Nodos
    var elementos = {};
    var sinPentagono = 0;
    for (i = 0; i < NODOS.length; i++) {
        var n = NODOS[i], id = n[0], tipo = n[1], nombre = n[2];
        var est = ESTILO[tipo], el;

        if (tipo == "ini" || tipo == "fin") {
            el = nuevoElemento(paquete, nombre, "StateNode", "StateNode");
            el.Subtype = (tipo == "ini") ? 100 : 101;           // 100 = inicial, 101 = final de actividad
        } else if (tipo == "acc" || tipo == "env") {
            el = nuevoElemento(paquete, nombre, "Action", "Activity");
        } else if (tipo == "dec") {
            el = nuevoElemento(paquete, nombre, "Decision", "Decision");
        } else if (tipo == "mer") {
            el = nuevoElemento(paquete, nombre, "MergeNode", "Decision");
        } else if (tipo == "ds") {
            el = nuevoElemento(paquete, nombre, "Object", "Object");
            el.Stereotype = "datastore";
        } else {
            el = nuevoElemento(paquete, nombre, "Artifact", "Object");
        }
        el.Update();
        if (tipo == "env" && !comoSenalDeEnvio(el)) sinPentagono++;
        pintar(el, est.color);
        el.Update();
        elementos[id] = el;

        ponerEnDiagrama(d, el, n[3] - est.w / 2, n[4], est.w, est.h, 1);
    }
    paquete.Elements.Refresh();

    // Flujos
    for (i = 0; i < FLUJOS.length; i++) {
        var f = FLUJOS[i];
        var o = elementos[f[0]], dst = elementos[f[1]];
        if (!o || !dst) { log("  (flujo omitido: " + f[0] + " -> " + f[1] + ")"); continue; }
        var tipoCon = f[2] == "cf" ? "ControlFlow" : (f[2] == "of" ? "ObjectFlow" : "Dependency");
        var con = o.Connectors.AddNew(f[3], tipoCon);
        con.SupplierID = dst.ElementID;
        con.Update();
        o.Connectors.Refresh();
    }

    d.DiagramObjects.Refresh();
    try { Repository.ReloadDiagram(d.DiagramID); } catch (e) { }
    Repository.RefreshModelView(destino.PackageID);
    try { Repository.OpenDiagram(d.DiagramID); } catch (e2) { }

    log("Listo: '" + DIAGRAMA + "' (" + NODOS.length + " nodos, " + FLUJOS.length + " flujos) en el paquete '" + PAQUETE + "'.");
    if (sinPentagono > 0)
        log("Aviso: tu versión de EA no permitió marcar " + sinPentagono + " acciones como 'señal de envío' (pentágono). " +
            "Se pueden cambiar a mano: clic derecho > Properties > Action > Kind = SendSignal.");
}

main();
