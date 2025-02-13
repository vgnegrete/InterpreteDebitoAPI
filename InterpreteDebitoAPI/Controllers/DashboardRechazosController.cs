using InterpreteDebitoAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterpreteDebitoAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class DashboardRechazosController : Controller
{
    private readonly IAccesoDatos? AD = null;

    public DashboardRechazosController(IAccesoDatos _accesoDatos)
    {
        AD = _accesoDatos;
    }

    [HttpPost, Authorize]
    public ActionResult<DashboardRechazosResponseDTO> GetDashboardRechazos(DashboardRequestDTO pParams)
    {
        try
        {
            DashboardRechazosResponseDTO? ADresponse = AD?.prConsultarDashboardRechazos(pParams);
            ADresponse.Result = 0;
            ADresponse.Mensaje = "Ok";

            return Ok(ADresponse);
        }
        catch (Exception Ex)
        {
            return StatusCode(500, new DashboardRechazosResponseDTO() { Result = 1, Mensaje = Ex.Message });
        }
    }


}
