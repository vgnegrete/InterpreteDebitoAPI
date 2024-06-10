using System;
namespace InterpreteDebitoAPI.Entities
{
	public class GenericResponse
	{
		public int Result { get; set; }
        public string? Mensaje { get; set; }
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
		public double Min { get; set; }
        public double Max { get; set; }
        public double Promedio { get; set; }
		public DateTime UltimaTransaccion { get; set; }
    }

	public class CantidadxTipo
	{
		public string? CodigoOperacion { get; set; }
		public int Cantidad { get; set; }
	}

	public class DashboardResponseDTO : GenericResponse
	{
		public TiemposEjecucion? tiemposEjecucion { get; set; }
		public List<CantidadxTipo>? LstOperacionesTipo {get; set;}
	}

}

