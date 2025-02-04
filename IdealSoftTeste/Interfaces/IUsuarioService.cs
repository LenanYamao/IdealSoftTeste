using IdealSoftTeste.Models;

namespace IdealSoftTeste.Interfaces
{
    public interface IUsuarioService
    {
        IEnumerable<Usuario> GetUsuarios ();
        Usuario? GetUsuarioById(int id);
        Usuario CreateUsuario(Usuario usuario);
        bool UpdateUsuario(int id, Usuario usuario);
        bool DeleteUsuario(int id);
    }
}
