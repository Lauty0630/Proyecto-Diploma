/*
 * FLY SAFE - 3ra entrega: todos los diagramas para Enterprise Architect
 * ------------------------------------------------------------------------------------
 * Genera, dentro del paquete seleccionado en el Project Browser:
 *   FLY SAFE - 3ra entrega
 *     Modelo de clases  (UI / BLL / DAL / BE / Servicios)  -> todas las clases del proyecto (C#)
 *     Modelo de datos                                       -> todas las tablas, con PK y FK
 *     Diagramas de clases
 *        DC general de negocio                        entidades del negocio: atributos, métodos y relaciones
 *        DC general de servicios                      solo clases de la capa Servicios: atributos y métodos
 *        DC BE Negocio (atributos)
 *        DC BE Servicios (atributos)
 *        DC BLL Negocio (métodos)
 *        DC BLL Servicio (métodos)
 *        DC DAL (completo)
 *     Ningún diagrama muestra el paquete (BE::, BLL::...) delante del nombre de la clase.
 *     Las líneas se dibujan rectas y están distribuidas para que no se crucen ni pasen por encima de
 *     una clase. Si después movés una clase, EA vuelve a trazarla.
 *     Atributos, métodos y parámetros salen del código compilado (balanceo exacto con el sistema). En los
 *     diagramas no se usa "Id": la clave de cada clase se llama Codigo<Clase> (Vuelo: NumeroVuelo) y los
 *     parámetros id/idX pasan a codigo/codigoX. Las entidades débiles (solo Id y Nombre: Aerolinea,
 *     Modulo y TipoEvento) no se dibujan, ni los enum, ni las clases vacías o con 1 o 2 atributos en BE.
 *     Usuario_GV42 solo aparece en los diagramas de servicios.
 *     Diagrama Entidad - Relación
 *        DER completo (tipo Martin, pata de gallo)     un solo DER, negocio y seguridad / servicios separados
 *     Bitácora de cambios
 *        Registro de cambios (BackEnd)                dos diagramas de actividad (triggers)
 *        Auditoría de cambios (FrontEnd)              casos de uso, dos secuencias, clases y DER (Vuelo_C | Vuelo)
 *
 * Notas:
 *   - En los diagramas de BE y BLL el script oculta atributos y/o métodos solo en ese diagrama. Si tu versión de
 *     EA no lo aplica, seleccioná todo (Ctrl+A) y usá Feature and Compartment Visibility (Ctrl+Shift+Y).
 *   - Si en algún diagrama todavía ves "BE::" delante del nombre: doble clic en el fondo del diagrama >
 *     Diagram > destildá "Show Namespace" (y para todos: Start > Preferences > Diagram > Show Namespace).
 *   - En el DER, la notación "pata de gallo" se pide con Information Engineering. Si no cambia sola:
 *     doble clic en el fondo del diagrama > Connectors > Connector Notation = Information Engineering.
 *
 * Compatible con JScript y JavaScript (los dos motores de scripting de EA).
 *
 * Cómo usarlo (EA 13 o superior):
 *   1. Seleccioná en el Project Browser el paquete donde querés los diagramas.
 *   2. Specialize/Tools > Scripting > New Normal Group (o uno existente) > New JavaScript script.
 *   3. Pegá este archivo completo, guardá y ejecutalo (botón Run). Tarda un poco: son muchas clases.
 *   4. El avance se ve en la ventana Output (pestaña Script).
 * Si lo volvés a ejecutar, borra el paquete "FLY SAFE - 3ra entrega" anterior y lo regenera.
 *
 * Generado a partir del código del proyecto (18/11/2026).
 */

var RAIZ = "FLY SAFE - 3ra entrega";

var DIAGRAMAS = [
 {
  "nombre": "DC general de negocio",
  "modo": "todo",
  "objs": [
   {
    "n": "AdicionalReserva_GV42",
    "l": 50,
    "t": 1391,
    "w": 228,
    "h": 150
   },
   {
    "n": "Aeropuerto_GV42",
    "l": 2246,
    "t": 614,
    "w": 170,
    "h": 158
   },
   {
    "n": "AsientoDisponibilidad_GV42",
    "l": 2783,
    "t": 340,
    "w": 210,
    "h": 136
   },
   {
    "n": "AsientoPasajero_GV42",
    "l": 2191,
    "t": 80,
    "w": 414,
    "h": 144
   },
   {
    "n": "Asiento_GV42",
    "l": 2824,
    "t": 560,
    "w": 170,
    "h": 172
   },
   {
    "n": "BoletoElectronico_GV42",
    "l": 54,
    "t": 624,
    "w": 270,
    "h": 682
   },
   {
    "n": "Boleto_GV42",
    "l": 937,
    "t": 896,
    "w": 170,
    "h": 178
   },
   {
    "n": "CargoExcesoEquipaje_GV42",
    "l": 1431,
    "t": 1544,
    "w": 198,
    "h": 178
   },
   {
    "n": "CheckIn_GV42",
    "l": 2034,
    "t": 1253,
    "w": 324,
    "h": 514
   },
   {
    "n": "Cliente_GV42",
    "l": 1043,
    "t": 1385,
    "w": 582,
    "h": 80
   },
   {
    "n": "CriterioBusquedaVuelo_GV42",
    "l": 1961,
    "t": 571,
    "w": 210,
    "h": 150
   },
   {
    "n": "Equipaje_GV42",
    "l": 1707,
    "t": 1531,
    "w": 252,
    "h": 178
   },
   {
    "n": "Pago_GV42",
    "l": 1127,
    "t": 61,
    "w": 198,
    "h": 178
   },
   {
    "n": "Pasajero_GV42",
    "l": 1189,
    "t": 873,
    "w": 588,
    "h": 200
   },
   {
    "n": "Persona_GV42",
    "l": 1019,
    "t": 1136,
    "w": 582,
    "h": 172
   },
   {
    "n": "Reembolso_GV42",
    "l": 1768,
    "t": 60,
    "w": 198,
    "h": 178
   },
   {
    "n": "Reserva_GV42",
    "l": 1395,
    "t": 109,
    "w": 312,
    "h": 550
   },
   {
    "n": "TarifaFamilia_GV42",
    "l": 946,
    "t": 300,
    "w": 270,
    "h": 312
   },
   {
    "n": "TarjetaEmbarque_GV42",
    "l": 2433,
    "t": 1435,
    "w": 198,
    "h": 262
   },
   {
    "n": "TipoAdicional_GV42",
    "l": 346,
    "t": 1502,
    "w": 240,
    "h": 242
   },
   {
    "n": "VueloCambio_GV42",
    "l": 2309,
    "t": 289,
    "w": 186,
    "h": 248
   },
   {
    "n": "VueloClase_GV42",
    "l": 2081,
    "t": 841,
    "w": 204,
    "h": 248
   },
   {
    "n": "Vuelo_GV42",
    "l": 2559,
    "t": 544,
    "w": 186,
    "h": 206
   }
  ],
  "marcos": [],
  "enlaces": [],
  "ocultar": [],
  "visibles": [
   [
    "Cliente_GV42",
    "Persona_GV42",
    "Generalization"
   ],
   [
    "Pasajero_GV42",
    "Persona_GV42",
    "Generalization"
   ],
   [
    "AdicionalReserva_GV42",
    "TipoAdicional_GV42",
    "Association"
   ],
   [
    "AsientoDisponibilidad_GV42",
    "Asiento_GV42",
    "Association"
   ],
   [
    "AsientoPasajero_GV42",
    "Asiento_GV42",
    "Association"
   ],
   [
    "Boleto_GV42",
    "Pasajero_GV42",
    "Association"
   ],
   [
    "BoletoElectronico_GV42",
    "AdicionalReserva_GV42",
    "Association"
   ],
   [
    "CheckIn_GV42",
    "Pasajero_GV42",
    "Association"
   ],
   [
    "CheckIn_GV42",
    "VueloClase_GV42",
    "Association"
   ],
   [
    "CheckIn_GV42",
    "AdicionalReserva_GV42",
    "Association"
   ],
   [
    "CheckIn_GV42",
    "Asiento_GV42",
    "Association"
   ],
   [
    "CheckIn_GV42",
    "Equipaje_GV42",
    "Association"
   ],
   [
    "CheckIn_GV42",
    "TarjetaEmbarque_GV42",
    "Association"
   ],
   [
    "CheckIn_GV42",
    "Vuelo_GV42",
    "Association"
   ],
   [
    "Equipaje_GV42",
    "CargoExcesoEquipaje_GV42",
    "Association"
   ],
   [
    "Reserva_GV42",
    "Pasajero_GV42",
    "Association"
   ],
   [
    "Reserva_GV42",
    "VueloClase_GV42",
    "Association"
   ],
   [
    "Reserva_GV42",
    "TarifaFamilia_GV42",
    "Association"
   ],
   [
    "Reserva_GV42",
    "AsientoPasajero_GV42",
    "Association"
   ],
   [
    "Reserva_GV42",
    "AdicionalReserva_GV42",
    "Association"
   ],
   [
    "Reserva_GV42",
    "Pago_GV42",
    "Association"
   ],
   [
    "Reserva_GV42",
    "Reembolso_GV42",
    "Association"
   ],
   [
    "Reserva_GV42",
    "Vuelo_GV42",
    "Association"
   ],
   [
    "VueloClase_GV42",
    "Vuelo_GV42",
    "Association"
   ],
   [
    "Vuelo_GV42",
    "Aeropuerto_GV42",
    "Association"
   ],
   [
    "Asiento_GV42",
    "Vuelo_GV42",
    "Association"
   ],
   [
    "CheckIn_GV42",
    "Reserva_GV42",
    "Association"
   ],
   [
    "CriterioBusquedaVuelo_GV42",
    "Aeropuerto_GV42",
    "Association"
   ],
   [
    "VueloCambio_GV42",
    "Vuelo_GV42",
    "Association"
   ]
  ],
  "notas": []
 },
 {
  "nombre": "DC general de servicios",
  "modo": "todo",
  "objs": [
   {
    "n": "Familia_GV42",
    "l": 742,
    "t": 61,
    "w": 294,
    "h": 158
   },
   {
    "n": "IComponentePermiso_GV42",
    "l": 349,
    "t": 60,
    "w": 294,
    "h": 116
   },
   {
    "n": "Patente_GV42",
    "l": 570,
    "t": 300,
    "w": 378,
    "h": 158
   },
   {
    "n": "Rol_GV42",
    "l": 181,
    "t": 249,
    "w": 294,
    "h": 200
   },
   {
    "n": "SessionManager_GV42",
    "l": 759,
    "t": 730,
    "w": 282,
    "h": 172
   },
   {
    "n": "Usuario_GV42",
    "l": 50,
    "t": 540,
    "w": 620,
    "h": 410
   },
   {
    "n": "IObservadorIdioma_GV42",
    "l": 1289,
    "t": 97,
    "w": 186,
    "h": 80
   },
   {
    "n": "IdiomaManager_GV42",
    "l": 1151,
    "t": 268,
    "w": 438,
    "h": 382
   },
   {
    "n": "Validaciones_GV42",
    "l": 1680,
    "t": 60,
    "w": 438,
    "h": 774
   },
   {
    "n": "Encriptador_GV42",
    "l": 50,
    "t": 1060,
    "w": 498,
    "h": 242
   },
   {
    "n": "Bitacora_GV42",
    "l": 658,
    "t": 1060,
    "w": 620,
    "h": 214
   },
   {
    "n": "ConfiguracionBD_GV42",
    "l": 1388,
    "t": 1060,
    "w": 426,
    "h": 186
   },
   {
    "n": "EncriptadorReversible_GV42",
    "l": 1924,
    "t": 1060,
    "w": 282,
    "h": 158
   },
   {
    "n": "DetectorInstancias_GV42",
    "l": 50,
    "t": 1412,
    "w": 408,
    "h": 116
   },
   {
    "n": "CalculadorIntegridad_GV42",
    "l": 568,
    "t": 1412,
    "w": 306,
    "h": 108
   },
   {
    "n": "EstiloXmlClientes_GV42",
    "l": 984,
    "t": 1412,
    "w": 186,
    "h": 94
   },
   {
    "n": "SerializadorXml_GV42",
    "l": 1280,
    "t": 1412,
    "w": 456,
    "h": 80
   },
   {
    "n": "Entregas_GV42",
    "l": 1846,
    "t": 1412,
    "w": 170,
    "h": 66
   }
  ],
  "marcos": [],
  "enlaces": [],
  "ocultar": [
   [
    "IComponentePermiso_GV42",
    "Patente_GV42",
    "Dependency"
   ]
  ],
  "visibles": [
   [
    "Familia_GV42",
    "IComponentePermiso_GV42",
    "Realisation"
   ],
   [
    "Patente_GV42",
    "IComponentePermiso_GV42",
    "Realisation"
   ],
   [
    "Rol_GV42",
    "IComponentePermiso_GV42",
    "Realisation"
   ],
   [
    "IdiomaManager_GV42",
    "IObservadorIdioma_GV42",
    "Association"
   ],
   [
    "SessionManager_GV42",
    "Usuario_GV42",
    "Association"
   ],
   [
    "Usuario_GV42",
    "Rol_GV42",
    "Association"
   ],
   [
    "Familia_GV42",
    "Patente_GV42",
    "Dependency"
   ],
   [
    "Rol_GV42",
    "Patente_GV42",
    "Dependency"
   ],
   [
    "Validaciones_GV42",
    "IdiomaManager_GV42",
    "Dependency"
   ]
  ],
  "notas": []
 },
 {
  "nombre": "DC BE Negocio (atributos)",
  "modo": "attrs",
  "objs": [
   {
    "n": "AdicionalReporte_GV42",
    "l": 189,
    "t": 644,
    "w": 180,
    "h": 108
   },
   {
    "n": "AdicionalReserva_GV42",
    "l": 989,
    "t": 1888,
    "w": 228,
    "h": 150
   },
   {
    "n": "Aeropuerto_GV42",
    "l": 2572,
    "t": 457,
    "w": 170,
    "h": 136
   },
   {
    "n": "AsientoDisponibilidad_GV42",
    "l": 2763,
    "t": 912,
    "w": 210,
    "h": 136
   },
   {
    "n": "AsientoPasajero_GV42",
    "l": 2753,
    "t": 60,
    "w": 174,
    "h": 108
   },
   {
    "n": "Asiento_GV42",
    "l": 2777,
    "t": 676,
    "w": 170,
    "h": 150
   },
   {
    "n": "BoletoElectronico_GV42",
    "l": 655,
    "t": 1326,
    "w": 270,
    "h": 682
   },
   {
    "n": "Boleto_GV42",
    "l": 1602,
    "t": 1305,
    "w": 170,
    "h": 178
   },
   {
    "n": "CargoExcesoEquipaje_GV42",
    "l": 2483,
    "t": 1874,
    "w": 198,
    "h": 178
   },
   {
    "n": "CheckIn_GV42",
    "l": 2225,
    "t": 996,
    "w": 324,
    "h": 514
   },
   {
    "n": "ContratacionAdicional_GV42",
    "l": 74,
    "t": 1864,
    "w": 210,
    "h": 94
   },
   {
    "n": "CriterioBusquedaVuelo_GV42",
    "l": 2600,
    "t": 229,
    "w": 210,
    "h": 150
   },
   {
    "n": "Equipaje_GV42",
    "l": 2463,
    "t": 1618,
    "w": 252,
    "h": 178
   },
   {
    "n": "OfertaCliente_GV42",
    "l": 369,
    "t": 2288,
    "w": 180,
    "h": 108
   },
   {
    "n": "OfertasCliente_GV42",
    "l": 626,
    "t": 2252,
    "w": 228,
    "h": 122
   },
   {
    "n": "Pago_GV42",
    "l": 768,
    "t": 1061,
    "w": 198,
    "h": 178
   },
   {
    "n": "Pasajero_GV42",
    "l": 1610,
    "t": 1092,
    "w": 240,
    "h": 136
   },
   {
    "n": "Persona_GV42",
    "l": 1837,
    "t": 1292,
    "w": 170,
    "h": 136
   },
   {
    "n": "Reembolso_GV42",
    "l": 1179,
    "t": 376,
    "w": 198,
    "h": 178
   },
   {
    "n": "ReglaAsociacion_GV42",
    "l": 198,
    "t": 2027,
    "w": 174,
    "h": 136
   },
   {
    "n": "ReporteCheckIn_GV42",
    "l": 2623,
    "t": 1138,
    "w": 228,
    "h": 416
   },
   {
    "n": "ReporteReserva_GV42",
    "l": 444,
    "t": 473,
    "w": 270,
    "h": 458
   },
   {
    "n": "Reserva_GV42",
    "l": 802,
    "t": 458,
    "w": 312,
    "h": 528
   },
   {
    "n": "TarifaFamilia_GV42",
    "l": 1058,
    "t": 1059,
    "w": 234,
    "h": 276
   },
   {
    "n": "TarjetaEmbarque_GV42",
    "l": 2204,
    "t": 1584,
    "w": 198,
    "h": 262
   },
   {
    "n": "TipoAdicional_GV42",
    "l": 50,
    "t": 2232,
    "w": 240,
    "h": 220
   },
   {
    "n": "VueloCambio_GV42",
    "l": 2261,
    "t": 495,
    "w": 186,
    "h": 248
   },
   {
    "n": "VueloClase_GV42",
    "l": 1953,
    "t": 813,
    "w": 204,
    "h": 248
   },
   {
    "n": "Vuelo_GV42",
    "l": 2512,
    "t": 677,
    "w": 186,
    "h": 206
   },
   {
    "n": "ReporteMillas_GV42",
    "l": 50,
    "t": 2562,
    "w": 276,
    "h": 528
   },
   {
    "n": "VueloVolado_GV42",
    "l": 401,
    "t": 2674,
    "w": 170,
    "h": 276
   }
  ],
  "marcos": [],
  "enlaces": [],
  "ocultar": [],
  "visibles": [
   [
    "Pasajero_GV42",
    "Persona_GV42",
    "Generalization"
   ],
   [
    "AdicionalReserva_GV42",
    "TipoAdicional_GV42",
    "Association"
   ],
   [
    "AsientoDisponibilidad_GV42",
    "Asiento_GV42",
    "Association"
   ],
   [
    "AsientoPasajero_GV42",
    "Asiento_GV42",
    "Association"
   ],
   [
    "Boleto_GV42",
    "Pasajero_GV42",
    "Association"
   ],
   [
    "BoletoElectronico_GV42",
    "AdicionalReserva_GV42",
    "Association"
   ],
   [
    "CheckIn_GV42",
    "Pasajero_GV42",
    "Association"
   ],
   [
    "CheckIn_GV42",
    "VueloClase_GV42",
    "Association"
   ],
   [
    "CheckIn_GV42",
    "AdicionalReserva_GV42",
    "Association"
   ],
   [
    "CheckIn_GV42",
    "Asiento_GV42",
    "Association"
   ],
   [
    "CheckIn_GV42",
    "Equipaje_GV42",
    "Association"
   ],
   [
    "CheckIn_GV42",
    "TarjetaEmbarque_GV42",
    "Association"
   ],
   [
    "CheckIn_GV42",
    "Vuelo_GV42",
    "Association"
   ],
   [
    "Equipaje_GV42",
    "CargoExcesoEquipaje_GV42",
    "Association"
   ],
   [
    "Reserva_GV42",
    "Pasajero_GV42",
    "Association"
   ],
   [
    "Reserva_GV42",
    "VueloClase_GV42",
    "Association"
   ],
   [
    "Reserva_GV42",
    "TarifaFamilia_GV42",
    "Association"
   ],
   [
    "Reserva_GV42",
    "AsientoPasajero_GV42",
    "Association"
   ],
   [
    "Reserva_GV42",
    "AdicionalReserva_GV42",
    "Association"
   ],
   [
    "Reserva_GV42",
    "Pago_GV42",
    "Association"
   ],
   [
    "Reserva_GV42",
    "Reembolso_GV42",
    "Association"
   ],
   [
    "Reserva_GV42",
    "Vuelo_GV42",
    "Association"
   ],
   [
    "ReporteMillas_GV42",
    "VueloVolado_GV42",
    "Association"
   ],
   [
    "OfertaCliente_GV42",
    "TipoAdicional_GV42",
    "Association"
   ],
   [
    "OfertasCliente_GV42",
    "OfertaCliente_GV42",
    "Association"
   ],
   [
    "ReporteReserva_GV42",
    "AdicionalReporte_GV42",
    "Association"
   ],
   [
    "VueloClase_GV42",
    "Vuelo_GV42",
    "Association"
   ],
   [
    "Vuelo_GV42",
    "Aeropuerto_GV42",
    "Association"
   ],
   [
    "Asiento_GV42",
    "Vuelo_GV42",
    "Association"
   ],
   [
    "CheckIn_GV42",
    "Reserva_GV42",
    "Association"
   ],
   [
    "CriterioBusquedaVuelo_GV42",
    "Aeropuerto_GV42",
    "Association"
   ],
   [
    "ReporteCheckIn_GV42",
    "CheckIn_GV42",
    "Association"
   ],
   [
    "ContratacionAdicional_GV42",
    "Reserva_GV42",
    "Association"
   ],
   [
    "ContratacionAdicional_GV42",
    "TipoAdicional_GV42",
    "Association"
   ],
   [
    "ReglaAsociacion_GV42",
    "TipoAdicional_GV42",
    "Association"
   ],
   [
    "ReporteReserva_GV42",
    "Reserva_GV42",
    "Association"
   ],
   [
    "VueloCambio_GV42",
    "Vuelo_GV42",
    "Association"
   ]
  ],
  "notas": []
 },
 {
  "nombre": "DC BE Servicios (atributos)",
  "modo": "attrs",
  "objs": [
   {
    "n": "Familia_GV42",
    "l": 332,
    "t": 65,
    "w": 246,
    "h": 94
   },
   {
    "n": "IComponentePermiso_GV42",
    "l": 50,
    "t": 60,
    "w": 192,
    "h": 94
   },
   {
    "n": "Patente_GV42",
    "l": 183,
    "t": 227,
    "w": 170,
    "h": 94
   },
   {
    "n": "Rol_GV42",
    "l": 52,
    "t": 402,
    "w": 246,
    "h": 122
   },
   {
    "n": "Usuario_GV42",
    "l": 415,
    "t": 223,
    "w": 216,
    "h": 374
   },
   {
    "n": "ClienteXml_GV42",
    "l": 785,
    "t": 258,
    "w": 170,
    "h": 122
   },
   {
    "n": "ListaClientesXml_GV42",
    "l": 741,
    "t": 60,
    "w": 216,
    "h": 108
   },
   {
    "n": "DetalleTampering",
    "l": 1123,
    "t": 257,
    "w": 170,
    "h": 94
   },
   {
    "n": "ResultadoIntegridad",
    "l": 1067,
    "t": 60,
    "w": 228,
    "h": 108
   },
   {
    "n": "Bitacora_GV42",
    "l": 50,
    "t": 707,
    "w": 192,
    "h": 178
   }
  ],
  "marcos": [],
  "enlaces": [],
  "ocultar": [
   [
    "IComponentePermiso_GV42",
    "Patente_GV42",
    "Dependency"
   ]
  ],
  "visibles": [
   [
    "Familia_GV42",
    "IComponentePermiso_GV42",
    "Realisation"
   ],
   [
    "Patente_GV42",
    "IComponentePermiso_GV42",
    "Realisation"
   ],
   [
    "Rol_GV42",
    "IComponentePermiso_GV42",
    "Realisation"
   ],
   [
    "Usuario_GV42",
    "Rol_GV42",
    "Association"
   ],
   [
    "ListaClientesXml_GV42",
    "ClienteXml_GV42",
    "Association"
   ],
   [
    "Familia_GV42",
    "Patente_GV42",
    "Dependency"
   ],
   [
    "Rol_GV42",
    "Patente_GV42",
    "Dependency"
   ],
   [
    "ResultadoIntegridad",
    "DetalleTampering",
    "Dependency"
   ]
  ],
  "notas": []
 },
 {
  "nombre": "DC BLL Negocio (métodos)",
  "modo": "metodos",
  "objs": [
   {
    "n": "BLLAerolinea_GV42",
    "l": 2676,
    "t": 732,
    "w": 282,
    "h": 136
   },
   {
    "n": "BLLAeropuerto_GV42",
    "l": 3031,
    "t": 714,
    "w": 432,
    "h": 150
   },
   {
    "n": "BLLBoleto_GV42",
    "l": 1243,
    "t": 1095,
    "w": 504,
    "h": 108
   },
   {
    "n": "BLLCheckIn_GV42",
    "l": 1442,
    "t": 2287,
    "w": 620,
    "h": 486
   },
   {
    "n": "BLLCliente_GV42",
    "l": 3170,
    "t": 227,
    "w": 426,
    "h": 150
   },
   {
    "n": "BLLDocumentosCheckIn_GV42",
    "l": 738,
    "t": 2132,
    "w": 620,
    "h": 122
   },
   {
    "n": "BLLNegocioUtil_GV42",
    "l": 2607,
    "t": 481,
    "w": 546,
    "h": 164
   },
   {
    "n": "BLLOfertasCliente_GV42",
    "l": 1093,
    "t": 648,
    "w": 620,
    "h": 178
   },
   {
    "n": "BLLReporteCheckIn_GV42",
    "l": 730,
    "t": 2411,
    "w": 620,
    "h": 234
   },
   {
    "n": "BLLReporteMillas_GV42",
    "l": 402,
    "t": 735,
    "w": 620,
    "h": 472
   },
   {
    "n": "BLLReporteReserva_GV42",
    "l": 50,
    "t": 2289,
    "w": 620,
    "h": 234
   },
   {
    "n": "BLLReserva_GV42",
    "l": 1809,
    "t": 244,
    "w": 620,
    "h": 962
   },
   {
    "n": "BLLVueloHistorial_GV42",
    "l": 1773,
    "t": 60,
    "w": 620,
    "h": 122
   },
   {
    "n": "BLLVuelo_GV42",
    "l": 2489,
    "t": 172,
    "w": 620,
    "h": 220
   },
   {
    "n": "Code128_GV42",
    "l": 2120,
    "t": 1483,
    "w": 612,
    "h": 80
   },
   {
    "n": "DibujoDocumento_GV42",
    "l": 1053,
    "t": 1599,
    "w": 620,
    "h": 304
   },
   {
    "n": "DisenioBoleto_GV42",
    "l": 1426,
    "t": 1267,
    "w": 620,
    "h": 192
   },
   {
    "n": "DisenioEtiquetaEquipaje_GV42",
    "l": 1467,
    "t": 2048,
    "w": 528,
    "h": 66
   },
   {
    "n": "DisenioTarjetaEmbarque_GV42",
    "l": 2122,
    "t": 2170,
    "w": 620,
    "h": 206
   },
   {
    "n": "GeneradorPdf_GV42",
    "l": 57,
    "t": 1857,
    "w": 620,
    "h": 304
   },
   {
    "n": "ILienzo_GV42",
    "l": 1862,
    "t": 1652,
    "w": 620,
    "h": 136
   },
   {
    "n": "LienzoPdf_GV42",
    "l": 686,
    "t": 1275,
    "w": 620,
    "h": 192
   }
  ],
  "marcos": [],
  "enlaces": [],
  "ocultar": [
   [
    "BLLBoleto_GV42",
    "BLLNegocioUtil_GV42",
    "Dependency"
   ],
   [
    "DisenioBoleto_GV42",
    "BLLBoleto_GV42",
    "Dependency"
   ],
   [
    "DisenioBoleto_GV42",
    "Code128_GV42",
    "Dependency"
   ],
   [
    "BLLCheckIn_GV42",
    "BLLNegocioUtil_GV42",
    "Dependency"
   ],
   [
    "DisenioEtiquetaEquipaje_GV42",
    "Code128_GV42",
    "Dependency"
   ],
   [
    "DisenioTarjetaEmbarque_GV42",
    "Code128_GV42",
    "Dependency"
   ],
   [
    "BLLReporteCheckIn_GV42",
    "BLLNegocioUtil_GV42",
    "Dependency"
   ],
   [
    "BLLReporteMillas_GV42",
    "BLLNegocioUtil_GV42",
    "Dependency"
   ],
   [
    "BLLOfertasCliente_GV42",
    "BLLNegocioUtil_GV42",
    "Dependency"
   ],
   [
    "BLLOfertasCliente_GV42",
    "BLLReporteMillas_GV42",
    "Dependency"
   ],
   [
    "BLLReporteReserva_GV42",
    "BLLNegocioUtil_GV42",
    "Dependency"
   ],
   [
    "BLLVueloHistorial_GV42",
    "BLLNegocioUtil_GV42",
    "Dependency"
   ]
  ],
  "visibles": [
   [
    "LienzoPdf_GV42",
    "ILienzo_GV42",
    "Realisation"
   ],
   [
    "BLLBoleto_GV42",
    "BLLReserva_GV42",
    "Dependency"
   ],
   [
    "BLLBoleto_GV42",
    "DisenioBoleto_GV42",
    "Dependency"
   ],
   [
    "BLLBoleto_GV42",
    "LienzoPdf_GV42",
    "Dependency"
   ],
   [
    "Code128_GV42",
    "ILienzo_GV42",
    "Dependency"
   ],
   [
    "DisenioBoleto_GV42",
    "ILienzo_GV42",
    "Dependency"
   ],
   [
    "LienzoPdf_GV42",
    "GeneradorPdf_GV42",
    "Dependency"
   ],
   [
    "BLLDocumentosCheckIn_GV42",
    "DibujoDocumento_GV42",
    "Dependency"
   ],
   [
    "BLLDocumentosCheckIn_GV42",
    "DisenioEtiquetaEquipaje_GV42",
    "Dependency"
   ],
   [
    "BLLDocumentosCheckIn_GV42",
    "DisenioTarjetaEmbarque_GV42",
    "Dependency"
   ],
   [
    "BLLDocumentosCheckIn_GV42",
    "GeneradorPdf_GV42",
    "Dependency"
   ],
   [
    "BLLDocumentosCheckIn_GV42",
    "ILienzo_GV42",
    "Dependency"
   ],
   [
    "BLLDocumentosCheckIn_GV42",
    "LienzoPdf_GV42",
    "Dependency"
   ],
   [
    "DibujoDocumento_GV42",
    "ILienzo_GV42",
    "Dependency"
   ],
   [
    "DisenioEtiquetaEquipaje_GV42",
    "ILienzo_GV42",
    "Dependency"
   ],
   [
    "DisenioTarjetaEmbarque_GV42",
    "BLLCheckIn_GV42",
    "Dependency"
   ],
   [
    "DisenioTarjetaEmbarque_GV42",
    "ILienzo_GV42",
    "Dependency"
   ],
   [
    "BLLReserva_GV42",
    "BLLNegocioUtil_GV42",
    "Dependency"
   ],
   [
    "BLLReporteCheckIn_GV42",
    "BLLCheckIn_GV42",
    "Dependency"
   ],
   [
    "BLLReporteCheckIn_GV42",
    "GeneradorPdf_GV42",
    "Dependency"
   ],
   [
    "BLLReporteMillas_GV42",
    "BLLOfertasCliente_GV42",
    "Dependency"
   ],
   [
    "BLLReporteMillas_GV42",
    "GeneradorPdf_GV42",
    "Dependency"
   ],
   [
    "BLLOfertasCliente_GV42",
    "BLLReserva_GV42",
    "Dependency"
   ],
   [
    "BLLReporteReserva_GV42",
    "GeneradorPdf_GV42",
    "Dependency"
   ],
   [
    "BLLVuelo_GV42",
    "BLLNegocioUtil_GV42",
    "Dependency"
   ],
   [
    "BLLCliente_GV42",
    "BLLNegocioUtil_GV42",
    "Dependency"
   ],
   [
    "BLLAerolinea_GV42",
    "BLLNegocioUtil_GV42",
    "Dependency"
   ],
   [
    "BLLAeropuerto_GV42",
    "BLLNegocioUtil_GV42",
    "Dependency"
   ],
   [
    "BLLVueloHistorial_GV42",
    "BLLVuelo_GV42",
    "Dependency"
   ]
  ],
  "notas": [
   {
    "texto": "BLLNegocioUtil_GV42 (constantes de módulo, formato y bitácora comunes) la usan casi todas las clases BLL del negocio; se dibuja la línea solo en las que no tienen otra relación.",
    "l": 3223,
    "t": 481,
    "w": 340,
    "h": 124
   },
   {
    "texto": "Code128_GV42 genera el código de barras que dibujan DisenioBoleto_GV42, DisenioTarjetaEmbarque_GV42 y DisenioEtiquetaEquipaje_GV42.",
    "l": 2802,
    "t": 1483,
    "w": 340,
    "h": 92
   }
  ]
 },
 {
  "nombre": "DC BLL Servicio (métodos)",
  "modo": "metodos",
  "objs": [
   {
    "n": "BLLBitacora_GV42",
    "l": 555,
    "t": 483,
    "w": 620,
    "h": 150
   },
   {
    "n": "BLLInstalador_GV42",
    "l": 50,
    "t": 376,
    "w": 444,
    "h": 248
   },
   {
    "n": "BLLIntegridad_GV42",
    "l": 306,
    "t": 698,
    "w": 498,
    "h": 318
   },
   {
    "n": "BLLPermisos_GV42",
    "l": 898,
    "t": 708,
    "w": 620,
    "h": 500
   },
   {
    "n": "BLLReinstalador_GV42",
    "l": 82,
    "t": 1081,
    "w": 396,
    "h": 122
   },
   {
    "n": "BLLUsuario_GV42",
    "l": 883,
    "t": 60,
    "w": 620,
    "h": 360
   },
   {
    "n": "BLLAyuda_GV42",
    "l": 50,
    "t": 1318,
    "w": 510,
    "h": 122
   },
   {
    "n": "TemaAyuda_GV42",
    "l": 374,
    "t": 1510,
    "w": 170,
    "h": 122,
    "modo": "attrs"
   }
  ],
  "marcos": [],
  "enlaces": [],
  "ocultar": [],
  "visibles": [
   [
    "BLLInstalador_GV42",
    "BLLIntegridad_GV42",
    "Dependency"
   ],
   [
    "BLLPermisos_GV42",
    "BLLBitacora_GV42",
    "Dependency"
   ],
   [
    "BLLPermisos_GV42",
    "BLLIntegridad_GV42",
    "Dependency"
   ],
   [
    "BLLUsuario_GV42",
    "BLLBitacora_GV42",
    "Dependency"
   ],
   [
    "BLLUsuario_GV42",
    "BLLInstalador_GV42",
    "Dependency"
   ],
   [
    "BLLUsuario_GV42",
    "BLLPermisos_GV42",
    "Dependency"
   ],
   [
    "BLLIntegridad_GV42",
    "BLLBitacora_GV42",
    "Dependency"
   ],
   [
    "BLLAyuda_GV42",
    "TemaAyuda_GV42",
    "Dependency"
   ],
   [
    "BLLReinstalador_GV42",
    "BLLInstalador_GV42",
    "Dependency"
   ],
   [
    "BLLReinstalador_GV42",
    "BLLIntegridad_GV42",
    "Dependency"
   ],
   [
    "BLLReinstalador_GV42",
    "BLLPermisos_GV42",
    "Dependency"
   ]
  ],
  "notas": []
 },
 {
  "nombre": "DC DAL (completo)",
  "modo": "todo",
  "objs": [
   {
    "n": "Acceso",
    "l": 1855,
    "t": 2079,
    "w": 620,
    "h": 312
   },
   {
    "n": "DALAerolinea_GV42",
    "l": 1206,
    "t": 1946,
    "w": 240,
    "h": 158
   },
   {
    "n": "DALAeropuerto_GV42",
    "l": 2552,
    "t": 1985,
    "w": 378,
    "h": 172
   },
   {
    "n": "DALAsiento_GV42",
    "l": 731,
    "t": 2847,
    "w": 546,
    "h": 228
   },
   {
    "n": "DALBitacora_GV42",
    "l": 733,
    "t": 1565,
    "w": 620,
    "h": 270
   },
   {
    "n": "DALBoleto_GV42",
    "l": 2611,
    "t": 2408,
    "w": 378,
    "h": 102
   },
   {
    "n": "DALCheckIn_GV42",
    "l": 579,
    "t": 3441,
    "w": 564,
    "h": 172
   },
   {
    "n": "DALCliente_GV42",
    "l": 2410,
    "t": 3117,
    "w": 288,
    "h": 214
   },
   {
    "n": "DALEquipaje_GV42",
    "l": 1372,
    "t": 3059,
    "w": 336,
    "h": 116
   },
   {
    "n": "DALFamilia_GV42",
    "l": 2787,
    "t": 60,
    "w": 620,
    "h": 284
   },
   {
    "n": "DALIntegridad_GV42",
    "l": 2765,
    "t": 3380,
    "w": 600,
    "h": 480
   },
   {
    "n": "DALPago_GV42",
    "l": 2214,
    "t": 3404,
    "w": 336,
    "h": 116
   },
   {
    "n": "DALPasajero_GV42",
    "l": 1846,
    "t": 1523,
    "w": 318,
    "h": 158
   },
   {
    "n": "DALPatente_GV42",
    "l": 2491,
    "t": 406,
    "w": 282,
    "h": 130
   },
   {
    "n": "DALReporteCheckIn_GV42",
    "l": 2404,
    "t": 1790,
    "w": 438,
    "h": 130
   },
   {
    "n": "DALReporteMillas_GV42",
    "l": 1455,
    "t": 1750,
    "w": 366,
    "h": 116
   },
   {
    "n": "DALReporteReserva_GV42",
    "l": 1158,
    "t": 2165,
    "w": 620,
    "h": 158
   },
   {
    "n": "DALReserva_GV42",
    "l": 1890,
    "t": 3741,
    "w": 620,
    "h": 368
   },
   {
    "n": "DALRol_GV42",
    "l": 2082,
    "t": 75,
    "w": 620,
    "h": 256
   },
   {
    "n": "DALTarifa_GV42",
    "l": 1841,
    "t": 3127,
    "w": 318,
    "h": 144
   },
   {
    "n": "DALTarjetaEmbarque_GV42",
    "l": 50,
    "t": 2834,
    "w": 620,
    "h": 116
   },
   {
    "n": "DALTipoAdicional_GV42",
    "l": 2553,
    "t": 2241,
    "w": 276,
    "h": 102
   },
   {
    "n": "DALUsuario_GV42",
    "l": 2728,
    "t": 2615,
    "w": 594,
    "h": 438
   },
   {
    "n": "DALUtil_GV42",
    "l": 691,
    "t": 1906,
    "w": 402,
    "h": 242
   },
   {
    "n": "DALVueloHistorial_GV42",
    "l": 1287,
    "t": 1280,
    "w": 620,
    "h": 158
   },
   {
    "n": "DALVuelo_GV42",
    "l": 1363,
    "t": 3340,
    "w": 620,
    "h": 298
   },
   {
    "n": "InstaladorBD_GV42",
    "l": 3082,
    "t": 1956,
    "w": 528,
    "h": 480
   }
  ],
  "marcos": [],
  "enlaces": [],
  "ocultar": [
   [
    "DALAeropuerto_GV42",
    "DALUtil_GV42",
    "Dependency"
   ],
   [
    "DALAsiento_GV42",
    "DALUtil_GV42",
    "Dependency"
   ],
   [
    "DALBoleto_GV42",
    "DALUtil_GV42",
    "Dependency"
   ],
   [
    "DALCheckIn_GV42",
    "DALUtil_GV42",
    "Dependency"
   ],
   [
    "DALCliente_GV42",
    "DALUtil_GV42",
    "Dependency"
   ],
   [
    "DALPasajero_GV42",
    "DALUtil_GV42",
    "Dependency"
   ],
   [
    "DALEquipaje_GV42",
    "DALUtil_GV42",
    "Dependency"
   ],
   [
    "DALPago_GV42",
    "DALUtil_GV42",
    "Dependency"
   ],
   [
    "DALReserva_GV42",
    "DALUtil_GV42",
    "Dependency"
   ],
   [
    "DALReporteCheckIn_GV42",
    "DALUtil_GV42",
    "Dependency"
   ],
   [
    "DALReporteMillas_GV42",
    "DALUtil_GV42",
    "Dependency"
   ],
   [
    "DALReporteReserva_GV42",
    "DALUtil_GV42",
    "Dependency"
   ],
   [
    "DALTarjetaEmbarque_GV42",
    "DALUtil_GV42",
    "Dependency"
   ],
   [
    "DALTipoAdicional_GV42",
    "DALUtil_GV42",
    "Dependency"
   ],
   [
    "DALVuelo_GV42",
    "DALUtil_GV42",
    "Dependency"
   ],
   [
    "DALVueloHistorial_GV42",
    "DALUtil_GV42",
    "Dependency"
   ],
   [
    "DALTarifa_GV42",
    "DALUtil_GV42",
    "Dependency"
   ]
  ],
  "visibles": [
   [
    "DALBitacora_GV42",
    "Acceso",
    "Dependency"
   ],
   [
    "DALFamilia_GV42",
    "Acceso",
    "Dependency"
   ],
   [
    "DALFamilia_GV42",
    "DALPatente_GV42",
    "Dependency"
   ],
   [
    "DALPatente_GV42",
    "Acceso",
    "Dependency"
   ],
   [
    "DALRol_GV42",
    "Acceso",
    "Dependency"
   ],
   [
    "DALRol_GV42",
    "DALFamilia_GV42",
    "Dependency"
   ],
   [
    "DALRol_GV42",
    "DALPatente_GV42",
    "Dependency"
   ],
   [
    "DALUsuario_GV42",
    "Acceso",
    "Dependency"
   ],
   [
    "DALUsuario_GV42",
    "DALIntegridad_GV42",
    "Dependency"
   ],
   [
    "DALIntegridad_GV42",
    "Acceso",
    "Dependency"
   ],
   [
    "InstaladorBD_GV42",
    "Acceso",
    "Dependency"
   ],
   [
    "DALAerolinea_GV42",
    "Acceso",
    "Dependency"
   ],
   [
    "DALAerolinea_GV42",
    "DALUtil_GV42",
    "Dependency"
   ],
   [
    "DALAeropuerto_GV42",
    "Acceso",
    "Dependency"
   ],
   [
    "DALAsiento_GV42",
    "Acceso",
    "Dependency"
   ],
   [
    "DALBoleto_GV42",
    "Acceso",
    "Dependency"
   ],
   [
    "DALCheckIn_GV42",
    "Acceso",
    "Dependency"
   ],
   [
    "DALCheckIn_GV42",
    "DALAsiento_GV42",
    "Dependency"
   ],
   [
    "DALCheckIn_GV42",
    "DALEquipaje_GV42",
    "Dependency"
   ],
   [
    "DALCheckIn_GV42",
    "DALReserva_GV42",
    "Dependency"
   ],
   [
    "DALCheckIn_GV42",
    "DALTarjetaEmbarque_GV42",
    "Dependency"
   ],
   [
    "DALCliente_GV42",
    "Acceso",
    "Dependency"
   ],
   [
    "DALPasajero_GV42",
    "Acceso",
    "Dependency"
   ],
   [
    "DALEquipaje_GV42",
    "Acceso",
    "Dependency"
   ],
   [
    "DALPago_GV42",
    "Acceso",
    "Dependency"
   ],
   [
    "DALReserva_GV42",
    "Acceso",
    "Dependency"
   ],
   [
    "DALReserva_GV42",
    "DALPago_GV42",
    "Dependency"
   ],
   [
    "DALReserva_GV42",
    "DALTarifa_GV42",
    "Dependency"
   ],
   [
    "DALReserva_GV42",
    "DALVuelo_GV42",
    "Dependency"
   ],
   [
    "DALReporteCheckIn_GV42",
    "Acceso",
    "Dependency"
   ],
   [
    "DALReporteMillas_GV42",
    "Acceso",
    "Dependency"
   ],
   [
    "DALReporteReserva_GV42",
    "Acceso",
    "Dependency"
   ],
   [
    "DALTarjetaEmbarque_GV42",
    "Acceso",
    "Dependency"
   ],
   [
    "DALTipoAdicional_GV42",
    "Acceso",
    "Dependency"
   ],
   [
    "DALVuelo_GV42",
    "Acceso",
    "Dependency"
   ],
   [
    "DALVueloHistorial_GV42",
    "Acceso",
    "Dependency"
   ],
   [
    "DALTarifa_GV42",
    "Acceso",
    "Dependency"
   ]
  ],
  "notas": [
   {
    "texto": "DALUtil_GV42 (conversión de filas y parámetros) lo usan todos los mappers del negocio.",
    "l": 691,
    "t": 2218,
    "w": 340,
    "h": 76
   }
  ]
 }
];

var DER = {
 "objs": [
  {
   "n": "Aerolinea",
   "l": 1681,
   "t": 278,
   "w": 190,
   "h": 92
  },
  {
   "n": "Aeropuerto",
   "l": 1661,
   "t": 83,
   "w": 190,
   "h": 134
  },
  {
   "n": "Asiento",
   "l": 1575,
   "t": 1025,
   "w": 222,
   "h": 204
  },
  {
   "n": "Boleto",
   "l": 1414,
   "t": 1611,
   "w": 216,
   "h": 162
  },
  {
   "n": "CambioReserva",
   "l": 497,
   "t": 1824,
   "w": 240,
   "h": 288
  },
  {
   "n": "CanalVenta",
   "l": 1041,
   "t": 1478,
   "w": 190,
   "h": 92
  },
  {
   "n": "CargoExcesoEquipaje",
   "l": 946,
   "t": 2021,
   "w": 240,
   "h": 204
  },
  {
   "n": "CheckIn",
   "l": 1696,
   "t": 1617,
   "w": 240,
   "h": 232
  },
  {
   "n": "ClaseVuelo",
   "l": 1295,
   "t": 1045,
   "w": 190,
   "h": 92
  },
  {
   "n": "Equipaje",
   "l": 1591,
   "t": 1934,
   "w": 204,
   "h": 162
  },
  {
   "n": "EstadoCheckIn",
   "l": 1808,
   "t": 1439,
   "w": 190,
   "h": 92
  },
  {
   "n": "EstadoReserva",
   "l": 1018,
   "t": 1633,
   "w": 190,
   "h": 92
  },
  {
   "n": "EtiquetaEquipaje",
   "l": 1867,
   "t": 2157,
   "w": 222,
   "h": 134
  },
  {
   "n": "MedioPago",
   "l": 60,
   "t": 2098,
   "w": 190,
   "h": 92
  },
  {
   "n": "Pago",
   "l": 73,
   "t": 1539,
   "w": 246,
   "h": 190
  },
  {
   "n": "Pasajero",
   "l": 1047,
   "t": 1256,
   "w": 204,
   "h": 162
  },
  {
   "n": "Reembolso",
   "l": 201,
   "t": 1808,
   "w": 228,
   "h": 204
  },
  {
   "n": "Reserva",
   "l": 670,
   "t": 1224,
   "w": 282,
   "h": 498
  },
  {
   "n": "ReservaAdicional",
   "l": 374,
   "t": 1261,
   "w": 210,
   "h": 190
  },
  {
   "n": "ReservaPasajero",
   "l": 1436,
   "t": 1315,
   "w": 216,
   "h": 204
  },
  {
   "n": "TarifaFamilia",
   "l": 555,
   "t": 945,
   "w": 282,
   "h": 190
  },
  {
   "n": "TarjetaEmbarque",
   "l": 1856,
   "t": 1930,
   "w": 252,
   "h": 162
  },
  {
   "n": "TipoAdicional",
   "l": 69,
   "t": 1219,
   "w": 216,
   "h": 162
  },
  {
   "n": "TipoViaje",
   "l": 810,
   "t": 1812,
   "w": 190,
   "h": 92
  },
  {
   "n": "Vuelo",
   "l": 1334,
   "t": 147,
   "w": 240,
   "h": 246
  },
  {
   "n": "VueloClase",
   "l": 951,
   "t": 985,
   "w": 252,
   "h": 176
  },
  {
   "n": "Vuelo_C",
   "l": 1960,
   "t": 80,
   "w": 240,
   "h": 316
  },
  {
   "n": "EVENTOS",
   "l": 3153,
   "t": 429,
   "w": 198,
   "h": 204
  },
  {
   "n": "Familia",
   "l": 2448,
   "t": 284,
   "w": 190,
   "h": 92
  },
  {
   "n": "FamiliaIntegrada",
   "l": 2370,
   "t": 80,
   "w": 192,
   "h": 120
  },
  {
   "n": "FamiliaPatente",
   "l": 2369,
   "t": 459,
   "w": 190,
   "h": 120
  },
  {
   "n": "Modulo",
   "l": 2902,
   "t": 635,
   "w": 190,
   "h": 92
  },
  {
   "n": "Patente",
   "l": 2479,
   "t": 659,
   "w": 190,
   "h": 106
  },
  {
   "n": "RolFamilia",
   "l": 2632,
   "t": 85,
   "w": 190,
   "h": 120
  },
  {
   "n": "RolPatente",
   "l": 2620,
   "t": 466,
   "w": 190,
   "h": 120
  },
  {
   "n": "Roles",
   "l": 2727,
   "t": 287,
   "w": 190,
   "h": 92
  },
  {
   "n": "TipoEvento",
   "l": 2872,
   "t": 466,
   "w": 190,
   "h": 92
  },
  {
   "n": "Usuario",
   "l": 3008,
   "t": 84,
   "w": 258,
   "h": 260
  },
  {
   "n": "IntegridadDVH",
   "l": 2369,
   "t": 875,
   "w": 210,
   "h": 120
  },
  {
   "n": "IntegridadDVV",
   "l": 2689,
   "t": 875,
   "w": 210,
   "h": 106
  }
 ],
 "marcos": [
  {
   "nombre": "Negocio",
   "l": 20,
   "t": 20,
   "w": 2219,
   "h": 2310
  },
  {
   "nombre": "Seguridad y servicios",
   "l": 2329,
   "t": 20,
   "w": 1061,
   "h": 1014
  }
 ],
 "lineas": [
  [
   "Vuelo",
   "FK_Vuelo_Aerolinea",
   0,
   0
  ],
  [
   "Vuelo",
   "FK_Vuelo_Destino",
   0,
   -26
  ],
  [
   "Vuelo",
   "FK_Vuelo_Origen",
   0,
   26
  ],
  [
   "VueloClase",
   "FK_VueloClase_Clase",
   0,
   0
  ],
  [
   "VueloClase",
   "FK_VueloClase_Vuelo",
   0,
   0
  ],
  [
   "Vuelo_C",
   "FK_Vuelo_C_Vuelo",
   0,
   0
  ],
  [
   "Vuelo_C",
   "FK_Vuelo_C_Aerolinea",
   0,
   0
  ],
  [
   "Vuelo_C",
   "FK_Vuelo_C_Origen",
   0,
   -26
  ],
  [
   "Vuelo_C",
   "FK_Vuelo_C_Destino",
   0,
   26
  ],
  [
   "Asiento",
   "FK_Asiento_Clase",
   0,
   0
  ],
  [
   "Asiento",
   "FK_Asiento_Vuelo",
   0,
   0
  ],
  [
   "Reserva",
   "FK_Reserva_CanalVenta",
   0,
   0
  ],
  [
   "Reserva",
   "FK_Reserva_Pasajero",
   0,
   0
  ],
  [
   "Reserva",
   "FK_Reserva_Estado",
   0,
   0
  ],
  [
   "Reserva",
   "FK_Reserva_TipoViaje",
   0,
   0
  ],
  [
   "Reserva",
   "FK_Reserva_VueloClase",
   -26,
   0
  ],
  [
   "Reserva",
   "FK_Reserva_VueloClaseVuelta",
   26,
   0
  ],
  [
   "Reserva",
   "FK_Reserva_Tarifa",
   0,
   0
  ],
  [
   "ReservaPasajero",
   "FK_ReservaPasajero_Asiento",
   0,
   0
  ],
  [
   "ReservaPasajero",
   "FK_ReservaPasajero_Pasajero",
   0,
   0
  ],
  [
   "ReservaPasajero",
   "FK_ReservaPasajero_Reserva",
   0,
   0
  ],
  [
   "Pago",
   "FK_Pago_Medio",
   0,
   0
  ],
  [
   "Pago",
   "FK_Pago_Reserva",
   0,
   0
  ],
  [
   "Reembolso",
   "FK_Reembolso_Reserva",
   0,
   0
  ],
  [
   "Reembolso",
   "FK_Reembolso_MedioPago",
   0,
   0
  ],
  [
   "CambioReserva",
   "FK_CambioReserva_Reserva",
   0,
   0
  ],
  [
   "CambioReserva",
   "FK_CambioReserva_MedioPago",
   0,
   0
  ],
  [
   "ReservaAdicional",
   "FK_ReservaAdicional_Reserva",
   0,
   0
  ],
  [
   "ReservaAdicional",
   "FK_ReservaAdicional_Tipo",
   0,
   0
  ],
  [
   "Boleto",
   "FK_Boleto_ReservaPasajero",
   0,
   0
  ],
  [
   "CheckIn",
   "FK_CheckIn_Asiento",
   0,
   0
  ],
  [
   "CheckIn",
   "FK_CheckIn_Estado",
   0,
   0
  ],
  [
   "CheckIn",
   "FK_CheckIn_ReservaPasajero",
   0,
   0
  ],
  [
   "Equipaje",
   "FK_Equipaje_CheckIn",
   0,
   0
  ],
  [
   "TarjetaEmbarque",
   "FK_Tarjeta_CheckIn",
   0,
   0
  ],
  [
   "CargoExcesoEquipaje",
   "FK_Cargo_Equipaje",
   0,
   0
  ],
  [
   "CargoExcesoEquipaje",
   "FK_Cargo_Medio",
   0,
   0
  ],
  [
   "EtiquetaEquipaje",
   "FK_Etiqueta_Equipaje",
   0,
   0
  ],
  [
   "Usuario",
   "FK_Usuario_Roles",
   0,
   0
  ],
  [
   "RolFamilia",
   "FK_RolFamilia_Familia",
   0,
   0
  ],
  [
   "RolFamilia",
   "FK_RolFamilia_Rol",
   0,
   0
  ],
  [
   "FamiliaIntegrada",
   "FK_FamiliaIntegrada_Hija",
   -26,
   0
  ],
  [
   "FamiliaIntegrada",
   "FK_FamiliaIntegrada_Padre",
   26,
   0
  ],
  [
   "RolPatente",
   "FK_RolPatente_Patente",
   0,
   0
  ],
  [
   "RolPatente",
   "FK_RolPatente_Rol",
   0,
   0
  ],
  [
   "FamiliaPatente",
   "FK_FamiliaPatente_Familia",
   0,
   0
  ],
  [
   "FamiliaPatente",
   "FK_FamiliaPatente_Patente",
   0,
   0
  ],
  [
   "EVENTOS",
   "FK_Eventos_Modulo",
   0,
   0
  ],
  [
   "EVENTOS",
   "FK_Eventos_TipoEvento",
   0,
   0
  ],
  [
   "EVENTOS",
   "FK_Eventos_Usuario",
   0,
   0
  ]
 ]
};

var L_BIT = {
 "a": {
  "nombre": "Administrador",
  "actor": "Administrador"
 },
 "frm": {
  "nombre": "FRMBitacoraVuelos_GV42",
  "clase": "FRMBitacoraVuelos_GV42",
  "est": "boundary"
 },
 "bll": {
  "nombre": "BLLVueloHistorial_GV42",
  "clase": "BLLVueloHistorial_GV42"
 },
 "bVu": {
  "nombre": "BLLVuelo_GV42",
  "clase": "BLLVuelo_GV42"
 },
 "util": {
  "nombre": "BLLNegocioUtil_GV42",
  "clase": "BLLNegocioUtil_GV42"
 },
 "bBit": {
  "nombre": "BLLBitacora_GV42",
  "clase": "BLLBitacora_GV42"
 },
 "dH": {
  "nombre": "DALVueloHistorial_GV42",
  "clase": "DALVueloHistorial_GV42"
 },
 "dVu": {
  "nombre": "DALVuelo_GV42",
  "clase": "DALVuelo_GV42"
 },
 "dBit": {
  "nombre": "DALBitacora_GV42",
  "clase": "DALBitacora_GV42"
 },
 "acc": {
  "nombre": "Acceso",
  "clase": "Acceso"
 }
};
var SECUENCIAS = [
 {
  "mensajes": [
   [
    "a",
    "frm",
    "ingresarFiltros(codigoVuelo, nombre, fechaIni, fechaFin)",
    "",
    130
   ],
   [
    "a",
    "frm",
    "btnAplicar_Click()",
    "",
    162
   ],
   [
    "frm",
    "bll",
    "Consultar(codigoVuelo, nombre, fechaIni, fechaFin, soloConCambios)",
    "",
    194
   ],
   [
    "bll",
    "util",
    "ExigirPatente(\"Vuelos.Bitacora\", mensaje)",
    "",
    226
   ],
   [
    "bll",
    "frm",
    "NegocioException_GV42(mensaje)",
    "r",
    292
   ],
   [
    "frm",
    "a",
    "mensaje de error",
    "r",
    324
   ],
   [
    "bll",
    "dH",
    "Listar(codigoVuelo, nombre, fechaIni, fechaFin, soloConCambios)",
    "",
    386
   ],
   [
    "dH",
    "acc",
    "leer(\"SELECT ... FROM Vuelo_C ...\", parametros)",
    "",
    418
   ],
   [
    "acc",
    "dH",
    "DataTable",
    "r",
    450
   ],
   [
    "dH",
    "bll",
    "List<VueloCambio_GV42>",
    "r",
    482
   ],
   [
    "bll",
    "frm",
    "List<VueloCambio_GV42>",
    "r",
    514
   ],
   [
    "frm",
    "a",
    "cambios: Cod. vuelo, fecha, hora, nombre, aerolínea, salida, llegada, puerta, $/kg, baja y Act",
    "r",
    546
   ]
  ],
  "fragmentos": [
   {
    "subtipo": 0,
    "nombre": "Validación de la consulta",
    "l": 18,
    "r": 1132,
    "t": 258,
    "b": 584,
    "ops": [
     {
      "guarda": "[sin permiso o fechaIni > fechaFin]",
      "size": 98
     },
     {
      "guarda": "[consulta válida]",
      "size": 228
     }
    ]
   }
  ],
  "alto": 656,
  "vidas": {},
  "lifelines": [
   "a",
   "frm",
   "bll",
   "util",
   "dH",
   "acc"
  ],
  "nombre": "Secuencia - Consultar bitácora de cambios"
 },
 {
  "mensajes": [
   [
    "a",
    "frm",
    "seleccionarRegistro(cambio)",
    "",
    130
   ],
   [
    "a",
    "frm",
    "btnActivar_Click()",
    "",
    162
   ],
   [
    "frm",
    "bll",
    "ActivarVersion(cambio : VueloCambio_GV42)",
    "",
    194
   ],
   [
    "bll",
    "util",
    "ExigirPatente(\"Vuelos.Activar\", mensaje)",
    "",
    226
   ],
   [
    "bll",
    "bVu",
    "BuscarActual(numeroVuelo : int)",
    "",
    258
   ],
   [
    "bVu",
    "dVu",
    "ListarTodos()",
    "",
    290
   ],
   [
    "dVu",
    "acc",
    "leer(query, parametros)",
    "",
    322
   ],
   [
    "acc",
    "dVu",
    "DataTable",
    "r",
    354
   ],
   [
    "dVu",
    "bVu",
    "List<Vuelo_GV42>",
    "r",
    386
   ],
   [
    "bVu",
    "bll",
    "Vuelo_GV42",
    "r",
    418
   ],
   [
    "bll",
    "bVu",
    "ContarReservasVigentes(numeroVuelo : int)",
    "",
    450
   ],
   [
    "bVu",
    "dVu",
    "ContarReservasVigentes(numeroVuelo)",
    "",
    482
   ],
   [
    "dVu",
    "acc",
    "leerEscalar(query, parametros)",
    "",
    514
   ],
   [
    "acc",
    "dVu",
    "valor : object",
    "r",
    546
   ],
   [
    "dVu",
    "bVu",
    "cantidad : int",
    "r",
    578
   ],
   [
    "bVu",
    "bll",
    "cantidad : int",
    "r",
    610
   ],
   [
    "bll",
    "frm",
    "NegocioException_GV42(mensaje)",
    "r",
    676
   ],
   [
    "frm",
    "a",
    "mensaje de error",
    "r",
    708
   ],
   [
    "bll",
    "dH",
    "ActivarVersion(codigoCambio : int)",
    "",
    770
   ],
   [
    "dH",
    "acc",
    "EjecutarEnTransaccion(trabajo)  [UPDATE Vuelo_C SET Act = 1 al elegido y 0 al anterior; el trigger actualiza Vuelo]",
    "",
    802
   ],
   [
    "acc",
    "dH",
    "codigo : string",
    "r",
    834
   ],
   [
    "dH",
    "bll",
    "codigo : string",
    "r",
    866
   ],
   [
    "bll",
    "util",
    "Auditar(modulo, \"Version de vuelo activada\", detalle, criticidad)",
    "",
    898
   ],
   [
    "util",
    "bBit",
    "RegistrarEvento(login, modulo, tipoEvento, detalle, criticidad)",
    "",
    930
   ],
   [
    "bBit",
    "dBit",
    "Guardar(registro : Bitacora_GV42)",
    "",
    962
   ],
   [
    "dBit",
    "acc",
    "escribir(\"INSERT INTO EVENTOS ...\", parametros)",
    "",
    994
   ],
   [
    "bll",
    "frm",
    "void",
    "r",
    1026
   ],
   [
    "frm",
    "a",
    "\"Registro activado\": la grilla muestra ese registro con Act = 1",
    "r",
    1058
   ]
  ],
  "fragmentos": [
   {
    "subtipo": 0,
    "nombre": "Validación de la activación",
    "l": 18,
    "r": 1892,
    "t": 642,
    "b": 1096,
    "ops": [
     {
      "guarda": "[sin permiso, registro ya activo o cambia la aerolínea / ruta con reservas vigentes]",
      "size": 98
     },
     {
      "guarda": "[se puede activar]",
      "size": 356
     }
    ]
   }
  ],
  "alto": 1168,
  "vidas": {},
  "lifelines": [
   "a",
   "frm",
   "bll",
   "bVu",
   "util",
   "bBit",
   "dH",
   "dVu",
   "dBit",
   "acc"
  ],
  "nombre": "Secuencia - Activar registro histórico"
 }
];
var BIT_DC = {
 "nombre": "Clases - Bitácora de cambios",
 "modo": "todo",
 "objs": [
  {
   "n": "Acceso",
   "l": 50,
   "t": 608,
   "w": 620,
   "h": 312
  },
  {
   "n": "BLLBitacora_GV42",
   "l": 839,
   "t": 84,
   "w": 620,
   "h": 200
  },
  {
   "n": "BLLNegocioUtil_GV42",
   "l": 1152,
   "t": 349,
   "w": 546,
   "h": 242
  },
  {
   "n": "BLLVueloHistorial_GV42",
   "l": 765,
   "t": 660,
   "w": 620,
   "h": 144
  },
  {
   "n": "BLLVuelo_GV42",
   "l": 1076,
   "t": 877,
   "w": 620,
   "h": 410
  },
  {
   "n": "DALBitacora_GV42",
   "l": 121,
   "t": 60,
   "w": 620,
   "h": 270
  },
  {
   "n": "DALVueloHistorial_GV42",
   "l": 444,
   "t": 390,
   "w": 620,
   "h": 158
  },
  {
   "n": "DALVuelo_GV42",
   "l": 366,
   "t": 989,
   "w": 620,
   "h": 298
  },
  {
   "n": "FRMBitacoraVuelos_GV42",
   "l": 829,
   "t": 870,
   "w": 186,
   "h": 44
  },
  {
   "n": "Aeropuerto_GV42",
   "l": 2072,
   "t": 60,
   "w": 170,
   "h": 158
  },
  {
   "n": "VueloCambio_GV42",
   "l": 1808,
   "t": 184,
   "w": 186,
   "h": 248
  },
  {
   "n": "Vuelo_GV42",
   "l": 2078,
   "t": 308,
   "w": 186,
   "h": 206
  }
 ],
 "marcos": [],
 "enlaces": [],
 "ocultar": [
  [
   "DALVuelo_GV42",
   "Aeropuerto_GV42",
   "Dependency"
  ],
  [
   "DALVuelo_GV42",
   "Vuelo_GV42",
   "Dependency"
  ],
  [
   "DALVueloHistorial_GV42",
   "VueloCambio_GV42",
   "Dependency"
  ],
  [
   "BLLVuelo_GV42",
   "Aeropuerto_GV42",
   "Dependency"
  ],
  [
   "BLLVuelo_GV42",
   "Vuelo_GV42",
   "Dependency"
  ],
  [
   "BLLVueloHistorial_GV42",
   "VueloCambio_GV42",
   "Dependency"
  ],
  [
   "BLLVueloHistorial_GV42",
   "Vuelo_GV42",
   "Dependency"
  ],
  [
   "FRMBitacoraVuelos_GV42",
   "VueloCambio_GV42",
   "Dependency"
  ]
 ],
 "visibles": [
  [
   "Vuelo_GV42",
   "Aeropuerto_GV42",
   "Association"
  ],
  [
   "VueloCambio_GV42",
   "Vuelo_GV42",
   "Association"
  ],
  [
   "DALBitacora_GV42",
   "Acceso",
   "Dependency"
  ],
  [
   "DALVuelo_GV42",
   "Acceso",
   "Dependency"
  ],
  [
   "DALVueloHistorial_GV42",
   "Acceso",
   "Dependency"
  ],
  [
   "BLLBitacora_GV42",
   "DALBitacora_GV42",
   "Dependency"
  ],
  [
   "BLLNegocioUtil_GV42",
   "BLLBitacora_GV42",
   "Dependency"
  ],
  [
   "BLLVuelo_GV42",
   "BLLNegocioUtil_GV42",
   "Dependency"
  ],
  [
   "BLLVuelo_GV42",
   "DALVuelo_GV42",
   "Dependency"
  ],
  [
   "BLLVueloHistorial_GV42",
   "BLLNegocioUtil_GV42",
   "Dependency"
  ],
  [
   "BLLVueloHistorial_GV42",
   "BLLVuelo_GV42",
   "Dependency"
  ],
  [
   "BLLVueloHistorial_GV42",
   "DALVueloHistorial_GV42",
   "Dependency"
  ],
  [
   "FRMBitacoraVuelos_GV42",
   "BLLVueloHistorial_GV42",
   "Dependency"
  ]
 ],
 "notas": []
};
var BIT_TABLAS = [
 "Vuelo",
 "Vuelo_C",
 "Aeropuerto"
];
var BIT_DCU = {
 "actor": "Administrador",
 "pos": {
  "Consultar bitácora de cambios": [
   330,
   120
  ],
  "Activar registro histórico": [
   330,
   300
  ]
 },
 "principal": [
  "Consultar bitácora de cambios"
 ],
 "rel": [
  [
   "Activar registro histórico",
   "Consultar bitácora de cambios",
   "extend"
  ]
 ]
};
var ACTIVIDADES = [
 {
  "nombre": "Actividad - Registro de cambios de vuelos (trigger)",
  "carriles": [
   [
    "Administrador",
    20,
    300,
    [
     0,
     230,
     0
    ]
   ],
   [
    "Tabla Vuelo",
    320,
    320,
    [
     192,
     255,
     255
    ]
   ],
   [
    "Trigger TR_Vuelo_Historial",
    640,
    360,
    [
     255,
     255,
     200
    ]
   ],
   [
    "Tabla Vuelo_C",
    1000,
    300,
    [
     230,
     220,
     255
    ]
   ]
  ],
  "nodos": [
   [
    "ini",
    "ini",
    "Inicio",
    170,
    70
   ],
   [
    "op",
    "acc",
    "Añadir, modificar o dar de baja un vuelo",
    170,
    120
   ],
   [
    "dTipo",
    "dec",
    "¿Operación?",
    480,
    203
   ],
   [
    "ins",
    "acc",
    "INSERT en Vuelo (alta)",
    395,
    290
   ],
   [
    "upd",
    "acc",
    "UPDATE en Vuelo (modificación o baja lógica)",
    565,
    290
   ],
   [
    "mer",
    "mer",
    "",
    480,
    383
   ],
   [
    "dsVuelo",
    "ds",
    "Vuelo",
    480,
    450
   ],
   [
    "disp",
    "acc",
    "Se dispara AFTER INSERT, UPDATE",
    820,
    450
   ],
   [
    "dCambio",
    "dec",
    "¿Cambiaron los datos del vuelo?",
    820,
    543
   ],
   [
    "finSin",
    "fin",
    "Sin registro nuevo",
    950,
    550
   ],
   [
    "desact",
    "acc",
    "Poner Act = 0 en el registro activo del vuelo",
    820,
    630
   ],
   [
    "nuevo",
    "acc",
    "Insertar registro con fecha, hora, datos del vuelo y Act = 1",
    820,
    720
   ],
   [
    "dsC",
    "ds",
    "Vuelo_C",
    1150,
    720
   ],
   [
    "fin",
    "fin",
    "Fin",
    1150,
    810
   ]
  ],
  "flujos": [
   [
    "ini",
    "op",
    "cf",
    ""
   ],
   [
    "op",
    "dTipo",
    "cf",
    ""
   ],
   [
    "dTipo",
    "ins",
    "cf",
    "alta"
   ],
   [
    "dTipo",
    "upd",
    "cf",
    "modificación / baja"
   ],
   [
    "ins",
    "mer",
    "cf",
    ""
   ],
   [
    "upd",
    "mer",
    "cf",
    ""
   ],
   [
    "mer",
    "dsVuelo",
    "cf",
    ""
   ],
   [
    "dsVuelo",
    "disp",
    "of",
    ""
   ],
   [
    "disp",
    "dCambio",
    "cf",
    ""
   ],
   [
    "dCambio",
    "finSin",
    "cf",
    "no"
   ],
   [
    "dCambio",
    "desact",
    "cf",
    "sí"
   ],
   [
    "desact",
    "dsC",
    "dep",
    ""
   ],
   [
    "desact",
    "nuevo",
    "cf",
    ""
   ],
   [
    "nuevo",
    "dsC",
    "of",
    ""
   ],
   [
    "dsC",
    "fin",
    "cf",
    ""
   ]
  ],
  "alto": 880
 },
 {
  "nombre": "Actividad - Activar un registro histórico (trigger)",
  "carriles": [
   [
    "Administrador",
    20,
    300,
    [
     0,
     230,
     0
    ]
   ],
   [
    "Tabla Vuelo_C",
    320,
    320,
    [
     230,
     220,
     255
    ]
   ],
   [
    "Trigger TR_Vuelo_C_Activar",
    640,
    360,
    [
     255,
     255,
     200
    ]
   ],
   [
    "Tabla Vuelo",
    1000,
    300,
    [
     192,
     255,
     255
    ]
   ]
  ],
  "nodos": [
   [
    "ini",
    "ini",
    "Inicio",
    170,
    70
   ],
   [
    "sel",
    "acc",
    "Elegir un registro y presionar ACTIVAR",
    170,
    120
   ],
   [
    "upd",
    "acc",
    "UPDATE Vuelo_C: Act = 1 al elegido y 0 al que estaba activo",
    480,
    120
   ],
   [
    "dsC",
    "ds",
    "Vuelo_C",
    480,
    210
   ],
   [
    "disp",
    "acc",
    "Se dispara AFTER UPDATE",
    820,
    210
   ],
   [
    "dPaso",
    "dec",
    "¿Algún registro pasó de Act = 0 a Act = 1?",
    820,
    303
   ],
   [
    "finSin",
    "fin",
    "Sin cambios en Vuelo",
    950,
    310
   ],
   [
    "copiar",
    "acc",
    "Copiar los datos de ese registro a Vuelo",
    820,
    390
   ],
   [
    "dsV",
    "ds",
    "Vuelo",
    1150,
    390
   ],
   [
    "fin",
    "fin",
    "Fin",
    1150,
    480
   ]
  ],
  "flujos": [
   [
    "ini",
    "sel",
    "cf",
    ""
   ],
   [
    "sel",
    "upd",
    "cf",
    ""
   ],
   [
    "upd",
    "dsC",
    "cf",
    ""
   ],
   [
    "dsC",
    "disp",
    "of",
    ""
   ],
   [
    "disp",
    "dPaso",
    "cf",
    ""
   ],
   [
    "dPaso",
    "finSin",
    "cf",
    "no"
   ],
   [
    "dPaso",
    "copiar",
    "cf",
    "sí"
   ],
   [
    "copiar",
    "dsV",
    "of",
    ""
   ],
   [
    "dsV",
    "fin",
    "cf",
    ""
   ]
  ],
  "alto": 550
 }
];
var MODELO = {
 "clases": [
  {
   "nombre": "Bitacora_GV42",
   "capa": "Servicios",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "Login",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Modulo",
     "t": "Modulo_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "TipoEvento",
     "t": "TipoEvento_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "Detalle",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Criticidad",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaHora",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "ModuloNombre",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "TipoEventoNombre",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Evento",
     "t": "string",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "Bitacora_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "Bitacora_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": [
      [
       "login",
       "string"
      ],
      [
       "modulo",
       "string"
      ],
      [
       "tipoEvento",
       "string"
      ],
      [
       "detalle",
       "string"
      ],
      [
       "criticidad",
       "string"
      ],
      [
       "fechaHora",
       "DateTime"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "Encriptador_GV42",
   "capa": "Servicios",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_instancia",
     "t": "Encriptador_GV42",
     "v": "Private",
     "s": true
    },
    {
     "n": "PREFIJO",
     "t": "string",
     "v": "Private",
     "s": true
    },
    {
     "n": "ITERACIONES",
     "t": "int",
     "v": "Private",
     "s": true
    },
    {
     "n": "BYTES_SAL",
     "t": "int",
     "v": "Private",
     "s": true
    },
    {
     "n": "BYTES_HASH",
     "t": "int",
     "v": "Private",
     "s": true
    },
    {
     "n": "Instancia",
     "t": "Encriptador_GV42",
     "v": "Public",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "Encriptador_GV42",
     "t": "",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "EncriptarContrasena",
     "t": "string",
     "v": "Public",
     "s": false,
     "p": [
      [
       "contrasenaPlana",
       "string"
      ]
     ]
    },
    {
     "n": "VerificarContrasena",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": [
      [
       "contrasenaPlana",
       "string"
      ],
      [
       "hashGuardado",
       "string"
      ]
     ]
    },
    {
     "n": "EsHashLegado",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": [
      [
       "hashGuardado",
       "string"
      ]
     ]
    },
    {
     "n": "Derivar",
     "t": "byte[]",
     "v": "Private",
     "s": true,
     "p": [
      [
       "contrasena",
       "string"
      ],
      [
       "sal",
       "byte[]"
      ],
      [
       "iteraciones",
       "int"
      ],
      [
       "largo",
       "int"
      ]
     ]
    },
    {
     "n": "Sha256Hex",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "texto",
       "string"
      ]
     ]
    },
    {
     "n": "IgualesTiempoConstante",
     "t": "bool",
     "v": "Private",
     "s": true,
     "p": [
      [
       "a",
       "byte[]"
      ],
      [
       "b",
       "byte[]"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "EncriptadorReversible_GV42",
   "capa": "Servicios",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_instancia",
     "t": "EncriptadorReversible_GV42",
     "v": "Private",
     "s": true
    },
    {
     "n": "CLAVE",
     "t": "byte[]",
     "v": "Private",
     "s": true
    },
    {
     "n": "IV",
     "t": "byte[]",
     "v": "Private",
     "s": true
    },
    {
     "n": "Instancia",
     "t": "EncriptadorReversible_GV42",
     "v": "Public",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "EncriptadorReversible_GV42",
     "t": "",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "Encriptar",
     "t": "string",
     "v": "Public",
     "s": false,
     "p": [
      [
       "textoPlano",
       "string"
      ]
     ]
    },
    {
     "n": "Desencriptar",
     "t": "string",
     "v": "Public",
     "s": false,
     "p": [
      [
       "textoCifrado",
       "string"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "Familia_GV42",
   "capa": "Servicios",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "CodigoPermiso",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "Nombre",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Hijos",
     "t": "List<IComponentePermiso_GV42>",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "Familia_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "Familia_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigo",
       "int"
      ],
      [
       "nombre",
       "string"
      ]
     ]
    },
    {
     "n": "ObtenerPatentes",
     "t": "IEnumerable<Patente_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ToString",
     "t": "string",
     "v": "Public",
     "s": false,
     "p": []
    }
   ]
  },
  {
   "nombre": "IComponentePermiso_GV42",
   "capa": "Servicios",
   "padre": null,
   "tipo": "Interface",
   "attrs": [
    {
     "n": "CodigoPermiso",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "Nombre",
     "t": "string",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "ObtenerPatentes",
     "t": "IEnumerable<Patente_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    }
   ]
  },
  {
   "nombre": "Patente_GV42",
   "capa": "Servicios",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "CodigoPermiso",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "Nombre",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "DataKey",
     "t": "string",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "Patente_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "Patente_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigo",
       "int"
      ],
      [
       "nombre",
       "string"
      ],
      [
       "dataKey",
       "string"
      ]
     ]
    },
    {
     "n": "ObtenerPatentes",
     "t": "IEnumerable<Patente_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ToString",
     "t": "string",
     "v": "Public",
     "s": false,
     "p": []
    }
   ]
  },
  {
   "nombre": "Rol_GV42",
   "capa": "Servicios",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_CodigoPermiso",
     "t": "int",
     "v": "Private",
     "s": false
    },
    {
     "n": "_Nombre",
     "t": "string",
     "v": "Private",
     "s": false
    },
    {
     "n": "CodigoPermiso",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "Nombre",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Hijos",
     "t": "List<IComponentePermiso_GV42>",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "Rol_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "Rol_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigo",
       "int"
      ],
      [
       "nombre",
       "string"
      ]
     ]
    },
    {
     "n": "ObtenerPatentes",
     "t": "IEnumerable<Patente_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "TienePermiso",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dataKey",
       "string"
      ]
     ]
    },
    {
     "n": "ToString",
     "t": "string",
     "v": "Public",
     "s": false,
     "p": []
    }
   ]
  },
  {
   "nombre": "ConfiguracionBD_GV42",
   "capa": "Servicios",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "NOMBRE_ARCHIVO",
     "t": "string",
     "v": "Private",
     "s": true
    },
    {
     "n": "CARPETA_APP",
     "t": "string",
     "v": "Private",
     "s": true
    },
    {
     "n": "CarpetaConfig",
     "t": "string",
     "v": "Private",
     "s": true
    },
    {
     "n": "RutaArchivo",
     "t": "string",
     "v": "Private",
     "s": true
    },
    {
     "n": "RutaArchivoLegacy",
     "t": "string",
     "v": "Private",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "LeerInstanciaGuardada",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": []
    },
    {
     "n": "GuardarInstancia",
     "t": "void",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ]
     ]
    },
    {
     "n": "ArmarConnectionString",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ],
      [
       "nombreBd",
       "string"
      ]
     ]
    },
    {
     "n": "ArmarConnectionStringMaster",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "DetectorInstancias_GV42",
   "capa": "Servicios",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "TIMEOUT_SEG_ENUMERATOR",
     "t": "int",
     "v": "Private",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "DetectarInstancias",
     "t": "List<string>",
     "v": "Public",
     "s": true,
     "p": []
    },
    {
     "n": "DetectarSqlServerLocalesConTimeout",
     "t": "List<string>",
     "v": "Private",
     "s": true,
     "p": [
      [
       "segundos",
       "int"
      ]
     ]
    },
    {
     "n": "DetectarLocalDB",
     "t": "List<string>",
     "v": "Private",
     "s": true,
     "p": []
    }
   ]
  },
  {
   "nombre": "IObservadorIdioma_GV42",
   "capa": "Servicios",
   "padre": null,
   "tipo": "Interface",
   "attrs": [],
   "metodos": [
    {
     "n": "ActualizarIdioma",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": []
    }
   ]
  },
  {
   "nombre": "IdiomaManager_GV42",
   "capa": "Servicios",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_instancia",
     "t": "IdiomaManager_GV42",
     "v": "Private",
     "s": true
    },
    {
     "n": "_observadores",
     "t": "List<IObservadorIdioma_GV42>",
     "v": "Private",
     "s": false
    },
    {
     "n": "_traducciones",
     "t": "Dictionary<string, string>",
     "v": "Private",
     "s": false
    },
    {
     "n": "_idiomaActual",
     "t": "string",
     "v": "Private",
     "s": false
    },
    {
     "n": "ES",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "EN",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "IDIOMA_POR_DEFECTO",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "Instancia",
     "t": "IdiomaManager_GV42",
     "v": "Public",
     "s": true
    },
    {
     "n": "IdiomaActual",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "EsIngles",
     "t": "bool",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "IdiomaManager_GV42",
     "t": "",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "Suscribir",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "observador",
       "IObservadorIdioma_GV42"
      ]
     ]
    },
    {
     "n": "Desuscribir",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "observador",
       "IObservadorIdioma_GV42"
      ]
     ]
    },
    {
     "n": "CambiarIdioma",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoIdioma",
       "string"
      ]
     ]
    },
    {
     "n": "Traducir",
     "t": "string",
     "v": "Public",
     "s": false,
     "p": [
      [
       "clave",
       "string"
      ]
     ]
    },
    {
     "n": "T",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "clave",
       "string"
      ]
     ]
    },
    {
     "n": "T",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "clave",
       "string"
      ],
      [
       "valores",
       "object[]"
      ]
     ]
    },
    {
     "n": "TConDefecto",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "clave",
       "string"
      ],
      [
       "porDefecto",
       "string"
      ]
     ]
    },
    {
     "n": "Existe",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": [
      [
       "clave",
       "string"
      ]
     ]
    },
    {
     "n": "Notificar",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "CargarDesdeArchivo",
     "t": "Dictionary<string, string>",
     "v": "Private",
     "s": false,
     "p": [
      [
       "codigoIdioma",
       "string"
      ]
     ]
    },
    {
     "n": "ParsearJsonPlano",
     "t": "Dictionary<string, string>",
     "v": "Private",
     "s": false,
     "p": [
      [
       "json",
       "string"
      ]
     ]
    },
    {
     "n": "Desescapar",
     "t": "string",
     "v": "Private",
     "s": false,
     "p": [
      [
       "s",
       "string"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "SessionManager_GV42",
   "capa": "Servicios",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_instancia",
     "t": "SessionManager_GV42",
     "v": "Private",
     "s": true
    },
    {
     "n": "_usuarioActual",
     "t": "Usuario_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "Instancia",
     "t": "SessionManager_GV42",
     "v": "Public",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "SessionManager_GV42",
     "t": "",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "IniciarSesion",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": [
      [
       "usuario",
       "Usuario_GV42"
      ]
     ]
    },
    {
     "n": "CerrarSesion",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "HaySesionActiva",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ObtenerUsuarioActual",
     "t": "Usuario_GV42",
     "v": "Public",
     "s": false,
     "p": []
    }
   ]
  },
  {
   "nombre": "Usuario_GV42",
   "capa": "Servicios",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_DNI",
     "t": "string",
     "v": "Private",
     "s": false
    },
    {
     "n": "_Apellido",
     "t": "string",
     "v": "Private",
     "s": false
    },
    {
     "n": "_Nombre",
     "t": "string",
     "v": "Private",
     "s": false
    },
    {
     "n": "_Login",
     "t": "string",
     "v": "Private",
     "s": false
    },
    {
     "n": "_Contrasena",
     "t": "string",
     "v": "Private",
     "s": false
    },
    {
     "n": "_Rol",
     "t": "Rol_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "_Email",
     "t": "string",
     "v": "Private",
     "s": false
    },
    {
     "n": "_Bloqueo",
     "t": "bool",
     "v": "Private",
     "s": false
    },
    {
     "n": "_Activo",
     "t": "bool",
     "v": "Private",
     "s": false
    },
    {
     "n": "DNI",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Apellido",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Nombre",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Login",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Contrasena",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Rol",
     "t": "Rol_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "RolNombre",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Email",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Bloqueo",
     "t": "bool",
     "v": "Public",
     "s": false
    },
    {
     "n": "Activo",
     "t": "bool",
     "v": "Public",
     "s": false
    },
    {
     "n": "IntentosFallidos",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "UltimoIntentoFallido",
     "t": "DateTime?",
     "v": "Public",
     "s": false
    },
    {
     "n": "DebeCambiarContrasena",
     "t": "bool",
     "v": "Public",
     "s": false
    },
    {
     "n": "Idioma",
     "t": "string",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "Usuario_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ],
      [
       "apellidos",
       "string"
      ],
      [
       "nombre",
       "string"
      ],
      [
       "login",
       "string"
      ],
      [
       "password",
       "string"
      ],
      [
       "rol",
       "Rol_GV42"
      ],
      [
       "email",
       "string"
      ]
     ]
    },
    {
     "n": "Usuario_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    }
   ]
  },
  {
   "nombre": "Validaciones_GV42",
   "capa": "Servicios",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "REGEX_DNI",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "REGEX_EMAIL",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "REGEX_SOLO_LETRAS",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "REGEX_CONTRASENA",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "REGEX_LOGIN",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "REGEX_TELEFONO",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "MAX_NOMBRE",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "MAX_EMAIL",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "MAX_LOGIN",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "MAX_CONTRASENA",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "MAX_TELEFONO",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "MAX_NOMBRE_ROL",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "MAX_NOMBRE_FAMILIA",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "MAX_NUMERO_TRANSACCION",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "REGEX_NOMBRE_PERFIL",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "REGEX_TX_TARJETA",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "REGEX_TX_TRANSFERENCIA",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "MIN_DIGITOS_TARJETA",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "MAX_DIGITOS_TARJETA",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "MAX_ANIOS_VENCIMIENTO",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "MENSAJE_DNI",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "MENSAJE_EMAIL",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "MENSAJE_NOMBRE",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "MENSAJE_APELLIDO",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "MENSAJE_CONTRASENA",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "MENSAJE_LOGIN",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "MENSAJE_NOMBRE_PERFIL",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "MENSAJE_TARJETA",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "MENSAJE_TELEFONO",
     "t": "string",
     "v": "Public",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "EsDniValido",
     "t": "bool",
     "v": "Public",
     "s": true,
     "p": [
      [
       "dni",
       "string"
      ]
     ]
    },
    {
     "n": "EsEmailValido",
     "t": "bool",
     "v": "Public",
     "s": true,
     "p": [
      [
       "email",
       "string"
      ]
     ]
    },
    {
     "n": "EsNombreValido",
     "t": "bool",
     "v": "Public",
     "s": true,
     "p": [
      [
       "nombre",
       "string"
      ]
     ]
    },
    {
     "n": "EsApellidoValido",
     "t": "bool",
     "v": "Public",
     "s": true,
     "p": [
      [
       "apellido",
       "string"
      ]
     ]
    },
    {
     "n": "EsContrasenaValida",
     "t": "bool",
     "v": "Public",
     "s": true,
     "p": [
      [
       "contrasena",
       "string"
      ]
     ]
    },
    {
     "n": "EsNombrePerfilValido",
     "t": "bool",
     "v": "Public",
     "s": true,
     "p": [
      [
       "nombre",
       "string"
      ],
      [
       "maximo",
       "int"
      ]
     ]
    },
    {
     "n": "EscaparLike",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "texto",
       "string"
      ]
     ]
    },
    {
     "n": "EsLoginValido",
     "t": "bool",
     "v": "Public",
     "s": true,
     "p": [
      [
       "login",
       "string"
      ]
     ]
    },
    {
     "n": "EsTelefonoValido",
     "t": "bool",
     "v": "Public",
     "s": true,
     "p": [
      [
       "telefono",
       "string"
      ]
     ]
    },
    {
     "n": "SoloDigitos",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "texto",
       "string"
      ]
     ]
    },
    {
     "n": "CumpleLuhn",
     "t": "bool",
     "v": "Public",
     "s": true,
     "p": [
      [
       "numero",
       "string"
      ]
     ]
    },
    {
     "n": "EsNumeroTarjetaValido",
     "t": "bool",
     "v": "Public",
     "s": true,
     "p": [
      [
       "numero",
       "string"
      ]
     ]
    },
    {
     "n": "MarcaTarjeta",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "numero",
       "string"
      ]
     ]
    },
    {
     "n": "EsAmex",
     "t": "bool",
     "v": "Public",
     "s": true,
     "p": [
      [
       "numero",
       "string"
      ]
     ]
    },
    {
     "n": "EsCodigoSeguridadValido",
     "t": "bool",
     "v": "Public",
     "s": true,
     "p": [
      [
       "codigo",
       "string"
      ],
      [
       "numeroTarjeta",
       "string"
      ]
     ]
    },
    {
     "n": "EsVencimientoValido",
     "t": "bool",
     "v": "Public",
     "s": true,
     "p": [
      [
       "mes",
       "int"
      ],
      [
       "anio",
       "int"
      ],
      [
       "hoy",
       "DateTime"
      ]
     ]
    },
    {
     "n": "EsTitularTarjetaValido",
     "t": "bool",
     "v": "Public",
     "s": true,
     "p": [
      [
       "titular",
       "string"
      ]
     ]
    },
    {
     "n": "EnmascararTarjeta",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "numero",
       "string"
      ]
     ]
    },
    {
     "n": "NormalizarEspacios",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "texto",
       "string"
      ]
     ]
    },
    {
     "n": "MismoTexto",
     "t": "bool",
     "v": "Public",
     "s": true,
     "p": [
      [
       "a",
       "string"
      ],
      [
       "b",
       "string"
      ]
     ]
    },
    {
     "n": "EsTextoDeLetras",
     "t": "bool",
     "v": "Private",
     "s": true,
     "p": [
      [
       "texto",
       "string"
      ]
     ]
    },
    {
     "n": "Canonico",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "texto",
       "string"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "CalculadorIntegridad_GV42",
   "capa": "Servicios",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": [
    {
     "n": "CalcularDVH",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "campos",
       "object[]"
      ]
     ]
    },
    {
     "n": "CalcularDVV",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "dvhs",
       "IEnumerable<string>"
      ]
     ]
    },
    {
     "n": "Sha256Hex",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "input",
       "string"
      ]
     ]
    },
    {
     "n": "NormalizarCampo",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "v",
       "object"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "Entregas_GV42",
   "capa": "Servicios",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "ENTREGA_3_ACTIVA",
     "t": "bool",
     "v": "Public",
     "s": true
    }
   ],
   "metodos": []
  },
  {
   "nombre": "EstiloXmlClientes_GV42",
   "capa": "Servicios",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "NOMBRE_ARCHIVO",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "CONTENIDO_CSS",
     "t": "string",
     "v": "Private",
     "s": true
    },
    {
     "n": "CSS",
     "t": "string",
     "v": "Public",
     "s": true
    }
   ],
   "metodos": []
  },
  {
   "nombre": "SerializadorXml_GV42",
   "capa": "Servicios",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": [
    {
     "n": "Serializar",
     "t": "void",
     "v": "Public",
     "s": true,
     "p": [
      [
       "objeto",
       "T"
      ],
      [
       "rutaArchivo",
       "string"
      ],
      [
       "hojaEstiloCss",
       "string"
      ]
     ]
    },
    {
     "n": "Deserializar",
     "t": "T",
     "v": "Public",
     "s": true,
     "p": [
      [
       "rutaArchivo",
       "string"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "AdicionalReserva_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "CodigoAdicionalReserva",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "TipoAdicional",
     "t": "TipoAdicional_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "Cantidad",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "CostoUnitario",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "Tramo",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "Subtotal",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "TipoNombre",
     "t": "string",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "Aeropuerto_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "CodigoAeropuerto",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "CodigoIata",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Nombre",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Ciudad",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Pais",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Descripcion",
     "t": "string",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "ToString",
     "t": "string",
     "v": "Public",
     "s": false,
     "p": []
    }
   ]
  },
  {
   "nombre": "AsientoDisponibilidad_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "Asiento",
     "t": "Asiento_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "Ocupado",
     "t": "bool",
     "v": "Public",
     "s": false
    },
    {
     "n": "Fila",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "Letra",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "NumeroAsiento",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Ubicacion",
     "t": "string",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "AsientoPasajero_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "DniPasajero",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Asiento",
     "t": "Asiento_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "EquipajeExtra",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "Tramo",
     "t": "int",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "AsientoPasajero_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "AsientoPasajero_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dniPasajero",
       "string"
      ],
      [
       "asiento",
       "Asiento_GV42"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "Asiento_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "CodigoAsiento",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "NumeroVuelo",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "NumeroAsiento",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Clase",
     "t": "ClaseVuelo_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "Ubicacion",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "EsPreferencial",
     "t": "bool",
     "v": "Public",
     "s": false
    },
    {
     "n": "ClaseTexto",
     "t": "string",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "ToString",
     "t": "string",
     "v": "Public",
     "s": false,
     "p": []
    }
   ]
  },
  {
   "nombre": "Boleto_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "CodigoBoleto",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "NumeroBoleto",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "NumeroReserva",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Pasajero",
     "t": "Pasajero_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "CodigoVuelo",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Tramo",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaEmision",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "PasajeroNombre",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "PasajeroDni",
     "t": "string",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "BoletoElectronico_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "NumeroBoleto",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "NumeroReserva",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaEmision",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "NumeroPasajero",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "TotalPasajeros",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "PasajeroNombre",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "PasajeroApellido",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "PasajeroDni",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Aerolinea",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "CodigoVuelo",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "OrigenIata",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "OrigenCiudad",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "OrigenAeropuerto",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "DestinoIata",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "DestinoCiudad",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "DestinoAeropuerto",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaHoraSalida",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaHoraLlegada",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "HoraEmbarque",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "CierreEmbarque",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "PuertaEmbarque",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Clase",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "TarifaNombre",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "TipoPasajero",
     "t": "TipoPasajero_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "Asistencia",
     "t": "AsistenciaEspecial_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "Asiento",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "UbicacionAsiento",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "FranquiciaEquipajeKg",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "TipoViaje",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Tramo",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "EsIdaYVuelta",
     "t": "bool",
     "v": "Public",
     "s": false
    },
    {
     "n": "TarifaPasajero",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "ImpuestosPasajero",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "TotalReserva",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "ServiciosAdicionales",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "FormaPago",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Estado",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "CodigoBarras",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "ClaseValor",
     "t": "ClaseVuelo_GV42?",
     "v": "Public",
     "s": false
    },
    {
     "n": "TipoViajeValor",
     "t": "TipoViaje_GV42?",
     "v": "Public",
     "s": false
    },
    {
     "n": "MedioPagoValor",
     "t": "MedioPago_GV42?",
     "v": "Public",
     "s": false
    },
    {
     "n": "EstadoValor",
     "t": "EstadoReserva_GV42?",
     "v": "Public",
     "s": false
    },
    {
     "n": "Adicionales",
     "t": "List<AdicionalReserva_GV42>",
     "v": "Public",
     "s": false
    },
    {
     "n": "PasajeroParaBoleto",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Duracion",
     "t": "TimeSpan",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "CargoExcesoEquipaje_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "FranquiciaKg",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "UnidadesExtraUsadas",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "KilosExceso",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "CostoPorKilo",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "ImporteCargo",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "MedioPago",
     "t": "MedioPago_GV42?",
     "v": "Public",
     "s": false
    },
    {
     "n": "NumeroTransaccion",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaHoraCobro",
     "t": "DateTime?",
     "v": "Public",
     "s": false
    },
    {
     "n": "TieneExceso",
     "t": "bool",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "CheckIn_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "CodigoCheckIn",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "CodigoReserva",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "NumeroReserva",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Tramo",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "ReservaConVuelta",
     "t": "bool",
     "v": "Public",
     "s": false
    },
    {
     "n": "Pasajero",
     "t": "Pasajero_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "VueloClase",
     "t": "VueloClase_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "EstadoReserva",
     "t": "EstadoReserva_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "Estado",
     "t": "EstadoCheckIn_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaHoraCheckIn",
     "t": "DateTime?",
     "v": "Public",
     "s": false
    },
    {
     "n": "LoginEncargado",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Canal",
     "t": "CanalVenta_GV42?",
     "v": "Public",
     "s": false
    },
    {
     "n": "DniTitular",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "TipoViaje",
     "t": "TipoViaje_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "EquipajeExtraComprado",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "ValijasIncluidas",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "TarifaNombre",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "TarifaIncluyePreferencial",
     "t": "bool",
     "v": "Public",
     "s": false
    },
    {
     "n": "TarifaAsientoPago",
     "t": "bool",
     "v": "Public",
     "s": false
    },
    {
     "n": "TipoPasajero",
     "t": "TipoPasajero_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "Asistencia",
     "t": "AsistenciaEspecial_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "Infantes",
     "t": "List<string>",
     "v": "Public",
     "s": false
    },
    {
     "n": "EsOnline",
     "t": "bool",
     "v": "Public",
     "s": false
    },
    {
     "n": "CanalTexto",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "ServiciosAdicionales",
     "t": "List<AdicionalReserva_GV42>",
     "v": "Public",
     "s": false
    },
    {
     "n": "PagoSeleccionAsiento",
     "t": "bool",
     "v": "Public",
     "s": false
    },
    {
     "n": "PuedeElegirAsiento",
     "t": "bool",
     "v": "Public",
     "s": false
    },
    {
     "n": "Asiento",
     "t": "Asiento_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "Equipaje",
     "t": "Equipaje_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "TarjetaEmbarque",
     "t": "TarjetaEmbarque_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "Vuelo",
     "t": "Vuelo_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "EstadoReservaTexto",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "EstadoTexto",
     "t": "string",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "CriterioBusquedaVuelo_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "CodigoOrigen",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "CodigoDestino",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaSalida",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "CantidadPasajeros",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "TipoViaje",
     "t": "TipoViaje_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaRegreso",
     "t": "DateTime?",
     "v": "Public",
     "s": false
    },
    {
     "n": "Clase",
     "t": "ClaseVuelo_GV42?",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "DatosTarjeta_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "Numero",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Titular",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "MesVencimiento",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "AnioVencimiento",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "CodigoSeguridad",
     "t": "string",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "ClaseVuelo_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Enumeration",
   "attrs": [
    {
     "n": "Economica",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Ejecutiva",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Primera",
     "t": "",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "TipoViaje_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Enumeration",
   "attrs": [
    {
     "n": "Ida",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "IdaYVuelta",
     "t": "",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "EstadoReserva_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Enumeration",
   "attrs": [
    {
     "n": "PendienteDePago",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Confirmada",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Cancelada",
     "t": "",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "MedioPago_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Enumeration",
   "attrs": [
    {
     "n": "TarjetaDebito",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "TarjetaCredito",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Transferencia",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Efectivo",
     "t": "",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "EstadoCheckIn_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Enumeration",
   "attrs": [
    {
     "n": "Pendiente",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Realizado",
     "t": "",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "CanalVenta_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Enumeration",
   "attrs": [
    {
     "n": "Presencial",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Autogestion",
     "t": "",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "TipoPasajero_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Enumeration",
   "attrs": [
    {
     "n": "Adulto",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Nino",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Infante",
     "t": "",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "AsistenciaEspecial_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Enumeration",
   "attrs": [
    {
     "n": "Ninguna",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "SillaDeRuedas",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Visual",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Auditiva",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "MovilidadReducida",
     "t": "",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "EstadoReembolso_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Enumeration",
   "attrs": [
    {
     "n": "Pendiente",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Procesado",
     "t": "",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "Textos_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "Traductor",
     "t": "Func<string, string, string>",
     "v": "Public",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "Texto",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "v",
       "ClaseVuelo_GV42"
      ]
     ]
    },
    {
     "n": "Texto",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "v",
       "TipoViaje_GV42"
      ]
     ]
    },
    {
     "n": "Texto",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "v",
       "EstadoReserva_GV42"
      ]
     ]
    },
    {
     "n": "Texto",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "v",
       "MedioPago_GV42"
      ]
     ]
    },
    {
     "n": "Texto",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "v",
       "EstadoCheckIn_GV42"
      ]
     ]
    },
    {
     "n": "Texto",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "v",
       "EstadoAsistencia_GV42"
      ]
     ]
    },
    {
     "n": "Texto",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "v",
       "CanalVenta_GV42"
      ]
     ]
    },
    {
     "n": "Texto",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "v",
       "TipoPasajero_GV42"
      ]
     ]
    },
    {
     "n": "Texto",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "v",
       "AsistenciaEspecial_GV42"
      ]
     ]
    },
    {
     "n": "Texto",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "v",
       "EstadoReembolso_GV42"
      ]
     ]
    },
    {
     "n": "Tr",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "clave",
       "string"
      ],
      [
       "enEspanol",
       "string"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "Equipaje_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "CodigoEquipaje",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "CodigoCheckIn",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "CantidadBultos",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "PesoTotalKg",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "FranquiciaKg",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "UnidadesExtra",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "Etiquetas",
     "t": "List<string>",
     "v": "Public",
     "s": false
    },
    {
     "n": "PesosKg",
     "t": "List<decimal>",
     "v": "Public",
     "s": false
    },
    {
     "n": "CargoExceso",
     "t": "CargoExcesoEquipaje_GV42",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "FranquiciaEquipaje_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "BultosIncluidos",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "KgPorBultoIncluido",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "KgPorUnidadExtra",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "UnidadesExtraDisponibles",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "UnidadesExtraMaxPasajero",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "PesoMaximoPorBulto",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "CostoKiloExceso",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "UnidadesExtraAplicables",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "BultosPermitidos",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "FranquiciaClaseKg",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "FranquiciaMaximaKg",
     "t": "decimal",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "NegocioException_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": [
    {
     "n": "NegocioException_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": [
      [
       "mensaje",
       "string"
      ]
     ]
    },
    {
     "n": "NegocioException_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": [
      [
       "mensaje",
       "string"
      ],
      [
       "inner",
       "Exception"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "Pago_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "CodigoPago",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "CodigoReserva",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "NumeroReserva",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "ImporteTotalAbonado",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "MedioPago",
     "t": "MedioPago_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "NumeroTransaccion",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaHoraPago",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "LoginVendedor",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "MedioPagoTexto",
     "t": "string",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "Cliente_GV42",
   "capa": "BE",
   "padre": "Persona_GV42",
   "tipo": "Class",
   "attrs": [],
   "metodos": [
    {
     "n": "Cliente_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "Cliente_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ],
      [
       "nombre",
       "string"
      ],
      [
       "apellido",
       "string"
      ],
      [
       "email",
       "string"
      ],
      [
       "telefono",
       "string"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "ClienteXml_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "DNI",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Apellido",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Nombre",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Email",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Telefono",
     "t": "string",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "ClienteXml_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "DesdeCliente",
     "t": "ClienteXml_GV42",
     "v": "Public",
     "s": true,
     "p": [
      [
       "c",
       "Cliente_GV42"
      ]
     ]
    },
    {
     "n": "ACliente",
     "t": "Cliente_GV42",
     "v": "Public",
     "s": false,
     "p": []
    }
   ]
  },
  {
   "nombre": "ListaClientesXml_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "FechaGeneracion",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "GeneradoPor",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Cantidad",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "Clientes",
     "t": "List<ClienteXml_GV42>",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "Pasajero_GV42",
   "capa": "BE",
   "padre": "Persona_GV42",
   "tipo": "Class",
   "attrs": [
    {
     "n": "EDAD_MINIMA_ADULTO",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "EDAD_MINIMA_NINO",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "FechaNacimiento",
     "t": "DateTime?",
     "v": "Public",
     "s": false
    },
    {
     "n": "Tipo",
     "t": "TipoPasajero_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "Asistencia",
     "t": "AsistenciaEspecial_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "EsInfante",
     "t": "bool",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "Pasajero_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "Pasajero_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ],
      [
       "nombre",
       "string"
      ],
      [
       "apellido",
       "string"
      ],
      [
       "email",
       "string"
      ],
      [
       "telefono",
       "string"
      ]
     ]
    },
    {
     "n": "EdadEn",
     "t": "int?",
     "v": "Public",
     "s": false,
     "p": [
      [
       "fecha",
       "DateTime"
      ]
     ]
    },
    {
     "n": "TipoEn",
     "t": "TipoPasajero_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "fechaVuelo",
       "DateTime"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "Persona_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "DNI",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Nombre",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Apellido",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Email",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Telefono",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "NombreCompleto",
     "t": "string",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "Persona_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "Persona_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ],
      [
       "nombre",
       "string"
      ],
      [
       "apellido",
       "string"
      ],
      [
       "email",
       "string"
      ],
      [
       "telefono",
       "string"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "Reserva_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "TRAMO_IDA",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "TRAMO_VUELTA",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "CodigoReserva",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "NumeroReserva",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Cliente",
     "t": "Pasajero_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "VueloClase",
     "t": "VueloClase_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "VueloClaseVuelta",
     "t": "VueloClase_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "Tarifa",
     "t": "TarifaFamilia_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "TipoViaje",
     "t": "TipoViaje_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaRegreso",
     "t": "DateTime?",
     "v": "Public",
     "s": false
    },
    {
     "n": "Pasajeros",
     "t": "List<Pasajero_GV42>",
     "v": "Public",
     "s": false
    },
    {
     "n": "AsientosPorPasajero",
     "t": "List<AsientoPasajero_GV42>",
     "v": "Public",
     "s": false
    },
    {
     "n": "Adicionales",
     "t": "List<AdicionalReserva_GV42>",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaRealizacion",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "ImporteBase",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "SubtotalAdicionales",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "Impuestos",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "ImporteTotal",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "Estado",
     "t": "EstadoReserva_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "LoginVendedor",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "CanalVenta",
     "t": "CanalVenta_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaCancelacion",
     "t": "DateTime?",
     "v": "Public",
     "s": false
    },
    {
     "n": "MontoPenalidadCancelacion",
     "t": "decimal?",
     "v": "Public",
     "s": false
    },
    {
     "n": "Pago",
     "t": "Pago_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaVencimiento",
     "t": "DateTime?",
     "v": "Public",
     "s": false
    },
    {
     "n": "VencidaSinPago",
     "t": "bool",
     "v": "Public",
     "s": false
    },
    {
     "n": "Reembolso",
     "t": "Reembolso_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "CantidadAsientos",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "Vuelo",
     "t": "Vuelo_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "Clase",
     "t": "ClaseVuelo_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "CantidadPasajeros",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "EstadoTexto",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "TieneVuelta",
     "t": "bool",
     "v": "Public",
     "s": false
    },
    {
     "n": "CantidadTramos",
     "t": "int",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "VueloClaseDeTramo",
     "t": "VueloClase_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "tramo",
       "int"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "TarjetaEmbarque_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "CodigoTarjetaEmbarque",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "NumeroTarjeta",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "NumeroReserva",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "NombreApellido",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "DNI",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "CodigoVuelo",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Origen",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Destino",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaHoraSalida",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "NumeroAsiento",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Clase",
     "t": "ClaseVuelo_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "PuertaEmbarque",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "HoraLimiteEmbarque",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaHoraEmision",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "ClaseTexto",
     "t": "string",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "TipoAdicional_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "CODIGO_EQUIPAJE_EXTRA",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "CODIGO_ASIENTO_PREFERENCIAL",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "CODIGO_SELECCION_ASIENTO",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "CodigoTipoAdicional",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "Nombre",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "PrecioUnitario",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "MaxPorPasajero",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "Codigo",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "SeleccionManual",
     "t": "bool",
     "v": "Public",
     "s": false
    },
    {
     "n": "EsSeleccionAsiento",
     "t": "bool",
     "v": "Public",
     "s": false
    },
    {
     "n": "EsEquipajeExtra",
     "t": "bool",
     "v": "Public",
     "s": false
    },
    {
     "n": "EsAsientoPreferencial",
     "t": "bool",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "ToString",
     "t": "string",
     "v": "Public",
     "s": false,
     "p": []
    }
   ]
  },
  {
   "nombre": "EstadoAsistencia_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Enumeration",
   "attrs": [
    {
     "n": "AConfirmar",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Presente",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Ausente",
     "t": "",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "ReporteCheckIn_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "CodigoCheckIn",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "CodigoVuelo",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "OrigenIata",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "OrigenCiudad",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "DestinoIata",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "DestinoCiudad",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaHoraSalida",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "NumeroReserva",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "PasajeroNombre",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "PasajeroApellido",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "PasajeroDni",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Estado",
     "t": "EstadoCheckIn_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "Asistencia",
     "t": "EstadoAsistencia_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaHoraCheckIn",
     "t": "DateTime?",
     "v": "Public",
     "s": false
    },
    {
     "n": "NumeroAsiento",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Clase",
     "t": "ClaseVuelo_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "Ubicacion",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "NumeroTarjeta",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "PuertaEmbarque",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "HoraLimiteEmbarque",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "PasajeroNombreCompleto",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Origen",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Destino",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "ClaseTexto",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "EstadoTexto",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "AsistenciaTexto",
     "t": "string",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "FiltroReporteCheckIn_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "CodigoVuelo",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaDesde",
     "t": "DateTime?",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaHasta",
     "t": "DateTime?",
     "v": "Public",
     "s": false
    },
    {
     "n": "Estado",
     "t": "EstadoCheckIn_GV42?",
     "v": "Public",
     "s": false
    },
    {
     "n": "Asistencia",
     "t": "EstadoAsistencia_GV42?",
     "v": "Public",
     "s": false
    },
    {
     "n": "Pasajero",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "FiltraPorFecha",
     "t": "bool",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "Descripcion",
     "t": "string",
     "v": "Public",
     "s": false,
     "p": []
    }
   ]
  },
  {
   "nombre": "PeriodoMillas_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Enumeration",
   "attrs": [
    {
     "n": "Mensual",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Trimestral",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Anual",
     "t": "",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "CategoriaMillas_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Enumeration",
   "attrs": [
    {
     "n": "Bronce",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Plata",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Oro",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Diamante",
     "t": "",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "TendenciaMillas_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Enumeration",
   "attrs": [
    {
     "n": "SinActividad",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "EnBaja",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Estable",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "EnAumento",
     "t": "",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "RiesgoAbandono_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Enumeration",
   "attrs": [
    {
     "n": "Bajo",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Medio",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Alto",
     "t": "",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "FiltroReporteMillas_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "Periodo",
     "t": "PeriodoMillas_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "Categoria",
     "t": "CategoriaMillas_GV42?",
     "v": "Public",
     "s": false
    },
    {
     "n": "Pasajero",
     "t": "string",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "VueloVolado_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "Dni",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Nombre",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Apellido",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Email",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Telefono",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "CodigoVuelo",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Fecha",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "OrigenIata",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "OrigenCiudad",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "OrigenPais",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "DestinoIata",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "DestinoCiudad",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "DestinoPais",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Clase",
     "t": "ClaseVuelo_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "Millas",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "Ruta",
     "t": "string",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "ReporteMillas_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "NumeroCuenta",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Dni",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Nombre",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Apellido",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Email",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Telefono",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Categoria",
     "t": "CategoriaMillas_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaIngreso",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "NombreCompleto",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "MillasTotales",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "MillasUltimoVuelo",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "MillasPeriodo",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "MillasUltimos12Meses",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "CategoriaSiguiente",
     "t": "CategoriaMillas_GV42?",
     "v": "Public",
     "s": false
    },
    {
     "n": "MillasParaSiguiente",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "VuelosPeriodo",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "VuelosTotales",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "RutasFrecuentes",
     "t": "List<string>",
     "v": "Public",
     "s": false
    },
    {
     "n": "ClaseHabitual",
     "t": "ClaseVuelo_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "VuelosPorMes",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "UltimoVuelo",
     "t": "VueloVolado_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "ProyeccionMillas90Dias",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaEstimadaSiguiente",
     "t": "DateTime?",
     "v": "Public",
     "s": false
    },
    {
     "n": "MillasVencen30Dias",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "MillasVencen60Dias",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaBajaCategoria",
     "t": "DateTime?",
     "v": "Public",
     "s": false
    },
    {
     "n": "CategoriaTrasBaja",
     "t": "CategoriaMillas_GV42?",
     "v": "Public",
     "s": false
    },
    {
     "n": "Tendencia",
     "t": "TendenciaMillas_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "Viajes",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "IntervaloPromedioDias",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "DiasSinVolar",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaProximoViaje",
     "t": "DateTime?",
     "v": "Public",
     "s": false
    },
    {
     "n": "Riesgo",
     "t": "RiesgoAbandono_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "Recomendacion",
     "t": "string",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "MotivoOferta_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Enumeration",
   "attrs": [
    {
     "n": "Millas",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "CompraAsociada",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Categoria",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Habitual",
     "t": "",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "ContratacionAdicional_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "CodigoReserva",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "CodigoTipoAdicional",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "DniCliente",
     "t": "string",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "ReglaAsociacion_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "CodigoAntecedente",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "CodigoConsecuente",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "ReservasJuntos",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "Soporte",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "Confianza",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "Lift",
     "t": "decimal",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "OfertaCliente_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "Motivo",
     "t": "MotivoOferta_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "Tipo",
     "t": "TipoAdicional_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "Titulo",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Detalle",
     "t": "string",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "OfertasCliente_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "Dni",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Categoria",
     "t": "CategoriaMillas_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "MillasUltimos12Meses",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "Resumen",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Ofertas",
     "t": "List<OfertaCliente_GV42>",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "ReporteReserva_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "CodigoReserva",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "NumeroReserva",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaRealizacion",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "PasajeroNombre",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "PasajeroApellido",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "PasajeroDni",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Email",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Telefono",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "CodigoVuelo",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "OrigenIata",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "OrigenCiudad",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "DestinoIata",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "DestinoCiudad",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaHoraSalida",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaHoraLlegada",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "Clase",
     "t": "ClaseVuelo_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "CantidadPasajeros",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "Adicionales",
     "t": "List<AdicionalReporte_GV42>",
     "v": "Public",
     "s": false
    },
    {
     "n": "ImporteBase",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "SubtotalAdicionales",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "Impuestos",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "ImporteTotal",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "Estado",
     "t": "EstadoReserva_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "MontoPenalidad",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "PasajeroNombreCompleto",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Origen",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Destino",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "ClaseTexto",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "EstadoTexto",
     "t": "string",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "AdicionalReporte_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "Tipo",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Cantidad",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "CostoUnitario",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "Subtotal",
     "t": "decimal",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "FechaReporte_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Enumeration",
   "attrs": [
    {
     "n": "Realizacion",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Salida",
     "t": "",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "FiltroReporteReservas_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "TipoFecha",
     "t": "FechaReporte_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaDesde",
     "t": "DateTime?",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaHasta",
     "t": "DateTime?",
     "v": "Public",
     "s": false
    },
    {
     "n": "CodigoVuelo",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Clase",
     "t": "ClaseVuelo_GV42?",
     "v": "Public",
     "s": false
    },
    {
     "n": "Estado",
     "t": "EstadoReserva_GV42?",
     "v": "Public",
     "s": false
    },
    {
     "n": "Pasajero",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "FiltraPorFecha",
     "t": "bool",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "Descripcion",
     "t": "string",
     "v": "Public",
     "s": false,
     "p": []
    }
   ]
  },
  {
   "nombre": "VueloClase_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "Vuelo",
     "t": "Vuelo_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "Clase",
     "t": "ClaseVuelo_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "PrecioBase",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "CapacidadAsientos",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "AsientosReservados",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "FranquiciaEquipajeKg",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "AsientosDisponibles",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "CodigoVuelo",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "AerolineaNombre",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "OrigenDescripcion",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "DestinoDescripcion",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaHoraSalida",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaHoraLlegada",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "ClaseTexto",
     "t": "string",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "Vuelo_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "NumeroVuelo",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "CodigoVuelo",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Aerolinea",
     "t": "Aerolinea_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "Origen",
     "t": "Aeropuerto_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "Destino",
     "t": "Aeropuerto_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaHoraSalida",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaHoraLlegada",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "PuertaEmbarque",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "CostoKiloExceso",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "BorradoLogico",
     "t": "bool",
     "v": "Public",
     "s": false
    },
    {
     "n": "Descripcion",
     "t": "string",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "VueloCambio_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "CodigoVueloCambio",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "NumeroVuelo",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "CodigoVuelo",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Fecha",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "Hora",
     "t": "TimeSpan",
     "v": "Public",
     "s": false
    },
    {
     "n": "Nombre",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Aerolinea",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaHoraSalida",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaHoraLlegada",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "PuertaEmbarque",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "CostoKiloExceso",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "BorradoLogico",
     "t": "bool",
     "v": "Public",
     "s": false
    },
    {
     "n": "Act",
     "t": "bool",
     "v": "Public",
     "s": false
    },
    {
     "n": "Descripcion",
     "t": "string",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "TarifaFamilia_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "ASIENTO_PAGO",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "ASIENTO_COMUN_GRATIS",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "ASIENTO_CUALQUIERA_GRATIS",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "REEMBOLSO_NO",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "REEMBOLSO_CON_PENALIDAD",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "REEMBOLSO_TOTAL",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "CodigoTarifaFamilia",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "Codigo",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Nombre",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "PorcentajeRecargo",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "ValijasIncluidas",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "AsientoIncluido",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "PorcentajePenalidadCambio",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "TipoReembolso",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "ElegirAsientoEsPago",
     "t": "bool",
     "v": "Public",
     "s": false
    },
    {
     "n": "IncluyePreferencial",
     "t": "bool",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "PrecioPara",
     "t": "decimal",
     "v": "Public",
     "s": false,
     "p": [
      [
       "precioBase",
       "decimal"
      ]
     ]
    },
    {
     "n": "ToString",
     "t": "string",
     "v": "Public",
     "s": false,
     "p": []
    }
   ]
  },
  {
   "nombre": "Reembolso_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "CodigoReembolso",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "CodigoReserva",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "Importe",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "MedioPago",
     "t": "MedioPago_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "Estado",
     "t": "EstadoReembolso_GV42",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaSolicitud",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "FechaProceso",
     "t": "DateTime?",
     "v": "Public",
     "s": false
    },
    {
     "n": "LoginProceso",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "EstadoTexto",
     "t": "string",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "CotizacionCambio_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "TarifaActual",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "TarifaNueva",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "PorcentajePenalidad",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "Penalidad",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "DiferenciaTarifa",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "ImpuestosDiferencia",
     "t": "decimal",
     "v": "Public",
     "s": false
    },
    {
     "n": "Total",
     "t": "decimal",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "PrecioFecha_GV42",
   "capa": "BE",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "Fecha",
     "t": "DateTime",
     "v": "Public",
     "s": false
    },
    {
     "n": "HayVuelos",
     "t": "bool",
     "v": "Public",
     "s": false
    },
    {
     "n": "PrecioMinimo",
     "t": "decimal",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "Acceso",
   "capa": "DAL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_instancia",
     "t": "Acceso",
     "v": "Private",
     "s": true
    },
    {
     "n": "CADENA_POR_DEFECTO",
     "t": "string",
     "v": "Private",
     "s": true
    },
    {
     "n": "ConnectionString",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "InstanciaActual",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "Instancia",
     "t": "Acceso",
     "v": "Public",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "Acceso",
     "t": "",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "IniciarTransaccion",
     "t": "SqlTransaction",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ConfirmarTransaccion",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "tx",
       "SqlTransaction"
      ]
     ]
    },
    {
     "n": "CancelarTransaccion",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "tx",
       "SqlTransaction"
      ]
     ]
    },
    {
     "n": "escribir",
     "t": "int",
     "v": "Public",
     "s": false,
     "p": [
      [
       "query",
       "string"
      ],
      [
       "parametro",
       "SqlParameter[]"
      ]
     ]
    },
    {
     "n": "leer",
     "t": "DataTable",
     "v": "Public",
     "s": false,
     "p": [
      [
       "query",
       "string"
      ],
      [
       "parametro",
       "SqlParameter[]"
      ]
     ]
    },
    {
     "n": "leerEscalar",
     "t": "object",
     "v": "Public",
     "s": false,
     "p": [
      [
       "query",
       "string"
      ],
      [
       "parametro",
       "SqlParameter[]"
      ]
     ]
    },
    {
     "n": "EjecutarEnTransaccion",
     "t": "T",
     "v": "Public",
     "s": false,
     "p": [
      [
       "trabajo",
       "Func<SqlTransaction, T>"
      ]
     ]
    },
    {
     "n": "escribir",
     "t": "int",
     "v": "Public",
     "s": false,
     "p": [
      [
       "tx",
       "SqlTransaction"
      ],
      [
       "query",
       "string"
      ],
      [
       "parametro",
       "SqlParameter[]"
      ]
     ]
    },
    {
     "n": "leerEscalar",
     "t": "object",
     "v": "Public",
     "s": false,
     "p": [
      [
       "tx",
       "SqlTransaction"
      ],
      [
       "query",
       "string"
      ],
      [
       "parametro",
       "SqlParameter[]"
      ]
     ]
    },
    {
     "n": "leer",
     "t": "DataTable",
     "v": "Public",
     "s": false,
     "p": [
      [
       "tx",
       "SqlTransaction"
      ],
      [
       "query",
       "string"
      ],
      [
       "parametro",
       "SqlParameter[]"
      ]
     ]
    },
    {
     "n": "AbrirConexion",
     "t": "SqlConnection",
     "v": "Private",
     "s": true,
     "p": []
    },
    {
     "n": "CrearComando",
     "t": "SqlCommand",
     "v": "Private",
     "s": true,
     "p": [
      [
       "conexion",
       "SqlConnection"
      ],
      [
       "tx",
       "SqlTransaction"
      ],
      [
       "query",
       "string"
      ],
      [
       "parametro",
       "SqlParameter[]"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "DALBitacora_GV42",
   "capa": "DAL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_acceso",
     "t": "Acceso",
     "v": "Private",
     "s": false
    },
    {
     "n": "SELECT_BASE",
     "t": "string",
     "v": "Private",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "DALBitacora_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "Guardar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "registro",
       "Bitacora_GV42"
      ]
     ]
    },
    {
     "n": "Listar",
     "t": "List<Bitacora_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "Filtrar",
     "t": "List<Bitacora_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "login",
       "string"
      ],
      [
       "modulo",
       "string"
      ],
      [
       "tipoEvento",
       "string"
      ],
      [
       "criticidad",
       "string"
      ],
      [
       "fechaInicio",
       "DateTime"
      ],
      [
       "fechaFin",
       "DateTime"
      ]
     ]
    },
    {
     "n": "ListarTiposEvento",
     "t": "List<string>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarModulos",
     "t": "List<string>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarEventos",
     "t": "List<TipoEvento_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "BuscarEvento",
     "t": "TipoEvento_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "nombre",
       "string"
      ]
     ]
    },
    {
     "n": "ListarModulo",
     "t": "List<Modulo_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "BuscarModulo",
     "t": "Modulo_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "nombre",
       "string"
      ]
     ]
    },
    {
     "n": "MapearLista",
     "t": "List<Bitacora_GV42>",
     "v": "Private",
     "s": false,
     "p": [
      [
       "dt",
       "DataTable"
      ]
     ]
    },
    {
     "n": "ResolverCodigoModulo",
     "t": "int",
     "v": "Private",
     "s": false,
     "p": [
      [
       "m",
       "Modulo_GV42"
      ]
     ]
    },
    {
     "n": "ResolverCodigoTipoEvento",
     "t": "int",
     "v": "Private",
     "s": false,
     "p": [
      [
       "t",
       "TipoEvento_GV42"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "DALFamilia_GV42",
   "capa": "DAL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_acceso",
     "t": "Acceso",
     "v": "Private",
     "s": false
    },
    {
     "n": "_dalPatente",
     "t": "DALPatente_GV42",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "DALFamilia_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarTodasPlanas",
     "t": "List<Familia_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ObtenerArbol",
     "t": "Familia_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoPermiso",
       "int"
      ]
     ]
    },
    {
     "n": "Crear",
     "t": "int",
     "v": "Public",
     "s": false,
     "p": [
      [
       "nombre",
       "string"
      ],
      [
       "idsPatentes",
       "List<int>"
      ],
      [
       "idsSubfamilias",
       "List<int>"
      ]
     ]
    },
    {
     "n": "Eliminar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoPermiso",
       "int"
      ]
     ]
    },
    {
     "n": "Modificar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoPermiso",
       "int"
      ],
      [
       "nombre",
       "string"
      ],
      [
       "idsPatentes",
       "List<int>"
      ],
      [
       "idsSubfamilias",
       "List<int>"
      ]
     ]
    },
    {
     "n": "CantidadRolesQueUsan",
     "t": "int",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoPermiso",
       "int"
      ]
     ]
    },
    {
     "n": "CantidadFamiliasQueLaContienen",
     "t": "int",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoPermiso",
       "int"
      ]
     ]
    },
    {
     "n": "NombresRolesQueUsan",
     "t": "List<string>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoPermiso",
       "int"
      ]
     ]
    },
    {
     "n": "NombresFamiliasQueLaContienen",
     "t": "List<string>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoPermiso",
       "int"
      ]
     ]
    },
    {
     "n": "CodigosPatentesDirectas",
     "t": "List<int>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoPermiso",
       "int"
      ]
     ]
    },
    {
     "n": "CodigosSubfamiliasDirectas",
     "t": "List<int>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoPermiso",
       "int"
      ]
     ]
    },
    {
     "n": "ObtenerArbolRec",
     "t": "Familia_GV42",
     "v": "Private",
     "s": false,
     "p": [
      [
       "codigoPermiso",
       "int"
      ],
      [
       "visitadas",
       "HashSet<int>"
      ]
     ]
    },
    {
     "n": "InsertarHijos",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "tx",
       "SqlTransaction"
      ],
      [
       "codigoPermiso",
       "int"
      ],
      [
       "idsPatentes",
       "List<int>"
      ],
      [
       "idsSubfamilias",
       "List<int>"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "DALPatente_GV42",
   "capa": "DAL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_acceso",
     "t": "Acceso",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "DALPatente_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarTodas",
     "t": "List<Patente_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "BuscarPorCodigo",
     "t": "Patente_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigo",
       "int"
      ]
     ]
    },
    {
     "n": "MapearFila",
     "t": "Patente_GV42",
     "v": "Private",
     "s": false,
     "p": [
      [
       "row",
       "DataRow"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "DALRol_GV42",
   "capa": "DAL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_acceso",
     "t": "Acceso",
     "v": "Private",
     "s": false
    },
    {
     "n": "_dalFamilia",
     "t": "DALFamilia_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "_dalPatente",
     "t": "DALPatente_GV42",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "DALRol_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarTodos",
     "t": "List<Rol_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ObtenerArbol",
     "t": "Rol_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoPermiso",
       "int"
      ]
     ]
    },
    {
     "n": "Crear",
     "t": "int",
     "v": "Public",
     "s": false,
     "p": [
      [
       "nombre",
       "string"
      ],
      [
       "idsPatentes",
       "List<int>"
      ],
      [
       "idsFamilias",
       "List<int>"
      ]
     ]
    },
    {
     "n": "EstaEnUso",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoPermiso",
       "int"
      ]
     ]
    },
    {
     "n": "CantidadUsuariosConRol",
     "t": "int",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoPermiso",
       "int"
      ]
     ]
    },
    {
     "n": "Eliminar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoPermiso",
       "int"
      ]
     ]
    },
    {
     "n": "Modificar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoPermiso",
       "int"
      ],
      [
       "nombre",
       "string"
      ],
      [
       "idsPatentes",
       "List<int>"
      ],
      [
       "idsFamilias",
       "List<int>"
      ]
     ]
    },
    {
     "n": "CodigosPatentesDirectas",
     "t": "List<int>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoPermiso",
       "int"
      ]
     ]
    },
    {
     "n": "CodigosFamiliasDirectas",
     "t": "List<int>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoPermiso",
       "int"
      ]
     ]
    },
    {
     "n": "InsertarHijos",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "tx",
       "SqlTransaction"
      ],
      [
       "codigoPermiso",
       "int"
      ],
      [
       "idsPatentes",
       "List<int>"
      ],
      [
       "idsFamilias",
       "List<int>"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "DALUsuario_GV42",
   "capa": "DAL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_acceso",
     "t": "Acceso",
     "v": "Private",
     "s": false
    },
    {
     "n": "_dalIntegridad",
     "t": "DALIntegridad_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "SELECT_BASE",
     "t": "string",
     "v": "Private",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "DALUsuario_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ExisteDNI",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ]
     ]
    },
    {
     "n": "ExisteEmail",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": [
      [
       "email",
       "string"
      ],
      [
       "dniExcluido",
       "string"
      ]
     ]
    },
    {
     "n": "BuscarPorLogin",
     "t": "Usuario_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "login",
       "string"
      ]
     ]
    },
    {
     "n": "ListarActivos",
     "t": "List<Usuario_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarTodos",
     "t": "List<Usuario_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "Bloquear",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "login",
       "string"
      ]
     ]
    },
    {
     "n": "Desbloquear",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ],
      [
       "contrasenaCifrada",
       "string"
      ]
     ]
    },
    {
     "n": "ActivarDesactivar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ],
      [
       "activo",
       "bool"
      ]
     ]
    },
    {
     "n": "ModificarEmail",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ],
      [
       "email",
       "string"
      ]
     ]
    },
    {
     "n": "ModificarRol",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ],
      [
       "codigoPermiso",
       "int"
      ]
     ]
    },
    {
     "n": "CambiarContrasena",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "login",
       "string"
      ],
      [
       "contrasenaCifrada",
       "string"
      ]
     ]
    },
    {
     "n": "ActualizarHashContrasena",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "login",
       "string"
      ],
      [
       "contrasenaCifrada",
       "string"
      ]
     ]
    },
    {
     "n": "ActualizarIntentosFallidos",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "login",
       "string"
      ],
      [
       "nuevosIntentos",
       "int"
      ],
      [
       "momentoIntento",
       "DateTime"
      ]
     ]
    },
    {
     "n": "GuardarIdioma",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "login",
       "string"
      ],
      [
       "idioma",
       "string"
      ]
     ]
    },
    {
     "n": "ResetearIntentosFallidos",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "login",
       "string"
      ]
     ]
    },
    {
     "n": "AgregarUsuario",
     "t": "int",
     "v": "Public",
     "s": false,
     "p": [
      [
       "usuario",
       "Usuario_GV42"
      ]
     ]
    },
    {
     "n": "AgregarUsuarioAutogestionado",
     "t": "int",
     "v": "Public",
     "s": false,
     "p": [
      [
       "usuario",
       "Usuario_GV42"
      ]
     ]
    },
    {
     "n": "ListarRoles",
     "t": "List<Rol_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "BuscarPorNombre",
     "t": "Rol_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "nombre",
       "string"
      ]
     ]
    },
    {
     "n": "RecalcularIntegridadUsuario",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ]
     ]
    },
    {
     "n": "DniDeLogin",
     "t": "string",
     "v": "Private",
     "s": false,
     "p": [
      [
       "login",
       "string"
      ]
     ]
    },
    {
     "n": "MapearLista",
     "t": "List<Usuario_GV42>",
     "v": "Private",
     "s": false,
     "p": [
      [
       "dt",
       "DataTable"
      ]
     ]
    },
    {
     "n": "MapearFila",
     "t": "Usuario_GV42",
     "v": "Private",
     "s": false,
     "p": [
      [
       "row",
       "DataRow"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "DALIntegridad_GV42",
   "capa": "DAL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_acceso",
     "t": "Acceso",
     "v": "Private",
     "s": false
    },
    {
     "n": "TABLAS_PROTEGIDAS",
     "t": "string[]",
     "v": "Public",
     "s": true
    },
    {
     "n": "IntegridadConocidamenteRota",
     "t": "bool",
     "v": "Public",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "DALIntegridad_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "TablasProtegidasExistentes",
     "t": "List<string>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "CalcularDVHsTabla",
     "t": "Dictionary<string, string>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "nombreTabla",
       "string"
      ]
     ]
    },
    {
     "n": "ObtenerDVHsAlmacenados",
     "t": "Dictionary<string, string>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "nombreTabla",
       "string"
      ]
     ]
    },
    {
     "n": "ObtenerDVVAlmacenado",
     "t": "string",
     "v": "Public",
     "s": false,
     "p": [
      [
       "nombreTabla",
       "string"
      ]
     ]
    },
    {
     "n": "RecalcularTabla",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "nombreTabla",
       "string"
      ]
     ]
    },
    {
     "n": "ActualizarDVHRegistros",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "nombreTabla",
       "string"
      ],
      [
       "esAfectado",
       "Func<string, bool>"
      ]
     ]
    },
    {
     "n": "ActualizarDVHRegistro",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "nombreTabla",
       "string"
      ],
      [
       "codigoRegistro",
       "string"
      ]
     ]
    },
    {
     "n": "ExisteAlgunDVV",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "HacerBackupSQL",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "connStringMaster",
       "string"
      ],
      [
       "nombreBd",
       "string"
      ],
      [
       "rutaArchivoBak",
       "string"
      ]
     ]
    },
    {
     "n": "RestaurarBackupSQL",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "connStringMaster",
       "string"
      ],
      [
       "nombreBd",
       "string"
      ],
      [
       "rutaArchivoBak",
       "string"
      ]
     ]
    },
    {
     "n": "BloquearTabla",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "tx",
       "SqlTransaction"
      ],
      [
       "nombreTabla",
       "string"
      ]
     ]
    },
    {
     "n": "LeerDVHs",
     "t": "Dictionary<string, string>",
     "v": "Private",
     "s": false,
     "p": [
      [
       "tx",
       "SqlTransaction"
      ],
      [
       "nombreTabla",
       "string"
      ]
     ]
    },
    {
     "n": "InsertarDVH",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "tx",
       "SqlTransaction"
      ],
      [
       "nombreTabla",
       "string"
      ],
      [
       "codigoRegistro",
       "string"
      ],
      [
       "dvh",
       "string"
      ]
     ]
    },
    {
     "n": "GuardarDVV",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "tx",
       "SqlTransaction"
      ],
      [
       "nombreTabla",
       "string"
      ],
      [
       "dvv",
       "string"
      ]
     ]
    },
    {
     "n": "DVHsModulo",
     "t": "Dictionary<string, string>",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "DVHsTipoEvento",
     "t": "Dictionary<string, string>",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "DVHsReserva",
     "t": "Dictionary<string, string>",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "DVHsPago",
     "t": "Dictionary<string, string>",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "DVHsUsuario",
     "t": "Dictionary<string, string>",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "DVHsRoles",
     "t": "Dictionary<string, string>",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "DVHsFamilia",
     "t": "Dictionary<string, string>",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "DVHsPatente",
     "t": "Dictionary<string, string>",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "DVHsFamiliaPatente",
     "t": "Dictionary<string, string>",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "DVHsFamiliaIntegrada",
     "t": "Dictionary<string, string>",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "DVHsRolPatente",
     "t": "Dictionary<string, string>",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "DVHsRolFamilia",
     "t": "Dictionary<string, string>",
     "v": "Private",
     "s": false,
     "p": []
    }
   ]
  },
  {
   "nombre": "InstaladorBD_GV42",
   "capa": "DAL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "NOMBRE_BD",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "NOMBRE_SCRIPT",
     "t": "string",
     "v": "Private",
     "s": true
    },
    {
     "n": "NOMBRE_SCRIPT_ACTUALIZACION",
     "t": "string",
     "v": "Private",
     "s": true
    },
    {
     "n": "NOMBRE_SCRIPT_BASE",
     "t": "string",
     "v": "Private",
     "s": true
    },
    {
     "n": "NOMBRE_SCRIPT_NEGOCIO",
     "t": "string",
     "v": "Private",
     "s": true
    },
    {
     "n": "NOMBRE_SCRIPT_LIMPIAR",
     "t": "string",
     "v": "Private",
     "s": true
    },
    {
     "n": "NOMBRE_SCRIPT_LIMPIAR_CATALOGOS",
     "t": "string",
     "v": "Private",
     "s": true
    },
    {
     "n": "NOMBRE_SCRIPT_DEMOSTRACION",
     "t": "string",
     "v": "Private",
     "s": true
    },
    {
     "n": "VERSION_ACTUAL",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "OBJETOS_BASE",
     "t": "string[]",
     "v": "Private",
     "s": true
    },
    {
     "n": "OBJETOS_REQUERIDOS",
     "t": "string[]",
     "v": "Private",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "ExisteBaseDatos",
     "t": "bool",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ]
     ]
    },
    {
     "n": "InstalarBaseDatos",
     "t": "void",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ],
      [
       "datosDemostracion",
       "bool"
      ]
     ]
    },
    {
     "n": "CargarDatosDemostracion",
     "t": "bool",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ]
     ]
    },
    {
     "n": "InstalarBaseUsuarios",
     "t": "void",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ]
     ]
    },
    {
     "n": "EsBaseUsuarios",
     "t": "bool",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ]
     ]
    },
    {
     "n": "AgregarNegocio",
     "t": "void",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ]
     ]
    },
    {
     "n": "NegocioInstaladoEnConexionActual",
     "t": "bool",
     "v": "Public",
     "s": true,
     "p": []
    },
    {
     "n": "ObtenerVersion",
     "t": "int",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ]
     ]
    },
    {
     "n": "ObjetosFaltantes",
     "t": "List<string>",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ]
     ]
    },
    {
     "n": "ActualizarBaseDatos",
     "t": "void",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ]
     ]
    },
    {
     "n": "ReinstalarBaseDatos",
     "t": "void",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ],
      [
       "datosDemostracion",
       "bool"
      ]
     ]
    },
    {
     "n": "GenerarVuelosFaltantes",
     "t": "int",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ],
      [
       "dias",
       "int"
      ]
     ]
    },
    {
     "n": "TareasPendientes",
     "t": "List<string>",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ]
     ]
    },
    {
     "n": "CifrarEmailsPlanos",
     "t": "int",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ]
     ]
    },
    {
     "n": "QuitarTarea",
     "t": "void",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ],
      [
       "nombre",
       "string"
      ]
     ]
    },
    {
     "n": "ConexionMaster",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ],
      [
       "timeout",
       "int"
      ]
     ]
    },
    {
     "n": "ConexionBase",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ]
     ]
    },
    {
     "n": "EliminarBaseDatos",
     "t": "void",
     "v": "Private",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ]
     ]
    },
    {
     "n": "EjecutarScript",
     "t": "void",
     "v": "Private",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ],
      [
       "nombreArchivo",
       "string"
      ],
      [
       "transaccional",
       "bool"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "DALAerolinea_GV42",
   "capa": "DAL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_acceso",
     "t": "Acceso",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "DALAerolinea_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarTodos",
     "t": "List<Aerolinea_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ContarVuelos",
     "t": "int",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigo",
       "int"
      ]
     ]
    },
    {
     "n": "Insertar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "a",
       "Aerolinea_GV42"
      ]
     ]
    },
    {
     "n": "Modificar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "a",
       "Aerolinea_GV42"
      ]
     ]
    },
    {
     "n": "Eliminar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigo",
       "int"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "DALAeropuerto_GV42",
   "capa": "DAL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_acceso",
     "t": "Acceso",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "DALAeropuerto_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarTodos",
     "t": "List<Aeropuerto_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ExisteCodigo",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoIata",
       "string"
      ],
      [
       "codigoExcluir",
       "int"
      ]
     ]
    },
    {
     "n": "ContarVuelos",
     "t": "int",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigo",
       "int"
      ]
     ]
    },
    {
     "n": "Insertar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "a",
       "Aeropuerto_GV42"
      ]
     ]
    },
    {
     "n": "Modificar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "a",
       "Aeropuerto_GV42"
      ]
     ]
    },
    {
     "n": "Eliminar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigo",
       "int"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "DALAsiento_GV42",
   "capa": "DAL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_acceso",
     "t": "Acceso",
     "v": "Private",
     "s": false
    },
    {
     "n": "SELECT_BASE",
     "t": "string",
     "v": "Private",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "DALAsiento_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarLibres",
     "t": "List<Asiento_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroVuelo",
       "int"
      ],
      [
       "clase",
       "ClaseVuelo_GV42"
      ]
     ]
    },
    {
     "n": "BuscarLibreAlAzar",
     "t": "Asiento_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroVuelo",
       "int"
      ],
      [
       "clase",
       "ClaseVuelo_GV42"
      ]
     ]
    },
    {
     "n": "BuscarPorNumero",
     "t": "Asiento_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroVuelo",
       "int"
      ],
      [
       "numeroAsiento",
       "string"
      ]
     ]
    },
    {
     "n": "BuscarPorCodigo",
     "t": "Asiento_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoAsiento",
       "int"
      ]
     ]
    },
    {
     "n": "ListarMapa",
     "t": "List<AsientoDisponibilidad_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroVuelo",
       "int"
      ],
      [
       "clase",
       "ClaseVuelo_GV42"
      ]
     ]
    },
    {
     "n": "EstaReservado",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoAsiento",
       "int"
      ]
     ]
    },
    {
     "n": "EstaOcupadoPorOtro",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoAsiento",
       "int"
      ],
      [
       "codigoCheckIn",
       "int"
      ]
     ]
    },
    {
     "n": "Asignar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoCheckIn",
       "int"
      ],
      [
       "codigoAsiento",
       "int"
      ]
     ]
    },
    {
     "n": "Mapear",
     "t": "Asiento_GV42",
     "v": "Private",
     "s": false,
     "p": [
      [
       "r",
       "DataRow"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "DALBoleto_GV42",
   "capa": "DAL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_acceso",
     "t": "Acceso",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "DALBoleto_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarPorReserva",
     "t": "List<Boleto_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroReserva",
       "string"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "DALCheckIn_GV42",
   "capa": "DAL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_acceso",
     "t": "Acceso",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "DALCheckIn_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "BuscarPorReservaYDni",
     "t": "CheckIn_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroReserva",
       "string"
      ],
      [
       "dni",
       "string"
      ]
     ]
    },
    {
     "n": "BuscarPorReservaYDni",
     "t": "CheckIn_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroReserva",
       "string"
      ],
      [
       "dni",
       "string"
      ],
      [
       "tramo",
       "int"
      ]
     ]
    },
    {
     "n": "ListarDnisPasajeros",
     "t": "List<string>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroReserva",
       "string"
      ]
     ]
    },
    {
     "n": "ListarInfantes",
     "t": "List<string>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoReserva",
       "int"
      ]
     ]
    },
    {
     "n": "BuscarPorCodigo",
     "t": "CheckIn_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoCheckIn",
       "int"
      ]
     ]
    },
    {
     "n": "MarcarRealizado",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoCheckIn",
       "int"
      ],
      [
       "loginEncargado",
       "string"
      ],
      [
       "canal",
       "CanalVenta_GV42"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "DALCliente_GV42",
   "capa": "DAL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_acceso",
     "t": "Acceso",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "DALCliente_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "Listar",
     "t": "List<Cliente_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ExisteDni",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ]
     ]
    },
    {
     "n": "BuscarPorDni",
     "t": "Cliente_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ]
     ]
    },
    {
     "n": "ContarReservas",
     "t": "int",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ]
     ]
    },
    {
     "n": "TieneUsuario",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ]
     ]
    },
    {
     "n": "Insertar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "c",
       "Cliente_GV42"
      ]
     ]
    },
    {
     "n": "Modificar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "c",
       "Cliente_GV42"
      ]
     ]
    },
    {
     "n": "Eliminar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ]
     ]
    },
    {
     "n": "Parametros",
     "t": "SqlParameter[]",
     "v": "Private",
     "s": true,
     "p": [
      [
       "c",
       "Cliente_GV42"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "DALPasajero_GV42",
   "capa": "DAL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_acceso",
     "t": "Acceso",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "DALPasajero_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ExisteDni",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ]
     ]
    },
    {
     "n": "BuscarPorDni",
     "t": "Pasajero_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ]
     ]
    },
    {
     "n": "BuscarDatosEnUsuario",
     "t": "Pasajero_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ]
     ]
    },
    {
     "n": "Eliminar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ]
     ]
    },
    {
     "n": "Insertar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "p",
       "Pasajero_GV42"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "DALEquipaje_GV42",
   "capa": "DAL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_acceso",
     "t": "Acceso",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "DALEquipaje_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "Registrar",
     "t": "Equipaje_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "e",
       "Equipaje_GV42"
      ]
     ]
    },
    {
     "n": "BuscarPorCheckIn",
     "t": "Equipaje_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoCheckIn",
       "int"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "DALPago_GV42",
   "capa": "DAL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_acceso",
     "t": "Acceso",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "DALPago_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "RegistrarPagoYConfirmar",
     "t": "Pago_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "pago",
       "Pago_GV42"
      ]
     ]
    },
    {
     "n": "BuscarPorReserva",
     "t": "Pago_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoReserva",
       "int"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "DALReserva_GV42",
   "capa": "DAL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_acceso",
     "t": "Acceso",
     "v": "Private",
     "s": false
    },
    {
     "n": "SELECT_LISTADO",
     "t": "string",
     "v": "Private",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "DALReserva_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "Crear",
     "t": "Reserva_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "r",
       "Reserva_GV42"
      ]
     ]
    },
    {
     "n": "BuscarPorNumero",
     "t": "Reserva_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroReserva",
       "string"
      ]
     ]
    },
    {
     "n": "ListarPorPersona",
     "t": "List<Reserva_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ]
     ]
    },
    {
     "n": "Buscar",
     "t": "List<Reserva_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "textoLibre",
       "string"
      ]
     ]
    },
    {
     "n": "Cancelar",
     "t": "Reserva_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoReserva",
       "int"
      ],
      [
       "montoPenalidad",
       "decimal"
      ],
      [
       "reembolso",
       "Reembolso_GV42"
      ]
     ]
    },
    {
     "n": "LiberarVencidas",
     "t": "List<int>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "BuscarReembolso",
     "t": "Reembolso_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoReserva",
       "int"
      ]
     ]
    },
    {
     "n": "ProcesarReembolso",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoReserva",
       "int"
      ],
      [
       "login",
       "string"
      ]
     ]
    },
    {
     "n": "CambiarVuelo",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "reserva",
       "Reserva_GV42"
      ],
      [
       "tramo",
       "int"
      ],
      [
       "nuevo",
       "VueloClase_GV42"
      ],
      [
       "asientos",
       "List<AsientoPasajero_GV42>"
      ],
      [
       "cotizacion",
       "CotizacionCambio_GV42"
      ],
      [
       "medioPago",
       "MedioPago_GV42?"
      ],
      [
       "numeroTransaccion",
       "string"
      ],
      [
       "login",
       "string"
      ]
     ]
    },
    {
     "n": "TieneCheckInRealizado",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoReserva",
       "int"
      ],
      [
       "tramo",
       "int"
      ]
     ]
    },
    {
     "n": "PasajeroYaViajaEnVuelo",
     "t": "string",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroVuelo",
       "int"
      ],
      [
       "dnis",
       "IEnumerable<string>"
      ],
      [
       "codigoReservaExcluir",
       "int"
      ]
     ]
    },
    {
     "n": "ContarPendientesDePago",
     "t": "int",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dniCliente",
       "string"
      ]
     ]
    },
    {
     "n": "TieneCheckInRealizado",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoReserva",
       "int"
      ]
     ]
    },
    {
     "n": "ListarPasajeros",
     "t": "List<Pasajero_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoReserva",
       "int"
      ]
     ]
    },
    {
     "n": "ListarAsientosPorPasajero",
     "t": "List<AsientoPasajero_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoReserva",
       "int"
      ]
     ]
    },
    {
     "n": "ListarAdicionales",
     "t": "List<AdicionalReserva_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoReserva",
       "int"
      ]
     ]
    },
    {
     "n": "BuscarVueloClaseVuelta",
     "t": "VueloClase_GV42",
     "v": "Private",
     "s": true,
     "p": [
      [
       "row",
       "DataRow"
      ]
     ]
    },
    {
     "n": "MapearListado",
     "t": "Reserva_GV42",
     "v": "Private",
     "s": false,
     "p": [
      [
       "row",
       "DataRow"
      ]
     ]
    },
    {
     "n": "LlenarDatosComunes",
     "t": "void",
     "v": "Private",
     "s": true,
     "p": [
      [
       "reserva",
       "Reserva_GV42"
      ],
      [
       "row",
       "DataRow"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "DALReporteCheckIn_GV42",
   "capa": "DAL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_acceso",
     "t": "Acceso",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "Listar",
     "t": "List<ReporteCheckIn_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "filtro",
       "FiltroReporteCheckIn_GV42"
      ]
     ]
    },
    {
     "n": "ArmarWhere",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "f",
       "FiltroReporteCheckIn_GV42"
      ]
     ]
    },
    {
     "n": "Parametros",
     "t": "SqlParameter[]",
     "v": "Private",
     "s": true,
     "p": [
      [
       "f",
       "FiltroReporteCheckIn_GV42"
      ]
     ]
    },
    {
     "n": "Escapar",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "texto",
       "string"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "DALReporteMillas_GV42",
   "capa": "DAL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_acceso",
     "t": "Acceso",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "DALReporteMillas_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarVuelosVolados",
     "t": "List<VueloVolado_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarContrataciones",
     "t": "List<ContratacionAdicional_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    }
   ]
  },
  {
   "nombre": "DALReporteReserva_GV42",
   "capa": "DAL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_acceso",
     "t": "Acceso",
     "v": "Private",
     "s": false
    },
    {
     "n": "FROM_RESERVAS",
     "t": "string",
     "v": "Private",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "Listar",
     "t": "List<ReporteReserva_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "filtro",
       "FiltroReporteReservas_GV42"
      ]
     ]
    },
    {
     "n": "CargarAdicionales",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "filtro",
       "FiltroReporteReservas_GV42"
      ],
      [
       "where",
       "string"
      ],
      [
       "porCodigo",
       "Dictionary<int, ReporteReserva_GV42>"
      ]
     ]
    },
    {
     "n": "ArmarWhere",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "f",
       "FiltroReporteReservas_GV42"
      ]
     ]
    },
    {
     "n": "Parametros",
     "t": "SqlParameter[]",
     "v": "Private",
     "s": true,
     "p": [
      [
       "f",
       "FiltroReporteReservas_GV42"
      ]
     ]
    },
    {
     "n": "Escapar",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "texto",
       "string"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "DALTarjetaEmbarque_GV42",
   "capa": "DAL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_acceso",
     "t": "Acceso",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "DALTarjetaEmbarque_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "Generar",
     "t": "TarjetaEmbarque_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoCheckIn",
       "int"
      ],
      [
       "puertaEmbarque",
       "string"
      ],
      [
       "horaLimiteEmbarque",
       "DateTime"
      ]
     ]
    },
    {
     "n": "BuscarPorCheckIn",
     "t": "TarjetaEmbarque_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoCheckIn",
       "int"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "DALTipoAdicional_GV42",
   "capa": "DAL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_acceso",
     "t": "Acceso",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "DALTipoAdicional_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarActivos",
     "t": "List<TipoAdicional_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    }
   ]
  },
  {
   "nombre": "DALUtil_GV42",
   "capa": "DAL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "COLUMNAS_VUELO_CLASE",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "JOINS_VUELO",
     "t": "string",
     "v": "Public",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "JoinVueloDeTramo",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "aliasTramo",
       "string"
      ]
     ]
    },
    {
     "n": "ADb",
     "t": "object",
     "v": "Public",
     "s": true,
     "p": [
      [
       "valor",
       "object"
      ]
     ]
    },
    {
     "n": "Str",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "r",
       "DataRow"
      ],
      [
       "columna",
       "string"
      ]
     ]
    },
    {
     "n": "Int",
     "t": "int",
     "v": "Public",
     "s": true,
     "p": [
      [
       "r",
       "DataRow"
      ],
      [
       "columna",
       "string"
      ]
     ]
    },
    {
     "n": "Dec",
     "t": "decimal",
     "v": "Public",
     "s": true,
     "p": [
      [
       "r",
       "DataRow"
      ],
      [
       "columna",
       "string"
      ]
     ]
    },
    {
     "n": "Fecha",
     "t": "DateTime",
     "v": "Public",
     "s": true,
     "p": [
      [
       "r",
       "DataRow"
      ],
      [
       "columna",
       "string"
      ]
     ]
    },
    {
     "n": "FechaNull",
     "t": "DateTime?",
     "v": "Public",
     "s": true,
     "p": [
      [
       "r",
       "DataRow"
      ],
      [
       "columna",
       "string"
      ]
     ]
    },
    {
     "n": "Cifrar",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "texto",
       "string"
      ]
     ]
    },
    {
     "n": "Descifrar",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "texto",
       "string"
      ]
     ]
    },
    {
     "n": "LlenarPersona",
     "t": "void",
     "v": "Public",
     "s": true,
     "p": [
      [
       "p",
       "Persona_GV42"
      ],
      [
       "r",
       "DataRow"
      ],
      [
       "pref",
       "string"
      ]
     ]
    },
    {
     "n": "MapearVueloClase",
     "t": "VueloClase_GV42",
     "v": "Public",
     "s": true,
     "p": [
      [
       "r",
       "DataRow"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "DALVuelo_GV42",
   "capa": "DAL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_acceso",
     "t": "Acceso",
     "v": "Private",
     "s": false
    },
    {
     "n": "SELECT_BASE",
     "t": "string",
     "v": "Private",
     "s": true
    },
    {
     "n": "SELECT_ABM",
     "t": "string",
     "v": "Private",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "DALVuelo_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "BuscarDisponibles",
     "t": "List<VueloClase_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "criterio",
       "CriterioBusquedaVuelo_GV42"
      ]
     ]
    },
    {
     "n": "PreciosMinimosPorFecha",
     "t": "Dictionary<DateTime, decimal>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "criterio",
       "CriterioBusquedaVuelo_GV42"
      ],
      [
       "desde",
       "DateTime"
      ],
      [
       "hasta",
       "DateTime"
      ]
     ]
    },
    {
     "n": "BuscarVueloClase",
     "t": "VueloClase_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroVuelo",
       "int"
      ],
      [
       "clase",
       "ClaseVuelo_GV42"
      ]
     ]
    },
    {
     "n": "BuscarVueloClaseSinFiltro",
     "t": "VueloClase_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroVuelo",
       "int"
      ],
      [
       "clase",
       "ClaseVuelo_GV42"
      ]
     ]
    },
    {
     "n": "ListarTodos",
     "t": "List<Vuelo_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarAerolineas",
     "t": "List<Aerolinea_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ExisteCodigo",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigo",
       "string"
      ],
      [
       "codigoExcluido",
       "int"
      ]
     ]
    },
    {
     "n": "ContarReservasVigentes",
     "t": "int",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroVuelo",
       "int"
      ]
     ]
    },
    {
     "n": "ContarCheckInsRealizados",
     "t": "int",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroVuelo",
       "int"
      ]
     ]
    },
    {
     "n": "ContarReservasIncoherentes",
     "t": "int",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroVuelo",
       "int"
      ],
      [
       "salida",
       "DateTime"
      ],
      [
       "llegada",
       "DateTime"
      ]
     ]
    },
    {
     "n": "Crear",
     "t": "int",
     "v": "Public",
     "s": false,
     "p": [
      [
       "v",
       "Vuelo_GV42"
      ],
      [
       "clases",
       "List<VueloClase_GV42>"
      ],
      [
       "filasPrimera",
       "int"
      ],
      [
       "filasEjecutiva",
       "int"
      ],
      [
       "filasTotales",
       "int"
      ]
     ]
    },
    {
     "n": "Modificar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "v",
       "Vuelo_GV42"
      ]
     ]
    },
    {
     "n": "CambiarBorradoLogico",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroVuelo",
       "int"
      ],
      [
       "baja",
       "bool"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "DALVueloHistorial_GV42",
   "capa": "DAL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_acceso",
     "t": "Acceso",
     "v": "Private",
     "s": false
    },
    {
     "n": "NOMBRE_RUTA",
     "t": "string",
     "v": "Private",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "DALVueloHistorial_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "Listar",
     "t": "List<VueloCambio_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoVuelo",
       "string"
      ],
      [
       "nombre",
       "string"
      ],
      [
       "fechaIni",
       "DateTime?"
      ],
      [
       "fechaFin",
       "DateTime?"
      ],
      [
       "soloConCambios",
       "bool"
      ]
     ]
    },
    {
     "n": "ListarCodigos",
     "t": "List<string>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarNombres",
     "t": "List<string>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ActivarVersion",
     "t": "string",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoCambio",
       "int"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "DALTarifa_GV42",
   "capa": "DAL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_acceso",
     "t": "Acceso",
     "v": "Private",
     "s": false
    },
    {
     "n": "SELECT_BASE",
     "t": "string",
     "v": "Private",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "DALTarifa_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarActivas",
     "t": "List<TarifaFamilia_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "BuscarPorCodigo",
     "t": "TarifaFamilia_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigo",
       "int"
      ]
     ]
    },
    {
     "n": "Mapear",
     "t": "TarifaFamilia_GV42",
     "v": "Private",
     "s": true,
     "p": [
      [
       "r",
       "DataRow"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "BLLBitacora_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_Instancia",
     "t": "BLLBitacora_GV42",
     "v": "Private",
     "s": true
    },
    {
     "n": "_DALBitacora",
     "t": "DALBitacora_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "Instancia",
     "t": "BLLBitacora_GV42",
     "v": "Public",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "BLLBitacora_GV42",
     "t": "",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "RegistrarEvento",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "login",
       "string"
      ],
      [
       "modulo",
       "string"
      ],
      [
       "tipoEvento",
       "string"
      ],
      [
       "detalle",
       "string"
      ],
      [
       "criticidad",
       "string"
      ]
     ]
    },
    {
     "n": "Listar",
     "t": "List<Bitacora_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "Filtrar",
     "t": "List<Bitacora_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "login",
       "string"
      ],
      [
       "modulo",
       "string"
      ],
      [
       "tipoEvento",
       "string"
      ],
      [
       "criticidad",
       "string"
      ],
      [
       "fechaInicio",
       "DateTime"
      ],
      [
       "fechaFin",
       "DateTime"
      ]
     ]
    },
    {
     "n": "ListarModulos",
     "t": "List<string>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarTiposEvento",
     "t": "List<string>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarCriticidades",
     "t": "List<string>",
     "v": "Public",
     "s": false,
     "p": []
    }
   ]
  },
  {
   "nombre": "BLLBoleto_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "MINUTOS_INICIO_EMBARQUE",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "MINUTOS_CIERRE_EMBARQUE",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "_bllReserva",
     "t": "BLLReserva_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "_dalBoleto",
     "t": "DALBoleto_GV42",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "ObtenerBoletosElectronicos",
     "t": "List<BoletoElectronico_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroReserva",
       "string"
      ]
     ]
    },
    {
     "n": "TextoServicios",
     "t": "string",
     "v": "Package",
     "s": true,
     "p": [
      [
       "adicionales",
       "List<AdicionalReserva_GV42>"
      ]
     ]
    },
    {
     "n": "ExportarPdf",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "ruta",
       "string"
      ],
      [
       "boletos",
       "List<BoletoElectronico_GV42>"
      ]
     ]
    },
    {
     "n": "CodigoDeBarras",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "boleto",
       "string"
      ],
      [
       "vuelo",
       "string"
      ],
      [
       "asiento",
       "string"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "Code128_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "PATRONES",
     "t": "string[]",
     "v": "Private",
     "s": true
    },
    {
     "n": "INICIO_B",
     "t": "int",
     "v": "Private",
     "s": true
    },
    {
     "n": "PARADA",
     "t": "int",
     "v": "Private",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "Codificar",
     "t": "List<int>",
     "v": "Public",
     "s": true,
     "p": [
      [
       "texto",
       "string"
      ]
     ]
    },
    {
     "n": "Dibujar",
     "t": "void",
     "v": "Public",
     "s": true,
     "p": [
      [
       "lienzo",
       "ILienzo_GV42"
      ],
      [
       "texto",
       "string"
      ],
      [
       "x",
       "float"
      ],
      [
       "y",
       "float"
      ],
      [
       "ancho",
       "float"
      ],
      [
       "alto",
       "float"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "DisenioBoleto_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "ANCHO",
     "t": "float",
     "v": "Public",
     "s": true
    },
    {
     "n": "ALTO",
     "t": "float",
     "v": "Public",
     "s": true
    },
    {
     "n": "ANCHO_PRINCIPAL",
     "t": "float",
     "v": "Private",
     "s": true
    },
    {
     "n": "ALTO_ENCABEZADO",
     "t": "float",
     "v": "Private",
     "s": true
    },
    {
     "n": "AZUL",
     "t": "int",
     "v": "Private",
     "s": true
    },
    {
     "n": "AZUL_OSCURO",
     "t": "int",
     "v": "Private",
     "s": true
    },
    {
     "n": "CELESTE",
     "t": "int",
     "v": "Private",
     "s": true
    },
    {
     "n": "TEXTO",
     "t": "int",
     "v": "Private",
     "s": true
    },
    {
     "n": "GRIS",
     "t": "int",
     "v": "Private",
     "s": true
    },
    {
     "n": "GRIS_CLARO",
     "t": "int",
     "v": "Private",
     "s": true
    },
    {
     "n": "BLANCO",
     "t": "int",
     "v": "Private",
     "s": true
    },
    {
     "n": "VERDE",
     "t": "int",
     "v": "Private",
     "s": true
    },
    {
     "n": "Cultura",
     "t": "CultureInfo",
     "v": "Private",
     "s": true
    },
    {
     "n": "CulturaIngles",
     "t": "CultureInfo",
     "v": "Private",
     "s": true
    },
    {
     "n": "CulturaFechas",
     "t": "CultureInfo",
     "v": "Private",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "Dibujar",
     "t": "void",
     "v": "Public",
     "s": true,
     "p": [
      [
       "l",
       "ILienzo_GV42"
      ],
      [
       "b",
       "BoletoElectronico_GV42"
      ],
      [
       "x0",
       "float"
      ],
      [
       "y0",
       "float"
      ]
     ]
    },
    {
     "n": "Campo",
     "t": "void",
     "v": "Private",
     "s": true,
     "p": [
      [
       "l",
       "ILienzo_GV42"
      ],
      [
       "x",
       "float"
      ],
      [
       "y",
       "float"
      ],
      [
       "etiqueta",
       "string"
      ],
      [
       "valor",
       "string"
      ],
      [
       "tamValor",
       "float"
      ],
      [
       "anchoMax",
       "float"
      ],
      [
       "color",
       "int"
      ]
     ]
    },
    {
     "n": "TextoAjustado",
     "t": "void",
     "v": "Private",
     "s": true,
     "p": [
      [
       "l",
       "ILienzo_GV42"
      ],
      [
       "x",
       "float"
      ],
      [
       "y",
       "float"
      ],
      [
       "texto",
       "string"
      ],
      [
       "tam",
       "float"
      ],
      [
       "negrita",
       "bool"
      ],
      [
       "color",
       "int"
      ],
      [
       "anchoMax",
       "float"
      ]
     ]
    },
    {
     "n": "TextoDerecha",
     "t": "void",
     "v": "Private",
     "s": true,
     "p": [
      [
       "l",
       "ILienzo_GV42"
      ],
      [
       "xDer",
       "float"
      ],
      [
       "y",
       "float"
      ],
      [
       "texto",
       "string"
      ],
      [
       "tam",
       "float"
      ],
      [
       "negrita",
       "bool"
      ],
      [
       "color",
       "int"
      ],
      [
       "anchoMax",
       "float"
      ]
     ]
    },
    {
     "n": "TextoCentrado",
     "t": "void",
     "v": "Private",
     "s": true,
     "p": [
      [
       "l",
       "ILienzo_GV42"
      ],
      [
       "xCentro",
       "float"
      ],
      [
       "y",
       "float"
      ],
      [
       "texto",
       "string"
      ],
      [
       "tam",
       "float"
      ],
      [
       "negrita",
       "bool"
      ],
      [
       "color",
       "int"
      ]
     ]
    },
    {
     "n": "Avion",
     "t": "void",
     "v": "Private",
     "s": true,
     "p": [
      [
       "l",
       "ILienzo_GV42"
      ],
      [
       "cx",
       "float"
      ],
      [
       "cy",
       "float"
      ],
      [
       "color",
       "int"
      ],
      [
       "escala",
       "float"
      ]
     ]
    },
    {
     "n": "Fecha",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "f",
       "DateTime"
      ]
     ]
    },
    {
     "n": "Duracion",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "d",
       "TimeSpan"
      ]
     ]
    },
    {
     "n": "TextoEstado",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "b",
       "BoletoElectronico_GV42"
      ]
     ]
    },
    {
     "n": "Dinero",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "importe",
       "decimal"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "ILienzo_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Interface",
   "attrs": [],
   "metodos": [
    {
     "n": "Rectangulo",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "x",
       "float"
      ],
      [
       "y",
       "float"
      ],
      [
       "ancho",
       "float"
      ],
      [
       "alto",
       "float"
      ],
      [
       "relleno",
       "int?"
      ],
      [
       "borde",
       "int?"
      ],
      [
       "grosor",
       "float"
      ]
     ]
    },
    {
     "n": "Linea",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "x1",
       "float"
      ],
      [
       "y1",
       "float"
      ],
      [
       "x2",
       "float"
      ],
      [
       "y2",
       "float"
      ],
      [
       "color",
       "int"
      ],
      [
       "grosor",
       "float"
      ],
      [
       "punteada",
       "bool"
      ]
     ]
    },
    {
     "n": "Poligono",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "xs",
       "float[]"
      ],
      [
       "ys",
       "float[]"
      ],
      [
       "relleno",
       "int"
      ]
     ]
    },
    {
     "n": "Texto",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "x",
       "float"
      ],
      [
       "y",
       "float"
      ],
      [
       "texto",
       "string"
      ],
      [
       "tamanio",
       "float"
      ],
      [
       "negrita",
       "bool"
      ],
      [
       "color",
       "int"
      ]
     ]
    },
    {
     "n": "AnchoTexto",
     "t": "float",
     "v": "Public",
     "s": false,
     "p": [
      [
       "texto",
       "string"
      ],
      [
       "tamanio",
       "float"
      ],
      [
       "negrita",
       "bool"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "LienzoPdf_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "INV",
     "t": "CultureInfo",
     "v": "Private",
     "s": true
    },
    {
     "n": "_paginas",
     "t": "List<StringBuilder>",
     "v": "Private",
     "s": false
    },
    {
     "n": "_actual",
     "t": "StringBuilder",
     "v": "Private",
     "s": false
    },
    {
     "n": "AnchoPagina",
     "t": "float",
     "v": "Public",
     "s": false
    },
    {
     "n": "AltoPagina",
     "t": "float",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "LienzoPdf_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "NuevaPagina",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "Guardar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "ruta",
       "string"
      ]
     ]
    },
    {
     "n": "Rectangulo",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "x",
       "float"
      ],
      [
       "y",
       "float"
      ],
      [
       "ancho",
       "float"
      ],
      [
       "alto",
       "float"
      ],
      [
       "relleno",
       "int?"
      ],
      [
       "borde",
       "int?"
      ],
      [
       "grosor",
       "float"
      ]
     ]
    },
    {
     "n": "Linea",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "x1",
       "float"
      ],
      [
       "y1",
       "float"
      ],
      [
       "x2",
       "float"
      ],
      [
       "y2",
       "float"
      ],
      [
       "color",
       "int"
      ],
      [
       "grosor",
       "float"
      ],
      [
       "punteada",
       "bool"
      ]
     ]
    },
    {
     "n": "Poligono",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "xs",
       "float[]"
      ],
      [
       "ys",
       "float[]"
      ],
      [
       "relleno",
       "int"
      ]
     ]
    },
    {
     "n": "Texto",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "x",
       "float"
      ],
      [
       "y",
       "float"
      ],
      [
       "texto",
       "string"
      ],
      [
       "tamanio",
       "float"
      ],
      [
       "negrita",
       "bool"
      ],
      [
       "color",
       "int"
      ]
     ]
    },
    {
     "n": "AnchoTexto",
     "t": "float",
     "v": "Public",
     "s": false,
     "p": [
      [
       "texto",
       "string"
      ],
      [
       "tamanio",
       "float"
      ],
      [
       "negrita",
       "bool"
      ]
     ]
    },
    {
     "n": "Y",
     "t": "float",
     "v": "Private",
     "s": false,
     "p": [
      [
       "y",
       "float"
      ]
     ]
    },
    {
     "n": "Color",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "rgb",
       "int"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "BLLInstalador_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "DIAS_VUELOS_DISPONIBLES",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "NombreBD",
     "t": "string",
     "v": "Public",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "ExisteBaseDatos",
     "t": "bool",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ]
     ]
    },
    {
     "n": "InstalarBaseDatos",
     "t": "void",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ],
      [
       "datosDemostracion",
       "bool"
      ]
     ]
    },
    {
     "n": "InstalarBaseDatosConDatosDePrueba",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ]
     ]
    },
    {
     "n": "InstalarBaseUsuarios",
     "t": "void",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ]
     ]
    },
    {
     "n": "EsBaseUsuarios",
     "t": "bool",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ]
     ]
    },
    {
     "n": "AgregarNegocio",
     "t": "void",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ]
     ]
    },
    {
     "n": "NegocioInstalado",
     "t": "bool",
     "v": "Public",
     "s": true,
     "p": []
    },
    {
     "n": "ObjetosFaltantes",
     "t": "List<string>",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ]
     ]
    },
    {
     "n": "NecesitaActualizacion",
     "t": "bool",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ]
     ]
    },
    {
     "n": "ActualizarBaseDatos",
     "t": "void",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ]
     ]
    },
    {
     "n": "ReinstalarBaseDatos",
     "t": "void",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ],
      [
       "datosDemostracion",
       "bool"
      ]
     ]
    },
    {
     "n": "AsegurarVuelosDisponibles",
     "t": "int",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ]
     ]
    },
    {
     "n": "EjecutarTareasPendientes",
     "t": "void",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ]
     ]
    },
    {
     "n": "ConfigurarConexion",
     "t": "void",
     "v": "Public",
     "s": true,
     "p": [
      [
       "instancia",
       "string"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "BLLPermisos_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "PATENTE_ADMIN_ROLES",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "PATENTE_ADMIN_USUARIOS",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "_dalPatente",
     "t": "DALPatente_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "_dalFamilia",
     "t": "DALFamilia_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "_dalRol",
     "t": "DALRol_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "_bllIntegridad",
     "t": "BLLIntegridad_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "ROLES_DE_SISTEMA",
     "t": "string[]",
     "v": "Private",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "BLLPermisos_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "EsRolDeSistema",
     "t": "bool",
     "v": "Public",
     "s": true,
     "p": [
      [
       "nombre",
       "string"
      ]
     ]
    },
    {
     "n": "EsAdministrador",
     "t": "bool",
     "v": "Private",
     "s": true,
     "p": [
      [
       "tiene",
       "Func<string, bool>"
      ]
     ]
    },
    {
     "n": "QuedaAlgunAdministrador",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dniExcluido",
       "string"
      ],
      [
       "dniConNuevoRol",
       "string"
      ],
      [
       "nuevoRolCodigo",
       "int"
      ],
      [
       "rolSimuladoCodigo",
       "int"
      ],
      [
       "clavesRolSimulado",
       "HashSet<string>"
      ],
      [
       "simularRol",
       "Func<int, HashSet<string>>"
      ]
     ]
    },
    {
     "n": "ClavesRolConFamiliaSimulada",
     "t": "HashSet<string>",
     "v": "Private",
     "s": false,
     "p": [
      [
       "codigoPermiso",
       "int"
      ],
      [
       "codigoPermiso",
       "int"
      ],
      [
       "clavesFamilia",
       "HashSet<string>"
      ]
     ]
    },
    {
     "n": "AgregarClavesSimuladas",
     "t": "void",
     "v": "Private",
     "s": true,
     "p": [
      [
       "componente",
       "IComponentePermiso_GV42"
      ],
      [
       "codigoPermiso",
       "int"
      ],
      [
       "clavesFamilia",
       "HashSet<string>"
      ],
      [
       "destino",
       "HashSet<string>"
      ]
     ]
    },
    {
     "n": "ClavesDePatentes",
     "t": "HashSet<string>",
     "v": "Private",
     "s": false,
     "p": [
      [
       "idsPatentes",
       "IEnumerable<int>"
      ]
     ]
    },
    {
     "n": "ListarPatentes",
     "t": "List<Patente_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarFamilias",
     "t": "List<Familia_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarRoles",
     "t": "List<Rol_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ObtenerArbolFamilia",
     "t": "Familia_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoPermiso",
       "int"
      ]
     ]
    },
    {
     "n": "ObtenerArbolRol",
     "t": "Rol_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoPermiso",
       "int"
      ]
     ]
    },
    {
     "n": "TienePermiso",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoPermiso",
       "int"
      ],
      [
       "dataKey",
       "string"
      ]
     ]
    },
    {
     "n": "CrearFamilia",
     "t": "int",
     "v": "Public",
     "s": false,
     "p": [
      [
       "nombre",
       "string"
      ],
      [
       "idsPatentes",
       "List<int>"
      ],
      [
       "idsSubfamilias",
       "List<int>"
      ]
     ]
    },
    {
     "n": "ModificarFamilia",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoPermiso",
       "int"
      ],
      [
       "nombre",
       "string"
      ],
      [
       "idsPatentes",
       "List<int>"
      ],
      [
       "idsSubfamilias",
       "List<int>"
      ]
     ]
    },
    {
     "n": "EliminarFamilia",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoPermiso",
       "int"
      ]
     ]
    },
    {
     "n": "ValidarRedundanciaPatentesYSubfamilias",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "idsPatentes",
       "List<int>"
      ],
      [
       "idsSubfamilias",
       "List<int>"
      ]
     ]
    },
    {
     "n": "ValidarRedundanciaEnAscendentesFamilia",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "codigoFamiliaModificada",
       "int"
      ],
      [
       "nuevasEfectivas",
       "HashSet<int>"
      ]
     ]
    },
    {
     "n": "BuscarFamiliaConMismasPatentesEfectivas",
     "t": "Familia_GV42",
     "v": "Private",
     "s": false,
     "p": [
      [
       "patentesEfectivas",
       "HashSet<int>"
      ],
      [
       "excluirCodigo",
       "int"
      ]
     ]
    },
    {
     "n": "CrearRol",
     "t": "int",
     "v": "Public",
     "s": false,
     "p": [
      [
       "nombre",
       "string"
      ],
      [
       "idsPatentes",
       "List<int>"
      ],
      [
       "idsFamilias",
       "List<int>"
      ]
     ]
    },
    {
     "n": "ModificarRol",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoPermiso",
       "int"
      ],
      [
       "nombre",
       "string"
      ],
      [
       "idsPatentes",
       "List<int>"
      ],
      [
       "idsFamilias",
       "List<int>"
      ]
     ]
    },
    {
     "n": "EliminarRol",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoPermiso",
       "int"
      ]
     ]
    },
    {
     "n": "ValidarRedundanciaPatentesYFamilias",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "idsPatentes",
       "List<int>"
      ],
      [
       "idsFamilias",
       "List<int>"
      ]
     ]
    },
    {
     "n": "BuscarRolConMismasPatentesEfectivas",
     "t": "Rol_GV42",
     "v": "Private",
     "s": false,
     "p": [
      [
       "patentesEfectivas",
       "HashSet<int>"
      ],
      [
       "excluirCodigo",
       "int"
      ]
     ]
    },
    {
     "n": "RecalcularFamilia",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "codigoPermiso",
       "int"
      ]
     ]
    },
    {
     "n": "RecalcularRol",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "codigoPermiso",
       "int"
      ]
     ]
    },
    {
     "n": "Auditar",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "tipoEvento",
       "string"
      ],
      [
       "detalle",
       "string"
      ],
      [
       "criticidad",
       "string"
      ]
     ]
    },
    {
     "n": "NormalizarNombre",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "nombre",
       "string"
      ],
      [
       "maximo",
       "int"
      ]
     ]
    },
    {
     "n": "MismoNombre",
     "t": "bool",
     "v": "Private",
     "s": true,
     "p": [
      [
       "a",
       "string"
      ],
      [
       "b",
       "string"
      ]
     ]
    },
    {
     "n": "ConstruirPatentesEfectivas",
     "t": "HashSet<int>",
     "v": "Private",
     "s": false,
     "p": [
      [
       "idsPatentes",
       "List<int>"
      ],
      [
       "idsSubfamilias",
       "List<int>"
      ]
     ]
    },
    {
     "n": "ContieneFamiliaRec",
     "t": "bool",
     "v": "Private",
     "s": false,
     "p": [
      [
       "nodo",
       "Familia_GV42"
      ],
      [
       "codigoBuscado",
       "int"
      ]
     ]
    },
    {
     "n": "RolContieneFamiliaRec",
     "t": "bool",
     "v": "Private",
     "s": false,
     "p": [
      [
       "rol",
       "Rol_GV42"
      ],
      [
       "codigoFamiliaBuscada",
       "int"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "BLLUsuario_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "MAX_INTENTOS",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "_DALUsuario",
     "t": "DALUsuario_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "VENTANA_INTENTOS",
     "t": "TimeSpan",
     "v": "Private",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "BLLUsuario_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "IntentarLogin",
     "t": "ResultadoLogin",
     "v": "Public",
     "s": false,
     "p": [
      [
       "login",
       "string"
      ],
      [
       "contrasena",
       "string"
      ]
     ]
    },
    {
     "n": "ListarActivos",
     "t": "List<Usuario_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarTodos",
     "t": "List<Usuario_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarRoles",
     "t": "List<Rol_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "BuscarPorLogin",
     "t": "Usuario_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "login",
       "string"
      ]
     ]
    },
    {
     "n": "ExisteDNI",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ]
     ]
    },
    {
     "n": "CambiarIdioma",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoIdioma",
       "string"
      ]
     ]
    },
    {
     "n": "Desbloquear",
     "t": "string",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ],
      [
       "login",
       "string"
      ]
     ]
    },
    {
     "n": "ActivarDesactivar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ],
      [
       "activo",
       "bool"
      ]
     ]
    },
    {
     "n": "ModificarEmail",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ],
      [
       "email",
       "string"
      ]
     ]
    },
    {
     "n": "ModificarRol",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ],
      [
       "rol",
       "Rol_GV42"
      ]
     ]
    },
    {
     "n": "CrearUsuario",
     "t": "string",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ],
      [
       "apellido",
       "string"
      ],
      [
       "nombre",
       "string"
      ],
      [
       "email",
       "string"
      ],
      [
       "rol",
       "Rol_GV42"
      ]
     ]
    },
    {
     "n": "CrearUsuarioAutogestionado",
     "t": "Usuario_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ],
      [
       "nombre",
       "string"
      ],
      [
       "apellido",
       "string"
      ],
      [
       "email",
       "string"
      ],
      [
       "login",
       "string"
      ],
      [
       "contrasenaPlana",
       "string"
      ]
     ]
    },
    {
     "n": "CambiarContrasena",
     "t": "ResultadoCambioContrasena",
     "v": "Public",
     "s": false,
     "p": [
      [
       "login",
       "string"
      ],
      [
       "contrasenaActual",
       "string"
      ],
      [
       "nuevaContrasena",
       "string"
      ],
      [
       "confirmarContrasena",
       "string"
      ]
     ]
    },
    {
     "n": "CerrarSesión",
     "t": "void",
     "v": "Public",
     "s": true,
     "p": []
    },
    {
     "n": "Auditar",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "login",
       "string"
      ],
      [
       "modulo",
       "string"
      ],
      [
       "tipoEvento",
       "string"
      ],
      [
       "detalle",
       "string"
      ],
      [
       "criticidad",
       "string"
      ]
     ]
    },
    {
     "n": "LoginSesion",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": []
    },
    {
     "n": "ValidarDatosPersonales",
     "t": "void",
     "v": "Private",
     "s": true,
     "p": [
      [
       "dni",
       "string"
      ],
      [
       "nombre",
       "string"
      ],
      [
       "apellido",
       "string"
      ],
      [
       "email",
       "string"
      ]
     ]
    },
    {
     "n": "CalcularNuevosIntentos",
     "t": "int",
     "v": "Private",
     "s": false,
     "p": [
      [
       "usuario",
       "Usuario_GV42"
      ]
     ]
    },
    {
     "n": "CredencialInicial",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "nombre",
       "string"
      ],
      [
       "dni",
       "string"
      ]
     ]
    },
    {
     "n": "VerificarMismaPersonaQuePasajero",
     "t": "void",
     "v": "Private",
     "s": true,
     "p": [
      [
       "dni",
       "string"
      ],
      [
       "nombre",
       "string"
      ],
      [
       "apellido",
       "string"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "GeneradorPdf_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "ANCHO_PAGINA",
     "t": "int",
     "v": "Private",
     "s": true
    },
    {
     "n": "ALTO_PAGINA",
     "t": "int",
     "v": "Private",
     "s": true
    },
    {
     "n": "MARGEN",
     "t": "int",
     "v": "Private",
     "s": true
    },
    {
     "n": "ALTO_FILA",
     "t": "int",
     "v": "Private",
     "s": true
    },
    {
     "n": "FILAS_POR_PAGINA",
     "t": "int",
     "v": "Private",
     "s": true
    },
    {
     "n": "ANCHO_PAGINA_PT",
     "t": "float",
     "v": "Package",
     "s": true
    },
    {
     "n": "ALTO_PAGINA_PT",
     "t": "float",
     "v": "Package",
     "s": true
    },
    {
     "n": "INV",
     "t": "CultureInfo",
     "v": "Private",
     "s": true
    },
    {
     "n": "_buffer",
     "t": "MemoryStream",
     "v": "Private",
     "s": false
    },
    {
     "n": "_offsetsObjetos",
     "t": "List<long>",
     "v": "Private",
     "s": false
    },
    {
     "n": "ANCHOS_HELVETICA",
     "t": "int[]",
     "v": "Private",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "Generar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "ruta",
       "string"
      ],
      [
       "titulo",
       "string"
      ],
      [
       "subtitulo",
       "string"
      ],
      [
       "headers",
       "string[]"
      ],
      [
       "anchosProporcionales",
       "float[]"
      ],
      [
       "filas",
       "List<string[]>"
      ]
     ]
    },
    {
     "n": "GenerarTablaMultilinea",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "ruta",
       "string"
      ],
      [
       "titulo",
       "string"
      ],
      [
       "subtitulos",
       "string[]"
      ],
      [
       "headers",
       "string[]"
      ],
      [
       "anchosProporcionales",
       "float[]"
      ],
      [
       "filas",
       "List<string[]>"
      ],
      [
       "tamanioFuente",
       "float"
      ],
      [
       "alinearDerecha",
       "bool[]"
      ],
      [
       "resumen",
       "string[]"
      ]
     ]
    },
    {
     "n": "Texto",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "sb",
       "StringBuilder"
      ],
      [
       "fuente",
       "string"
      ],
      [
       "tam",
       "float"
      ],
      [
       "gris",
       "float"
      ],
      [
       "x",
       "float"
      ],
      [
       "y",
       "float"
      ],
      [
       "texto",
       "string"
      ]
     ]
    },
    {
     "n": "TextoCelda",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "sb",
       "StringBuilder"
      ],
      [
       "fuente",
       "string"
      ],
      [
       "tam",
       "float"
      ],
      [
       "gris",
       "float"
      ],
      [
       "x",
       "float"
      ],
      [
       "ancho",
       "float"
      ],
      [
       "y",
       "float"
      ],
      [
       "texto",
       "string"
      ],
      [
       "derecha",
       "bool"
      ],
      [
       "negrita",
       "bool"
      ]
     ]
    },
    {
     "n": "Linea",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "sb",
       "StringBuilder"
      ],
      [
       "gris",
       "float"
      ],
      [
       "grosor",
       "float"
      ],
      [
       "x1",
       "float"
      ],
      [
       "x2",
       "float"
      ],
      [
       "y",
       "float"
      ]
     ]
    },
    {
     "n": "Partir",
     "t": "List<string>",
     "v": "Private",
     "s": true,
     "p": [
      [
       "texto",
       "string"
      ],
      [
       "ancho",
       "float"
      ],
      [
       "tam",
       "float"
      ],
      [
       "negrita",
       "bool"
      ]
     ]
    },
    {
     "n": "AnchoTexto",
     "t": "float",
     "v": "Private",
     "s": true,
     "p": [
      [
       "texto",
       "string"
      ],
      [
       "tam",
       "float"
      ],
      [
       "negrita",
       "bool"
      ]
     ]
    },
    {
     "n": "SinAcento",
     "t": "char",
     "v": "Private",
     "s": true,
     "p": [
      [
       "c",
       "char"
      ]
     ]
    },
    {
     "n": "MedirHelvetica",
     "t": "float",
     "v": "Package",
     "s": true,
     "p": [
      [
       "texto",
       "string"
      ],
      [
       "tam",
       "float"
      ],
      [
       "negrita",
       "bool"
      ]
     ]
    },
    {
     "n": "EscaparPdf",
     "t": "string",
     "v": "Package",
     "s": true,
     "p": [
      [
       "s",
       "string"
      ]
     ]
    },
    {
     "n": "EscribirDocumento",
     "t": "void",
     "v": "Package",
     "s": false,
     "p": [
      [
       "ruta",
       "string"
      ],
      [
       "paginas",
       "IList<string>"
      ]
     ]
    },
    {
     "n": "EscribirHeader",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "EscribirObjeto",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "codigo",
       "int"
      ],
      [
       "contenido",
       "string"
      ]
     ]
    },
    {
     "n": "EscribirStreamObjeto",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "codigo",
       "int"
      ],
      [
       "contenidoStream",
       "string"
      ]
     ]
    },
    {
     "n": "EscribirXref",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "EscribirTrailer",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "codigoCatalog",
       "int"
      ],
      [
       "xrefOffset",
       "long"
      ]
     ]
    },
    {
     "n": "EscribirBytes",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "s",
       "string"
      ]
     ]
    },
    {
     "n": "EscaparTexto",
     "t": "string",
     "v": "Private",
     "s": false,
     "p": [
      [
       "s",
       "string"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "BLLIntegridad_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "NOMBRE_BD",
     "t": "string",
     "v": "Private",
     "s": true
    },
    {
     "n": "CARPETA_BACKUPS",
     "t": "string",
     "v": "Private",
     "s": true
    },
    {
     "n": "CANTIDAD_BACKUPS_A_CONSERVAR",
     "t": "int",
     "v": "Private",
     "s": true
    },
    {
     "n": "INTERVALO_BACKUP_HORAS",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "_dal",
     "t": "DALIntegridad_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "_timerBackup",
     "t": "Timer",
     "v": "Private",
     "s": true
    },
    {
     "n": "_lockTimer",
     "t": "object",
     "v": "Private",
     "s": true
    },
    {
     "n": "CONN_MASTER",
     "t": "string",
     "v": "Private",
     "s": true
    },
    {
     "n": "IntegridadConocidamenteRota",
     "t": "bool",
     "v": "Public",
     "s": true
    },
    {
     "n": "CarpetaBackups",
     "t": "string",
     "v": "Public",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "BLLIntegridad_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "Verificar",
     "t": "ResultadoIntegridad",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "Recalcular",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "RecalcularTabla",
     "t": "void",
     "v": "Package",
     "s": false,
     "p": [
      [
       "nombreTabla",
       "string"
      ]
     ]
    },
    {
     "n": "ActualizarRegistro",
     "t": "void",
     "v": "Package",
     "s": false,
     "p": [
      [
       "nombreTabla",
       "string"
      ],
      [
       "codigoRegistro",
       "string"
      ]
     ]
    },
    {
     "n": "ActualizarRegistros",
     "t": "void",
     "v": "Package",
     "s": false,
     "p": [
      [
       "nombreTabla",
       "string"
      ],
      [
       "esAfectado",
       "Func<string, bool>"
      ]
     ]
    },
    {
     "n": "HacerBackupAutomatico",
     "t": "string",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "HacerBackupManual",
     "t": "string",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "RestaurarBackupManual",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "rutaArchivoBak",
       "string"
      ]
     ]
    },
    {
     "n": "ObtenerUltimoBackup",
     "t": "string",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "UltimoBackupDisponible",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": []
    },
    {
     "n": "LimpiarBackupsViejos",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "RestaurarUltimoBackup",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "HacerBackupEnRuta",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "rutaArchivoBak",
       "string"
      ]
     ]
    },
    {
     "n": "RestaurarBackupDesdeRuta",
     "t": "void",
     "v": "Package",
     "s": false,
     "p": [
      [
       "rutaArchivoBak",
       "string"
      ]
     ]
    },
    {
     "n": "IniciarBackupsProgramados",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "DetenerBackupsProgramados",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "DebeHacerBackupAhora",
     "t": "bool",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "EjecutarBackupProgramado",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "state",
       "object"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "ResultadoIntegridad",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "EsIntegra",
     "t": "bool",
     "v": "Public",
     "s": false
    },
    {
     "n": "EsBootstrap",
     "t": "bool",
     "v": "Public",
     "s": false
    },
    {
     "n": "TablasComprometidas",
     "t": "List<string>",
     "v": "Public",
     "s": false
    },
    {
     "n": "Detalles",
     "t": "List<DetalleTampering>",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "TipoTampering",
   "capa": "BLL",
   "padre": null,
   "tipo": "Enumeration",
   "attrs": [
    {
     "n": "Insertado",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Modificado",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Eliminado",
     "t": "",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "DetalleTampering",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "Tabla",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "CodigoRegistro",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Tipo",
     "t": "TipoTampering",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "BLLCheckIn_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "HORAS_APERTURA_CHECKIN",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "MINUTOS_CIERRE_CHECKIN",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "MINUTOS_LIMITE_EMBARQUE",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "KG_POR_EQUIPAJE_EXTRA",
     "t": "decimal",
     "v": "Public",
     "s": true
    },
    {
     "n": "MAX_PESO_POR_BULTO",
     "t": "decimal",
     "v": "Public",
     "s": true
    },
    {
     "n": "PATENTE_MOSTRADOR",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "PATENTE_ONLINE",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "_dalCheckIn",
     "t": "DALCheckIn_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "_dalAsiento",
     "t": "DALAsiento_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "_dalEquipaje",
     "t": "DALEquipaje_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "_dalTarjeta",
     "t": "DALTarjetaEmbarque_GV42",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "BLLCheckIn_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "PuedeAtenderMostrador",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "PuedeHacerCheckInOnline",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ExigirAcceso",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "ci",
       "CheckIn_GV42"
      ]
     ]
    },
    {
     "n": "CanalActual",
     "t": "CanalVenta_GV42",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "DniSesion",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": []
    },
    {
     "n": "ListarPasajeros",
     "t": "List<CheckIn_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroReserva",
       "string"
      ]
     ]
    },
    {
     "n": "IniciarCheckIn",
     "t": "CheckIn_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroReserva",
       "string"
      ],
      [
       "dniPasajero",
       "string"
      ]
     ]
    },
    {
     "n": "IniciarCheckIn",
     "t": "CheckIn_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroReserva",
       "string"
      ],
      [
       "dniPasajero",
       "string"
      ],
      [
       "tramo",
       "int"
      ]
     ]
    },
    {
     "n": "BuscarRealizado",
     "t": "CheckIn_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroReserva",
       "string"
      ],
      [
       "dniPasajero",
       "string"
      ]
     ]
    },
    {
     "n": "BuscarRealizado",
     "t": "CheckIn_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroReserva",
       "string"
      ],
      [
       "dniPasajero",
       "string"
      ],
      [
       "tramo",
       "int"
      ]
     ]
    },
    {
     "n": "MotivoNoDisponible",
     "t": "string",
     "v": "Public",
     "s": false,
     "p": [
      [
       "ci",
       "CheckIn_GV42"
      ]
     ]
    },
    {
     "n": "Buscar",
     "t": "CheckIn_GV42",
     "v": "Private",
     "s": false,
     "p": [
      [
       "numeroReserva",
       "string"
      ],
      [
       "dniPasajero",
       "string"
      ],
      [
       "tramo",
       "int"
      ]
     ]
    },
    {
     "n": "ValidarPuedeHacerCheckIn",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "ci",
       "CheckIn_GV42"
      ]
     ]
    },
    {
     "n": "ValidarVentana",
     "t": "void",
     "v": "Private",
     "s": true,
     "p": [
      [
       "ci",
       "CheckIn_GV42"
      ]
     ]
    },
    {
     "n": "ObtenerPendiente",
     "t": "CheckIn_GV42",
     "v": "Private",
     "s": false,
     "p": [
      [
       "codigoCheckIn",
       "int"
      ]
     ]
    },
    {
     "n": "ObtenerFranquicia",
     "t": "FranquiciaEquipaje_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoCheckIn",
       "int"
      ]
     ]
    },
    {
     "n": "CalcularFranquicia",
     "t": "FranquiciaEquipaje_GV42",
     "v": "Private",
     "s": true,
     "p": [
      [
       "ci",
       "CheckIn_GV42"
      ]
     ]
    },
    {
     "n": "CalcularCargoExceso",
     "t": "CargoExcesoEquipaje_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoCheckIn",
       "int"
      ],
      [
       "pesosPorBulto",
       "IList<decimal>"
      ]
     ]
    },
    {
     "n": "CalcularCargo",
     "t": "CargoExcesoEquipaje_GV42",
     "v": "Private",
     "s": true,
     "p": [
      [
       "f",
       "FranquiciaEquipaje_GV42"
      ],
      [
       "pesos",
       "IList<decimal>"
      ],
      [
       "claseTexto",
       "string"
      ]
     ]
    },
    {
     "n": "RegistrarEquipaje",
     "t": "Equipaje_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoCheckIn",
       "int"
      ],
      [
       "pesosPorBulto",
       "IList<decimal>"
      ],
      [
       "medioCobro",
       "MedioPago_GV42?"
      ],
      [
       "numeroOperacion",
       "string"
      ],
      [
       "tarjeta",
       "DatosTarjeta_GV42"
      ]
     ]
    },
    {
     "n": "NumeroTransaccionCobro",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "medio",
       "MedioPago_GV42"
      ],
      [
       "numeroOperacion",
       "string"
      ],
      [
       "tarjeta",
       "DatosTarjeta_GV42"
      ]
     ]
    },
    {
     "n": "ObtenerParaEquipaje",
     "t": "CheckIn_GV42",
     "v": "Private",
     "s": false,
     "p": [
      [
       "codigoCheckIn",
       "int"
      ]
     ]
    },
    {
     "n": "PuedeDespacharPosterior",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": [
      [
       "ci",
       "CheckIn_GV42"
      ]
     ]
    },
    {
     "n": "ObtenerMapaAsientos",
     "t": "List<AsientoDisponibilidad_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoCheckIn",
       "int"
      ]
     ]
    },
    {
     "n": "PuedeElegirPreferencial",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": [
      [
       "ci",
       "CheckIn_GV42"
      ]
     ]
    },
    {
     "n": "PuedeElegirAsiento",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": [
      [
       "ci",
       "CheckIn_GV42"
      ]
     ]
    },
    {
     "n": "ValidarAsiento",
     "t": "Asiento_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoCheckIn",
       "int"
      ]
     ]
    },
    {
     "n": "CambiarAsiento",
     "t": "Asiento_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoCheckIn",
       "int"
      ],
      [
       "codigoAsientoNuevo",
       "int"
      ]
     ]
    },
    {
     "n": "ConfirmarCheckIn",
     "t": "CheckIn_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoCheckIn",
       "int"
      ]
     ]
    },
    {
     "n": "Obtener",
     "t": "CheckIn_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoCheckIn",
       "int"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "BLLDocumentosCheckIn_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "ANCHO_PAGINA",
     "t": "float",
     "v": "Public",
     "s": true
    },
    {
     "n": "ALTO_PAGINA",
     "t": "float",
     "v": "Public",
     "s": true
    },
    {
     "n": "ETIQUETAS_POR_FILA",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "FILAS_POR_PAGINA_SIGUIENTE",
     "t": "int",
     "v": "Private",
     "s": true
    },
    {
     "n": "MARGEN_SUPERIOR",
     "t": "float",
     "v": "Private",
     "s": true
    },
    {
     "n": "SEPARACION_ETIQUETAS",
     "t": "float",
     "v": "Private",
     "s": true
    },
    {
     "n": "SEPARACION_CORTE",
     "t": "float",
     "v": "Private",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "CantidadPaginas",
     "t": "int",
     "v": "Public",
     "s": true,
     "p": [
      [
       "ci",
       "CheckIn_GV42"
      ]
     ]
    },
    {
     "n": "DibujarPagina",
     "t": "void",
     "v": "Public",
     "s": true,
     "p": [
      [
       "l",
       "ILienzo_GV42"
      ],
      [
       "ci",
       "CheckIn_GV42"
      ],
      [
       "pagina",
       "int"
      ],
      [
       "x0",
       "float"
      ],
      [
       "y0",
       "float"
      ]
     ]
    },
    {
     "n": "ExportarPdf",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "ruta",
       "string"
      ],
      [
       "ci",
       "CheckIn_GV42"
      ]
     ]
    },
    {
     "n": "CantidadEtiquetas",
     "t": "int",
     "v": "Private",
     "s": true,
     "p": [
      [
       "ci",
       "CheckIn_GV42"
      ]
     ]
    },
    {
     "n": "DibujarFila",
     "t": "void",
     "v": "Private",
     "s": true,
     "p": [
      [
       "l",
       "ILienzo_GV42"
      ],
      [
       "ci",
       "CheckIn_GV42"
      ],
      [
       "desde",
       "int"
      ],
      [
       "cantidad",
       "int"
      ],
      [
       "x0",
       "float"
      ],
      [
       "y",
       "float"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "DibujoDocumento_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "AZUL",
     "t": "int",
     "v": "Package",
     "s": true
    },
    {
     "n": "AZUL_OSCURO",
     "t": "int",
     "v": "Package",
     "s": true
    },
    {
     "n": "CELESTE",
     "t": "int",
     "v": "Package",
     "s": true
    },
    {
     "n": "CELESTE_BORDE",
     "t": "int",
     "v": "Package",
     "s": true
    },
    {
     "n": "TEXTO",
     "t": "int",
     "v": "Package",
     "s": true
    },
    {
     "n": "GRIS",
     "t": "int",
     "v": "Package",
     "s": true
    },
    {
     "n": "GRIS_CLARO",
     "t": "int",
     "v": "Package",
     "s": true
    },
    {
     "n": "BLANCO",
     "t": "int",
     "v": "Package",
     "s": true
    },
    {
     "n": "VERDE",
     "t": "int",
     "v": "Package",
     "s": true
    },
    {
     "n": "NARANJA",
     "t": "int",
     "v": "Package",
     "s": true
    },
    {
     "n": "Cultura",
     "t": "CultureInfo",
     "v": "Package",
     "s": true
    },
    {
     "n": "CulturaIngles",
     "t": "CultureInfo",
     "v": "Private",
     "s": true
    },
    {
     "n": "CulturaFechas",
     "t": "CultureInfo",
     "v": "Package",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "Campo",
     "t": "void",
     "v": "Package",
     "s": true,
     "p": [
      [
       "l",
       "ILienzo_GV42"
      ],
      [
       "x",
       "float"
      ],
      [
       "y",
       "float"
      ],
      [
       "etiqueta",
       "string"
      ],
      [
       "valor",
       "string"
      ],
      [
       "tamValor",
       "float"
      ],
      [
       "anchoMax",
       "float"
      ],
      [
       "color",
       "int"
      ],
      [
       "colorEtiqueta",
       "int"
      ]
     ]
    },
    {
     "n": "TextoAjustado",
     "t": "void",
     "v": "Package",
     "s": true,
     "p": [
      [
       "l",
       "ILienzo_GV42"
      ],
      [
       "x",
       "float"
      ],
      [
       "y",
       "float"
      ],
      [
       "texto",
       "string"
      ],
      [
       "tam",
       "float"
      ],
      [
       "negrita",
       "bool"
      ],
      [
       "color",
       "int"
      ],
      [
       "anchoMax",
       "float"
      ]
     ]
    },
    {
     "n": "TextoDerecha",
     "t": "void",
     "v": "Package",
     "s": true,
     "p": [
      [
       "l",
       "ILienzo_GV42"
      ],
      [
       "xDer",
       "float"
      ],
      [
       "y",
       "float"
      ],
      [
       "texto",
       "string"
      ],
      [
       "tam",
       "float"
      ],
      [
       "negrita",
       "bool"
      ],
      [
       "color",
       "int"
      ],
      [
       "anchoMax",
       "float"
      ]
     ]
    },
    {
     "n": "TextoCentrado",
     "t": "void",
     "v": "Package",
     "s": true,
     "p": [
      [
       "l",
       "ILienzo_GV42"
      ],
      [
       "xCentro",
       "float"
      ],
      [
       "y",
       "float"
      ],
      [
       "texto",
       "string"
      ],
      [
       "tam",
       "float"
      ],
      [
       "negrita",
       "bool"
      ],
      [
       "color",
       "int"
      ],
      [
       "anchoMax",
       "float"
      ]
     ]
    },
    {
     "n": "Pastilla",
     "t": "float",
     "v": "Package",
     "s": true,
     "p": [
      [
       "l",
       "ILienzo_GV42"
      ],
      [
       "x",
       "float"
      ],
      [
       "y",
       "float"
      ],
      [
       "texto",
       "string"
      ],
      [
       "tam",
       "float"
      ],
      [
       "fondo",
       "int"
      ],
      [
       "color",
       "int"
      ]
     ]
    },
    {
     "n": "AjustarTamanio",
     "t": "float",
     "v": "Private",
     "s": true,
     "p": [
      [
       "l",
       "ILienzo_GV42"
      ],
      [
       "texto",
       "string"
      ],
      [
       "tam",
       "float"
      ],
      [
       "negrita",
       "bool"
      ],
      [
       "anchoMax",
       "float"
      ]
     ]
    },
    {
     "n": "Avion",
     "t": "void",
     "v": "Package",
     "s": true,
     "p": [
      [
       "l",
       "ILienzo_GV42"
      ],
      [
       "cx",
       "float"
      ],
      [
       "cy",
       "float"
      ],
      [
       "color",
       "int"
      ],
      [
       "escala",
       "float"
      ]
     ]
    },
    {
     "n": "OrigenIata",
     "t": "string",
     "v": "Package",
     "s": true,
     "p": [
      [
       "ci",
       "CheckIn_GV42"
      ]
     ]
    },
    {
     "n": "DestinoIata",
     "t": "string",
     "v": "Package",
     "s": true,
     "p": [
      [
       "ci",
       "CheckIn_GV42"
      ]
     ]
    },
    {
     "n": "OrigenCiudad",
     "t": "string",
     "v": "Package",
     "s": true,
     "p": [
      [
       "ci",
       "CheckIn_GV42"
      ]
     ]
    },
    {
     "n": "DestinoCiudad",
     "t": "string",
     "v": "Package",
     "s": true,
     "p": [
      [
       "ci",
       "CheckIn_GV42"
      ]
     ]
    },
    {
     "n": "Aerolinea",
     "t": "string",
     "v": "Package",
     "s": true,
     "p": [
      [
       "ci",
       "CheckIn_GV42"
      ]
     ]
    },
    {
     "n": "Fecha",
     "t": "string",
     "v": "Package",
     "s": true,
     "p": [
      [
       "f",
       "DateTime"
      ]
     ]
    },
    {
     "n": "FechaCorta",
     "t": "string",
     "v": "Package",
     "s": true,
     "p": [
      [
       "f",
       "DateTime"
      ]
     ]
    },
    {
     "n": "Kilos",
     "t": "string",
     "v": "Package",
     "s": true,
     "p": [
      [
       "kg",
       "decimal"
      ]
     ]
    },
    {
     "n": "Dinero",
     "t": "string",
     "v": "Package",
     "s": true,
     "p": [
      [
       "importe",
       "decimal"
      ]
     ]
    },
    {
     "n": "IataDeTexto",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "texto",
       "string"
      ]
     ]
    },
    {
     "n": "CiudadDeTexto",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "texto",
       "string"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "DisenioEtiquetaEquipaje_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "ANCHO",
     "t": "float",
     "v": "Public",
     "s": true
    },
    {
     "n": "ALTO",
     "t": "float",
     "v": "Public",
     "s": true
    },
    {
     "n": "ALTO_ENCABEZADO",
     "t": "float",
     "v": "Private",
     "s": true
    },
    {
     "n": "MARGEN",
     "t": "float",
     "v": "Private",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "Dibujar",
     "t": "void",
     "v": "Public",
     "s": true,
     "p": [
      [
       "l",
       "ILienzo_GV42"
      ],
      [
       "ci",
       "CheckIn_GV42"
      ],
      [
       "indice",
       "int"
      ],
      [
       "x0",
       "float"
      ],
      [
       "y0",
       "float"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "DisenioTarjetaEmbarque_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "ANCHO",
     "t": "float",
     "v": "Public",
     "s": true
    },
    {
     "n": "ALTO",
     "t": "float",
     "v": "Public",
     "s": true
    },
    {
     "n": "ANCHO_PRINCIPAL",
     "t": "float",
     "v": "Private",
     "s": true
    },
    {
     "n": "ALTO_ENCABEZADO",
     "t": "float",
     "v": "Private",
     "s": true
    },
    {
     "n": "Y_DESTACADOS",
     "t": "float",
     "v": "Private",
     "s": true
    },
    {
     "n": "ALTO_DESTACADO",
     "t": "float",
     "v": "Private",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "Dibujar",
     "t": "void",
     "v": "Public",
     "s": true,
     "p": [
      [
       "l",
       "ILienzo_GV42"
      ],
      [
       "ci",
       "CheckIn_GV42"
      ],
      [
       "x0",
       "float"
      ],
      [
       "y0",
       "float"
      ]
     ]
    },
    {
     "n": "TextoCanal",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "ci",
       "CheckIn_GV42"
      ]
     ]
    },
    {
     "n": "CodigoOrigen",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "ci",
       "CheckIn_GV42"
      ]
     ]
    },
    {
     "n": "CodigoDestino",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "ci",
       "CheckIn_GV42"
      ]
     ]
    },
    {
     "n": "TextoBultos",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "e",
       "Equipaje_GV42"
      ]
     ]
    },
    {
     "n": "Destacado",
     "t": "void",
     "v": "Private",
     "s": true,
     "p": [
      [
       "l",
       "ILienzo_GV42"
      ],
      [
       "x",
       "float"
      ],
      [
       "y",
       "float"
      ],
      [
       "ancho",
       "float"
      ],
      [
       "etiqueta",
       "string"
      ],
      [
       "valor",
       "string"
      ],
      [
       "tamValor",
       "float"
      ],
      [
       "borde",
       "int?"
      ],
      [
       "resaltado",
       "bool"
      ]
     ]
    },
    {
     "n": "ResumenEquipaje",
     "t": "void",
     "v": "Private",
     "s": true,
     "p": [
      [
       "l",
       "ILienzo_GV42"
      ],
      [
       "e",
       "Equipaje_GV42"
      ],
      [
       "x",
       "float"
      ],
      [
       "y",
       "float"
      ],
      [
       "anchoMax",
       "float"
      ]
     ]
    },
    {
     "n": "AnchoPartes",
     "t": "float",
     "v": "Private",
     "s": true,
     "p": [
      [
       "l",
       "ILienzo_GV42"
      ],
      [
       "partes",
       "List<KeyValuePair<string, int>>"
      ],
      [
       "separador",
       "string"
      ],
      [
       "tam",
       "float"
      ]
     ]
    },
    {
     "n": "CanalCorto",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "ci",
       "CheckIn_GV42"
      ]
     ]
    },
    {
     "n": "TextoUbicacion",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "ci",
       "CheckIn_GV42"
      ]
     ]
    },
    {
     "n": "NombreAeropuerto",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "ci",
       "CheckIn_GV42"
      ],
      [
       "origen",
       "bool"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "BLLNegocioUtil_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "MODULO_RESERVAS",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "MODULO_CHECKIN",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "MODULO_VUELOS",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "Cultura",
     "t": "CultureInfo",
     "v": "Private",
     "s": true
    },
    {
     "n": "_generadorAutorizacion",
     "t": "Random",
     "v": "Private",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "LoginActual",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": []
    },
    {
     "n": "TienePatente",
     "t": "bool",
     "v": "Public",
     "s": true,
     "p": [
      [
       "dataKey",
       "string"
      ]
     ]
    },
    {
     "n": "ExigirPatente",
     "t": "void",
     "v": "Public",
     "s": true,
     "p": [
      [
       "dataKey",
       "string"
      ],
      [
       "mensaje",
       "string"
      ]
     ]
    },
    {
     "n": "Auditar",
     "t": "void",
     "v": "Public",
     "s": true,
     "p": [
      [
       "modulo",
       "string"
      ],
      [
       "tipoEvento",
       "string"
      ],
      [
       "detalle",
       "string"
      ],
      [
       "criticidad",
       "string"
      ]
     ]
    },
    {
     "n": "AutorizarTarjeta",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "tarjeta",
       "DatosTarjeta_GV42"
      ],
      [
       "medioPago",
       "MedioPago_GV42"
      ]
     ]
    },
    {
     "n": "Dinero",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "importe",
       "decimal"
      ]
     ]
    },
    {
     "n": "NombreAdicional",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "nombre",
       "string"
      ]
     ]
    },
    {
     "n": "ValidarPersona",
     "t": "void",
     "v": "Public",
     "s": true,
     "p": [
      [
       "p",
       "Persona_GV42"
      ],
      [
       "rol",
       "string"
      ],
      [
       "contactoObligatorio",
       "bool"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "BLLReserva_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "TASA_IMPUESTOS",
     "t": "decimal",
     "v": "Public",
     "s": true
    },
    {
     "n": "MAX_PASAJEROS_POR_RESERVA",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "MAX_RESERVAS_PENDIENTES_AUTOGESTION",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "MAX_CANTIDAD_ADICIONAL",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "MAX_COSTO_ADICIONAL",
     "t": "decimal",
     "v": "Public",
     "s": true
    },
    {
     "n": "HORAS_SIN_PENALIDAD",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "HORAS_PENALIDAD_PARCIAL",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "PORCENTAJE_PENALIDAD_PARCIAL",
     "t": "decimal",
     "v": "Public",
     "s": true
    },
    {
     "n": "PORCENTAJE_PENALIDAD_TOTAL",
     "t": "decimal",
     "v": "Public",
     "s": true
    },
    {
     "n": "MINUTOS_VENCIMIENTO_AUTOGESTION",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "HORAS_VENCIMIENTO_PRESENCIAL",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "DESCUENTO_NINO",
     "t": "decimal",
     "v": "Public",
     "s": true
    },
    {
     "n": "PORCENTAJE_TARIFA_INFANTE",
     "t": "decimal",
     "v": "Public",
     "s": true
    },
    {
     "n": "HORAS_LIMITE_CAMBIO",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "DIAS_FECHAS_FLEXIBLES",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "_dalAeropuerto",
     "t": "DALAeropuerto_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "_dalTipoAdicional",
     "t": "DALTipoAdicional_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "_dalVuelo",
     "t": "DALVuelo_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "_dalPasajero",
     "t": "DALPasajero_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "_dalReserva",
     "t": "DALReserva_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "_dalPago",
     "t": "DALPago_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "_dalBoleto",
     "t": "DALBoleto_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "_dalAsiento",
     "t": "DALAsiento_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "_dalTarifa",
     "t": "DALTarifa_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "_bllIntegridad",
     "t": "BLLIntegridad_GV42",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "BLLReserva_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarAeropuertos",
     "t": "List<Aeropuerto_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarTiposAdicional",
     "t": "List<TipoAdicional_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ObtenerServicioEquipajeExtra",
     "t": "TipoAdicional_GV42",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarTarifas",
     "t": "List<TarifaFamilia_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ObtenerServicioSeleccionAsiento",
     "t": "TipoAdicional_GV42",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "FactorTarifa",
     "t": "decimal",
     "v": "Public",
     "s": true,
     "p": [
      [
       "tipo",
       "TipoPasajero_GV42"
      ]
     ]
    },
    {
     "n": "ObtenerServicioAsientoPreferencial",
     "t": "TipoAdicional_GV42",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "BuscarVuelosDisponibles",
     "t": "List<VueloClase_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "criterio",
       "CriterioBusquedaVuelo_GV42"
      ]
     ]
    },
    {
     "n": "PreciosPorFecha",
     "t": "List<PrecioFecha_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "criterio",
       "CriterioBusquedaVuelo_GV42"
      ],
      [
       "desdeMinimo",
       "DateTime?"
      ]
     ]
    },
    {
     "n": "BuscarVuelosDeRegreso",
     "t": "List<VueloClase_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "ida",
       "VueloClase_GV42"
      ],
      [
       "fechaRegreso",
       "DateTime"
      ],
      [
       "cantidadPasajeros",
       "int"
      ],
      [
       "clase",
       "ClaseVuelo_GV42?"
      ]
     ]
    },
    {
     "n": "ValidarCriterio",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "c",
       "CriterioBusquedaVuelo_GV42"
      ]
     ]
    },
    {
     "n": "BuscarPasajero",
     "t": "Pasajero_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ]
     ]
    },
    {
     "n": "PrecargarDesdeUsuario",
     "t": "Pasajero_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ]
     ]
    },
    {
     "n": "ExigirVendedorParaBuscarPersonas",
     "t": "void",
     "v": "Private",
     "s": true,
     "p": []
    },
    {
     "n": "PersonaRegistrada",
     "t": "Pasajero_GV42",
     "v": "Private",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ],
      [
       "esPasajero",
       "bool"
      ]
     ]
    },
    {
     "n": "VerificarIdentidad",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "p",
       "Persona_GV42"
      ],
      [
       "rol",
       "string"
      ],
      [
       "mostrarRegistrado",
       "bool"
      ]
     ]
    },
    {
     "n": "RegistrarPasajero",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "pasajero",
       "Pasajero_GV42"
      ]
     ]
    },
    {
     "n": "RegistrarClienteAutogestionado",
     "t": "Usuario_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "cliente",
       "Pasajero_GV42"
      ],
      [
       "login",
       "string"
      ],
      [
       "contrasenaPlana",
       "string"
      ],
      [
       "confirmarContrasena",
       "string"
      ]
     ]
    },
    {
     "n": "PatentesActuales",
     "t": "HashSet<string>",
     "v": "Private",
     "s": true,
     "p": []
    },
    {
     "n": "DniSesion",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": []
    },
    {
     "n": "EsDeLaSesion",
     "t": "bool",
     "v": "Private",
     "s": true,
     "p": [
      [
       "r",
       "Reserva_GV42"
      ]
     ]
    },
    {
     "n": "EsPasajeroDeLaSesion",
     "t": "bool",
     "v": "Private",
     "s": true,
     "p": [
      [
       "r",
       "Reserva_GV42"
      ]
     ]
    },
    {
     "n": "EsTitularEnSesion",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": [
      [
       "r",
       "Reserva_GV42"
      ]
     ]
    },
    {
     "n": "DniSiEsSoloPasajero",
     "t": "string",
     "v": "Public",
     "s": false,
     "p": [
      [
       "r",
       "Reserva_GV42"
      ]
     ]
    },
    {
     "n": "PuedeGenerarParaTerceros",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "PuedeConsultarTodas",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "PuedeRegistrarPagoDeTerceros",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "PuedeCancelar",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "CanalSegunSesion",
     "t": "CanalVenta_GV42",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "ObtenerMapaAsientos",
     "t": "List<AsientoDisponibilidad_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroVuelo",
       "int"
      ],
      [
       "clase",
       "ClaseVuelo_GV42"
      ]
     ]
    },
    {
     "n": "ObtenerTitularDeSesion",
     "t": "Pasajero_GV42",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "GenerarReserva",
     "t": "Reserva_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "borrador",
       "Reserva_GV42"
      ]
     ]
    },
    {
     "n": "ExigirPasajerosNoRepetidosEnVuelo",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "vuelo",
       "Vuelo_GV42"
      ],
      [
       "pasajeros",
       "IEnumerable<Pasajero_GV42>"
      ],
      [
       "codigoReservaExcluir",
       "int"
      ]
     ]
    },
    {
     "n": "CalcularTarifaTramo",
     "t": "decimal",
     "v": "Public",
     "s": true,
     "p": [
      [
       "tarifa",
       "TarifaFamilia_GV42"
      ],
      [
       "vc",
       "VueloClase_GV42"
      ],
      [
       "pasajeros",
       "IEnumerable<Pasajero_GV42>"
      ]
     ]
    },
    {
     "n": "ValidarAsientoDeVuelo",
     "t": "Asiento_GV42",
     "v": "Private",
     "s": false,
     "p": [
      [
       "elegido",
       "Asiento_GV42"
      ],
      [
       "vc",
       "VueloClase_GV42"
      ]
     ]
    },
    {
     "n": "ValidarAsientos",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "borrador",
       "Reserva_GV42"
      ],
      [
       "vc",
       "VueloClase_GV42"
      ],
      [
       "tramo",
       "int"
      ]
     ]
    },
    {
     "n": "AgregarSeleccionAsiento",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "borrador",
       "Reserva_GV42"
      ],
      [
       "tramo",
       "int"
      ]
     ]
    },
    {
     "n": "AgregarRecargoPreferencial",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "borrador",
       "Reserva_GV42"
      ],
      [
       "tramo",
       "int"
      ]
     ]
    },
    {
     "n": "AgregarEquipajeExtra",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "borrador",
       "Reserva_GV42"
      ],
      [
       "tramo",
       "int"
      ]
     ]
    },
    {
     "n": "ValidarPasajerosParaReserva",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "pasajeros",
       "List<Pasajero_GV42>"
      ],
      [
       "fechaVuelo",
       "DateTime"
      ]
     ]
    },
    {
     "n": "MaximoAdicional",
     "t": "int",
     "v": "Public",
     "s": false,
     "p": [
      [
       "tipo",
       "TipoAdicional_GV42"
      ],
      [
       "cantidadPasajeros",
       "int"
      ]
     ]
    },
    {
     "n": "ValidarAdicionales",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "adicionales",
       "List<AdicionalReserva_GV42>"
      ],
      [
       "canal",
       "CanalVenta_GV42"
      ],
      [
       "cantidadPasajeros",
       "int"
      ]
     ]
    },
    {
     "n": "BuscarReserva",
     "t": "Reserva_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroReserva",
       "string"
      ]
     ]
    },
    {
     "n": "ListarMisReservas",
     "t": "List<Reserva_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "BuscarReservas",
     "t": "List<Reserva_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "textoLibre",
       "string"
      ]
     ]
    },
    {
     "n": "ObtenerBoletos",
     "t": "List<Boleto_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroReserva",
       "string"
      ]
     ]
    },
    {
     "n": "RegistrarPago",
     "t": "Pago_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroReserva",
       "string"
      ],
      [
       "medioPago",
       "MedioPago_GV42"
      ],
      [
       "importeAbonado",
       "decimal"
      ],
      [
       "numeroTransaccion",
       "string"
      ]
     ]
    },
    {
     "n": "RegistrarPago",
     "t": "Pago_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroReserva",
       "string"
      ],
      [
       "medioPago",
       "MedioPago_GV42"
      ],
      [
       "importeAbonado",
       "decimal"
      ],
      [
       "numeroTransaccion",
       "string"
      ],
      [
       "tarjeta",
       "DatosTarjeta_GV42"
      ]
     ]
    },
    {
     "n": "CalcularPorcentajePenalidad",
     "t": "decimal",
     "v": "Public",
     "s": false,
     "p": [
      [
       "reserva",
       "Reserva_GV42"
      ]
     ]
    },
    {
     "n": "CalcularPorcentajePenalidad",
     "t": "decimal",
     "v": "Public",
     "s": false,
     "p": [
      [
       "fechaHoraSalida",
       "DateTime"
      ]
     ]
    },
    {
     "n": "CancelarReserva",
     "t": "Reserva_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroReserva",
       "string"
      ]
     ]
    },
    {
     "n": "PuedeProcesarReembolsos",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ProcesarReembolso",
     "t": "Reembolso_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroReserva",
       "string"
      ]
     ]
    },
    {
     "n": "PuedeCambiarVuelo",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "MotivoNoSePuedeCambiar",
     "t": "string",
     "v": "Public",
     "s": false,
     "p": [
      [
       "reserva",
       "Reserva_GV42"
      ],
      [
       "tramo",
       "int"
      ]
     ]
    },
    {
     "n": "ValidarPuedeCambiar",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "reserva",
       "Reserva_GV42"
      ],
      [
       "tramo",
       "int"
      ]
     ]
    },
    {
     "n": "EsCoherenteConOtroTramo",
     "t": "bool",
     "v": "Private",
     "s": true,
     "p": [
      [
       "reserva",
       "Reserva_GV42"
      ],
      [
       "tramo",
       "int"
      ],
      [
       "nuevo",
       "Vuelo_GV42"
      ]
     ]
    },
    {
     "n": "BuscarVuelosParaCambio",
     "t": "List<VueloClase_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroReserva",
       "string"
      ],
      [
       "tramo",
       "int"
      ],
      [
       "fecha",
       "DateTime"
      ],
      [
       "clase",
       "ClaseVuelo_GV42?"
      ]
     ]
    },
    {
     "n": "CotizarCambio",
     "t": "CotizacionCambio_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroReserva",
       "string"
      ],
      [
       "tramo",
       "int"
      ],
      [
       "codigoVueloNuevo",
       "int"
      ],
      [
       "claseNueva",
       "ClaseVuelo_GV42"
      ]
     ]
    },
    {
     "n": "Cotizar",
     "t": "CotizacionCambio_GV42",
     "v": "Private",
     "s": true,
     "p": [
      [
       "reserva",
       "Reserva_GV42"
      ],
      [
       "tramo",
       "int"
      ],
      [
       "nuevo",
       "VueloClase_GV42"
      ]
     ]
    },
    {
     "n": "CambioRequiereAsientos",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": [
      [
       "reserva",
       "Reserva_GV42"
      ],
      [
       "tramo",
       "int"
      ]
     ]
    },
    {
     "n": "CambiarVuelo",
     "t": "CotizacionCambio_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroReserva",
       "string"
      ],
      [
       "tramo",
       "int"
      ],
      [
       "codigoVueloNuevo",
       "int"
      ],
      [
       "claseNueva",
       "ClaseVuelo_GV42"
      ],
      [
       "asientos",
       "List<AsientoPasajero_GV42>"
      ],
      [
       "medioPago",
       "MedioPago_GV42?"
      ],
      [
       "numeroOperacion",
       "string"
      ],
      [
       "tarjeta",
       "DatosTarjeta_GV42"
      ]
     ]
    },
    {
     "n": "AplicarVencimientos",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "RecalcularIntegridad",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "tabla",
       "string"
      ],
      [
       "codigo",
       "int"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "TemaAyuda_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "CodigoTemaAyuda",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "CodigoPadre",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Formularios",
     "t": "string[]",
     "v": "Public",
     "s": false
    },
    {
     "n": "Titulo",
     "t": "string",
     "v": "Public",
     "s": false
    },
    {
     "n": "Texto",
     "t": "string",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "BLLAyuda_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "TEMA_INICIAL",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "_temas",
     "t": "List<TemaAyuda_GV42>",
     "v": "Private",
     "s": true
    }
   ],
   "metodos": [
    {
     "n": "Temas",
     "t": "List<TemaAyuda_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "Hijos",
     "t": "List<TemaAyuda_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoPadre",
       "string"
      ]
     ]
    },
    {
     "n": "TemaDeFormulario",
     "t": "string",
     "v": "Public",
     "s": false,
     "p": [
      [
       "nombreClase",
       "string"
      ]
     ]
    },
    {
     "n": "ExportarPdf",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "ruta",
       "string"
      ]
     ]
    },
    {
     "n": "Tema",
     "t": "TemaAyuda_GV42",
     "v": "Private",
     "s": true,
     "p": [
      [
       "codigo",
       "string"
      ],
      [
       "codigoPadre",
       "string"
      ],
      [
       "formularios",
       "string[]"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "BLLReinstalador_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "PATENTE",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "Instancia",
     "t": "string",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "BaseAccesible",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ValidarAdministrador",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "login",
       "string"
      ],
      [
       "contrasena",
       "string"
      ]
     ]
    },
    {
     "n": "RestaurarBackup",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "rutaArchivoBak",
       "string"
      ]
     ]
    },
    {
     "n": "ReinstalarLimpia",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "datosDemostracion",
       "bool"
      ]
     ]
    },
    {
     "n": "DejarLista",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": []
    }
   ]
  },
  {
   "nombre": "BLLReporteCheckIn_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "PATENTE_VER",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "PATENTE_EXPORTAR",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "EVENTO_CONSULTA",
     "t": "string",
     "v": "Private",
     "s": true
    },
    {
     "n": "EVENTO_PDF",
     "t": "string",
     "v": "Private",
     "s": true
    },
    {
     "n": "_dal",
     "t": "DALReporteCheckIn_GV42",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "PuedeVer",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "PuedeExportar",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "Generar",
     "t": "List<ReporteCheckIn_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "filtro",
       "FiltroReporteCheckIn_GV42"
      ]
     ]
    },
    {
     "n": "Completar",
     "t": "void",
     "v": "Public",
     "s": true,
     "p": [
      [
       "filas",
       "List<ReporteCheckIn_GV42>"
      ],
      [
       "ahora",
       "DateTime"
      ]
     ]
    },
    {
     "n": "CalcularAsistencia",
     "t": "EstadoAsistencia_GV42",
     "v": "Public",
     "s": true,
     "p": [
      [
       "estado",
       "EstadoCheckIn_GV42"
      ],
      [
       "salida",
       "DateTime"
      ],
      [
       "ahora",
       "DateTime"
      ]
     ]
    },
    {
     "n": "ExportarPdf",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "ruta",
       "string"
      ],
      [
       "filas",
       "List<ReporteCheckIn_GV42>"
      ],
      [
       "filtro",
       "FiltroReporteCheckIn_GV42"
      ]
     ]
    },
    {
     "n": "Resumen",
     "t": "List<string>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "filas",
       "List<ReporteCheckIn_GV42>"
      ]
     ]
    },
    {
     "n": "TextoUbicacion",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "ubicacion",
       "string"
      ]
     ]
    },
    {
     "n": "Normalizar",
     "t": "FiltroReporteCheckIn_GV42",
     "v": "Private",
     "s": true,
     "p": [
      [
       "f",
       "FiltroReporteCheckIn_GV42"
      ]
     ]
    },
    {
     "n": "DescripcionFiltro",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "f",
       "FiltroReporteCheckIn_GV42"
      ]
     ]
    },
    {
     "n": "Guion",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "texto",
       "string"
      ]
     ]
    },
    {
     "n": "LoginSeguro",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": []
    },
    {
     "n": "Recortar",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "s",
       "string"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "BLLReporteMillas_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "PATENTE_VER",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "PATENTE_EXPORTAR",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "MILLAS_NACIONAL",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "MILLAS_INTERNACIONAL",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "MESES_VIGENCIA",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "DIAS_PROYECCION",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "DIAS_MISMO_VIAJE",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "DIAS_MAX_ESTIMACION",
     "t": "int",
     "v": "Private",
     "s": true
    },
    {
     "n": "UMBRALES",
     "t": "int[]",
     "v": "Private",
     "s": true
    },
    {
     "n": "EVENTO_CONSULTA",
     "t": "string",
     "v": "Private",
     "s": true
    },
    {
     "n": "EVENTO_PDF",
     "t": "string",
     "v": "Private",
     "s": true
    },
    {
     "n": "_dal",
     "t": "DALReporteMillas_GV42",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "PuedeVer",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "PuedeExportar",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "Generar",
     "t": "List<ReporteMillas_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "filtro",
       "FiltroReporteMillas_GV42"
      ]
     ]
    },
    {
     "n": "Calcular",
     "t": "List<ReporteMillas_GV42>",
     "v": "Public",
     "s": true,
     "p": [
      [
       "vuelos",
       "List<VueloVolado_GV42>"
      ],
      [
       "filtro",
       "FiltroReporteMillas_GV42"
      ],
      [
       "ahora",
       "DateTime"
      ]
     ]
    },
    {
     "n": "MillasDelVuelo",
     "t": "int",
     "v": "Public",
     "s": true,
     "p": [
      [
       "v",
       "VueloVolado_GV42"
      ]
     ]
    },
    {
     "n": "CategoriaPara",
     "t": "CategoriaMillas_GV42",
     "v": "Public",
     "s": true,
     "p": [
      [
       "millasUltimos12Meses",
       "int"
      ]
     ]
    },
    {
     "n": "UmbralDe",
     "t": "int",
     "v": "Public",
     "s": true,
     "p": [
      [
       "categoria",
       "CategoriaMillas_GV42"
      ]
     ]
    },
    {
     "n": "ExportarPdf",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "ruta",
       "string"
      ],
      [
       "filas",
       "List<ReporteMillas_GV42>"
      ],
      [
       "filtro",
       "FiltroReporteMillas_GV42"
      ]
     ]
    },
    {
     "n": "Resumen",
     "t": "List<string>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "filas",
       "List<ReporteMillas_GV42>"
      ]
     ]
    },
    {
     "n": "TextoCategoria",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "c",
       "CategoriaMillas_GV42"
      ]
     ]
    },
    {
     "n": "TextoPeriodo",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "p",
       "PeriodoMillas_GV42"
      ]
     ]
    },
    {
     "n": "TextoFaltan",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "f",
       "ReporteMillas_GV42"
      ]
     ]
    },
    {
     "n": "TextoFechaEstimada",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "f",
       "ReporteMillas_GV42"
      ]
     ]
    },
    {
     "n": "TextoTendencia",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "t",
       "TendenciaMillas_GV42"
      ]
     ]
    },
    {
     "n": "TextoRiesgo",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "r",
       "RiesgoAbandono_GV42"
      ]
     ]
    },
    {
     "n": "TextoBaja",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "f",
       "ReporteMillas_GV42"
      ]
     ]
    },
    {
     "n": "TextoProximoViaje",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "f",
       "ReporteMillas_GV42"
      ]
     ]
    },
    {
     "n": "TextoUltimoVuelo",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "f",
       "ReporteMillas_GV42"
      ]
     ]
    },
    {
     "n": "Numero",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "n",
       "int"
      ]
     ]
    },
    {
     "n": "Detalle",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "f",
       "ReporteMillas_GV42"
      ]
     ]
    },
    {
     "n": "ArmarPerfil",
     "t": "ReporteMillas_GV42",
     "v": "Private",
     "s": true,
     "p": [
      [
       "vuelos",
       "List<VueloVolado_GV42>"
      ],
      [
       "periodo",
       "PeriodoMillas_GV42"
      ],
      [
       "ahora",
       "DateTime"
      ]
     ]
    },
    {
     "n": "MillasEnVentana",
     "t": "int",
     "v": "Private",
     "s": true,
     "p": [
      [
       "vuelos",
       "List<VueloVolado_GV42>"
      ],
      [
       "dia",
       "DateTime"
      ]
     ]
    },
    {
     "n": "EstimarSubida",
     "t": "DateTime?",
     "v": "Private",
     "s": true,
     "p": [
      [
       "p",
       "ReporteMillas_GV42"
      ],
      [
       "de12Meses",
       "List<VueloVolado_GV42>"
      ],
      [
       "millasPorDia",
       "decimal"
      ],
      [
       "ahora",
       "DateTime"
      ]
     ]
    },
    {
     "n": "EstimarBaja",
     "t": "void",
     "v": "Private",
     "s": true,
     "p": [
      [
       "p",
       "ReporteMillas_GV42"
      ],
      [
       "de12Meses",
       "List<VueloVolado_GV42>"
      ],
      [
       "ahora",
       "DateTime"
      ]
     ]
    },
    {
     "n": "Tendencia",
     "t": "TendenciaMillas_GV42",
     "v": "Private",
     "s": true,
     "p": [
      [
       "vuelos",
       "List<VueloVolado_GV42>"
      ],
      [
       "ahora",
       "DateTime"
      ]
     ]
    },
    {
     "n": "EstimarProximoViaje",
     "t": "void",
     "v": "Private",
     "s": true,
     "p": [
      [
       "p",
       "ReporteMillas_GV42"
      ],
      [
       "vuelos",
       "List<VueloVolado_GV42>"
      ],
      [
       "ahora",
       "DateTime"
      ]
     ]
    },
    {
     "n": "Vence",
     "t": "bool",
     "v": "Private",
     "s": true,
     "p": [
      [
       "v",
       "VueloVolado_GV42"
      ],
      [
       "ahora",
       "DateTime"
      ],
      [
       "dias",
       "int"
      ]
     ]
    },
    {
     "n": "Recomendar",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "p",
       "ReporteMillas_GV42"
      ],
      [
       "vuelos",
       "List<VueloVolado_GV42>"
      ]
     ]
    },
    {
     "n": "Normalizar",
     "t": "FiltroReporteMillas_GV42",
     "v": "Private",
     "s": true,
     "p": [
      [
       "f",
       "FiltroReporteMillas_GV42"
      ]
     ]
    },
    {
     "n": "LoginSeguro",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": []
    }
   ]
  },
  {
   "nombre": "BLLOfertasCliente_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "MIN_RESERVAS_REGLA",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "MIN_CONFIANZA",
     "t": "decimal",
     "v": "Public",
     "s": true
    },
    {
     "n": "MIN_LIFT",
     "t": "decimal",
     "v": "Public",
     "s": true
    },
    {
     "n": "MIN_PARTICIPACION_CATEGORIA",
     "t": "decimal",
     "v": "Public",
     "s": true
    },
    {
     "n": "MIN_RESERVAS_HABITUAL",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "MAX_OFERTAS_ASOCIADAS",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "MAX_OFERTAS_CATEGORIA",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "_dal",
     "t": "DALReporteMillas_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "_dalTipo",
     "t": "DALTipoAdicional_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "_reservas",
     "t": "BLLReserva_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "_perfiles",
     "t": "List<ReporteMillas_GV42>",
     "v": "Private",
     "s": false
    },
    {
     "n": "_contrataciones",
     "t": "List<ContratacionAdicional_GV42>",
     "v": "Private",
     "s": false
    },
    {
     "n": "_catalogo",
     "t": "List<TipoAdicional_GV42>",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "ObtenerOfertas",
     "t": "OfertasCliente_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ],
      [
       "vuelo",
       "VueloClase_GV42"
      ],
      [
       "idsElegidos",
       "IEnumerable<int>"
      ],
      [
       "recargar",
       "bool"
      ]
     ]
    },
    {
     "n": "AgregarServiciosSugeridos",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "filas",
       "List<ReporteMillas_GV42>"
      ],
      [
       "volados",
       "List<VueloVolado_GV42>"
      ],
      [
       "ahora",
       "DateTime"
      ]
     ]
    },
    {
     "n": "Armar",
     "t": "OfertasCliente_GV42",
     "v": "Public",
     "s": true,
     "p": [
      [
       "dni",
       "string"
      ],
      [
       "vuelo",
       "VueloClase_GV42"
      ],
      [
       "idsElegidos",
       "IEnumerable<int>"
      ],
      [
       "catalogo",
       "List<TipoAdicional_GV42>"
      ],
      [
       "perfiles",
       "List<ReporteMillas_GV42>"
      ],
      [
       "contrataciones",
       "List<ContratacionAdicional_GV42>"
      ]
     ]
    },
    {
     "n": "CalcularReglas",
     "t": "List<ReglaAsociacion_GV42>",
     "v": "Public",
     "s": true,
     "p": [
      [
       "contrataciones",
       "List<ContratacionAdicional_GV42>"
      ]
     ]
    },
    {
     "n": "MillasDelViaje",
     "t": "int",
     "v": "Public",
     "s": true,
     "p": [
      [
       "vuelo",
       "Vuelo_GV42"
      ],
      [
       "clase",
       "ClaseVuelo_GV42"
      ]
     ]
    },
    {
     "n": "OfertaMillas",
     "t": "OfertaCliente_GV42",
     "v": "Private",
     "s": true,
     "p": [
      [
       "vuelo",
       "VueloClase_GV42"
      ],
      [
       "categoria",
       "CategoriaMillas_GV42"
      ],
      [
       "millas12",
       "int"
      ]
     ]
    },
    {
     "n": "Catalogo",
     "t": "List<TipoAdicional_GV42>",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "ValidarAcceso",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ]
     ]
    },
    {
     "n": "Porcentaje",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "proporcion",
       "decimal"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "BLLReporteReserva_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "PATENTE_VER",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "PATENTE_EXPORTAR",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "EVENTO_CONSULTA",
     "t": "string",
     "v": "Private",
     "s": true
    },
    {
     "n": "EVENTO_PDF",
     "t": "string",
     "v": "Private",
     "s": true
    },
    {
     "n": "Cultura",
     "t": "CultureInfo",
     "v": "Private",
     "s": true
    },
    {
     "n": "_dal",
     "t": "DALReporteReserva_GV42",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "PuedeVer",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "PuedeExportar",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "Generar",
     "t": "List<ReporteReserva_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "filtro",
       "FiltroReporteReservas_GV42"
      ]
     ]
    },
    {
     "n": "ExportarPdf",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "ruta",
       "string"
      ],
      [
       "filas",
       "List<ReporteReserva_GV42>"
      ],
      [
       "filtro",
       "FiltroReporteReservas_GV42"
      ]
     ]
    },
    {
     "n": "Resumen",
     "t": "List<string>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "filas",
       "List<ReporteReserva_GV42>"
      ]
     ]
    },
    {
     "n": "TextoAdicionales",
     "t": "string",
     "v": "Public",
     "s": true,
     "p": [
      [
       "f",
       "ReporteReserva_GV42"
      ],
      [
       "separador",
       "string"
      ]
     ]
    },
    {
     "n": "Normalizar",
     "t": "FiltroReporteReservas_GV42",
     "v": "Private",
     "s": true,
     "p": [
      [
       "f",
       "FiltroReporteReservas_GV42"
      ]
     ]
    },
    {
     "n": "DescripcionFiltro",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "f",
       "FiltroReporteReservas_GV42"
      ]
     ]
    },
    {
     "n": "AdicionalesPdf",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "f",
       "ReporteReserva_GV42"
      ]
     ]
    },
    {
     "n": "Numero",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "importe",
       "decimal"
      ]
     ]
    },
    {
     "n": "Dinero",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "importe",
       "decimal"
      ]
     ]
    },
    {
     "n": "LoginSeguro",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": []
    },
    {
     "n": "Recortar",
     "t": "string",
     "v": "Private",
     "s": true,
     "p": [
      [
       "s",
       "string"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "BLLVuelo_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "MAX_HORAS_VUELO",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "MAX_DIAS_ANTICIPACION",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "MAX_COSTO_KILO",
     "t": "decimal",
     "v": "Public",
     "s": true
    },
    {
     "n": "MAX_PRECIO_BASE",
     "t": "decimal",
     "v": "Public",
     "s": true
    },
    {
     "n": "ASIENTOS_POR_FILA",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "FILAS_PRIMERA",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "FILAS_EJECUTIVA",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "FILAS_ECONOMICA",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "FRANQUICIA_ECONOMICA",
     "t": "decimal",
     "v": "Public",
     "s": true
    },
    {
     "n": "FRANQUICIA_EJECUTIVA",
     "t": "decimal",
     "v": "Public",
     "s": true
    },
    {
     "n": "FRANQUICIA_PRIMERA",
     "t": "decimal",
     "v": "Public",
     "s": true
    },
    {
     "n": "_dalVuelo",
     "t": "DALVuelo_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "_dalAeropuerto",
     "t": "DALAeropuerto_GV42",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "Listar",
     "t": "List<Vuelo_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarAerolineas",
     "t": "List<Aerolinea_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarAeropuertos",
     "t": "List<Aeropuerto_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "BuscarActual",
     "t": "Vuelo_GV42",
     "v": "Package",
     "s": false,
     "p": [
      [
       "numeroVuelo",
       "int"
      ]
     ]
    },
    {
     "n": "ContarReservasVigentes",
     "t": "int",
     "v": "Package",
     "s": false,
     "p": [
      [
       "numeroVuelo",
       "int"
      ]
     ]
    },
    {
     "n": "ValidarCambio",
     "t": "void",
     "v": "Package",
     "s": false,
     "p": [
      [
       "actual",
       "Vuelo_GV42"
      ],
      [
       "codigo",
       "string"
      ],
      [
       "aerolinea",
       "string"
      ],
      [
       "ruta",
       "string"
      ],
      [
       "salida",
       "DateTime"
      ],
      [
       "llegada",
       "DateTime"
      ],
      [
       "costoKilo",
       "decimal"
      ]
     ]
    },
    {
     "n": "ValidarReprogramacion",
     "t": "void",
     "v": "Package",
     "s": false,
     "p": [
      [
       "actual",
       "Vuelo_GV42"
      ],
      [
       "salida",
       "DateTime"
      ],
      [
       "llegada",
       "DateTime"
      ]
     ]
    },
    {
     "n": "Crear",
     "t": "int",
     "v": "Public",
     "s": false,
     "p": [
      [
       "v",
       "Vuelo_GV42"
      ],
      [
       "precioEconomica",
       "decimal"
      ],
      [
       "precioEjecutiva",
       "decimal"
      ],
      [
       "precioPrimera",
       "decimal"
      ]
     ]
    },
    {
     "n": "ValidarDatos",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "v",
       "Vuelo_GV42"
      ]
     ]
    },
    {
     "n": "Modificar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "v",
       "Vuelo_GV42"
      ]
     ]
    },
    {
     "n": "DarDeBaja",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "v",
       "Vuelo_GV42"
      ]
     ]
    },
    {
     "n": "Reactivar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "v",
       "Vuelo_GV42"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "BLLCliente_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "PATENTE_VER",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "PATENTE_GESTIONAR",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "_dal",
     "t": "DALCliente_GV42",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "PuedeGestionar",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "Listar",
     "t": "List<Cliente_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "Crear",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "cliente",
       "Cliente_GV42"
      ]
     ]
    },
    {
     "n": "Modificar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "cliente",
       "Cliente_GV42"
      ]
     ]
    },
    {
     "n": "Eliminar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "dni",
       "string"
      ]
     ]
    },
    {
     "n": "Serializar",
     "t": "int",
     "v": "Public",
     "s": false,
     "p": [
      [
       "clientes",
       "List<Cliente_GV42>"
      ],
      [
       "rutaArchivo",
       "string"
      ]
     ]
    },
    {
     "n": "Deserializar",
     "t": "List<Cliente_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "rutaArchivo",
       "string"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "BLLAerolinea_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "PATENTE_GESTIONAR",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "MAX_NOMBRE",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "_dal",
     "t": "DALAerolinea_GV42",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "Listar",
     "t": "List<Aerolinea_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "Crear",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "aerolinea",
       "Aerolinea_GV42"
      ]
     ]
    },
    {
     "n": "Modificar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "aerolinea",
       "Aerolinea_GV42"
      ]
     ]
    },
    {
     "n": "Eliminar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "aerolinea",
       "Aerolinea_GV42"
      ]
     ]
    },
    {
     "n": "Validar",
     "t": "void",
     "v": "Private",
     "s": true,
     "p": [
      [
       "a",
       "Aerolinea_GV42"
      ]
     ]
    },
    {
     "n": "ExigirNombreUnico",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "a",
       "Aerolinea_GV42"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "BLLAeropuerto_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "PATENTE_GESTIONAR",
     "t": "string",
     "v": "Public",
     "s": true
    },
    {
     "n": "LARGO_CODIGO",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "MAX_NOMBRE",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "MAX_CIUDAD",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "MAX_PAIS",
     "t": "int",
     "v": "Public",
     "s": true
    },
    {
     "n": "_dal",
     "t": "DALAeropuerto_GV42",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "Listar",
     "t": "List<Aeropuerto_GV42>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "Crear",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "aeropuerto",
       "Aeropuerto_GV42"
      ]
     ]
    },
    {
     "n": "Modificar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "aeropuerto",
       "Aeropuerto_GV42"
      ]
     ]
    },
    {
     "n": "Eliminar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "aeropuerto",
       "Aeropuerto_GV42"
      ]
     ]
    },
    {
     "n": "Validar",
     "t": "void",
     "v": "Private",
     "s": true,
     "p": [
      [
       "a",
       "Aeropuerto_GV42"
      ],
      [
       "validarCodigo",
       "bool"
      ]
     ]
    },
    {
     "n": "ExigirNombreUnico",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "a",
       "Aeropuerto_GV42"
      ]
     ]
    },
    {
     "n": "ExigirTexto",
     "t": "void",
     "v": "Private",
     "s": true,
     "p": [
      [
       "valor",
       "string"
      ],
      [
       "maximo",
       "int"
      ],
      [
       "claveMensaje",
       "string"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "BLLVueloHistorial_GV42",
   "capa": "BLL",
   "padre": null,
   "tipo": "Class",
   "attrs": [
    {
     "n": "_dal",
     "t": "DALVueloHistorial_GV42",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "PuedeActivar",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarCodigos",
     "t": "List<string>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ListarNombres",
     "t": "List<string>",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "Consultar",
     "t": "List<VueloCambio_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "codigoVuelo",
       "string"
      ],
      [
       "nombre",
       "string"
      ],
      [
       "fechaIni",
       "DateTime?"
      ],
      [
       "fechaFin",
       "DateTime?"
      ],
      [
       "soloConCambios",
       "bool"
      ]
     ]
    },
    {
     "n": "ActivarVersion",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "cambio",
       "VueloCambio_GV42"
      ]
     ]
    }
   ]
  },
  {
   "nombre": "FRMAviso_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMBackupManual",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMBitacoraDeEventos",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMCambiarContrasenia",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMIniciarSesion",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMMenuPrincipalAdmin",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMMenuPrincipalUsuario",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMGestionUsuariosAdmin",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMGestionPermisos",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMIntegridad",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMSeleccionInstancia",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMRegistroCliente_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMReservarVuelo_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMPagoReserva_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "Tema_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMTarjetaEmbarque_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "CtrlTarjetaEmbarque_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "CtrlEtiquetaEquipaje_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "CtrlPesoValija_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMCheckIn_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "EstiloBoton_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Enumeration",
   "attrs": [
    {
     "n": "Primario",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Secundario",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Peligro",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Exito",
     "t": "",
     "v": "Public",
     "s": false
    },
    {
     "n": "Advertencia",
     "t": "",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": []
  },
  {
   "nombre": "BotonModerno_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "PanelTarjeta_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "PanelEncabezado_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "GrillaModerna_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "TextBoxModerno_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "MenuModerno_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "LienzoGdi_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "CtrlBoleto_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMBoletos_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "CtrlButacas_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "CtrlPasajero_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "CtrlOfertasCliente_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "CtrlAdicional_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "CtrlEquipajePasajero_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "CtrlVistaTarjeta_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMGestionVuelos_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMMaestroClientes_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMMaestroAerolineas_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMMaestroAeropuertos_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMAyuda_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMReinstalador_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMReporteMillas_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMBitacoraVuelos_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMConsultarReservas_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMAltaVuelo_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMReporteCheckIn_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMReporteReservas_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "CtrlNavegadorPasajero_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "FRMCambiarVuelo_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "CtrlFechasFlexibles_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  },
  {
   "nombre": "CtrlTarifa_GV42",
   "capa": "UI",
   "padre": null,
   "tipo": "Class",
   "attrs": [],
   "metodos": []
  }
 ],
 "relaciones": [
  {
   "o": "Familia_GV42",
   "d": "IComponentePermiso_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "Patente_GV42",
   "d": "IComponentePermiso_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "Rol_GV42",
   "d": "IComponentePermiso_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "Cliente_GV42",
   "d": "Persona_GV42",
   "tipo": "Generalization"
  },
  {
   "o": "Pasajero_GV42",
   "d": "Persona_GV42",
   "tipo": "Generalization"
  },
  {
   "o": "LienzoPdf_GV42",
   "d": "ILienzo_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMAviso_GV42",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMBackupManual",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMBitacoraDeEventos",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMCambiarContrasenia",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMIniciarSesion",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMMenuPrincipalUsuario",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMGestionUsuariosAdmin",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMGestionPermisos",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMIntegridad",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMSeleccionInstancia",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMRegistroCliente_GV42",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMPagoReserva_GV42",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMTarjetaEmbarque_GV42",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "LienzoGdi_GV42",
   "d": "ILienzo_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMBoletos_GV42",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMGestionVuelos_GV42",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMMaestroClientes_GV42",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMMaestroAerolineas_GV42",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMMaestroAeropuertos_GV42",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMAyuda_GV42",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMReinstalador_GV42",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMReporteMillas_GV42",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMBitacoraVuelos_GV42",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMConsultarReservas_GV42",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMAltaVuelo_GV42",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMReporteCheckIn_GV42",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMReporteReservas_GV42",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "FRMCambiarVuelo_GV42",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Realisation"
  },
  {
   "o": "IdiomaManager_GV42",
   "d": "IObservadorIdioma_GV42",
   "tipo": "Association",
   "rol": "_observadores",
   "mult": "0..*"
  },
  {
   "o": "SessionManager_GV42",
   "d": "Usuario_GV42",
   "tipo": "Association",
   "rol": "_usuarioActual",
   "mult": "1"
  },
  {
   "o": "Usuario_GV42",
   "d": "Rol_GV42",
   "tipo": "Association",
   "rol": "_Rol",
   "mult": "1"
  },
  {
   "o": "AdicionalReserva_GV42",
   "d": "TipoAdicional_GV42",
   "tipo": "Association",
   "rol": "TipoAdicional",
   "mult": "1"
  },
  {
   "o": "AsientoDisponibilidad_GV42",
   "d": "Asiento_GV42",
   "tipo": "Association",
   "rol": "Asiento",
   "mult": "1"
  },
  {
   "o": "AsientoPasajero_GV42",
   "d": "Asiento_GV42",
   "tipo": "Association",
   "rol": "Asiento",
   "mult": "1"
  },
  {
   "o": "Asiento_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Association",
   "rol": "Clase",
   "mult": "1"
  },
  {
   "o": "Boleto_GV42",
   "d": "Pasajero_GV42",
   "tipo": "Association",
   "rol": "Pasajero",
   "mult": "1"
  },
  {
   "o": "BoletoElectronico_GV42",
   "d": "TipoPasajero_GV42",
   "tipo": "Association",
   "rol": "TipoPasajero",
   "mult": "1"
  },
  {
   "o": "BoletoElectronico_GV42",
   "d": "AsistenciaEspecial_GV42",
   "tipo": "Association",
   "rol": "Asistencia",
   "mult": "1"
  },
  {
   "o": "BoletoElectronico_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Association",
   "rol": "ClaseValor",
   "mult": "0..1"
  },
  {
   "o": "BoletoElectronico_GV42",
   "d": "TipoViaje_GV42",
   "tipo": "Association",
   "rol": "TipoViajeValor",
   "mult": "0..1"
  },
  {
   "o": "BoletoElectronico_GV42",
   "d": "MedioPago_GV42",
   "tipo": "Association",
   "rol": "MedioPagoValor",
   "mult": "0..1"
  },
  {
   "o": "BoletoElectronico_GV42",
   "d": "EstadoReserva_GV42",
   "tipo": "Association",
   "rol": "EstadoValor",
   "mult": "0..1"
  },
  {
   "o": "BoletoElectronico_GV42",
   "d": "AdicionalReserva_GV42",
   "tipo": "Association",
   "rol": "Adicionales",
   "mult": "0..*"
  },
  {
   "o": "CargoExcesoEquipaje_GV42",
   "d": "MedioPago_GV42",
   "tipo": "Association",
   "rol": "MedioPago",
   "mult": "0..1"
  },
  {
   "o": "CheckIn_GV42",
   "d": "Pasajero_GV42",
   "tipo": "Association",
   "rol": "Pasajero",
   "mult": "1"
  },
  {
   "o": "CheckIn_GV42",
   "d": "VueloClase_GV42",
   "tipo": "Association",
   "rol": "VueloClase",
   "mult": "1"
  },
  {
   "o": "CheckIn_GV42",
   "d": "EstadoReserva_GV42",
   "tipo": "Association",
   "rol": "EstadoReserva",
   "mult": "1"
  },
  {
   "o": "CheckIn_GV42",
   "d": "EstadoCheckIn_GV42",
   "tipo": "Association",
   "rol": "Estado",
   "mult": "1"
  },
  {
   "o": "CheckIn_GV42",
   "d": "CanalVenta_GV42",
   "tipo": "Association",
   "rol": "Canal",
   "mult": "0..1"
  },
  {
   "o": "CheckIn_GV42",
   "d": "TipoViaje_GV42",
   "tipo": "Association",
   "rol": "TipoViaje",
   "mult": "1"
  },
  {
   "o": "CheckIn_GV42",
   "d": "TipoPasajero_GV42",
   "tipo": "Association",
   "rol": "TipoPasajero",
   "mult": "1"
  },
  {
   "o": "CheckIn_GV42",
   "d": "AsistenciaEspecial_GV42",
   "tipo": "Association",
   "rol": "Asistencia",
   "mult": "1"
  },
  {
   "o": "CheckIn_GV42",
   "d": "AdicionalReserva_GV42",
   "tipo": "Association",
   "rol": "ServiciosAdicionales",
   "mult": "0..*"
  },
  {
   "o": "CheckIn_GV42",
   "d": "Asiento_GV42",
   "tipo": "Association",
   "rol": "Asiento",
   "mult": "1"
  },
  {
   "o": "CheckIn_GV42",
   "d": "Equipaje_GV42",
   "tipo": "Association",
   "rol": "Equipaje",
   "mult": "1"
  },
  {
   "o": "CheckIn_GV42",
   "d": "TarjetaEmbarque_GV42",
   "tipo": "Association",
   "rol": "TarjetaEmbarque",
   "mult": "1"
  },
  {
   "o": "CheckIn_GV42",
   "d": "Vuelo_GV42",
   "tipo": "Association",
   "rol": "Vuelo",
   "mult": "1"
  },
  {
   "o": "CriterioBusquedaVuelo_GV42",
   "d": "TipoViaje_GV42",
   "tipo": "Association",
   "rol": "TipoViaje",
   "mult": "1"
  },
  {
   "o": "CriterioBusquedaVuelo_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Association",
   "rol": "Clase",
   "mult": "0..1"
  },
  {
   "o": "Equipaje_GV42",
   "d": "CargoExcesoEquipaje_GV42",
   "tipo": "Association",
   "rol": "CargoExceso",
   "mult": "1"
  },
  {
   "o": "Pago_GV42",
   "d": "MedioPago_GV42",
   "tipo": "Association",
   "rol": "MedioPago",
   "mult": "1"
  },
  {
   "o": "ListaClientesXml_GV42",
   "d": "ClienteXml_GV42",
   "tipo": "Association",
   "rol": "Clientes",
   "mult": "0..*"
  },
  {
   "o": "Pasajero_GV42",
   "d": "TipoPasajero_GV42",
   "tipo": "Association",
   "rol": "Tipo",
   "mult": "1"
  },
  {
   "o": "Pasajero_GV42",
   "d": "AsistenciaEspecial_GV42",
   "tipo": "Association",
   "rol": "Asistencia",
   "mult": "1"
  },
  {
   "o": "Reserva_GV42",
   "d": "Pasajero_GV42",
   "tipo": "Association",
   "rol": "Cliente",
   "mult": "1"
  },
  {
   "o": "Reserva_GV42",
   "d": "VueloClase_GV42",
   "tipo": "Association",
   "rol": "VueloClase",
   "mult": "1"
  },
  {
   "o": "Reserva_GV42",
   "d": "TarifaFamilia_GV42",
   "tipo": "Association",
   "rol": "Tarifa",
   "mult": "1"
  },
  {
   "o": "Reserva_GV42",
   "d": "TipoViaje_GV42",
   "tipo": "Association",
   "rol": "TipoViaje",
   "mult": "1"
  },
  {
   "o": "Reserva_GV42",
   "d": "AsientoPasajero_GV42",
   "tipo": "Association",
   "rol": "AsientosPorPasajero",
   "mult": "0..*"
  },
  {
   "o": "Reserva_GV42",
   "d": "AdicionalReserva_GV42",
   "tipo": "Association",
   "rol": "Adicionales",
   "mult": "0..*"
  },
  {
   "o": "Reserva_GV42",
   "d": "EstadoReserva_GV42",
   "tipo": "Association",
   "rol": "Estado",
   "mult": "1"
  },
  {
   "o": "Reserva_GV42",
   "d": "CanalVenta_GV42",
   "tipo": "Association",
   "rol": "CanalVenta",
   "mult": "1"
  },
  {
   "o": "Reserva_GV42",
   "d": "Pago_GV42",
   "tipo": "Association",
   "rol": "Pago",
   "mult": "1"
  },
  {
   "o": "Reserva_GV42",
   "d": "Reembolso_GV42",
   "tipo": "Association",
   "rol": "Reembolso",
   "mult": "1"
  },
  {
   "o": "Reserva_GV42",
   "d": "Vuelo_GV42",
   "tipo": "Association",
   "rol": "Vuelo",
   "mult": "1"
  },
  {
   "o": "Reserva_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Association",
   "rol": "Clase",
   "mult": "1"
  },
  {
   "o": "TarjetaEmbarque_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Association",
   "rol": "Clase",
   "mult": "1"
  },
  {
   "o": "ReporteCheckIn_GV42",
   "d": "EstadoCheckIn_GV42",
   "tipo": "Association",
   "rol": "Estado",
   "mult": "1"
  },
  {
   "o": "ReporteCheckIn_GV42",
   "d": "EstadoAsistencia_GV42",
   "tipo": "Association",
   "rol": "Asistencia",
   "mult": "1"
  },
  {
   "o": "ReporteCheckIn_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Association",
   "rol": "Clase",
   "mult": "1"
  },
  {
   "o": "FiltroReporteCheckIn_GV42",
   "d": "EstadoCheckIn_GV42",
   "tipo": "Association",
   "rol": "Estado",
   "mult": "0..1"
  },
  {
   "o": "FiltroReporteCheckIn_GV42",
   "d": "EstadoAsistencia_GV42",
   "tipo": "Association",
   "rol": "Asistencia",
   "mult": "0..1"
  },
  {
   "o": "FiltroReporteMillas_GV42",
   "d": "PeriodoMillas_GV42",
   "tipo": "Association",
   "rol": "Periodo",
   "mult": "1"
  },
  {
   "o": "FiltroReporteMillas_GV42",
   "d": "CategoriaMillas_GV42",
   "tipo": "Association",
   "rol": "Categoria",
   "mult": "0..1"
  },
  {
   "o": "VueloVolado_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Association",
   "rol": "Clase",
   "mult": "1"
  },
  {
   "o": "ReporteMillas_GV42",
   "d": "CategoriaMillas_GV42",
   "tipo": "Association",
   "rol": "Categoria",
   "mult": "1"
  },
  {
   "o": "ReporteMillas_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Association",
   "rol": "ClaseHabitual",
   "mult": "1"
  },
  {
   "o": "ReporteMillas_GV42",
   "d": "VueloVolado_GV42",
   "tipo": "Association",
   "rol": "UltimoVuelo",
   "mult": "1"
  },
  {
   "o": "ReporteMillas_GV42",
   "d": "TendenciaMillas_GV42",
   "tipo": "Association",
   "rol": "Tendencia",
   "mult": "1"
  },
  {
   "o": "ReporteMillas_GV42",
   "d": "RiesgoAbandono_GV42",
   "tipo": "Association",
   "rol": "Riesgo",
   "mult": "1"
  },
  {
   "o": "OfertaCliente_GV42",
   "d": "MotivoOferta_GV42",
   "tipo": "Association",
   "rol": "Motivo",
   "mult": "1"
  },
  {
   "o": "OfertaCliente_GV42",
   "d": "TipoAdicional_GV42",
   "tipo": "Association",
   "rol": "Tipo",
   "mult": "1"
  },
  {
   "o": "OfertasCliente_GV42",
   "d": "CategoriaMillas_GV42",
   "tipo": "Association",
   "rol": "Categoria",
   "mult": "1"
  },
  {
   "o": "OfertasCliente_GV42",
   "d": "OfertaCliente_GV42",
   "tipo": "Association",
   "rol": "Ofertas",
   "mult": "0..*"
  },
  {
   "o": "ReporteReserva_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Association",
   "rol": "Clase",
   "mult": "1"
  },
  {
   "o": "ReporteReserva_GV42",
   "d": "AdicionalReporte_GV42",
   "tipo": "Association",
   "rol": "Adicionales",
   "mult": "0..*"
  },
  {
   "o": "ReporteReserva_GV42",
   "d": "EstadoReserva_GV42",
   "tipo": "Association",
   "rol": "Estado",
   "mult": "1"
  },
  {
   "o": "FiltroReporteReservas_GV42",
   "d": "FechaReporte_GV42",
   "tipo": "Association",
   "rol": "TipoFecha",
   "mult": "1"
  },
  {
   "o": "FiltroReporteReservas_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Association",
   "rol": "Clase",
   "mult": "0..1"
  },
  {
   "o": "FiltroReporteReservas_GV42",
   "d": "EstadoReserva_GV42",
   "tipo": "Association",
   "rol": "Estado",
   "mult": "0..1"
  },
  {
   "o": "VueloClase_GV42",
   "d": "Vuelo_GV42",
   "tipo": "Association",
   "rol": "Vuelo",
   "mult": "1"
  },
  {
   "o": "VueloClase_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Association",
   "rol": "Clase",
   "mult": "1"
  },
  {
   "o": "Vuelo_GV42",
   "d": "Aeropuerto_GV42",
   "tipo": "Association",
   "rol": "Origen",
   "mult": "1"
  },
  {
   "o": "Reembolso_GV42",
   "d": "MedioPago_GV42",
   "tipo": "Association",
   "rol": "MedioPago",
   "mult": "1"
  },
  {
   "o": "Reembolso_GV42",
   "d": "EstadoReembolso_GV42",
   "tipo": "Association",
   "rol": "Estado",
   "mult": "1"
  },
  {
   "o": "Asiento_GV42",
   "d": "Vuelo_GV42",
   "tipo": "Association",
   "rol": "NumeroVuelo",
   "mult": "1"
  },
  {
   "o": "CheckIn_GV42",
   "d": "Reserva_GV42",
   "tipo": "Association",
   "rol": "CodigoReserva",
   "mult": "1"
  },
  {
   "o": "CriterioBusquedaVuelo_GV42",
   "d": "Aeropuerto_GV42",
   "tipo": "Association",
   "rol": "CodigoOrigen / CodigoDestino",
   "mult": "1"
  },
  {
   "o": "ReporteCheckIn_GV42",
   "d": "CheckIn_GV42",
   "tipo": "Association",
   "rol": "CodigoCheckIn",
   "mult": "1"
  },
  {
   "o": "ContratacionAdicional_GV42",
   "d": "Reserva_GV42",
   "tipo": "Association",
   "rol": "CodigoReserva",
   "mult": "1"
  },
  {
   "o": "ContratacionAdicional_GV42",
   "d": "TipoAdicional_GV42",
   "tipo": "Association",
   "rol": "CodigoTipoAdicional",
   "mult": "1"
  },
  {
   "o": "ReglaAsociacion_GV42",
   "d": "TipoAdicional_GV42",
   "tipo": "Association",
   "rol": "CodigoAntecedente / CodigoConsecuente",
   "mult": "1"
  },
  {
   "o": "ReporteReserva_GV42",
   "d": "Reserva_GV42",
   "tipo": "Association",
   "rol": "CodigoReserva",
   "mult": "1"
  },
  {
   "o": "VueloCambio_GV42",
   "d": "Vuelo_GV42",
   "tipo": "Association",
   "rol": "NumeroVuelo",
   "mult": "1"
  },
  {
   "o": "Familia_GV42",
   "d": "Patente_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "IComponentePermiso_GV42",
   "d": "Patente_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "Rol_GV42",
   "d": "Patente_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "Validaciones_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "Textos_GV42",
   "d": "AsistenciaEspecial_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "Textos_GV42",
   "d": "CanalVenta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "Textos_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "Textos_GV42",
   "d": "EstadoAsistencia_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "Textos_GV42",
   "d": "EstadoCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "Textos_GV42",
   "d": "EstadoReembolso_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "Textos_GV42",
   "d": "EstadoReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "Textos_GV42",
   "d": "MedioPago_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "Textos_GV42",
   "d": "TipoPasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "Textos_GV42",
   "d": "TipoViaje_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "ClienteXml_GV42",
   "d": "Cliente_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALBitacora_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "DALBitacora_GV42",
   "d": "Bitacora_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALBitacora_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALBitacora_GV42",
   "d": "Validaciones_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALFamilia_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "DALFamilia_GV42",
   "d": "DALPatente_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALFamilia_GV42",
   "d": "Familia_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALFamilia_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALFamilia_GV42",
   "d": "Patente_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALPatente_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "DALPatente_GV42",
   "d": "Patente_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALRol_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "DALRol_GV42",
   "d": "DALFamilia_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALRol_GV42",
   "d": "DALPatente_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALRol_GV42",
   "d": "Familia_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALRol_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALRol_GV42",
   "d": "Patente_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALRol_GV42",
   "d": "Rol_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALUsuario_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "DALUsuario_GV42",
   "d": "DALIntegridad_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALUsuario_GV42",
   "d": "EncriptadorReversible_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALUsuario_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALUsuario_GV42",
   "d": "Rol_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALUsuario_GV42",
   "d": "Usuario_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALIntegridad_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "DALIntegridad_GV42",
   "d": "CalculadorIntegridad_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "InstaladorBD_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "InstaladorBD_GV42",
   "d": "EncriptadorReversible_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALAerolinea_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "DALAerolinea_GV42",
   "d": "DALUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALAeropuerto_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "DALAeropuerto_GV42",
   "d": "Aeropuerto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALAeropuerto_GV42",
   "d": "DALUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALAsiento_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "DALAsiento_GV42",
   "d": "AsientoDisponibilidad_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALAsiento_GV42",
   "d": "Asiento_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALAsiento_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALAsiento_GV42",
   "d": "DALUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALAsiento_GV42",
   "d": "EstadoCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALAsiento_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALAsiento_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALBoleto_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "DALBoleto_GV42",
   "d": "Boleto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALBoleto_GV42",
   "d": "DALUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALBoleto_GV42",
   "d": "Pasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALCheckIn_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "DALCheckIn_GV42",
   "d": "AsistenciaEspecial_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALCheckIn_GV42",
   "d": "CanalVenta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALCheckIn_GV42",
   "d": "CheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALCheckIn_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALCheckIn_GV42",
   "d": "DALAsiento_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALCheckIn_GV42",
   "d": "DALEquipaje_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALCheckIn_GV42",
   "d": "DALReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALCheckIn_GV42",
   "d": "DALTarjetaEmbarque_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALCheckIn_GV42",
   "d": "DALUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALCheckIn_GV42",
   "d": "EstadoCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALCheckIn_GV42",
   "d": "EstadoReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALCheckIn_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALCheckIn_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALCheckIn_GV42",
   "d": "Pasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALCheckIn_GV42",
   "d": "TarifaFamilia_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALCheckIn_GV42",
   "d": "TipoPasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALCheckIn_GV42",
   "d": "TipoViaje_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALCliente_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "DALCliente_GV42",
   "d": "Cliente_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALCliente_GV42",
   "d": "DALUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALPasajero_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "DALPasajero_GV42",
   "d": "DALUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALPasajero_GV42",
   "d": "Pasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALEquipaje_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "DALEquipaje_GV42",
   "d": "CargoExcesoEquipaje_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALEquipaje_GV42",
   "d": "DALUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALEquipaje_GV42",
   "d": "Equipaje_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALEquipaje_GV42",
   "d": "MedioPago_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALPago_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "DALPago_GV42",
   "d": "DALUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALPago_GV42",
   "d": "EstadoReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALPago_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALPago_GV42",
   "d": "MedioPago_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALPago_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALPago_GV42",
   "d": "Pago_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReserva_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "DALReserva_GV42",
   "d": "AdicionalReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReserva_GV42",
   "d": "AsientoPasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReserva_GV42",
   "d": "Asiento_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReserva_GV42",
   "d": "AsistenciaEspecial_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReserva_GV42",
   "d": "CanalVenta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReserva_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReserva_GV42",
   "d": "CotizacionCambio_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReserva_GV42",
   "d": "DALPago_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReserva_GV42",
   "d": "DALTarifa_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReserva_GV42",
   "d": "DALUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReserva_GV42",
   "d": "DALVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReserva_GV42",
   "d": "EstadoCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReserva_GV42",
   "d": "EstadoReembolso_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReserva_GV42",
   "d": "EstadoReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReserva_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReserva_GV42",
   "d": "MedioPago_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReserva_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReserva_GV42",
   "d": "Pasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReserva_GV42",
   "d": "Reembolso_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReserva_GV42",
   "d": "Reserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReserva_GV42",
   "d": "TipoAdicional_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReserva_GV42",
   "d": "TipoPasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReserva_GV42",
   "d": "TipoViaje_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReserva_GV42",
   "d": "Validaciones_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReserva_GV42",
   "d": "VueloClase_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReporteCheckIn_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "DALReporteCheckIn_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReporteCheckIn_GV42",
   "d": "DALUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReporteCheckIn_GV42",
   "d": "EstadoCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReporteCheckIn_GV42",
   "d": "EstadoReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReporteCheckIn_GV42",
   "d": "FiltroReporteCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReporteCheckIn_GV42",
   "d": "ReporteCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReporteMillas_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "DALReporteMillas_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReporteMillas_GV42",
   "d": "ContratacionAdicional_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReporteMillas_GV42",
   "d": "DALUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReporteMillas_GV42",
   "d": "EstadoCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReporteMillas_GV42",
   "d": "EstadoReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReporteMillas_GV42",
   "d": "VueloVolado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReporteReserva_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "DALReporteReserva_GV42",
   "d": "AdicionalReporte_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReporteReserva_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReporteReserva_GV42",
   "d": "DALUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReporteReserva_GV42",
   "d": "EstadoReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReporteReserva_GV42",
   "d": "FechaReporte_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReporteReserva_GV42",
   "d": "FiltroReporteReservas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReporteReserva_GV42",
   "d": "ReporteReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALTarjetaEmbarque_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "DALTarjetaEmbarque_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALTarjetaEmbarque_GV42",
   "d": "DALUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALTarjetaEmbarque_GV42",
   "d": "EstadoCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALTarjetaEmbarque_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALTarjetaEmbarque_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALTarjetaEmbarque_GV42",
   "d": "TarjetaEmbarque_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALTipoAdicional_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "DALTipoAdicional_GV42",
   "d": "DALUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALTipoAdicional_GV42",
   "d": "TipoAdicional_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALUtil_GV42",
   "d": "Aeropuerto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALUtil_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALUtil_GV42",
   "d": "EncriptadorReversible_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALUtil_GV42",
   "d": "Persona_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALUtil_GV42",
   "d": "VueloClase_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALUtil_GV42",
   "d": "Vuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALVuelo_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "DALVuelo_GV42",
   "d": "Aeropuerto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALVuelo_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALVuelo_GV42",
   "d": "CriterioBusquedaVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALVuelo_GV42",
   "d": "DALUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALVuelo_GV42",
   "d": "EstadoCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALVuelo_GV42",
   "d": "EstadoReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALVuelo_GV42",
   "d": "VueloClase_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALVuelo_GV42",
   "d": "Vuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALVueloHistorial_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "DALVueloHistorial_GV42",
   "d": "DALUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALVueloHistorial_GV42",
   "d": "EstadoReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALVueloHistorial_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALVueloHistorial_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALVueloHistorial_GV42",
   "d": "VueloCambio_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALTarifa_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "DALTarifa_GV42",
   "d": "DALUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALTarifa_GV42",
   "d": "TarifaFamilia_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLBitacora_GV42",
   "d": "Bitacora_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLBitacora_GV42",
   "d": "DALBitacora_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLBitacora_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLBoleto_GV42",
   "d": "AdicionalReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLBoleto_GV42",
   "d": "Asiento_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLBoleto_GV42",
   "d": "AsistenciaEspecial_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLBoleto_GV42",
   "d": "BLLNegocioUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLBoleto_GV42",
   "d": "BLLReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLBoleto_GV42",
   "d": "BoletoElectronico_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLBoleto_GV42",
   "d": "Boleto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLBoleto_GV42",
   "d": "DALBoleto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLBoleto_GV42",
   "d": "DisenioBoleto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLBoleto_GV42",
   "d": "EstadoReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLBoleto_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLBoleto_GV42",
   "d": "LienzoPdf_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLBoleto_GV42",
   "d": "MedioPago_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLBoleto_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLBoleto_GV42",
   "d": "Pasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLBoleto_GV42",
   "d": "Reserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLBoleto_GV42",
   "d": "TipoPasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLBoleto_GV42",
   "d": "VueloClase_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLBoleto_GV42",
   "d": "Vuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "Code128_GV42",
   "d": "ILienzo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DisenioBoleto_GV42",
   "d": "BLLBoleto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DisenioBoleto_GV42",
   "d": "BoletoElectronico_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DisenioBoleto_GV42",
   "d": "Code128_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DisenioBoleto_GV42",
   "d": "EstadoReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DisenioBoleto_GV42",
   "d": "ILienzo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DisenioBoleto_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DisenioBoleto_GV42",
   "d": "TipoPasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "LienzoPdf_GV42",
   "d": "GeneradorPdf_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLInstalador_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "BLLInstalador_GV42",
   "d": "BLLIntegridad_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLInstalador_GV42",
   "d": "ConfiguracionBD_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLInstalador_GV42",
   "d": "InstaladorBD_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLPermisos_GV42",
   "d": "BLLBitacora_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLPermisos_GV42",
   "d": "BLLIntegridad_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLPermisos_GV42",
   "d": "BLLNegocioUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLPermisos_GV42",
   "d": "DALFamilia_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLPermisos_GV42",
   "d": "DALPatente_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLPermisos_GV42",
   "d": "DALRol_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLPermisos_GV42",
   "d": "DALUsuario_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLPermisos_GV42",
   "d": "Familia_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLPermisos_GV42",
   "d": "IComponentePermiso_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLPermisos_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLPermisos_GV42",
   "d": "Patente_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLPermisos_GV42",
   "d": "Rol_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLPermisos_GV42",
   "d": "SessionManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLPermisos_GV42",
   "d": "Usuario_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLPermisos_GV42",
   "d": "Validaciones_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLUsuario_GV42",
   "d": "BLLBitacora_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLUsuario_GV42",
   "d": "BLLInstalador_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLUsuario_GV42",
   "d": "BLLNegocioUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLUsuario_GV42",
   "d": "BLLPermisos_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLUsuario_GV42",
   "d": "DALPasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLUsuario_GV42",
   "d": "DALUsuario_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLUsuario_GV42",
   "d": "Encriptador_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLUsuario_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLUsuario_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLUsuario_GV42",
   "d": "Pasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLUsuario_GV42",
   "d": "Rol_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLUsuario_GV42",
   "d": "SessionManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLUsuario_GV42",
   "d": "Usuario_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLUsuario_GV42",
   "d": "Validaciones_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "GeneradorPdf_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLIntegridad_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "BLLIntegridad_GV42",
   "d": "BLLBitacora_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLIntegridad_GV42",
   "d": "BLLNegocioUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLIntegridad_GV42",
   "d": "CalculadorIntegridad_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLIntegridad_GV42",
   "d": "ConfiguracionBD_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLIntegridad_GV42",
   "d": "DALIntegridad_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLIntegridad_GV42",
   "d": "DetalleTampering",
   "tipo": "Dependency"
  },
  {
   "o": "BLLIntegridad_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLIntegridad_GV42",
   "d": "ResultadoIntegridad",
   "tipo": "Dependency"
  },
  {
   "o": "BLLIntegridad_GV42",
   "d": "TipoTampering",
   "tipo": "Dependency"
  },
  {
   "o": "ResultadoIntegridad",
   "d": "DetalleTampering",
   "tipo": "Dependency"
  },
  {
   "o": "DetalleTampering",
   "d": "TipoTampering",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCheckIn_GV42",
   "d": "AsientoDisponibilidad_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCheckIn_GV42",
   "d": "Asiento_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCheckIn_GV42",
   "d": "BLLNegocioUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCheckIn_GV42",
   "d": "CanalVenta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCheckIn_GV42",
   "d": "CargoExcesoEquipaje_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCheckIn_GV42",
   "d": "CheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCheckIn_GV42",
   "d": "DALAsiento_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCheckIn_GV42",
   "d": "DALCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCheckIn_GV42",
   "d": "DALEquipaje_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCheckIn_GV42",
   "d": "DALTarjetaEmbarque_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCheckIn_GV42",
   "d": "DatosTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCheckIn_GV42",
   "d": "Equipaje_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCheckIn_GV42",
   "d": "EstadoCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCheckIn_GV42",
   "d": "EstadoReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCheckIn_GV42",
   "d": "FranquiciaEquipaje_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCheckIn_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCheckIn_GV42",
   "d": "MedioPago_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCheckIn_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCheckIn_GV42",
   "d": "Reserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCheckIn_GV42",
   "d": "SessionManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCheckIn_GV42",
   "d": "TarjetaEmbarque_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCheckIn_GV42",
   "d": "Usuario_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCheckIn_GV42",
   "d": "Validaciones_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLDocumentosCheckIn_GV42",
   "d": "CheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLDocumentosCheckIn_GV42",
   "d": "DibujoDocumento_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLDocumentosCheckIn_GV42",
   "d": "DisenioEtiquetaEquipaje_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLDocumentosCheckIn_GV42",
   "d": "DisenioTarjetaEmbarque_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLDocumentosCheckIn_GV42",
   "d": "GeneradorPdf_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLDocumentosCheckIn_GV42",
   "d": "ILienzo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLDocumentosCheckIn_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLDocumentosCheckIn_GV42",
   "d": "LienzoPdf_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLDocumentosCheckIn_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DibujoDocumento_GV42",
   "d": "CheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DibujoDocumento_GV42",
   "d": "ILienzo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DibujoDocumento_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DibujoDocumento_GV42",
   "d": "Vuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DisenioEtiquetaEquipaje_GV42",
   "d": "CheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DisenioEtiquetaEquipaje_GV42",
   "d": "Code128_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DisenioEtiquetaEquipaje_GV42",
   "d": "ILienzo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DisenioEtiquetaEquipaje_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DisenioEtiquetaEquipaje_GV42",
   "d": "TarjetaEmbarque_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DisenioTarjetaEmbarque_GV42",
   "d": "Aeropuerto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DisenioTarjetaEmbarque_GV42",
   "d": "AsistenciaEspecial_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DisenioTarjetaEmbarque_GV42",
   "d": "BLLCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DisenioTarjetaEmbarque_GV42",
   "d": "CargoExcesoEquipaje_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DisenioTarjetaEmbarque_GV42",
   "d": "CheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DisenioTarjetaEmbarque_GV42",
   "d": "Code128_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DisenioTarjetaEmbarque_GV42",
   "d": "Equipaje_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DisenioTarjetaEmbarque_GV42",
   "d": "ILienzo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DisenioTarjetaEmbarque_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DisenioTarjetaEmbarque_GV42",
   "d": "TarjetaEmbarque_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DisenioTarjetaEmbarque_GV42",
   "d": "Vuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLNegocioUtil_GV42",
   "d": "BLLBitacora_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLNegocioUtil_GV42",
   "d": "BLLPermisos_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLNegocioUtil_GV42",
   "d": "DatosTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLNegocioUtil_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLNegocioUtil_GV42",
   "d": "MedioPago_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLNegocioUtil_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLNegocioUtil_GV42",
   "d": "Persona_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLNegocioUtil_GV42",
   "d": "Rol_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLNegocioUtil_GV42",
   "d": "SessionManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLNegocioUtil_GV42",
   "d": "Usuario_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLNegocioUtil_GV42",
   "d": "Validaciones_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "AdicionalReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "Aeropuerto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "AsientoDisponibilidad_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "AsientoPasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "Asiento_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "BLLIntegridad_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "BLLNegocioUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "BLLPermisos_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "BLLUsuario_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "Boleto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "CanalVenta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "CotizacionCambio_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "CriterioBusquedaVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "DALAeropuerto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "DALAsiento_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "DALBoleto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "DALPago_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "DALPasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "DALReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "DALTarifa_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "DALTipoAdicional_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "DALVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "DatosTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "EstadoReembolso_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "EstadoReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "MedioPago_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "Pago_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "Pasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "Persona_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "PrecioFecha_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "Reembolso_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "Reserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "Rol_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "SessionManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "TarifaFamilia_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "TipoAdicional_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "TipoPasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "TipoViaje_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "Usuario_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "Validaciones_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "VueloClase_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "Vuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "TemaAyuda_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLAyuda_GV42",
   "d": "GeneradorPdf_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLAyuda_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLAyuda_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLAyuda_GV42",
   "d": "TemaAyuda_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReinstalador_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReinstalador_GV42",
   "d": "BLLInstalador_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReinstalador_GV42",
   "d": "BLLIntegridad_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReinstalador_GV42",
   "d": "BLLPermisos_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReinstalador_GV42",
   "d": "DALUsuario_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReinstalador_GV42",
   "d": "Encriptador_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReinstalador_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReinstalador_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReinstalador_GV42",
   "d": "Rol_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReinstalador_GV42",
   "d": "Usuario_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteCheckIn_GV42",
   "d": "BLLCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteCheckIn_GV42",
   "d": "BLLNegocioUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteCheckIn_GV42",
   "d": "DALReporteCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteCheckIn_GV42",
   "d": "EstadoAsistencia_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteCheckIn_GV42",
   "d": "EstadoCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteCheckIn_GV42",
   "d": "FiltroReporteCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteCheckIn_GV42",
   "d": "GeneradorPdf_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteCheckIn_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteCheckIn_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteCheckIn_GV42",
   "d": "ReporteCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteCheckIn_GV42",
   "d": "Validaciones_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteMillas_GV42",
   "d": "BLLNegocioUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteMillas_GV42",
   "d": "BLLOfertasCliente_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteMillas_GV42",
   "d": "CategoriaMillas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteMillas_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteMillas_GV42",
   "d": "DALReporteMillas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteMillas_GV42",
   "d": "FiltroReporteMillas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteMillas_GV42",
   "d": "GeneradorPdf_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteMillas_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteMillas_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteMillas_GV42",
   "d": "PeriodoMillas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteMillas_GV42",
   "d": "ReporteMillas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteMillas_GV42",
   "d": "RiesgoAbandono_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteMillas_GV42",
   "d": "TendenciaMillas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteMillas_GV42",
   "d": "Validaciones_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteMillas_GV42",
   "d": "VueloVolado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLOfertasCliente_GV42",
   "d": "BLLNegocioUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLOfertasCliente_GV42",
   "d": "BLLReporteMillas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLOfertasCliente_GV42",
   "d": "BLLReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLOfertasCliente_GV42",
   "d": "CategoriaMillas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLOfertasCliente_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLOfertasCliente_GV42",
   "d": "ContratacionAdicional_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLOfertasCliente_GV42",
   "d": "DALReporteMillas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLOfertasCliente_GV42",
   "d": "DALTipoAdicional_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLOfertasCliente_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLOfertasCliente_GV42",
   "d": "MotivoOferta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLOfertasCliente_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLOfertasCliente_GV42",
   "d": "OfertaCliente_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLOfertasCliente_GV42",
   "d": "OfertasCliente_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLOfertasCliente_GV42",
   "d": "ReglaAsociacion_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLOfertasCliente_GV42",
   "d": "ReporteMillas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLOfertasCliente_GV42",
   "d": "SessionManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLOfertasCliente_GV42",
   "d": "TipoAdicional_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLOfertasCliente_GV42",
   "d": "Usuario_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLOfertasCliente_GV42",
   "d": "VueloClase_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLOfertasCliente_GV42",
   "d": "VueloVolado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLOfertasCliente_GV42",
   "d": "Vuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteReserva_GV42",
   "d": "BLLNegocioUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteReserva_GV42",
   "d": "DALReporteReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteReserva_GV42",
   "d": "EstadoReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteReserva_GV42",
   "d": "FechaReporte_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteReserva_GV42",
   "d": "FiltroReporteReservas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteReserva_GV42",
   "d": "GeneradorPdf_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteReserva_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteReserva_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteReserva_GV42",
   "d": "ReporteReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReporteReserva_GV42",
   "d": "Validaciones_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLVuelo_GV42",
   "d": "Aeropuerto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLVuelo_GV42",
   "d": "BLLNegocioUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLVuelo_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLVuelo_GV42",
   "d": "DALAeropuerto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLVuelo_GV42",
   "d": "DALVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLVuelo_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLVuelo_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLVuelo_GV42",
   "d": "VueloClase_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLVuelo_GV42",
   "d": "Vuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCliente_GV42",
   "d": "BLLNegocioUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCliente_GV42",
   "d": "ClienteXml_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCliente_GV42",
   "d": "Cliente_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCliente_GV42",
   "d": "DALCliente_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCliente_GV42",
   "d": "DALPasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCliente_GV42",
   "d": "EstiloXmlClientes_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCliente_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCliente_GV42",
   "d": "ListaClientesXml_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCliente_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCliente_GV42",
   "d": "Pasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCliente_GV42",
   "d": "SerializadorXml_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLCliente_GV42",
   "d": "Validaciones_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLAerolinea_GV42",
   "d": "BLLNegocioUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLAerolinea_GV42",
   "d": "DALAerolinea_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLAerolinea_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLAerolinea_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLAerolinea_GV42",
   "d": "Validaciones_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLAeropuerto_GV42",
   "d": "Aeropuerto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLAeropuerto_GV42",
   "d": "BLLNegocioUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLAeropuerto_GV42",
   "d": "DALAeropuerto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLAeropuerto_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLAeropuerto_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLAeropuerto_GV42",
   "d": "Validaciones_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLVueloHistorial_GV42",
   "d": "BLLNegocioUtil_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLVueloHistorial_GV42",
   "d": "BLLVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLVueloHistorial_GV42",
   "d": "DALVueloHistorial_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLVueloHistorial_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLVueloHistorial_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLVueloHistorial_GV42",
   "d": "VueloCambio_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLVueloHistorial_GV42",
   "d": "Vuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMAviso_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMAviso_GV42",
   "d": "PanelEncabezado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBackupManual",
   "d": "BLLIntegridad_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBackupManual",
   "d": "BLLPermisos_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBackupManual",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBackupManual",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBackupManual",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBackupManual",
   "d": "PanelEncabezado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBackupManual",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBackupManual",
   "d": "SessionManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBitacoraDeEventos",
   "d": "BLLBitacora_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBitacoraDeEventos",
   "d": "BLLPermisos_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBitacoraDeEventos",
   "d": "BLLUsuario_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBitacoraDeEventos",
   "d": "Bitacora_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBitacoraDeEventos",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBitacoraDeEventos",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBitacoraDeEventos",
   "d": "GeneradorPdf_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBitacoraDeEventos",
   "d": "GrillaModerna_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBitacoraDeEventos",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBitacoraDeEventos",
   "d": "PanelEncabezado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBitacoraDeEventos",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBitacoraDeEventos",
   "d": "Rol_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBitacoraDeEventos",
   "d": "SessionManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBitacoraDeEventos",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBitacoraDeEventos",
   "d": "TextBoxModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBitacoraDeEventos",
   "d": "Usuario_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBitacoraDeEventos",
   "d": "Validaciones_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarContrasenia",
   "d": "BLLUsuario_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarContrasenia",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarContrasenia",
   "d": "FRMIniciarSesion",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarContrasenia",
   "d": "FRMMenuPrincipalAdmin",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarContrasenia",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarContrasenia",
   "d": "PanelEncabezado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarContrasenia",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarContrasenia",
   "d": "SessionManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarContrasenia",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarContrasenia",
   "d": "TextBoxModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarContrasenia",
   "d": "Usuario_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarContrasenia",
   "d": "Validaciones_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIniciarSesion",
   "d": "BLLAyuda_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIniciarSesion",
   "d": "BLLBitacora_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIniciarSesion",
   "d": "BLLInstalador_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIniciarSesion",
   "d": "BLLIntegridad_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIniciarSesion",
   "d": "BLLPermisos_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIniciarSesion",
   "d": "BLLUsuario_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIniciarSesion",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIniciarSesion",
   "d": "Entregas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIniciarSesion",
   "d": "FRMAyuda_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIniciarSesion",
   "d": "FRMCambiarContrasenia",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIniciarSesion",
   "d": "FRMIntegridad",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIniciarSesion",
   "d": "FRMMenuPrincipalAdmin",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIniciarSesion",
   "d": "FRMRegistroCliente_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIniciarSesion",
   "d": "FRMReinstalador_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIniciarSesion",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIniciarSesion",
   "d": "PanelEncabezado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIniciarSesion",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIniciarSesion",
   "d": "ResultadoIntegridad",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIniciarSesion",
   "d": "Rol_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIniciarSesion",
   "d": "SessionManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIniciarSesion",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIniciarSesion",
   "d": "TextBoxModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIniciarSesion",
   "d": "Usuario_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIniciarSesion",
   "d": "Validaciones_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "BLLAyuda_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "BLLCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "BLLPermisos_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "BLLReporteMillas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "BLLUsuario_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "Entregas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "FRMAyuda_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "FRMBackupManual",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "FRMBitacoraDeEventos",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "FRMBitacoraVuelos_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "FRMCambiarContrasenia",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "FRMCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "FRMConsultarReservas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "FRMGestionPermisos",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "FRMGestionUsuariosAdmin",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "FRMGestionVuelos_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "FRMIniciarSesion",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "FRMMaestroAerolineas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "FRMMaestroAeropuertos_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "FRMMaestroClientes_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "FRMPagoReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "FRMReporteCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "FRMReporteMillas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "FRMReporteReservas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "FRMReservarVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "MenuModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "Rol_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "SessionManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalAdmin",
   "d": "Usuario_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalUsuario",
   "d": "BLLUsuario_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalUsuario",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalUsuario",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalUsuario",
   "d": "FRMCambiarContrasenia",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalUsuario",
   "d": "FRMIniciarSesion",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalUsuario",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalUsuario",
   "d": "MenuModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalUsuario",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalUsuario",
   "d": "SessionManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMenuPrincipalUsuario",
   "d": "Usuario_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionUsuariosAdmin",
   "d": "BLLPermisos_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionUsuariosAdmin",
   "d": "BLLUsuario_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionUsuariosAdmin",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionUsuariosAdmin",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionUsuariosAdmin",
   "d": "GrillaModerna_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionUsuariosAdmin",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionUsuariosAdmin",
   "d": "PanelEncabezado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionUsuariosAdmin",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionUsuariosAdmin",
   "d": "Rol_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionUsuariosAdmin",
   "d": "SessionManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionUsuariosAdmin",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionUsuariosAdmin",
   "d": "TextBoxModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionUsuariosAdmin",
   "d": "Usuario_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionUsuariosAdmin",
   "d": "Validaciones_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionPermisos",
   "d": "BLLPermisos_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionPermisos",
   "d": "BLLUsuario_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionPermisos",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionPermisos",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionPermisos",
   "d": "FRMIniciarSesion",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionPermisos",
   "d": "FRMMenuPrincipalAdmin",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionPermisos",
   "d": "Familia_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionPermisos",
   "d": "GrillaModerna_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionPermisos",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionPermisos",
   "d": "PanelEncabezado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionPermisos",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionPermisos",
   "d": "Patente_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionPermisos",
   "d": "Rol_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionPermisos",
   "d": "SessionManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionPermisos",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionPermisos",
   "d": "TextBoxModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionPermisos",
   "d": "Usuario_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionPermisos",
   "d": "Validaciones_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIntegridad",
   "d": "BLLIntegridad_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIntegridad",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIntegridad",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIntegridad",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIntegridad",
   "d": "PanelEncabezado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIntegridad",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIntegridad",
   "d": "ResultadoIntegridad",
   "tipo": "Dependency"
  },
  {
   "o": "FRMIntegridad",
   "d": "TipoTampering",
   "tipo": "Dependency"
  },
  {
   "o": "FRMSeleccionInstancia",
   "d": "BLLInstalador_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMSeleccionInstancia",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMSeleccionInstancia",
   "d": "ConfiguracionBD_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMSeleccionInstancia",
   "d": "DetectorInstancias_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMSeleccionInstancia",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMSeleccionInstancia",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMSeleccionInstancia",
   "d": "PanelEncabezado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMSeleccionInstancia",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMRegistroCliente_GV42",
   "d": "BLLAyuda_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMRegistroCliente_GV42",
   "d": "BLLReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMRegistroCliente_GV42",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMRegistroCliente_GV42",
   "d": "Entregas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMRegistroCliente_GV42",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMRegistroCliente_GV42",
   "d": "FRMAyuda_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMRegistroCliente_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMRegistroCliente_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMRegistroCliente_GV42",
   "d": "PanelEncabezado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMRegistroCliente_GV42",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMRegistroCliente_GV42",
   "d": "Pasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMRegistroCliente_GV42",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMRegistroCliente_GV42",
   "d": "TextBoxModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMRegistroCliente_GV42",
   "d": "Validaciones_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "AdicionalReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "Aeropuerto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "AsientoDisponibilidad_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "AsientoPasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "Asiento_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "AsistenciaEspecial_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "BLLOfertasCliente_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "BLLReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "CriterioBusquedaVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "CtrlAdicional_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "CtrlButacas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "CtrlEquipajePasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "CtrlFechasFlexibles_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "CtrlOfertasCliente_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "CtrlPasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "CtrlTarifa_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "Entregas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "FRMPagoReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "GrillaModerna_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "OfertaCliente_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "OfertasCliente_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "PanelEncabezado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "Pasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "PrecioFecha_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "Reserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "SessionManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "TarifaFamilia_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "TextBoxModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "TipoAdicional_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "TipoPasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "TipoViaje_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "Usuario_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "Validaciones_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "VueloClase_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "Vuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMPagoReserva_GV42",
   "d": "BLLReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMPagoReserva_GV42",
   "d": "Boleto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMPagoReserva_GV42",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMPagoReserva_GV42",
   "d": "CtrlVistaTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMPagoReserva_GV42",
   "d": "DatosTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMPagoReserva_GV42",
   "d": "EstadoReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMPagoReserva_GV42",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMPagoReserva_GV42",
   "d": "FRMBoletos_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMPagoReserva_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMPagoReserva_GV42",
   "d": "MedioPago_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMPagoReserva_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMPagoReserva_GV42",
   "d": "Pago_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMPagoReserva_GV42",
   "d": "PanelEncabezado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMPagoReserva_GV42",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMPagoReserva_GV42",
   "d": "Reserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMPagoReserva_GV42",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMPagoReserva_GV42",
   "d": "TextBoxModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMPagoReserva_GV42",
   "d": "Validaciones_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMPagoReserva_GV42",
   "d": "VueloClase_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "Tema_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMTarjetaEmbarque_GV42",
   "d": "BLLDocumentosCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMTarjetaEmbarque_GV42",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMTarjetaEmbarque_GV42",
   "d": "CheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMTarjetaEmbarque_GV42",
   "d": "CtrlEtiquetaEquipaje_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMTarjetaEmbarque_GV42",
   "d": "CtrlTarjetaEmbarque_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMTarjetaEmbarque_GV42",
   "d": "DisenioTarjetaEmbarque_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMTarjetaEmbarque_GV42",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMTarjetaEmbarque_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMTarjetaEmbarque_GV42",
   "d": "LienzoGdi_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMTarjetaEmbarque_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMTarjetaEmbarque_GV42",
   "d": "PanelEncabezado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMTarjetaEmbarque_GV42",
   "d": "TarjetaEmbarque_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlTarjetaEmbarque_GV42",
   "d": "CheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlTarjetaEmbarque_GV42",
   "d": "DisenioTarjetaEmbarque_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlTarjetaEmbarque_GV42",
   "d": "LienzoGdi_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlTarjetaEmbarque_GV42",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlEtiquetaEquipaje_GV42",
   "d": "CheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlEtiquetaEquipaje_GV42",
   "d": "DisenioEtiquetaEquipaje_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlEtiquetaEquipaje_GV42",
   "d": "LienzoGdi_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlEtiquetaEquipaje_GV42",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "AdicionalReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "AsientoDisponibilidad_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "Asiento_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "AsistenciaEspecial_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "BLLCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "CargoExcesoEquipaje_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "CheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "CtrlButacas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "CtrlNavegadorPasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "CtrlPesoValija_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "CtrlVistaTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "DatosTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "Equipaje_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "EstadoCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "EstadoReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "FRMTarjetaEmbarque_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "FranquiciaEquipaje_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "GrillaModerna_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "MedioPago_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "PanelEncabezado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "Reserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "TarjetaEmbarque_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "TextBoxModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "Validaciones_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCheckIn_GV42",
   "d": "Vuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BotonModerno_GV42",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BotonModerno_GV42",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "PanelTarjeta_GV42",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "TextBoxModerno_GV42",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "MenuModerno_GV42",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlBoleto_GV42",
   "d": "BoletoElectronico_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlBoleto_GV42",
   "d": "DisenioBoleto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlBoleto_GV42",
   "d": "LienzoGdi_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlBoleto_GV42",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBoletos_GV42",
   "d": "BLLBoleto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBoletos_GV42",
   "d": "BoletoElectronico_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBoletos_GV42",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBoletos_GV42",
   "d": "CtrlBoleto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBoletos_GV42",
   "d": "DisenioBoleto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBoletos_GV42",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBoletos_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBoletos_GV42",
   "d": "LienzoGdi_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBoletos_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBoletos_GV42",
   "d": "PanelEncabezado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlButacas_GV42",
   "d": "AsientoDisponibilidad_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlButacas_GV42",
   "d": "Asiento_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlButacas_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlButacas_GV42",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlPasajero_GV42",
   "d": "AsistenciaEspecial_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlPasajero_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlPasajero_GV42",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlPasajero_GV42",
   "d": "Pasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlPasajero_GV42",
   "d": "TextBoxModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlPasajero_GV42",
   "d": "TipoPasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlOfertasCliente_GV42",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlOfertasCliente_GV42",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlOfertasCliente_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlOfertasCliente_GV42",
   "d": "OfertaCliente_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlOfertasCliente_GV42",
   "d": "OfertasCliente_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlOfertasCliente_GV42",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlAdicional_GV42",
   "d": "BLLReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlAdicional_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlAdicional_GV42",
   "d": "TipoAdicional_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlEquipajePasajero_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlVistaTarjeta_GV42",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionVuelos_GV42",
   "d": "Aeropuerto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionVuelos_GV42",
   "d": "BLLVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionVuelos_GV42",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionVuelos_GV42",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionVuelos_GV42",
   "d": "FRMAltaVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionVuelos_GV42",
   "d": "GrillaModerna_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionVuelos_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionVuelos_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionVuelos_GV42",
   "d": "PanelEncabezado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionVuelos_GV42",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionVuelos_GV42",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionVuelos_GV42",
   "d": "TextBoxModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMGestionVuelos_GV42",
   "d": "Vuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroClientes_GV42",
   "d": "BLLCliente_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroClientes_GV42",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroClientes_GV42",
   "d": "Cliente_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroClientes_GV42",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroClientes_GV42",
   "d": "GrillaModerna_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroClientes_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroClientes_GV42",
   "d": "PanelEncabezado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroClientes_GV42",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroClientes_GV42",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroClientes_GV42",
   "d": "TextBoxModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroClientes_GV42",
   "d": "Validaciones_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroAerolineas_GV42",
   "d": "BLLAerolinea_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroAerolineas_GV42",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroAerolineas_GV42",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroAerolineas_GV42",
   "d": "GrillaModerna_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroAerolineas_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroAerolineas_GV42",
   "d": "PanelEncabezado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroAerolineas_GV42",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroAerolineas_GV42",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroAerolineas_GV42",
   "d": "TextBoxModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroAeropuertos_GV42",
   "d": "Aeropuerto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroAeropuertos_GV42",
   "d": "BLLAeropuerto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroAeropuertos_GV42",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroAeropuertos_GV42",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroAeropuertos_GV42",
   "d": "GrillaModerna_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroAeropuertos_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroAeropuertos_GV42",
   "d": "PanelEncabezado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroAeropuertos_GV42",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroAeropuertos_GV42",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMMaestroAeropuertos_GV42",
   "d": "TextBoxModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMAyuda_GV42",
   "d": "BLLAyuda_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMAyuda_GV42",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMAyuda_GV42",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMAyuda_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMAyuda_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMAyuda_GV42",
   "d": "PanelEncabezado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMAyuda_GV42",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMAyuda_GV42",
   "d": "TemaAyuda_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReinstalador_GV42",
   "d": "BLLAyuda_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReinstalador_GV42",
   "d": "BLLIntegridad_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReinstalador_GV42",
   "d": "BLLReinstalador_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReinstalador_GV42",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReinstalador_GV42",
   "d": "Entregas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReinstalador_GV42",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReinstalador_GV42",
   "d": "FRMAyuda_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReinstalador_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReinstalador_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReinstalador_GV42",
   "d": "PanelEncabezado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReinstalador_GV42",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReinstalador_GV42",
   "d": "TextBoxModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReinstalador_GV42",
   "d": "Validaciones_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteMillas_GV42",
   "d": "BLLReporteMillas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteMillas_GV42",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteMillas_GV42",
   "d": "CategoriaMillas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteMillas_GV42",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteMillas_GV42",
   "d": "FiltroReporteMillas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteMillas_GV42",
   "d": "GrillaModerna_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteMillas_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteMillas_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteMillas_GV42",
   "d": "PanelEncabezado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteMillas_GV42",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteMillas_GV42",
   "d": "PeriodoMillas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteMillas_GV42",
   "d": "ReporteMillas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteMillas_GV42",
   "d": "RiesgoAbandono_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteMillas_GV42",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteMillas_GV42",
   "d": "TendenciaMillas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteMillas_GV42",
   "d": "TextBoxModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBitacoraVuelos_GV42",
   "d": "BLLVueloHistorial_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBitacoraVuelos_GV42",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBitacoraVuelos_GV42",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBitacoraVuelos_GV42",
   "d": "GrillaModerna_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBitacoraVuelos_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBitacoraVuelos_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBitacoraVuelos_GV42",
   "d": "PanelEncabezado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBitacoraVuelos_GV42",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMBitacoraVuelos_GV42",
   "d": "VueloCambio_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMConsultarReservas_GV42",
   "d": "BLLCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMConsultarReservas_GV42",
   "d": "BLLReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMConsultarReservas_GV42",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMConsultarReservas_GV42",
   "d": "EstadoReembolso_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMConsultarReservas_GV42",
   "d": "EstadoReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMConsultarReservas_GV42",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMConsultarReservas_GV42",
   "d": "FRMBoletos_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMConsultarReservas_GV42",
   "d": "FRMCambiarVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMConsultarReservas_GV42",
   "d": "FRMCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMConsultarReservas_GV42",
   "d": "FRMMenuPrincipalAdmin",
   "tipo": "Dependency"
  },
  {
   "o": "FRMConsultarReservas_GV42",
   "d": "GrillaModerna_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMConsultarReservas_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMConsultarReservas_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMConsultarReservas_GV42",
   "d": "PanelEncabezado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMConsultarReservas_GV42",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMConsultarReservas_GV42",
   "d": "Reserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMConsultarReservas_GV42",
   "d": "TarifaFamilia_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMConsultarReservas_GV42",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMConsultarReservas_GV42",
   "d": "TextBoxModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMAltaVuelo_GV42",
   "d": "Aeropuerto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMAltaVuelo_GV42",
   "d": "BLLVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMAltaVuelo_GV42",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMAltaVuelo_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMAltaVuelo_GV42",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMAltaVuelo_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMAltaVuelo_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMAltaVuelo_GV42",
   "d": "PanelEncabezado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMAltaVuelo_GV42",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMAltaVuelo_GV42",
   "d": "TextBoxModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMAltaVuelo_GV42",
   "d": "Vuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteCheckIn_GV42",
   "d": "BLLReporteCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteCheckIn_GV42",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteCheckIn_GV42",
   "d": "EstadoAsistencia_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteCheckIn_GV42",
   "d": "EstadoCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteCheckIn_GV42",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteCheckIn_GV42",
   "d": "FiltroReporteCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteCheckIn_GV42",
   "d": "GrillaModerna_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteCheckIn_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteCheckIn_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteCheckIn_GV42",
   "d": "PanelEncabezado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteCheckIn_GV42",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteCheckIn_GV42",
   "d": "ReporteCheckIn_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteCheckIn_GV42",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteCheckIn_GV42",
   "d": "TextBoxModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteReservas_GV42",
   "d": "BLLReporteReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteReservas_GV42",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteReservas_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteReservas_GV42",
   "d": "EstadoReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteReservas_GV42",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteReservas_GV42",
   "d": "FechaReporte_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteReservas_GV42",
   "d": "FiltroReporteReservas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteReservas_GV42",
   "d": "GrillaModerna_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteReservas_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteReservas_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteReservas_GV42",
   "d": "PanelEncabezado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteReservas_GV42",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteReservas_GV42",
   "d": "ReporteReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteReservas_GV42",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReporteReservas_GV42",
   "d": "TextBoxModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlNavegadorPasajero_GV42",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlNavegadorPasajero_GV42",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarVuelo_GV42",
   "d": "Aeropuerto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarVuelo_GV42",
   "d": "AsientoDisponibilidad_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarVuelo_GV42",
   "d": "AsientoPasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarVuelo_GV42",
   "d": "Asiento_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarVuelo_GV42",
   "d": "BLLReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarVuelo_GV42",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarVuelo_GV42",
   "d": "CotizacionCambio_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarVuelo_GV42",
   "d": "CtrlButacas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarVuelo_GV42",
   "d": "DatosTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarVuelo_GV42",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarVuelo_GV42",
   "d": "GrillaModerna_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarVuelo_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarVuelo_GV42",
   "d": "MedioPago_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarVuelo_GV42",
   "d": "NegocioException_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarVuelo_GV42",
   "d": "PanelEncabezado_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarVuelo_GV42",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarVuelo_GV42",
   "d": "Pasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarVuelo_GV42",
   "d": "Reserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarVuelo_GV42",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarVuelo_GV42",
   "d": "TextBoxModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarVuelo_GV42",
   "d": "Validaciones_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarVuelo_GV42",
   "d": "VueloClase_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMCambiarVuelo_GV42",
   "d": "Vuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlFechasFlexibles_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlFechasFlexibles_GV42",
   "d": "PrecioFecha_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlFechasFlexibles_GV42",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlTarifa_GV42",
   "d": "BotonModerno_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlTarifa_GV42",
   "d": "EstiloBoton_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlTarifa_GV42",
   "d": "IdiomaManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlTarifa_GV42",
   "d": "PanelTarjeta_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlTarifa_GV42",
   "d": "TarifaFamilia_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "CtrlTarifa_GV42",
   "d": "Tema_GV42",
   "tipo": "Dependency"
  }
 ],
 "tablas": {
  "Aerolinea": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": true
    },
    {
     "n": "Nombre",
     "t": "nvarchar",
     "l": "80",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": []
  },
  "Aeropuerto": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": true
    },
    {
     "n": "CodigoIata",
     "t": "char",
     "l": "3",
     "nn": true,
     "id": false
    },
    {
     "n": "Nombre",
     "t": "nvarchar",
     "l": "100",
     "nn": true,
     "id": false
    },
    {
     "n": "Ciudad",
     "t": "nvarchar",
     "l": "60",
     "nn": true,
     "id": false
    },
    {
     "n": "Pais",
     "t": "nvarchar",
     "l": "60",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": []
  },
  "Asiento": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": true
    },
    {
     "n": "IdVuelo",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "Fila",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "Letra",
     "t": "char",
     "l": "1",
     "nn": true,
     "id": false
    },
    {
     "n": "NumeroAsiento",
     "t": "computed",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "IdClase",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "Ubicacion",
     "t": "nvarchar",
     "l": "10",
     "nn": true,
     "id": false
    },
    {
     "n": "EsPreferencial",
     "t": "computed",
     "l": "",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": [
    {
     "n": "FK_Asiento_Clase",
     "cols": [
      "IdClase"
     ],
     "ref": "ClaseVuelo",
     "rcols": [
      "Id"
     ]
    },
    {
     "n": "FK_Asiento_Vuelo",
     "cols": [
      "IdVuelo"
     ],
     "ref": "Vuelo",
     "rcols": [
      "Id"
     ]
    }
   ]
  },
  "Boleto": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": true
    },
    {
     "n": "NumeroBoleto",
     "t": "computed",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "IdReserva",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "DniPasajero",
     "t": "nvarchar",
     "l": "20",
     "nn": true,
     "id": false
    },
    {
     "n": "FechaEmision",
     "t": "datetime2",
     "l": "0",
     "nn": true,
     "id": false
    },
    {
     "n": "Tramo",
     "t": "tinyint",
     "l": "",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": [
    {
     "n": "FK_Boleto_ReservaPasajero",
     "cols": [
      "IdReserva",
      "DniPasajero"
     ],
     "ref": "ReservaPasajero",
     "rcols": [
      "IdReserva",
      "DniPasajero"
     ]
    }
   ]
  },
  "CanalVenta": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "Nombre",
     "t": "nvarchar",
     "l": "30",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": []
  },
  "CargoExcesoEquipaje": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": true
    },
    {
     "n": "IdEquipaje",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "KilosExceso",
     "t": "decimal",
     "l": "7,2",
     "nn": true,
     "id": false
    },
    {
     "n": "CostoPorKilo",
     "t": "decimal",
     "l": "10,2",
     "nn": true,
     "id": false
    },
    {
     "n": "ImporteCargo",
     "t": "decimal",
     "l": "12,2",
     "nn": true,
     "id": false
    },
    {
     "n": "IdMedioPago",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "NumeroTransaccion",
     "t": "nvarchar",
     "l": "40",
     "nn": false,
     "id": false
    },
    {
     "n": "FechaHoraCobro",
     "t": "datetime2",
     "l": "0",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": [
    {
     "n": "FK_Cargo_Equipaje",
     "cols": [
      "IdEquipaje"
     ],
     "ref": "Equipaje",
     "rcols": [
      "Id"
     ]
    },
    {
     "n": "FK_Cargo_Medio",
     "cols": [
      "IdMedioPago"
     ],
     "ref": "MedioPago",
     "rcols": [
      "Id"
     ]
    }
   ]
  },
  "CheckIn": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": true
    },
    {
     "n": "IdReserva",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "DniPasajero",
     "t": "nvarchar",
     "l": "20",
     "nn": true,
     "id": false
    },
    {
     "n": "IdEstadoCheckIn",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "FechaHoraCheckIn",
     "t": "datetime2",
     "l": "0",
     "nn": false,
     "id": false
    },
    {
     "n": "IdAsiento",
     "t": "int",
     "l": "",
     "nn": false,
     "id": false
    },
    {
     "n": "LoginEncargado",
     "t": "nvarchar",
     "l": "50",
     "nn": false,
     "id": false
    },
    {
     "n": "IdCanal",
     "t": "int",
     "l": "",
     "nn": false,
     "id": false
    },
    {
     "n": "Tramo",
     "t": "tinyint",
     "l": "",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": [
    {
     "n": "FK_CheckIn_Asiento",
     "cols": [
      "IdAsiento"
     ],
     "ref": "Asiento",
     "rcols": [
      "Id"
     ]
    },
    {
     "n": "FK_CheckIn_Estado",
     "cols": [
      "IdEstadoCheckIn"
     ],
     "ref": "EstadoCheckIn",
     "rcols": [
      "Id"
     ]
    },
    {
     "n": "FK_CheckIn_ReservaPasajero",
     "cols": [
      "IdReserva",
      "DniPasajero"
     ],
     "ref": "ReservaPasajero",
     "rcols": [
      "IdReserva",
      "DniPasajero"
     ]
    }
   ]
  },
  "ClaseVuelo": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "Nombre",
     "t": "nvarchar",
     "l": "30",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": []
  },
  "Equipaje": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": true
    },
    {
     "n": "IdCheckIn",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "CantidadBultos",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "PesoTotalKg",
     "t": "decimal",
     "l": "7,2",
     "nn": true,
     "id": false
    },
    {
     "n": "FranquiciaKg",
     "t": "decimal",
     "l": "7,2",
     "nn": true,
     "id": false
    },
    {
     "n": "UnidadesExtra",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": [
    {
     "n": "FK_Equipaje_CheckIn",
     "cols": [
      "IdCheckIn"
     ],
     "ref": "CheckIn",
     "rcols": [
      "Id"
     ]
    }
   ]
  },
  "EstadoCheckIn": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "Nombre",
     "t": "nvarchar",
     "l": "30",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": []
  },
  "EstadoReserva": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "Nombre",
     "t": "nvarchar",
     "l": "30",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": []
  },
  "EtiquetaEquipaje": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": true
    },
    {
     "n": "IdEquipaje",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "CodigoEquipaje",
     "t": "nvarchar",
     "l": "20",
     "nn": true,
     "id": false
    },
    {
     "n": "PesoKg",
     "t": "decimal",
     "l": "5,2",
     "nn": false,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": [
    {
     "n": "FK_Etiqueta_Equipaje",
     "cols": [
      "IdEquipaje"
     ],
     "ref": "Equipaje",
     "rcols": [
      "Id"
     ]
    }
   ]
  },
  "EVENTOS": {
   "cols": [
    {
     "n": "Id_Evento",
     "t": "int",
     "l": "",
     "nn": true,
     "id": true
    },
    {
     "n": "UserName",
     "t": "nvarchar",
     "l": "250",
     "nn": true,
     "id": false
    },
    {
     "n": "Criticidad",
     "t": "nvarchar",
     "l": "50",
     "nn": true,
     "id": false
    },
    {
     "n": "FechaHora",
     "t": "datetime",
     "l": "",
     "nn": false,
     "id": false
    },
    {
     "n": "IdModulo",
     "t": "int",
     "l": "",
     "nn": false,
     "id": false
    },
    {
     "n": "IdTipoEvento",
     "t": "int",
     "l": "",
     "nn": false,
     "id": false
    },
    {
     "n": "Detalle",
     "t": "nvarchar",
     "l": "500",
     "nn": false,
     "id": false
    }
   ],
   "pk": [
    "Id_Evento"
   ],
   "fk": [
    {
     "n": "FK_Eventos_Modulo",
     "cols": [
      "IdModulo"
     ],
     "ref": "Modulo",
     "rcols": [
      "Id"
     ]
    },
    {
     "n": "FK_Eventos_TipoEvento",
     "cols": [
      "IdTipoEvento"
     ],
     "ref": "TipoEvento",
     "rcols": [
      "Id"
     ]
    },
    {
     "n": "FK_Eventos_Usuario",
     "cols": [
      "UserName"
     ],
     "ref": "Usuario",
     "rcols": [
      "UserName"
     ]
    }
   ]
  },
  "Familia": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": true
    },
    {
     "n": "Nombre",
     "t": "nvarchar",
     "l": "100",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": []
  },
  "FamiliaIntegrada": {
   "cols": [
    {
     "n": "IdFamiliaPadre",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "IdFamiliaHija",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "IdFamiliaPadre",
    "IdFamiliaHija"
   ],
   "fk": [
    {
     "n": "FK_FamiliaIntegrada_Hija",
     "cols": [
      "IdFamiliaHija"
     ],
     "ref": "Familia",
     "rcols": [
      "Id"
     ]
    },
    {
     "n": "FK_FamiliaIntegrada_Padre",
     "cols": [
      "IdFamiliaPadre"
     ],
     "ref": "Familia",
     "rcols": [
      "Id"
     ]
    }
   ]
  },
  "FamiliaPatente": {
   "cols": [
    {
     "n": "IdFamilia",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "IdPatente",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "IdFamilia",
    "IdPatente"
   ],
   "fk": [
    {
     "n": "FK_FamiliaPatente_Familia",
     "cols": [
      "IdFamilia"
     ],
     "ref": "Familia",
     "rcols": [
      "Id"
     ]
    },
    {
     "n": "FK_FamiliaPatente_Patente",
     "cols": [
      "IdPatente"
     ],
     "ref": "Patente",
     "rcols": [
      "Id"
     ]
    }
   ]
  },
  "IntegridadDVH": {
   "cols": [
    {
     "n": "NombreTabla",
     "t": "nvarchar",
     "l": "50",
     "nn": true,
     "id": false
    },
    {
     "n": "IdRegistro",
     "t": "nvarchar",
     "l": "50",
     "nn": true,
     "id": false
    },
    {
     "n": "DVH",
     "t": "nvarchar",
     "l": "64",
     "nn": true,
     "id": false
    },
    {
     "n": "FechaCalculo",
     "t": "datetime",
     "l": "",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "NombreTabla",
    "IdRegistro"
   ],
   "fk": []
  },
  "IntegridadDVV": {
   "cols": [
    {
     "n": "NombreTabla",
     "t": "nvarchar",
     "l": "50",
     "nn": true,
     "id": false
    },
    {
     "n": "DVV",
     "t": "nvarchar",
     "l": "64",
     "nn": true,
     "id": false
    },
    {
     "n": "FechaCalculo",
     "t": "datetime",
     "l": "",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "NombreTabla"
   ],
   "fk": []
  },
  "MedioPago": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "Nombre",
     "t": "nvarchar",
     "l": "30",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": []
  },
  "Modulo": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": true
    },
    {
     "n": "Nombre",
     "t": "nvarchar",
     "l": "50",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": []
  },
  "Pago": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": true
    },
    {
     "n": "IdReserva",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "ImporteTotalAbonado",
     "t": "decimal",
     "l": "12,2",
     "nn": true,
     "id": false
    },
    {
     "n": "IdMedioPago",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "NumeroTransaccion",
     "t": "nvarchar",
     "l": "40",
     "nn": true,
     "id": false
    },
    {
     "n": "FechaHoraPago",
     "t": "datetime2",
     "l": "0",
     "nn": true,
     "id": false
    },
    {
     "n": "LoginVendedor",
     "t": "nvarchar",
     "l": "50",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": [
    {
     "n": "FK_Pago_Medio",
     "cols": [
      "IdMedioPago"
     ],
     "ref": "MedioPago",
     "rcols": [
      "Id"
     ]
    },
    {
     "n": "FK_Pago_Reserva",
     "cols": [
      "IdReserva"
     ],
     "ref": "Reserva",
     "rcols": [
      "Id"
     ]
    }
   ]
  },
  "Pasajero": {
   "cols": [
    {
     "n": "DNI",
     "t": "nvarchar",
     "l": "20",
     "nn": true,
     "id": false
    },
    {
     "n": "Nombre",
     "t": "nvarchar",
     "l": "60",
     "nn": true,
     "id": false
    },
    {
     "n": "Apellido",
     "t": "nvarchar",
     "l": "60",
     "nn": true,
     "id": false
    },
    {
     "n": "Email",
     "t": "nvarchar",
     "l": "500",
     "nn": true,
     "id": false
    },
    {
     "n": "Telefono",
     "t": "nvarchar",
     "l": "30",
     "nn": true,
     "id": false
    },
    {
     "n": "FechaAlta",
     "t": "datetime2",
     "l": "0",
     "nn": true,
     "id": false
    },
    {
     "n": "FechaNacimiento",
     "t": "date",
     "l": "",
     "nn": false,
     "id": false
    }
   ],
   "pk": [
    "DNI"
   ],
   "fk": []
  },
  "Patente": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": true
    },
    {
     "n": "Nombre",
     "t": "nvarchar",
     "l": "100",
     "nn": true,
     "id": false
    },
    {
     "n": "DataKey",
     "t": "nvarchar",
     "l": "100",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": []
  },
  "Reserva": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": true
    },
    {
     "n": "NumeroReserva",
     "t": "computed",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "DniCliente",
     "t": "nvarchar",
     "l": "20",
     "nn": true,
     "id": false
    },
    {
     "n": "IdVuelo",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "IdClase",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "IdTipoViaje",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "FechaRegreso",
     "t": "date",
     "l": "",
     "nn": false,
     "id": false
    },
    {
     "n": "CantidadPasajeros",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "ImporteBase",
     "t": "decimal",
     "l": "12,2",
     "nn": true,
     "id": false
    },
    {
     "n": "SubtotalAdicionales",
     "t": "decimal",
     "l": "12,2",
     "nn": true,
     "id": false
    },
    {
     "n": "Impuestos",
     "t": "decimal",
     "l": "12,2",
     "nn": true,
     "id": false
    },
    {
     "n": "ImporteTotal",
     "t": "decimal",
     "l": "12,2",
     "nn": true,
     "id": false
    },
    {
     "n": "IdEstadoReserva",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "FechaRealizacion",
     "t": "datetime2",
     "l": "0",
     "nn": true,
     "id": false
    },
    {
     "n": "LoginVendedor",
     "t": "nvarchar",
     "l": "50",
     "nn": true,
     "id": false
    },
    {
     "n": "IdCanalVenta",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "FechaCancelacion",
     "t": "datetime2",
     "l": "0",
     "nn": false,
     "id": false
    },
    {
     "n": "MontoPenalidadCancelacion",
     "t": "decimal",
     "l": "12,2",
     "nn": false,
     "id": false
    },
    {
     "n": "IdVueloVuelta",
     "t": "int",
     "l": "",
     "nn": false,
     "id": false
    },
    {
     "n": "IdClaseVuelta",
     "t": "int",
     "l": "",
     "nn": false,
     "id": false
    },
    {
     "n": "FechaVencimiento",
     "t": "datetime2",
     "l": "0",
     "nn": false,
     "id": false
    },
    {
     "n": "VencidaSinPago",
     "t": "bit",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "CantidadAsientos",
     "t": "int",
     "l": "",
     "nn": false,
     "id": false
    },
    {
     "n": "IdTarifa",
     "t": "int",
     "l": "",
     "nn": false,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": [
    {
     "n": "FK_Reserva_CanalVenta",
     "cols": [
      "IdCanalVenta"
     ],
     "ref": "CanalVenta",
     "rcols": [
      "Id"
     ]
    },
    {
     "n": "FK_Reserva_Pasajero",
     "cols": [
      "DniCliente"
     ],
     "ref": "Pasajero",
     "rcols": [
      "DNI"
     ]
    },
    {
     "n": "FK_Reserva_Estado",
     "cols": [
      "IdEstadoReserva"
     ],
     "ref": "EstadoReserva",
     "rcols": [
      "Id"
     ]
    },
    {
     "n": "FK_Reserva_TipoViaje",
     "cols": [
      "IdTipoViaje"
     ],
     "ref": "TipoViaje",
     "rcols": [
      "Id"
     ]
    },
    {
     "n": "FK_Reserva_VueloClase",
     "cols": [
      "IdVuelo",
      "IdClase"
     ],
     "ref": "VueloClase",
     "rcols": [
      "IdVuelo",
      "IdClase"
     ]
    },
    {
     "n": "FK_Reserva_VueloClaseVuelta",
     "cols": [
      "IdVueloVuelta",
      "IdClaseVuelta"
     ],
     "ref": "VueloClase",
     "rcols": [
      "IdVuelo",
      "IdClase"
     ]
    },
    {
     "n": "FK_Reserva_Tarifa",
     "cols": [
      "IdTarifa"
     ],
     "ref": "TarifaFamilia",
     "rcols": [
      "Id"
     ]
    }
   ]
  },
  "ReservaAdicional": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": true
    },
    {
     "n": "IdReserva",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "IdTipoAdicional",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "Cantidad",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "CostoUnitario",
     "t": "decimal",
     "l": "12,2",
     "nn": true,
     "id": false
    },
    {
     "n": "Subtotal",
     "t": "computed",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "Tramo",
     "t": "tinyint",
     "l": "",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": [
    {
     "n": "FK_ReservaAdicional_Reserva",
     "cols": [
      "IdReserva"
     ],
     "ref": "Reserva",
     "rcols": [
      "Id"
     ]
    },
    {
     "n": "FK_ReservaAdicional_Tipo",
     "cols": [
      "IdTipoAdicional"
     ],
     "ref": "TipoAdicional",
     "rcols": [
      "Id"
     ]
    }
   ]
  },
  "ReservaPasajero": {
   "cols": [
    {
     "n": "IdReserva",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "DniPasajero",
     "t": "nvarchar",
     "l": "20",
     "nn": true,
     "id": false
    },
    {
     "n": "IdAsiento",
     "t": "int",
     "l": "",
     "nn": false,
     "id": false
    },
    {
     "n": "EquipajeExtra",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "Tramo",
     "t": "tinyint",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "IdTipoPasajero",
     "t": "tinyint",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "IdAsistencia",
     "t": "tinyint",
     "l": "",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "IdReserva",
    "DniPasajero",
    "Tramo"
   ],
   "fk": [
    {
     "n": "FK_ReservaPasajero_Asiento",
     "cols": [
      "IdAsiento"
     ],
     "ref": "Asiento",
     "rcols": [
      "Id"
     ]
    },
    {
     "n": "FK_ReservaPasajero_Pasajero",
     "cols": [
      "DniPasajero"
     ],
     "ref": "Pasajero",
     "rcols": [
      "DNI"
     ]
    },
    {
     "n": "FK_ReservaPasajero_Reserva",
     "cols": [
      "IdReserva"
     ],
     "ref": "Reserva",
     "rcols": [
      "Id"
     ]
    }
   ]
  },
  "Roles": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": true
    },
    {
     "n": "Nombre",
     "t": "nvarchar",
     "l": "50",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": []
  },
  "RolFamilia": {
   "cols": [
    {
     "n": "IdRol",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "IdFamilia",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "IdRol",
    "IdFamilia"
   ],
   "fk": [
    {
     "n": "FK_RolFamilia_Familia",
     "cols": [
      "IdFamilia"
     ],
     "ref": "Familia",
     "rcols": [
      "Id"
     ]
    },
    {
     "n": "FK_RolFamilia_Rol",
     "cols": [
      "IdRol"
     ],
     "ref": "Roles",
     "rcols": [
      "Id"
     ]
    }
   ]
  },
  "RolPatente": {
   "cols": [
    {
     "n": "IdRol",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "IdPatente",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "IdRol",
    "IdPatente"
   ],
   "fk": [
    {
     "n": "FK_RolPatente_Patente",
     "cols": [
      "IdPatente"
     ],
     "ref": "Patente",
     "rcols": [
      "Id"
     ]
    },
    {
     "n": "FK_RolPatente_Rol",
     "cols": [
      "IdRol"
     ],
     "ref": "Roles",
     "rcols": [
      "Id"
     ]
    }
   ]
  },
  "TarjetaEmbarque": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": true
    },
    {
     "n": "NumeroTarjeta",
     "t": "computed",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "IdCheckIn",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "PuertaEmbarque",
     "t": "nvarchar",
     "l": "10",
     "nn": true,
     "id": false
    },
    {
     "n": "HoraLimiteEmbarque",
     "t": "datetime2",
     "l": "0",
     "nn": true,
     "id": false
    },
    {
     "n": "FechaHoraEmision",
     "t": "datetime2",
     "l": "0",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": [
    {
     "n": "FK_Tarjeta_CheckIn",
     "cols": [
      "IdCheckIn"
     ],
     "ref": "CheckIn",
     "rcols": [
      "Id"
     ]
    }
   ]
  },
  "TipoAdicional": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": true
    },
    {
     "n": "Nombre",
     "t": "nvarchar",
     "l": "60",
     "nn": true,
     "id": false
    },
    {
     "n": "Activo",
     "t": "bit",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "PrecioUnitario",
     "t": "decimal",
     "l": "12,2",
     "nn": true,
     "id": false
    },
    {
     "n": "MaxPorPasajero",
     "t": "tinyint",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "Codigo",
     "t": "nvarchar",
     "l": "30",
     "nn": false,
     "id": false
    },
    {
     "n": "SeleccionManual",
     "t": "bit",
     "l": "",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": []
  },
  "TipoEvento": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": true
    },
    {
     "n": "Nombre",
     "t": "nvarchar",
     "l": "100",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": []
  },
  "TipoViaje": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "Nombre",
     "t": "nvarchar",
     "l": "30",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": []
  },
  "Usuario": {
   "cols": [
    {
     "n": "DNI",
     "t": "nvarchar",
     "l": "250",
     "nn": true,
     "id": false
    },
    {
     "n": "Apellido",
     "t": "nvarchar",
     "l": "250",
     "nn": true,
     "id": false
    },
    {
     "n": "Nombre",
     "t": "nvarchar",
     "l": "250",
     "nn": true,
     "id": false
    },
    {
     "n": "UserName",
     "t": "nvarchar",
     "l": "250",
     "nn": true,
     "id": false
    },
    {
     "n": "Contrasena",
     "t": "nvarchar",
     "l": "250",
     "nn": true,
     "id": false
    },
    {
     "n": "Email",
     "t": "nvarchar",
     "l": "250",
     "nn": true,
     "id": false
    },
    {
     "n": "Bloqueo",
     "t": "bit",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "Activo",
     "t": "bit",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "IdRol",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "IntentosFallidos",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "UltimoIntentoFallido",
     "t": "datetime",
     "l": "",
     "nn": false,
     "id": false
    },
    {
     "n": "DebeCambiarContrasena",
     "t": "bit",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "Idioma",
     "t": "nvarchar",
     "l": "5",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "DNI"
   ],
   "fk": [
    {
     "n": "FK_Usuario_Roles",
     "cols": [
      "IdRol"
     ],
     "ref": "Roles",
     "rcols": [
      "Id"
     ]
    }
   ]
  },
  "Vuelo": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": true
    },
    {
     "n": "CodigoVuelo",
     "t": "nvarchar",
     "l": "10",
     "nn": true,
     "id": false
    },
    {
     "n": "IdAerolinea",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "IdOrigen",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "IdDestino",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "FechaHoraSalida",
     "t": "datetime2",
     "l": "0",
     "nn": true,
     "id": false
    },
    {
     "n": "FechaHoraLlegada",
     "t": "datetime2",
     "l": "0",
     "nn": true,
     "id": false
    },
    {
     "n": "PuertaEmbarque",
     "t": "nvarchar",
     "l": "10",
     "nn": true,
     "id": false
    },
    {
     "n": "CostoKiloExceso",
     "t": "decimal",
     "l": "10,2",
     "nn": true,
     "id": false
    },
    {
     "n": "BorradoLogico",
     "t": "bit",
     "l": "",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": [
    {
     "n": "FK_Vuelo_Aerolinea",
     "cols": [
      "IdAerolinea"
     ],
     "ref": "Aerolinea",
     "rcols": [
      "Id"
     ]
    },
    {
     "n": "FK_Vuelo_Destino",
     "cols": [
      "IdDestino"
     ],
     "ref": "Aeropuerto",
     "rcols": [
      "Id"
     ]
    },
    {
     "n": "FK_Vuelo_Origen",
     "cols": [
      "IdOrigen"
     ],
     "ref": "Aeropuerto",
     "rcols": [
      "Id"
     ]
    }
   ]
  },
  "VueloClase": {
   "cols": [
    {
     "n": "IdVuelo",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "IdClase",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "PrecioBase",
     "t": "decimal",
     "l": "12,2",
     "nn": true,
     "id": false
    },
    {
     "n": "CapacidadAsientos",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "AsientosReservados",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "FranquiciaEquipajeKg",
     "t": "decimal",
     "l": "6,2",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "IdVuelo",
    "IdClase"
   ],
   "fk": [
    {
     "n": "FK_VueloClase_Clase",
     "cols": [
      "IdClase"
     ],
     "ref": "ClaseVuelo",
     "rcols": [
      "Id"
     ]
    },
    {
     "n": "FK_VueloClase_Vuelo",
     "cols": [
      "IdVuelo"
     ],
     "ref": "Vuelo",
     "rcols": [
      "Id"
     ]
    }
   ]
  },
  "Vuelo_C": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": true
    },
    {
     "n": "IdVuelo",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "Fecha",
     "t": "date",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "Hora",
     "t": "time",
     "l": "0",
     "nn": true,
     "id": false
    },
    {
     "n": "CodigoVuelo",
     "t": "nvarchar",
     "l": "10",
     "nn": true,
     "id": false
    },
    {
     "n": "IdAerolinea",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "IdOrigen",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "IdDestino",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "FechaHoraSalida",
     "t": "datetime2",
     "l": "0",
     "nn": true,
     "id": false
    },
    {
     "n": "FechaHoraLlegada",
     "t": "datetime2",
     "l": "0",
     "nn": true,
     "id": false
    },
    {
     "n": "PuertaEmbarque",
     "t": "nvarchar",
     "l": "10",
     "nn": true,
     "id": false
    },
    {
     "n": "CostoKiloExceso",
     "t": "decimal",
     "l": "10,2",
     "nn": true,
     "id": false
    },
    {
     "n": "BorradoLogico",
     "t": "bit",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "Act",
     "t": "bit",
     "l": "",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": [
    {
     "n": "FK_Vuelo_C_Vuelo",
     "cols": [
      "IdVuelo"
     ],
     "ref": "Vuelo",
     "rcols": [
      "Id"
     ]
    },
    {
     "n": "FK_Vuelo_C_Aerolinea",
     "cols": [
      "IdAerolinea"
     ],
     "ref": "Aerolinea",
     "rcols": [
      "Id"
     ]
    },
    {
     "n": "FK_Vuelo_C_Origen",
     "cols": [
      "IdOrigen"
     ],
     "ref": "Aeropuerto",
     "rcols": [
      "Id"
     ]
    },
    {
     "n": "FK_Vuelo_C_Destino",
     "cols": [
      "IdDestino"
     ],
     "ref": "Aeropuerto",
     "rcols": [
      "Id"
     ]
    }
   ]
  },
  "TarifaFamilia": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "Codigo",
     "t": "nvarchar",
     "l": "10",
     "nn": true,
     "id": false
    },
    {
     "n": "Nombre",
     "t": "nvarchar",
     "l": "30",
     "nn": true,
     "id": false
    },
    {
     "n": "PorcentajeRecargo",
     "t": "decimal",
     "l": "5,2",
     "nn": true,
     "id": false
    },
    {
     "n": "ValijasIncluidas",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "AsientoIncluido",
     "t": "tinyint",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "PorcentajePenalidadCambio",
     "t": "decimal",
     "l": "5,2",
     "nn": true,
     "id": false
    },
    {
     "n": "TipoReembolso",
     "t": "tinyint",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "Activo",
     "t": "bit",
     "l": "",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": []
  },
  "Reembolso": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": true
    },
    {
     "n": "IdReserva",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "Importe",
     "t": "decimal",
     "l": "12,2",
     "nn": true,
     "id": false
    },
    {
     "n": "IdMedioPago",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "IdEstado",
     "t": "tinyint",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "FechaSolicitud",
     "t": "datetime2",
     "l": "0",
     "nn": true,
     "id": false
    },
    {
     "n": "FechaProceso",
     "t": "datetime2",
     "l": "0",
     "nn": false,
     "id": false
    },
    {
     "n": "LoginProceso",
     "t": "nvarchar",
     "l": "50",
     "nn": false,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": [
    {
     "n": "FK_Reembolso_Reserva",
     "cols": [
      "IdReserva"
     ],
     "ref": "Reserva",
     "rcols": [
      "Id"
     ]
    },
    {
     "n": "FK_Reembolso_MedioPago",
     "cols": [
      "IdMedioPago"
     ],
     "ref": "MedioPago",
     "rcols": [
      "Id"
     ]
    }
   ]
  },
  "CambioReserva": {
   "cols": [
    {
     "n": "Id",
     "t": "int",
     "l": "",
     "nn": true,
     "id": true
    },
    {
     "n": "IdReserva",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "Tramo",
     "t": "tinyint",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "IdVueloAnterior",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "IdClaseAnterior",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "IdVueloNuevo",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "IdClaseNuevo",
     "t": "int",
     "l": "",
     "nn": true,
     "id": false
    },
    {
     "n": "Penalidad",
     "t": "decimal",
     "l": "12,2",
     "nn": true,
     "id": false
    },
    {
     "n": "DiferenciaTarifa",
     "t": "decimal",
     "l": "12,2",
     "nn": true,
     "id": false
    },
    {
     "n": "ImporteCobrado",
     "t": "decimal",
     "l": "12,2",
     "nn": true,
     "id": false
    },
    {
     "n": "IdMedioPago",
     "t": "int",
     "l": "",
     "nn": false,
     "id": false
    },
    {
     "n": "NumeroTransaccion",
     "t": "nvarchar",
     "l": "40",
     "nn": false,
     "id": false
    },
    {
     "n": "FechaCambio",
     "t": "datetime2",
     "l": "0",
     "nn": true,
     "id": false
    },
    {
     "n": "LoginUsuario",
     "t": "nvarchar",
     "l": "50",
     "nn": true,
     "id": false
    }
   ],
   "pk": [
    "Id"
   ],
   "fk": [
    {
     "n": "FK_CambioReserva_Reserva",
     "cols": [
      "IdReserva"
     ],
     "ref": "Reserva",
     "rcols": [
      "Id"
     ]
    },
    {
     "n": "FK_CambioReserva_MedioPago",
     "cols": [
      "IdMedioPago"
     ],
     "ref": "MedioPago",
     "rcols": [
      "Id"
     ]
    }
   ]
  }
 }
};

// ------------------------------------------------------------------------------ utilidades
// Compatible con JScript (motor por defecto de EA): sin Object.keys, Array.indexOf, JSON, etc.

function log(msg) { Session.Output(msg); }

function nuevoPaquete(padre, nombre) {
    var p = padre.Packages.AddNew(nombre, "Package");
    p.Update();
    padre.Packages.Refresh();
    return p;
}

function nuevoElemento(paquete, nombre, tipo, estereotipo) {
    var e = paquete.Elements.AddNew(nombre, tipo);
    if (estereotipo) e.Stereotype = estereotipo;
    e.Update();
    paquete.Elements.Refresh();
    return e;
}

// Igual que nuevoElemento, pero si la versión de EA no acepta el tipo usa el alternativo.
function nuevoElementoAlt(paquete, nombre, tipo, alternativo) {
    var el;
    try { el = paquete.Elements.AddNew(nombre, tipo); }
    catch (e) { el = paquete.Elements.AddNew(nombre, alternativo); }
    el.Update();
    return el;
}

function ponerEnDiagrama(diagrama, elemento, x, y, ancho, alto, orden) {
    var pos = "l=" + Math.round(x) + ";r=" + Math.round(x + ancho) + ";t=" + Math.round(y) + ";b=" + Math.round(y + alto) + ";";
    var o = diagrama.DiagramObjects.AddNew(pos, "");
    o.ElementID = elemento.ElementID;
    if (orden) { try { o.Sequence = orden; } catch (e) { } }   // orden Z: 1 = adelante; los carriles van al fondo
    o.Update();
    return o;
}

// EA guarda los colores como BGR en un entero.
function colorEA(rgb) { return rgb[0] + rgb[1] * 256 + rgb[2] * 65536; }

function pintar(el, rgb) {
    if (!rgb) return;
    try { el.SetAppearance(1, 0, colorEA(rgb)); el.Update(); } catch (e) { }   // 1 = local, 0 = fondo
}

// Oculta el paquete delante del nombre (BE::Cliente_GV42 -> Cliente_GV42): opción "Show Namespace".
function sinNamespace(d) {
    try { d.HighlightImports = false; } catch (e) { }
    try {
        var sx = "" + d.StyleEx;
        if (sx.indexOf("ShowFQN=") >= 0) sx = sx.replace(/ShowFQN=[^;]*;/, "ShowFQN=0;"); else sx += "ShowFQN=0;";
        d.StyleEx = sx;
    } catch (e2) { }
    try { d.Update(); } catch (e3) { }
}

function terminarDiagrama(d) {
    sinNamespace(d);
    d.DiagramObjects.Refresh();
    try { Repository.ReloadDiagram(d.DiagramID); } catch (e) { }
}

var actores = {};    // nombre del actor -> Element
var casosUso = {};   // no se usan pelotas de casos de uso en estas secuencias

// ====================================================================================== clases

function textoMetodo(m) {
    var ps = [], i;
    for (i = 0; i < m.p.length; i++) ps.push(m.p[i][0] + ": " + m.p[i][1]);
    return m.n + "(" + ps.join(", ") + ")" + (m.t ? ": " + m.t : "");
}

var elementosClase = {};   // nombre de clase -> Element
var conectores = {};       // "origen|destino|tipo" -> ConnectorID

function crearClases(paqueteModelo) {
    var capas = {}, nombresCapa = ["UI", "BLL", "DAL", "BE", "Servicios"], i, j;
    for (i = 0; i < nombresCapa.length; i++) capas[nombresCapa[i]] = nuevoPaquete(paqueteModelo, nombresCapa[i]);

    for (i = 0; i < MODELO.clases.length; i++) {
        var c = MODELO.clases[i];
        var el = nuevoElemento(capas[c.capa], c.nombre, c.tipo, null);

        for (j = 0; j < c.attrs.length; j++) {
            var a = c.attrs[j];
            var at = el.Attributes.AddNew(a.n, a.t);
            at.Visibility = a.v;
            at.IsStatic = a.s;
            if (c.tipo == "Enumeration") at.Stereotype = "enum";
            at.Update();
        }
        el.Attributes.Refresh();

        for (j = 0; j < c.metodos.length; j++) {
            var m = c.metodos[j];
            var op = el.Methods.AddNew(m.n, m.t);
            op.Visibility = m.v;
            op.IsStatic = m.s;
            if (m.t == "") op.Stereotype = "create";
            op.Update();
            for (var k = 0; k < m.p.length; k++) {
                var par = op.Parameters.AddNew(m.p[k][0], m.p[k][1]);
                par.Kind = "in";
                par.Position = k;
                par.Update();
            }
            op.Parameters.Refresh();
        }
        el.Methods.Refresh();
        el.Update();
        elementosClase[c.nombre] = el;
    }

    // Relaciones (generalización, asociación entre entidades, dependencia de uso entre capas)
    for (i = 0; i < MODELO.relaciones.length; i++) {
        var r = MODELO.relaciones[i];
        var o = elementosClase[r.o], d = elementosClase[r.d];
        if (!o || !d) continue;
        var con = o.Connectors.AddNew("", r.tipo);
        con.SupplierID = d.ElementID;
        if (r.tipo == "Association") {
            con.Direction = "Source -> Destination";
            con.SupplierEnd.Role = r.rol;
            con.SupplierEnd.Cardinality = r.mult;
            con.SupplierEnd.Navigable = "Navigable";
            con.ClientEnd.Cardinality = "0..*";
        }
        if (r.tipo == "Dependency") con.Stereotype = "use";
        con.Update();
        conectores[r.o + "|" + r.d + "|" + r.tipo] = con.ConnectorID;
        o.Connectors.Refresh();
    }
    log("  Clases creadas: " + MODELO.clases.length + " | relaciones: " + MODELO.relaciones.length);
}

// Qué se muestra de cada clase en un diagrama: "todo", "attrs" (solo atributos), "metodos" (solo
// métodos) o "nombre" (solo el nombre). Se guarda en el estilo del objeto dentro de ese diagrama.
var ESTILO_VISTA = {
    todo:    "",
    attrs:   "OpPub=0;OpPri=0;OpPro=0;OpPkg=0;",
    metodos: "AttPub=0;AttPri=0;AttPro=0;AttPkg=0;",
    nombre:  "AttPub=0;AttPri=0;AttPro=0;AttPkg=0;OpPub=0;OpPri=0;OpPro=0;OpPkg=0;"
};

function ocultarConectores(d, lista) {
    for (var i = 0; i < lista.length; i++) {
        var id = conectores[lista[i][0] + "|" + lista[i][1] + "|" + lista[i][2]];
        if (!id) continue;
        try {
            var dl = d.DiagramLinks.AddNew("", "");
            dl.ConnectorID = id;
            dl.IsHidden = true;
            dl.Update();
        } catch (e) { }
    }
    try { d.DiagramLinks.Refresh(); } catch (e2) { }
}

// Línea recta (Direct) entre los centros de las dos clases, con un desplazamiento opcional para separar
// dos líneas del mismo par de tablas (por ejemplo, Vuelo -> Aeropuerto de origen y de destino).
function lineaRecta(d, connectorID, sx, sy) {
    if (!connectorID) return;
    try {
        var dl = d.DiagramLinks.AddNew("", "");
        dl.ConnectorID = connectorID;
        dl.IsHidden = false;
        try { dl.Style = "Mode=1;"; } catch (e1) { }
        if (sx || sy) {
            try { dl.Geometry = "SX=" + sx + ";SY=" + sy + ";EX=" + sx + ";EY=" + sy + ";"; } catch (e2) { }
        }
        dl.Update();
    } catch (e) { }
}

function lineasRectas(d, lista) {
    for (var i = 0; i < lista.length; i++)
        lineaRecta(d, conectores[lista[i][0] + "|" + lista[i][1] + "|" + lista[i][2]], 0, 0);
    try { d.DiagramLinks.Refresh(); } catch (e) { }
}

// Notas explicativas dentro del diagrama.
function crearNotas(paquete, d, notas) {
    for (var i = 0; i < notas.length; i++) {
        var n = notas[i];
        var el = paquete.Elements.AddNew("", "Note");
        el.Notes = n.texto;
        el.Update();
        ponerEnDiagrama(d, el, n.l, n.t, n.w, n.h, 1);
    }
    paquete.Elements.Refresh();
}

// Marcos (Boundary) con título: agrupan las clases o tablas de una capa o de una zona.
function crearMarcos(paquete, d, marcos) {
    var creados = {};
    for (var i = 0; i < marcos.length; i++) {
        var mk = marcos[i];
        var b = nuevoElemento(paquete, mk.nombre, "Boundary", null);
        ponerEnDiagrama(d, b, mk.l, mk.t, mk.w, mk.h, 100 + i);
        creados[mk.nombre] = b;
    }
    return creados;
}

function diagramaDeClases(paquete, def) {
    var d = paquete.Diagrams.AddNew(def.nombre, "Logical");
    d.Update();
    sinNamespace(d);
    var i, marcos = crearMarcos(paquete, d, def.marcos);
    for (i = 0; i < def.objs.length; i++) {
        var p = def.objs[i], el = elementosClase[p.n];
        if (!el) continue;
        var o = ponerEnDiagrama(d, el, p.l, p.t, p.w, p.h, 1);
        if (ESTILO_VISTA[p.modo || def.modo]) {
            try { o.Style = ESTILO_VISTA[p.modo || def.modo]; o.Update(); } catch (e) { }
        }
    }
    // Dependencias entre capas (de marco a marco)
    for (i = 0; i < def.enlaces.length; i++) {
        var a = marcos[def.enlaces[i][0]], b = marcos[def.enlaces[i][1]];
        if (!a || !b) continue;
        var con = a.Connectors.AddNew("", "Dependency");
        con.SupplierID = b.ElementID;
        con.Stereotype = "use";
        con.Update();
        a.Connectors.Refresh();
    }
    d.DiagramObjects.Refresh();
    crearNotas(paquete, d, def.notas || []);
    ocultarConectores(d, def.ocultar);
    lineasRectas(d, def.visibles || []);
    d.Update();
    try { Repository.ReloadDiagram(d.DiagramID); } catch (e3) { }
    return d;
}

// ====================================================================================== tablas (DER)

var elementosTabla = {};
var conectoresFK = {};      // "tabla|nombreFK" -> ConnectorID

function crearTablas(paqueteDatos) {
    var nombre, t, i;
    for (nombre in MODELO.tablas) {
        t = MODELO.tablas[nombre];
        var el = nuevoElemento(paqueteDatos, nombre, "Class", "table");
        el.Gentype = "SQL Server 2012";
        for (i = 0; i < t.cols.length; i++) {
            var c = t.cols[i];
            var tipo = c.t == "computed" ? "nvarchar" : c.t;
            var at = el.Attributes.AddNew(c.n, tipo);
            at.Stereotype = "column";
            at.AllowDuplicates = c.nn;              // en EA: AllowDuplicates = NOT NULL
            if (c.l) {
                var partes = c.l.split(",");
                if (tipo == "decimal" || tipo == "numeric") { at.Precision = partes[0]; if (partes[1]) at.Scale = partes[1]; }
                else at.Length = partes[0];
            }
            if (c.t == "computed") at.Notes = "Columna calculada (PERSISTED).";
            if (c.id) at.Notes = "IDENTITY(1,1)";
            at.Update();
        }
        el.Attributes.Refresh();

        if (t.pk.length) {
            var pk = el.Methods.AddNew("PK_" + nombre, "");
            pk.Stereotype = "PK";
            pk.Update();
            for (i = 0; i < t.pk.length; i++) {
                var tipoPk = "int";
                for (var k = 0; k < t.cols.length; k++) if (t.cols[k].n == t.pk[i]) tipoPk = t.cols[k].t;
                var pp = pk.Parameters.AddNew(t.pk[i], tipoPk); pp.Position = i; pp.Update();
            }
        }
        for (i = 0; i < t.fk.length; i++) {
            var fk = el.Methods.AddNew(t.fk[i].n, "");
            fk.Stereotype = "FK";
            fk.Update();
            for (var j = 0; j < t.fk[i].cols.length; j++) {
                var fp = fk.Parameters.AddNew(t.fk[i].cols[j], ""); fp.Position = j; fp.Update();
            }
        }
        el.Methods.Refresh();
        el.Update();
        elementosTabla[nombre] = el;
    }
    // Relaciones FK (tabla hija -> tabla padre)
    for (nombre in MODELO.tablas) {
        t = MODELO.tablas[nombre];
        for (i = 0; i < t.fk.length; i++) {
            var hija = elementosTabla[nombre], padre = elementosTabla[t.fk[i].ref];
            if (!hija || !padre) continue;
            var con = hija.Connectors.AddNew(t.fk[i].n, "Association");
            con.SupplierID = padre.ElementID;
            con.Stereotype = "FK";
            con.ClientEnd.Cardinality = "0..*";
            con.SupplierEnd.Cardinality = "1";
            con.ClientEnd.Role = t.fk[i].n;
            con.SupplierEnd.Role = "PK_" + t.fk[i].ref;
            try { con.StyleEx = "FKINFO=SRC=" + t.fk[i].n + ":DST=PK_" + t.fk[i].ref + ":;"; } catch (e) { }
            con.Update();
            conectoresFK[nombre + "|" + t.fk[i].n] = con.ConnectorID;
            hija.Connectors.Refresh();
        }
    }
    var cantidad = 0;
    for (nombre in MODELO.tablas) cantidad++;
    log("  Tablas creadas: " + cantidad);
}

// Notación "pata de gallo" (Martin / Information Engineering) en un diagrama de datos.
function notacionPataDeGallo(d) {
    try {
        var ex = "" + d.ExtendedStyle;
        if (ex.indexOf("ConnectorNotation=") >= 0) ex = ex.replace(/ConnectorNotation=[^;]*;/, "ConnectorNotation=Information Engineering;");
        else ex = ex + "ConnectorNotation=Information Engineering;";
        d.ExtendedStyle = ex;
        d.Update();
    } catch (e) { }
}

function diagramaDERGeneral(paquete) {
    var d = paquete.Diagrams.AddNew("DER completo - FLY SAFE", "Logical");
    d.Update();
    notacionPataDeGallo(d);
    crearMarcos(paquete, d, DER.marcos);
    for (var i = 0; i < DER.objs.length; i++) {
        var p = DER.objs[i];
        if (elementosTabla[p.n]) ponerEnDiagrama(d, elementosTabla[p.n], p.l, p.t, p.w, p.h, 1);
    }
    d.DiagramObjects.Refresh();
    for (i = 0; i < DER.lineas.length; i++) {
        var ln = DER.lineas[i];
        lineaRecta(d, conectoresFK[ln[0] + "|" + ln[1]], ln[2], ln[3]);
    }
    try { d.DiagramLinks.Refresh(); } catch (e) { }
    terminarDiagrama(d);
    return d;
}

function diagramaDERBitacora(paquete) {
    var d = paquete.Diagrams.AddNew("DER - Bitácora de cambios (Vuelo_C | Vuelo)", "Logical");
    d.Update();
    notacionPataDeGallo(d);
    var x = 20;
    for (var i = 0; i < BIT_TABLAS.length; i++) {
        var t = MODELO.tablas[BIT_TABLAS[i]], max = BIT_TABLAS[i].length;
        for (var k = 0; k < t.cols.length; k++) max = Math.max(max, t.cols[k].n.length + t.cols[k].t.length + 10);
        var w = Math.max(190, max * 6 + 30), h = 50 + 14 * (t.cols.length + t.fk.length + 1);
        ponerEnDiagrama(d, elementosTabla[BIT_TABLAS[i]], x, 20, w, h, 1);
        x += w + 90;
    }
    terminarDiagrama(d);
    return d;
}

// ====================================================================================== secuencias

// Fragmento combinado (alt = 0, opt = 1, loop = 4). Cada operando es una partición con su guarda.
function crearFragmento(paquete, diagrama, f) {
    var el = nuevoElemento(paquete, f.nombre, "InteractionFragment", null);
    el.Subtype = f.subtipo;
    el.Update();
    for (var i = 0; i < f.ops.length; i++) {
        var p = el.Partitions.AddNew(f.ops[i].guarda, "");
        try { p.Size = f.ops[i].size; } catch (e1) { }
    }
    el.Update();
    try { el.Partitions.Refresh(); } catch (e2) { }
    ponerEnDiagrama(diagrama, el, f.l, f.t, f.r - f.l, f.b - f.t);
}

// Sirve para las secuencias de diseño (formularios como boundary, BLL, DAL, pelotas de casos de uso)
// y para la secuencia de roles (todas las líneas de vida son actores).
function diagramaSecuencia(paquete, nombre, caso, defs) {
    var d = paquete.Diagrams.AddNew(nombre, "Sequence");
    d.Update();

    var lineas = {}, x = 30, i;
    for (i = 0; i < caso.lifelines.length; i++) {
        var id = caso.lifelines[i], def = defs[id], el;
        if (def.cu) {
            // Pelota del caso de uso invocado (extend / include): vive solo mientras se ejecuta.
            var vida = caso.vidas[id];
            ponerEnDiagrama(d, casosUso[def.cu], x, vida[0], 150, vida[1] - vida[0]);
            lineas[id] = casosUso[def.cu];
            x += 190;
            continue;
        }
        if (def.actor) {
            el = actores[def.actor];
        } else {
            // Formularios = boundary; BLL, DAL y servicios = lifeline con el nombre de la clase.
            el = nuevoElemento(paquete, def.nombre, "Sequence", def.est ? def.est : null);
        }
        ponerEnDiagrama(d, el, x, 50, 140, caso.alto);
        lineas[id] = el;
        x += 190;
    }

    // Fragmentos primero (quedan por detrás de los mensajes)
    for (i = 0; i < caso.fragmentos.length; i++) crearFragmento(paquete, d, caso.fragmentos[i]);

    for (i = 0; i < caso.mensajes.length; i++) {
        var m = caso.mensajes[i];
        var origen = lineas[m[0]], destino = lineas[m[1]];
        var con = origen.Connectors.AddNew(m[2], "Sequence");
        con.SupplierID = destino.ElementID;
        con.DiagramID = d.DiagramID;
        con.SequenceNo = i + 1;
        try { con.StartPointY = -m[4]; con.EndPointY = -m[4]; } catch (e3) { }
        con.Update();
        origen.Connectors.Refresh();
        if (m[3] == "r") {
            // Mensaje de retorno (línea punteada). EA lo guarda en t_connector.PDATA4.
            try { Repository.Execute("UPDATE t_connector SET PDATA4 = '1' WHERE Connector_ID = " + con.ConnectorID); } catch (e4) { }
        }
        // Posición vertical del mensaje (EA la toma de PtStartY / PtEndY).
        try { Repository.Execute("UPDATE t_connector SET PtStartY = " + (-m[4]) + ", PtEndY = " + (-m[4]) + " WHERE Connector_ID = " + con.ConnectorID); } catch (e5) { }
    }
    terminarDiagrama(d);
    return d;
}

// ====================================================================================== actividad

// Estilos de los nodos del diagrama de procesos:
//   ini = nodo inicial, fin = nodo final, env = señal de envío (dato que entrega el cliente),
//   acc = actividad, dec = decisión, mer = unión (merge), ds = almacén de datos, art = documento / dato que se devuelve
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

// Diagrama de actividad con carriles (se usa para explicar los triggers de la bitácora de cambios).
function diagramaActividad(paquete, def) {
    var d = paquete.Diagrams.AddNew(def.nombre, "Activity");
    d.Update();
    var i;
    for (i = 0; i < def.carriles.length; i++) {
        var c = def.carriles[i];
        var part = nuevoElementoAlt(paquete, c[0], "ActivityPartition", "ActivityPartition");
        pintar(part, c[3]);
        ponerEnDiagrama(d, part, c[1], 20, c[2], def.alto, 100 + i);
    }
    var elementos = {};
    for (i = 0; i < def.nodos.length; i++) {
        var n = def.nodos[i], tipo = n[1], est = ESTILO[tipo], el;
        if (tipo == "ini" || tipo == "fin") {
            el = nuevoElementoAlt(paquete, n[2], "StateNode", "StateNode");
            el.Subtype = (tipo == "ini") ? 100 : 101;
        } else if (tipo == "acc") {
            el = nuevoElementoAlt(paquete, n[2], "Action", "Activity");
        } else if (tipo == "dec") {
            el = nuevoElementoAlt(paquete, n[2], "Decision", "Decision");
        } else if (tipo == "mer") {
            el = nuevoElementoAlt(paquete, n[2], "MergeNode", "Decision");
        } else {
            el = nuevoElementoAlt(paquete, n[2], "Object", "Object");
            el.Stereotype = "datastore";
        }
        el.Update();
        pintar(el, est.color);
        elementos[n[0]] = el;
        ponerEnDiagrama(d, el, n[3] - est.w / 2, n[4], est.w, est.h, 1);
    }
    paquete.Elements.Refresh();
    for (i = 0; i < def.flujos.length; i++) {
        var f = def.flujos[i], o = elementos[f[0]];
        var con = o.Connectors.AddNew(f[3], f[2] == "cf" ? "ControlFlow" : (f[2] == "of" ? "ObjectFlow" : "Dependency"));
        con.SupplierID = elementos[f[1]].ElementID;
        con.Update();
        o.Connectors.Refresh();
    }
    terminarDiagrama(d);
    return d;
}

// ====================================================================================== casos de uso

function diagramaCasosDeUsoBitacora(paquete) {
    var d = paquete.Diagrams.AddNew("Casos de uso - Auditoría de cambios", "Use Case");
    d.Update();
    var actor = actores[BIT_DCU.actor], nombre, i, cus = {};
    ponerEnDiagrama(d, actor, 60, 160, 90, 150);
    for (nombre in BIT_DCU.pos) {
        cus[nombre] = nuevoElemento(paquete, nombre, "UseCase", null);
        ponerEnDiagrama(d, cus[nombre], BIT_DCU.pos[nombre][0], BIT_DCU.pos[nombre][1], 220, 90);
    }
    for (i = 0; i < BIT_DCU.principal.length; i++) {
        var con = actor.Connectors.AddNew("", "Association");
        con.SupplierID = cus[BIT_DCU.principal[i]].ElementID;
        con.Update();
    }
    actor.Connectors.Refresh();
    for (i = 0; i < BIT_DCU.rel.length; i++) {
        var r = BIT_DCU.rel[i], o = cus[r[0]];
        var dep = o.Connectors.AddNew("", "Dependency");
        dep.SupplierID = cus[r[1]].ElementID;
        dep.Stereotype = r[2];
        dep.Update();
        o.Connectors.Refresh();
    }
    terminarDiagrama(d);
    return d;
}

// ====================================================================================== principal

function main() {
    Repository.EnsureOutputVisible("Script");
    Repository.ClearOutput("Script");

    var destino = Repository.GetTreeSelectedPackage();
    if (destino == null) {
        Session.Prompt("Seleccioná un paquete en el Project Browser antes de ejecutar el script.", 1);  // 1 = promptOK
        return;
    }

    var i;
    for (i = destino.Packages.Count - 1; i >= 0; i--) {
        if (destino.Packages.GetAt(i).Name == RAIZ) {
            destino.Packages.DeleteAt(i, false);
            log("Se borró la versión anterior de '" + RAIZ + "'.");
        }
    }
    destino.Packages.Refresh();

    log("Generando '" + RAIZ + "' en '" + destino.Name + "' ...");
    var raiz = nuevoPaquete(destino, RAIZ);

    crearClases(nuevoPaquete(raiz, "Modelo de clases"));
    crearTablas(nuevoPaquete(raiz, "Modelo de datos"));

    // ---------------------------------------------------------------- Diagramas de clases
    var pClases = nuevoPaquete(raiz, "Diagramas de clases");
    for (i = 0; i < DIAGRAMAS.length; i++) {
        diagramaDeClases(pClases, DIAGRAMAS[i]);
        log("  Clases: " + DIAGRAMAS[i].nombre + " (" + DIAGRAMAS[i].objs.length + " clases)");
    }

    // ---------------------------------------------------------------- DER general
    diagramaDERGeneral(nuevoPaquete(raiz, "Diagrama Entidad - Relación"));
    log("  DER general (" + DER.objs.length + " tablas)");

    // ---------------------------------------------------------------- Bitácora de cambios
    var pBit = nuevoPaquete(raiz, "Bitácora de cambios");
    var pBack = nuevoPaquete(pBit, "Registro de cambios (BackEnd)");
    for (i = 0; i < ACTIVIDADES.length; i++) {
        diagramaActividad(nuevoPaquete(pBack, ACTIVIDADES[i].nombre), ACTIVIDADES[i]);
        log("  Bitácora de cambios: " + ACTIVIDADES[i].nombre);
    }
    var pFront = nuevoPaquete(pBit, "Auditoría de cambios (FrontEnd)");
    actores["Administrador"] = nuevoElemento(pFront, "Administrador", "Actor", null);
    diagramaCasosDeUsoBitacora(pFront);
    for (i = 0; i < SECUENCIAS.length; i++) {
        diagramaSecuencia(nuevoPaquete(pFront, SECUENCIAS[i].nombre), SECUENCIAS[i].nombre, SECUENCIAS[i], L_BIT);
        log("  Bitácora de cambios: " + SECUENCIAS[i].nombre + " (" + SECUENCIAS[i].mensajes.length + " mensajes)");
    }
    diagramaDeClases(pFront, BIT_DC);
    diagramaDERBitacora(pFront);
    log("  Bitácora de cambios: casos de uso, clases y DER");

    Repository.RefreshModelView(destino.PackageID);
    log("Listo. Abrí cada diagrama desde el Project Browser (paquete '" + RAIZ + "').");
}

main();
