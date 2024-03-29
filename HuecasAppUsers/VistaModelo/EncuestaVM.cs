﻿using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using HuecasAppUsers.Conexiones;
using HuecasAppUsers.Datos;
using HuecasAppUsers.Modelo;
using HuecasAppUsers.Vista;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;
using Label = Xamarin.Forms.Label;

namespace HuecasAppUsers.VistaModelo
{


    public class EncuestaVM : BaseVM
    {
        #region ConstrutorGlobal
        public EncuestaVM(INavigation navigation)
        {
            Navigation = navigation;
            Task.Run(async () =>
            {
                await MostrarPais();
            }).Wait();

            Task.Run(async () =>
            {
                await MostrarCiudad();
            }).Wait();
            Task.Run(async () =>
            {
                await obtenerDataUserAsync();
            }).Wait();

            Task.Run(async () =>
            {
                await VistaNormal();
            }).Wait();

        }
        #endregion

        #region VariablesGlobales
        //paneles 
        bool panelFoto;
        bool panelEncuesta;
        public bool panelGeo;
        //UBICACION GEO
        public static string Geolocalizacion { get; set; }
        //id                                                                                    
        public string _IdPais;
        public string _IdCiudad;
        public string _IdLocal;
        public string _IdPlato;
        public string _IdPlatoLocal;
        public string _IdCalificacion;
        public string _IdEncuesta;
        public string _IdUsuario;
        //usuario
        string _idUsuario;
        string _apellido;
        string _correo;
        string _nombre;
        string _contrasenia;
        string _idAdmin;
        bool _estado;
        int _numEncuesta = 1;
        //local
        string txtNombreLocal;
        //plato
        string txtNombrePlato;




        #endregion
        #region ObjetosGlobales

        public bool PanelFoto { get { return panelFoto; } set { SetValue(ref panelFoto, value); } }
        public bool PanelEncuesta { get { return panelEncuesta; } set { SetValue(ref panelEncuesta, value); } }
        public bool PanelGeo { get { return panelGeo; } set { SetValue(ref panelGeo, value); } }


