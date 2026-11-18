/*
 * FLY SAFE - DS CUN09 "Auditoría de cambios" para Enterprise Architect
 * ------------------------------------------------------------------------------------
 * Crea, dentro del paquete seleccionado en el Project Browser, el paquete
 * "DS CUN09 - Auditoría de cambios" con el diagrama de secuencia:
 *   - consulta de la bitácora (abrir la pantalla, filtrar con "Aplicar")
 *   - fragmento opt [el administrador activa un registro anterior] con la activación (rollback)
 * Las líneas de vida usan los nombres reales de las clases del proyecto.
 *
 * Cómo usarlo (EA 13 o superior):
 *   1. Seleccioná en el Project Browser el paquete donde querés el diagrama.
 *   2. Specialize/Tools > Scripting > New Normal Group (o uno existente) > New JavaScript script.
 *   3. Pegá este archivo completo, guardá y ejecutalo (botón Run).
 * Si lo volvés a ejecutar, borra el paquete anterior con el mismo nombre y lo regenera.
 * Compatible con JScript y JavaScript.
 */

var PAQUETE = "DS CUN09 - Auditoría de cambios";

var DEFS = {
 "a": {
  "nombre": "Administrador",
  "actor": true
 },
 "frm": {
  "nombre": "FRMBitacoraVuelos_GV42",
  "est": "boundary"
 },
 "bll": {
  "nombre": "BLLVueloHistorial_GV42"
 },
 "util": {
  "nombre": "BLLNegocioUtil_GV42"
 },
 "bVu": {
  "nombre": "BLLVuelo_GV42"
 },
 "dH": {
  "nombre": "DALVueloHistorial_GV42"
 },
 "bd": {
  "nombre": "Base de datos"
 }
};

var CASO = {
 "nombre": "DS CUN09 - Auditoría de cambios",
 "lifelines": [
  "a",
  "frm",
  "bll",
  "util",
  "bVu",
  "dH",
  "bd"
 ],
 "alto": 1456,
 "mensajes": [
  [
   "a",
   "frm",
   "Ingresa a “Bitácora de cambios”",
   "",
   130
  ],
  [
   "frm",
   "bll",
   "PuedeActivar()",
   "",
   162
  ],
  [
   "bll",
   "util",
   "TienePatente(\"Vuelos.Activar\")",
   "",
   194
  ],
  [
   "util",
   "bll",
   "bool",
   "r",
   226
  ],
  [
   "bll",
   "frm",
   "bool",
   "r",
   258
  ],
  [
   "frm",
   "bll",
   "ListarCodigos() / ListarNombres()",
   "",
   290
  ],
  [
   "bll",
   "dH",
   "ListarCodigos() / ListarNombres()",
   "",
   322
  ],
  [
   "dH",
   "bd",
   "SELECT DISTINCT ... FROM Vuelo_C",
   "",
   354
  ],
  [
   "bd",
   "dH",
   "códigos y rutas",
   "r",
   386
  ],
  [
   "dH",
   "bll",
   "List<string>",
   "r",
   418
  ],
  [
   "bll",
   "frm",
   "List<string>",
   "r",
   450
  ],
  [
   "frm",
   "frm",
   "FiltrosDeArranque() (último mes)",
   "",
   482
  ],
  [
   "a",
   "frm",
   "Carga la consulta y presiona “Aplicar”",
   "",
   524
  ],
  [
   "frm",
   "bll",
   "Consultar(codigo, nombre, fechaIni, fechaFin, soloConCambios)",
   "",
   556
  ],
  [
   "bll",
   "util",
   "ExigirPatente(\"Vuelos.Bitacora\")",
   "",
   588
  ],
  [
   "bll",
   "bll",
   "Valida fechaIni <= fechaFin",
   "",
   620
  ],
  [
   "bll",
   "dH",
   "Listar(codigo, nombre, fechaIni, fechaFin, soloConCambios)",
   "",
   662
  ],
  [
   "dH",
   "bd",
   "SELECT ... FROM Vuelo_C",
   "",
   694
  ],
  [
   "bd",
   "dH",
   "registros",
   "r",
   726
  ],
  [
   "dH",
   "bll",
   "List<VueloCambio_GV42>",
   "r",
   758
  ],
  [
   "bll",
   "frm",
   "List<VueloCambio_GV42>",
   "r",
   790
  ],
  [
   "frm",
   "a",
   "Muestra la grilla con el registro activo resaltado",
   "r",
   822
  ],
  [
   "a",
   "frm",
   "Selecciona un registro anterior y presiona “Activar”",
   "",
   894
  ],
  [
   "frm",
   "a",
   "Pide confirmación",
   "r",
   926
  ],
  [
   "a",
   "frm",
   "Acepta",
   "",
   958
  ],
  [
   "frm",
   "bll",
   "ActivarVersion(cambio)",
   "",
   990
  ],
  [
   "bll",
   "util",
   "ExigirPatente(\"Vuelos.Activar\")",
   "",
   1022
  ],
  [
   "bll",
   "bVu",
   "BuscarActual(idVuelo)",
   "",
   1054
  ],
  [
   "bll",
   "bVu",
   "ContarReservasVigentes(idVuelo)",
   "",
   1086
  ],
  [
   "bll",
   "bVu",
   "ValidarReprogramacion(actual, salida, llegada)",
   "",
   1118
  ],
  [
   "bll",
   "dH",
   "ActivarVersion(idCambio)",
   "",
   1150
  ],
  [
   "dH",
   "bd",
   "UPDATE Vuelo_C SET Act ... (transacción)",
   "",
   1182
  ],
  [
   "bd",
   "bd",
   "TR_Vuelo_C_Activar actualiza Vuelo",
   "",
   1214
  ],
  [
   "bd",
   "dH",
   "OK",
   "r",
   1256
  ],
  [
   "dH",
   "bll",
   "codigoVuelo",
   "r",
   1288
  ],
  [
   "bll",
   "util",
   "Auditar(\"Version de vuelo activada\")",
   "",
   1320
  ],
  [
   "bll",
   "frm",
   "",
   "r",
   1352
  ],
  [
   "frm",
   "a",
   "“Registro activado” y recarga la grilla",
   "r",
   1384
  ]
 ],
 "fragmentos": [
  {
   "subtipo": 1,
   "nombre": "Activar registro",
   "l": 15,
   "r": 1345,
   "t": 864,
   "b": 1406,
   "ops": [
    {
     "guarda": "[el administrador activa un registro anterior]",
     "size": 542
    }
   ]
  }
 ]
};

