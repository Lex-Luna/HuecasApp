using System;
using System.Collections.Generic;
using System.Text;

namespace HuecasAppUsers.Modelo
{
     public class UsuarioM
    {
        public string Apellido { get; set; }
        public string Contrasenia { get; set; }
        public string Correo { get; set; }
        public bool Estado { get; set; }
        public string Nombre { get; set; }
        public string IdUsuario { get; set; }
        public string IdAdministrador { get; set; }
    }
}
