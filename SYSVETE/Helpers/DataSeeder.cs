using SYSVETE.Models;

namespace SYSVETE.Helpers
{
    public class DataSeeder
    {
        private readonly SYSVETEContext _context;
        private readonly string constrasena = "AdminPa$$word";
        public DataSeeder(SYSVETEContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (!_context.Roles.Any())
            {
                var roles = new List<Rol>()
                {
                    new Rol() {
                        Codigo = 1,
                        Descripcion = "Administrador",
                        Activo = true,
                    },
                    new Rol() {
                        Codigo = 2,
                        Descripcion = "Veterinario",
                        Activo = true,
                    },
                    new Rol() {
                        Codigo = 3,
                        Descripcion = "Empleado",
                        Activo = true,
                    },
                };
                _context.Roles.AddRange(roles);
                _context.SaveChanges();
            }

            if (!_context.Usuarios.Any())
            {
                var admin = new Usuario()
                {
                    Nombre = "Administrador",
                    Alias = "admin",
                    Contrasena = BCrypt.Net.BCrypt.HashPassword(constrasena),
                    Activo = true,
                };

                var idRolAdmin = _context.Roles
                    .Where(rol => rol.Codigo == 1 && rol.Activo)
                    .Select(rol => rol.IdRol)
                    .Single();
                admin.IdRol = idRolAdmin;

                _context.Usuarios.Add(admin);
                _context.SaveChanges();
            }
        }
    }
}
