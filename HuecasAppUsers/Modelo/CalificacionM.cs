using System;
using System.Collections.Generic;
using System.Text;

namespace HuecasAppUsers.Modelo
{
    public class CalificacionM
    {
        public string IdCalificacion { get; set; }
        public string IdLocal { get; set; }
        public string IdPlato { get; set; }
        public int? CalificacionAtencion { get; set; }
        public int? CalificacionComida { get; set; }
        public int? CalificacionLugar { get; set; }
        public bool? Recomendacion { get; set; }

    }
}
