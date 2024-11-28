using Firebase.Auth;
using HuecasAppUsers.Conexiones;
using HuecasAppUsers.Vista;
using Newtonsoft.Json;
using System;
//using System.Collections.Generic;
//using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;


//using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
//using Firebase.Auth;
//using HuecasAppUsers.Conexiones;
//using Newtonsoft.Json;
//using Xamarin.Essentials;
//using System;
//using Firebase.Database.Query;
using HuecasAppUsers.Modelo;
//using System.ComponentModel;
//using Xamarin.Forms.Internals;

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
                
            }
        }
        public bool Admin {
            get => _admin;
            set
            {
                _admin = value;
                
            }
        }

        
       
        public async Task CrearCuenta(string correo, string pass)
        {
            try
            {

                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constantes.WebapyFirebase));
                //await authProvider.CreateUserWithEmailAndPasswordAsync(correo, pass);
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(correo, pass);
                Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(auth));

            }
            catch (Exception ex)
            {

                throw ex;
            }

          }

        public async Task ValidCuenta(string correo, string pass)
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constantes.WebapyFirebase));
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(correo, pass);
                /*vamos a generar un nuevo token serializado*/
                var serializartoken = JsonConvert.SerializeObject(auth);
                Preferences.Set("MyFirebaseRefreshToken", serializartoken);
                var guardarId = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
                //await App.Current.MainPage
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
            }

        }
        
    }
}
