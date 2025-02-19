using InterpreteDebitoAPI;
using InterpreteDebitoAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterpreteDebitoAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ConsultarDetalleTransaccionOriginalController : ControllerBase
{
    private readonly IAccesoDatos? AD = null;

    public ConsultarDetalleTransaccionOriginalController(IAccesoDatos _accesoDatos)
    {
        AD = _accesoDatos;
    }

    [HttpPost]
    public IActionResult ConsultarDetalle(DetalleTransaccionOriginalRequestDTO param)
    {
        try
        {
            List<DetalleOperacion> Lst = AD?.prConsultarDetalleTransaccionOriginal(param);

            return Ok(new {Lst, Result = 0, Mensaje = "Ok" } );
        }
        catch (Exception Ex)
        {
            return StatusCode(500,  new { Lst=new List<DetalleOperacion>(), Result = 1, Mensaje = Ex.Message });
        }
    }

}
