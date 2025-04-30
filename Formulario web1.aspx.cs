using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wsCheckUsuario.Models;


namespace wsCheckUsuario
{
    public partial class Formulario_web1 : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            // configurar el evento 1 Gridview
            GridView1.PageIndexChanging += GridView1_PageIndexChanging;

            await cargaDatosApi();

        }

        private void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //Actualizar el indice de pagina del gridview1
            //actualizar datos del gridview1
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }

        //Método asíncrono para ejecutar  vwrtpusuario
        protected async void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            // Llamar a la función que realiza el filtrado y carga los datos
            await cargaDatosApi();
        }

        private async Task cargaDatosApi()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Construir la URL con el filtro
                    string filtro = TextBox1.Text; // Obtener el texto del TextBox
                    string apiUrl = "https://localhost:44354/check/usuario/vwRptUsuario?filtro=" + filtro;

                    HttpResponseMessage respuesta = await client.GetAsync(apiUrl);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado = await respuesta.Content.ReadAsStringAsync();
                        clsApiStatus objRespuesta = JsonConvert.DeserializeObject<clsApiStatus>(resultado);

                        JArray jsonArray = (JArray)objRespuesta.datos["vwRptUsuario"];
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonArray.ToString());

                        // Establecer el origen de datos para el GridView
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>alert('Error de conexión con webapi');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'>alert('Error inesperado ...');</script>");
            }
        }






    }
}