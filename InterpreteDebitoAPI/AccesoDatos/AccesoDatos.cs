using System;
using System.Data.SqlClient;
using Dapper;
using InterpreteDebitoAPI.Entities;

namespace InterpreteDebitoAPI
{
    public interface IAccesoDatos
    {
        public TiemposEjecucionResponseDTO prConsultarTiemposEjecucion(TiemposEjecucionRequestDTO Params);
        public TiemposEjecucionResponseDTO prConsultarPagTiemposEjecucion(TiemposEjecucionRequestDTO Params);
        public DetalleTransaccionResponseDTO prConsultarDetalleTransaccion(DetalleTransaccionRequestDTO Params);
        public DashboardResponseDTO prConsultarDashboard(DashboardRequestDTO pParams);
        public TramasResponseDTO prConsultarTramas(DashboardRequestDTO pParams);
        public DashboardRechazosResponseDTO prConsultarDashboardRechazos(DashboardRequestDTO pParams);
        public EvolutivoResponseDTO prConsultarEvolutivo(EvolutivoRequestDTO pParams);
        public List<DetalleOperacion> prConsultarDetalleTransaccionOriginal(DetalleTransaccionOriginalRequestDTO request);


    }

	public class AccesoDatos: IAccesoDatos
    {
        public string strCon;
        private readonly IConfiguration configuration;

        public AccesoDatos(IConfiguration configuration)
        {
            this.configuration = configuration;

            string Server = configuration.GetSection("DBConnection")["Server"];
            string Database = configuration.GetSection("DBConnection")["Database"];
            string User = configuration.GetSection("DBConnection")["User"];
            string Password = configuration.GetSection("DBConnection")["Password"];

            strCon  = $"Server= {Server}; Database= {Database}; User Id= {User}; Password= {Password}; Connect Timeout=2; max pool size=10; Encrypt=True; TrustServerCertificate=True";
        }

        public TiemposEjecucionResponseDTO prConsultarTiemposEjecucion (TiemposEjecucionRequestDTO Params)
        {
            using (SqlConnection sqlcon = new SqlConnection(strCon))
            {
                var queryResult = sqlcon.QueryMultiple("[MSJ].[prConsultarTiemposEjecucion]",
                    new
                    {
                        Params.FechaIni,
                        Params.FechaFin,
                        Params.MTI,
                        Params.CodigoOperacion,
                        Params.NumeroAutorizacion,
                    },
                    commandType: System.Data.CommandType.StoredProcedure
                    );

                return new TiemposEjecucionResponseDTO() { Tiempos = queryResult.ReadFirst<TiemposEjecucion>(), Lst = queryResult.Read<TEjecucionTrama>().ToList() };
            }
        }

        public TiemposEjecucionResponseDTO prConsultarPagTiemposEjecucion(TiemposEjecucionRequestDTO Params)
        {
            using (SqlConnection sqlcon = new SqlConnection(strCon))
            {
                var queryResult = sqlcon.QueryMultiple("[MSJ].[prConsultarPagTiemposEjecucion]",
                    new
                    {
                        Params.FechaIni,
                        Params.FechaFin,
                        Params.MTI,
                        Params.CodigoOperacion,
                        Params.NumeroAutorizacion,
                        Params.paging?.Skip,
                        Params.paging?.Take
                    },
                    commandType: System.Data.CommandType.StoredProcedure
                    );

                return new TiemposEjecucionResponseDTO() { Tiempos = queryResult.ReadFirst<TiemposEjecucion>(), Lst = queryResult.Read<TEjecucionTrama>().ToList() };
            }
        }

