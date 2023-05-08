using Firebase.Auth;
using HuecasAppUsers.Conexiones;
using HuecasAppUsers.Datos;
using HuecasAppUsers.Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HuecasAppUsers.VistaModelo
{
    public class DetalleEncuestaUserVM : BaseVM
    {
        #region CONSTRUCTOR
        public DetalleEncuestaUserVM(INavigation navigation, EncuestaM e)
        {
            Navigation = navigation;
            IdCalificacion = e.IdCalificacion;

            Task.Run(async () =>
            {
                await MostrarCalificacionId(IdCalificacion);

            }).Wait();
            Task.Run(async () =>
            {
                await obtenerDataUserAsync();
            }).Wait();
        }

        #endregion
        #region Variables
        string _IdUsuario;
        string _apellido;
        string _correo;
        string _nombre;
        string _contrania;
        string _idAdmin;
        bool _estado;
        int _numEncuesta;
        string IdEncuesta;


        #endregion
        #region Objetos

        public ObservableCollection<CalificacionM> LisCalificacion { get; set; }
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
        #region Procesos
        public string IdCalificacion { get; set; }
        private async Task Volver()
        {
            await Navigation.PopAsync();
        }
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
        public async Task MostrarCalificacionId(string Id)
        {
            try
            {
                CalificacionD f = new CalificacionD();
                var calificacion = await f.MostCalificacionXId(Id);
                LisCalificacion = new ObservableCollection<CalificacionM>(calificacion);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: No se pudo consulta la tabla Calificacion " + e);
            }

        }



        #endregion
        #region Comandos
        //public ICommand InsertarRecolecoresComand => new Command(async () => await InsertarRecolecoresProces());
        public ICommand Volvercomamd => new Command(async () => await Volver());
        //public ICommand NavClientesComand => new Command(async () => await NavClientes());

        #endregion

    }
}
