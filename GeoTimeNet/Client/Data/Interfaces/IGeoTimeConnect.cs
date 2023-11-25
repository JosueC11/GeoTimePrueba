using GeotimeNet.Client.Modelo.FromApi;
using GeotimeNet.Client.Modelo.FromApi.Response;


namespace GeotimeNet.Client.Data.Interfaces
{
    public interface IGeoTimeConnect
    {
        public event Action OnChange;

        /// <summary>
        /// setSchema: Metodo utilizado para cambiar de esquema de base de datos en la api.
        /// </summary>
        /// <param name="schema">Nombre del Schema de Base de Datos</param>
        public void setSchema(string schema);

        /// <summary>
        /// Post: Método para crear o actualizar registros en objetos de la base de datos a traves del método Post
        /// </summary>
        /// <returns>Objeto de la clase EventResponse </returns>
        /// <param name="apiName">Nombre del método de la Api a Ejecutar</param>
        /// <param name="model">Objeto de la clase a enviar en el cuerpo del mensaje</param>

        public Task<EventResponse> Post(string apiName, object model);

        /// <summary>
        /// Delete:  Metodo generico para boorado de datos de las tablas
        /// </summary>
        /// <param name="apiName"></param>
        /// <param name="id"></param>
        /// <returns>EventResponse</returns>
        public Task<EventResponse> Delete(string apiName, string id);


        /// <summary>
        /// GetEmpleado: Método para obtener una lista de empleados 
        /// </summary>
        /// <returns>Lista de cEmpleados</returns>
        public Task<IEnumerable<cEmpleado>> GetEmpleado();

        /// <summary>
        /// GetEmpleado: Método para un empleado específico
        /// </summary>
        /// <returns>Una instancia de la clase cEmpleado</returns>
        /// ///<param name="id">Id del empleado requerido</param>
        public Task<cEmpleado> GetEmpleado(string id);

        /// <summary>
        /// getPeriodo: Método para obtener una lista de Periodos 
        /// </summary>
        /// <returns>Lista de cPeriodos</returns>
        public Task<IEnumerable<cPhPeriodo>> GetPeriodo();

        /// <summary>
        /// GetEmpleado: Método para un periodo específico
        /// </summary>
        /// <returns>Una instancia de la clase cPeriodo</returns>
        /// ///<param name="id">Id del periodo requerido</param>
        public Task<cPhPeriodo> GetPeriodo(string id);

        /// <summary>
        /// getPeriodo: Método para obtener una lista de Periodos 
        /// </summary>
        /// <returns>Lista de cPeriodos</returns>
        /// <param name="fecha">Fecha del periodo</param>
        /// <param name="vigente">Periodo está vigente </param>
        public Task<IEnumerable<cPhPeriodo>> GetPeriodo(string fecha, string vigente);

        /// <summary>
        /// GetPeriodoVigenteEmpleado: Método para obtener periodo vigente de un empleado 
        /// </summary>
        /// <returns>Una instancia de la clase cPeriodo</returns>
        /// <param name="idnumero">Número del empleado requerido</param>
        /// <param name="fechaPeriodo">Fecha del periodo</param>
        public Task<cPhPeriodo> GetPeriodoVigenteEmpleado(string idnumero, string fechaPeriodo);


        /// <summary>
        /// GetAccionPersonal: Método para una Acciones de Personal 
        /// </summary>
        /// <returns>Una instancia de cAccionPersonal</returns>
        /// <param name="idregistro">Id de acción</param>
        public Task<cAccionPersonal> GetAccionPersonal(long idregistro);
        /// <summary>
        /// GetAccionPersonal: Método para obtener una lista de Acciones de Personal 
        /// </summary>
        /// <returns>Lista de cAccionPersonal</returns>
        /// <param name="IdPlanilla">Código de Planilla</param>
        /// <param name="FechaInicio">Fecha de Inicio del reporte para obtener la lista de acciones de personal</param>
        /// <param name="FechaFin">Fecha de Final del reporte para obtener la lista de acciones de personal</param>
        public Task<IEnumerable<cAccionPersonal>> GetAccionPersonal(string IdPlanilla, DateTime FechaInicio, DateTime FechaFin);

        /// <summary>
        /// GetAccionPersonal: Método para obtener una lista de Acciones de Personal 
        /// </summary>
        /// <returns>Lista de cAccionPersonal</returns>
        /// <param name="IdPlanilla">Código de Planilla</param>
        /// <param name="FechaInicio">Fecha de Inicio del reporte para obtener la lista de acciones de personal</param>
        /// <param name="FechaFin">Fecha de Final del reporte para obtener la lista de acciones de personal</param>
        /// <param name="usuario">id de usuario que realizó el registro de la acción</param>
        public Task<IEnumerable<cAccionPersonal>> GetAccionPersonal(string IdPlanilla, DateTime FechaInicio, DateTime FechaFin, string usuario);

