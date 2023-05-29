using Firebase.Auth;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

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
        string _contrania;
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
                PanelGeo= false;
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
        MediaFile foto;

        

        #endregion
        #region ObjetosLocal 
        
        public string Identificacion { get { return _identificacion; } set { SetValue(ref _identificacion, value); } }
        public string TxtBarrio { get { return txtBarrio; } set { SetValue(ref txtBarrio, value); } }
        public string TxtDireccion { get { return txtDireccion; } set { SetValue(ref txtDireccion, value); } }
        //Mostrar los lista de pais y ciudad

        public UbicacionM SelectPais { get { return selectPais; } set { SetProperty(ref selectPais, value); IdPais = selectPais.IdPais; } }
        public UbicacionM SelectCiudad { get { return selectCiudad; } set { SetProperty(ref selectCiudad, value); IdCiudad = selectCiudad.IdCiudad; } }
        //selesccionar pais ciudad 

        public List<UbicacionM> Listpais { get { return listPais; } set { SetValue(ref listPais, value); } }//
        public List<UbicacionM> Listciudad { get { return listCiudad; } set { SetValue(ref listCiudad, value); } }//ciudad = selectPais.Ciudad.ToString();

        #endregion
        #region ProcesosLocal

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
        
        async Task AgregarLocal()
        {
            try
            {
                await SubirFoto();
                var funcion = new LocalD();
                var parametros = new LocalM();
                parametros.Geolocalizacion = Geolocalizacion;
                parametros.FotoFachada = rutafoto;
                parametros.NombreLocal = TxtNombreLocal;
                parametros.Direccion = TxtDireccion;
                parametros.Barrio = TxtBarrio;
                parametros.IdPais = IdPais;
                parametros.IdCiudad = IdCiudad;
                _IdLocal = await funcion.InsertarLocal(parametros);
            }
                catch (Exception e)
            {

                throw e;
            }
            
        }

        public async Task SubirFoto()
        {
            try
            {
                var funcion = new LocalD();
                if (rutafoto != null)
                {
                    rutafoto = await funcion.SubirFotoFachada(foto.GetStream(), IdLocal);
                }
                else
                {
                    await DisplayAlert("No tomaste foto", "La seccion foto saldra vacia", "Ok");
                    rutafoto = "No hay foto";
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public async Task TomarFoto()
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
            PanelGeo= true;
            PanelEncuesta= false;
        }

        async Task  OkFoto()
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
                PanelGeo= false;
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
        }

        #endregion
        #region ComandosLocal
        public ICommand Volvercomamd => new Command(async () => await Volver());
        public ICommand AgregarLocalComamd => new Command(async () => await AgregarLocal());
        public ICommand OkFotoComamd => new Command(async () => await OkFoto());
        public ICommand OkGeoComamd => new Command(async () => await OkGeo());
        public ICommand MostrarpanelGeoComamd => new Command(() => MostrarpanelGeo());
        public ICommand TomarFotoComamd => new Command(async () => await TomarFoto());
        public ICommand PagGeoComamd => new Command(async () => await PagGeo());


        #endregion


        #region VariablesPlato
        string txtPrecioPlato;
        string txtComentario;
        string txtDescripcion;
        #endregion
        #region ObjetosPlato 
        public string TxtPrecioPlato { get { return txtPrecioPlato; } set { SetValue(ref txtPrecioPlato, value); } }
        public string TxtComentario { get { return txtComentario; } set { SetValue(ref txtComentario, value); } }
        public string TxtDescripcion { get { return txtDescripcion; } set { SetValue(ref txtDescripcion, value); } }

        #endregion
        #region ProcesosPlato


        public async Task AgregarPlato()
        {
            //await Subirfoto();
            var funcion = new PlatoD();
            var parametros = new PlatoM();
            parametros.Nombre = TxtNombrePlato;
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


        #endregion
        #region ComandosPlato
        public ICommand AgregarPlatoComand => new Command(async () => await AgregarPlato());


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
        private async Task IrMenuUser()
        {
            await Navigation.PushAsync(new MenuUser());
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
                Contrania = data[0].Contrasenia;
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
            p.Contrasenia = Contrania;
            await f.AddNumEncuesta(p);
        }

        public async Task AddEncusta()
        {
            var funcion = new CalificacionD();
            var parametros = new CalificacionM();
            parametros.CalificacionAtencion = TxtAtencion;
            parametros.CalificacionComida = TxtComida;
            parametros.CalificacionLugar = TxtLocal;
            parametros.Recomendacion = Recomendado;
            parametros.IdLocal = _IdLocal;
            parametros.IdPlato = _IdPlato;
            Promedio = (TxtAtencion + TxtComida + TxtLocal) / 3;

            _IdCalificacion = await funcion.InserCalificacion(parametros);

            var funcion2 = new EncuestaD();
            var parametros2 = new EncuestaM();
            parametros2.IdPlatoLocal = _IdPlatoLocal;
            parametros2.IdPlato = _IdPlato;
            parametros2.IdLocal = _IdLocal;
            parametros2.IdCalificacion = _IdCalificacion;
            parametros2.IdUsuario = IdUsuario;
            parametros2.FechaData = DateTime.Now;
            parametros2.Estado = true;
            parametros2.NomUsuario = NombreCompleto;
            parametros2.NomLocal = txtNombreLocal;
            parametros2.NomPlato = txtNombrePlato;
            parametros2.PromCalificacion = Promedio;
            parametros2.TotalEncuesta = Totalencuesta;

            await EditEncuestaUserAdd();

            _IdEncuesta = await funcion2.InsertarEncuesta(parametros2);
            await IrMenuUser();
        }




        #endregion
        #region ComandosEncuesta
        public ICommand AdEncustaCommand => new Command(async () => await AddEncusta());

        #endregion




    }
}
