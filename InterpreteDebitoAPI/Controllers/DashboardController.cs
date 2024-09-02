using System;
using InterpreteDebitoAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterpreteDebitoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DashboardController : ControllerBase
    {
        [HttpGet, Authorize]
        public ActionResult<DashboardResponseDTO> GetDashboard()
		{
            Random rnd = new Random();
            double MinRandom = rnd.Next(1, 30)/10.0;
            double MaxRandom = MinRandom + rnd.Next(1, 20) / 10.0;

            return Ok(new DashboardResponseDTO()
            {
                Result = 0,
                Mensaje = "Ok",
                tiemposEjecucion = new TiemposEjecucion() { Minimo =MinRandom, Maximo = MaxRandom, Media = (MinRandom + MaxRandom)/2.0, UltimaTransaccion = DateTime.Now },
                LstOperacionesTipo = new List<CantidadxTipo>() {
                    new CantidadxTipo() { CodigoOperacion="00", Operacion="Venta", Cantidad = rnd.Next(60, 100) },
                    new CantidadxTipo() { CodigoOperacion="01", Operacion="Retiro de efectivo", Cantidad = rnd.Next(40, 80) },
                    new CantidadxTipo() { CodigoOperacion="17", Operacion="Compra con cashback", Cantidad = rnd.Next(40, 120) },
                    new CantidadxTipo() { CodigoOperacion="45", Operacion="Pago recurrente", Cantidad = rnd.Next(5, 10) },
                    new CantidadxTipo() { CodigoOperacion="65", Operacion="Recarga telefonica", Cantidad = rnd.Next(10, 20) },
                    new CantidadxTipo() { CodigoOperacion="20", Operacion="Devolucion", Cantidad = rnd.Next(1, 5) },
                    new CantidadxTipo() { CodigoOperacion="25", Operacion="Deposito en efectivo", Cantidad = rnd.Next(50, 70) }
                }

            });

		}
	}
}

