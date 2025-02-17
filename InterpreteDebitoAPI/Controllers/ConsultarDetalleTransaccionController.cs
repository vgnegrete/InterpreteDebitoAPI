using System;
using InterpreteDebitoAPI;
using InterpreteDebitoAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterpreteDebitoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsultarDetalleTransaccionController : Controller
    {
        private readonly IAccesoDatos? AD = null;

        public ConsultarDetalleTransaccionController(IAccesoDatos _accesoDatos)
        {
            AD = _accesoDatos;
        }

        [HttpPost, Authorize]
        public IActionResult ConsultarDetalle(DetalleTransaccionRequestDTO param)
        {
            try
            {
                DetalleTransaccionResponseDTO? ADresponse = AD?.prConsultarDetalleTransaccion(param);

                ADresponse.Result = 0;
                ADresponse.Mensaje = "Ok";

                return Ok(ADresponse);
            }
            catch (Exception Ex)
            {
                return StatusCode(500, new DetalleTransaccionResponseDTO() { Result = 1, Mensaje = Ex.Message });
            }
        }
    }
}

