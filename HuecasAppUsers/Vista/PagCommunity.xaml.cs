﻿using HuecasAppUsers.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuecasAppUsers.Datos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase.Auth;
using HuecasAppUsers.Conexiones;
using HuecasAppUsers.Modelo;
using Newtonsoft.Json;
using Xamarin.Essentials;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace HuecasAppUsers.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagCommunity : ContentPage
    {
        #region Constructor
        public PagCommunity()
        {
            InitializeComponent();
            //BindingContext = new ComunityVM(Navigation);
            BindingContext = this;
            Task.Run(async () =>
            {
                await MostrarEncuestas();
            }).Wait();

        }
        #endregion
        #region Variables
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
        EncuestaM selectEncuesta;
        ObservableCollection<EncuestaM> lisEncuestaRecomendados;


        #endregion
        #region Objetos
        //public EncuestaM SelectEncuesta { get { return selectEncuesta; } set { SetProperty(ref selectEncuesta, value); IdEncuesta = selectEncuesta.IdEncuesta; } }
        //public ObservableCollection<EncuestaM> LisEncuestaRecomendados1 { get; set; }
        public ObservableCollection<EncuestaM> LisEncuestaRecomendados
        {
            get => lisEncuestaRecomendados;
            set
            {
                lisEncuestaRecomendados = value;
                OnPropertyChanged(); Debug.WriteLine("LisEncuestaRecomendados changed");
            }
        }

        public int NumEncuesta { set; get; }
        public string Apellido { set; get; }
        public string Contrania { set; get; }
        public string IdAdmin { set; get; }

        public bool Estado { set; get; }
        public string Nombre { set; get; }


        public string IdUsuario { set; get; }

        public string Correo { set; get; }
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

        //Texto de Busqueda
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {

            var searchTerm = e.NewTextValue;
            var filteredItems = LisEncuestaRecomendados.Where(item => item.NomPlato != null && item.NomPlato.Contains(searchTerm)).ToList();
            EncuestasCommunity.ItemsSource = filteredItems;

        }

        public async Task MostrarEncuestas()
        {
            EncuestaD f = new EncuestaD();
            var encuestas = await f.MostEncuestaRecomendada();
            LisEncuestaRecomendados = new ObservableCollection<EncuestaM>(encuestas);

        }


        #endregion
    }
}