/*
 * FLY SAFE - Diagrama de actividad "Bitácora de cambios" (triggers) para Enterprise Architect
 * ------------------------------------------------------------------------------------
 * Versión simple, igual al modelo: cada tabla (Vuelo y Vuelo_C) se conecta con sus triggers,
 * cada trigger tiene una nota con el evento que lo dispara y un cuadro con lo que hace.
 *
 * Crea, dentro del paquete seleccionado en el Project Browser, el paquete
 * "Actividad - Bitácora de cambios" con el diagrama adentro.
 *
 * Cómo usarlo (EA 13 o superior):
 *   1. Seleccioná en el Project Browser el paquete donde lo querés.
 *   2. Specialize/Tools > Scripting > New Normal Group (o uno existente) > New JavaScript script.
 *   3. Pegá este archivo completo, guardá y ejecutalo (botón Run).
 * Si lo volvés a ejecutar, borra el paquete anterior con el mismo nombre y lo regenera.
 * Compatible con JScript y JavaScript.
 */

var PAQUETE = "Actividad - Bitácora de cambios";
var DIAGRAMA = "Diagrama de actividad - Bitácora de cambios (triggers)";

// Colores (RGB) como en el modelo
var ROSA = [248, 206, 204], AMARILLO = [255, 242, 204], VIOLETA = [225, 213, 231], CELESTE = [218, 232, 252];

// Tablas: nombre y posición vertical
var TABLAS = [
    { n: "Vuelo",   t: 150 },
    { n: "Vuelo_C", t: 536 }
];

// Triggers: tabla, nombre, evento que lo dispara (nota), qué hace (cuadro) y posición vertical
var TRIGGERS = [
    { tabla: "Vuelo", n: "TR_Vuelo_Historial", evento: "Insert / Update", t: 85,
      hace: "Mantiene Vuelo_C al día: cuando se inserta o modifica un vuelo, desactiva la versión anterior (Act = 0) y guarda la nueva como activa (Act = 1)." },
    { tabla: "Vuelo", n: "TR_Vuelo_BorradoLogico", evento: "Delete", t: 225,
      hace: "En lugar de borrar el vuelo, lo da de baja lógica (BorradoLogico = 1). La baja queda registrada como una versión nueva en Vuelo_C." },
    { tabla: "Vuelo_C", n: "TR_Vuelo_C_Activar", evento: "Update: cuando Act pasa a 1 (true)", t: 400,
      hace: "Copia los datos del registro activado a la tabla Vuelo: vuelve a dejar vigente esa versión del vuelo (rollback)." },
    { tabla: "Vuelo_C", n: "TR_Vuelo_C_SinBorrado", evento: "Delete", t: 540,
      hace: "Impide borrar registros de Vuelo_C (error 50001), así el historial nunca pierde información." },
    { tabla: "Vuelo_C", n: "TR_Vuelo_C_SoloAct", evento: "Update de otra columna que no sea Act", t: 680,
      hace: "Deshace el cambio (error 50002): de un registro histórico solo se puede modificar Act." }
];

function log(msg) { Session.Output(msg); }

function colorEA(rgb) { return rgb[0] + rgb[1] * 256 + rgb[2] * 65536; }

function nuevoElemento(paquete, nombre, tipo, rgb, notas) {
    var e = paquete.Elements.AddNew(nombre, tipo);
    if (notas) e.Notes = notas;
    e.Update();
    if (rgb) { try { e.SetAppearance(1, 0, colorEA(rgb)); e.Update(); } catch (ex) { } }
    paquete.Elements.Refresh();
    return e;
}

function ponerEnDiagrama(diagrama, elemento, x, y, ancho, alto) {
    var pos = "l=" + x + ";r=" + (x + ancho) + ";t=" + y + ";b=" + (y + alto) + ";";
    var o = diagrama.DiagramObjects.AddNew(pos, "");
    o.ElementID = elemento.ElementID;
    o.Update();
    return o;
}

function conectar(origen, destino, tipo) {
    var c = origen.Connectors.AddNew("", tipo);
    c.SupplierID = destino.ElementID;
    c.Update();
    origen.Connectors.Refresh();
    return c;
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

    var d = paquete.Diagrams.AddNew(DIAGRAMA, "Activity");
    d.Update();

    // Tablas (rosa)
    var tablas = {};
    for (i = 0; i < TABLAS.length; i++) {
        var tb = nuevoElemento(paquete, TABLAS[i].n, "Action", ROSA, null);
        ponerEnDiagrama(d, tb, 30, TABLAS[i].t, 130, 50);
        tablas[TABLAS[i].n] = tb;
    }

    // Triggers (amarillo), evento (nota violeta) y qué hace (cuadro celeste)
    for (i = 0; i < TRIGGERS.length; i++) {
        var tr = TRIGGERS[i];
        var elTr = nuevoElemento(paquete, tr.n, "Action", AMARILLO, null);
        ponerEnDiagrama(d, elTr, 280, tr.t, 200, 42);

        var nota = nuevoElemento(paquete, "", "Note", VIOLETA, tr.evento);
        ponerEnDiagrama(d, nota, 270, tr.t - 65, 220, 40);

        var cuadro = nuevoElemento(paquete, tr.hace, "Action", CELESTE, null);
        ponerEnDiagrama(d, cuadro, 580, tr.t - 14, 380, 70);

        conectar(tablas[tr.tabla], elTr, "ControlFlow");   // tabla -> trigger
        conectar(elTr, cuadro, "ControlFlow");             // trigger -> qué hace
        conectar(nota, elTr, "NoteLink");                  // nota del evento pegada al trigger
    }

    d.DiagramObjects.Refresh();
    try { Repository.ReloadDiagram(d.DiagramID); } catch (e) { }
    Repository.RefreshModelView(destino.PackageID);
    log("Listo: '" + DIAGRAMA + "' en el paquete '" + PAQUETE + "'.");
}

main();
