﻿using System;
using InterpreteDebitoAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterpreteDebitoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IAccesoDatos? AD = null;

        public DashboardController(IAccesoDatos _accesoDatos)
        {
            AD = _accesoDatos;
        }

        [HttpPost, Authorize]
        public ActionResult<DashboardResponseDTO> GetDashboard(DashboardRequestDTO pParams)
		{

            try
            {
                DashboardResponseDTO? ADresponse = AD?.prConsultarDashboard(pParams);
                ADresponse.Result = 0;
                ADresponse.Mensaje = "Ok";

                return Ok(ADresponse);
            }
            catch (Exception Ex)
            {
                return StatusCode(500, new DashboardResponseDTO() { Result = 1, Mensaje = Ex.Message });
            }
		}
	}
}