        /// <summary>
        /// GetAccionPersonalPorEstado: Método para obtener una lista de Acciones de Personal 
        /// </summary>
        /// <returns>Lista de cAccionPersonal</returns>
        /// <param name="IdPlanilla">Código de Planilla</param>
        /// <param name="usuario">id de usuario que realizó el registro de la acción</param>
        /// <param name="estado">Estado de la acción de personal</param>
        public Task<IEnumerable<cAccionPersonal>> GetAccionPersonalPorEstado(string IdPlanilla, string usuario, char estado);

        /// <summary>
        /// GetAccionPersonalPorPeriodo: Método para obtener una lista de Acciones de Personal por periodo 
        /// </summary>
        /// <returns>Lista de cAccionPersonal</returns>
        /// <param name="idnumero">id del empleado</param>
        /// <param name="fecha">fecha para determinar periodo vigente</param>
        /// <param name="idplanilla">id de planilla</param>
        public Task<IEnumerable<cAccionPersonal>> GetAccionPersonalPorPeriodo(string idnumero, string fecha, string idplanilla);

        /// <summary>
        /// GetAccionPersonalPorEstado: Método para obtener una lista de Acciones de Personal 
        /// </summary>
        /// <returns>Lista de cAccionPersonal</returns>
        /// <param name="IdPlanilla">Código de Planilla</param>
        /// <param name="estado">Estado de la acción de personal</param>
        public Task<IEnumerable<cAccionPersonal>> GetAccionPersonalPorEstado(string IdPlanilla, char estado);

        /// <summary>
        /// getMarcaIn: Método para obtener una lista de Marcas de ingreso, salida y descanso 
        /// </summary>
        /// <returns>Lista de cMarcaIn</returns>
        public Task<IEnumerable<cMarcaIn>> GetMarcaIn();

        /// <summary>
        /// getMarcaIn: Método para obtener una lista de Marcas de ingreso, salida y descanso para un colaborador
        /// </summary>
        /// <returns>Lista de cMarcaIn</returns>
        /// ///<param name="idtarjeta">idtarjeta del empleado requerido</param>
        public Task<IEnumerable<cMarcaIn>> GetMarcaIn(string idtarjeta);

        /// <summary>
        /// GetMarcaExtraApb: Método para obtener una Solicitud de Horas Extra
        /// </summary>
        /// <returns>Una instancia de cMarcaExtraApb</returns>
        /// <param name="idregsitro">nuero de registro</param>
        public Task<cMarcaExtraApb> GetMarcaExtraApb(long idregistro);

        /// <summary>
        /// GetMarcaExtraApb: Método para obtener una lista de Solicitudes de Horas Extra
        /// </summary>
        /// <returns>Lista de cMarcaExtraApb</returns>
        /// <param name="idnumero">Número de Empleado</param>
        /// <param name="fechaPeriodo">Fecha del Periodo para el cual se requieren las marcas</param>
        /// <param name="idplanilla">id de planilla </param>
        public Task<IEnumerable<cMarcaExtraApb>> GetMarcaExtraApb(string idnumero, string fechaPeriodo, string idplanilla, bool byPeriodo);

        /// <summary>
        /// GetMarcaMovTurno: Método para obtener todos los datos de la tabla Marcas_Mov_Turnos 
        /// </summary>
        /// <returns>Lista de registros de la clase cMarcaMovTurno</returns>
        public Task<IEnumerable<cMarcaMovTurno>> GetMarcaMovTurno();

        /// <summary>
        /// GetMarcaMovTurno: Método para obtener una lista de Marcas Mov Turnos por empleado 
        /// </summary>
        /// <returns>Lista de cMarcaMovTurno</returns>
        /// <param name="idnumero">Número de Empleado</param>
        /// <param name="fechaPeriodo">Fecha del Periodo para el cual se requieren los turnos</param>
        public Task<IEnumerable<cMarcaMovTurno>> GetMarcaMovTurno(string idnumero, string fechaPeriodo);

        /// <summary>
        /// GetMarcaMovTurnoByGrupo: Método para obtener una lista de Marcas Mov Turnos por Grupos 
        /// </summary>
        /// <returns>Lista de cMarcaMovTurno</returns>
        /// <param name="idgrupo">Número de grupos</param>
        /// <param name="fechaPeriodo">Fecha del Periodo para el cual se requieren los turnos</param>
        public Task<IEnumerable<cMarcaMovTurno>> GetMarcaMovTurnoByGrupo(string fechaPeriodo, string idgrupo);

