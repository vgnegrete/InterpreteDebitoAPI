using InterpreteDebitoAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterpreteDebitoAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class EvolutivoController: Controller
{
    private readonly IAccesoDatos? AD = null;

    public EvolutivoController(IAccesoDatos _accesoDatos)
    {
        AD = _accesoDatos;
    }

    [HttpPost, Authorize]
    public ActionResult<EvolutivoResponseDTO> GetEvolutivo(EvolutivoRequestDTO pParams)
    {
        try
        {
            EvolutivoResponseDTO? ADresponse = AD?.prConsultarEvolutivo(pParams);
            ADresponse.Result = 0;
            ADresponse.Mensaje = "Ok";

            return Ok(ADresponse);
        }
        catch (Exception Ex)
        {
            return StatusCode(500, new EvolutivoResponseDTO() { Result = 1, Mensaje = Ex.Message });
        }
    }


}
