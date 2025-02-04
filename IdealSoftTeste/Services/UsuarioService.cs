using IdealSoftTeste.Data;
using IdealSoftTeste.Interfaces;
using IdealSoftTeste.Models;
using Microsoft.EntityFrameworkCore;

namespace IdealSoftTeste.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly usuarioDbContext _context;

        public UsuarioService(usuarioDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Usuario> GetUsuarios()
        {
            return _context.Usuarios.ToList();
        }
        public Usuario? GetUsuarioById(int id)
        {
            return _context.Usuarios.Find(id);
        }
        public Usuario CreateUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return usuario;
        }
        public bool UpdateUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
                return false;

            _context.Entry(usuario).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Usuarios.Any(e => e.Id == id))
                    return false;
                throw;
            }
        }
        public bool DeleteUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            try
            {
                if (usuario == null)
                    return false;
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
