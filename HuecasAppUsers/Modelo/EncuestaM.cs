﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HuecasAppUsers.Modelo
{
    public class EncuestaM
    {
        
        public string IdEncuesta { get; set; }
        public bool Estado { get; set; }
        public string FechaEncuesta { get; set; }
        public string IdCalificacion { get; set; }
        public string IdPlatoLocal { get; set; }
        public string IdUsuario { get; set; }
    }
}
