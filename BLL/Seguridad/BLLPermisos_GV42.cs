using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{

    public class BLLPermisos_GV42
    {
        #region Constantes

        // ---- Reglas de protección -------------------------------------------------------------
        // Un "administrador" es quien puede gestionar roles y modificar usuarios: siempre tiene que
        // quedar al menos uno activo y no bloqueado, si no nadie más puede administrar el sistema.
        public const string PATENTE_ADMIN_ROLES = "Permisos.Roles";
        public const string PATENTE_ADMIN_USUARIOS = "Usuarios.Modificar";

        #endregion

        #region Campos

        private readonly DALPatente_GV42 _dalPatente;
        private readonly DALFamilia_GV42 _dalFamilia;
        private readonly DALRol_GV42 _dalRol;
        private readonly BLLIntegridad_GV42 _bllIntegridad;

        // Roles que el sistema usa por nombre (el autoregistro busca "Cliente"): no se renombran ni se borran.
        private static readonly string[] ROLES_DE_SISTEMA = { "Admin", "Cliente" };

        #endregion

        #region Constructor

        public BLLPermisos_GV42()
        {
            _dalPatente = new DALPatente_GV42();
            _dalFamilia = new DALFamilia_GV42();
            _dalRol = new DALRol_GV42();
            _bllIntegridad = new BLLIntegridad_GV42();
        }

        #endregion

        #region Reglas de protección

        public static bool EsRolDeSistema(string nombre) =>
            nombre != null && ROLES_DE_SISTEMA.Any(r => r.Equals(nombre.Trim(), StringComparison.OrdinalIgnoreCase));

        private static bool EsAdministrador(Func<string, bool> tiene) =>
            tiene(PATENTE_ADMIN_ROLES) && tiene(PATENTE_ADMIN_USUARIOS);

        // ¿Queda al menos un usuario activo, no bloqueado y con patentes de administrador?
        // Permite simular el cambio antes de hacerlo: excluir un usuario, cambiarle el rol a uno,
        // o reemplazar las patentes efectivas de un rol.
        public bool QuedaAlgunAdministrador(string dniExcluido = null, string dniConNuevoRol = null, int nuevoRolId = 0,
                                            int rolSimuladoId = 0, HashSet<string> clavesRolSimulado = null)
        {
            var cache = new Dictionary<int, bool>();
            foreach (Usuario_GV42 u in new DALUsuario_GV42().ListarTodos())
            {
                if (u == null || !u.Activo || u.Bloqueo || u.Rol == null) continue;
                if (dniExcluido != null && u.DNI == dniExcluido) continue;

                int idRol = (dniConNuevoRol != null && u.DNI == dniConNuevoRol) ? nuevoRolId : u.Rol.Id;
                bool esAdmin;
                if (!cache.TryGetValue(idRol, out esAdmin))
                {
                    if (clavesRolSimulado != null && idRol == rolSimuladoId)
                        esAdmin = EsAdministrador(clavesRolSimulado.Contains);
                    else
                    {
                        Rol_GV42 arbol = _dalRol.ObtenerArbol(idRol);
                        esAdmin = arbol != null && EsAdministrador(arbol.TienePermiso);
                    }
                    cache[idRol] = esAdmin;
                }
                if (esAdmin) return true;
            }
            return false;
        }

        private HashSet<string> ClavesDePatentes(IEnumerable<int> idsPatentes)
        {
            var ids = new HashSet<int>(idsPatentes);
            return new HashSet<string>(_dalPatente.ListarTodas().Where(p => ids.Contains(p.Id)).Select(p => p.DataKey ?? ""));
        }

        #endregion

        #region Consultas

        public List<Patente_GV42> ListarPatentes() => _dalPatente.ListarTodas();
        public List<Familia_GV42> ListarFamilias() => _dalFamilia.ListarTodasPlanas();
        public List<Rol_GV42> ListarRoles() => _dalRol.ListarTodos();

        public Familia_GV42 ObtenerArbolFamilia(int idFamilia) => _dalFamilia.ObtenerArbol(idFamilia);
        public Rol_GV42 ObtenerArbolRol(int idRol) => _dalRol.ObtenerArbol(idRol);

        public bool TienePermiso(int idRol, string dataKey)
        {
            Rol_GV42 rol = _dalRol.ObtenerArbol(idRol);
            return rol != null && rol.TienePermiso(dataKey);
        }

        #endregion

        #region Familias

        public int CrearFamilia(string nombre, List<int> idsPatentes, List<int> idsSubfamilias)
        {
            BLLNegocioUtil_GV42.ExigirPatente("Permisos.Familias", IdiomaManager_GV42.T("neg.permisos.sinPermisoFamilias"));
            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception(IdiomaManager_GV42.T("err.nombreFamiliaObligatorio"));
            nombre = NormalizarNombre(nombre, Validaciones_GV42.MAX_NOMBRE_FAMILIA);

            if (_dalFamilia.ListarTodasPlanas().Any(f => MismoNombre(f.Nombre, nombre)))
                throw new Exception(string.Format(IdiomaManager_GV42.T("err.familiaNombreDuplicado"), nombre));

            idsPatentes = (idsPatentes ?? new List<int>()).Distinct().OrderBy(i => i).ToList();
            idsSubfamilias = (idsSubfamilias ?? new List<int>()).Distinct().OrderBy(i => i).ToList();

            if (idsPatentes.Count == 0 && idsSubfamilias.Count == 0)
                throw new Exception(IdiomaManager_GV42.T("err.familiaSinContenido"));

            ValidarRedundanciaPatentesYSubfamilias(idsPatentes, idsSubfamilias);

            HashSet<int> patentesEfectivasNueva = ConstruirPatentesEfectivas(idsPatentes, idsSubfamilias);
            Familia_GV42 equivalente = BuscarFamiliaConMismasPatentesEfectivas(patentesEfectivasNueva);
            if (equivalente != null)
                throw new Exception(string.Format(IdiomaManager_GV42.T("err.familiaComposicionDuplicada"), equivalente.Nombre));

            int idCreada = _dalFamilia.Crear(nombre, idsPatentes, idsSubfamilias);
            Auditar("Familia creada",
                    $"Nombre: '{nombre}', Patentes: {idsPatentes.Count}, Subfamilias: {idsSubfamilias.Count}",
                    "Media");
            RecalcularFamilias();
            return idCreada;
        }

        public void ModificarFamilia(int idFamilia, string nombre, List<int> idsPatentes, List<int> idsSubfamilias)
        {
            BLLNegocioUtil_GV42.ExigirPatente("Permisos.Familias", IdiomaManager_GV42.T("neg.permisos.sinPermisoFamilias"));
            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception(IdiomaManager_GV42.T("err.nombreFamiliaObligatorio"));
            nombre = NormalizarNombre(nombre, Validaciones_GV42.MAX_NOMBRE_FAMILIA);

            if (_dalFamilia.ListarTodasPlanas()
                .Any(f => f.Id != idFamilia && MismoNombre(f.Nombre, nombre)))
                throw new Exception(string.Format(IdiomaManager_GV42.T("err.familiaOtroNombreDuplicado"), nombre));

            idsPatentes = (idsPatentes ?? new List<int>()).Distinct().OrderBy(i => i).ToList();
            idsSubfamilias = (idsSubfamilias ?? new List<int>()).Distinct().OrderBy(i => i).ToList();

            if (idsPatentes.Count == 0 && idsSubfamilias.Count == 0)
                throw new Exception(IdiomaManager_GV42.T("err.familiaSinContenido"));

            if (idsSubfamilias.Contains(idFamilia))
                throw new Exception(IdiomaManager_GV42.T("err.familiaCiclo"));

            foreach (int idSub in idsSubfamilias)
            {
                Familia_GV42 arbolSub = _dalFamilia.ObtenerArbol(idSub);
                if (arbolSub == null) continue;
                if (ContieneFamiliaRec(arbolSub, idFamilia))
                    throw new Exception(string.Format(
                        IdiomaManager_GV42.T("err.familiaCicloTransitivo"), arbolSub.Nombre));
            }

            ValidarRedundanciaPatentesYSubfamilias(idsPatentes, idsSubfamilias);

            HashSet<int> patentesEfectivasNueva = ConstruirPatentesEfectivas(idsPatentes, idsSubfamilias);

            ValidarRedundanciaEnAscendentesFamilia(idFamilia, patentesEfectivasNueva);

            Familia_GV42 equivalente = BuscarFamiliaConMismasPatentesEfectivas(patentesEfectivasNueva, excluirId: idFamilia);
            if (equivalente != null)
                throw new Exception(string.Format(IdiomaManager_GV42.T("err.familiaOtraComposicionDuplicada"), equivalente.Nombre));


            // Composición anterior, para volver atrás si el cambio deja al sistema sin administradores
            // (la familia puede estar dentro del rol Admin, directa o indirectamente).
            Familia_GV42 anterior = _dalFamilia.ObtenerArbol(idFamilia);

            _dalFamilia.Modificar(idFamilia, nombre, idsPatentes, idsSubfamilias);

            if (anterior != null && !QuedaAlgunAdministrador())
            {
                _dalFamilia.Modificar(idFamilia, anterior.Nombre,
                    anterior.Hijos.OfType<Patente_GV42>().Select(h => h.Id).ToList(),
                    anterior.Hijos.OfType<Familia_GV42>().Select(h => h.Id).ToList());
                throw new Exception(IdiomaManager_GV42.T("neg.permisos.sinAdminFamilia", PATENTE_ADMIN_ROLES, PATENTE_ADMIN_USUARIOS));
            }
            Auditar("Familia modificada",
                    $"IdFamilia: {idFamilia}, Nombre: '{nombre}', Patentes: {idsPatentes.Count}, Subfamilias: {idsSubfamilias.Count}",
                    "Media");
            RecalcularFamilias();
        }

        public void EliminarFamilia(int idFamilia)
        {
            BLLNegocioUtil_GV42.ExigirPatente("Permisos.Familias", IdiomaManager_GV42.T("neg.permisos.sinPermisoFamilias"));
            List<string> rolesQueLaUsan = _dalFamilia.NombresRolesQueUsan(idFamilia);
            if (rolesQueLaUsan.Count > 0)
            {
                throw new Exception(string.Format(
                    IdiomaManager_GV42.T("err.familiaEnUsoRoles"),
                    rolesQueLaUsan.Count, string.Join(", ", rolesQueLaUsan)));
            }

            List<string> familiasQueLaContienen = _dalFamilia.NombresFamiliasQueLaContienen(idFamilia);
            if (familiasQueLaContienen.Count > 0)
            {
                throw new Exception(string.Format(
                    IdiomaManager_GV42.T("err.familiaEnUsoFamilias"),
                    familiasQueLaContienen.Count, string.Join(", ", familiasQueLaContienen)));
            }

            _dalFamilia.Eliminar(idFamilia);
            Auditar("Familia eliminada", $"IdFamilia: {idFamilia}", "Alta");
            RecalcularFamilias();
        }

        private void ValidarRedundanciaPatentesYSubfamilias(List<int> idsPatentes, List<int> idsSubfamilias)
        {
            // ── (a) Patente directa que ya viene por alguna subfamilia ──
            if (idsPatentes.Count > 0 && idsSubfamilias.Count > 0)
            {
                foreach (int idSub in idsSubfamilias)
                {
                    Familia_GV42 arbol = _dalFamilia.ObtenerArbol(idSub);
                    if (arbol == null) continue;

                    HashSet<int> patentesDeLaSub = new HashSet<int>(arbol.ObtenerPatentes().Select(p => p.Id));

                    foreach (int idPat in idsPatentes)
                    {
                        if (patentesDeLaSub.Contains(idPat))
                        {
                            Patente_GV42 pat = _dalPatente.BuscarPorId(idPat);
                            string nombrePat = pat != null ? pat.Nombre : $"Id {idPat}";
                            throw new Exception(string.Format(
                                IdiomaManager_GV42.T("err.redundanciaPatenteEnSub"), nombrePat, arbol.Nombre));
                        }
                    }
                }
            }

            // ── (b) Dos subfamilias hermanas comparten alguna patente ──
            if (idsSubfamilias.Count >= 2)
            {
                var efectivasPorSub = new Dictionary<int, HashSet<int>>();
                var nombresPorSub = new Dictionary<int, string>();
                foreach (int idSub in idsSubfamilias)
                {
                    Familia_GV42 arbol = _dalFamilia.ObtenerArbol(idSub);
                    if (arbol == null) continue;
                    efectivasPorSub[idSub] = new HashSet<int>(arbol.ObtenerPatentes().Select(p => p.Id));
                    nombresPorSub[idSub] = arbol.Nombre;
                }

                var subs = efectivasPorSub.Keys.ToList();
                for (int i = 0; i < subs.Count; i++)
                {
                    for (int j = i + 1; j < subs.Count; j++)
                    {
                        var solapadas = efectivasPorSub[subs[i]].Intersect(efectivasPorSub[subs[j]]).ToList();
                        if (solapadas.Any())
                        {
                            Patente_GV42 pat = _dalPatente.BuscarPorId(solapadas.First());
                            string nombrePat = pat != null ? pat.Nombre : $"Id {solapadas.First()}";
                            throw new Exception(string.Format(
                                IdiomaManager_GV42.T("err.redundanciaSubsCompartenPatente"),
                                nombrePat, nombresPorSub[subs[i]], nombresPorSub[subs[j]]));
                        }
                    }
                }
            }
        }

        private void ValidarRedundanciaEnAscendentesFamilia(int idFamiliaModificada, HashSet<int> nuevasEfectivas)
        {
            foreach (var famAncestro in _dalFamilia.ListarTodasPlanas())
            {
                if (famAncestro.Id == idFamiliaModificada) continue;

                Familia_GV42 arbolAncestro = _dalFamilia.ObtenerArbol(famAncestro.Id);
                if (arbolAncestro == null) continue;

                if (!ContieneFamiliaRec(arbolAncestro, idFamiliaModificada)) continue;

                // (a) Patentes directas del ancestro que se solapan con las nuevas efectivas.
                var patentesDirectas = _dalFamilia.IdsPatentesDirectas(famAncestro.Id);
                var solapDirectas = patentesDirectas.Intersect(nuevasEfectivas).ToList();
                if (solapDirectas.Any())
                {
                    Patente_GV42 pat = _dalPatente.BuscarPorId(solapDirectas.First());
                    string nombrePat = pat != null ? pat.Nombre : $"Id {solapDirectas.First()}";
                    throw new Exception(string.Format(
                        IdiomaManager_GV42.T("err.redundanciaAscendenteDirecta"), nombrePat, famAncestro.Nombre));
                }

                // (b) Patentes que aporta otra subfamilia hermana del ancestro, solapadas con las nuevas efectivas.
                var subsDelAncestro = _dalFamilia.IdsSubfamiliasDirectas(famAncestro.Id);
                foreach (int idHermana in subsDelAncestro)
                {
                    if (idHermana == idFamiliaModificada) continue;
                    Familia_GV42 arbolHermana = _dalFamilia.ObtenerArbol(idHermana);
                    if (arbolHermana == null) continue;
                    if (ContieneFamiliaRec(arbolHermana, idFamiliaModificada)) continue;

                    HashSet<int> efectivasHermana = new HashSet<int>(arbolHermana.ObtenerPatentes().Select(p => p.Id));
                    var solapHermana = efectivasHermana.Intersect(nuevasEfectivas).ToList();
                    if (solapHermana.Any())
                    {
                        Patente_GV42 pat = _dalPatente.BuscarPorId(solapHermana.First());
                        string nombrePat = pat != null ? pat.Nombre : $"Id {solapHermana.First()}";
                        throw new Exception(string.Format(
                            IdiomaManager_GV42.T("err.redundanciaAscendenteHermana"),
                            nombrePat, famAncestro.Nombre, arbolHermana.Nombre));
                    }
                }
            }

            // ── Mismas dos validaciones pero para roles que contengan la familia ──
            foreach (var rol in _dalRol.ListarTodos())
            {
                Rol_GV42 arbolRol = _dalRol.ObtenerArbol(rol.Id);
                if (arbolRol == null) continue;
                if (!RolContieneFamiliaRec(arbolRol, idFamiliaModificada)) continue;

                var patentesDirectasRol = _dalRol.IdsPatentesDirectas(rol.Id);
                var solapDirectas = patentesDirectasRol.Intersect(nuevasEfectivas).ToList();
                if (solapDirectas.Any())
                {
                    Patente_GV42 pat = _dalPatente.BuscarPorId(solapDirectas.First());
                    string nombrePat = pat != null ? pat.Nombre : $"Id {solapDirectas.First()}";
                    throw new Exception(string.Format(
                        IdiomaManager_GV42.T("err.redundanciaAscendenteRolDirecta"), nombrePat, rol.Nombre));
                }

                var familiasDelRol = _dalRol.IdsFamiliasDirectas(rol.Id);
                foreach (int idFamHermana in familiasDelRol)
                {
                    if (idFamHermana == idFamiliaModificada) continue;
                    Familia_GV42 arbolHermana = _dalFamilia.ObtenerArbol(idFamHermana);
                    if (arbolHermana == null) continue;
                    if (ContieneFamiliaRec(arbolHermana, idFamiliaModificada)) continue;

                    HashSet<int> efectivasHermana = new HashSet<int>(arbolHermana.ObtenerPatentes().Select(p => p.Id));
                    var solapHermana = efectivasHermana.Intersect(nuevasEfectivas).ToList();
                    if (solapHermana.Any())
                    {
                        Patente_GV42 pat = _dalPatente.BuscarPorId(solapHermana.First());
                        string nombrePat = pat != null ? pat.Nombre : $"Id {solapHermana.First()}";
                        throw new Exception(string.Format(
                            IdiomaManager_GV42.T("err.redundanciaAscendenteRolFamilia"),
                            nombrePat, rol.Nombre, arbolHermana.Nombre));
                    }
                }
            }
        }

        private Familia_GV42 BuscarFamiliaConMismasPatentesEfectivas(HashSet<int> patentesEfectivas, int excluirId = 0)
        {
            foreach (var fam in _dalFamilia.ListarTodasPlanas())
            {
                if (fam.Id == excluirId) continue;
                Familia_GV42 arbol = _dalFamilia.ObtenerArbol(fam.Id);
                if (arbol == null) continue;
                HashSet<int> efectivas = new HashSet<int>(arbol.ObtenerPatentes().Select(p => p.Id));
                if (efectivas.SetEquals(patentesEfectivas))
                    return fam;
            }
            return null;
        }

        #endregion

        #region Roles

        public int CrearRol(string nombre, List<int> idsPatentes, List<int> idsFamilias)
        {
            BLLNegocioUtil_GV42.ExigirPatente(PATENTE_ADMIN_ROLES, IdiomaManager_GV42.T("neg.permisos.sinPermisoRoles"));
            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception(IdiomaManager_GV42.T("err.nombreRolObligatorio"));
            nombre = NormalizarNombre(nombre, Validaciones_GV42.MAX_NOMBRE_ROL);

            if (_dalRol.ListarTodos().Any(r => MismoNombre(r.Nombre, nombre)))
                throw new Exception(string.Format(IdiomaManager_GV42.T("err.rolNombreDuplicado"), nombre));

            idsPatentes = (idsPatentes ?? new List<int>()).Distinct().ToList();
            idsFamilias = (idsFamilias ?? new List<int>()).Distinct().ToList();

            if (idsPatentes.Count == 0 && idsFamilias.Count == 0)
                throw new Exception(IdiomaManager_GV42.T("err.rolSinContenido"));

            ValidarRedundanciaPatentesYFamilias(idsPatentes, idsFamilias);

            HashSet<int> efectivasNuevo = ConstruirPatentesEfectivas(idsPatentes, idsFamilias);
            Rol_GV42 equivalente = BuscarRolConMismasPatentesEfectivas(efectivasNuevo);
            if (equivalente != null)
                throw new Exception(string.Format(IdiomaManager_GV42.T("err.rolComposicionDuplicada"), equivalente.Nombre));

            int idCreado = _dalRol.Crear(nombre, idsPatentes, idsFamilias);
            Auditar("Rol creado",
                    $"Nombre: '{nombre}', Patentes: {idsPatentes.Count}, Familias: {idsFamilias.Count}",
                    "Media");
            RecalcularRoles();
            return idCreado;
        }

        public void ModificarRol(int idRol, string nombre, List<int> idsPatentes, List<int> idsFamilias)
        {
            BLLNegocioUtil_GV42.ExigirPatente(PATENTE_ADMIN_ROLES, IdiomaManager_GV42.T("neg.permisos.sinPermisoRoles"));
            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception(IdiomaManager_GV42.T("err.nombreRolObligatorio"));
            nombre = NormalizarNombre(nombre, Validaciones_GV42.MAX_NOMBRE_ROL);

            Rol_GV42 actual = _dalRol.ListarTodos().FirstOrDefault(r => r.Id == idRol);
            if (actual == null)
                throw new Exception(IdiomaManager_GV42.T("neg.permisos.rolNoExiste"));
            if (EsRolDeSistema(actual.Nombre) && !MismoNombre(actual.Nombre, nombre))
                throw new Exception(IdiomaManager_GV42.T("neg.permisos.rolSistemaRenombrar", actual.Nombre));

            if (_dalRol.ListarTodos()
                .Any(r => r.Id != idRol && MismoNombre(r.Nombre, nombre)))
                throw new Exception(string.Format(IdiomaManager_GV42.T("err.rolOtroNombreDuplicado"), nombre));

            idsPatentes = (idsPatentes ?? new List<int>()).Distinct().ToList();
            idsFamilias = (idsFamilias ?? new List<int>()).Distinct().ToList();

            if (idsPatentes.Count == 0 && idsFamilias.Count == 0)
                throw new Exception(IdiomaManager_GV42.T("err.rolSinContenido"));

            ValidarRedundanciaPatentesYFamilias(idsPatentes, idsFamilias);

            HashSet<int> efectivasNuevo = ConstruirPatentesEfectivas(idsPatentes, idsFamilias);
            Rol_GV42 equivalente = BuscarRolConMismasPatentesEfectivas(efectivasNuevo, excluirId: idRol);
            if (equivalente != null)
                throw new Exception(string.Format(IdiomaManager_GV42.T("err.rolOtraComposicionDuplicada"), equivalente.Nombre));

            if (!QuedaAlgunAdministrador(rolSimuladoId: idRol, clavesRolSimulado: ClavesDePatentes(efectivasNuevo)))
                throw new Exception(IdiomaManager_GV42.T("neg.permisos.sinAdminRol", PATENTE_ADMIN_ROLES, PATENTE_ADMIN_USUARIOS));

            _dalRol.Modificar(idRol, nombre, idsPatentes, idsFamilias);
            Auditar("Rol modificado",
                    $"IdRol: {idRol}, Nombre: '{nombre}', Patentes: {idsPatentes.Count}, Familias: {idsFamilias.Count}",
                    "Media");
            RecalcularRoles();
        }

        public void EliminarRol(int idRol)
        {
            BLLNegocioUtil_GV42.ExigirPatente(PATENTE_ADMIN_ROLES, IdiomaManager_GV42.T("neg.permisos.sinPermisoRoles"));
            Rol_GV42 existente = _dalRol.ListarTodos().FirstOrDefault(r => r.Id == idRol);
            if (existente == null)
                throw new Exception(IdiomaManager_GV42.T("neg.permisos.rolNoExiste"));
            if (EsRolDeSistema(existente.Nombre))
                throw new Exception(IdiomaManager_GV42.T("neg.permisos.rolSistemaEliminar", existente.Nombre));

            int cantUsuarios = _dalRol.CantidadUsuariosConRol(idRol);
            if (cantUsuarios > 0)
            {
                throw new Exception(string.Format(IdiomaManager_GV42.T("err.rolEnUso"), cantUsuarios));
            }

            _dalRol.Eliminar(idRol);
            Auditar("Rol eliminado", $"IdRol: {idRol}", "Alta");
            RecalcularRoles();
        }

        private void ValidarRedundanciaPatentesYFamilias(List<int> idsPatentes, List<int> idsFamilias)
        {
            // ── (a) Patente directa que ya viene por alguna familia ──
            if (idsPatentes.Count > 0 && idsFamilias.Count > 0)
            {
                foreach (int idFam in idsFamilias)
                {
                    Familia_GV42 arbol = _dalFamilia.ObtenerArbol(idFam);
                    if (arbol == null) continue;

                    HashSet<int> patentesDeLaFam = new HashSet<int>(arbol.ObtenerPatentes().Select(p => p.Id));

                    foreach (int idPat in idsPatentes)
                    {
                        if (patentesDeLaFam.Contains(idPat))
                        {
                            Patente_GV42 pat = _dalPatente.BuscarPorId(idPat);
                            string nombrePat = pat != null ? pat.Nombre : $"Id {idPat}";
                            throw new Exception(string.Format(
                                IdiomaManager_GV42.T("err.redundanciaPatenteEnFam"), nombrePat, arbol.Nombre));
                        }
                    }
                }
            }

            // ── (b) Dos familias hermanas comparten alguna patente ──
            if (idsFamilias.Count >= 2)
            {
                var efectivasPorFam = new Dictionary<int, HashSet<int>>();
                var nombresPorFam = new Dictionary<int, string>();
                foreach (int idFam in idsFamilias)
                {
                    Familia_GV42 arbol = _dalFamilia.ObtenerArbol(idFam);
                    if (arbol == null) continue;
                    efectivasPorFam[idFam] = new HashSet<int>(arbol.ObtenerPatentes().Select(p => p.Id));
                    nombresPorFam[idFam] = arbol.Nombre;
                }

                var fams = efectivasPorFam.Keys.ToList();
                for (int i = 0; i < fams.Count; i++)
                {
                    for (int j = i + 1; j < fams.Count; j++)
                    {
                        var solapadas = efectivasPorFam[fams[i]].Intersect(efectivasPorFam[fams[j]]).ToList();
                        if (solapadas.Any())
                        {
                            Patente_GV42 pat = _dalPatente.BuscarPorId(solapadas.First());
                            string nombrePat = pat != null ? pat.Nombre : $"Id {solapadas.First()}";
                            throw new Exception(string.Format(
                                IdiomaManager_GV42.T("err.redundanciaFamsCompartenPatente"),
                                nombrePat, nombresPorFam[fams[i]], nombresPorFam[fams[j]]));
                        }
                    }
                }
            }
        }

        private Rol_GV42 BuscarRolConMismasPatentesEfectivas(HashSet<int> patentesEfectivas, int excluirId = 0)
        {
            foreach (var rol in _dalRol.ListarTodos())
            {
                if (rol.Id == excluirId) continue;
                Rol_GV42 arbol = _dalRol.ObtenerArbol(rol.Id);
                if (arbol == null) continue;
                HashSet<int> efectivas = new HashSet<int>(arbol.ObtenerPatentes().Select(p => p.Id));
                if (efectivas.SetEquals(patentesEfectivas))
                    return rol;
            }
            return null;
        }

        #endregion

        #region Métodos privados

        private void RecalcularFamilias()
        {
            if (BLLIntegridad_GV42.IntegridadConocidamenteRota) return;
            try
            {
                _bllIntegridad.RecalcularTabla("Familia");
                _bllIntegridad.RecalcularTabla("FamiliaPatente");
                _bllIntegridad.RecalcularTabla("FamiliaIntegrada");
            }
            catch { }
        }

        private void RecalcularRoles()
        {
            if (BLLIntegridad_GV42.IntegridadConocidamenteRota) return;
            try
            {
                _bllIntegridad.RecalcularTabla("Roles");
                _bllIntegridad.RecalcularTabla("RolPatente");
                _bllIntegridad.RecalcularTabla("RolFamilia");
            }
            catch { }
        }

        private void Auditar(string tipoEvento, string detalle, string criticidad)
        {
            try
            {
                var actual = SessionManager_GV42.Instancia.ObtenerUsuarioActual();
                string login = actual != null ? actual.Login : "SISTEMA";
                BLLBitacora_GV42.Instancia.RegistrarEvento(login, "Admin", tipoEvento, detalle, criticidad);
            }
            catch { }
        }

        private static string NormalizarNombre(string nombre, int maximo)
        {
            nombre = Validaciones_GV42.NormalizarEspacios(nombre);
            if (!Validaciones_GV42.EsNombrePerfilValido(nombre, maximo))
                throw new Exception(Validaciones_GV42.MENSAJE_NOMBRE_PERFIL + " " + IdiomaManager_GV42.T("neg.permisos.maximoCaracteres", maximo));
            return nombre;
        }

        private static bool MismoNombre(string a, string b) =>
            string.Equals(Validaciones_GV42.NormalizarEspacios(a), Validaciones_GV42.NormalizarEspacios(b), StringComparison.OrdinalIgnoreCase);

        private HashSet<int> ConstruirPatentesEfectivas(List<int> idsPatentes, List<int> idsSubfamilias)
        {
            HashSet<int> efectivas = new HashSet<int>(idsPatentes);
            foreach (int idSub in idsSubfamilias)
            {
                Familia_GV42 arbol = _dalFamilia.ObtenerArbol(idSub);
                if (arbol == null) continue;
                foreach (var p in arbol.ObtenerPatentes())
                    efectivas.Add(p.Id);
            }
            return efectivas;
        }

        private bool ContieneFamiliaRec(Familia_GV42 nodo, int idBuscado)
        {
            if (nodo == null) return false;
            foreach (var hijo in nodo.Hijos)
            {
                if (hijo is Familia_GV42 sub)
                {
                    if (sub.Id == idBuscado) return true;
                    if (ContieneFamiliaRec(sub, idBuscado)) return true;
                }
            }
            return false;
        }

        private bool RolContieneFamiliaRec(Rol_GV42 rol, int idFamiliaBuscada)
        {
            if (rol == null) return false;
            foreach (var hijo in rol.Hijos)
            {
                if (hijo is Familia_GV42 fam)
                {
                    if (fam.Id == idFamiliaBuscada) return true;
                    if (ContieneFamiliaRec(fam, idFamiliaBuscada)) return true;
                }
            }
            return false;
        }

        #endregion
    }
}
