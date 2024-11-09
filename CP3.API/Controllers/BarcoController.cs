using CP3.Application.Dtos;
using CP3.Application.Services;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CP3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarcoController : ControllerBase
    {
        private readonly IBarcoApplicationService _applicationService;

        public BarcoController(IBarcoApplicationService applicationService)
        {
            _applicationService = applicationService;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BarcoEntity>), 200)]  
        [ProducesResponseType(204)] 
        public IActionResult Get()
        {
            var barcos = _applicationService.ObterTodosBarcos();

            if (barcos != null && barcos.Any())
                return Ok(barcos);  

            return NoContent();  
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BarcoEntity), 200)]  
        [ProducesResponseType(404)]  
        public IActionResult GetPorId(int id)
        {
            var barco = _applicationService.ObterBarcoPorId(id);

            if (barco != null)
                return Ok(barco);  

            return NotFound(new { Message = "Barco não encontrado." });  
        }


        [HttpPost]
        [ProducesResponseType(typeof(BarcoEntity), 201)]  
        [ProducesResponseType(400)]  
        public IActionResult Post([FromBody] BarcoDto entity)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);  

            try
            {
 
                var barco = _applicationService.AdicionarBarco(entity);


                return CreatedAtAction(nameof(GetPorId), new { id = barco.Id }, barco);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message,
                    Status = HttpStatusCode.BadRequest
                });  
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BarcoEntity), 200)]  
        [ProducesResponseType(400)]  
        [ProducesResponseType(404)]  
        public IActionResult Put(int id, [FromBody] BarcoDto entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);  

            try
            {

                var barco = _applicationService.EditarBarco(id, entity);

                return Ok(barco);  
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Barco não encontrado." });  
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message,
                    Status = HttpStatusCode.BadRequest
                });  
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(200)]  
        [ProducesResponseType(404)]  
        public IActionResult Delete(int id)
        {
            try
            {
                var barco = _applicationService.RemoverBarco(id);
                return Ok(barco);  
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Barco não encontrado." });  
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message,
                    Status = HttpStatusCode.BadRequest
                });  
            }
        }
    }
}
