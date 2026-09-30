/*
 * FLY SAFE - Diagramas de CUN01 a CUN05 para Enterprise Architect
 * ------------------------------------------------------------------------------------
 * Genera, dentro del paquete seleccionado en el Project Browser:
 *   FLY SAFE - CUN01 a CUN05
 *     Modelo de clases  (UI / BLL / DAL / BE / Servicios)  -> clases con los atributos y
 *                        métodos reales del proyecto Proyecto-Diploma (C#)
 *     Modelo de datos   -> tablas de "Gestion Usuario" (EsquemaCompleto.sql) con PK y FK
 *     Actores           -> Vendedor
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
 * Si lo volvés a ejecutar, borra el paquete "FLY SAFE - CUN01 a CUN05" anterior y lo regenera.
 *
 * Generado a partir del código del proyecto (30/09/2026).
 */

var RAIZ = "FLY SAFE - CUN01 a CUN05";

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
   "d": "BLLReserva_GV42",
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
   "d": "BLLReserva_GV42",
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
    "frm",
    "«extend» CUN02 Registrar cliente",
    "",
    896
   ],
   [
    "frm",
    "v",
    "datos del cliente (nombre, apellido, email, teléfono)",
    "r",
    958
   ],
   [
    "v",
    "frm",
    "ingresarPasajero(dni, nombre, apellido, email, telefono)",
    "",
    1052
   ],
   [
    "v",
    "frm",
    "btnSiguiente_Click()",
    "",
    1112
   ],
   [
    "frm",
    "frm",
    "ValidarYAvanzarPasajeros()",
    "",
    1144
   ],
   [
    "frm",
    "bll",
    "ValidarPasajerosParaReserva(pasajeros : List<Pasajero_GV42>)",
    "",
    1176
   ],
   [
    "bll",
    "util",
    "ValidarPersona(p : Persona_GV42, rol : string)",
    "",
    1242
   ],
   [
    "bll",
    "bll",
    "VerificarIdentidad(p, rol, mostrarRegistrado)",
    "",
    1274
   ],
   [
    "bll",
    "dPa",
    "BuscarPorDni(p.DNI)",
    "",
    1306
   ],
   [
    "dPa",
    "acc",
    "leer(query, parametros)",
    "",
    1338
   ],
   [
    "acc",
    "dPa",
    "DataTable",
    "r",
    1370
   ],
   [
    "dPa",
    "bll",
    "Pasajero_GV42",
    "r",
    1402
   ],
   [
    "bll",
    "frm",
    "NegocioException_GV42(mensaje)",
    "r",
    1496
   ],
   [
    "frm",
    "v",
    "mensaje de error",
    "r",
    1528
   ],
   [
    "bll",
    "frm",
    "void",
    "r",
    1590
   ],
   [
    "frm",
    "bll",
    "ObtenerMapaAsientos(idVuelo : int, clase : ClaseVuelo_GV42)",
    "",
    1650
   ],
   [
    "bll",
    "dAs",
    "ListarMapa(idVuelo, clase)",
    "",
    1682
   ],
   [
    "dAs",
    "acc",
    "leer(query, parametros)",
    "",
    1714
   ],
   [
    "acc",
    "dAs",
    "DataTable",
    "r",
    1746
   ],
   [
    "dAs",
    "bll",
    "List<AsientoDisponibilidad_GV42>",
    "r",
    1778
   ],
   [
    "bll",
    "frm",
    "List<AsientoDisponibilidad_GV42>",
    "r",
    1810
   ],
   [
    "frm",
    "v",
    "mapa de butacas (libres / ocupadas)",
    "r",
    1842
   ],
   [
    "v",
    "frm",
    "seleccionarButaca(asiento) / ctrlButacas_AsientoClickeado()",
    "",
    1908
   ],
   [
    "frm",
    "frm",
    "«extend» CUN03 Registrar adicionales",
    "",
    2002
   ],
   [
    "v",
    "frm",
    "confirmarReserva() / btnSiguiente_Click()",
    "",
    2062
   ],
   [
    "frm",
    "frm",
    "ConfirmarReserva()",
    "",
    2094
   ],
   [
    "frm",
    "bll",
    "GenerarReserva(borrador : Reserva_GV42)",
    "",
    2126
   ],
   [
    "bll",
    "util",
    "LoginActual()",
    "",
    2158
   ],
   [
    "util",
    "bll",
    "login : string",
    "r",
    2190
   ],
   [
    "bll",
    "bll",
    "ValidarPasajerosParaReserva(pasajeros)",
    "",
    2222
   ],
   [
    "bll",
    "bll",
    "ValidarAdicionales(adicionales, canal)",
    "",
    2254
   ],
   [
    "bll",
    "dVu",
    "BuscarVueloClase(idVuelo : int, clase : ClaseVuelo_GV42)",
    "",
    2286
   ],
   [
    "dVu",
    "acc",
    "leer(query, parametros)",
    "",
    2318
   ],
   [
    "acc",
    "dVu",
    "DataTable",
    "r",
    2350
   ],
   [
    "dVu",
    "bll",
    "VueloClase_GV42",
    "r",
    2382
   ],
   [
    "bll",
    "bll",
    "ValidarAsientos(borrador, vc)",
    "",
    2414
   ],
   [
    "bll",
    "frm",
    "NegocioException_GV42(\"Solo quedan N asiento(s) disponible(s)\")",
    "r",
    2480
   ],
   [
    "frm",
    "v",
    "mensaje de error",
    "r",
    2512
   ],
   [
    "bll",
    "bll",
    "calcular ImporteBase, SubtotalAdicionales, Impuestos, ImporteTotal",
    "",
    2574
   ],
   [
    "bll",
    "dRe",
    "Crear(r : Reserva_GV42)",
    "",
    2606
   ],
   [
    "dRe",
    "acc",
    "EjecutarEnTransaccion(trabajo)  [UPDATE VueloClase; INSERT Reserva, ReservaPasajero, ReservaAdicional]",
    "",
    2638
   ],
   [
    "acc",
    "dRe",
    "Reserva_GV42",
    "r",
    2670
   ],
   [
    "dRe",
    "bll",
    "Reserva_GV42 (Pendiente de Pago)",
    "r",
    2702
   ],
   [
    "bll",
    "bll",
    "RecalcularIntegridad(\"Reserva\")",
    "",
    2734
   ],
   [
    "bll",
    "bInt",
    "RecalcularTabla(\"Reserva\")",
    "",
    2766
   ],
   [
    "bInt",
    "dInt",
    "CalcularDVHsTabla(\"Reserva\")",
    "",
    2798
   ],
   [
    "dInt",
    "acc",
    "leer(query, null)",
    "",
    2830
   ],
   [
    "acc",
    "dInt",
    "DataTable",
    "r",
    2862
   ],
   [
    "dInt",
    "bInt",
    "dvhs : Dictionary<string, string>",
    "r",
    2894
   ],
   [
    "bInt",
    "dInt",
    "GuardarDVHs(nombreTabla, dvhs)",
    "",
    2926
   ],
   [
    "bInt",
    "calc",
    "CalcularDVV(dvhs)",
    "",
    2958
   ],
   [
    "calc",
    "bInt",
    "dvv : string",
    "r",
    2990
   ],
   [
    "bInt",
    "dInt",
    "GuardarDVV(nombreTabla, dvv)",
    "",
    3022
   ],
   [
    "dInt",
    "acc",
    "escribir(query, parametros)",
    "",
    3054
   ],
   [
    "acc",
    "dInt",
    "filas afectadas : int",
    "r",
    3086
   ],
   [
    "bll",
    "util",
    "Auditar(modulo, \"Reserva generada\", detalle, criticidad)",
    "",
    3118
   ],
   [
    "util",
    "bBit",
    "RegistrarEvento(login, modulo, tipoEvento, detalle, criticidad)",
    "",
    3150
   ],
   [
    "bBit",
    "dBit",
    "Guardar(registro : Bitacora_GV42)",
    "",
    3182
   ],
   [
    "dBit",
    "acc",
    "escribir(\"INSERT INTO EVENTOS ...\", parametros)",
    "",
    3214
   ],
   [
    "acc",
    "dBit",
    "filas afectadas : int",
    "r",
    3246
   ],
   [
    "bll",
    "frm",
    "Reserva_GV42",
    "r",
    3278
   ],
   [
    "frm",
    "frm",
    "MostrarResultado()",
    "",
    3310
   ],
   [
    "frm",
    "v",
    "N° de reserva, pasajeros, vuelo, clase, adicionales, importes, estado \"Pendiente de Pago\"",
    "r",
    3342
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
    "r": 372,
    "t": 862,
    "b": 996,
    "ops": [
     {
      "guarda": "[cliente == null]",
      "size": 66
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
    "t": 1018,
    "b": 1090,
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
    "l": 398,
    "r": 2652,
    "t": 1208,
    "b": 1440,
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
    "r": 562,
    "t": 1462,
    "b": 1628,
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
    "subtipo": 4,
    "nombre": "Por cada pasajero",
    "l": 18,
    "r": 372,
    "t": 1874,
    "b": 1946,
    "ops": [
     {
      "guarda": "[i < cantidadPasajeros]",
      "size": 72
     }
    ]
   },
   {
    "subtipo": 1,
    "nombre": "Adicionales",
    "l": 208,
    "r": 372,
    "t": 1968,
    "b": 2040,
    "ops": [
     {
      "guarda": "[el cliente solicita adicionales]",
      "size": 72
     }
    ]
   },
   {
    "subtipo": 0,
    "nombre": "Disponibilidad",
    "l": 18,
    "r": 2652,
    "t": 2446,
    "b": 3380,
    "ops": [
     {
      "guarda": "[AsientosDisponibles < CantidadPasajeros]",
      "size": 98
     },
     {
      "guarda": "[hay disponibilidad]",
      "size": 836
     }
    ]
   }
  ],
  "alto": 3452,
  "id": "CUN01",
  "titulo": "Reservar vuelo",
  "lifelines": [
   "v",
   "frm",
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
  "notas": [
   "Alternativa 3.1: si no hay vuelos se informa y se vuelve al paso 2.",
   "Alternativa 5.1: si el cliente no existe se ejecuta la extensión CUN02 Registrar cliente."
  ],
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
  ]
 },
 {
  "mensajes": [
   [
    "v",
    "frm",
    "ingresarDniCliente(dni) / btnBuscarCliente_Click()",
    "",
    130
   ],
   [
    "frm",
    "bll",
    "BuscarPasajero(dni : string)",
    "",
    162
   ],
   [
    "bll",
    "dPa",
    "BuscarPorDni(dni)",
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
    "DataTable (vacía)",
    "r",
    258
   ],
   [
    "dPa",
    "bll",
    "null",
    "r",
    290
   ],
   [
    "bll",
    "frm",
    "null",
    "r",
    322
   ],
   [
    "frm",
    "bll",
    "PrecargarDesdeUsuario(dni : string)",
    "",
    354
   ],
   [
    "bll",
    "dPa",
    "BuscarDatosEnUsuario(dni)",
    "",
    386
   ],
   [
    "dPa",
    "acc",
    "leer(query, parametros)",
    "",
    418
   ],
   [
    "acc",
    "dPa",
    "DataTable (vacía)",
    "r",
    450
   ],
   [
    "dPa",
    "bll",
    "null",
    "r",
    482
   ],
   [
    "bll",
    "frm",
    "null",
    "r",
    514
   ],
   [
    "frm",
    "v",
    "\"No existe una persona registrada con ese DNI\" + formulario de alta",
    "r",
    546
   ],
   [
    "v",
    "frm",
    "ingresarDatosCliente(nombre, apellido, dni, email, telefono)",
    "",
    578
   ],
   [
    "v",
    "frm",
    "btnRegistrarCliente_Click()",
    "",
    610
   ],
   [
    "frm",
    "bll",
    "RegistrarPasajero(pasajero : Pasajero_GV42)",
    "",
    642
   ],
   [
    "bll",
    "util",
    "ValidarPersona(pasajero, \"Cliente\")",
    "",
    674
   ],
   [
    "bll",
    "dPa",
    "ExisteDni(dni)",
    "",
    706
   ],
   [
    "dPa",
    "acc",
    "leerEscalar(query, parametros)",
    "",
    738
   ],
   [
    "acc",
    "dPa",
    "cantidad : object",
    "r",
    770
   ],
   [
    "dPa",
    "bll",
    "existe : bool",
    "r",
    802
   ],
   [
    "bll",
    "frm",
    "NegocioException_GV42(mensaje)",
    "r",
    868
   ],
   [
    "frm",
    "v",
    "mensaje de error",
    "r",
    900
   ],
   [
    "bll",
    "bll",
    "VerificarIdentidad(pasajero, \"Cliente\", true)",
    "",
    962
   ],
   [
    "bll",
    "dPa",
    "Insertar(p : Pasajero_GV42)",
    "",
    994
   ],
   [
    "dPa",
    "acc",
    "escribir(\"INSERT INTO Pasajero ...\", parametros)",
    "",
    1026
   ],
   [
    "acc",
    "dPa",
    "filas afectadas : int",
    "r",
    1058
   ],
   [
    "dPa",
    "bll",
    "void",
    "r",
    1090
   ],
   [
    "bll",
    "util",
    "Auditar(modulo, \"Cliente registrado\", detalle, criticidad)",
    "",
    1122
   ],
   [
    "util",
    "bBit",
    "RegistrarEvento(login, modulo, tipoEvento, detalle, criticidad)",
    "",
    1154
   ],
   [
    "bBit",
    "dBit",
    "Guardar(registro : Bitacora_GV42)",
    "",
    1186
   ],
   [
    "dBit",
    "acc",
    "escribir(\"INSERT INTO EVENTOS ...\", parametros)",
    "",
    1218
   ],
   [
    "acc",
    "dBit",
    "filas afectadas : int",
    "r",
    1250
   ],
   [
    "bll",
    "frm",
    "void",
    "r",
    1282
   ],
   [
    "frm",
    "v",
    "\"Cliente registrado\" (retorna a CUN01)",
    "r",
    1314
   ]
  ],
  "fragmentos": [
   {
    "subtipo": 0,
    "nombre": "Validación del cliente",
    "l": 18,
    "r": 1512,
    "t": 834,
    "b": 1352,
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
  "alto": 1424,
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
  "notas": [
   "Validaciones: DNI de 7-8 dígitos, nombre y apellido, email y teléfono (Validaciones_GV42); el DNI no puede estar registrado a nombre de otra persona."
  ],
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
  ]
 },
 {
  "mensajes": [
   [
    "v",
    "frm",
    "avanzarAAdicionales() / btnSiguiente_Click()",
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
    "v",
    "frm",
    "confirmarReserva() / btnSiguiente_Click()  (retorno a CUN01)",
    "",
    640
   ],
   [
    "frm",
    "bll",
    "GenerarReserva(borrador : Reserva_GV42)",
    "",
    672
   ],
   [
    "bll",
    "bll",
    "ValidarAdicionales(adicionales, canal)",
    "",
    704
   ],
   [
    "bll",
    "dTA",
    "ListarActivos()",
    "",
    736
   ],
   [
    "dTA",
    "acc",
    "leer(query, null)",
    "",
    768
   ],
   [
    "acc",
    "dTA",
    "DataTable",
    "r",
    800
   ],
   [
    "dTA",
    "bll",
    "List<TipoAdicional_GV42>",
    "r",
    832
   ],
   [
    "bll",
    "frm",
    "NegocioException_GV42(mensaje)",
    "r",
    898
   ],
   [
    "frm",
    "v",
    "mensaje de error",
    "r",
    930
   ],
   [
    "bll",
    "dRe",
    "Crear(r : Reserva_GV42)",
    "",
    992
   ],
   [
    "dRe",
    "acc",
    "escribir(tx, \"INSERT INTO ReservaAdicional ...\", parametros)",
    "",
    1024
   ],
   [
    "acc",
    "dRe",
    "filas afectadas : int",
    "r",
    1056
   ],
   [
    "dRe",
    "bll",
    "Reserva_GV42",
    "r",
    1088
   ],
   [
    "bll",
    "frm",
    "Reserva_GV42",
    "r",
    1120
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
   },
   {
    "subtipo": 0,
    "nombre": "Validación de adicionales",
    "l": 18,
    "r": 1132,
    "t": 864,
    "b": 1158,
    "ops": [
     {
      "guarda": "[cantidad fuera de 1..20 o costo <= 0]",
      "size": 98
     },
     {
      "guarda": "[adicionales válidos]",
      "size": 196
     }
    ]
   }
  ],
  "alto": 1230,
  "id": "CUN03",
  "titulo": "Registrar adicionales",
  "lifelines": [
   "v",
   "frm",
   "bll",
   "dTA",
   "dRe",
   "acc"
  ],
  "notas": [
   "En autogestión el costo se toma del catálogo (TipoAdicional.PrecioUnitario); el vendedor puede ajustarlo."
  ],
  "clases": [
   "FRMReservarVuelo_GV42",
   "BLLReserva_GV42",
   "DALTipoAdicional_GV42",
   "DALReserva_GV42",
   "Acceso",
   "Reserva_GV42",
   "AdicionalReserva_GV42",
   "TipoAdicional_GV42"
  ],
  "tablas": [
   "Reserva",
   "ReservaAdicional",
   "TipoAdicional"
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
    "EjecutarEnTransaccion(trabajo)  [UPDATE Reserva -> Confirmada; INSERT Pago]",
    "",
    1112
   ],
   [
    "dPg",
    "dPg",
    "«include» CUN05 Generar boletos",
    "",
    1144
   ],
   [
    "acc",
    "dPg",
    "Pago_GV42",
    "r",
    1176
   ],
   [
    "dPg",
    "bll",
    "Pago_GV42",
    "r",
    1208
   ],
   [
    "bll",
    "bll",
    "RecalcularIntegridad(\"Pago\")",
    "",
    1240
   ],
   [
    "bll",
    "bInt",
    "RecalcularTabla(\"Pago\")",
    "",
    1272
   ],
   [
    "bInt",
    "dInt",
    "CalcularDVHsTabla(\"Pago\")",
    "",
    1304
   ],
   [
    "dInt",
    "acc",
    "leer(query, null)",
    "",
    1336
   ],
   [
    "acc",
    "dInt",
    "DataTable",
    "r",
    1368
   ],
   [
    "dInt",
    "bInt",
    "dvhs : Dictionary<string, string>",
    "r",
    1400
   ],
   [
    "bInt",
    "dInt",
    "GuardarDVHs(nombreTabla, dvhs)",
    "",
    1432
   ],
   [
    "bInt",
    "calc",
    "CalcularDVV(dvhs)",
    "",
    1464
   ],
   [
    "calc",
    "bInt",
    "dvv : string",
    "r",
    1496
   ],
   [
    "bInt",
    "dInt",
    "GuardarDVV(nombreTabla, dvv)",
    "",
    1528
   ],
   [
    "dInt",
    "acc",
    "escribir(query, parametros)",
    "",
    1560
   ],
   [
    "acc",
    "dInt",
    "filas afectadas : int",
    "r",
    1592
   ],
   [
    "bll",
    "bll",
    "RecalcularIntegridad(\"Reserva\")",
    "",
    1624
   ],
   [
    "bll",
    "bInt",
    "RecalcularTabla(\"Reserva\")",
    "",
    1656
   ],
   [
    "bll",
    "util",
    "Auditar(modulo, \"Pago registrado\", detalle, criticidad)",
    "",
    1688
   ],
   [
    "util",
    "bBit",
    "RegistrarEvento(login, modulo, tipoEvento, detalle, criticidad)",
    "",
    1720
   ],
   [
    "bBit",
    "dBit",
    "Guardar(registro : Bitacora_GV42)",
    "",
    1752
   ],
   [
    "dBit",
    "acc",
    "escribir(\"INSERT INTO EVENTOS ...\", parametros)",
    "",
    1784
   ],
   [
    "acc",
    "dBit",
    "filas afectadas : int",
    "r",
    1816
   ],
   [
    "bll",
    "frmP",
    "Pago_GV42",
    "r",
    1848
   ],
   [
    "frmP",
    "v",
    "\"Pago registrado y reserva confirmada\"",
    "r",
    1880
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
    "r": 2272,
    "t": 858,
    "b": 1918,
    "ops": [
     {
      "guarda": "[Estado != Pendiente de Pago o importe != ImporteTotal]",
      "size": 98
     },
     {
      "guarda": "[pago válido]",
      "size": 962
     }
    ]
   },
   {
    "subtipo": 1,
    "nombre": "Efectivo",
    "l": 404,
    "r": 556,
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
  "alto": 1990,
  "id": "CUN04",
  "titulo": "Registrar pago",
  "lifelines": [
   "v",
   "frmP",
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
  "notas": [
   "La transferencia / autorización bancaria es simulada: el vendedor ingresa el número de transacción."
  ],
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
  ]
 },
 {
  "mensajes": [
   [
    "v",
    "frmP",
    "confirmarPago() / btnConfirmarPago_Click()  (desde CUN04)",
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
  "notas": [
   "Se ejecuta automáticamente al confirmarse el pago («include» desde CUN04). NumeroBoleto y FechaEmision los genera la base."
  ],
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
        o.Connectors.Refresh();
    }
    log("  Clases creadas: " + MODELO.clases.length + " | relaciones: " + MODELO.relaciones.length);
}

function capaDe(nombre) {
    for (var i = 0; i < MODELO.clases.length; i++) if (MODELO.clases[i].nombre == nombre) return MODELO.clases[i];
    return null;
}

// Diagrama de clases acomodado por capas: UI arriba, luego BLL/Servicios, DAL y BE abajo.
function diagramaClases(paquete, caso) {
    var d = paquete.Diagrams.AddNew("Clases " + caso.id + " - " + caso.titulo, "Logical");
    d.Update();
    var filas = [["UI"], ["BLL", "Servicios"], ["DAL"], ["BE"]];
    var y = 20, MAX_ANCHO = 2200;
    for (var f = 0; f < filas.length; f++) {
        var x = 20, altoFila = 0, hubo = false;
        for (var i = 0; i < caso.clases.length; i++) {
            var c = capaDe(caso.clases[i]);
            if (!c || !contiene(filas[f], c.capa)) continue;
            var w = anchoClase(c), h = altoClase(c);
            if (x + w > MAX_ANCHO && x > 20) { y += altoFila + 50; x = 20; altoFila = 0; }
            ponerEnDiagrama(d, elementosClase[c.nombre], x, y, w, h);
            x += w + 40;
            if (h > altoFila) altoFila = h;
            hubo = true;
        }
        if (hubo) y += altoFila + 70;
    }
    d.DiagramObjects.Refresh();
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

    // Notas con aclaraciones del caso de uso
    var yNota = 50;
    for (i = 0; i < caso.notas.length; i++) {
        var nota = nuevoElemento(paquete, "", "Note", null);
        nota.Notes = caso.notas[i];
        nota.Update();
        ponerEnDiagrama(d, nota, x + 20, yNota, 280, 90);
        yNota += 110;
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
        if (destino.Packages.GetAt(i).Name == RAIZ) {
            destino.Packages.DeleteAt(i, false);
            log("Se borró la versión anterior de '" + RAIZ + "'.");
        }
    }
    destino.Packages.Refresh();

    log("Generando '" + RAIZ + "' en '" + destino.Name + "' ...");
    var raiz = nuevoPaquete(destino, RAIZ);

    var pAct = nuevoPaquete(raiz, "Actores");
    actores["Vendedor"] = nuevoElemento(pAct, "Vendedor", "Actor", null);

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
