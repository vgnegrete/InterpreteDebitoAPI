using System;
using InterpreteDebitoAPI;
using InterpreteDebitoAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterpreteDebitoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsultarTiemposController : Controller
    {
        private readonly IAccesoDatos? AD = null;

        public ConsultarTiemposController(IAccesoDatos _accesoDatos)
        {
            AD = _accesoDatos;
        }

        [HttpPost]
        public IActionResult ConsultarTiempos(TiemposEjecucionRequestDTO param )
		{
            try
            {
                TiemposEjecucionResponseDTO? ADresponse = AD?.prConsultarTiemposEjecucion(param);
                ADresponse.Result = 0;
                ADresponse.Mensaje = "Ok";

                return Ok( ADresponse);
            }
            catch (Exception Ex)
            {
                return StatusCode(500, new TiemposEjecucionResponseDTO() { Result=1, Mensaje= Ex.Message });
            }
        }
	}
}

