using Firebase.Auth;
using HuecasAppUsers.Conexiones;
using HuecasAppUsers.Datos;
using HuecasAppUsers.Modelo;
using HuecasAppUsers.Vista;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HuecasAppUsers.VistaModelo
{
    public class ComunityVM : BaseVM
    {
        #region CONSTRUCTOR
        public ComunityVM(INavigation navigation)
        {
            Navigation = navigation;
            Task.Run(async () =>
            {
                await obtenerDataUserAsync();
            }).Wait();
            Task.Run(async () =>
            {
                await MostrarEncuestas(SearchBarText);
            }).Wait();

        }
        #endregion
        #region VARIABLES
        string _IdUsuario;
        string _apellido;
        string _correo;
        string searchBarText;
        string _nombre;
        string _contrania;
        string _idAdmin;
        bool _estado;
        int _numEncuesta;
        string IdEncuesta;
        ObservableCollection <EncuestaM> lisEncuestaRecomendados;





        EncuestaM selectEncuesta;
        public List<EncuestaM> listaEncueta = new List<EncuestaM>();


        #endregion
        #region OBJETOS 

        //public List<EncuestaM> ListaEncueta { get { return listaEncueta; } set { SetProperty(ref listaEncueta, value); IdEncuesta = selectEncuesta.IdEncuesta; } }
        public EncuestaM SelectEncuesta { get { return selectEncuesta; } set { SetProperty(ref selectEncuesta, value); IdEncuesta = selectEncuesta.IdEncuesta; } }
        public ObservableCollection<EncuestaM> LisEncuestaRecomendados1 { get; set; }
        public ObservableCollection<EncuestaM> LisEncuestaRecomendados
        {
            get => lisEncuestaRecomendados;
            set
            {
                lisEncuestaRecomendados = value;
                OnPropertyChanged();Debug.WriteLine("LisEncuestaRecomendados changed");
            }
        }
        public string SearchBarText
        {
            get  => searchBarText; 
            set 
            {
                searchBarText = value;
                Task task = MostrarEncuestas(searchBarText);
            }
        }

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
        EncuestaD f = new EncuestaD();
        

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
            }
            catch (Exception)
            {
                await DisplayAlert("Alerta", "X tu seguridad la sesion se a cerrado", "Ok");
            }
        }



        


        public async Task MostrarEncuestas(string searchText)
        {
            var encuestas = await f.MostEncuestaRecomendada();
           if (!string.IsNullOrEmpty(searchText))
            {
                encuestas = encuestas.Where(e => e.NomPlato.Contains(searchText)).ToList();/* ||
                                                 e.NomLocal.Contains(searchText) ||
                                                 e.PromCalificacion.ToString().Contains(searchText) ||
                                                 e.NomUsuario.Contains(searchText) ||
                                                 e.Categorias.Contains(searchText) ||
                                                 e.Barrio.Contains(searchText) || 
                                                 e.Precio.Contains(searchText)).ToList();*/
            }
            LisEncuestaRecomendados = new ObservableCollection<EncuestaM>(encuestas);
            Debug.WriteLine("LisEncuestaRecomendados updated: " + LisEncuestaRecomendados.Count);
        }






        private async Task IrDetalleEncuesta(EncuestaM p)
        {
            await Navigation.PushAsync(new DetalleEncuestaUser(p));
        }

        #endregion
        #region COMANDOS

        public ICommand IrDetalleEncuestaCommand => new Command<EncuestaM>(async (p) => await IrDetalleEncuesta(p));
        



        #endregion
    }
}
