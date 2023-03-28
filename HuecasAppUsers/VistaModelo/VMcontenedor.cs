using Firebase.Auth;
using HuecasAppUsers.Conexiones;
using HuecasAppUsers.Datos;
using HuecasAppUsers.Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace HuecasAppUsers.VistaModelo
{
    public class VMcontenedor : BaseVM
    {
        #region VARIABLES
        string _IdUsuario;
        string _correo;
        string _nombre;
        string _apellido;
        #endregion
        #region CONSTRUCTOR
        public VMcontenedor(INavigation navigation)
        {
            Navigation = navigation;
            obtenerDataUserAsync();
        }
        #endregion
        #region OBJETOS 
        public string Nombre
        {
            get { return _nombre; }
            set { SetValue(ref _nombre, value); }
        }

        public string Apellido
        {
            get { return _apellido; }
            set { SetValue(ref _apellido, value); }
        }
        public string Correo
        {
            get { return _correo; }
            set { SetValue(ref _correo, value); }
        }
        #endregion
        #region PROCESOS
        private  async Task obtenerDataUserAsync()
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
                Nombre = data[0].Nombre;
                //Apellido = data[0].Apellido;
                //Correo = data[0].Correo;
                //Preferences.Remove("MyFirebaseRefreshToken");

            }
            catch (Exception)
            {
                await DisplayAlert("Alerta", "X tu seguridad la sesion se a cerrado", "Ok");

            }
        }
        private async Task Volver()
        {
            await Navigation.PopAsync();
        }
        #endregion

    }

}