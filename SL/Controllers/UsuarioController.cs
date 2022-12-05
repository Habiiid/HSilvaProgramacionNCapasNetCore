using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // GET: api/<UsuarioController>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result result = BL.Usuario.GetAll(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
            //return new string[] { "Leonardo", "Isaac","Jesus" };
        }

        [HttpPost("GetAll")]
        public IActionResult GetAll(string? nombre, string? ap, string? am)
        {

            ML.Usuario usuario = new ML.Usuario();

            //alumno.Nombre = nombre;
            usuario.Nombre = (nombre == null) ? "" : nombre;
            usuario.ApellidoPaterno = (ap == null) ? "" : ap;
            usuario.ApellidoMaterno = (am == null) ? "" : am;

            usuario.Rol = new ML.Rol();
            ML.Result result = BL.Usuario.GetAll(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
            //return new string[] { "Leonardo", "Isaac","Jesus" };
        }

        // GET api/<UsuarioController>/5
        [HttpGet("GetById/{idUsuario}")]
        public IActionResult GetById(int idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result result = BL.Usuario.GetById(idUsuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<UsuarioController>
        [HttpPost("Add")]
        public IActionResult Add([FromBody] ML.Usuario usuario)
        {

            ML.Result result = BL.Usuario.Add(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("Update/{idUsuario}")]
        public IActionResult Update([FromBody] ML.Usuario usuario)
        {
            //ML.Usuario usuario = new ML.Usuario();
            //  usuario.Rol = new ML.Rol();

            ML.Result result = BL.Usuario.Update(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("Delete/{idUsuario}")]
        public IActionResult Delete(int idUsuario)
        {
            if (idUsuario > 0)
            {
                ML.Usuario usuario = new ML.Usuario();
                //usuario.Rol = new ML.Rol();
                ML.Result result = BL.Usuario.Delete(idUsuario);

                if (result.Correct)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}