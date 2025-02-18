using System;
using System.IO;
using System.Net;
using System.Reflection;
using InterpreteDebitoAPI;
using InterpreteDebitoAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace InterpreteDebitoAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ConsultarTramasController :ControllerBase
{
    private readonly IAccesoDatos? AD = null;

        public ConsultarTramasController(IAccesoDatos _accesoDatos)
        {
            AD = _accesoDatos;
        }

        [HttpGet]
        public IActionResult ConsultarTiempos(DateTime pFechaIni, DateTime pFechaFin)
		{
            try
            {
                DashboardRequestDTO param = new DashboardRequestDTO()
                {
                    FechaIni = pFechaIni,
                    FechaFin = pFechaFin,
                };

                TramasResponseDTO? ADresponse = AD?.prConsultarTramas(param);

                if (ADresponse.Result == 0)
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    var stream = new MemoryStream();
                    using var package = new ExcelPackage(stream);
                    using ExcelWorkbook workbook = package.Workbook;
                    using ExcelWorksheet worksheet = workbook.Worksheets.Add("Tramas");

                    //Columnas de Trama
                    int ColumNumber  =1;
                    foreach (PropertyInfo propertyInfo in  ADresponse.lstTramas.First().GetType().GetProperties())
                    {
                        worksheet.Cells[1, ColumNumber++].Value = propertyInfo.Name;
                    }
                    
                    //Columnas Desglose de datos adicionales
                    worksheet.Cells[1, ColumNumber++].Value = "28.01 SIA-ID-CD";
                    worksheet.Cells[1, ColumNumber++].Value = "28.15 TransaccionForzada";
                    worksheet.Cells[1, ColumNumber++].Value = "28.31 TokenNegocio";

                    //Columnas de desglose de datos del punto de servicio
                    worksheet.Cells[1, ColumNumber++].Value = "22.XX DatosPuntoServicio";


                    //Data
                    // Data
                    int row = 2;
                    foreach (var Trama in ADresponse.lstTramas)
                    {
                        int Column =1;
                        //Propiedades de la trama
                        foreach (PropertyInfo propertyInfo in  Trama.GetType().GetProperties())
                        {
                            worksheet.Cells[row, Column++].Value = propertyInfo.GetValue(Trama);
                        }

                        //Desglose de datos adicionales
                        worksheet.Cells[row, Column++].Value = Trama.lstDetalleDatosAdicionales?.First(i=>i.Codigo=="01").Data;
                        worksheet.Cells[row, Column++].Value = Trama.lstDetalleDatosAdicionales?.Where(i=>i.Codigo=="15").Count()>0?"VERDADERO":"FALSO";
                        worksheet.Cells[row, Column++].Value = Trama.lstDetalleDatosAdicionales?.First(i=>i.Codigo=="31").Data;

                        string csv = String.Join(";", Trama.lstDetalleDatosPuntoServicio);
                        worksheet.Cells[row, Column++].Value = csv;

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
