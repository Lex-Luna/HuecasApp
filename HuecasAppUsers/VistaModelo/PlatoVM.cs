using HuecasAppUsers.Datos;
using HuecasAppUsers.Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HuecasAppUsers.VistaModelo
{
    public class PlatoVM
    : BaseVM
    {

        #region CONSTRUCTOR
        public PlatoVM(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion
        #region VariablesPlato
        string txtNombrePlato;
        string txtPrecioPlato;
        string txtComentario;
        string txtDescripcion;
        #endregion
        #region ObjetosPlato 
        public string TxtNombrePlato{get { return txtNombrePlato; }set { SetValue(ref txtNombrePlato, value); }}
        public string TxtPrecioPlato { get { return txtPrecioPlato; }set { SetValue(ref txtPrecioPlato, value); }}
        public string TxtComentario { get { return txtComentario; }set { SetValue(ref txtComentario, value); }}
        public string TxtDescripcion { get { return txtDescripcion; }set { SetValue(ref txtDescripcion, value); }}

        #endregion
        #region ProcesosPlato
        private async Task Volver()
        {
            await Navigation.PopAsync();
        }

        public async Task AgregarPlato()
        {
            //await Subirfoto();
            var funcion = new PlatoD();
            var parametros = new PlatoM();
            parametros.Nombre = TxtNombrePlato;
            parametros.Descripcion = TxtDescripcion;
            parametros.Comentario = TxtComentario;
            parametros.Precio = TxtPrecioPlato;
            await funcion.InserPlato(parametros);
        }


        #endregion
        #region ComandosPlato
        //public ICommand InsertarRecolecoresComand => new Command(async () => await InsertarRecolecoresProces());
        public ICommand Volvercomamd => new Command(async () => await Volver());
        //public ICommand NavClientesComand => new Command(async () => await NavClientes());

        #endregion
    }
}
