namespace SYSVETE.Autorizacion
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute: Attribute
    {
        //No hace un joraca si tiene este metodo
        //Para autenticar y otros metodos que no necesiten un usuario logeado
    }
}
