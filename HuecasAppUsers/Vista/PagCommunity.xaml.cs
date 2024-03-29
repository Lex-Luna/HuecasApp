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
using System.Windows.Input;
using System.Globalization;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Picker = Xamarin.Forms.Picker;

namespace HuecasAppUsers.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagCommunity : ContentPage
    {
        #region Constructor
        public PagCommunity()
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
        private DateTime fechaSeleccionada;
        
        ObservableCollection<EncuestaM> lisEncuestaRecomendados;
        ObservableCollection<EncuestaM> lisEncuestaFiltrados;
        
         
        #endregion
        #region Objetos

        public ObservableCollection<EncuestaM> LisEncuestaRecomendados
        {
            get => lisEncuestaRecomendados;
            set
            {
                lisEncuestaRecomendados= value;
                OnPropertyChanged(); Debug.WriteLine("LisEncuestaRecomendados changed");
            }
        }
        public ObservableCollection<EncuestaM> LisEncuestaFiltrados
        {
            get => lisEncuestaFiltrados;
            set
            {
                lisEncuestaFiltrados= value;
                OnPropertyChanged(); Debug.WriteLine("LisEncuestaRecomendados changed");
            }
        }
        public List<string>Categorias { get; set; }
        public List<string> OpcionCategorias { get; set; } = new List<string> { "Comida Internacional", "Comida Marina", "Comida Rápida", "Comida Típica", "Postres", "Bebidas", "Otros" };
        public string CategoriaSeleccionada { get; set; }

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
            PanelFecha.IsVisible=false;
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
        #endregion
        #region TextosBusqueda

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
        private void BuscarComida_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchTerm = e.NewTextValue.ToLower();
            var filteredItems = LisEncuestaRecomendados.Where(item =>
                item.NomPlato != null && RemoveDiacritics(item.NomPlato.ToLower()).Contains(searchTerm)).ToList();
            ComidaCommunity.ItemsSource = filteredItems;

        }
        private void BuscarBarrio_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchTerm = e.NewTextValue.ToLower();
            var filteredItems = LisEncuestaRecomendados.Where(item =>
                item.Barrio != null && RemoveDiacritics(item.Barrio.ToLower()).Contains(searchTerm)).ToList();
            BarrioCommunity.ItemsSource = filteredItems;
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
        #region BusquedaDatePicker
        private void datePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            
            var filteredItems = LisEncuestaRecomendados.
                Where(item => item.FechaData.Date == e.NewDate.Date)
                .ToList();
                FechaCommunity.ItemsSource = filteredItems;

        }

        #endregion
        #region Procesos
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
        public async Task MostrarEncuestas()
        {
            EncuestaD f = new EncuestaD();
            var encuestas = await f.MostEncuestaRecomendada();
            LisEncuestaRecomendados = new ObservableCollection<EncuestaM>(encuestas);

        }
        

        #endregion
        #region CategoriaPicker
        private void PkCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            string selectedCategory = (string)picker.SelectedItem;

            var filteredItems = LisEncuestaRecomendados
                .Where(item => item.Categorias == selectedCategory)
                .ToList();

            CategoriaCommunity.ItemsSource = filteredItems;

        }
        

        #endregion
        #region Comandos
        public ICommand IrDetalleEncuestaCommand => new Command<EncuestaM>(async (p) => await IrDetalleEncuesta(p));


        #endregion

    }
}