using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Negocio
{
    public static class Seguridad
    {
        public static bool sesionActiva(Usuario user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;
            if(usuario != null && usuario.Id != 0)
                return true;
            else
                return false;
        }

        public static bool esAdmin(Usuario user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;
            //return usuario != null ? usuario.Admin : false;

            if (usuario != null && usuario.Admin)
                return true;
            else
                return false;
        }
    }
}
