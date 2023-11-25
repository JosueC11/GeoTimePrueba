
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;

using GeotimeNet.Client.Modelo;
using GeotimeNet.Client.Modelo.FromApi.Request;
using GeotimeNet.Client.Modelo.FromApi.Response;
using GeotimeNet.Client.Modelo.FromApi;
using GeotimeNet.Client.Data.Interfaces;
using System.Net;

namespace GeotimeNet.Client.Data
{
    public class GeoTimeConnect:IGeoTimeConnect
    {

        private readonly NavigationManager navigationManager;
        private string Schema;

        public event Action OnChange;
        public GeoTimeConnect(NavigationManager NavigationManager)
        {
            navigationManager = NavigationManager;
            Schema = "";
        }

        public void setSchema(string schema)
        {
            Schema = schema;
            NotifyStateChanged(); 
        }
        private void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }

        /// <summary>
        /// GetUrlApi: Método para obtener los datos de configuracion que se encuentran en el appsettings.json
        /// </summary>
        /// <returns>Instancia de la clase AppSettings</returns>
        private async Task<AppSettings> GetUrlApi()
        {
            var httpClient = new HttpClient { BaseAddress = new Uri(navigationManager.BaseUri) };
            var appSettings = await httpClient.GetFromJsonAsync<AppSettings>("appsettings.json");
            return appSettings;
        }


        /// <summary>
        /// getToken: Método para obtener token de acceso a la Api
        /// </summary>
        /// <returns>String con el token solicitado</returns>
   
        public async Task<string> getToken()
        {
            
            string? token = "";
            try
            {
                AppSettings appSettings = await GetUrlApi();
                using var client = new HttpClient();
                var api = appSettings.ApiGeoTimeUrl + "/User";
                client.BaseAddress = new Uri(appSettings.ApiGeoTimeUrl);
                var userRequest = new UserRequest
                {                   
                    ClientId = "197ac2e4bd0843c3974725a6544e1089c4a7dcae59087543ba6428c9914c35d9",
                    User = "GSITCR",
                    Password = "c5bbf3d10de5c6dfdad016e6e948a27d343b5e22f35471324388460c4e14a27c",
                    Schema = Schema==""?appSettings.ApiGeotimeSchema : Schema,
                    BDName = appSettings.ApiGeotimeDataBase
                };

                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);

                var postData = JsonSerializer.Serialize(userRequest);
                var contentData = new StringContent(postData, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(api, contentData);

                if (response.IsSuccessStatusCode)
                {
                    var respuesta = await response.Content.ReadFromJsonAsync<UserResponse>();

                    if (respuesta is not null)
                        token = respuesta.Token;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return token == null ? "" : token;
        }

        /// <summary>
        /// Post: Método para crear o actualizar registros en objetos de la base de datos a traves del método Post
        /// </summary>
        /// <returns>Objeto de la clase EventResponse </returns>
        /// <param name="apiName">Nombre del método de la Api a Ejecutar</param>
        /// <param name="model">Objeto de la clase a enviar en el cuerpo del mensaje</param>
        public async Task<EventResponse> Post(string apiName, object model)
        {
            EventResponse eventoAdd = new();

            try
            {
                AppSettings appSettings = await GetUrlApi();
                apiName = appSettings.ApiGeoTimeUrl + "/" + apiName;
                using var client = new HttpClient();
                client.BaseAddress = new Uri(appSettings.ApiGeoTimeUrl);
                var token = await getToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);

                var postData = JsonSerializer.Serialize(model);
                var contentData = new StringContent(postData, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(apiName, contentData);

                eventoAdd = await response.Content.ReadFromJsonAsync<EventResponse>();
            }
            catch (Exception e)
            {
                if (e.InnerException is null)
                {
                    Console.Write(e.Message);
                }
                else
                {
                    Console.Write(e.InnerException.Message);
                }
                throw;
            }
            return eventoAdd;
        }

        /// <summary>
        /// Delete:  Metodo generico para borrado de datos de las tablas
        /// </summary>
        /// <param name="apiName"></param>
        /// <param name="id"></param>
        /// <returns>EventResponse</returns>
        public async Task<EventResponse> Delete(string apiName, string id)
        {
            EventResponse respuesta = new();
            try
            {
                AppSettings appSettings = await GetUrlApi();
                apiName = $"{appSettings.ApiGeoTimeUrl}/{apiName}/{id}";
                using var client = new HttpClient();
                client.BaseAddress = new Uri(appSettings.ApiGeoTimeUrl);
                var token = await getToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);

                var responseTask = await client.DeleteAsync(apiName);

                respuesta = await responseTask.Content.ReadFromJsonAsync<EventResponse>();

            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
                throw;
            }
            return respuesta;

        }

        /// <summary>
        /// GetEmpleado: Método para obtener una lista de empleados 
        /// </summary>
        /// <returns>Lista de cEmpleados</returns>

        public async Task<HttpResponseMessage> Get(string method)
        {
            AppSettings appSettings = await GetUrlApi();
            using var client = new HttpClient();
            client.BaseAddress = new Uri(appSettings.ApiGeoTimeUrl);
            var api = $"{appSettings.ApiGeoTimeUrl}{method}";
            var token = await getToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await client.GetAsync(api);
        }
        public async Task<IEnumerable<cEmpleado>> GetEmpleado() {
            IEnumerable<cEmpleado> empleado = null;
            HttpResponseMessage result = null;
            
            try
            {
                var method = "/Empleado";
                result =  await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cEmpleado>>();
                    readJob.Wait();
                    empleado = readJob.Result;

                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
                empleado = Enumerable.Empty<cEmpleado>();

            }
            return empleado;
        }

        /// <summary>
        /// GetEmpleado: Método para un empleado específico
        /// </summary>
        /// <returns>Una instancia de la clase cEmpleado</returns>
        /// ///<param name="id">Id del empleado requerido</param>
        public async Task<cEmpleado> GetEmpleado(string id)
        {
            cEmpleado empleado = null;
            HttpResponseMessage result = null;
            try
            {

                var method = $"/Empleado/{id}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<cEmpleado>();
                    readJob.Wait();
                    empleado = readJob.Result;
                }

            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
            }
            return empleado;
        }


