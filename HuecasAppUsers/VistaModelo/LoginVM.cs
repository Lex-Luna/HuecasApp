using HuecasAppUsers.Datos;
using HuecasAppUsers.Vista;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HuecasAppUsers.VistaModelo
{
    public class LoginVM : BaseVM
    {

        #region CONSTRUCTOR
        public LoginVM(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion
        #region VARIABLES
        string correo;
        string contraseña;
        #endregion

        #region OBJETOS 
        public string Correo
        {
            get { return correo; }
            set { SetValue(ref correo, value); }
        }


        public string Contraseña
        {
            get { return contraseña; }
            set { SetValue(ref contraseña, value); }
        }

        #endregion

        #region PROCESOS
        private async Task Volver()
        {
            await Navigation.PopAsync();
        }

        private async void btnCrearCuenta_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CrearCorreo());
        }

        private async void btnIniciar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Correo))
            {
                if (!string.IsNullOrEmpty(Contraseña))
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
                await funcion.ValidCuenta(Correo, Contraseña);


            }
            catch (Exception)
            {

                await DisplayAlert("Alerta", "Correo o contraseñ invlidaa", "OK");
            }
        }
        private async Task btnIniciar()
        {
            if (!string.IsNullOrEmpty(Correo))
            {
                if (!string.IsNullOrEmpty(Contraseña))
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

        private async Task btnCrearCuenta()
        {
            await Navigation.PushAsync(new CrearCorreo());
        }
        #endregion
        #region COMANDOS
        //public ICommand InsertarRecolecoresComand => new Command(async () => await InsertarRecolecoresProces());
        public ICommand btnIniciarcomamd => new Command(async () => await btnIniciar());
        public ICommand btnCrearCuentaComand => new Command(async () => await btnCrearCuenta());

        #endregion
    }
}
