using HuecasAppUsers.Datos;
using HuecasAppUsers.Modelo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HuecasAppUsers.VistaModelo
{
    public class LocalVM : BaseVM
    {
    /*
        #region CosntructorLocal 
        public LocalVM(INavigation navigation)
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
        #region  VariablesLocal

        public List<UbicacionM> listPais = new List<UbicacionM>();
        public List<UbicacionM> listCiudad = new List<UbicacionM>();

        //string pais;
        //string ciudad;
        string txtNombreLocal;
        string txtBarrio;
        string txtDireccion;
        public string IdPais;
        public string IdCiudad;

        public string _IdPais;
        public string _IdCiudad;

        UbicacionM selectPais;
        UbicacionM selectCiudad;



        #endregion
        #region ObjetosLocal 
        public string TxtNombreLocal { get { return txtNombreLocal; } set { SetValue(ref txtNombreLocal, value); } }
        public string TxtBarrio { get { return txtBarrio; } set { SetValue(ref txtBarrio, value); } }
        public string TxtDireccion { get { return txtDireccion; } set { SetValue(ref txtDireccion, value); } }
        //Mstrar los lista de pais y ciudad

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
            //parametros.FotoFachada = rutafoto;
            //parametros.Geo = Geolocalizacion;
            parametros.NombreLocal = TxtNombreLocal;
            parametros.Direccion = TxtDireccion;
            parametros.Barrio = TxtBarrio;
            parametros.IdPais = IdPais;
            parametros.IdCiudad = IdCiudad;
            await funcion.InsertarLocal(parametros);
        }


        #endregion
        #region ComandosLocal
        public ICommand Volvercomamd => new Command(async () => await Volver());
        public ICommand AgregarLocalComamd => new Command(async () => await AgregarLocal());
        //public ICommand NavClientesComand => new Command(async () => await NavClientes());

        #endregion
    */
    }


}
