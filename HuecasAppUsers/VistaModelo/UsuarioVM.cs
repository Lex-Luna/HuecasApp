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

namespace HuecasAppUsers.VistaModelo
{
    public class UsuarioVM :BaseVM
    {
        #region CONSTRUCTOR
        public UsuarioVM(INavigation navigation)
        {
            Navigation = navigation;
            obtenerDataUserAsync();
            
            Task.Run(async () =>
            {
                await MostrarMisEncuestas(IdUsuario);
            }).Wait();

        }
        #endregion
        #region VARIABLES
        string _IdUsuario;
        string _correo;
        string _nombre;
        int _numEncuesta=0;

        public List<EncuestaM> listaEncueta= new List<EncuestaM>();


        #endregion
        #region OBJETOS 

        public ObservableCollection<EncuestaM> LisEncueta { get; set; }


        public List<EncuestaM> ListaEncueta
        {
            get { return listaEncueta; }
            set
            {
                SetValue(ref listaEncueta, value);
            }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { SetValue(ref _nombre, value); }
        }

        public int NumEncuesta
        {
            get { return _numEncuesta; }
            set { SetValue(ref _numEncuesta, value); }
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
                IdUsuario = data[0].IdUsuario;
                NumEncuesta = data[0].NumEncuesta;                
                //Preferences.Remove("MyFirebaseRefreshToken");  parece que el CPU esta a tope     , v

            }
            catch (Exception)
            {
                await DisplayAlert("Alerta", "X tu seguridad la sesion se a cerrado", "Ok");

            }
        }


        public async Task MostrarMisEncuestas(string idUser)
        {
            //UsuarioM p = new UsuarioM();
            EncuestaD f = new EncuestaD();
            var encuestas = await f.MostEncuestaIdUser(idUser);
            LisEncueta = new ObservableCollection<EncuestaM>(encuestas);
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