        /// <summary>
        /// GetTurno: Método para obtener una lista de turnos
        /// </summary>
        /// <returns>Lista de cTurno</returns>
        public Task<IEnumerable<cTurno>> GetTurno();

        /// <summary>
        /// GetTurno: Método para obtener un Turno específico
        /// </summary>
        /// <returns>Una instancia de la clase cturno</returns>
        /// ///<param name="id">Id del turno requerido</param>
        public Task<cTurno> GetTurno(int id);

        /// <summary>
        /// GetMarca: Método para obtener una lista de Marcas por empleado y Periodo
        /// </summary>
        /// <returns>Lista de cMarca</returns>
        /// <param name="idnumero">Número de Empleado</param>
        /// <param name="fechaPeriodo">Fecha del Periodo para el cual se requieren las marcas</param>
        public Task<IEnumerable<cMarca>> GetMarca(string idnumero, string fechaPeriodo);

        /// <summary>
        /// GetMarcaProceso: Método para obtener una lista de Marcas Proceso por Periodo
        /// </summary>
        /// <returns>Lista de cMarcaProceso</returns>
        /// <param name="fechaPeriodo">Fecha del Periodo para el cual se requieren las marcas</param>
        public Task<IEnumerable<cMarcaProceso>> GetMarcaProceso(string fechaPeriodo);

        /// <summary>
        /// GetMarca: Método para obtener una lista de Marcas Proceso por empleado y Periodo
        /// </summary>
        /// <returns>Lista de cMarcaProceso</returns>
        /// <param name="idnumero">Número de Empleado</param>
        /// <param name="fechaPeriodo">Fecha del Periodo para el cual se requieren las marcas</param>
        public Task<IEnumerable<cMarcaProceso>> GetMarcaProceso(string idnumero, string fechaPeriodo);

        /// <summary>
        /// GetMarcaAudit: Método para obtener una lista de Marcas Auditoria por empleado, periodo y planilla
        /// </summary>
        /// <returns>Lista de cMarcaAudit</returns>
        /// <param name="idnumero">Número de Empleado</param>
        /// <param name="fechaPeriodo">Fecha del Periodo para el cual se requieren las marcas</param>
        /// <param name="idplanilla">id de planilla </param>
        public Task<IEnumerable<cMarcaAudit>> GetMarcaAudit(string idnumero, string fechaPeriodo, string idplanilla);

        /// <summary>
        /// GetMarcaDescanso: Método para obtener una lista de Marcas de Descanso por empleado, periodo y planilla
        /// </summary>
        /// <returns>Lista de cMarcaProceso</returns>
        /// <param name="idnumero">Número de Empleado</param>
        /// <param name="fechaPeriodo">Fecha del Periodo para el cual se requieren las marcas</param>
        /// <param name="idplanilla">id de planilla </param>
        public Task<IEnumerable<cMarcaDescanso>> GetMarcaDescanso(string idnumero, string fechaPeriodo, string idplanilla);

        /// <summary>
        /// GetCentroCosto: Método para obtener una lista de Centros de Costo
        /// </summary>
        /// <returns>Lista de cCentroCosto</returns>
        public Task<IEnumerable<cCentroCosto>> GetCentroCosto();

        /// <summary>
        /// GetIncidencia: Método para obtener una lista de Incidencias
        /// </summary>
        /// <returns>Lista de cIncidencia</returns>
        public Task<IEnumerable<cIncidencia>> GetIncidencia();

        /// <summary>
        /// GetIncidencia: Método para obtener una incidencia específica
        /// </summary>
        /// <returns>Una instancia de la clase cIncidencia</returns>
        /// ///<param name="id">Id de incidencia requerido</param>
        public Task<cIncidencia> GetIncidencia(int id);

        /// <summary>
        /// GetPhPlanilla: Método para obtener una planilla específica
        /// </summary>
        /// <returns>Una instancia de la clase cPhPlanilla</returns>
        /// <param name="id">Id de planilla requerido</param>
        public Task<cPhPlanilla> GetPhPlanilla(string id);

        /// <summary>
        /// /Obtener lista de Compañias asociadas al usuario
        /// </summary>
        /// <returns>Lista de Companias de Usuario </returns>

        public Task<IEnumerable<cPhCompaniaUsuario>> GetPhCompaniaUsuario(string idnumero);

