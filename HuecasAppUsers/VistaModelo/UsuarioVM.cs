using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using HuecasAppUsers.Vista;
using Firebase.Auth;
using HuecasAppUsers.Conexiones;
using HuecasAppUsers.Datos;
using HuecasAppUsers.Modelo;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace HuecasAppUsers.VistaModelo
{
    public class UsuarioVM :BaseVM
    {
        #region CONSTRUCTOR
        public UsuarioVM(INavigation navigation)
        {
            Navigation = navigation;
            obtenerDataUserAsync();
        }
        #endregion
        #region VARIABLES
        string _IdUsuario;
        string _correo;
        string _nombre;
        string identificacion;
        #endregion
        #region OBJETOS 
        public string Nombre
        {
            get { return _nombre; }
            set { SetValue(ref _nombre, value); }
        }
        public string Identificacion
        {
            get { return identificacion; }
            set { SetValue(ref identificacion, value); }
        }
        public string Correo
        {
            get { return _correo; }
            set { SetValue(ref _correo, value); }
        }
    
        #endregion
        #region PROCESOS
        private async Task obtenerDataUserAsync()
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
                //Preferences.Remove("MyFirebaseRefreshToken");  parece que el CPU esta a tope     , v

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

        private async Task IrEncuesta()
        {
            await Navigation.PushAsync(new Encuesta());
        }

        
        #endregion
        #region COMANDOS
        //public ICommand InsertarRecolecoresComand => new Command(async () => await InsertarRecolecoresProces());
        public ICommand Volvercomamd => new Command(async () => await Volver());
        public ICommand IrEncuestacomamd => new Command(async () => await IrEncuesta());
        //public ICommand NavClientesComand => new Command(async () => await NavClientes());

        #endregion
    }
}
