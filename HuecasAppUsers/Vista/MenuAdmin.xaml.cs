﻿using Firebase.Auth;
using HuecasAppUsers.Conexiones;
using HuecasAppUsers.Datos;
using HuecasAppUsers.Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HuecasAppUsers.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuAdmin : ContentPage
    {
        public MenuAdmin()
        {
            InitializeComponent();
            BindingContext = this;
            Task.Run(async () =>
            {
                await obtenerDataUserAsync();
            }).Wait();

        }
        #region VARIABLES
        string _nombre;
        string _IdUsuario;
        string _apellido;
        string _correo;
        string _contrania;
        string _idAdmin;
        string _fotoUsuario;
        bool _estado;
        int _numEncuesta;


        public ObservableCollection<EncuestaM> listaEncueta1;


        #endregion
        #region OBJETOS 


        public EncuestaM SelectEncuesta { set; get; }
        public ObservableCollection<EncuestaM> LisEncueta1
        {
            get => listaEncueta1;
            set
            {
                listaEncueta1 = value;
                OnPropertyChanged(); Debug.WriteLine("LisEncuestaRecomendados changed");
            }
        }

        public int NumEncuesta
        {
            get => _numEncuesta;
            set
            {
                _numEncuesta = value;
                OnPropertyChanged();
            }
        }
        public string Apellido
        {
            get => _apellido;
            set
            {
                _apellido = value;
                OnPropertyChanged();
            }
        }
        public string FotoUsuario
        {
            get => _fotoUsuario;
            set
            {
                _fotoUsuario = value;
                OnPropertyChanged();
            }
        }
        public string Contrania
        {
            get => _contrania;
            set
            {
                _contrania = value;
                OnPropertyChanged();
            }
        }

        public string IdAdmin
        {
            get => _idAdmin;
            set
            {
                _idAdmin = value;
                OnPropertyChanged();
            }
        }

        public bool Estado
        {
            get => _estado;
            set
            {
                _estado = value;
                OnPropertyChanged();

            }
        }

        public string Nombre
        {
            get => _nombre;
            set
            {
                _nombre = value;
                OnPropertyChanged();
            }
        }


        public string IdUsuario
        {
            get => _IdUsuario;
            set
            {
                _IdUsuario = value;
                OnPropertyChanged();
            }
        }

        public string Correo
        {
            get => _correo;
            set
            {
                _correo = value;
                OnPropertyChanged();
            }
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
                FotoUsuario = data[0].FotoUsuario;
            }
            catch (Exception)
            {
                await DisplayAlert("Alerta", "X tu seguridad la sesion se a cerrado", "Ok");
            }
        }

        private async Task IrEncuestaAdmin()
        {
            await Navigation.PushAsync(new EncuestaAdmin());
        }

        private async Task IrUserqueda()
        {
            await Navigation.PushAsync(new Userqueda());
        }
        #endregion
        #region COMANDOS

        public ICommand IrEncuestaAdminComamd => new Command(async () => await IrEncuestaAdmin());
        public ICommand IrUserquedaComamd => new Command(async () => await IrUserqueda());

        #endregion


    }
}