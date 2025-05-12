using Microsoft.EntityFrameworkCore;
using SYSVETE.Models;
using SYSVETE.Autorizacion;
using Microsoft.Data.SqlClient;
using SYSVETE.Helpers;

namespace SYSVETE.Services
{

    public interface IUsuario
    {
        Task<AuthenticateResponse> Autenticar(AuthenticateRequest request);
        Task<List<Usuario>> ObtenerUsuarios();
        Task<int> InsertarUsuario(UsuarioNuevo usuario);
        Task<int> EditarUsuario(UsuarioNuevo usuario);
        Task<Usuario> ObtenerUsuarioPorID(int idUsuario);
        Task BorrarUsuario(int idUsuario);

    }
    public class UsuarioService : IUsuario
    {
        private SYSVETEContext _context;
        private IJWTUtils _jWTUtils;

        public UsuarioService(SYSVETEContext context, IJWTUtils jWTUtils)
        {
            _context = context;
            _jWTUtils = jWTUtils;
        }
        public async Task<AuthenticateResponse> Autenticar(AuthenticateRequest request)
        {
            try
            {
                var usuario = await _context.Usuarios
                    .Where(u => u.Alias == request.Usuario
                        && u.Activo)
                    .FirstOrDefaultAsync();
                if (usuario == null)
                {
                    return null;
                }

                //if (!BCrypt.Net.BCrypt.Verify(request.Contrasena, usuario.Contrasena))
                //{
                //    return null;
                //}

                var decode = Serializador.DecodeFrom64(usuario.Contrasena);

                if (decode != request.Contrasena)
                {
                    return null;
                }

                var token = _jWTUtils.GenerarToken(usuario);

                return new AuthenticateResponse(usuario, token);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Usuario> ObtenerUsuarioPorID(int idUsuario)
        {
            try
            {
                var usuario = await _context.Usuarios
                    .Where(u => u.IdUsuario == idUsuario && u.Activo)
                    .FirstOrDefaultAsync();
                return usuario;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<Usuario>> ObtenerUsuarios()
        {
            try
            {
                var usuarios = await _context.Usuarios.Include(u => u.IdRolNavigation)
                    .Where(u => u.Activo)
                    .ToListAsync();
                return usuarios;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> InsertarUsuario(UsuarioNuevo usuario)
        {
            try
            {
                if (string.IsNullOrEmpty(usuario.Alias) || string.IsNullOrWhiteSpace(usuario.Alias))
                {
                    throw new Exception("Campo Alias no puede estar vacio!");
                }

                if (string.IsNullOrEmpty(usuario.Nombre) || string.IsNullOrWhiteSpace(usuario.Nombre))
                {
                    throw new Exception("Campo Nombre no puede estar vacio!");
                }

                if (string.IsNullOrEmpty(usuario.Contrasena) || string.IsNullOrWhiteSpace(usuario.Contrasena))
                {
                    throw new Exception("Campo Contraseña no puede estar vacio!");
                }

                if (usuario.IdRol == 0)
                {
                    throw new Exception("Campo Rol no puede estar vacio!");
                }

                //var encriptado = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena);

                var encriptado = Serializador.EncodePasswordToBase64(usuario.Contrasena);

                Usuario nuevoUsuario = new Usuario()
                {
                    Alias = usuario.Alias,
                    Nombre = usuario.Nombre,
                    Contrasena = encriptado,
                    Activo = usuario.Activo,
                    IdRol = usuario.IdRol
                };

                _context.Usuarios.Add(nuevoUsuario);
                await _context.SaveChangesAsync();
                var nuevoid = nuevoUsuario.IdUsuario;
                return nuevoid;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task BorrarUsuario(int idUsuario)
        {
            using (var scope = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.verificar_dependencia_registro @id, @tabla",
                        new SqlParameter("@id", idUsuario),
                        new SqlParameter("@tabla", "Usuario"));

                    var rol = await _context.Usuarios.Where(r => r.IdUsuario == idUsuario)
                        .SingleOrDefaultAsync();

                    if (rol == null)
                    {
                        throw new Exception("No existe el rol!");
                    }

                    rol.Borrado = true;

                    await _context.SaveChangesAsync();
                    await scope.CommitAsync();

                }
                catch (Exception)
                {
                    await scope.RollbackAsync();
                    throw;
                }

            }
        }
        public async Task<int> EditarUsuario(UsuarioNuevo usuario)
        {
            try
            {
                if (string.IsNullOrEmpty(usuario.Alias) || string.IsNullOrWhiteSpace(usuario.Alias))
                {
                    throw new Exception("Campo Alias no puede estar vacio!");
                }

                if (string.IsNullOrEmpty(usuario.Nombre) || string.IsNullOrWhiteSpace(usuario.Nombre))
                {
                    throw new Exception("Campo Nombre no puede estar vacio!");
                }

                if (string.IsNullOrEmpty(usuario.Contrasena) || string.IsNullOrWhiteSpace(usuario.Contrasena))
                {
                    throw new Exception("Campo Contraseña no puede estar vacio!");
                }

                if (usuario.IdRol == 0)
                {
                    throw new Exception("Campo Rol no puede estar vacio!");
                }

                var usuarioAEditar = await _context.Usuarios
                    .Where(e => e.IdUsuario == usuario.IdUsuario)
                    .SingleOrDefaultAsync();

                if (usuarioAEditar == null)
                {
                    throw new Exception("No existe el usuario buscado!");
                }

                var encriptado = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena);

                Usuario dto = new Usuario()
                {
                    Alias = usuario.Alias,
                    Nombre = usuario.Nombre,
                    Contrasena = encriptado,
                    Activo = usuario.Activo,
                    IdRol = usuario.IdRol
                };

                usuarioAEditar.Alias = dto.Alias;
                usuarioAEditar.Nombre = dto.Nombre;
                usuarioAEditar.Activo = dto.Activo;
                usuarioAEditar.Contrasena = dto.Contrasena;
                usuarioAEditar.IdRol = dto.IdRol;

                await _context.SaveChangesAsync();

                return usuarioAEditar.IdUsuario;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
