using Firebase.Auth;
using HuecasAppUsers.Conexiones;
using HuecasAppUsers.Vista;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Firebase.Auth;
using HuecasAppUsers.Conexiones;
using Newtonsoft.Json;
using Xamarin.Essentials;
using System;
using Firebase.Database.Query;
using HuecasAppUsers.Modelo;
using System.ComponentModel;
using Xamarin.Forms.Internals;

namespace HuecasAppUsers.Datos
{
    public class CuentaD
    {
        string _correo;
        bool _admin;
        public string Correo
        {
            get => _correo;
            set
            {
                _correo = value;
                //OnPropertyChanged();
            }
        }
        public bool Admin {
            get => _admin;
            set
            {
                _admin = value;
                //OnPropertyChanged();
            }
        }

        /*private void OnPropertyChanged()
        {
            base.OnPropertyChanged(propertyName);
            foreach (var logicalChildren in ChildrenNotDrawnByThisElement)
            {
                if (logicalChildren is IPropertyPropagationController controller)
                    PropertyPropagationExtensions.PropagatePropertyChanged(propertyName, this, new[] { logicalChildren });
            }

            if (_effects == null || _effects.Count == 0)
                return;

            var args = new PropertyChangedEventArgs(propertyName);
            foreach (Effect effect in _effects)
            {
                effect?.SendOnElementPropertyChanged(args);
            }
        }*/

        public async Task CrearCuenta(string correo, string pass)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constantes.WebapyFirebase));
            await authProvider.CreateUserWithEmailAndPasswordAsync(correo, pass);

        }
        public async Task ValidCuenta(string correo, string pass)
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constantes.WebapyFirebase));
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(correo, pass);
                var serializartoken = JsonConvert.SerializeObject(auth);
                Preferences.Set("MyFirebaseRefreshToken", serializartoken);
                var guardarId = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
                var refrescarCOntenido = await authProvider.RefreshAuthAsync(guardarId);
                Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(refrescarCOntenido));
                Correo = guardarId.User.Email;
                var f = new UsuarioD();
                var p = new UsuarioM();
                p.Correo = Correo;
                var data = await f.MostUsuarioXcorreo(p);
                Admin = data[0].Admin;
                if (Admin==false)
                {
                    Application.Current.MainPage = new NavigationPage(new Contenedor());
                }
                else
                {
                    Application.Current.MainPage = new NavigationPage(new MenuAdmin());
                }

            }
            catch (Exception er)
            {
                throw er;
                await App.Current.MainPage.DisplayAlert("Error", "Datos incorrectos", "OK");

            }

        }
        /*private async Task obtenerDataUserAsync()
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constantes.WebapyFirebase));
                var guardarId = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
                var refrescarCOntenido = await authProvider.RefreshAuthAsync(guardarId);
                Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(refrescarCOntenido));
                Correo = guardarId.User.Email;
                var f = new UsuarioD();
                var p = new UsuarioM();
                p.Correo = Correo;
                var data = await f.MostUsuarioXcorreo(p);
                Admin = data[0].Admin;
                /*Apellido = data[0].Apellido;
                IdUsuario = data[0].IdUsuario;
                NumEncuesta = data[0].NumEncuesta;
                IdAdmin = data[0].IdAdministrador;
                Contrania = data[0].Contrasenia;
                Estado = data[0].Estado;
                FotoUsuario = data[0].FotoUsuario;




            }
            catch (Exception e)
            {
                throw e;

            }
        }*/
    }
}
