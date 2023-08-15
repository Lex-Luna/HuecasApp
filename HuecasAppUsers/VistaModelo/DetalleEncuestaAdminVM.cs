using Firebase.Auth;
using HuecasAppUsers.Conexiones;
using HuecasAppUsers.Datos;
using HuecasAppUsers.Modelo;
using HuecasAppUsers.Vista;
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
    public class DetalleEncuestaAdminVM : BaseVM
    {
        public DetalleEncuestaAdminVM(INavigation navigation, EncuestaM e)
        {
            Navigation = navigation;
            IdEncuesta = e.IdEncuesta;
            IdUsuario = e.IdUsuario;
            IdPlato = e.IdPlato;
            IdLocal = e.IdLocal;
            IdCalificacion = e.IdCalificacion;


            Task.Run(async () =>
            {
                await MostrarLocalId(IdLocal);

            }).Wait();

            Task.Run(async () =>
            {
                await MostrarPlatoId(IdPlato);

            }).Wait();

            Task.Run(async () =>
            {
                await MostrarCalificacionId(IdCalificacion);

            }).Wait();



            VanearEncuestaCommand = new Command(async () => await VanearEncuesta(IdEncuesta));

        }
        #region Variables
        string _IdUsuario;
        string _apellido;
        string _correo;
        string _nombre;
        string _contrania;
        string _idAdmin;
        string fotoUsuario;
        bool _estado;
        int _numEncuesta;
        //string video;


        #endregion
        #region Objetos

        public ObservableCollection<CalificacionM> LisCalificacion { get; set; }
        public ObservableCollection<PlatoM> LisPlato { get; set; }
        public ObservableCollection<LocalM> LisLocal { get; set; }
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
        public string FotoUsuario
        {
            get { return fotoUsuario; }
            set { SetValue(ref fotoUsuario, value); }
        }

        public string IdEncuesta { get; set; }
        public string IdCalificacion { get; set; }
        public string IdPlato { get; set; }
        public string IdLocal { get; set; }

        #endregion
        #region Procesos
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
                //IdUsuario = data[0].IdUsuario;
                NumEncuesta = data[0].NumEncuesta;
                IdAdmin = data[0].IdAdministrador;
                Contrania = data[0].Contrasenia;
                Estado = data[0].Estado;

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
        public async Task MostrarLocalId(string Id)
        {
            try
            {
                LocalD f = new LocalD();
                var local = await f.MostLocalXId(Id);
                LisLocal = new ObservableCollection<LocalM>(local);


            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: No se pudo consulta la tabla Calificacion " + e);
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


        public async Task MostrarPlatoId(string Id)
        {
            try
            {
                PlatoD f = new PlatoD();
                var plato = await f.MostPlatoXId(Id);
                LisPlato = new ObservableCollection<PlatoM>(plato);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: No se pudo consulta la tabla Calificacion " + e);
            }

        }
        async Task VanearEncuesta(string id)
        {
            await NumEnncuestaVaneada(IdUsuario);
            try
            {
                var f = new EncuestaD();
                var p = new EncuestaM { IdEncuesta = id };
                await f.EncuestaVaneada(p);
                await DisplayAlert("Alerta", "Encuesta Baneada Correctamente", "OK");
                await Navigation.PushAsync(new MenuAdmin());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private async Task NumEnncuestaVaneada(string id)
        {
            try
            {
                var f = new UsuarioD();
                var p = new UsuarioM { IdUsuario = id };


                await f.AddNumVaneoEncuesta(p);
            }
            catch (Exception e)
            {

                throw e;
            }

        }



        #endregion
        #region Comandos

        public ICommand Volvercomamd => new Command(async () => await Volver());
        public Command VanearEncuestaCommand { get; }

        #endregion
    }
}
