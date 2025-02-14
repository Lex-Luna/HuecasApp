﻿using HuecasAppUsers.Modelo;
using HuecasAppUsers.VistaModelo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HuecasAppUsers.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleEncuestaUser : ContentPage
    {
        public DetalleEncuestaUser(EncuestaM e)
        {
            InitializeComponent();
            BindingContext = new DetalleEncuestaUserVM(Navigation, e);
            VIstaPrincipal();

        }
        
        

        private void VIstaPrincipal()
        {
            MenuBotones.IsVisible = true;
            InfoCalificacion.IsVisible = false;
            InfoLocal.IsVisible = false;
            InfoPlato.IsVisible = false;
            
        }

        private void btnLocal_Clicked(object sender, EventArgs e)
        {
            MenuBotones.IsVisible = false;
            InfoCalificacion.IsVisible = false;
            InfoLocal.IsVisible = true;
            InfoPlato.IsVisible = false;
            
        }

        private void btnComida_Clicked(object sender, EventArgs e)
        {
            MenuBotones.IsVisible = false;
            InfoCalificacion.IsVisible = false;
            InfoLocal.IsVisible = false;
            InfoPlato.IsVisible = true;
            
        }

        private void btnCalificacion_Clicked(object sender, EventArgs e)
        {
            MenuBotones.IsVisible = false;
            InfoCalificacion.IsVisible = true;
            InfoLocal.IsVisible = false;
            InfoPlato.IsVisible = false;
            
        }

        private void btnCerrar1_Clicked(object sender, EventArgs e)
        {
            VIstaPrincipal();
        }
        private void btnCerrar2_Clicked(object sender, EventArgs e)
        {
            VIstaPrincipal();
        }
        private void btnCerrar3_Clicked(object sender, EventArgs e)
        {
            VIstaPrincipal();
        }

        private void Atras_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void CerrarFoto_Clicked(object sender, EventArgs e)
        {
            MenuBotones.IsVisible = false;
            InfoCalificacion.IsVisible = false;
            InfoLocal.IsVisible = true;
            InfoPlato.IsVisible = false;
            
        }

        private void CerrarVideo_Clicked(object sender, EventArgs e)
        {
            MenuBotones.IsVisible = false;
            InfoCalificacion.IsVisible = false;
            InfoLocal.IsVisible = true;
            InfoPlato.IsVisible = false;
            
        }

        private void btnfoto_Clicked(object sender, EventArgs e)
        {
            MenuBotones.IsVisible = false;
            InfoCalificacion.IsVisible = false;
            InfoLocal.IsVisible = false;
            InfoPlato.IsVisible = false;
            
        }

        private void btnlocal_Clicked_1(object sender, EventArgs e)
        {
            MenuBotones.IsVisible = false;
            InfoCalificacion.IsVisible = false;
            InfoLocal.IsVisible = true;
            InfoPlato.IsVisible = false;
            
        }
        private void  OnLabelTapped(object sender, EventArgs e)
        {
            var label = (Label)sender;
            string text = label.Text;
            
            Launcher.OpenAsync(text);
            
        }

        [Obsolete]
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var label = (Label)sender;
            string ubicacion = label.Text;
            string[] separadas = ubicacion.Split(';');
            double latitud = Convert.ToDouble(separadas[0]);
            double longitud = Convert.ToDouble(separadas[1]);

            //var uri = new Uri($"https://maps.google.com/?q={latitud},{longitud}");
            var uri = new Uri($"https://www.google.com/maps/search/?api=1&query={latitud.ToString(CultureInfo.InvariantCulture)},{longitud.ToString(CultureInfo.InvariantCulture)}");
            Device.OpenUri(uri);
        }
    }
}