using HuecasAppUsers.Modelo;
using HuecasAppUsers.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HuecasAppUsers.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleEncuestaUser : ContentPage
    {
        public DetalleEncuestaUser(EncuestaM e)
        {
            InitializeComponent();
            BindingContext = new DetalleEncuestaUserVM(Navigation, e);
        }
    }
}