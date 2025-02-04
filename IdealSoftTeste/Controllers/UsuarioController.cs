using IdealSoftTeste.Interfaces;
using IdealSoftTeste.Models;
using Microsoft.AspNetCore.Mvc;

namespace IdealSoftTeste.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> GetUsuarios()
        {
            IEnumerable<Usuario> usuarios = _usuarioService.GetUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public ActionResult<Usuario> GetUsuario(int id)
        {
            var item = _usuarioService.GetUsuarioById(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public ActionResult<Usuario> PostUsuario(Usuario item)
        {
            var createdItem = _usuarioService.CreateUsuario(item);
            return CreatedAtAction(nameof(GetUsuario), new { id = createdItem.Id }, createdItem);
        }

        [HttpPut("{id}")]
        public ActionResult PutUsuario(int id, Usuario item)
        {
            if (id != item.Id)
                return BadRequest();

            var updated = _usuarioService.UpdateUsuario(id, item);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUsuario(int id)
        {
            var deleted = _usuarioService.DeleteUsuario(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
