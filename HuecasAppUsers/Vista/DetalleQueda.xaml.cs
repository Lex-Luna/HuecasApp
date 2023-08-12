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
    public partial class DetalleQueda : ContentPage
    {
        public DetalleQueda(UsuarioM u)
        {
            InitializeComponent();
            BindingContext = new DetalleQuedaVM(Navigation, u);
        }

        private void Atras_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}