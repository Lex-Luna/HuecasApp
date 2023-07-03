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
using System.Collections.ObjectModel;
using System.Diagnostics;
using Rg.Plugins.Popup.Extensions;

namespace HuecasAppUsers.VistaModelo
{
    public class UsuarioVM : BaseVM
    {
        /*
        #region CONSTRUCTOR
        public UsuarioVM(INavigation navigation)
        {
            Navigation = navigation;
            Task.Run(async () =>
            {
                await obtenerDataUserAsync();
            }).Wait();
            Task.Run(async () =>
            {
                await MostrarMisEncuestas(IdUsuario);
            }).Wait();

        }
        #endregion
        #region VARIABLES
        string _IdUsuario;
        string _apellido;
        string _correo;
        string _nombre;
        string _contrania;
        string _idAdmin;
        bool _estado;
        int _numEncuesta;
        string IdEncuesta;

        EncuestaM selectEncuesta;
        public List<EncuestaM> listaEncueta = new List<EncuestaM>();


        #endregion
        #region OBJETOS 

        public List<EncuestaM> ListaEncueta { get { return listaEncueta; } set { SetProperty(ref listaEncueta, value); IdEncuesta = selectEncuesta.IdEncuesta; } }
        public EncuestaM SelectEncuesta { get { return selectEncuesta; } set { SetProperty(ref selectEncuesta, value); IdEncuesta = selectEncuesta.IdEncuesta; } }
        public ObservableCollection<EncuestaM> LisEncueta { get; set; }

        public int NumEncuesta
        {
            get { return _numEncuesta; }
            set { SetValue(ref _numEncuesta, value); }
        }
        public string Apellido
        {
            get { return _apellido; }
            set { SetValue(ref _apellido, value); }
        }
        public string Contrania
        {
            get { return _contrania; }
            set { SetValue(ref _contrania, value); }
        }

        public string IdAdmin
        {
            get { return _idAdmin; }
            set { SetValue(ref _idAdmin, value); }
        }

        public bool Estado
        {
            get { return _estado; }
            set { SetValue(ref _estado, value); }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { SetValue(ref _nombre, value); }
        }


        public string IdUsuario
        {
            get { return _IdUsuario; }
            set { SetValue(ref _IdUsuario, value); }
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
                Apellido = data[0].Apellido;
                IdUsuario = data[0].IdUsuario;
                NumEncuesta = data[0].NumEncuesta;
                IdAdmin = data[0].IdAdministrador;
                Contrania = data[0].Contrasenia;
                Estado = data[0].Estado;


                //Preferences.Remove("MyFirebaseRefreshToken");  parece que el CPU esta a tope     , v

            }
            catch (Exception)
            {
                await DisplayAlert("Alerta", "X tu seguridad la sesion se a cerrado", "Ok");

            }
        }
        private async Task IrEncuesta()
        {
            await Navigation.PushAsync(new Encuesta());
        }


        public async Task MostrarMisEncuestas(string idUser)
        {
            EncuestaD f = new EncuestaD();
            var encuestas = await f.MostEncuestaIdUser(idUser);
            LisEncueta = new ObservableCollection<EncuestaM>(encuestas);
        }


        private async Task IrDetalleEncuesta(EncuestaM p)
        {
            try
            {
                await Navigation.PushAsync(new DetalleEncuestaUser(p));
            }
            catch (Exception e)
            {

                Debug.WriteLine("Error: No se pudo tomar el ID " + e);

            }
        }
        private async Task MostrarPerfil()
        {
            await Navigation.PushPopupAsync(new PerfilUser() );
        } 
        #endregion
        #region COMANDOS
        //public ICommand InsertarRecolecoresComand => new Command(async () => await InsertarRecolecoresProces());

        public ICommand IrEncuestacomamd => new Command(async () => await IrEncuesta());
        public ICommand MostrarPerfilcomamd => new Command(async () => await MostrarPerfil());
        public ICommand IrDetalleEncuestaCommand => new Command<EncuestaM>(async (p) => await IrDetalleEncuesta(p));

        #endregion*/
    }   
}

        