        public DetalleTransaccionResponseDTO prConsultarDetalleTransaccion(DetalleTransaccionRequestDTO Params)
        {
            using (SqlConnection sqlcon = new SqlConnection(strCon))
            {
                var queryResult = sqlcon.QueryMultiple("[MSJ].[prConsultarDetalleTransaccion]",
                    new
                    {
                        Params.UID
                    },
                    commandType: System.Data.CommandType.StoredProcedure
                    );

                return new DetalleTransaccionResponseDTO() { LogOperacion = queryResult.ReadFirst<DetalleOperacion>(), LogRequest = queryResult.ReadFirstOrDefault<CMVWSRequest>(), LogResponse= queryResult.ReadFirstOrDefault<CMVWSResponse>() };
            }
        }

        public List<DetalleOperacion> prConsultarDetalleTransaccionOriginal(DetalleTransaccionOriginalRequestDTO request)
        {
            using (SqlConnection sqlcon = new SqlConnection(strCon))
            {
                var queryResult = sqlcon.QueryMultiple("[MSJ].[prConsultarTransaccionOriginal]",
                    new
                    {
                        request.DatosOperacionOriginal,
                        request.CodigoOperacion,
                        request.NumeroAutorizacion
                    },
                    commandType: System.Data.CommandType.StoredProcedure
                    );

                return queryResult.Read<DetalleOperacion>().ToList();
            }
        }

        public DashboardResponseDTO prConsultarDashboard(DashboardRequestDTO pParams)
        {
            using (SqlConnection sqlcon = new SqlConnection(strCon))
            {
                var queryResult = sqlcon.QueryMultiple("[MSJ].[prConsultarDashboard]",
                    new
                    {
                        FechaInicio = pParams.FechaIni,
                        pParams.FechaFin
                    },
                    commandType: System.Data.CommandType.StoredProcedure
                    );

                return new DashboardResponseDTO()
                {   tiemposEjecucion = queryResult.ReadFirst<TiemposEjecucion>(),
                    LstOperacionesTipo = queryResult.Read<CantidadxTipo>().ToList(),
                    LstOperacionesDia = queryResult.Read<CantidadxDia>().ToList(),
                    LstOperacionesHora = queryResult.Read<CantidadxHora>().ToList(),
                };
            }
        }

        public TramasResponseDTO prConsultarTramas(DashboardRequestDTO pParams)
        {
            using (SqlConnection sqlcon = new SqlConnection(strCon))
            {
                var queryResult = sqlcon.QueryMultiple("[MSJ].[prConsultarTramas]",
                    new
                    {
                        pParams.FechaIni,
                        pParams.FechaFin
                    },
                    commandType: System.Data.CommandType.StoredProcedure
                    );

                return new TramasResponseDTO()
                {   lstTramas =  queryResult.Read<MENSAJE_DESGLOSADO>().ToList(),
                };
            }
        }

        public DashboardRechazosResponseDTO prConsultarDashboardRechazos(DashboardRequestDTO pParams)
        {
            using (SqlConnection sqlcon = new SqlConnection(strCon))
            {
                var queryResult = sqlcon.QueryMultiple("[MSJ].[prConsultarDashboardRechazos]",
                    new
                    {
                        FechaInicio = pParams.FechaIni,
                        pParams.FechaFin
                    },
                    commandType: System.Data.CommandType.StoredProcedure
                    );

                return new DashboardRechazosResponseDTO()
                {   lstStatusCode = queryResult.Read<RespuestasXStatus>().ToList(),
                    lstCodigosAccion = queryResult.Read<RespuestasXCodAccion>().ToList(),
                };
            }
        }

        public EvolutivoResponseDTO prConsultarEvolutivo(EvolutivoRequestDTO pParams)
        {
            using (SqlConnection sqlcon = new SqlConnection(strCon))
            {
                var queryResult = sqlcon.QueryMultiple("[MSJ].[prConsultarEvolutivo]",
                    new
                    {
                        FechaInicio = pParams.FechaIni,
                        pParams.FechaFin,
                        pParams.CodigoOperacion
                    },
                    commandType: System.Data.CommandType.StoredProcedure
                    );

                return new EvolutivoResponseDTO()
                {   lst = queryResult.Read<EvolutivoItem>().ToList(),
                };
            }
        }

    }
}