function log(msg) { Session.Output(msg); }

function nuevoElemento(paquete, nombre, tipo, estereotipo) {
    var e = paquete.Elements.AddNew(nombre, tipo);
    if (estereotipo) e.Stereotype = estereotipo;
    e.Update();
    paquete.Elements.Refresh();
    return e;
}

function ponerEnDiagrama(diagrama, elemento, x, y, ancho, alto) {
    var pos = "l=" + Math.round(x) + ";r=" + Math.round(x + ancho) + ";t=" + Math.round(y) + ";b=" + Math.round(y + alto) + ";";
    var o = diagrama.DiagramObjects.AddNew(pos, "");
    o.ElementID = elemento.ElementID;
    o.Update();
    return o;
}

function crearFragmento(paquete, diagrama, f) {
    var el = nuevoElemento(paquete, f.nombre, "InteractionFragment", null);
    el.Subtype = f.subtipo;              // 0 = alt, 1 = opt, 4 = loop
    el.Update();
    for (var i = 0; i < f.ops.length; i++) {
        var p = el.Partitions.AddNew(f.ops[i].guarda, "");
        try { p.Size = f.ops[i].size; } catch (e1) { }
    }
    el.Update();
    try { el.Partitions.Refresh(); } catch (e2) { }
    ponerEnDiagrama(diagrama, el, f.l, f.t, f.r - f.l, f.b - f.t);
}

function main() {
    Repository.EnsureOutputVisible("Script");
    Repository.ClearOutput("Script");
    var destino = Repository.GetTreeSelectedPackage();
    if (destino == null) {
        Session.Prompt("Seleccioná un paquete en el Project Browser antes de ejecutar el script.", 1);
        return;
    }
    var i;
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

    var d = paquete.Diagrams.AddNew(CASO.nombre, "Sequence");
    d.Update();

    // Líneas de vida: actor, formulario (boundary), BLL, DAL y base de datos
    var lineas = {}, x = 30;
    for (i = 0; i < CASO.lifelines.length; i++) {
        var id = CASO.lifelines[i], def = DEFS[id], el;
        if (def.actor) el = nuevoElemento(paquete, def.nombre, "Actor", null);
        else el = nuevoElemento(paquete, def.nombre, "Sequence", def.est ? def.est : null);
        ponerEnDiagrama(d, el, x, 50, 140, CASO.alto);
        lineas[id] = el;
        x += 190;
    }

    // Fragmento opt (queda por detrás de los mensajes)
    for (i = 0; i < CASO.fragmentos.length; i++) crearFragmento(paquete, d, CASO.fragmentos[i]);

    // Mensajes: [origen, destino, texto, "r" = retorno, posición vertical]
    for (i = 0; i < CASO.mensajes.length; i++) {
        var m = CASO.mensajes[i];
        var origen = lineas[m[0]], dest = lineas[m[1]];
        var con = origen.Connectors.AddNew(m[2], "Sequence");
        con.SupplierID = dest.ElementID;
        con.DiagramID = d.DiagramID;
        con.SequenceNo = i + 1;
        try { con.StartPointY = -m[4]; con.EndPointY = -m[4]; } catch (e3) { }
        con.Update();
        origen.Connectors.Refresh();
        if (m[3] == "r") {
            // Mensaje de retorno (línea punteada): EA lo guarda en t_connector.PDATA4
            try { Repository.Execute("UPDATE t_connector SET PDATA4 = '1' WHERE Connector_ID = " + con.ConnectorID); } catch (e4) { }
        }
        try { Repository.Execute("UPDATE t_connector SET PtStartY = " + (-m[4]) + ", PtEndY = " + (-m[4]) + " WHERE Connector_ID = " + con.ConnectorID); } catch (e5) { }
    }

    d.DiagramObjects.Refresh();
    try { Repository.ReloadDiagram(d.DiagramID); } catch (e6) { }
    Repository.RefreshModelView(destino.PackageID);
    log("Listo: '" + CASO.nombre + "' (" + CASO.mensajes.length + " mensajes) en el paquete '" + PAQUETE + "'.");
}

main();
