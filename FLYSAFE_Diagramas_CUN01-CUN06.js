/*
 * FLY SAFE - Diagramas de CUN01 a CUN06 para Enterprise Architect
 * ------------------------------------------------------------------------------------
 * Genera, dentro del paquete seleccionado en el Project Browser:
 *   FLY SAFE - CUN01 a CUN06
 *     Modelo de clases  (UI / BLL / DAL / BE / Servicios)  -> clases con los atributos y
 *                        métodos reales del proyecto Proyecto-Diploma (C#)
 *     Modelo de datos   -> tablas de "Gestion Usuario" (EsquemaCompleto.sql) con PK y FK
 *     Actores           -> Vendedor
 *     Casos de uso      -> CUN01 a CUN06 (se usan como "pelota" en las secuencias que los invocan)
 *     CUNxx ...         -> por cada caso de uso: diagrama de secuencia (formularios como boundary,
 *                          fragmentos alt/opt/loop, bitácora y dígito verificador), diagrama de clases y DER
 *
 * Compatible con JScript y JavaScript (los dos motores de scripting de EA).
 *
 * Cómo usarlo (EA 13 o superior):
 *   1. Seleccioná en el Project Browser el paquete donde querés los diagramas.
 *   2. Specialize/Tools > Scripting > New Normal Group (o uno existente) > New JavaScript script.
 *   3. Pegá este archivo completo, guardá y ejecutalo (botón Run).
 *   4. El avance se ve en la ventana Output (pestaña Script).
 * Si lo volvés a ejecutar, borra el paquete "FLY SAFE - CUN01 a CUN06" anterior y lo regenera.
 *
 * Generado a partir del código del proyecto (30/09/2026).
 */

var RAIZ = "FLY SAFE - CUN01 a CUN06";

