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
            VIstaPrincipal();

        }

        private void VIstaPrincipal()
        {
            MenuBotones.IsVisible = true;
            InfoCalificacion.IsVisible = false;
            InfoLocal.IsVisible = false;
            InfoPlato.IsVisible = false;
        }

        private void btnLocal_Clicked(object sender, EventArgs e)
        {
            MenuBotones.IsVisible = false;
            InfoCalificacion.IsVisible = false;
            InfoLocal.IsVisible = true;
            InfoPlato.IsVisible = false;
        }

        private void btnComida_Clicked(object sender, EventArgs e)
        {
            MenuBotones.IsVisible = false;
            InfoCalificacion.IsVisible = false;
            InfoLocal.IsVisible = false;
            InfoPlato.IsVisible = true;
        }

        private void btnCalificacion_Clicked(object sender, EventArgs e)
        {
            MenuBotones.IsVisible = false;
            InfoCalificacion.IsVisible = true;
            InfoLocal.IsVisible = false;
            InfoPlato.IsVisible = false;
        }

        private void btnCerrar1_Clicked(object sender, EventArgs e)
        {
            VIstaPrincipal();
        }
        private void btnCerrar2_Clicked(object sender, EventArgs e)
        {
            VIstaPrincipal();
        }
        private void btnCerrar3_Clicked(object sender, EventArgs e)
        {
            VIstaPrincipal();
        }

        private void Atras_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}