using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ejercicio.Business;
using Ejercicio.Entities;


namespace EjercicioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EjercicioController : ControllerBase
    {
        private readonly EjercicioService _ejercicioService;

        public EjercicioController()
        {
            _ejercicioService = new EjercicioService();
        }

        [HttpPost("upload")]
        //public IActionResult ProcesarMovimientos([FromForm] IFormFile archivo)
        public async Task<IActionResult> UploadMovimientos( IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
            {
                return BadRequest("No se ha proporcionado un archivo o el archivo está vacío.");
            }

            // Validar que el archivo sea CSV
            var extension = Path.GetExtension(archivo.FileName).ToLower();
            if (extension != ".csv")
            {
                return BadRequest("Solo se aceptan archivos CSV.");
            }

            try
            {
                using (var stream = archivo.OpenReadStream())
                {
                    // Procesar el archivo
                    var (total, promedio, cantidad, msj) = _ejercicioService.UploadMovimientos(stream);

                    if (msj != "")
                    {
                        return Ok(new
                        {
                            MSJ = msj
                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            TotalMovimientos = total,
                            PromedioMovimientos = promedio,
                            NumeroMovimientos = cantidad
                        });

                    }
                    
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al procesar el archivo: {ex.Message}");
            }
        }
        /**/

        /*
        [HttpPost("upload")]
        public async Task<IActionResult> UploadMovimientos([FromForm] FormFile archivo)
        {
            //if (archivo == null || archivo.Length == 0)
            //{
            //    return BadRequest("No se ha proporcionado un archivo o el archivo está vacío.");
            //}

            //try
            //{
            //    using (var stream = archivo.OpenReadStream())
            //    {
                    // Procesar el archivo aquí
                    return Ok("Archivo procesado exitosamente.");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            //}
        }
        */





        /*
        [HttpPost("upload")]
        public async Task<IActionResult> ProcesarMovimientos([FromForm] IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
            {
                return BadRequest("No se ha proporcionado un archivo o el archivo está vacío.");
            }

            try
            {
                using (var stream = archivo.OpenReadStream())
                {
                    // Aquí iría la lógica para procesar el archivo...
                    return Ok("Archivo procesado exitosamente.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al procesar el archivo: {ex.Message}");
            }
        }
*/


    }
}