var MODELO = {
 "clases": [
  {
   "nombre": "FRMReservarVuelo_GV42",
   "capa": "UI",
   "padre": "Form",
   "attrs": [
    {
     "n": "_bll",
     "t": "BLLReserva_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "_esVendedor",
     "t": "bool",
     "v": "Private",
     "s": false
    },
    {
     "n": "_resultados",
     "t": "List<VueloClase_GV42>",
     "v": "Private",
     "s": false
    },
    {
     "n": "_vueloElegido",
     "t": "VueloClase_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "_clienteElegido",
     "t": "Pasajero_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "_pasajeros",
     "t": "List<DatosPasajero>",
     "v": "Private",
     "s": false
    },
    {
     "n": "_mapaAsientos",
     "t": "List<AsientoDisponibilidad_GV42>",
     "v": "Private",
     "s": false
    },
    {
     "n": "_indicePasajeroActivo",
     "t": "int",
     "v": "Private",
     "s": false
    },
    {
     "n": "_reservaGenerada",
     "t": "Reserva_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "_pasoActual",
     "t": "int",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "FRMReservarVuelo_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "btnSiguiente_Click",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "sender",
       "object"
      ],
      [
       "e",
       "EventArgs"
      ]
     ]
    },
    {
     "n": "CargarAeropuertos",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "btnBuscarVuelos_Click",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "sender",
       "object"
      ],
      [
       "e",
       "EventArgs"
      ]
     ]
    },
    {
     "n": "ValidarYAvanzarBusqueda",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "btnBuscarCliente_Click",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "sender",
       "object"
      ],
      [
       "e",
       "EventArgs"
      ]
     ]
    },
    {
     "n": "btnRegistrarCliente_Click",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "sender",
       "object"
      ],
      [
       "e",
       "EventArgs"
      ]
     ]
    },
    {
     "n": "ValidarYAvanzarCliente",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "ReconstruirFilasPasajeros",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "AutocompletarPasajero",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "dp",
       "DatosPasajero"
      ]
     ]
    },
    {
     "n": "PasajerosEnPantalla",
     "t": "List<Pasajero_GV42>",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "ValidarYAvanzarPasajeros",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "PrepararPasoAsientos",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "RefrescarButacas",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "CambiarPasajeroActivo",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "delta",
       "int"
      ]
     ]
    },
    {
     "n": "ctrlButacas_AsientoClickeado",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "sender",
       "object"
      ],
      [
       "asiento",
       "Asiento_GV42"
      ]
     ]
    },
    {
     "n": "ValidarYAvanzarAsientos",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "CargarAdicionales",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "ArmarResumen",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "ConfirmarReserva",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": []
    },
    {
     "n": "MostrarResultado",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": []
    }
   ],
   "tipo": "Class"
  },
  {
   "nombre": "FRMPagoReserva_GV42",
   "capa": "UI",
   "padre": "Form",
   "attrs": [
    {
     "n": "_bll",
     "t": "BLLReserva_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "_reserva",
     "t": "Reserva_GV42",
     "v": "Private",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "FRMPagoReserva_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": [
      [
       "numeroReservaInicial",
       "string"
      ]
     ]
    },
    {
     "n": "btnBuscar_Click",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "sender",
       "object"
      ],
      [
       "e",
       "EventArgs"
      ]
     ]
    },
    {
     "n": "btnConfirmarPago_Click",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "sender",
       "object"
      ],
      [
       "e",
       "EventArgs"
      ]
     ]
    }
   ],
   "tipo": "Class"
  },
  {
   "nombre": "CtrlButacas_GV42",
   "capa": "UI",
   "padre": "UserControl",
   "attrs": [
    {
     "n": "ANCHO_BOTON",
     "t": "int",
     "v": "Private",
     "s": true
    },
    {
     "n": "ALTO_BOTON",
     "t": "int",
     "v": "Private",
     "s": true
    },
    {
     "n": "ESPACIO",
     "t": "int",
     "v": "Private",
     "s": true
    },
    {
     "n": "ANCHO_PASILLO",
     "t": "int",
     "v": "Private",
     "s": true
    },
    {
     "n": "_pnlReferencias",
     "t": "FlowLayoutPanel",
     "v": "Private",
     "s": false
    },
    {
     "n": "_botonesPorIdAsiento",
     "t": "Dictionary<int, Button>",
     "v": "Private",
     "s": false
    },
    {
     "n": "_asientos",
     "t": "List<AsientoDisponibilidad_GV42>",
     "v": "Private",
     "s": false
    },
    {
     "n": "AsientoClickeado",
     "t": "event EventHandler<Asiento_GV42>",
     "v": "Public",
     "s": false
    }
   ],
   "metodos": [
    {
     "n": "CtrlButacas_GV42",
     "t": "",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "CargarMapa",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "asientos",
       "List<AsientoDisponibilidad_GV42>"
      ],
      [
       "ocupadosLocalmente",
       "HashSet<int>"
      ],
      [
       "seleccionActualId",
       "int?"
      ]
     ]
    }
   ],
   "tipo": "Class"
  },
  {
   "nombre": "BLLReserva_GV42",
   "capa": "BLL",
   "padre": null,
   "attrs": [
    {
     "n": "TASA_IMPUESTOS",
     "t": "decimal",
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
     "n": "_bllIntegridad",
     "t": "BLLIntegridad_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "MAX_PASAJEROS_POR_RESERVA",
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
     "n": "RecalcularIntegridad",
     "t": "void",
     "v": "Private",
     "s": false,
     "p": [
      [
       "tabla",
       "string"
      ]
     ]
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
     "n": "ObtenerMapaAsientos",
     "t": "List<AsientoDisponibilidad_GV42>",
     "v": "Public",
     "s": false,
     "p": [
      [
       "idVuelo",
       "int"
      ],
      [
       "clase",
       "ClaseVuelo_GV42"
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
    }
   ],
   "tipo": "Class"
  },
  {
   "nombre": "BLLNegocioUtil_GV42",
   "capa": "BLL",
   "padre": null,
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
     "s": false
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
      ]
     ]
    }
   ],
   "tipo": "Class"
  },
  {
   "nombre": "BLLBitacora_GV42",
   "capa": "BLL",
   "padre": null,
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
   ],
   "tipo": "Class"
  },
  {
   "nombre": "BLLIntegridad_GV42",
   "capa": "BLL",
   "padre": null,
   "attrs": [
    {
     "n": "_dal",
     "t": "DALIntegridad_GV42",
     "v": "Private",
     "s": false
    },
    {
     "n": "NOMBRE_BD",
     "t": "string",
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
     "n": "_timerBackup",
     "t": "Timer",
     "v": "Private",
     "s": true
    },
    {
     "n": "_lockTimer",
     "t": "object",
     "v": "Private",
     "s": false
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
     "n": "HacerBackupAutomatico",
     "t": "string",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "ObtenerUltimoBackup",
     "t": "string",
     "v": "Public",
     "s": false,
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
   ],
   "tipo": "Class"
  },
  {
   "nombre": "DALReserva_GV42",
   "capa": "DAL",
   "padre": null,
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
     "n": "ListarPorCliente",
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
       "idReserva",
       "int"
      ],
      [
       "montoPenalidad",
       "decimal"
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
       "idReserva",
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
       "idReserva",
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
       "idReserva",
       "int"
      ]
     ]
    }
   ],
   "tipo": "Class"
  },
  {
   "nombre": "DALPasajero_GV42",
   "capa": "DAL",
   "padre": null,
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
   ],
   "tipo": "Class"
  },
  {
   "nombre": "DALVuelo_GV42",
   "capa": "DAL",
   "padre": null,
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
     "n": "BuscarVueloClase",
     "t": "VueloClase_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "idVuelo",
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
       "idExcluido",
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
       "idVuelo",
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
       "idVuelo",
       "int"
      ],
      [
       "baja",
       "bool"
      ]
     ]
    }
   ],
   "tipo": "Class"
  },
  {
   "nombre": "DALAsiento_GV42",
   "capa": "DAL",
   "padre": null,
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
       "idVuelo",
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
       "idVuelo",
       "int"
      ],
      [
       "numeroAsiento",
       "string"
      ]
     ]
    },
    {
     "n": "BuscarPorId",
     "t": "Asiento_GV42",
     "v": "Public",
     "s": false,
     "p": [
      [
       "idAsiento",
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
       "idVuelo",
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
       "idAsiento",
       "int"
      ]
     ]
    },
    {
     "n": "EstaOcupado",
     "t": "bool",
     "v": "Public",
     "s": false,
     "p": [
      [
       "idAsiento",
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
       "idCheckIn",
       "int"
      ],
      [
       "idAsiento",
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
   ],
   "tipo": "Class"
  },
  {
   "nombre": "DALPago_GV42",
   "capa": "DAL",
   "padre": null,
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
       "idReserva",
       "int"
      ]
     ]
    }
   ],
   "tipo": "Class"
  },
  {
   "nombre": "DALBoleto_GV42",
   "capa": "DAL",
   "padre": null,
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
   ],
   "tipo": "Class"
  },
  {
   "nombre": "DALTipoAdicional_GV42",
   "capa": "DAL",
   "padre": null,
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
   ],
   "tipo": "Class"
  },
  {
   "nombre": "DALBitacora_GV42",
   "capa": "DAL",
   "padre": null,
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
     "n": "ResolverIdModulo",
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
     "n": "ResolverIdTipoEvento",
     "t": "int",
     "v": "Private",
     "s": false,
     "p": [
      [
       "t",
       "TipoEvento_GV42"
      ]
     ]
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
    }
   ],
   "tipo": "Class"
  },
  {
   "nombre": "DALIntegridad_GV42",
   "capa": "DAL",
   "padre": null,
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
     "s": false
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
     "n": "GuardarDVHs",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
      [
       "nombreTabla",
       "string"
      ],
      [
       "dvhs",
       "Dictionary<string, string>"
      ]
     ]
    },
    {
     "n": "GuardarDVV",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": [
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
    }
   ],
   "tipo": "Class"
  },
  {
   "nombre": "Acceso",
   "capa": "DAL",
   "padre": null,
   "attrs": [
    {
     "n": "_instancia",
     "t": "Acceso",
     "v": "Private",
     "s": true
    },
    {
     "n": "conexion",
     "t": "SqlConnection",
     "v": "Protected",
     "s": false
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
     "n": "conectar",
     "t": "void",
     "v": "Public",
     "s": false,
     "p": []
    },
    {
     "n": "desconectar",
     "t": "void",
     "v": "Public",
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
     "n": "CrearComando",
     "t": "SqlCommand",
     "v": "Private",
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
    }
   ],
   "tipo": "Class"
  },
  {
   "nombre": "Persona_GV42",
   "capa": "BE",
   "padre": null,
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
   ],
   "tipo": "Class"
  },
  {
   "nombre": "Pasajero_GV42",
   "capa": "BE",
   "padre": "Persona_GV42",
   "attrs": [],
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
    }
   ],
   "tipo": "Class"
  },
  {
   "nombre": "Reserva_GV42",
   "capa": "BE",
   "padre": null,
   "attrs": [
    {
     "n": "Id",
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
    }
   ],
   "metodos": [],
   "tipo": "Class"
  },
  {
   "nombre": "Vuelo_GV42",
   "capa": "BE",
   "padre": null,
   "attrs": [
    {
     "n": "Id",
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
   "metodos": [],
   "tipo": "Class"
  },
  {
   "nombre": "VueloClase_GV42",
   "capa": "BE",
   "padre": null,
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
   "metodos": [],
   "tipo": "Class"
  },
  {
   "nombre": "Aeropuerto_GV42",
   "capa": "BE",
   "padre": null,
   "attrs": [
    {
     "n": "Id",
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
   ],
   "tipo": "Class"
  },
  {
   "nombre": "Aerolinea_GV42",
   "capa": "BE",
   "padre": null,
   "attrs": [
    {
     "n": "Id",
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
     "n": "ToString",
     "t": "string",
     "v": "Public",
     "s": false,
     "p": []
    }
   ],
   "tipo": "Class"
  },
  {
   "nombre": "Asiento_GV42",
   "capa": "BE",
   "padre": null,
   "attrs": [
    {
     "n": "Id",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "IdVuelo",
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
   ],
   "tipo": "Class"
  },
  {
   "nombre": "AsientoPasajero_GV42",
   "capa": "BE",
   "padre": null,
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
   ],
   "tipo": "Class"
  },
  {
   "nombre": "AsientoDisponibilidad_GV42",
   "capa": "BE",
   "padre": null,
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
   "metodos": [],
   "tipo": "Class"
  },
  {
   "nombre": "AdicionalReserva_GV42",
   "capa": "BE",
   "padre": null,
   "attrs": [
    {
     "n": "Id",
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
   "metodos": [],
   "tipo": "Class"
  },
  {
   "nombre": "TipoAdicional_GV42",
   "capa": "BE",
   "padre": null,
   "attrs": [
    {
     "n": "Id",
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
   ],
   "tipo": "Class"
  },
  {
   "nombre": "Pago_GV42",
   "capa": "BE",
   "padre": null,
   "attrs": [
    {
     "n": "Id",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "IdReserva",
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
   "metodos": [],
   "tipo": "Class"
  },
  {
   "nombre": "Boleto_GV42",
   "capa": "BE",
   "padre": null,
   "attrs": [
    {
     "n": "Id",
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
   "metodos": [],
   "tipo": "Class"
  },
  {
   "nombre": "CriterioBusquedaVuelo_GV42",
   "capa": "BE",
   "padre": null,
   "attrs": [
    {
     "n": "IdOrigen",
     "t": "int",
     "v": "Public",
     "s": false
    },
    {
     "n": "IdDestino",
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
   "metodos": [],
   "tipo": "Class"
  },
  {
   "nombre": "NegocioException_GV42",
   "capa": "BE",
   "padre": "Exception",
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
   ],
   "tipo": "Class"
  },
  {
   "nombre": "Validaciones_GV42",
   "capa": "Servicios",
   "padre": null,
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
   ],
   "tipo": "Class"
  },
  {
   "nombre": "SessionManager_GV42",
   "capa": "Servicios",
   "padre": null,
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
   ],
   "tipo": "Class"
  },
  {
   "nombre": "CalculadorIntegridad_GV42",
   "capa": "Servicios",
   "padre": null,
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
   ],
   "tipo": "Class"
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
  }
 ],
 "relaciones": [
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
   "d": "BLLReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "CriterioBusquedaVuelo_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "CtrlButacas_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "FRMPagoReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "Pasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "Reserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMReservarVuelo_GV42",
   "d": "TipoAdicional_GV42",
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
   "o": "FRMPagoReserva_GV42",
   "d": "BLLReserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "FRMPagoReserva_GV42",
   "d": "Reserva_GV42",
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
   "d": "Boleto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "CriterioBusquedaVuelo_GV42",
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
   "d": "Reserva_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "SessionManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLReserva_GV42",
   "d": "TipoAdicional_GV42",
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
   "o": "BLLNegocioUtil_GV42",
   "d": "BLLBitacora_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLNegocioUtil_GV42",
   "d": "Persona_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLNegocioUtil_GV42",
   "d": "SessionManager_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLNegocioUtil_GV42",
   "d": "Validaciones_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLBitacora_GV42",
   "d": "DALBitacora_GV42",
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
   "d": "CalculadorIntegridad_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLIntegridad_GV42",
   "d": "DALIntegridad_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "BLLIntegridad_GV42",
   "d": "SessionManager_GV42",
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
   "d": "DALPago_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALReserva_GV42",
   "d": "Pasajero_GV42",
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
   "o": "DALPasajero_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "DALPasajero_GV42",
   "d": "Pasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALVuelo_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "DALVuelo_GV42",
   "d": "Aerolinea_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALVuelo_GV42",
   "d": "Aeropuerto_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALVuelo_GV42",
   "d": "CriterioBusquedaVuelo_GV42",
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
   "o": "DALPago_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "DALPago_GV42",
   "d": "Pago_GV42",
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
   "d": "Pasajero_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALTipoAdicional_GV42",
   "d": "Acceso",
   "tipo": "Dependency"
  },
  {
   "o": "DALTipoAdicional_GV42",
   "d": "TipoAdicional_GV42",
   "tipo": "Dependency"
  },
  {
   "o": "DALBitacora_GV42",
   "d": "Acceso",
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
   "o": "Pasajero_GV42",
   "d": "Persona_GV42",
   "tipo": "Generalization"
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
   "d": "TipoViaje_GV42",
   "tipo": "Association",
   "rol": "TipoViaje",
   "mult": "1"
  },
  {
   "o": "Reserva_GV42",
   "d": "Pasajero_GV42",
   "tipo": "Association",
   "rol": "Pasajeros",
   "mult": "0..*"
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
   "mult": "0..1"
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
   "o": "Vuelo_GV42",
   "d": "Aerolinea_GV42",
   "tipo": "Association",
   "rol": "Aerolinea",
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
   "o": "Vuelo_GV42",
   "d": "Aeropuerto_GV42",
   "tipo": "Association",
   "rol": "Destino",
   "mult": "1"
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
   "o": "Asiento_GV42",
   "d": "ClaseVuelo_GV42",
   "tipo": "Association",
   "rol": "Clase",
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
   "o": "AsientoDisponibilidad_GV42",
   "d": "Asiento_GV42",
   "tipo": "Association",
   "rol": "Asiento",
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
   "o": "Pago_GV42",
   "d": "MedioPago_GV42",
   "tipo": "Association",
   "rol": "MedioPago",
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
   "o": "CriterioBusquedaVuelo_GV42",
   "d": "TipoViaje_GV42",
   "tipo": "Association",
   "rol": "TipoViaje",
   "mult": "1"
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
    }
   ],
   "pk": [
    "DNI"
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
    }
   ],
   "pk": [
    "IdReserva",
    "DniPasajero"
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
  }
 }
};
var CASOS = [
 {
  "mensajes": [
   [
    "v",
    "frm",
    "ingresarCriterios(origen, destino, fechaSalida, fechaRegreso, cantidadPasajeros, clase)",
    "",
    130
   ],
   [
    "v",
    "frm",
    "btnBuscarVuelos_Click()",
    "",
    162
   ],
   [
    "frm",
    "bll",
    "BuscarVuelosDisponibles(criterio : CriterioBusquedaVuelo_GV42)",
    "",
    194
   ],
   [
    "bll",
    "bll",
    "ValidarCriterio(criterio)",
    "",
    226
   ],
   [
    "bll",
    "dVu",
    "BuscarDisponibles(criterio)",
    "",
    258
   ],
   [
    "dVu",
    "acc",
    "leer(query, parametros)",
    "",
    290
   ],
   [
    "acc",
    "dVu",
    "DataTable",
    "r",
    322
   ],
   [
    "dVu",
    "bll",
    "List<VueloClase_GV42>",
    "r",
    354
   ],
   [
    "bll",
    "frm",
    "List<VueloClase_GV42>",
    "r",
    386
   ],
   [
    "frm",
    "v",
    "\"No hay vuelos disponibles para esa búsqueda\"",
    "r",
    452
   ],
   [
    "frm",
    "v",
    "vuelos: código, aerolínea, origen, destino, salida, llegada, clase, disponibles",
    "r",
    514
   ],
   [
    "v",
    "frm",
    "seleccionarVuelo(vuelo) / btnSiguiente_Click()",
    "",
    574
   ],
   [
    "frm",
    "frm",
    "ValidarYAvanzarBusqueda()",
    "",
    606
   ],
   [
    "v",
    "frm",
    "ingresarDniCliente(dni) / btnBuscarCliente_Click()",
    "",
    638
   ],
   [
    "frm",
    "bll",
    "BuscarPasajero(dni : string)",
    "",
    670
   ],
   [
    "bll",
    "dPa",
    "BuscarPorDni(dni)",
    "",
    702
   ],
   [
    "dPa",
    "acc",
    "leer(query, parametros)",
    "",
    734
   ],
   [
    "acc",
    "dPa",
    "DataTable",
    "r",
    766
   ],
   [
    "dPa",
    "bll",
    "Pasajero_GV42",
    "r",
    798
   ],
   [
    "bll",
    "frm",
    "Pasajero_GV42",
    "r",
    830
   ],
   [
    "frm",
    "v",
    ":Cliente no existe",
    "r",
    896
   ],
   [
    "v",
    "frm",
    "RegistrarCliente()",
    "",
    928
   ],
   [
    "frm",
    "uc02",
    "«extend» CUN02 Registrar cliente",
    "",
    960
   ],
   [
    "uc02",
    "frm",
    ":Cliente registrado",
    "r",
    992
   ],
   [
    "frm",
    "v",
    ":Cliente registrado",
    "r",
    1024
   ],
   [
    "frm",
    "v",
    "datos del cliente (nombre, apellido, email, teléfono)",
    "r",
    1086
   ],
   [
    "v",
    "frm",
    "ingresarPasajero(dni, nombre, apellido, email, telefono)",
    "",
    1180
   ],
   [
    "v",
    "frm",
    "btnSiguiente_Click()",
    "",
    1240
   ],
   [
    "frm",
    "frm",
    "ValidarYAvanzarPasajeros()",
    "",
    1272
   ],
   [
    "frm",
    "bll",
    "ValidarPasajerosParaReserva(pasajeros : List<Pasajero_GV42>)",
    "",
    1304
   ],
   [
    "bll",
    "util",
    "ValidarPersona(p : Persona_GV42, rol : string)",
    "",
    1370
   ],
   [
    "bll",
    "bll",
    "VerificarIdentidad(p, rol, mostrarRegistrado)",
    "",
    1402
   ],
   [
    "bll",
    "dPa",
    "BuscarPorDni(p.DNI)",
    "",
    1434
   ],
   [
    "dPa",
    "acc",
    "leer(query, parametros)",
    "",
    1466
   ],
   [
    "acc",
    "dPa",
    "DataTable",
    "r",
    1498
   ],
   [
    "dPa",
    "bll",
    "Pasajero_GV42",
    "r",
    1530
   ],
   [
    "bll",
    "frm",
    "NegocioException_GV42(mensaje)",
    "r",
    1624
   ],
   [
    "frm",
    "v",
    "mensaje de error",
    "r",
    1656
   ],
   [
    "bll",
    "frm",
    "void",
    "r",
    1718
   ],
   [
    "v",
    "frm",
    "SeleccionarButacas()",
    "",
    1778
   ],
   [
    "frm",
    "uc06",
    "«include» CUN06 Seleccionar butaca disponible",
    "",
    1810
   ],
   [
    "uc06",
    "frm",
    ":Butacas asignadas",
    "r",
    1842
   ],
   [
    "frm",
    "v",
    ":Butacas asignadas",
    "r",
    1874
   ],
   [
    "v",
    "frm",
    "RegistrarAdicionales()",
    "",
    1940
   ],
   [
    "frm",
    "uc03",
    "«extend» CUN03 Registrar adicionales",
    "",
    1972
   ],
   [
    "uc03",
    "frm",
    ":Adicionales registrados",
    "r",
    2004
   ],
   [
    "frm",
    "v",
    ":Adicionales registrados",
    "r",
    2036
   ],
   [
    "v",
    "frm",
    "confirmarReserva() / btnSiguiente_Click()",
    "",
    2096
   ],
   [
    "frm",
    "frm",
    "ConfirmarReserva()",
    "",
    2128
   ],
   [
    "frm",
    "bll",
    "GenerarReserva(borrador : Reserva_GV42)",
    "",
    2160
   ],
   [
    "bll",
    "util",
    "LoginActual()",
    "",
    2192
   ],
   [
    "util",
    "bll",
    "login : string",
    "r",
    2224
   ],
   [
    "bll",
    "bll",
    "ValidarPasajerosParaReserva(pasajeros)",
    "",
    2256
   ],
   [
    "bll",
    "bll",
    "ValidarAdicionales(adicionales, canal)",
    "",
    2288
   ],
   [
    "bll",
    "dVu",
    "BuscarVueloClase(idVuelo : int, clase : ClaseVuelo_GV42)",
    "",
    2320
   ],
   [
    "dVu",
    "acc",
    "leer(query, parametros)",
    "",
    2352
   ],
   [
    "acc",
    "dVu",
    "DataTable",
    "r",
    2384
   ],
   [
    "dVu",
    "bll",
    "VueloClase_GV42",
    "r",
    2416
   ],
   [
    "bll",
    "bll",
    "ValidarAsientos(borrador, vc)",
    "",
    2448
   ],
   [
    "bll",
    "dAs",
    "EstaReservado(idAsiento : int)",
    "",
    2514
   ],
   [
    "dAs",
    "acc",
    "leerEscalar(query, parametros)",
    "",
    2546
   ],
   [
    "acc",
    "dAs",
    "cantidad : object",
    "r",
    2578
   ],
   [
    "dAs",
    "bll",
    "reservado : bool",
    "r",
    2610
   ],
   [
    "bll",
    "frm",
    "NegocioException_GV42(\"Solo quedan N asiento(s) disponible(s)\")",
    "r",
    2704
   ],
   [
    "frm",
    "v",
    "mensaje de error",
    "r",
    2736
   ],
   [
    "bll",
    "bll",
    "calcular ImporteBase, SubtotalAdicionales, Impuestos, ImporteTotal",
    "",
    2798
   ],
   [
    "bll",
    "dRe",
    "Crear(r : Reserva_GV42)",
    "",
    2830
   ],
   [
    "dRe",
    "acc",
    "EjecutarEnTransaccion(trabajo)  [UPDATE VueloClase; INSERT Reserva, ReservaPasajero, ReservaAdicional]",
    "",
    2862
   ],
   [
    "acc",
    "dRe",
    "Reserva_GV42",
    "r",
    2894
   ],
   [
    "dRe",
    "bll",
    "Reserva_GV42 (Pendiente de Pago)",
    "r",
    2926
   ],
   [
    "bll",
    "bll",
    "RecalcularIntegridad(\"Reserva\")",
    "",
    2958
   ],
   [
    "bll",
    "bInt",
    "RecalcularTabla(\"Reserva\")",
    "",
    2990
   ],
   [
    "bInt",
    "dInt",
    "CalcularDVHsTabla(\"Reserva\")",
    "",
    3022
   ],
   [
    "dInt",
    "acc",
    "leer(query, null)",
    "",
    3054
   ],
   [
    "acc",
    "dInt",
    "DataTable",
    "r",
    3086
   ],
   [
    "dInt",
    "bInt",
    "dvhs : Dictionary<string, string>",
    "r",
    3118
   ],
   [
    "bInt",
    "dInt",
    "GuardarDVHs(nombreTabla, dvhs)",
    "",
    3150
   ],
   [
    "bInt",
    "calc",
    "CalcularDVV(dvhs)",
    "",
    3182
   ],
   [
    "calc",
    "bInt",
    "dvv : string",
    "r",
    3214
   ],
   [
    "bInt",
    "dInt",
    "GuardarDVV(nombreTabla, dvv)",
    "",
    3246
   ],
   [
    "dInt",
    "acc",
    "escribir(query, parametros)",
    "",
    3278
   ],
   [
    "acc",
    "dInt",
    "filas afectadas : int",
    "r",
    3310
   ],
   [
    "bll",
    "util",
    "Auditar(modulo, \"Reserva generada\", detalle, criticidad)",
    "",
    3342
   ],
   [
    "util",
    "bBit",
    "RegistrarEvento(login, modulo, tipoEvento, detalle, criticidad)",
    "",
    3374
   ],
   [
    "bBit",
    "dBit",
    "Guardar(registro : Bitacora_GV42)",
    "",
    3406
   ],
   [
    "dBit",
    "acc",
    "escribir(\"INSERT INTO EVENTOS ...\", parametros)",
    "",
    3438
   ],
   [
    "acc",
    "dBit",
    "filas afectadas : int",
    "r",
    3470
   ],
   [
    "bll",
    "frm",
    "Reserva_GV42",
    "r",
    3502
   ],
   [
    "frm",
    "frm",
    "MostrarResultado()",
    "",
    3534
   ],
   [
    "frm",
    "v",
    "N° de reserva, pasajeros, vuelo, clase, adicionales, importes, estado \"Pendiente de Pago\"",
    "r",
    3566
   ]
  ],
  "fragmentos": [
   {
    "subtipo": 0,
    "nombre": "Vuelos disponibles",
    "l": 18,
    "r": 372,
    "t": 418,
    "b": 552,
    "ops": [
     {
      "guarda": "[lista.Count == 0]",
      "size": 66
     },
     {
      "guarda": "[lista.Count > 0]",
      "size": 68
     }
    ]
   },
   {
    "subtipo": 0,
    "nombre": "Cliente existente",
    "l": 18,
    "r": 562,
    "t": 862,
    "b": 1124,
    "ops": [
     {
      "guarda": "[cliente == null]",
      "size": 194
     },
     {
      "guarda": "[cliente != null]",
      "size": 68
     }
    ]
   },
   {
    "subtipo": 4,
    "nombre": "Por cada pasajero",
    "l": 18,
    "r": 372,
    "t": 1146,
    "b": 1218,
    "ops": [
     {
      "guarda": "[i < cantidadPasajeros]",
      "size": 72
     }
    ]
   },
   {
    "subtipo": 4,
    "nombre": "Por cada pasajero",
    "l": 968,
    "r": 3222,
    "t": 1336,
    "b": 1568,
    "ops": [
     {
      "guarda": "[p in pasajeros]",
      "size": 232
     }
    ]
   },
   {
    "subtipo": 0,
    "nombre": "Datos de pasajeros",
    "l": 18,
    "r": 1132,
    "t": 1590,
    "b": 1756,
    "ops": [
     {
      "guarda": "[datos inválidos o DNI registrado con otro nombre]",
      "size": 98
     },
     {
      "guarda": "[datos válidos]",
      "size": 68
     }
    ]
   },
   {
    "subtipo": 1,
    "nombre": "Adicionales",
    "l": 18,
    "r": 942,
    "t": 1906,
    "b": 2074,
    "ops": [
     {
      "guarda": "[el cliente solicita adicionales]",
      "size": 168
     }
    ]
   },
   {
    "subtipo": 4,
    "nombre": "Por cada butaca elegida",
    "l": 968,
    "r": 3222,
    "t": 2480,
    "b": 2648,
    "ops": [
     {
      "guarda": "[ap in AsientosPorPasajero]",
      "size": 168
     }
    ]
   },
   {
    "subtipo": 0,
    "nombre": "Disponibilidad",
    "l": 18,
    "r": 3222,
    "t": 2670,
    "b": 3604,
    "ops": [
     {
      "guarda": "[sin cupo o butaca ya tomada]",
      "size": 98
     },
     {
      "guarda": "[hay disponibilidad]",
      "size": 836
     }
    ]
   }
  ],
  "alto": 3676,
  "vidas": {
   "uc02": [
    926,
    1018
   ],
   "uc06": [
    1776,
    1868
   ],
   "uc03": [
    1938,
    2030
   ]
  },
  "id": "CUN01",
  "titulo": "Reservar vuelo",
  "lifelines": [
   "v",
   "frm",
   "uc02",
   "uc06",
   "uc03",
   "bll",
   "util",
   "bBit",
   "bInt",
   "calc",
   "dVu",
   "dPa",
   "dAs",
   "dRe",
   "dBit",
   "dInt",
   "acc"
  ],
  "notas": [],
  "clases": [
   "FRMReservarVuelo_GV42",
   "BLLReserva_GV42",
   "BLLNegocioUtil_GV42",
   "BLLBitacora_GV42",
   "BLLIntegridad_GV42",
   "DALVuelo_GV42",
   "DALPasajero_GV42",
   "DALAsiento_GV42",
   "DALReserva_GV42",
   "DALBitacora_GV42",
   "DALIntegridad_GV42",
   "Acceso",
   "CalculadorIntegridad_GV42",
   "Reserva_GV42",
   "Pasajero_GV42",
   "Persona_GV42",
   "CriterioBusquedaVuelo_GV42",
   "VueloClase_GV42",
   "Vuelo_GV42",
   "Aeropuerto_GV42",
   "Aerolinea_GV42",
   "Asiento_GV42",
   "AsientoPasajero_GV42",
   "AdicionalReserva_GV42",
   "ClaseVuelo_GV42",
   "TipoViaje_GV42",
   "EstadoReserva_GV42",
   "CanalVenta_GV42"
  ],
  "tablas": [
   "Reserva",
   "ReservaPasajero",
   "Pasajero",
   "Vuelo",
   "VueloClase",
   "ClaseVuelo",
   "Aeropuerto",
   "Aerolinea",
   "Asiento",
   "EstadoReserva",
   "TipoViaje",
   "CanalVenta",
   "ReservaAdicional",
   "TipoAdicional"
  ],
  "layoutClases": [
   {
    "n": "FRMReservarVuelo_GV42",
    "l": 1378,
    "t": 20,
    "w": 468,
    "h": 494
   },
   {
    "n": "BLLReserva_GV42",
    "l": 1292,
    "t": 624,
    "w": 640,
    "h": 760
   },
   {
    "n": "BLLNegocioUtil_GV42",
    "l": 1612,
    "t": 1494,
    "w": 546,
    "h": 200
   },
   {
    "n": "BLLIntegridad_GV42",
    "l": 2218,
    "t": 1494,
    "w": 354,
    "h": 382
   },
   {
    "n": "BLLBitacora_GV42",
    "l": 1562,
    "t": 1986,
    "w": 640,
    "h": 200
   },
   {
    "n": "DALVuelo_GV42",
    "l": 20,
    "t": 2296,
    "w": 498,
    "h": 228
   },
   {
    "n": "DALAsiento_GV42",
    "l": 608,
    "t": 2296,
    "w": 522,
    "h": 214
   },
   {
    "n": "DALReserva_GV42",
    "l": 1190,
    "t": 2296,
    "w": 444,
    "h": 228
   },
   {
    "n": "DALPasajero_GV42",
    "l": 1694,
    "t": 2296,
    "w": 318,
    "h": 158
   },
   {
    "n": "DALBitacora_GV42",
    "l": 2192,
    "t": 2296,
    "w": 640,
    "h": 270
   },
   {
    "n": "DALIntegridad_GV42",
    "l": 2892,
    "t": 2296,
    "w": 582,
    "h": 410
   },
   {
    "n": "Acceso",
    "l": 1636,
    "t": 2816,
    "w": 546,
    "h": 312
   },
   {
    "n": "CalculadorIntegridad_GV42",
    "l": 2812,
    "t": 2816,
    "w": 306,
    "h": 108
   },
   {
    "n": "Reserva_GV42",
    "l": 1513,
    "t": 3238,
    "w": 312,
    "h": 388
   },
   {
    "n": "CriterioBusquedaVuelo_GV42",
    "l": 1885,
    "t": 3238,
    "w": 186,
    "h": 150
   },
   {
    "n": "TipoViaje_GV42",
    "l": 670,
    "t": 3736,
    "w": 170,
    "h": 80
   },
   {
    "n": "AdicionalReserva_GV42",
    "l": 960,
    "t": 3736,
    "w": 228,
    "h": 136
   },
   {
    "n": "EstadoReserva_GV42",
    "l": 1248,
    "t": 3736,
    "w": 170,
    "h": 94
   },
   {
    "n": "CanalVenta_GV42",
    "l": 1478,
    "t": 3736,
    "w": 170,
    "h": 80
   },
   {
    "n": "VueloClase_GV42",
    "l": 1708,
    "t": 3736,
    "w": 204,
    "h": 248
   },
   {
    "n": "Pasajero_GV42",
    "l": 1972,
    "t": 3736,
    "w": 588,
    "h": 80
   },
   {
    "n": "AsientoPasajero_GV42",
    "l": 2620,
    "t": 3736,
    "w": 414,
    "h": 116
   },
   {
    "n": "Vuelo_GV42",
    "l": 1593,
    "t": 4094,
    "w": 186,
    "h": 206
   },
   {
    "n": "Persona_GV42",
    "l": 2079,
    "t": 4094,
    "w": 582,
    "h": 172
   },
   {
    "n": "Asiento_GV42",
    "l": 2871,
    "t": 4094,
    "w": 170,
    "h": 158
   },
   {
    "n": "Aeropuerto_GV42",
    "l": 1567,
    "t": 4410,
    "w": 170,
    "h": 158
   },
   {
    "n": "Aerolinea_GV42",
    "l": 1797,
    "t": 4410,
    "w": 170,
    "h": 102
   },
   {
    "n": "ClaseVuelo_GV42",
    "l": 2057,
    "t": 4410,
    "w": 170,
    "h": 94
   }
  ],
  "ocultar": [
   [
    "FRMReservarVuelo_GV42",
    "AdicionalReserva_GV42",
    "Dependency"
   ],
   [
    "FRMReservarVuelo_GV42",
    "Aeropuerto_GV42",
    "Dependency"
   ],
   [
    "FRMReservarVuelo_GV42",
    "AsientoPasajero_GV42",
    "Dependency"
   ],
   [
    "FRMReservarVuelo_GV42",
    "Asiento_GV42",
    "Dependency"
   ],
   [
    "FRMReservarVuelo_GV42",
    "CriterioBusquedaVuelo_GV42",
    "Dependency"
   ],
   [
    "FRMReservarVuelo_GV42",
    "Pasajero_GV42",
    "Dependency"
   ],
   [
    "FRMReservarVuelo_GV42",
    "Reserva_GV42",
    "Dependency"
   ],
   [
    "FRMReservarVuelo_GV42",
    "VueloClase_GV42",
    "Dependency"
   ],
   [
    "BLLReserva_GV42",
    "AdicionalReserva_GV42",
    "Dependency"
   ],
   [
    "BLLReserva_GV42",
    "Aeropuerto_GV42",
    "Dependency"
   ],
   [
    "BLLReserva_GV42",
    "AsientoPasajero_GV42",
    "Dependency"
   ],
   [
    "BLLReserva_GV42",
    "CriterioBusquedaVuelo_GV42",
    "Dependency"
   ],
   [
    "BLLReserva_GV42",
    "Pasajero_GV42",
    "Dependency"
   ],
   [
    "BLLReserva_GV42",
    "Persona_GV42",
    "Dependency"
   ],
   [
    "BLLReserva_GV42",
    "Reserva_GV42",
    "Dependency"
   ],
   [
    "BLLReserva_GV42",
    "VueloClase_GV42",
    "Dependency"
   ],
   [
    "BLLNegocioUtil_GV42",
    "Persona_GV42",
    "Dependency"
   ],
   [
    "DALReserva_GV42",
    "AdicionalReserva_GV42",
    "Dependency"
   ],
   [
    "DALReserva_GV42",
    "AsientoPasajero_GV42",
    "Dependency"
   ],
   [
    "DALReserva_GV42",
    "Asiento_GV42",
    "Dependency"
   ],
   [
    "DALReserva_GV42",
    "Pasajero_GV42",
    "Dependency"
   ],
   [
    "DALReserva_GV42",
    "Reserva_GV42",
    "Dependency"
   ],
   [
    "DALPasajero_GV42",
    "Pasajero_GV42",
    "Dependency"
   ],
   [
    "DALVuelo_GV42",
    "Aerolinea_GV42",
    "Dependency"
   ],
   [
    "DALVuelo_GV42",
    "Aeropuerto_GV42",
    "Dependency"
   ],
   [
    "DALVuelo_GV42",
    "CriterioBusquedaVuelo_GV42",
    "Dependency"
   ],
   [
    "DALVuelo_GV42",
    "VueloClase_GV42",
    "Dependency"
   ],
   [
    "DALVuelo_GV42",
    "Vuelo_GV42",
    "Dependency"
   ],
   [
    "DALAsiento_GV42",
    "Asiento_GV42",
    "Dependency"
   ]
  ]
 },
 {
  "mensajes": [
   [
    "v",
    "frm",
    "RegistrarCliente()",
    "",
    130
   ],
   [
    "frm",
    "bll",
    "PrecargarDesdeUsuario(dni : string)",
    "",
    162
   ],
   [
    "bll",
    "dPa",
    "BuscarDatosEnUsuario(dni)",
    "",
    194
   ],
   [
    "dPa",
    "acc",
    "leer(query, parametros)",
    "",
    226
   ],
   [
    "acc",
    "dPa",
    "DataTable",
    "r",
    258
   ],
   [
    "dPa",
    "bll",
    "Pasajero_GV42",
    "r",
    290
   ],
   [
    "bll",
    "frm",
    "Pasajero_GV42",
    "r",
    322
   ],
   [
    "frm",
    "v",
    "datos precargados de la cuenta (completar teléfono)",
    "r",
    388
   ],
   [
    "frm",
    "v",
    "formulario de alta vacío",
    "r",
    450
   ],
   [
    "v",
    "frm",
    "ingresarDatosCliente(nombre, apellido, dni, email, telefono)",
    "",
    510
   ],
   [
    "v",
    "frm",
    "btnRegistrarCliente_Click()",
    "",
    542
   ],
   [
    "frm",
    "bll",
    "RegistrarPasajero(pasajero : Pasajero_GV42)",
    "",
    574
   ],
   [
    "bll",
    "util",
    "ValidarPersona(pasajero, \"Cliente\")",
    "",
    606
   ],
   [
    "bll",
    "dPa",
    "ExisteDni(dni)",
    "",
    638
   ],
   [
    "dPa",
    "acc",
    "leerEscalar(query, parametros)",
    "",
    670
   ],
   [
    "acc",
    "dPa",
    "cantidad : object",
    "r",
    702
   ],
   [
    "dPa",
    "bll",
    "existe : bool",
    "r",
    734
   ],
   [
    "bll",
    "frm",
    "NegocioException_GV42(mensaje)",
    "r",
    800
   ],
   [
    "frm",
    "v",
    "mensaje de error",
    "r",
    832
   ],
   [
    "bll",
    "bll",
    "VerificarIdentidad(pasajero, \"Cliente\", true)",
    "",
    894
   ],
   [
    "bll",
    "dPa",
    "Insertar(p : Pasajero_GV42)",
    "",
    926
   ],
   [
    "dPa",
    "acc",
    "escribir(\"INSERT INTO Pasajero ...\", parametros)",
    "",
    958
   ],
   [
    "acc",
    "dPa",
    "filas afectadas : int",
    "r",
    990
   ],
   [
    "dPa",
    "bll",
    "void",
    "r",
    1022
   ],
   [
    "bll",
    "util",
    "Auditar(modulo, \"Cliente registrado\", detalle, criticidad)",
    "",
    1054
   ],
   [
    "util",
    "bBit",
    "RegistrarEvento(login, modulo, tipoEvento, detalle, criticidad)",
    "",
    1086
   ],
   [
    "bBit",
    "dBit",
    "Guardar(registro : Bitacora_GV42)",
    "",
    1118
   ],
   [
    "dBit",
    "acc",
    "escribir(\"INSERT INTO EVENTOS ...\", parametros)",
    "",
    1150
   ],
   [
    "acc",
    "dBit",
    "filas afectadas : int",
    "r",
    1182
   ],
   [
    "bll",
    "frm",
    "void",
    "r",
    1214
   ],
   [
    "frm",
    "v",
    "\"Cliente registrado\" (retorna a CUN01)",
    "r",
    1246
   ]
  ],
  "fragmentos": [
   {
    "subtipo": 0,
    "nombre": "Cuenta de usuario",
    "l": 18,
    "r": 372,
    "t": 354,
    "b": 488,
    "ops": [
     {
      "guarda": "[el DNI tiene cuenta de usuario]",
      "size": 66
     },
     {
      "guarda": "[sin cuenta]",
      "size": 68
     }
    ]
   },
   {
    "subtipo": 0,
    "nombre": "Validación del cliente",
    "l": 18,
    "r": 1512,
    "t": 766,
    "b": 1284,
    "ops": [
     {
      "guarda": "[datos inválidos o existe == true]",
      "size": 98
     },
     {
      "guarda": "[datos válidos y DNI no registrado]",
      "size": 420
     }
    ]
   }
  ],
  "alto": 1356,
  "vidas": {},
  "id": "CUN02",
  "titulo": "Registrar cliente",
  "lifelines": [
   "v",
   "frm",
   "bll",
   "util",
   "bBit",
   "dPa",
   "dBit",
   "acc"
  ],
  "notas": [],
  "clases": [
   "FRMReservarVuelo_GV42",
   "BLLReserva_GV42",
   "BLLNegocioUtil_GV42",
   "BLLBitacora_GV42",
   "DALPasajero_GV42",
   "DALBitacora_GV42",
   "Acceso",
   "Pasajero_GV42",
   "Persona_GV42",
   "Validaciones_GV42"
  ],
  "tablas": [
   "Pasajero",
   "Usuario",
   "Roles"
  ],
  "layoutClases": [
   {
    "n": "FRMReservarVuelo_GV42",
    "l": 326,
    "t": 20,
    "w": 468,
    "h": 494
   },
   {
    "n": "BLLReserva_GV42",
    "l": 510,
    "t": 624,
    "w": 640,
    "h": 760
   },
   {
    "n": "BLLNegocioUtil_GV42",
    "l": 497,
    "t": 1494,
    "w": 546,
    "h": 200
   },
   {
    "n": "BLLBitacora_GV42",
    "l": 660,
    "t": 1804,
    "w": 640,
    "h": 200
   },
   {
    "n": "DALBitacora_GV42",
    "l": 501,
    "t": 2114,
    "w": 640,
    "h": 270
   },
   {
    "n": "DALPasajero_GV42",
    "l": 1381,
    "t": 2114,
    "w": 318,
    "h": 158
   },
   {
    "n": "Validaciones_GV42",
    "l": 20,
    "t": 2494,
    "w": 294,
    "h": 438
   },
   {
    "n": "Acceso",
    "l": 884,
    "t": 2494,
    "w": 546,
    "h": 312
   },
   {
    "n": "Pasajero_GV42",
    "l": 416,
    "t": 3042,
    "w": 588,
    "h": 80
   },
   {
    "n": "Persona_GV42",
    "l": 419,
    "t": 3232,
    "w": 582,
    "h": 172
   }
  ],
  "ocultar": [
   [
    "FRMReservarVuelo_GV42",
    "Pasajero_GV42",
    "Dependency"
   ],
   [
    "BLLReserva_GV42",
    "Pasajero_GV42",
    "Dependency"
   ],
   [
    "BLLReserva_GV42",
    "Persona_GV42",
    "Dependency"
   ],
   [
    "BLLNegocioUtil_GV42",
    "Persona_GV42",
    "Dependency"
   ],
   [
    "DALPasajero_GV42",
    "Pasajero_GV42",
    "Dependency"
   ]
  ]
 },
 {
  "mensajes": [
   [
    "v",
    "frm",
    "RegistrarAdicionales()",
    "",
    130
   ],
   [
    "frm",
    "frm",
    "CargarAdicionales()",
    "",
    162
   ],
   [
    "frm",
    "bll",
    "ListarTiposAdicional()",
    "",
    194
   ],
   [
    "bll",
    "dTA",
    "ListarActivos()",
    "",
    226
   ],
   [
    "dTA",
    "acc",
    "leer(query, null)",
    "",
    258
   ],
   [
    "acc",
    "dTA",
    "DataTable",
    "r",
    290
   ],
   [
    "dTA",
    "bll",
    "List<TipoAdicional_GV42>",
    "r",
    322
   ],
   [
    "bll",
    "frm",
    "List<TipoAdicional_GV42>",
    "r",
    354
   ],
   [
    "frm",
    "v",
    "catálogo de adicionales (tipo, precio unitario)",
    "r",
    386
   ],
   [
    "v",
    "frm",
    "seleccionarAdicional(tipo, cantidad)",
    "",
    452
   ],
   [
    "frm",
    "frm",
    "calcular subtotal = Cantidad x CostoUnitario",
    "",
    484
   ],
   [
    "v",
    "frm",
    "confirmarAdicionales() / btnSiguiente_Click()",
    "",
    544
   ],
   [
    "frm",
    "frm",
    "ArmarResumen()",
    "",
    576
   ],
   [
    "frm",
    "v",
    "resumen con adicionales y subtotales",
    "r",
    608
   ],
   [
    "frm",
    "v",
    ":Adicionales registrados (retorno a CUN01)",
    "r",
    640
   ]
  ],
  "fragmentos": [
   {
    "subtipo": 4,
    "nombre": "Por cada adicional elegido",
    "l": 18,
    "r": 372,
    "t": 418,
    "b": 522,
    "ops": [
     {
      "guarda": "[el cliente pide otro adicional]",
      "size": 104
     }
    ]
   }
  ],
  "alto": 722,
  "vidas": {},
  "id": "CUN03",
  "titulo": "Registrar adicionales",
  "lifelines": [
   "v",
   "frm",
   "bll",
   "dTA",
   "acc"
  ],
  "notas": [],
  "clases": [
   "FRMReservarVuelo_GV42",
   "BLLReserva_GV42",
   "DALTipoAdicional_GV42",
   "Acceso",
   "Reserva_GV42",
   "AdicionalReserva_GV42",
   "TipoAdicional_GV42"
  ],
  "tablas": [
   "Reserva",
   "ReservaAdicional",
   "TipoAdicional"
  ],
  "layoutClases": [
   {
    "n": "FRMReservarVuelo_GV42",
    "l": 106,
    "t": 20,
    "w": 468,
    "h": 494
   },
   {
    "n": "BLLReserva_GV42",
    "l": 20,
    "t": 624,
    "w": 640,
    "h": 760
   },
   {
    "n": "DALTipoAdicional_GV42",
    "l": 202,
    "t": 1494,
    "w": 276,
    "h": 102
   },
   {
    "n": "Acceso",
    "l": 67,
    "t": 1706,
    "w": 546,
    "h": 312
   },
   {
    "n": "Reserva_GV42",
    "l": 184,
    "t": 2128,
    "w": 312,
    "h": 388
   },
   {
    "n": "AdicionalReserva_GV42",
    "l": 226,
    "t": 2626,
    "w": 228,
    "h": 136
   },
   {
    "n": "TipoAdicional_GV42",
    "l": 255,
    "t": 2872,
    "w": 170,
    "h": 116
   }
  ],
  "ocultar": [
   [
    "FRMReservarVuelo_GV42",
    "AdicionalReserva_GV42",
    "Dependency"
   ],
   [
    "FRMReservarVuelo_GV42",
    "Reserva_GV42",
    "Dependency"
   ],
   [
    "FRMReservarVuelo_GV42",
    "TipoAdicional_GV42",
    "Dependency"
   ],
   [
    "BLLReserva_GV42",
    "AdicionalReserva_GV42",
    "Dependency"
   ],
   [
    "BLLReserva_GV42",
    "Reserva_GV42",
    "Dependency"
   ],
   [
    "BLLReserva_GV42",
    "TipoAdicional_GV42",
    "Dependency"
   ],
   [
    "DALTipoAdicional_GV42",
    "TipoAdicional_GV42",
    "Dependency"
   ]
  ]
 },
 {
  "mensajes": [
   [
    "v",
    "frmP",
    "ingresarNumeroReserva(numero) / btnBuscar_Click()",
    "",
    130
   ],
   [
    "frmP",
    "bll",
    "BuscarReserva(numeroReserva : string)",
    "",
    162
   ],
   [
    "bll",
    "dRe",
    "BuscarPorNumero(numeroReserva)",
    "",
    194
   ],
   [
    "dRe",
    "acc",
    "leer(query, parametros)",
    "",
    226
   ],
   [
    "acc",
    "dRe",
    "DataTable",
    "r",
    258
   ],
   [
    "dRe",
    "bll",
    "Reserva_GV42",
    "r",
    290
   ],
   [
    "bll",
    "frmP",
    "Reserva_GV42",
    "r",
    322
   ],
   [
    "frmP",
    "v",
    "\"No existe la reserva\" / \"ya no está pendiente de pago\"",
    "r",
    388
   ],
   [
    "frmP",
    "v",
    "N° de reserva, cliente e importe total a cobrar",
    "r",
    450
   ],
   [
    "v",
    "frmP",
    "seleccionarMedioPago(medio : MedioPago_GV42)",
    "",
    510
   ],
   [
    "v",
    "frmP",
    "ingresarNumeroTransaccion(numero)  (transferencia simulada)",
    "",
    576
   ],
   [
    "v",
    "frmP",
    "dejar número de transacción vacío (se autogenera)",
    "",
    638
   ],
   [
    "v",
    "frmP",
    "btnConfirmarPago_Click()",
    "",
    698
   ],
   [
    "frmP",
    "bll",
    "RegistrarPago(numeroReserva, medioPago, importeAbonado, numeroTransaccion)",
    "",
    730
   ],
   [
    "bll",
    "util",
    "LoginActual()",
    "",
    762
   ],
   [
    "util",
    "bll",
    "login : string",
    "r",
    794
   ],
   [
    "bll",
    "bll",
    "BuscarReserva(numeroReserva)",
    "",
    826
   ],
   [
    "bll",
    "frmP",
    "NegocioException_GV42(mensaje)",
    "r",
    892
   ],
   [
    "frmP",
    "v",
    "mensaje de error",
    "r",
    924
   ],
   [
    "bll",
    "bll",
    "numeroTransaccion = \"EFE-\" + fecha y hora",
    "",
    1020
   ],
   [
    "bll",
    "dPg",
    "RegistrarPagoYConfirmar(pago : Pago_GV42)",
    "",
    1080
   ],
   [
    "dPg",
    "acc",
    "EjecutarEnTransaccion(trabajo)  [UPDATE Reserva -> Confirmada; INSERT Pago, Boleto]",
    "",
    1112
   ],
   [
    "acc",
    "dPg",
    "Pago_GV42",
    "r",
    1144
   ],
   [
    "dPg",
    "bll",
    "Pago_GV42",
    "r",
    1176
   ],
   [
    "bll",
    "bll",
    "RecalcularIntegridad(\"Pago\")",
    "",
    1208
   ],
   [
    "bll",
    "bInt",
    "RecalcularTabla(\"Pago\")",
    "",
    1240
   ],
   [
    "bInt",
    "dInt",
    "CalcularDVHsTabla(\"Pago\")",
    "",
    1272
   ],
   [
    "dInt",
    "acc",
    "leer(query, null)",
    "",
    1304
   ],
   [
    "acc",
    "dInt",
    "DataTable",
    "r",
    1336
   ],
   [
    "dInt",
    "bInt",
    "dvhs : Dictionary<string, string>",
    "r",
    1368
   ],
   [
    "bInt",
    "dInt",
    "GuardarDVHs(nombreTabla, dvhs)",
    "",
    1400
   ],
   [
    "bInt",
    "calc",
    "CalcularDVV(dvhs)",
    "",
    1432
   ],
   [
    "calc",
    "bInt",
    "dvv : string",
    "r",
    1464
   ],
   [
    "bInt",
    "dInt",
    "GuardarDVV(nombreTabla, dvv)",
    "",
    1496
   ],
   [
    "dInt",
    "acc",
    "escribir(query, parametros)",
    "",
    1528
   ],
   [
    "acc",
    "dInt",
    "filas afectadas : int",
    "r",
    1560
   ],
   [
    "bll",
    "bll",
    "RecalcularIntegridad(\"Reserva\")",
    "",
    1592
   ],
   [
    "bll",
    "bInt",
    "RecalcularTabla(\"Reserva\")",
    "",
    1624
   ],
   [
    "bll",
    "util",
    "Auditar(modulo, \"Pago registrado\", detalle, criticidad)",
    "",
    1656
   ],
   [
    "util",
    "bBit",
    "RegistrarEvento(login, modulo, tipoEvento, detalle, criticidad)",
    "",
    1688
   ],
   [
    "bBit",
    "dBit",
    "Guardar(registro : Bitacora_GV42)",
    "",
    1720
   ],
   [
    "dBit",
    "acc",
    "escribir(\"INSERT INTO EVENTOS ...\", parametros)",
    "",
    1752
   ],
   [
    "acc",
    "dBit",
    "filas afectadas : int",
    "r",
    1784
   ],
   [
    "bll",
    "frmP",
    "Pago_GV42",
    "r",
    1816
   ],
   [
    "frmP",
    "uc05",
    "«include» CUN05 Generar boletos",
    "",
    1848
   ],
   [
    "uc05",
    "frmP",
    ":Boletos generados",
    "r",
    1880
   ],
   [
    "frmP",
    "v",
    "\"Pago registrado y reserva confirmada\" + boletos",
    "r",
    1912
   ]
  ],
  "fragmentos": [
   {
    "subtipo": 0,
    "nombre": "Reserva a cobrar",
    "l": 18,
    "r": 372,
    "t": 354,
    "b": 488,
    "ops": [
     {
      "guarda": "[reserva == null o Estado != Pendiente de Pago]",
      "size": 66
     },
     {
      "guarda": "[Estado == Pendiente de Pago]",
      "size": 68
     }
    ]
   },
   {
    "subtipo": 0,
    "nombre": "Medio de pago",
    "l": 18,
    "r": 372,
    "t": 542,
    "b": 676,
    "ops": [
     {
      "guarda": "[Tarjeta o Transferencia]",
      "size": 66
     },
     {
      "guarda": "[Efectivo]",
      "size": 68
     }
    ]
   },
   {
    "subtipo": 0,
    "nombre": "Validación del pago",
    "l": 18,
    "r": 2462,
    "t": 858,
    "b": 1950,
    "ops": [
     {
      "guarda": "[Estado != Pendiente de Pago o importe != ImporteTotal]",
      "size": 98
     },
     {
      "guarda": "[pago válido]",
      "size": 994
     }
    ]
   },
   {
    "subtipo": 1,
    "nombre": "Efectivo",
    "l": 594,
    "r": 746,
    "t": 986,
    "b": 1058,
    "ops": [
     {
      "guarda": "[numeroTransaccion vacío y medio == Efectivo]",
      "size": 72
     }
    ]
   }
  ],
  "alto": 2022,
  "vidas": {
   "uc05": [
    1814,
    1906
   ]
  },
  "id": "CUN04",
  "titulo": "Registrar pago",
  "lifelines": [
   "v",
   "frmP",
   "uc05",
   "bll",
   "util",
   "bBit",
   "bInt",
   "calc",
   "dRe",
   "dPg",
   "dBit",
   "dInt",
   "acc"
  ],
  "notas": [],
  "clases": [
   "FRMPagoReserva_GV42",
   "BLLReserva_GV42",
   "BLLNegocioUtil_GV42",
   "BLLBitacora_GV42",
   "BLLIntegridad_GV42",
   "DALReserva_GV42",
   "DALPago_GV42",
   "DALBitacora_GV42",
   "DALIntegridad_GV42",
   "Acceso",
   "CalculadorIntegridad_GV42",
   "Reserva_GV42",
   "Pago_GV42",
   "MedioPago_GV42",
   "EstadoReserva_GV42"
  ],
  "tablas": [
   "Reserva",
   "Pago",
   "MedioPago",
   "EstadoReserva",
   "Pasajero"
  ],
  "layoutClases": [
   {
    "n": "FRMPagoReserva_GV42",
    "l": 514,
    "t": 20,
    "w": 378,
    "h": 130
   },
   {
    "n": "BLLReserva_GV42",
    "l": 383,
    "t": 260,
    "w": 640,
    "h": 760
   },
   {
    "n": "BLLNegocioUtil_GV42",
    "l": 553,
    "t": 1130,
    "w": 546,
    "h": 200
   },
   {
    "n": "BLLIntegridad_GV42",
    "l": 1279,
    "t": 1130,
    "w": 354,
    "h": 382
   },
   {
    "n": "BLLBitacora_GV42",
    "l": 623,
    "t": 1622,
    "w": 640,
    "h": 200
   },
   {
    "n": "DALReserva_GV42",
    "l": 20,
    "t": 1932,
    "w": 444,
    "h": 228
   },
   {
    "n": "DALBitacora_GV42",
    "l": 524,
    "t": 1932,
    "w": 640,
    "h": 270
   },
   {
    "n": "DALIntegridad_GV42",
    "l": 1464,
    "t": 1932,
    "w": 582,
    "h": 410
   },
   {
    "n": "DALPago_GV42",
    "l": 265,
    "t": 2452,
    "w": 336,
    "h": 116
   },
   {
    "n": "Acceso",
    "l": 697,
    "t": 2678,
    "w": 546,
    "h": 312
   },
   {
    "n": "CalculadorIntegridad_GV42",
    "l": 1303,
    "t": 2678,
    "w": 306,
    "h": 108
   },
   {
    "n": "Reserva_GV42",
    "l": 757,
    "t": 3100,
    "w": 312,
    "h": 388
   },
   {
    "n": "Pago_GV42",
    "l": 699,
    "t": 3598,
    "w": 198,
    "h": 178
   },
   {
    "n": "EstadoReserva_GV42",
    "l": 957,
    "t": 3598,
    "w": 170,
    "h": 94
   },
   {
    "n": "MedioPago_GV42",
    "l": 708,
    "t": 3886,
    "w": 170,
    "h": 108
   }
  ],
  "ocultar": [
   [
    "FRMPagoReserva_GV42",
    "Reserva_GV42",
    "Dependency"
   ],
   [
    "BLLReserva_GV42",
    "Pago_GV42",
    "Dependency"
   ],
   [
    "BLLReserva_GV42",
    "Reserva_GV42",
    "Dependency"
   ],
   [
    "DALReserva_GV42",
    "Reserva_GV42",
    "Dependency"
   ],
   [
    "DALPago_GV42",
    "Pago_GV42",
    "Dependency"
   ]
  ]
 },
 {
  "mensajes": [
   [
    "v",
    "frmP",
    "GenerarBoletos()  (al confirmar el pago en CUN04)",
    "",
    130
   ],
   [
    "frmP",
    "bll",
    "RegistrarPago(numeroReserva, medioPago, importeAbonado, numeroTransaccion)",
    "",
    162
   ],
   [
    "bll",
    "dPg",
    "RegistrarPagoYConfirmar(pago : Pago_GV42)",
    "",
    194
   ],
   [
    "dPg",
    "acc",
    "escribir(tx, \"INSERT INTO Boleto (IdReserva, DniPasajero) SELECT ... FROM ReservaPasajero\", parametros)",
    "",
    226
   ],
   [
    "acc",
    "dPg",
    "filas afectadas : int  (un boleto por pasajero)",
    "r",
    258
   ],
   [
    "dPg",
    "bll",
    "Pago_GV42",
    "r",
    290
   ],
   [
    "bll",
    "frmP",
    "Pago_GV42",
    "r",
    322
   ],
   [
    "frmP",
    "bll",
    "ObtenerBoletos(numeroReserva : string)",
    "",
    354
   ],
   [
    "bll",
    "bll",
    "BuscarReserva(numeroReserva)",
    "",
    386
   ],
   [
    "bll",
    "frmP",
    "NegocioException_GV42(\"La reserva todavía no está confirmada\")",
    "r",
    452
   ],
   [
    "frmP",
    "v",
    "mensaje de error",
    "r",
    484
   ],
   [
    "bll",
    "dBo",
    "ListarPorReserva(numeroReserva)",
    "",
    546
   ],
   [
    "dBo",
    "acc",
    "leer(query, parametros)",
    "",
    578
   ],
   [
    "acc",
    "dBo",
    "DataTable",
    "r",
    610
   ],
   [
    "dBo",
    "bll",
    "List<Boleto_GV42>",
    "r",
    642
   ],
   [
    "bll",
    "frmP",
    "List<Boleto_GV42>",
    "r",
    674
   ],
   [
    "frmP",
    "v",
    "boletos: N° de boleto, N° de reserva, pasajero (nombre, apellido, DNI), vuelo",
    "r",
    706
   ]
  ],
  "fragmentos": [
   {
    "subtipo": 0,
    "nombre": "Estado de la reserva",
    "l": 18,
    "r": 1132,
    "t": 418,
    "b": 744,
    "ops": [
     {
      "guarda": "[Estado != Confirmada]",
      "size": 98
     },
     {
      "guarda": "[Estado == Confirmada]",
      "size": 228
     }
    ]
   }
  ],
  "alto": 816,
  "vidas": {},
  "id": "CUN05",
  "titulo": "Generar boletos",
  "lifelines": [
   "v",
   "frmP",
   "bll",
   "dPg",
   "dBo",
   "acc"
  ],
  "notas": [],
  "clases": [
   "FRMPagoReserva_GV42",
   "BLLReserva_GV42",
   "DALPago_GV42",
   "DALBoleto_GV42",
   "Acceso",
   "Boleto_GV42",
   "Reserva_GV42",
   "Pasajero_GV42",
   "Persona_GV42"
  ],
  "tablas": [
   "Boleto",
   "Reserva",
   "ReservaPasajero",
   "Pasajero",
   "Vuelo"
  ],
  "layoutClases": [
   {
    "n": "FRMPagoReserva_GV42",
    "l": 218,
    "t": 20,
    "w": 378,
    "h": 130
   },
   {
    "n": "BLLReserva_GV42",
    "l": 87,
    "t": 260,
    "w": 640,
    "h": 760
   },
   {
    "n": "DALPago_GV42",
    "l": 20,
    "t": 1130,
    "w": 336,
    "h": 116
   },
   {
    "n": "DALBoleto_GV42",
    "l": 416,
    "t": 1130,
    "w": 378,
    "h": 102
   },
   {
    "n": "Acceso",
    "l": 134,
    "t": 1356,
    "w": 546,
    "h": 312
   },
   {
    "n": "Boleto_GV42",
    "l": 136,
    "t": 1778,
    "w": 170,
    "h": 164
   },
   {
    "n": "Reserva_GV42",
    "l": 366,
    "t": 1778,
    "w": 312,
    "h": 388
   },
   {
    "n": "Pasajero_GV42",
    "l": 113,
    "t": 2276,
    "w": 588,
    "h": 80
   },
   {
    "n": "Persona_GV42",
    "l": 116,
    "t": 2466,
    "w": 582,
    "h": 172
   }
  ],
  "ocultar": [
   [
    "FRMPagoReserva_GV42",
    "Reserva_GV42",
    "Dependency"
   ],
   [
    "BLLReserva_GV42",
    "Boleto_GV42",
    "Dependency"
   ],
   [
    "BLLReserva_GV42",
    "Pasajero_GV42",
    "Dependency"
   ],
   [
    "BLLReserva_GV42",
    "Persona_GV42",
    "Dependency"
   ],
   [
    "BLLReserva_GV42",
    "Reserva_GV42",
    "Dependency"
   ],
   [
    "DALBoleto_GV42",
    "Boleto_GV42",
    "Dependency"
   ],
   [
    "DALBoleto_GV42",
    "Pasajero_GV42",
    "Dependency"
   ]
  ]
 },
 {
  "mensajes": [
   [
    "v",
    "frm",
    "SeleccionarButacas()",
    "",
    130
   ],
   [
    "frm",
    "frm",
    "PrepararPasoAsientos()",
    "",
    162
   ],
   [
    "frm",
    "bll",
    "ObtenerMapaAsientos(idVuelo : int, clase : ClaseVuelo_GV42)",
    "",
    194
   ],
   [
    "bll",
    "dVu",
    "BuscarVueloClase(idVuelo, clase)",
    "",
    226
   ],
   [
    "dVu",
    "acc",
    "leer(query, parametros)",
    "",
    258
   ],
   [
    "acc",
    "dVu",
    "DataTable",
    "r",
    290
   ],
   [
    "dVu",
    "bll",
    "VueloClase_GV42",
    "r",
    322
   ],
   [
    "bll",
    "frm",
    "NegocioException_GV42(\"El vuelo no ofrece la clase seleccionada\")",
    "r",
    388
   ],
   [
    "frm",
    "v",
    "mensaje de error",
    "r",
    420
   ],
   [
    "bll",
    "dAs",
    "ListarMapa(idVuelo, clase)",
    "",
    482
   ],
   [
    "dAs",
    "acc",
    "leer(\"SELECT ... FROM Asiento LEFT JOIN ReservaPasajero ...\", parametros)",
    "",
    514
   ],
   [
    "acc",
    "dAs",
    "DataTable",
    "r",
    546
   ],
   [
    "dAs",
    "bll",
    "List<AsientoDisponibilidad_GV42>",
    "r",
    578
   ],
   [
    "bll",
    "frm",
    "List<AsientoDisponibilidad_GV42>",
    "r",
    610
   ],
   [
    "frm",
    "frm",
    "RefrescarButacas()",
    "",
    642
   ],
   [
    "frm",
    "ctrl",
    "CargarMapa(asientos, ocupadosLocalmente, seleccionActualId)",
    "",
    674
   ],
   [
    "ctrl",
    "v",
    "mapa de butacas: libres, ocupadas y elegidas (fila, letra, ubicación)",
    "r",
    706
   ],
   [
    "v",
    "ctrl",
    "clickButaca(asiento)",
    "",
    800
   ],
   [
    "ctrl",
    "v",
    "butaca deshabilitada (no se puede elegir)",
    "r",
    866
   ],
   [
    "ctrl",
    "frm",
    "AsientoClickeado(sender, asiento : Asiento_GV42)",
    "",
    928
   ],
   [
    "frm",
    "frm",
    "ctrlButacas_AsientoClickeado(sender, asiento)",
    "",
    960
   ],
   [
    "frm",
    "frm",
    "RefrescarButacas()",
    "",
    992
   ],
   [
    "frm",
    "ctrl",
    "CargarMapa(asientos, ocupadosLocalmente, seleccionActualId)",
    "",
    1024
   ],
   [
    "ctrl",
    "v",
    "butaca marcada como elegida para el pasajero",
    "r",
    1056
   ],
   [
    "v",
    "frm",
    "pasajeroSiguiente() / CambiarPasajeroActivo(+1)",
    "",
    1116
   ],
   [
    "v",
    "frm",
    "btnSiguiente_Click()",
    "",
    1176
   ],
   [
    "frm",
    "frm",
    "ValidarYAvanzarAsientos()",
    "",
    1208
   ],
   [
    "frm",
    "v",
    "\"Elegí un asiento para cada pasajero\"",
    "r",
    1274
   ],
   [
    "frm",
    "v",
    ":Butacas asignadas (retorno a CUN01)",
    "r",
    1336
   ]
  ],
  "fragmentos": [
   {
    "subtipo": 0,
    "nombre": "Clase del vuelo",
    "l": 18,
    "r": 1322,
    "t": 354,
    "b": 744,
    "ops": [
     {
      "guarda": "[vc == null]",
      "size": 98
     },
     {
      "guarda": "[vc != null]",
      "size": 292
     }
    ]
   },
   {
    "subtipo": 4,
    "nombre": "Por cada pasajero",
    "l": 18,
    "r": 562,
    "t": 766,
    "b": 1154,
    "ops": [
     {
      "guarda": "[i < cantidadPasajeros]",
      "size": 388
     }
    ]
   },
   {
    "subtipo": 0,
    "nombre": "Disponibilidad de la butaca",
    "l": 24,
    "r": 556,
    "t": 832,
    "b": 1094,
    "ops": [
     {
      "guarda": "[Ocupado o elegida por otro pasajero de la reserva]",
      "size": 66
     },
     {
      "guarda": "[butaca libre]",
      "size": 196
     }
    ]
   },
   {
    "subtipo": 0,
    "nombre": "Butacas elegidas",
    "l": 18,
    "r": 372,
    "t": 1240,
    "b": 1374,
    "ops": [
     {
      "guarda": "[_asientoPorPasajero.Count != cantidadPasajeros]",
      "size": 66
     },
     {
      "guarda": "[todos los pasajeros tienen butaca]",
      "size": 68
     }
    ]
   }
  ],
  "alto": 1446,
  "vidas": {},
  "id": "CUN06",
  "titulo": "Seleccionar butaca disponible",
  "lifelines": [
   "v",
   "frm",
   "ctrl",
   "bll",
   "dVu",
   "dAs",
   "acc"
  ],
  "notas": [],
  "clases": [
   "FRMReservarVuelo_GV42",
   "CtrlButacas_GV42",
   "BLLReserva_GV42",
   "DALVuelo_GV42",
   "DALAsiento_GV42",
   "Acceso",
   "Asiento_GV42",
   "AsientoDisponibilidad_GV42",
   "AsientoPasajero_GV42",
   "VueloClase_GV42",
   "Reserva_GV42",
   "ClaseVuelo_GV42"
  ],
  "tablas": [
   "Asiento",
   "ReservaPasajero",
   "Reserva",
   "Vuelo",
   "VueloClase",
   "ClaseVuelo"
  ],
  "layoutClases": [
   {
    "n": "FRMReservarVuelo_GV42",
    "l": 326,
    "t": 20,
    "w": 468,
    "h": 494
   },
   {
    "n": "CtrlButacas_GV42",
    "l": 240,
    "t": 624,
    "w": 640,
    "h": 200
   },
   {
    "n": "BLLReserva_GV42",
    "l": 240,
    "t": 934,
    "w": 640,
    "h": 760
   },
   {
    "n": "DALVuelo_GV42",
    "l": 20,
    "t": 1804,
    "w": 498,
    "h": 228
   },
   {
    "n": "DALAsiento_GV42",
    "l": 578,
    "t": 1804,
    "w": 522,
    "h": 214
   },
   {
    "n": "Acceso",
    "l": 287,
    "t": 2142,
    "w": 546,
    "h": 312
   },
   {
    "n": "AsientoDisponibilidad_GV42",
    "l": 401,
    "t": 2564,
    "w": 186,
    "h": 136
   },
   {
    "n": "Reserva_GV42",
    "l": 647,
    "t": 2564,
    "w": 312,
    "h": 388
   },
   {
    "n": "AsientoPasajero_GV42",
    "l": 341,
    "t": 3062,
    "w": 414,
    "h": 116
   },
   {
    "n": "VueloClase_GV42",
    "l": 815,
    "t": 3062,
    "w": 204,
    "h": 248
   },
   {
    "n": "Asiento_GV42",
    "l": 475,
    "t": 3420,
    "w": 170,
    "h": 158
   },
   {
    "n": "ClaseVuelo_GV42",
    "l": 625,
    "t": 3688,
    "w": 170,
    "h": 94
   }
  ],
  "ocultar": [
   [
    "FRMReservarVuelo_GV42",
    "AsientoDisponibilidad_GV42",
    "Dependency"
   ],
   [
    "FRMReservarVuelo_GV42",
    "AsientoPasajero_GV42",
    "Dependency"
   ],
   [
    "FRMReservarVuelo_GV42",
    "Asiento_GV42",
    "Dependency"
   ],
   [
    "FRMReservarVuelo_GV42",
    "Reserva_GV42",
    "Dependency"
   ],
   [
    "FRMReservarVuelo_GV42",
    "VueloClase_GV42",
    "Dependency"
   ],
   [
    "CtrlButacas_GV42",
    "AsientoDisponibilidad_GV42",
    "Dependency"
   ],
   [
    "CtrlButacas_GV42",
    "Asiento_GV42",
    "Dependency"
   ],
   [
    "BLLReserva_GV42",
    "AsientoDisponibilidad_GV42",
    "Dependency"
   ],
   [
    "BLLReserva_GV42",
    "AsientoPasajero_GV42",
    "Dependency"
   ],
   [
    "BLLReserva_GV42",
    "Reserva_GV42",
    "Dependency"
   ],
   [
    "BLLReserva_GV42",
    "VueloClase_GV42",
    "Dependency"
   ],
   [
    "DALVuelo_GV42",
    "VueloClase_GV42",
    "Dependency"
   ],
   [
    "DALAsiento_GV42",
    "AsientoDisponibilidad_GV42",
    "Dependency"
   ],
   [
    "DALAsiento_GV42",
    "Asiento_GV42",
    "Dependency"
   ]
  ]
 }
];
var LIFELINES = {
 "v": {
  "nombre": "Vendedor",
  "actor": "Vendedor"
 },
 "frm": {
  "nombre": "FRMReservarVuelo_GV42",
  "clase": "FRMReservarVuelo_GV42",
  "est": "boundary"
 },
 "frmP": {
  "nombre": "FRMPagoReserva_GV42",
  "clase": "FRMPagoReserva_GV42",
  "est": "boundary"
 },
 "bll": {
  "nombre": "BLLReserva_GV42",
  "clase": "BLLReserva_GV42"
 },
 "util": {
  "nombre": "BLLNegocioUtil_GV42",
  "clase": "BLLNegocioUtil_GV42"
 },
 "bBit": {
  "nombre": "BLLBitacora_GV42",
  "clase": "BLLBitacora_GV42"
 },
 "bInt": {
  "nombre": "DV: BLLIntegridad_GV42",
  "clase": "BLLIntegridad_GV42"
 },
 "calc": {
  "nombre": "Servicio: CalculadorIntegridad_GV42",
  "clase": "CalculadorIntegridad_GV42"
 },
 "dVu": {
  "nombre": "DALVuelo_GV42",
  "clase": "DALVuelo_GV42"
 },
 "dPa": {
  "nombre": "DALPasajero_GV42",
  "clase": "DALPasajero_GV42"
 },
 "dAs": {
  "nombre": "DALAsiento_GV42",
  "clase": "DALAsiento_GV42"
 },
 "dRe": {
  "nombre": "DALReserva_GV42",
  "clase": "DALReserva_GV42"
 },
 "dTA": {
  "nombre": "DALTipoAdicional_GV42",
  "clase": "DALTipoAdicional_GV42"
 },
 "dPg": {
  "nombre": "DALPago_GV42",
  "clase": "DALPago_GV42"
 },
 "dBo": {
  "nombre": "DALBoleto_GV42",
  "clase": "DALBoleto_GV42"
 },
 "dBit": {
  "nombre": "DALBitacora_GV42",
  "clase": "DALBitacora_GV42"
 },
 "dInt": {
  "nombre": "DV: DALIntegridad_GV42",
  "clase": "DALIntegridad_GV42"
 },
 "acc": {
  "nombre": "Acceso",
  "clase": "Acceso"
 },
 "ctrl": {
  "nombre": "CtrlButacas_GV42",
  "clase": "CtrlButacas_GV42",
  "est": "boundary"
 },
 "uc02": {
  "nombre": "CUN02 Registrar cliente",
  "cu": "CUN02"
 },
 "uc03": {
  "nombre": "CUN03 Registrar adicionales",
  "cu": "CUN03"
 },
 "uc05": {
  "nombre": "CUN05 Generar boletos",
  "cu": "CUN05"
 },
 "uc06": {
  "nombre": "CUN06 Seleccionar butaca disponible",
  "cu": "CUN06"
 }
};

// ------------------------------------------------------------------------------ utilidades
// Compatible con JScript (motor por defecto de EA): sin Object.keys, Array.indexOf, JSON, etc.

function contiene(lista, valor) {
    for (var i = 0; i < lista.length; i++) if (lista[i] == valor) return true;
    return false;
}

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

function ponerEnDiagrama(diagrama, elemento, x, y, ancho, alto) {
    var pos = "l=" + x + ";r=" + (x + ancho) + ";t=" + y + ";b=" + (y + alto) + ";";
    var o = diagrama.DiagramObjects.AddNew(pos, "");
    o.ElementID = elemento.ElementID;
    o.Update();
    return o;
}

function largoMaximo(clase) {
    var max = clase.nombre.length + 4, i, t;
    for (i = 0; i < clase.attrs.length; i++) {
        t = clase.attrs[i].n.length + clase.attrs[i].t.length + 4;
        if (t > max) max = t;
    }
    for (i = 0; i < clase.metodos.length; i++) {
        t = textoMetodo(clase.metodos[i]).length + 2;
        if (t > max) max = t;
    }
    return max;
}

function textoMetodo(m) {
    var ps = [], i;
    for (i = 0; i < m.p.length; i++) ps.push(m.p[i][0] + ": " + m.p[i][1]);
    return m.n + "(" + ps.join(", ") + ")" + (m.t ? ": " + m.t : "");
}

function anchoClase(c) { return Math.max(170, Math.min(640, largoMaximo(c) * 6 + 24)); }
function altoClase(c)  { return 44 + 14 * (c.attrs.length + c.metodos.length) + (c.attrs.length ? 8 : 0) + (c.metodos.length ? 8 : 0); }

// ------------------------------------------------------------------------------ clases

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

function capaDe(nombre) {
    for (var i = 0; i < MODELO.clases.length; i++) if (MODELO.clases[i].nombre == nombre) return MODELO.clases[i];
    return null;
}

// Diagrama de clases acomodado por capas (UI / BLL+Servicios / DAL / entidades en niveles /
// enumeraciones). El orden dentro de cada fila se calculó para minimizar cruces de flechas.
// Las dependencias «use» hacia entidades BE siguen en el modelo, pero se ocultan en el dibujo
// (son las que cruzaban todo el diagrama).
function diagramaClases(paquete, caso) {
    var d = paquete.Diagrams.AddNew("Clases " + caso.id + " - " + caso.titulo, "Logical");
    d.Update();
    var i, p;
    for (i = 0; i < caso.layoutClases.length; i++) {
        p = caso.layoutClases[i];
        if (elementosClase[p.n]) ponerEnDiagrama(d, elementosClase[p.n], p.l, p.t, p.w, p.h);
    }
    d.DiagramObjects.Refresh();
    for (i = 0; i < caso.ocultar.length; i++) {
        var id = conectores[caso.ocultar[i][0] + "|" + caso.ocultar[i][1] + "|" + caso.ocultar[i][2]];
        if (!id) continue;
        try {
            var dl = d.DiagramLinks.AddNew("", "");
            dl.ConnectorID = id;
            dl.IsHidden = true;
            dl.Update();
        } catch (e) { }
    }
    try { d.DiagramLinks.Refresh(); } catch (e2) { }
    d.Update();
    return d;
}

// ------------------------------------------------------------------------------ tablas (DER)

var elementosTabla = {};

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
            hija.Connectors.Refresh();
        }
    }
    var cantidad = 0;
    for (nombre in MODELO.tablas) cantidad++;
    log("  Tablas creadas: " + cantidad);
}