        /// <summary>
        /// getPeriodo: Método para obtener una lista de Periodos 
        /// </summary>
        /// <returns>Lista de cPeriodos</returns>
        public async Task<IEnumerable<cPhPeriodo>> GetPeriodo()
        {
            IEnumerable<cPhPeriodo> periodo = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/Periodo";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cPhPeriodo>>();
                    readJob.Wait();
                    periodo = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
                periodo = Enumerable.Empty<cPhPeriodo>();
            }
            return periodo;
        }

        /// <summary>
        /// getPeriodo: Método para un periodo específico
        /// </summary>
        /// <returns>Una instancia de la clase cPeriodo</returns>
        /// ///<param name="id">Id del periodo requerido</param>
        public async Task<cPhPeriodo> GetPeriodo(string id)
        {
            cPhPeriodo periodo = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/Periodo/{id}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<cPhPeriodo>();
                    readJob.Wait();
                    periodo = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
            }
            return periodo;
        }

        /// <summary>
        /// getPeriodo: Método para obtener una lista de Periodos 
        /// </summary>
        /// <returns>Lista de cPeriodos</returns>
        /// <param name="fecha">Fecha del periodo</param>
        /// <param name="vigente">Periodo está vigente </param>
        public async Task<IEnumerable<cPhPeriodo>> GetPeriodo(string fecha, string vigente)
        {
            IEnumerable<cPhPeriodo> periodo = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/Periodo/{fecha}/{vigente}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cPhPeriodo>>();
                    readJob.Wait();
                    periodo = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
                periodo = Enumerable.Empty<cPhPeriodo>();
            }
            return periodo;
        }

        /// <summary>
        /// GetPeriodoVigenteEmpleado: Método para obtener periodo vigente de un empleado 
        /// </summary>
        /// <returns>Una instancia de la clase cPeriodo</returns>
        /// <param name="idnumero">Número del empleado requerido</param>
        /// <param name="fechaPeriodo">Fecha del periodo</param>
        public async Task<cPhPeriodo> GetPeriodoVigenteEmpleado(string idnumero, string fechaPeriodo)
        {
            cPhPeriodo periodo = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/PeriodoVigenteEmpleado/{idnumero}/{fechaPeriodo}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<cPhPeriodo>();
                    readJob.Wait();
                    periodo = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
            }
            return periodo;
        }

        /// <summary>
        /// GetAccionPersonal: Método para una Acciones de Personal 
        /// </summary>
        /// <returns>Una instancia de cAccionPersonal</returns>
        /// <param name="idregistro">Id de acción</param>
        public async Task<cAccionPersonal> GetAccionPersonal(long idregistro)
        {
            cAccionPersonal accionPersonal = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/AccionPersonal/{idregistro}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<cAccionPersonal>();
                    readJob.Wait();
                    accionPersonal = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);               
            }
            return accionPersonal;
        }


        /// <summary>
        /// GetAccionPersonal: Método para obtener una lista de Acciones de Personal 
        /// </summary>
        /// <returns>Lista de cAccionPersonal</returns>
        /// <param name="IdPlanilla">Código de Planilla</param>
        /// <param name="FechaInicio">Fecha de Inicio del reporte para obtener la lista de acciones de personal</param>
        /// <param name="FechaFin">Fecha de Final del reporte para obtener la lista de acciones de personal</param>
        public async Task<IEnumerable<cAccionPersonal>> GetAccionPersonal(string IdPlanilla, DateTime FechaInicio, DateTime FechaFin) 
        {
            IEnumerable<cAccionPersonal> accionPersonal = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/AccionPersonal/{IdPlanilla}/{FechaInicio}/{FechaFin}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cAccionPersonal>>();
                    readJob.Wait();
                    accionPersonal = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
                accionPersonal = Enumerable.Empty<cAccionPersonal>();
            }
            return accionPersonal;
        }

        /// <summary>
        /// GetAccionPersonal: Método para obtener una lista de Acciones de Personal 
        /// </summary>
        /// <returns>Lista de cAccionPersonal</returns>
        /// <param name="IdPlanilla">Código de Planilla</param>
        /// <param name="FechaInicio">Fecha de Inicio del reporte para obtener la lista de acciones de personal</param>
        /// <param name="FechaFin">Fecha de Final del reporte para obtener la lista de acciones de personal</param>
        /// <param name="usuario">id de usuario que realizó el registro de la acción</param>
        public async Task<IEnumerable<cAccionPersonal>> GetAccionPersonal(string IdPlanilla, DateTime FechaInicio, DateTime FechaFin, string usuario) 
        {
            IEnumerable<cAccionPersonal> accionPersonal = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/AccionPersonal/{IdPlanilla}/{FechaInicio}/{FechaFin}/{usuario}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cAccionPersonal>>();
                    readJob.Wait();
                    accionPersonal = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
                accionPersonal = Enumerable.Empty<cAccionPersonal>();
            }
            return accionPersonal;
        }

        /// <summary>
        /// GetAccionPersonalPorEstado: Método para obtener una lista de Acciones de Personal 
        /// </summary>
        /// <returns>Lista de cAccionPersonal</returns>
        /// <param name="IdPlanilla">Código de Planilla</param>
        /// <param name="usuario">id de usuario que realizó el registro de la acción</param>
        /// <param name="estado">Estado de la acción de personal</param>
        public async Task<IEnumerable<cAccionPersonal>> GetAccionPersonalPorEstado(string IdPlanilla, string usuario, char estado) 
        {
            IEnumerable<cAccionPersonal> accionPersonal = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/AccionPersonalPorEstado/{IdPlanilla}/{usuario}/{estado}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cAccionPersonal>>();
                    readJob.Wait();
                    accionPersonal = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
                accionPersonal = Enumerable.Empty<cAccionPersonal>();
            }
            return accionPersonal;

        }

        /// <summary>
        /// GetAccionPersonalPorPeriodo: Método para obtener una lista de Acciones de Personal por periodo 
        /// </summary>
        /// <returns>Lista de cAccionPersonal</returns>
        /// <param name="idnumero">id del empleado</param>
        /// <param name="fecha">fecha para determinar periodo vigente</param>
        /// <param name="idplanilla">id de planilla</param>
        public async Task<IEnumerable<cAccionPersonal>> GetAccionPersonalPorPeriodo(string idnumero, string fecha, string idplanilla)
        {
            IEnumerable<cAccionPersonal> accionPersonal = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/AccionPersonalPorPeriodo/{idnumero}/{fecha}/{idplanilla}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cAccionPersonal>>();
                    readJob.Wait();
                    accionPersonal = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
                accionPersonal = Enumerable.Empty<cAccionPersonal>();
            }
            return accionPersonal;

        }

        /// <summary>
        /// GetAccionPersonalPorEstado: Método para obtener una lista de Acciones de Personal 
        /// </summary>
        /// <returns>Lista de cAccionPersonal</returns>
        /// <param name="IdPlanilla">Código de Planilla</param>
        /// <param name="estado">Estado de la acción de personal</param>
        public async Task<IEnumerable<cAccionPersonal>> GetAccionPersonalPorEstado(string IdPlanilla, char estado) 
        {
            IEnumerable<cAccionPersonal> accionPersonal = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/AccionPersonalPorEstado/{IdPlanilla}/{estado}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cAccionPersonal>>();
                    readJob.Wait();
                    accionPersonal = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
                accionPersonal = Enumerable.Empty<cAccionPersonal>();
            }
            return accionPersonal;

        }


        /// <summary>
        /// getMarcaIn: Método para obtener una lista de Marcas de ingreso, salida y descanso 
        /// </summary>
        /// <returns>Lista de cMarcaIn</returns>
        public async Task<IEnumerable<cMarcaIn>> GetMarcaIn()
        {
            IEnumerable<cMarcaIn> marcaIn = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/MarcaIn";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cMarcaIn>>();
                    readJob.Wait();
                    marcaIn = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
                marcaIn = Enumerable.Empty<cMarcaIn>();
            }
            return marcaIn;
        }

        /// <summary>
        /// getMarcaIn: Método para obtener una lista de Marcas de ingreso, salida y descanso para un colaborador
        /// </summary>
        /// <returns>Lista de cMarcaIn</returns>
        /// ///<param name="idtarjeta">idtarjeta del empleado requerido</param>
        public async Task<IEnumerable<cMarcaIn>> GetMarcaIn(string idtarjeta)
        {
            IEnumerable<cMarcaIn> marcaIn = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/MarcaIn/{idtarjeta}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cMarcaIn>>();
                    readJob.Wait();
                    marcaIn = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                marcaIn = Enumerable.Empty<cMarcaIn>();
                Console.WriteLine(e);
            }
            return marcaIn;
        }

        public async Task<IEnumerable<cMarcaExtraApb>> GetMarcaExtraApb()
        {
            IEnumerable<cMarcaExtraApb> marcaExtraApb = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/MarcaExtraApb";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cMarcaExtraApb>>();
                    readJob.Wait();
                    marcaExtraApb = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                marcaExtraApb = Enumerable.Empty<cMarcaExtraApb>();
                Console.WriteLine(e);
            }
            return marcaExtraApb;
        }

       
        /// <summary>
        /// GetMarcaExtraApb: Método para obtener una lista de Solicitudes de Horas Extra
        /// </summary>
        /// <returns>Lista de cMarcaExtraApb</returns>
        /// <param name="idnumero">Número de Empleado</param>
        /// <param name="fechaPeriodo">Fecha del Periodo para el cual se requieren las marcas</param>
        /// <param name="idplanilla">id de planilla </param>
        public async Task<IEnumerable<cMarcaExtraApb>> GetMarcaExtraApb(string idnumero, string fechaPeriodo, string idplanilla, bool byPeriodo)
        {
            IEnumerable<cMarcaExtraApb>? marcaExtraApb = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/MarcaExtraApb/{idnumero}/{fechaPeriodo}/{idplanilla}/{byPeriodo}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cMarcaExtraApb>>();
                    readJob.Wait();
                    marcaExtraApb = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                marcaExtraApb = Enumerable.Empty<cMarcaExtraApb>();
                Console.WriteLine(e);
            }
            return marcaExtraApb;
        }

        /// <summary>
        /// GetMarcaExtraApb: Método para obtener una Solicitud de Horas Extra
        /// </summary>
        /// <returns>Una instancia de cMarcaExtraApb</returns>
        /// <param name="idregsitro">nuero de registro</param>
        public async Task<cMarcaExtraApb> GetMarcaExtraApb(long idregistro)
        {
            cMarcaExtraApb? marcaExtraApb = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/MarcaExtraApb/{idregistro}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<cMarcaExtraApb>();
                    readJob.Wait();
                    marcaExtraApb = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
            }
            return marcaExtraApb;
        }

        /// <summary>
        /// GetMarcaMovTurno: Método para obtener todos los datos de la tabla Marcas_Mov_Turnos 
        /// </summary>
        /// <returns>Lista de registros de la clase cMarcaMovTurno</returns>
        public async Task<IEnumerable<cMarcaMovTurno>> GetMarcaMovTurno() 
        {
            IEnumerable<cMarcaMovTurno>? marcaMovTurno = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/MarcaMovTurno";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cMarcaMovTurno>>();
                    readJob.Wait();
                    marcaMovTurno = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                marcaMovTurno = Enumerable.Empty<cMarcaMovTurno>();
                Console.WriteLine(e);
            }
            return marcaMovTurno;

        }

        /// <summary>
        /// GetMarcaMovTurno: Método para obtener una lista de Marcas Mov Turnos por empleado 
        /// </summary>
        /// <returns>Lista de cMarcaMovTurno</returns>
        /// <param name="idnumero">Número de Empleado</param>
        /// <param name="fechaPeriodo">Fecha del Periodo para el cual se requieren los turnos</param>
        public async Task<IEnumerable<cMarcaMovTurno>> GetMarcaMovTurno(string idnumero, string fechaPeriodo) 
        {
            IEnumerable<cMarcaMovTurno>? marcaMovTurno = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/MarcaMovTurno/{idnumero}/{fechaPeriodo}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cMarcaMovTurno>>();
                    readJob.Wait();
                    marcaMovTurno = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                marcaMovTurno = Enumerable.Empty<cMarcaMovTurno>();
                Console.WriteLine(e);
            }
            return marcaMovTurno;
        }

        /// <summary>
        /// GetMarcaMovTurnoByGrupo: Método para obtener una lista de Marcas Mov Turnos por Grupos 
        /// </summary>
        /// <returns>Lista de cMarcaMovTurno</returns>
        /// <param name="idgrupo">Número de grupos</param>
        /// <param name="fechaPeriodo">Fecha del Periodo para el cual se requieren los turnos</param>
        public async Task<IEnumerable<cMarcaMovTurno>> GetMarcaMovTurnoByGrupo(string fechaPeriodo, string idgrupo)
        {
            IEnumerable<cMarcaMovTurno>? marcaMovTurno = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/MarcaMovTurnoByGrupo/{fechaPeriodo}/{idgrupo}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cMarcaMovTurno>>();
                    readJob.Wait();
                    marcaMovTurno = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                marcaMovTurno = Enumerable.Empty<cMarcaMovTurno>();
                Console.WriteLine(e);
            }
            return marcaMovTurno;
        }

        /// <summary>
        /// GetTurno: Método para obtener una lista de turnos
        /// </summary>
        /// <returns>Lista de cTurno</returns>
        public async Task<IEnumerable<cTurno>> GetTurno()
        {
            IEnumerable<cTurno> turno = null;
            HttpResponseMessage result = null;

            try
            {
                var method = "/Turno";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cTurno>>();
                    readJob.Wait();
                    turno = readJob.Result;

                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
                turno = Enumerable.Empty<cTurno>();

            }
            return turno;
        }

        /// <summary>
        /// GetTurno: Método para obtener un Turno específico
        /// </summary>
        /// <returns>Una instancia de la clase cturno</returns>
        /// ///<param name="id">Id del turno requerido</param>
        public async Task<cTurno> GetTurno(int id)
        {
            cTurno turno = null;
            HttpResponseMessage result = null;
            try
            {

                var method = $"/Turno/{id}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<cTurno>();
                    readJob.Wait();
                    turno = readJob.Result;
                }

            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
            }
            return turno;
        }

        /// <summary>
        /// GetMarca: Método para obtener una lista de Marcas por empleado y Periodo
        /// </summary>
        /// <returns>Lista de cMarca</returns>
        /// <param name="idnumero">Número de Empleado</param>
        /// <param name="fechaPeriodo">Fecha del Periodo para el cual se requieren las marcas</param>
        public async Task<IEnumerable<cMarca>> GetMarca(string idnumero, string fechaPeriodo)
        {
            IEnumerable<cMarca>? marca = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/Marca/{idnumero}/{fechaPeriodo}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cMarca>>();
                    readJob.Wait();
                    marca = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                marca = Enumerable.Empty<cMarca>();
                Console.WriteLine(e);
            }
            return marca;
        }

        /// <summary>
        /// GetMarcaProceso: Método para obtener una lista de Marcas Proceso por Periodo
        /// </summary>
        /// <returns>Lista de cMarcaProceso</returns>
        /// <param name="fechaPeriodo">Fecha del Periodo para el cual se requieren las marcas</param>
        public async Task<IEnumerable<cMarcaProceso>> GetMarcaProceso(string fechaPeriodo)
        {
            IEnumerable<cMarcaProceso>? marcaProceso = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/MarcaProceso/{fechaPeriodo}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cMarcaProceso>>();
                    readJob.Wait();
                    marcaProceso = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                marcaProceso = Enumerable.Empty<cMarcaProceso>();
                Console.WriteLine(e);
            }
            return marcaProceso;
        }

        /// <summary>
        /// GetMarcaProceso: Método para obtener una lista de Marcas Proceso por empleado y Periodo
        /// </summary>
        /// <returns>Lista de cMarcaProceso</returns>
        /// <param name="idnumero">Número de Empleado</param>
        /// <param name="fechaPeriodo">Fecha del Periodo para el cual se requieren las marcas</param>
        public async Task<IEnumerable<cMarcaProceso>> GetMarcaProceso(string idnumero, string fechaPeriodo)
        {
            IEnumerable<cMarcaProceso>? marcaProceso = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/MarcaProceso/{idnumero}/{fechaPeriodo}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cMarcaProceso>>();
                    readJob.Wait();
                    marcaProceso = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                marcaProceso = Enumerable.Empty<cMarcaProceso>();
                Console.WriteLine(e);
            }
            return marcaProceso;
        }

        /// <summary>
        /// GetMarcaAudit: Método para obtener una lista de Marcas Auditoria por empleado, periodo y planilla
        /// </summary>
        /// <returns>Lista de cMarcaAudit</returns>
        /// <param name="idnumero">Número de Empleado</param>
        /// <param name="fechaPeriodo">Fecha del Periodo para el cual se requieren las marcas</param>
        /// <param name="idplanilla">id de planilla </param>
        public async Task<IEnumerable<cMarcaAudit>> GetMarcaAudit(string idnumero, string fechaPeriodo, string idplanilla)
        {
            IEnumerable<cMarcaAudit>? marcaAudit = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/MarcaAudit/{idnumero}/{fechaPeriodo}/{idplanilla}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cMarcaAudit>>();
                    readJob.Wait();
                    marcaAudit = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                marcaAudit = Enumerable.Empty<cMarcaAudit>();
                Console.WriteLine(e);
            }
            return marcaAudit;
        }

        /// <summary>
        /// GetMarcaDescanso: Método para obtener una lista de Marcas de Descanso por empleado, periodo y planilla
        /// </summary>
        /// <returns>Lista de cMarcaProceso</returns>
        /// <param name="idnumero">Número de Empleado</param>
        /// <param name="fechaPeriodo">Fecha del Periodo para el cual se requieren las marcas</param>
        /// <param name="idplanilla">id de planilla </param>
        public async Task<IEnumerable<cMarcaDescanso>> GetMarcaDescanso(string idnumero, string fechaPeriodo, string idplanilla)
        {
            IEnumerable<cMarcaDescanso>? marcaDescanso = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/MarcaDescanso/{idnumero}/{fechaPeriodo}/{idplanilla}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cMarcaDescanso>>();
                    readJob.Wait();
                    marcaDescanso = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                marcaDescanso = Enumerable.Empty<cMarcaDescanso>();
                Console.WriteLine(e);
            }
            return marcaDescanso;
        }

        /// <summary>
        /// GetCentroCosto: Método para obtener una lista de Centros de Costo
        /// </summary>
        /// <returns>Lista de cCentroCosto</returns>
        public async Task<IEnumerable<cCentroCosto>> GetCentroCosto()
        {
            IEnumerable<cCentroCosto> centroCosto = null;
            HttpResponseMessage result = null;

            try
            {
                var method = "/CentroCosto";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cCentroCosto>>();
                    readJob.Wait();
                    centroCosto = readJob.Result;

                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
                centroCosto = Enumerable.Empty<cCentroCosto>();

            }
            return centroCosto;
        }

        /// <summary>
        /// GetIncidencia: Método para obtener una lista de Incidencias
        /// </summary>
        /// <returns>Lista de cIncidencia</returns>
        public async Task<IEnumerable<cIncidencia>> GetIncidencia()
        {
            IEnumerable<cIncidencia> incidencia = null;
            HttpResponseMessage result = null;

            try
            {
                var method = "/Incidencia";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cIncidencia>>();
                    readJob.Wait();
                    incidencia = readJob.Result;

                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
                incidencia = Enumerable.Empty<cIncidencia>();

            }
            return incidencia;
        }

        /// <summary>
        /// GetIncidencia: Método para obtener una incidencia específica
        /// </summary>
        /// <returns>Una instancia de la clase cIncidencia</returns>
        /// ///<param name="id">Id de incidencia requerido</param>
        public async Task<cIncidencia> GetIncidencia(int id)
        {
            cIncidencia incidencia = null;
            HttpResponseMessage result = null;
            try
            {

                var method = $"/Incidencia/{id}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<cIncidencia>();
                    readJob.Wait();
                    incidencia = readJob.Result;
                }

            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
            }
            return incidencia;
        }

        /// <summary>
        /// GetPhPlanilla: Método para obtener una planilla específica
        /// </summary>
        /// <returns>Una instancia de la clase cPhPlanilla</returns>
        /// <param name="id">Id de planilla requerido</param>
        public async Task<cPhPlanilla> GetPhPlanilla(string id)
        {
            cPhPlanilla phPlanilla = null;
            HttpResponseMessage result = null;
            try
            {

                var method = $"/PhPlanilla/{id}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<cPhPlanilla>();
                    readJob.Wait();
                    phPlanilla = readJob.Result;
                }

            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
            }
            return phPlanilla;
        }

        /// <summary>
        /// /Obtener lista de Compañias asociadas al usuario
        /// </summary>
        /// <returns>Lista de Companias de Usuario </returns>

        public async Task<IEnumerable<cPhCompaniaUsuario>> GetPhCompaniaUsuario(string idnumero) 
        {
            IEnumerable<cPhCompaniaUsuario>? companiaUsuario = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/PhCompaniaUsuario/{idnumero}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cPhCompaniaUsuario>>();
                    readJob.Wait();
                    companiaUsuario = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                companiaUsuario = Enumerable.Empty<cPhCompaniaUsuario>();
                Console.WriteLine(e);
            }
            return companiaUsuario;
        }

        /// <summary>
        /// GetMarcasIncidencias: Obtener las Marcas Incidencias para un empleado, planilla y un periodo especifico
        /// </summary>
        /// <param name="idnumero">numero de empleado a buscar</param>
        /// <param name="fecha">fecha para determinar periodo</param>
        /// <param name="idplanilla">id de planilla</param>
        /// <returns>Lista de Marcas Incidencias</returns>
        public async Task<IEnumerable<cMarcaIncidencia>> GetMarcaIncidencia(string idnumero, string fecha, string idplanilla) {

            IEnumerable<cMarcaIncidencia>? marcaIncidencia = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/MarcaIncidencia/{idnumero}/{fecha}/{idplanilla}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cMarcaIncidencia>>();
                    readJob.Wait();
                    marcaIncidencia = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                marcaIncidencia = Enumerable.Empty<cMarcaIncidencia>();
                Console.WriteLine(e);
            }
            return marcaIncidencia;
        }

        /// <summary>
        /// GetMarcaIncidencia: Obtener una Marca Incidencia especifica segun el id indicado en el parámetro
        /// </summary>
        /// <param name="id">numero de marca incidencia</param>
        /// <returns>Una instancia de Marcas Incidencias</returns>
        public async Task<cMarcaIncidencia> GetMarcaIncidencia(long id) {
            cMarcaIncidencia? marcaIncidencia = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/MarcaIncidencia/{id}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<cMarcaIncidencia>();
                    readJob.Wait();
                    marcaIncidencia = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
            }
            return marcaIncidencia;
        }

        /// <summary>
        /// GetPhUsuario: Método para obtener un registro de Usuario
        /// </summary>
        /// <returns>Una instancia de la clase cPhUsuario</returns>
        /// <param name="id">Id de empleado requerido</param>
        public async Task<cPhUsuario> GetPhUsuario(string id)
        {
            cPhUsuario phUsuario = null;
            HttpResponseMessage result = null;
            try
            {

                var method = $"/PhUsuario/{id}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<cPhUsuario>();
                    readJob.Wait();
                    phUsuario = readJob.Result;
                }

            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
            }
            return phUsuario;
        }

        /// <summary>
        /// GetPhSistema: Método para obtener un registro de Sistema
        /// </summary>
        /// <returns>Una instancia de la clase cPhUsuario</returns>
        public async Task<cPhSistema> GetPhSistema()
        {
            cPhSistema phSistema = null;
            HttpResponseMessage result = null;
            try
            {

                var method = $"/PhSistema/";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<cPhSistema>();
                    readJob.Wait();
                    phSistema = readJob.Result;
                }

            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
            }
            return phSistema;
        }

        /// <summary>
        /// GetPortalConfig: Método para obtener un registro de Configuracion del Portal
        /// </summary>
        /// <returns>Una instancia de la clase cPortal_Config</returns>
        public async Task<cPortal_Config> GetPortalConfig()
        {
            cPortal_Config portalConfig = null;
            HttpResponseMessage result = null;
            try
            {

                var method = $"/PortalConfig/";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<cPortal_Config>();
                    readJob.Wait();
                    portalConfig = readJob.Result;
                }

            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
            }
            return portalConfig;
        }

        /// <summary>
        /// GetPortalOpcion: Obtener lista de opciones del menu de Portal 
        /// </summary>
        /// <returns>Lista de Opciones del sistema</returns>
        public async Task<IEnumerable<cPortal_Opcion>> GetPortalOpcion()
        {
            IEnumerable<cPortal_Opcion>? portal_Opcion = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/PortalOpcion/";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cPortal_Opcion>>();
                    readJob.Wait();
                    portal_Opcion = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                portal_Opcion = Enumerable.Empty<cPortal_Opcion>();
                Console.WriteLine(e);
            }
            return portal_Opcion;
        }

        /// <summary>
        /// GetPortalOpcion: Obtener datos de una opcion de sistema 
        /// </summary>
        /// <param name="id">id de la opcion</param>
        /// <returns>Instancia de cPortal_Opcion con los datos de la opción </returns>
        public async Task<cPortal_Opcion> GetPortalOpcion(string id)
        {
            cPortal_Opcion portal_Opcion = null;
            HttpResponseMessage result = null;
            try
            {

                var method = $"/PortalOpcion/{id}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<cPortal_Opcion>();
                    readJob.Wait();
                    portal_Opcion = readJob.Result;
                }

            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
            }
            return portal_Opcion;
        }

        public async Task<IEnumerable<cPhFormulacion>> GetPhFormulacion()
        {
            IEnumerable<cPhFormulacion>? phFormulacion = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/PhFormulacion/";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cPhFormulacion>>();
                    readJob.Wait();
                    phFormulacion = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                phFormulacion = Enumerable.Empty<cPhFormulacion>();
                Console.WriteLine(e);
            }
            return phFormulacion;
        }

        public async Task<cPhFormulacion> GetPhFormulacion(int id)
        {
            cPhFormulacion? phFormulacion = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/PhFormulacion/{id}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<cPhFormulacion>();
                    readJob.Wait();
                    phFormulacion = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
            }
            return phFormulacion;
        }

        public async Task<IEnumerable<cDepartamento>> GetDepartamento()
        {
            IEnumerable<cDepartamento>? departamentos = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/Departamento/";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cDepartamento>>();
                    readJob.Wait();
                    departamentos = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                departamentos = Enumerable.Empty<cDepartamento>();
                Console.WriteLine(e);
            }
            return departamentos;
        }

        public async Task<cDepartamento> GetDepartamento(string id)
        {
            cDepartamento? departamento = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/Departamento/{id}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<cDepartamento>();
                    readJob.Wait();
                    departamento = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
            }
            return departamento;
        }

        public async Task<IEnumerable<cPhHorario>> GetHorarios()
        {
            IEnumerable<cPhHorario>? horarios = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/Horarios/";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cPhHorario>>();
                    readJob.Wait();
                    horarios = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                horarios = Enumerable.Empty<cPhHorario>();
                Console.WriteLine(e);
            }
            return horarios;
        }

        public async Task<cPhHorario> GetHorarios(int IDHORARIO)
        {
            cPhHorario? horario = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/Horarios/{IDHORARIO}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<cPhHorario>();
                    readJob.Wait();
                    horario = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
            }
            return horario;
        }


        public async Task<IEnumerable<cPhHorarioTurno>> GetHorarioTurno()
        {
            IEnumerable<cPhHorarioTurno>? horario_turno = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/HorarioTurno/";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cPhHorarioTurno>>();
                    readJob.Wait();
                    horario_turno = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                horario_turno = Enumerable.Empty<cPhHorarioTurno>();
                Console.WriteLine(e);
            }
            return horario_turno;
        }

        public async Task<cPhHorarioTurno> GetHorarioTurno(int IDHORARIO)
        {
            cPhHorarioTurno? horario_turno = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/HorarioTurno/{IDHORARIO}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<cPhHorarioTurno>();
                    readJob.Wait();
                    horario_turno = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
            }
            return horario_turno;
        }

        public async Task<IEnumerable<cConcepto>> GetConcepto()
        {
            IEnumerable<cConcepto>? conceptos = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/Concepto/";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<IList<cConcepto>>();
                    readJob.Wait();
                    conceptos = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                conceptos = Enumerable.Empty<cConcepto>();
                Console.WriteLine(e);
            }
            return conceptos;
        }

        public async Task<cConcepto> GetConcepto(string id)
        {
            cConcepto? concepto = null;
            HttpResponseMessage result = null;
            try
            {
                var method = $"/Concepto/{id}";
                result = await Get(method);

                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadFromJsonAsync<cConcepto>();
                    readJob.Wait();
                    concepto = readJob.Result;
                }
            }
            catch (Exception e)
            {
                //devuelve el codigo de error
                Console.WriteLine(e);
            }
            return concepto;
        }
    }
}
