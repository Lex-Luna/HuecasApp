using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using HuecasAppUsers.Conexiones;
using HuecasAppUsers.Datos;
using HuecasAppUsers.Modelo;
using HuecasAppUsers.Vista;
using Newtonsoft.Json;
using System;
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
        #region CONSTRUCTOR
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
        }
        #endregion

        #region VariablesIds
        public string _IdPais;
        public string _IdCiudad;
        public string _IdLocal;
        public string _IdPlato;
        public string _IdPlatoLocal;
        public string _IdCalificacion;
        public string _IdEncuesta;
        public string _IdUsuario;
        #endregion


        #region  VariablesLocal
        public string idLocal;
        public List<UbicacionM> listPais = new List<UbicacionM>();
        public List<UbicacionM> listCiudad = new List<UbicacionM>();

        //string pais;
        //string ciudad;
        string txtNombreLocal;
        string txtBarrio;
        string txtDireccion;
        public string IdPais;
        public string IdCiudad;

        

        UbicacionM selectPais;
        UbicacionM selectCiudad;



        #endregion
        #region ObjetosLocal 
        public string TxtNombreLocal { get { return txtNombreLocal; } set { SetValue(ref txtNombreLocal, value); } }
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

        public async Task MostrarPais()
        {
            var funcion = new UbicacionD();
            Listpais = await funcion.MostPais();
        }
        public async Task MostrarCiudad()
        {
            var funcion = new UbicacionD();
            Listciudad = await funcion.MostCiudad();
        }
        
        private async Task Volver()
        {
            await Navigation.PopAsync();
        }

        public async Task AgregarLocal()
        {
            //await Subirfoto();
            var funcion = new LocalD();
            var parametros = new LocalM();
            parametros.NombreLocal = TxtNombreLocal;
            parametros.Direccion = TxtDireccion;
            parametros.Barrio = TxtBarrio;
            parametros.IdPais = IdPais;
            parametros.IdCiudad = IdCiudad;
            _IdLocal = await funcion.InsertarLocal(parametros);
        }
        

        public async  Task<string> ObtenerIdEncuesta(EncuestaM encuesta)
        {
            FirebaseClient firebaseClient = new FirebaseClient("https://huecasapp-d8da1-default-rtdb.firebaseio.com/");
            var pushResponse = await Constantes.firebase.Child("Encuestas").PostAsync(encuesta);
            return pushResponse.Key;

        }


        #endregion
        #region ComandosLocal
        public ICommand Volvercomamd => new Command(async () => await Volver());
        public ICommand AgregarLocalComamd => new Command(async () => await AgregarLocal());
        //public ICommand NavClientesComand => new Command(async () => await NavClientes());

        #endregion


        #region VariablesPlato
        string txtNombrePlato;
        string txtPrecioPlato;
        string txtComentario;
        string txtDescripcion;
        #endregion
        #region ObjetosPlato 
        public string TxtNombrePlato { get { return txtNombrePlato; } set { SetValue(ref txtNombrePlato, value); } }
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
            _IdPlato= await funcion.InserPlato(parametros);
            var funcion2 = new PlatoLocalD();
            var parametros2 = new PlatoLocalM();
            parametros2.IdLocal = _IdLocal;
            parametros2.IdPLato = _IdPlato;
            _IdPlatoLocal = await funcion2.InserPlatoLocal(parametros2);
        }


        #endregion
        #region ComandosPlato
        public ICommand AgregarPlatoComand => new Command(async () => await AgregarPlato());
        //public ICommand NavClientesComand => new Command(async () => await NavClientes());

        #endregion


        


        #region VariablesEncuesta
        public string txtLocal;
        public string txtComida;
        public string txtAtencion;
        public bool recomendado = true;
        #endregion
        #region Objetos
        public string TxtAtencion { get { return txtAtencion; } set { SetValue(ref txtAtencion, value); } }
        public string TxtLocal { get { return txtLocal; } set { SetValue(ref txtLocal, value); } }
        public string TxtComida { get { return txtComida; } set { SetValue(ref txtComida, value); } }
        public bool Recomendado { get { return recomendado; } set { SetValue(ref recomendado, value); } }

        #endregion
        #region ProcesosEncuesta


        public async Task AdEncusta()
        {
            var funcion = new CalificacionD();
            var parametros = new CalificacionM();
            parametros.CalificacionAtencion = TxtAtencion;
            parametros.CalificacionComida = TxtComida;
            parametros.CalificacionLugar = TxtLocal;
            parametros.Recomendacion = Recomendado;

            _IdCalificacion = await funcion.InserCalificacion(parametros);


            var funcion2 = new EncuestaD();
            var parametros2 = new EncuestaM();
            parametros2.IdPlatoLocal = _IdPlatoLocal;
            parametros2.IdCalificacion= _IdCalificacion;
            parametros2.FechaEncuesta = DateTime.Now.ToString("dd/MM/yyyy");
            parametros2.Estado = true;
            
            _IdEncuesta = await funcion2.InsertarEncuesta(parametros2);
        }


        #endregion
        #region ComandosEncuesta
        public ICommand AdEncustaCommand => new Command(async () => await AdEncusta());
        //public ICommand NavClientesComand => new Command(async () => await NavClientes());

        #endregion


        

    }
}
