using HuecasAppUsers.VistaModelo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace HuecasAppUsers.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Geo : ContentPage
    {
        public Geo()
        {
            InitializeComponent();
        }
        
        
        Pin punto = new Pin();
        public static double latitud = 0;
        public static double longitud = 0;

        //nos reubica  nuestra ubicacion... arrastra  el pin
        protected override async void OnAppearing()
        {
            try
            {
                punto = new Pin()   
                {
                    Label = "Tu ubicación",
                    Type = PinType.Place,
                    Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory
                .FromBundle("alfi.png") : BitmapDescriptorFactory.FromView(new Image()
                {
                    Source = "alfi.png",
                    WidthRequest = 64,
                    HeightRequest = 64
                }),
                    Position = new Position(0, 0),
                    IsDraggable = true //puede moverse el pin
                };
                map.Pins.Add(punto);
                await LocalizacionActual();
            }
            catch (Exception e )
            {

                throw e;
            }
            
        }
        private void map_PinDragEnd(object sender, Xamarin.Forms.GoogleMaps.PinDragEventArgs e)
        {
            var posicion = new Position(e.Pin.Position.Latitude, e.Pin.Position.Longitude);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(posicion, Distance.FromMeters(500)));
            latitud = e.Pin.Position.Latitude;
            longitud = e.Pin.Position.Longitude;
            EncuestaVM.Geolocalizacion= latitud+","+longitud;
        }
        public async Task LocalizacionActual()
        {
            try
            {
                var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                var status2 = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.High, //darle timepo para que busque la ubicacion
                        Timeout = TimeSpan.FromSeconds(30)
                    });

                }  
                if (location == null)
                {
                    await DisplayAlert("Alerta", "Sin acceso al GPS", "OK");
                }
                else
                {
                    latitud = location.Latitude;
                    longitud = location.Longitude;
                    var posicion = new Position(latitud, longitud);
                    punto.Position = new Position(latitud, longitud);
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(posicion, Distance.FromMeters(500)));
                } 
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        
       
        private async void btnConfirmar1_Clicked(object sender, EventArgs e)
        {

            if (latitud != 0 && longitud != 0)
            {
                EncuestaVM.Geolocalizacion = latitud + "," + longitud;
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Sin datos", "Ubique un punto", "OK");
            }
        }
    }
}