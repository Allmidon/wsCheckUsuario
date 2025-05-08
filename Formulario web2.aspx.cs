using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wsCheckUsuario.Models;

namespace wsCheckUsuario
{
    public partial class Formulario_web2 : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            // validacion de 1er carga de página (postBack)
            if (Page.IsPostBack == false)
            {
                //llamamos el metodo
                await cargaDatosTipoUsuario();
            }

        }


        // Creación del método asíncrono para ejecutar el
        // endpoint vwTipoUsuario
        private async Task cargaDatosTipoUsuario()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Configuración de la peticion HTTP
                    string apiUrl = "https://localhost:44354/check/usuario/vwTipoUsuario";
                    // Ejecución del endpoint
                    HttpResponseMessage respuesta = await client.GetAsync(apiUrl);
                    // ---------------------------------------------------
                    // Validación de recepción de respuesta Json
                    clsApiStatus objRespuesta = new clsApiStatus();

                    // Validación del estatus OK
                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado = await respuesta.Content.ReadAsStringAsync();
                        objRespuesta = JsonConvert.DeserializeObject<clsApiStatus>(resultado);
                        // ------------------------------------------
                        JArray jsonArray = (JArray)objRespuesta.datos["vwTipoUsuario"];
                        // Convertir JArray a DataTable
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonArray.ToString());
                        // -------------------------------------------
                        // Visualización de los datos formateados DropDownList
                        DropDownList1.DataSource = dt;
                        DropDownList1.DataTextField = "descripcion";
                        DropDownList1.DataValueField = "clave";
                        DropDownList1.DataBind();
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>" +
                                       "alert('Error de conexión con el servicio');" +
                                       "</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'>" +
                               "alert('Error de la aplicación, intentar nuevamente');" +
                               "</script>");
            }
        }

        private async Task consultaUsuarioPorClave()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string filtro = HttpUtility.UrlEncode(TextBox1.Text.Trim());
                    string apiUrl = $"https://localhost:44354/check/usuario/vwRptUsuariocve?filtro={TextBox1.Text}";

                    HttpResponseMessage respuesta = await client.GetAsync(apiUrl);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado = await respuesta.Content.ReadAsStringAsync();

                        RespuestaAPI respuestaApi = JsonConvert.DeserializeObject<RespuestaAPI>(resultado);

                        if (respuestaApi != null && respuestaApi.statusExec &&
                            respuestaApi.datos != null && respuestaApi.datos.vwRptUsuario.Count > 0)
                        {
                            Usuario usuario = respuestaApi.datos.vwRptUsuario[0];

                            TextBox2.Text = usuario.USU_NOMBRE;
                            TextBox3.Text = usuario.USU_APELLIDO_PATERNO;
                            TextBox4.Text = usuario.USU_APELLIDO_MATERNO;
                            TextBox5.Text = usuario.USU_USUARIO;
                            TextBox6.Text = usuario.USU_CONTRASENA;
                            TextBox7.Text = usuario.USU_RUTA;
                            DropDownList1.SelectedValue = usuario.TIP_CVE_TIPOUSUARIO.ToString();
                        }
                        else
                        {
                            Response.Write("<script>alert('Usuario no encontrado.');</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Error al conectar con la API.');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error de aplicación: " + ex.Message + "');</script>");
            }
        }

        // Creación del método asíncrono para ejecutar el
        // endpoint spInsUsuario
        private async Task cargaDatos()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Configuración del Json que se enviará
                    String data = @"{
                                  ""nombre"":""" + TextBox2.Text + "\"," +
                                  "\"apellidoPaterno\":\"" + TextBox3.Text + "\"," +
                                  "\"apellidoMaterno\":\"" + TextBox4.Text + "\"," +
                                  "\"usuario\":\"" + TextBox5.Text + "\"," +
                                  "\"contrasena\":\"" + TextBox6.Text + "\"," +
                                  "\"ruta\":\"" + TextBox7.Text + "\"," +
                                  "\"tipo\":\"" + DropDownList1.SelectedValue + "\"" +
                                  "}";
                    // Configuración del contenido del <body> a enviar
                    HttpContent contenido = new StringContent
                                (data, Encoding.UTF8, "application/json");
                    // Ejecución de la petición HTTP
                    string apiUrl = "https://localhost:44354/check/usuario/spinsusuario";
                    // ----------------------------------------------
                    HttpResponseMessage respuesta =
                        await client.PostAsync(apiUrl, contenido);
                    // ---------------------------------------------------
                    // Validación de recepción de respuesta Json
                    clsApiStatus objRespuesta = new clsApiStatus();
                    // ---------------------------------------------------

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado =
                                await respuesta.Content.ReadAsStringAsync();
                        objRespuesta = JsonConvert.DeserializeObject<clsApiStatus>(resultado);

                        // Bandera de estatus del proceso
                        if (objRespuesta.ban == 0)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('Usuario registrado exitosamente');" +
                                           "</script>");
                            Response.Write("<script language='javascript'>" +
                                           "document.location.href='Formulario web2.aspx';" +
                                           "</script>");
                        }
                        if (objRespuesta.ban == 1)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('El nombre de usuario ya existe');" +
                                           "</script>");
                        }
                        if (objRespuesta.ban == 2)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('El usuario ya existe');" +
                                           "</script>");
                        }
                        if (objRespuesta.ban == 3)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('El tipo de usuario no existe');" +
                                           "</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>" +
                                       "alert('Error de conexión con el servicio');" +
                                       "</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'>" +
                               "alert('Error de la aplicación, intentar nuevamente');" +
                               "</script>");
            }
        }

        private async Task updateDatos()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Configuración del Json que se enviará
                    String data = @"{
                           ""cve"":""" + TextBox1.Text + "\"," +
                                   "\"nombre\":\"" + TextBox2.Text + "\"," +
                                  "\"apellidoPaterno\":\"" + TextBox3.Text + "\"," +
                                  "\"apellidoMaterno\":\"" + TextBox4.Text + "\"," +
                                  "\"usuario\":\"" + TextBox5.Text + "\"," +
                                  "\"contrasena\":\"" + TextBox6.Text + "\"," +
                                  "\"ruta\":\"" + TextBox7.Text + "\"," +
                                  "\"tipo\":\"" + DropDownList1.SelectedValue + "\"" +
                                  "}";
                    // Configuración del contenido del <body> a enviar
                    HttpContent contenido = new StringContent
                                (data, Encoding.UTF8, "application/json");
                    // Ejecución de la petición HTTP
                    string apiUrl = "https://localhost:44354/check/usuario/spUpdUsuario";
                    // ----------------------------------------------
                    HttpResponseMessage respuesta =
                        await client.PostAsync(apiUrl, contenido);
                    // ---------------------------------------------------
                    // Validación de recepción de respuesta Json
                    clsApiStatus objRespuesta = new clsApiStatus();
                    // ---------------------------------------------------

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado =
                                await respuesta.Content.ReadAsStringAsync();
                        objRespuesta = JsonConvert.DeserializeObject<clsApiStatus>(resultado);

                        // Bandera de estatus del proceso
                        if (objRespuesta.ban == 0)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('Usuario Actualizado exitosamente');" +
                                           "</script>");
                            Response.Write("<script language='javascript'>" +
                                           "document.location.href='Formulario web2.aspx';" +
                                           "</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>" +
                                       "alert('Error de conexión con el servicio');" +
                                       "</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'>" +
                               "alert('Error de la aplicación, intentar nuevamente');" +
                               "</script>");
            }
        }

        public class Usuario
        {
            public int USU_CVE_USUARIO { get; set; }
            public string USU_NOMBRE { get; set; }
            public string USU_APELLIDO_PATERNO { get; set; }
            public string USU_APELLIDO_MATERNO { get; set; }
            public string USU_USUARIO { get; set; }
            public string USU_CONTRASENA { get; set; }
            public string USU_RUTA { get; set; }
            public int TIP_CVE_TIPOUSUARIO { get; set; }
        }

        public class Datos
        {
            public List<Usuario> vwRptUsuario { get; set; }
        }

        public class RespuestaAPI
        {
            public bool statusExec { get; set; }
            public string msg { get; set; }
            public int ban { get; set; }
            public Datos datos { get; set; }
        }
        private async Task DeleteUsuario()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Configuración del Json que se enviará
                    String data = @"{
  ""cve"":""" + TextBox1.Text + @"""
}";
                    // Configuración del contenido del <body> a enviar
                    HttpContent contenido = new StringContent
                                (data, Encoding.UTF8, "application/json");
                    // Ejecución de la petición HTTP
                    string apiUrl = "https://localhost:44354/check/usuario/spDelUsuario";
                    // ----------------------------------------------
                    HttpResponseMessage respuesta =
                        await client.PostAsync(apiUrl, contenido);
                    // ---------------------------------------------------
                    // Validación de recepción de respuesta Json
                    clsApiStatus objRespuesta = new clsApiStatus();
                    // ---------------------------------------------------

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado =
                                await respuesta.Content.ReadAsStringAsync();
                        objRespuesta = JsonConvert.DeserializeObject<clsApiStatus>(resultado);

                        // Bandera de estatus del proceso
                        if (objRespuesta.ban == 0)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('Usuario Eliminado exitosamente');" +
                                           "</script>");
                            Response.Write("<script language='javascript'>" +
                                           "document.location.href='Formulario web2.aspx';" +
                                           "</script>");
                        }

                    }
                    else
                    {
                        Response.Write("<script language='javascript'>" +
                                       "alert('Error de conexión con el servicio');" +
                                       "</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'>" +
                               "alert('Error de la aplicación, intentar nuevamente');" +
                               "</script>");
            }
        }

        protected async void Button1_Click(object sender, EventArgs e)
        {
            // NOmbre
            if (TextBox2.Text == "")
            {
                Response.Write("<script language='javascript'>" +
                               "alert('El nombre está vacío');" +
                               "</script>");
            }
            else
            {
                //APELLIDO Pat
                if (TextBox3.Text == "")
                {
                    Response.Write("<script language='javascript'>" +
                                   "alert('El apellido paterno está vacío');" +
                                   "</script>");
                }
                else
                {
                    //APELLIDO MATERNO
                    if (TextBox4.Text == "")
                    {
                        Response.Write("<script language='javascript'>" +
                                       "alert('El apellido materno está vacío');" +
                                       "</script>");
                    }
                    else
                    {
                        //USUARIO
                        if (TextBox5.Text == "")
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('El usuario está vacío');" +
                                           "</script>");
                        }
                        else
                        {
                            //CONTRASEÑA
                            if (TextBox6.Text == "")
                            {
                                Response.Write("<script language='javascript'>" +
                                               "alert('La contraseña está vacía');" +
                                               "</script>");
                            }
                            else
                            {
                                //RUTA FOTO
                                if (TextBox7.Text == "")
                                {
                                    Response.Write("<script language='javascript'>" +
                                                   "alert('La ruta está vacía');" +
                                                   "</script>");
                                }
                                else
                                {
                                    //ejecucion asincrona del metodo d inserción de datos
                                    await cargaDatos();
                                }
                            }
                        }
                    }
                }
            }
        }

        protected async void Button3_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text == "")
            {
                Response.Write("<script language='javascript'>" +
                "alert('El cve está vacío');" +
                "</script>");
            }
            else
            {
                await DeleteUsuario();
            }
        }

        protected async void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            if (TextBox1.Text == "")
            {
                Response.Write("<script language='javascript'>" +
                "alert('El cve está vacío');" +
                "</script>");
            }
            else
            {
                await consultaUsuarioPorClave();
            }
        }

        protected async void Button2_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text == "")
            {
                Response.Write("<script language='javascript'>" +
                "alert('El cve está vacío');" +
                "</script>");
            }
            else
            {
                if (TextBox2.Text == "")
                {
                    Response.Write("<script language='javascript'>" +
                                   "alert('El nombre esta vacio');" +
                                   "</script>");

                }
                else
                {
                    //Apellido Paterno
                    if (TextBox3.Text == "")
                    {
                        Response.Write("<script language='javascript'>" +
                                       "alert('El A. Paterno esta vacio');" +
                                       "</script>");

                    }
                    else
                    {
                        //Apellido Materno
                        if (TextBox4.Text == "")
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('El A. Materno esta vacio');" +
                                           "</script>");

                        }
                        else
                        {
                            //Nombre 
                            if (TextBox5.Text == "")
                            {
                                Response.Write("<script language='javascript'>" +
                                               "alert('El Usuario esta vacio');" +
                                               "</script>");

                            }
                            else
                            {
                                //Nombre 
                                if (TextBox6.Text == "")
                                {
                                    Response.Write("<script language='javascript'>" +
                                                   "alert('La contraseña esta vacio');" +
                                                   "</script>");

                                }
                                else
                                {
                                    //Nombre 
                                    if (TextBox7.Text == "")
                                    {
                                        Response.Write("<script language='javascript'>" +
                                                       "alert('La ruta de la foto esta vacio');" +
                                                       "</script>");

                                    }
                                    else
                                    {
                                        //ejecucion asincrona del metodo de insercion de usuario
                                        await updateDatos();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}