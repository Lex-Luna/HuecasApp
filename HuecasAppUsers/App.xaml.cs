using Firebase.Auth;
using HuecasAppUsers.Conexiones;
using HuecasAppUsers.Datos;
using HuecasAppUsers.Vista;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HuecasAppUsers
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Login());
        }
        public async Task CheckAndRefreshToken()
        {
            var savedToken = Preferences.Get("MyFirebaseRefreshToken", string.Empty);
            if (!string.IsNullOrEmpty(savedToken))
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constantes.WebapyFirebase));
                var auth = JsonConvert.DeserializeObject<FirebaseAuth>(savedToken);
                try
                {
                    var refreshedAuth = await authProvider.RefreshAuthAsync(auth);
                    Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(refreshedAuth));
                    // Redirigir a la página principal
                    Application.Current.MainPage = new NavigationPage(new Contenedor());
                }
                catch (Exception)
                {
                    // Si el token no es válido, redirigir a la página de inicio de sesión
                    Application.Current.MainPage = new NavigationPage(new Login());
                }
            }
            else
            {
                // Si no hay token guardado, redirigir a la página de inicio de sesión
                Application.Current.MainPage = new NavigationPage(new Login());
            }
        }

        protected override void OnStart()
        {
            CheckAndRefreshToken();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
