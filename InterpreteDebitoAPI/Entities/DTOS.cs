using System;
namespace InterpreteDebitoAPI.Entities
{
	public class GenericResponse
	{
		public int Result { get; set; }
        public string? Mensaje { get; set; }
    }

    public class Paging
    {
        public int Skip { get; set; }
        public int Take { get; set; }

    }

    public class AllowedUser
    {
        public string? User { get; set; }
        public string? Password { get; set; }
    }

    public class TokenInfo
	{
		public DateTime fechaCaducidad { get; set; }
		public string? Token { get; set; }
	}

	public class LoginRequestDTO
	{
		public string? User { get; set; }
        public string? Password { get; set; }

    }

    public class LogInResponseDTO : GenericResponse
	{
		public TokenInfo? tokenInfo { get; set; }
	}

	public class TiemposEjecucion
	{
        public double MinimoInter { get; set; }
        public double MaximoInter { get; set; }
        public double MediaInter { get; set; }

		public double MinimoEjec { get; set; }
        public double MaximoEjec { get; set; }
        public double MediaEjec { get; set; }

        public double MinimoResp { get; set; }
        public double MaximoResp { get; set; }
        public double MediaResp { get; set; }

        public int Count { get; set; }

		public DateTime UltimaTransaccion { get; set; }
        public DateTime FechaIni { get; set; }
        public DateTime FechaFin { get; set; }
    }

    public class CantidadxTipo
	{
		public string? CodigoOperacion { get; set; }
		public string? Operacion { get; set; }
		public int Cantidad { get; set; }

        public double MinimoImporte {get; set;}
        public double MaximoImporte {get; set;}
        public double PromedioImporte {get; set;}

	}

    public class CantidadxDia
    {
        public DateTime fecha { get; set; }
        public string CodigoOperacion { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
    }

    public class CantidadxHora
    {
        public int Hora { get; set; }
        public int Cantidad { get; set; }
    }

    public class DashboardRequestDTO
    {
        public DateTime FechaIni { get; set; }
        public DateTime FechaFin { get; set; }

    }

    public class DashboardResponseDTO : GenericResponse
	{
		public TiemposEjecucion? tiemposEjecucion { get; set; }
		public List<CantidadxTipo>? LstOperacionesTipo {get; set;}
        public List<CantidadxDia>? LstOperacionesDia { get; set; }
        public List<CantidadxHora>? LstOperacionesHora { get; set; }

    }

    public class TiemposEjecucionRequestDTO
	{
		public DateTime FechaIni { get; set; }
        public DateTime FechaFin { get; set; }
        public string? MTI { get; set; }
        public string? CodigoOperacion { get; set; }
        public string? NumeroAutorizacion { get; set; }
        public Paging? paging { get; set; }
        
    }

    public class EvolutivoRequestDTO : DashboardRequestDTO
    {
        public string? CodigoOperacion {get; set;}
    }

	public class TEjecucionTrama
	{
		public Guid ? UID { get; set; }
        public string? MTI { get; set; }
        public string? CodigoOperacion { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public DateTime Fechadesglose { get; set; }
        public double TiempoDesglose { get; set; }
        public string? Endpoint { get; set; }
        public DateTime FechaRequest { get; set; }
        public DateTime FechaResponse { get; set; }
        public double TiempoEjecucion { get; set; }
        public double TMedioEjecucion { get; set; }
        public bool ExcedioTMedio { get; set; }
        public string NumeroAutorizacion { get; set; }
        public double? Monto { get; set; }
        public string Accion { get; set; }

    }

    public class TiemposEjecucionResponseDTO : GenericResponse
    {
        public List<TEjecucionTrama>? Lst { get; set; }
		public TiemposEjecucion? Tiempos { get; set; }
	}

	public class DetalleOperacion
	{
		public Guid? UID { get; set; }
        public DateTime? FechaAlta { get; set; }
        public string? MTI { get; set; }
        public string? CodigoOperacion { get; set; }
        public string? Operacion { get; set; }
        public string? TipoOperacion { get; set; }
        public double? ImporteOperacion { get; set; }
        public string? FechaTransaccion { get; set; }
        public string? EntidadAdquiriente { get; set; }
        public string? NombreComercio { get; set; }
        public string? CodigoMoneda { get; set; }
        public string? NumeroAutorizacion { get; set; }
		public bool EsAutParcial { get; set; }
        public string? TokenNegocio { get; set; }
        public string? SiaID { get; set; }
        public string? DatosAdicionales {get; set;}
        public string? DatosPuntoServicio {get; set;}
        public List<DetalleDatosAdicionales>? lstDetalleDatosAdicionales => Utilerias.DesglosarDatosAdicionales(DatosAdicionales);
        public List<DescripcionPuntoServicio> lstDetalleDatosPuntoServicio => Utilerias.DesglosarDatosPuntoServicio( DatosPuntoServicio);

    }

