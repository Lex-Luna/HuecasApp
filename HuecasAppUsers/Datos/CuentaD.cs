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

namespace HuecasAppUsers.Datos
{
    public class CuentaD
    {
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
                Application.Current.MainPage = new NavigationPage(new Contenedor());

                await App.Current.MainPage.DisplayAlert("Correcto", "Listo", "OK");
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Datos incorrectos", "OK");

            }
        }
    }
}
