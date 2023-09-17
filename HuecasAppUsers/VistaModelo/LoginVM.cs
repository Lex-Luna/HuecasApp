﻿using Firebase.Auth;
using HuecasAppUsers.Conexiones;
using HuecasAppUsers.Datos;
using HuecasAppUsers.Vista;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HuecasAppUsers.VistaModelo
{
    public class LoginVM : BaseVM
    {

        #region CONSTRUCTOR
        public LoginVM(INavigation navigation)
        {
            Navigation = navigation;
            Task.Run(async () =>
            {
                await VistaPrincipal();
            }).Wait();
            
        }
        #endregion
        #region VARIABLES
        string correo;
        string contraseña;
        bool panelLogin;
        bool recuperarContrasenia;
        #endregion

        #region OBJETOS 
        public bool PanelLogin
        {
            get { return panelLogin; }
            set { SetValue(ref panelLogin, value); }
        }
        public bool RecuperarContrasenia
        {
            get { return recuperarContrasenia; }
            set { SetValue(ref recuperarContrasenia, value); }
        }
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
       
        private async Task ResetPasswordAsync()
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constantes.WebapyFirebase));
                await authProvider.SendPasswordResetEmailAsync(Correo);
                await DisplayAlert("Alerta", "Revisa tu Email y cambia de contraseña", "Ok");
                await VistaPrincipal();
            }
            catch (Exception e)
            {
                await DisplayAlert("Alerta", "El usuario no existe en nuestra base de datos", "Ok");
                throw e;
            }
            
        }
        async Task VistaPrincipal()
        {
            PanelLogin = true;
            RecuperarContrasenia = false;
            await Task.CompletedTask;
        }
        async Task VistaRecuperacion()
        {
            PanelLogin = false;
            RecuperarContrasenia = true;
            await Task.CompletedTask;
        }

        private async void btnCrearCuenta_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CrearCorreo());
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

                await DisplayAlert("Alerta", "Correo o contraseña invlidaa", "OK");
            }
        }
        private async Task btnIniciar()
        {
            try
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
            catch (Exception exs)
            {

                throw exs;
            }
            
        }

        private async Task btnCrearCuenta()
        {
            await Navigation.PushAsync(new CrearCorreo());
        }
        #endregion
        #region COMANDOS
        
        public ICommand btnIniciarcomamd => new Command(async () => await btnIniciar());
        public ICommand btnCrearCuentaComand => new Command(async () => await btnCrearCuenta());
        public ICommand RecuperarCuentaComand => new Command(async () => await ResetPasswordAsync());
        public ICommand VistaPrincipalComand => new Command(async () =>  await VistaPrincipal());
        public ICommand VistaRecuperacionComand => new Command(async () =>  await VistaRecuperacion());

        #endregion
    }
}