	public class CMVWSRequest
	{
        public Guid? UID { get; set; }
        public DateTime? FechaAlta { get; set; }
        public string? Endpoint { get; set; }
        public string? Body { get; set; }
    }

	public class CMVWSResponse
	{
        public Guid? UID { get; set; }
        public DateTime? FechaAlta { get; set; }
        public string? httpStatusCode { get; set; }
        public string? CodigoAccion { get; set; }
        public string? Accion { get; set; }
        public string? Response { get; set; }
    }

    public class DetalleTransaccionResponseDTO : GenericResponse
    {
        public DetalleOperacion? LogOperacion {get; set;}
        public CMVWSRequest? LogRequest { get; set; }
        public CMVWSResponse? LogResponse { get; set; }
    }

    public class DetalleTransaccionRequestDTO
    {
        public string? UID { get; set; }
    }

    public class MENSAJE_DESGLOSADO
    {
        public Guid UID {get;set;}
        public DateTime FechaAlta {get;set;}
        public string MTI {get;set;}
        public string BitMap {get;set;}
        public string PAN {get;set;}
        public string CodigoOperacion {get;set;}
        public string ImporteOperacion {get;set;}
        public string ImporteConciliacion {get;set;}
        public string ImporteOriginalTransaccion {get;set;}
        public string FechaTransaccion {get;set;}
        public string IndiceConversion {get;set;}
        public string NumeroIdentificacionTransaccion {get;set;}
        public string FechaLocal {get;set;}
        public string FechaCaducidad {get;set;}
        public string CodigoActividad {get;set;}
        public string CodigoPais {get;set;}
        public string DatosPuntoServicio {get;set;}
        public string SecuencialTarjeta {get;set;}
        public string CodigoRazon {get;set;}
        public string CodigoActividadNacional {get;set;}
        public string NumeroSesionOBPS {get;set;}
        public string NumeroSesionOrigen {get;set;}
        public string ImporteTasas {get;set;}
        public string MonedaTasas {get;set;}
        public string EntidadAdquiriente {get;set;}
        public string EntidadPresentadora {get;set;}
        public string EntidadAutorizadora {get;set;}
        public string DatosPista2 {get;set;}
        public string NumeroReferencia {get;set;}
        public string NumeroAutorizacion {get;set;}
        public string CodigoRespuesta {get;set;}
        public string IdentificadorATM {get;set;}
        public string IdentificadorComercio {get;set;}
        public string NombreComercio {get;set;}
        public string DatosAdicionales {get;set;}
        public string CodigoMoneda {get;set;}
        public string CodigoMonedaOriginal {get;set;}
        public string BloquePIN {get;set;}
        public string InformacionControlSeguridad {get;set;}
        public string CodigoMonedaConciliacion {get;set;}
        public string DatosEMV {get;set;}
        public string DatosOperacionOriginal {get;set;}
        public string DatosPrivadosOrigen {get;set;}
        public string FechaContable {get;set;}
        public string DatosControlRed {get;set;}
        public string MAC {get;set;}
        public string CodigoAccion {get; set;}
        public string Descripcion {get; set;}

    }

    public class TramasResponseDTO : GenericResponse
    {
        public List<MENSAJE_DESGLOSADO> lstTramas {get; set;}
    }

    public class RespuestasXStatus
    {
        public string? httpStatusCode {get; set;}
        public string? Descripcion {get; set;}
        public int? Cantidad {get; set;}

    } 

    public class RespuestasXCodAccion
    {
        public string? CodigoAccion {get; set;}
        public string? Descripcion {get; set;}
        public string? Aprobada {get; set;}
        public int? Cantidad {get; set;}

    }

    public class DashboardRechazosResponseDTO : GenericResponse
    {
        public List<RespuestasXStatus>? lstStatusCode {get; set;}
        public List<RespuestasXCodAccion>? lstCodigosAccion {get; set;}

    }

    public class EvolutivoItem
    {
        public string? Periodo {get; set;}
        public int? CantidadOperaciones {get; set;}
        public double? ImporteMin {get; set;}
        public double? ImporteMax {get; set;}
        public double? ImportePromedio {get; set;}
        public double? ImporteSuma {get; set;}
    }

    public class EvolutivoResponseDTO : GenericResponse
    {
        public List<EvolutivoItem>? lst {get; set;}
    }

    public class DetalleDatosAdicionales
    {
        public string? Codigo {get; set;}
        public string? Descripcion {get; set;}
        public string? Data {get; set;}
    }

    public class DescripcionPuntoServicio
    {
        public int? Posicion {get; set;}
        public string? ValorTrama {get; set;}
        public string? Description {get; set;}
        public string? Valor {get;set;}
    }


}