function diagramaDER(paquete, caso) {
    var d = paquete.Diagrams.AddNew("DER " + caso.id + " - " + caso.titulo, "Logical");
    d.Update();
    var x = 20, y = 20, altoFila = 0, porFila = 4, n = 0;
    for (var i = 0; i < caso.tablas.length; i++) {
        var t = MODELO.tablas[caso.tablas[i]];
        if (!t) continue;
        var max = caso.tablas[i].length;
        for (var k = 0; k < t.cols.length; k++) max = Math.max(max, t.cols[k].n.length + t.cols[k].t.length + 10);
        var w = Math.max(190, max * 6 + 30), h = 50 + 14 * (t.cols.length + t.fk.length + 1);
        ponerEnDiagrama(d, elementosTabla[caso.tablas[i]], x, y, w, h);
        x += w + 70;
        if (h > altoFila) altoFila = h;
        n++;
        if (n % porFila == 0) { x = 20; y += altoFila + 70; altoFila = 0; }
    }
    d.DiagramObjects.Refresh();
    return d;
}

// ------------------------------------------------------------------------------ secuencia

var actores = {};
var casosUso = {};   // "CUN02" -> elemento UseCase (la "pelota" que se ubica en las secuencias)

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

function diagramaSecuencia(paquete, caso) {
    var d = paquete.Diagrams.AddNew("Secuencia " + caso.id + " - " + caso.titulo, "Sequence");
    d.Update();

    var lineas = {}, x = 30, i;
    for (i = 0; i < caso.lifelines.length; i++) {
        var id = caso.lifelines[i], def = LIFELINES[id], el;
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

    d.DiagramObjects.Refresh();
    try { Repository.ReloadDiagram(d.DiagramID); } catch (e6) { }
    return d;
}

// ------------------------------------------------------------------------------ principal

function main() {
    Repository.EnsureOutputVisible("Script");
    Repository.ClearOutput("Script");

    var destino = Repository.GetTreeSelectedPackage();
    if (destino == null) {
        Session.Prompt("Seleccioná un paquete en el Project Browser antes de ejecutar el script.", 1);  // 1 = promptOK
        return;
    }

    // Si ya se había generado, se borra para no duplicar
    for (var i = destino.Packages.Count - 1; i >= 0; i--) {
        var nombreViejo = destino.Packages.GetAt(i).Name;
        if (nombreViejo == RAIZ || nombreViejo == "FLY SAFE - CUN01 a CUN05") {
            destino.Packages.DeleteAt(i, false);
            log("Se borró la versión anterior '" + nombreViejo + "'.");
        }
    }
    destino.Packages.Refresh();

    log("Generando '" + RAIZ + "' en '" + destino.Name + "' ...");
    var raiz = nuevoPaquete(destino, RAIZ);

    var pAct = nuevoPaquete(raiz, "Actores");
    actores["Vendedor"] = nuevoElemento(pAct, "Vendedor", "Actor", null);

    var pCU = nuevoPaquete(raiz, "Casos de uso");
    for (var u = 0; u < CASOS.length; u++)
        casosUso[CASOS[u].id] = nuevoElemento(pCU, CASOS[u].id + " " + CASOS[u].titulo, "UseCase", null);

    crearClases(nuevoPaquete(raiz, "Modelo de clases"));
    crearTablas(nuevoPaquete(raiz, "Modelo de datos"));

    for (var c = 0; c < CASOS.length; c++) {
        var caso = CASOS[c];
        var p = nuevoPaquete(raiz, caso.id + " " + caso.titulo);
        var dS = diagramaSecuencia(p, caso);
        var dC = diagramaClases(p, caso);
        var dD = diagramaDER(p, caso);
        log("  " + caso.id + " " + caso.titulo + ": secuencia (" + caso.mensajes.length + " mensajes), clases (" +
            caso.clases.length + "), DER (" + caso.tablas.length + " tablas)");
    }

    Repository.RefreshModelView(destino.PackageID);
    log("Listo. Abrí cada diagrama desde el Project Browser (paquete '" + RAIZ + "').");
}

main();