        //usuario
        public string Contrasenia
        {
            get { return _contrasenia; }
            set { SetValue(ref _contrasenia, value); }
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
        public string Apellido { get { return _apellido; } set { SetValue(ref _apellido, value); } }
        public int NumEncuesta { get { return _numEncuesta; } set { SetValue(ref _numEncuesta, value); } }
        public string Nombre { get { return _nombre; } set { SetValue(ref _nombre, value); } }

        public string Correo { get { return _correo; } set { SetValue(ref _correo, value); } }

        public string IdUsuario { get { return _idUsuario; } set { SetValue(ref _idUsuario, value); } }


        public string NombreCompleto
        {
            get { return string.Format("{0} {1}", Nombre, Apellido); }
        }

        //local
        public string TxtNombreLocal { get { return txtNombreLocal; } set { SetValue(ref txtNombreLocal, value); } }
        //plato
        public string TxtNombrePlato { get { return txtNombrePlato; } set { SetValue(ref txtNombrePlato, value); } }


        #endregion
        #region ProcesosGoblaes

        public async Task VistaNormal()
        {
            try
            {
                PanelEncuesta = true;
                PanelGeo = false;
                PanelFoto = false;
                await Task.Delay(500);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        #endregion

        #region  VariablesLocal
        public string IdLocal;
        public List<UbicacionM> listPais = new List<UbicacionM>();
        public List<UbicacionM> listCiudad = new List<UbicacionM>();

        string txtBarrio;
        string txtDireccion;
        public string IdPais;
        public string IdCiudad;

        public string FotoFachada;
        string _identificacion;
        UbicacionM selectPais;
        UbicacionM selectCiudad;

        //variables de las fotos
        string rutafoto;
        string rutaVideo;

        MediaFile foto;
        MediaFile video;



        #endregion
        #region ObjetosLocal 
        //De la categoria


        public List<string> Propiedades { get; set; } = new List<string> { "Comida Internacional", "Comida Marina", "Comida Rápida", "Comida Típica", "Postres", "Bebidas", "Otros" };
        public string PropiedadSeleccionada { get; set; }
        //FIn
        public string Identificacion { get { return _identificacion; } set { SetValue(ref _identificacion, value); } }
        public string TxtBarrio { get { return txtBarrio; } set { SetValue(ref txtBarrio, value); } }
        public string TxtDireccion { get { return txtDireccion; } set { SetValue(ref txtDireccion, value); } }
        //Mostrar los lista de pais y ciudad

        public UbicacionM SelectPais { get { return selectPais; } set { SetProperty(ref selectPais, value); IdPais = selectPais.IdPais; } }
        public UbicacionM SelectCiudad { get { return selectCiudad; } set { SetProperty(ref selectCiudad, value); IdCiudad = selectCiudad.IdCiudad; } }

        // pais ciudad 

        public List<UbicacionM> Listpais { get { return listPais; } set { SetValue(ref listPais, value); } }
        public List<UbicacionM> Listciudad { get { return listCiudad; } set { SetValue(ref listCiudad, value); } }


        #endregion
        #region ProcesosLocal

        async Task AgregarLocal()
        {

            try
            {
                await SubirFotoFachada();
                await SubirVideoLocal();
                var funcion = new LocalD();
                var parametros = new LocalM();

                if (!string.IsNullOrEmpty(Geolocalizacion))
                {
                    if (!string.IsNullOrEmpty(rutafoto))
                    {
                        if (!string.IsNullOrEmpty(rutaVideo))
                        {
                            if (!string.IsNullOrEmpty(TxtNombreLocal))
                            {
                                if (!string.IsNullOrEmpty(TxtDireccion))
                                {
                                    if (!string.IsNullOrEmpty(TxtBarrio))
                                    {
                                        if (!string.IsNullOrEmpty(IdPais))
                                        {
                                            if (!string.IsNullOrEmpty(IdCiudad))
                                            {
                                                if (!string.IsNullOrEmpty(PropiedadSeleccionada))
                                                {
                                                    parametros.Geolocalizacion = Geolocalizacion;
                                                    parametros.FotoFachada = rutafoto;
                                                    parametros.Video = rutaVideo;
                                                    parametros.NombreLocal = TxtNombreLocal;
                                                    parametros.Direccion = TxtDireccion;
                                                    parametros.Barrio = TxtBarrio;
                                                    parametros.IdPais = IdPais;
                                                    parametros.IdCiudad = IdCiudad;
                                                    parametros.Categorias = PropiedadSeleccionada;
                                                    _IdLocal = await funcion.InsertarLocal(parametros);
                                                }
                                                else
                                                    await DisplayAlert("Alerta", "No se ha seleccionado el tipo de comida del restaurante ", "OK");
                                            }
                                            else
                                                await DisplayAlert("Alerta", "No se seleccionado la ciudad del local", "OK");
                                        }
                                        else
                                            await DisplayAlert("Alerta", "No se ha seleccionado el pais del local", "OK");

                                    }
                                    else
                                        await DisplayAlert("Alerta", "No se ha agregado el barrio del local", "OK");
                                }
                                else
                                    await DisplayAlert("Alerta", "No se ha agregado las calles del local", "OK");
                            }
                            else
                                await DisplayAlert("Alerta", "No se ha agregado el nombre del local", "OK");
                        }
                        else
                            await DisplayAlert("Alerta", "No se ha agregado un video del interior del local", "OK");
                    }
                    else
                        await DisplayAlert("Alerta", "No se ha agregado una foto del local", "OK");

                }
                else
                    await DisplayAlert("Alerta", "No se ha agregado la geolocalizacion", "OK");
            }
            catch (Exception e)
            {

                throw e;
            }


        }



        async Task MostrarPais()
        {
            var funcion = new UbicacionD();
            Listpais = await funcion.MostPais();
        }
        async Task MostrarCiudad()
        {
            var funcion = new UbicacionD();
            Listciudad = await funcion.MostCiudad();
        }

        async Task Volver()
        {
            await Navigation.PopAsync();
        }
        public async Task SubirVideoLocal()
        {
            try
            {
                var funcion = new LocalD();
                if (rutaVideo == null)
                {
                    rutaVideo = await funcion.SubirVideo(video.GetStream(), TxtNombreLocal);
                }
                else
                {
                    await DisplayAlert("No gravaste el video", "La seccion video saldra vacia", "Ok");
                    rutaVideo = "No hay Video";
                }
            }
            catch (Exception)
            {
                await DisplayAlert("No gravaste el video", "La seccion video saldra vacia", "Ok");
                rutaVideo = "No hay Video";
            }

        }

        public async Task CapturarVideo()
        {
            var opcionesCamara = new StoreVideoOptions();
            opcionesCamara.Quality = VideoQuality.Medium;
            opcionesCamara.DesiredLength = TimeSpan.FromSeconds(10);
            opcionesCamara.SaveToAlbum = true;
            try
            {
                video = await Plugin.Media.CrossMedia.Current.TakeVideoAsync(opcionesCamara);
                if (video != null)
                {
                    Video = ImageSource.FromStream(() =>
                    {
                        return video.GetStream();
                    });
                    PanelEncuesta = false;
                    PanelFoto = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task SubirFotoFachada()
        {
            try
            {
                var funcion = new LocalD();
                if (rutafoto == null)
                {
                    rutafoto = await funcion.SubirFotoFachada(foto.GetStream(), IdLocal);
                }
                else
                {
                    await DisplayAlert("No tomaste foto", "La seccion foto saldra vacia", "Ok");
                    rutafoto = "No hay foto";
                }
            }
            catch (Exception)
            {
                await DisplayAlert("No tomaste foto", "La seccion foto saldra vacia", "Ok");
                rutafoto = "No hay foto";
            }

        }
        public async Task TomarFotoLocal()
        {
            var camara = new StoreCameraMediaOptions();
            camara.PhotoSize = PhotoSize.Medium;
            camara.SaveToAlbum = true;
            try
            {
                foto = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(camara);
                if (foto != null)
                {
                    Foto = ImageSource.FromStream(() =>
                    {
                        return foto.GetStream();
                    });
                    PanelEncuesta = false;
                    PanelFoto = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        void MostrarpanelGeo()
        {
            PanelGeo = true;
            PanelEncuesta = false;
        }

        async Task OkFoto()
        {
            try
            {
                PanelEncuesta = true;
                PanelFoto = false;
                await Task.Delay(500);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        async Task OkGeo()
        {
            try
            {
                PanelEncuesta = true;
                PanelGeo = false;
                await Task.Delay(500);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        async Task PagGeo()
        {
            await Navigation.PushAsync(new Geo());
            await Task.Delay(3000).ContinueWith(t => VistaNormal());

        }

        #endregion
        #region ComandosLocal
        public ICommand Volvercomamd => new Command(async () => await Volver());
        public ICommand AgregarLocalComamd => new Command(async () => await AgregarLocal());
        public ICommand OkFotoComamd => new Command(async () => await OkFoto());
        public ICommand OkGeoComamd => new Command(async () => await OkGeo());
        public ICommand MostrarpanelGeoComamd => new Command(() => MostrarpanelGeo());
        public ICommand TomarFotoLocalComamd => new Command(async () => await TomarFotoLocal());
        public ICommand CapturarVideoComamd => new Command(async () => await CapturarVideo());
        public ICommand PagGeoComamd => new Command(async () => await PagGeo());


        #endregion

        #region VariablesPlato

        string txtPrecioPlato;
        string txtComentario;
        string txtDescripcion;
        MediaFile fotoPlato;
        string rutafotoPlato;
        #endregion--
        #region ObjetosPlato 
        public string TxtPrecioPlato { get { return txtPrecioPlato; } set { SetValue(ref txtPrecioPlato, value); } }
        public string TxtComentario { get { return txtComentario; } set { SetValue(ref txtComentario, value); } }
        public string TxtDescripcion { get { return txtDescripcion; } set { SetValue(ref txtDescripcion, value); } }

        #endregion
        #region ProcesosPlato


        public async Task TomarFotoPlato()
        {
            var camara = new StoreCameraMediaOptions();
            camara.PhotoSize = PhotoSize.Medium;
            camara.SaveToAlbum = true;
            try
            {
                fotoPlato = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(camara);
                if (fotoPlato != null)
                {
                    Foto = ImageSource.FromStream(() =>
                    {
                        return fotoPlato.GetStream();
                    });
                    PanelEncuesta = false;
                    PanelFoto = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public async Task SubirFotoPlato()
        {
            try
            {
                var funcion = new PlatoD();
                if (fotoPlato != null && rutafotoPlato == null)
                {
                    rutafotoPlato = await funcion.SubirFotoPlato(fotoPlato.GetStream(), _IdPlato);
                }
                else
                {
                    await DisplayAlert("No tomaste foto", "La seccion foto saldra vacia", "Ok");
                    rutafotoPlato = "No hay foto";
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task AgregarPlato()
        {

            try
            {
                await SubirFotoPlato();
                var funcion = new PlatoD();
                var parametros = new PlatoM();
                if (!string.IsNullOrEmpty(TxtNombrePlato))
                {
                    if (!string.IsNullOrEmpty(rutafotoPlato))
                    {
                        if (!string.IsNullOrEmpty(TxtDescripcion))
                        {
                            if (!string.IsNullOrEmpty(TxtComentario))
                            {
                                if (!string.IsNullOrEmpty(TxtPrecioPlato))
                                {
                                    parametros.Nombre = TxtNombrePlato;
                                    parametros.FotoPlato = rutafotoPlato;
                                    parametros.Descripcion = TxtDescripcion;
                                    parametros.Comentario = TxtComentario;
                                    parametros.Precio = TxtPrecioPlato;
                                    _IdPlato = await funcion.InserPlato(parametros);
                                    var funcion2 = new PlatoLocalD();
                                    var parametros2 = new PlatoLocalM();
                                    parametros2.IdLocal = _IdLocal;
                                    parametros2.IdPLato = _IdPlato;
                                    _IdPlatoLocal = await funcion2.InserPlatoLocal(parametros2);
                                }
                                else
                                    await DisplayAlert("Alerta", "Agregue el costo de su plato por favor", "OK");
                            }
                            else
                                await DisplayAlert("Alerta", "Agregue un comentario sobre su plato por favor", "OK");
                        }
                        else
                            await DisplayAlert("Alerta", "Agregue una descripcion de su plato por favor", "OK");

                    }
                    else
                        await DisplayAlert("Alerta", "Agregue una foto de su plato por favor ", "OK");
                }
                else
                    await DisplayAlert("Alerta", "Agregue El nombre del plato por favor", "OK");

            }
            catch (Exception e)
            {

                throw e;
            }


        }

        #endregion
        #region ComandosPlato
        public ICommand AgregarPlatoComand => new Command(async () => await AgregarPlato());
        public ICommand TomarFotoPlatoComand => new Command(async () => await TomarFotoPlato());


        #endregion


        #region VariablesEncuesta
        public int txtLocal;
        public int txtComida;
        public int txtAtencion;
        public int _promedio;
        public int _totalencuesta;
        public bool recomendado = true;

        #endregion
        #region ObjetosEncuesta
        public int TxtAtencion { get { return txtAtencion; } set { SetValue(ref txtAtencion, value); } }
        public int TxtLocal { get { return txtLocal; } set { SetValue(ref txtLocal, value); } }
        public int TxtComida { get { return txtComida; } set { SetValue(ref txtComida, value); } }
        public int Promedio { get { return _promedio; } set { SetValue(ref _promedio, value); } }
        public int Totalencuesta { get { return _totalencuesta; } set { SetValue(ref _totalencuesta, value); } }
        public bool Recomendado { get { return recomendado; } set { SetValue(ref recomendado, value); } }

        #endregion
        #region ProcesosEncuesta
        private async Task IrContenedor()
        {
            await Navigation.PushAsync(new Contenedor());
        }
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
                Contrasenia = data[0].Contrasenia;
                Estado = data[0].Estado;


                //Preferences.Remove("MyFirebaseRefreshToken");  parece que el CPU esta a tope     , v

            }
            catch (Exception)
            {
                await DisplayAlert("Alerta", "X tu seguridad la sesion se a cerrado", "Ok");

            }
        }



        private async Task EditEncuestaUserAdd()
        {
            var f = new UsuarioD();
            var p = new UsuarioM();
            p.NumEncuesta = ++NumEncuesta;
            p.Apellido = Apellido;
            p.Nombre = Nombre;
            p.Estado = Estado;
            p.Correo = Correo;
            p.Contrasenia = Contrasenia;
            await f.AddNumEncuesta(p);
        }

        public async Task AddEncusta()
        {
            bool answer = await DisplayAlert("Esta seguro de haber llenado las encuestas anteriores", "Si la respuesta es si presiones el boton Si, de lo contrario ´presione No y llene la informacios solicitada por favor", "Si", "No");
            if (answer)
            {
                try
                {
                    var funcion = new CalificacionD();
                    var parametros = new CalificacionM();
                    parametros.CalificacionAtencion = TxtAtencion;
                    parametros.CalificacionComida = TxtComida;
                    parametros.CalificacionLugar = TxtLocal;
                    parametros.Recomendacion = Recomendado;
                    parametros.IdPlatoLocal = _IdPlatoLocal;
                    Promedio = (TxtAtencion + TxtComida + TxtLocal) / 3;

                    _IdCalificacion = await funcion.InserCalificacion(parametros);

                    var funcion2 = new EncuestaD();
                    var parametros2 = new EncuestaM();
                    parametros2.IdPlatoLocal = _IdPlatoLocal;
                    parametros2.IdLocal = _IdLocal;
                    parametros2.IdPlato = _IdPlato;
                    parametros2.IdCalificacion = _IdCalificacion;
                    parametros2.IdUsuario = IdUsuario;
                    parametros2.FechaData = DateTime.Now;
                    parametros2.Estado = true;
                    parametros2.NomUsuario = NombreCompleto;
                    parametros2.NomLocal = txtNombreLocal;
                    parametros2.NomPlato = txtNombrePlato;
                    parametros2.PromCalificacion = Promedio;
                    parametros2.TotalEncuesta = Totalencuesta;
                    parametros2.Recomendado = Recomendado;
                    parametros2.Categorias = PropiedadSeleccionada;
                    parametros2.Barrio = TxtBarrio;
                    parametros2.Precio = TxtPrecioPlato;
                    _IdEncuesta = await funcion2.InsertarEncuesta(parametros2);
                    await EditEncuestaUserAdd();
                    await IrContenedor();


                }
                catch (Exception e)
                {

                    throw e;
                }
            }
        }




        #endregion
        #region ComandosEncuesta
        public ICommand AdEncustaCommand => new Command(async () => await AddEncusta());

        #endregion




    }
}
