﻿using Firebase.Auth;
using HuecasAppUsers.Conexiones;
using HuecasAppUsers.Datos;
using HuecasAppUsers.Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
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
    public partial class EncuestaAdmin : ContentPage
    {
        #region Constructor
        public EncuestaAdmin()
        {
            InitializeComponent();
            BindingContext = this;
            VistaPrinccipal();
            Task.Run(async () =>
            {
                await MostrarEncuestas();
            }).Wait();

        }
        #endregion
        #region Variables

        ObservableCollection<EncuestaM> lisEncuestaRecomendados;

        #endregion
        #region Objetos

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
       
        #region BotonPaneles
        
        void VistaPrinccipal()
        {
            PanelComida.IsVisible = false;
            PanelPrincipal.IsVisible = true;
            PanelRestaurante.IsVisible = false;
            PanelPrecio.IsVisible = false;
            PanelCategoria.IsVisible = false;
            PanelBarrio.IsVisible = false;
            PanelFecha.IsVisible = false;
        }
        private void BtnRestaurante_Clicked(object sender, EventArgs e)
        {
            PanelComida.IsVisible = false;
            PanelPrincipal.IsVisible = false;
            PanelRestaurante.IsVisible = true;
            PanelPrecio.IsVisible = false;
            PanelCategoria.IsVisible = false;
            PanelBarrio.IsVisible = false;
            PanelFecha.IsVisible = false;
        }
        private void BtnCategoria_Clicked(object sender, EventArgs e)
        {
            PanelComida.IsVisible = false;
            PanelPrincipal.IsVisible = false;
            PanelRestaurante.IsVisible = false;
            PanelPrecio.IsVisible = false;
            PanelCategoria.IsVisible = true;
            PanelBarrio.IsVisible = false;
            PanelFecha.IsVisible = false;
        }

        private void BtnPlato_Clicked(object sender, EventArgs e)
        {
            PanelComida.IsVisible = true;
            PanelPrincipal.IsVisible = false;
            PanelRestaurante.IsVisible = false;
            PanelPrecio.IsVisible = false;
            PanelCategoria.IsVisible = false;
            PanelBarrio.IsVisible = false;
            PanelFecha.IsVisible = false;
        }
        private void BtnBarrio_Clicked(object sender, EventArgs e)
        {
            PanelComida.IsVisible = false;
            PanelPrincipal.IsVisible = false;
            PanelRestaurante.IsVisible = false;
            PanelPrecio.IsVisible = false;
            PanelCategoria.IsVisible = false;
            PanelBarrio.IsVisible = true;
            PanelFecha.IsVisible = false;
        }
        private void BtnPrecios_Clicked(object sender, EventArgs e)
        {
            PanelComida.IsVisible = false;
            PanelPrincipal.IsVisible = false;
            PanelRestaurante.IsVisible = false;
            PanelPrecio.IsVisible = true;
            PanelCategoria.IsVisible = false;
            PanelBarrio.IsVisible = false;
            PanelFecha.IsVisible = false;
        }
        private void BtnFecha_Clicked(object sender, EventArgs e)
        {
            PanelComida.IsVisible = false;
            PanelPrincipal.IsVisible = false;
            PanelRestaurante.IsVisible = false;
            PanelPrecio.IsVisible = false;
            PanelCategoria.IsVisible = false;
            PanelBarrio.IsVisible = false;
            PanelFecha.IsVisible = true;
        }
        #endregion
        #region BotonCerrar

        private void Atras_Clicked(object sender, EventArgs e)
        {
            VistaPrinccipal();
        }
        private void Atras1_Clicked(object sender, EventArgs e)
        {
            VistaPrinccipal();
        }

        private void Atras2_Clicked(object sender, EventArgs e)
        {
            VistaPrinccipal();
        }

        private void Atras3_Clicked(object sender, EventArgs e)
        {
            VistaPrinccipal();
        }

        private void Atras4_Clicked(object sender, EventArgs e)
        {
            VistaPrinccipal();
        }
        private void Atras_Clicked_1(object sender, EventArgs e)
        {
            VistaPrinccipal();
        }
        private void Atras_Clicked_2(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        private void Vuelve_Clicked(object sender, EventArgs e)
        {
            VistaPrinccipal();
        }
        #endregion
        #region Busquedas 
        public static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
        private void BuscarRestaurante_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchTerm = e.NewTextValue.ToLower();
            var filteredItems = LisEncuestaRecomendados.Where(item =>
                item.NomLocal != null && RemoveDiacritics(item.NomLocal.ToLower()).Contains(searchTerm)).ToList();
            RestauranteCommunity.ItemsSource = filteredItems;
        }
        private void BuscarCategoria_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchTerm = e.NewTextValue.ToLower();
            var filteredItems = LisEncuestaRecomendados.Where(item =>
                item.Categorias != null && RemoveDiacritics(item.Categorias.ToLower()).Contains(searchTerm)).ToList();
            CategoriaCommunity.ItemsSource = filteredItems;
        }

        private void BuscarBarrio_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var searchTerm = e.NewTextValue.ToLower();
                var filteredItems = LisEncuestaRecomendados.Where(item =>
                    item.Barrio != null && RemoveDiacritics(item.Barrio.ToLower()).Contains(searchTerm)).ToList();
                BarrioCommunity.ItemsSource = filteredItems;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        private void BuscarComida_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchTerm = e.NewTextValue.ToLower();
            var filteredItems = LisEncuestaRecomendados.Where(item =>
                item.NomPlato != null && RemoveDiacritics(item.NomPlato.ToLower()).Contains(searchTerm)).ToList();
            ComidaCommunity.ItemsSource = filteredItems;

        }

        private void BuscarPrecio_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchTerm = e.NewTextValue;
            if (int.TryParse(searchTerm, out int precioBusqueda))
            {
                var filteredItems = LisEncuestaRecomendados.Where(item =>
                {
                    if (item.Precio != null && double.TryParse(item.Precio, out double precio))
                    {
                        return precio <= precioBusqueda;
                    }
                    else
                    {
                        // Manejar el error de conversión aquí
                        return false;
                    }
                }).ToList();
                PrecioCommunity.ItemsSource = filteredItems;

            }

        }

        private void BuscarFecha_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            var searchTerm = e.NewTextValue;
            var filteredItems = LisEncuestaRecomendados.Where(item =>
                item.FechaData != null && item.FechaData.ToString("dd/MM/yyyy").Contains(searchTerm)).ToList();
            FechaCommunity.ItemsSource = filteredItems;
        }
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
        public async Task MostrarEncuestas()
        {
            EncuestaD f = new EncuestaD();
            var encuestas = await f.MostEncuestaRecomendada();
            LisEncuestaRecomendados = new ObservableCollection<EncuestaM>(encuestas);

        }
        private async Task IrDetalleEncuestaAdmin(EncuestaM p)
        {
            try
            {
                await Navigation.PushAsync(new DetalleEncuestaAdmin(p));
            }
            catch (Exception e)
            {

                Debug.WriteLine("Error: No se pudo tomar el ID " + e);

            }
        }



        #endregion
        #region Comandos
        public ICommand IrDetalleEncuestaAdminCommand => new Command<EncuestaM>(async (p) => await IrDetalleEncuestaAdmin(p));


        #endregion

    }
}