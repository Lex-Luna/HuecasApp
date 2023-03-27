using HuecasAppUsers.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using HuecasAppUsers.Datos;

namespace HuecasAppUsers.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            //LoginOk();
            InitializeComponent();
            BindingContext = new LoginVM(Navigation);
        }
        
        /*private async void btnCrearCuenta_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CrearCorreo());
        }

        private async void btnIniciar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtcorreo.Text))
            {
                if (!string.IsNullOrEmpty(txtcontraseña.Text))
                {
                    await ValidarDatos();
                }
                else
                {
                    await DisplayAlert("Alerta", "Ingrese su contraseña", "OK");
                }
            }
            else
            {
                await DisplayAlert("Alerta", "Ingrese su correo", "OK");
            }
        }
        public async Task ValidarDatos()
        {
            try
            {
                var funcion = new CuentaD();
                await funcion.ValidCuenta(txtcorreo.Text, txtcontraseña.Text);


            }
            catch (Exception)
            {

                await DisplayAlert("Alerta", "Correo o contraseñ invlidaa", "OK");
            }
        }*/
    }
}