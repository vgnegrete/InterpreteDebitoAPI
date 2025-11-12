using System;
using System.IO;
using System.Net;
using InterpreteDebitoAPI;
using InterpreteDebitoAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

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

        [HttpGet]
        public IActionResult ConsultarTiempos(DateTime pFechaIni, DateTime pFechaFin, string? pMTI, string? pCodigoOperacion, string? pNumeroAutorizacion)
		{
            try
            {
                TiemposEjecucionRequestDTO param = new TiemposEjecucionRequestDTO()
                {
                    FechaIni = pFechaIni,
                    FechaFin = pFechaFin,
                    CodigoOperacion = pCodigoOperacion,
                    MTI = pMTI,
                    NumeroAutorizacion = pNumeroAutorizacion
                };

                TiemposEjecucionResponseDTO? ADresponse = AD?.prConsultarTiemposEjecucion(param);

                if (ADresponse.Result == 0)
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    var stream = new MemoryStream();
                    using var package = new ExcelPackage(stream);
                    using ExcelWorkbook workbook = package.Workbook;
                    using ExcelWorksheet worksheet = workbook.Worksheets.Add("Operaciones");

                    //Headers
                    worksheet.Cells[1, 1].Value = "Fecha recepción";
                    worksheet.Cells[1, 2].Value = "MTI";
                    worksheet.Cells[1, 3].Value = "Codigo operacion";
                    worksheet.Cells[1, 4].Value = "Numero aut";
                    worksheet.Cells[1, 5].Value = "Tarjeta";
                    worksheet.Cells[1, 6].Value = "Socio";
                    worksheet.Cells[1, 7].Value = "Monto";
                    worksheet.Cells[1, 8].Value = "Monto Autorizado";
                    worksheet.Cells[1, 9].Value = "Accion";
                    worksheet.Cells[1, 10].Value = "Tiempo interp ";
                    worksheet.Cells[1, 11].Value = "Tiempo ejecucion";
                    worksheet.Cells[1, 12].Value = "Tiempo respuesta";


                    //Data
                    // Data
                    int row = 2;
                    foreach (var Trama in ADresponse.Lst)
                    {
                        worksheet.Cells[row, 1].Value = Trama.FechaRecepcion;
                        worksheet.Cells[row, 2].Value = Trama.MTI;
                        worksheet.Cells[row, 3].Value = Trama.CodigoOperacion;
                        worksheet.Cells[row, 4].Value = Trama.NumeroAutorizacion;
                        worksheet.Cells[row, 5].Value = Trama.NumeroTarjeta;
                        worksheet.Cells[row, 6].Value = Trama.NumSocio;
                        worksheet.Cells[row, 7].Value = Trama.Monto;
                        worksheet.Cells[row, 8].Value = Trama.MontoAutoriado;
                        worksheet.Cells[row, 9].Value = Trama.Accion;
                        worksheet.Cells[row, 10].Value = Trama.TiempoDesglose;
                        worksheet.Cells[row, 11].Value = Trama.TiempoEjecucion;
                        worksheet.Cells[row, 12].Value = Trama.TiempoRespuesta;


                        row++;
                    }

                    package.Save();
                    stream.Position = 0;

                    var result = new FileStreamResult(stream, "application/ms-excel") { FileDownloadName = $"Tramas.xlsx" };

                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return result;

                }

                return NotFound();
                
            }
            catch (Exception Ex)
            {
                return StatusCode(500, new TiemposEjecucionResponseDTO() { Result = 1, Mensaje = Ex.Message });
            }
        }
	}
}

