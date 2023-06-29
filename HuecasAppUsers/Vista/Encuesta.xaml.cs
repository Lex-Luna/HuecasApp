using HuecasAppUsers.Datos;
using HuecasAppUsers.Modelo;
using HuecasAppUsers.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;

namespace HuecasAppUsers.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Encuesta : ContentPage
    {
        public Encuesta()
        {
            InitializeComponent();
            BindingContext = new EncuestaVM(Navigation);
        }
        #region Slider
        private void Slider_Atencion(object sender, ValueChangedEventArgs e)
        {
            double newValue = Math.Round(e.NewValue);
            
            sliderAtencion.Text = newValue.ToString();
            if (e.NewValue >= 7)
            {
                FrAtencion.BackgroundColor = Color.Green;
            }  else
                FrAtencion.BackgroundColor = Color.Black;
            
        }

        private void Slider_Comida(object sender, ValueChangedEventArgs e)
        {
            double newValue = Math.Round(e.NewValue);
            sliderComida.Text = newValue.ToString();
            if (e.NewValue >=7)
            {
                FrComida.BackgroundColor = Color.Green;
            }
            else
                FrComida.BackgroundColor = Color.Black;
        }

        private void Slider_EstadoLocal(object sender, ValueChangedEventArgs e)
        {
            double newValue = Math.Round(e.NewValue);
            sliderEstadoLocal.Text = newValue.ToString();
            if (e.NewValue >= 7)
            {
                FrLocal.BackgroundColor = Color.Green;
            }
            else
                FrLocal.BackgroundColor = Color.Black;
        }
        #endregion
        #region BotonScroll
        private async void BtnLocal_Clicked(object sender, EventArgs e)
        {
            var ScrollView = Content as ScrollView;
            var aPlato = FindByName("gridPlato") as View;
            await ScrollView.ScrollToAsync(aPlato, ScrollToPosition.Start, true);
            GridLocal.IsVisible = false;
        }

        private async void BtnPlato_Clicked(object sender, EventArgs e)
        {
            var ScrollView = Content as ScrollView;
            var aPlato = FindByName("gridCalificaciones") as View;
            await ScrollView.ScrollToAsync(aPlato, ScrollToPosition.Start, true);
            gridPlato.IsVisible = false;
        }


        #endregion



        #region CalificacionVariable
        public string _idCalificacion;
        #endregion
        
        #region CalificacionProceso

        private  void Calificacion_Clicked(object sender, EventArgs e)
        {
            /*    var funcion = new CalificacionD();
                var parametros = new CalificacionM();
                parametros.CalificacionAtencion = sliderAtencion.Text;
                parametros.CalificacionComida = sliderComida.Text;
                parametros.CalificacionLugar = sliderEstadoLocal.Text;
                parametros.Recomendacion = recomendacion.IsToggled;

            _idCalificacion = await funcion.InserCalificacion(parametros);
             */
            }

            #endregion


        }
    }