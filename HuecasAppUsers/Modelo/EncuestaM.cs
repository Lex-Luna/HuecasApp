using System;
using System.Collections.Generic;
using System.Text;

namespace HuecasAppUsers.Modelo
{
    public class EncuestaM
    {
        
        public string IdEncuesta { get; set; }
        public bool Estado { get; set; }
        public string NomPlato { get; set; }
        public string NomLocal{ get; set; }
        public int PromCalificacion{ get; set; }
        public string FechaEncuesta { get; set; }
        public DateTime FechaData { get; set; }
        public string IdCalificacion { get; set; }
        public string IdPlatoLocal { get; set; }
        public string IdUsuario { get; set; }
        public string NomUsuario { get; set; }
        
    }
}