        /// <summary>
        /// GetMarcasIncidencias: Obtener las Marcas Incidencias para un empleado, planilla y un periodo especifico
        /// </summary>
        /// <param name="idnumero">numero de empleado a buscar</param>
        /// <param name="fecha">fecha para determinar periodo</param>
        /// <param name="idplanilla">id de planilla</param>
        /// <returns>Lista de Marcas Incidencias</returns>
        public Task<IEnumerable<cMarcaIncidencia>> GetMarcaIncidencia(string idnumero, string fecha, string idplanilla);

        /// <summary>
        /// GetMarcaIncidencia: Obtener una Marca Incidencia especifica segun el id indicado en el parámetro
        /// </summary>
        /// <param name="id">numero de marca incidencia</param>
        /// <returns>Una instancia de Marcas Incidencias</returns>
        public Task<cMarcaIncidencia> GetMarcaIncidencia(long id);

        /// <summary>
        /// GetPhUsuario: Método para obtener un registro de Usuario
        /// </summary>
        /// <returns>Una instancia de la clase cPhUsuario</returns>
        /// <param name="id">Id de empleado requerido</param>
        public Task<cPhUsuario> GetPhUsuario(string id);

        /// <summary>
        /// GetPhSistema: Método para obtener un registro de Sistema
        /// </summary>
        /// <returns>Una instancia de la clase cPhUsuario</returns>
        public Task<cPhSistema> GetPhSistema();

        /// <summary>
        /// GetPortalConfig: Método para obtener un registro de Configuracion del Portal
        /// </summary>
        /// <returns>Una instancia de la clase cPortal_Config</returns>
        public Task<cPortal_Config> GetPortalConfig();

        /// <summary>
        /// GetPortalOpcion: Obtener lista de opciones del menu de Portal 
        /// </summary>
        /// <returns>Lista de Opciones del sistema</returns>
        public Task<IEnumerable<cPortal_Opcion>> GetPortalOpcion();

        /// <summary>
        /// GetPortalOpcion: Obtener datos de una opcion de sistema 
        /// </summary>
        /// <param name="id">id de la opcion</param>
        /// <returns>Instancia de cPortal_Opcion con los datos de la opción </returns>
        public Task<cPortal_Opcion> GetPortalOpcion(string id);

        /// <summary>
        /// GetPhFormulacion: Obtener lista de registros de la tabla PH_FROMULACION
        /// </summary>
        /// <returns>Lista de cPh_Formulacion </returns>
        public Task<IEnumerable<cPhFormulacion>> GetPhFormulacion();

        /// <summary>
        /// GetPhFormulacion: Obtener datos de una opcion de sistema 
        /// </summary>
        /// <param name="id">id de la opcion</param>
        /// <returns>Instancia de cPortal_Opcion con los datos de la opción </returns>
        public Task<cPhFormulacion> GetPhFormulacion(int id);

        /// <summary>
        /// GetDepartamento: obtener lista de departamentos
        /// </summary>
        /// <returns>Lista de departamentos</returns>
        public Task<IEnumerable<cDepartamento>> GetDepartamento();

        /// <summary>
        /// GetDepartamento: Obtiene un registro de la tabla departamentos
        /// </summary>
        /// <param name="id">id del departamento</param>
        /// <returns>retorna una instancia de departamento</returns>
        public Task<cDepartamento> GetDepartamento(string id);

        /// <summary>
        /// GetHorarios: Método para obtener una lista de registros de la tabla ph_horarios
        /// </summary>
        /// <returns>Un horario</returns>
        public Task<IEnumerable<cPhHorario>> GetHorarios();
        /// <summary>
        /// GetHorarios: Obtener varios registros de la tabla ph_horarios
        /// </summary>
        /// <returns>Lista de horario</returns>
        public Task<cPhHorario> GetHorarios(int IDHORARIO);

        /// <summary>
        /// GetHorario_Turno: Método para obtener los registros de la tabla ph_horarios_turnos
        /// </summary>
        /// <returns>Listas de horario turno</returns>
        public Task<IEnumerable<cPhHorarioTurno>> GetHorarioTurno();

        /// <summary>
        /// GetHorario_Turno: Método para obtener los registros de la tabla ph_horarios_turnos
        /// </summary>
        /// <returns>Un horario turno</returns>
        public Task<cPhHorarioTurno> GetHorarioTurno(int IDHORARIO);

        /// <summary>
        /// GetConcepto: obtener lista de conceptos
        /// </summary>
        /// <returns>Lista de concepto</returns>
        public Task<IEnumerable<cConcepto>> GetConcepto();

        /// <summary>
        /// GetConcepto: Obtiene un registro de la tabla concepto
        /// </summary>
        /// <param name="id">id del concepto</param>
        /// <returns>retorna una instancia de concepto</returns>
        public Task<cConcepto> GetConcepto(string id);

    }
}
