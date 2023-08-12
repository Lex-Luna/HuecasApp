using HuecasAppUsers.Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HuecasAppUsers.VistaModelo
{
    public class DetalleQuedaVM : BaseVM
    {
        public DetalleQuedaVM(INavigation navigation, UsuarioM p)
        {
            Navigation = navigation;

        }

        string _IdUsuario;

    }
}
