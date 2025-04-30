using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wsCheckUsuario
{
    public partial class mpPrincipal : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Validacion de la sesión activa
            if (Session["nomUsuario"].ToString() == "" &&
                Session["usuUsuario"].ToString() == "" &&
                Session["urlUsuario"].ToString() == "" &&
                Session["rolUsuario"].ToString() == "" ) {


                //mensaje acceso denegado y enviar a wpacceso.apsx
                Response.Write("<script languaje= 'javascript'>" +
                            "alert ('Acceso Denegado...'); " +
                           "</script>");

                Response.Write("<script language='javascript'>" +
                                "document.location.href='wpAcceso.aspx';" +
                                "</script");
            }


            //Actualizacion de las etiquetas de la aplicaicon
            Label1.Text = Application["nomEmpresa"].ToString();
            Label6.Text = Session["nomUsuario"].ToString() + "( " +
                          Session["usuUsuario"].ToString() + ") - " +
                          Session["rolUsuario"].ToString();

            //fot de usuario
            Image2.ImageUrl = Session["urlUsuario"].ToString();
        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            //cerrar la sesión del usuario
            Session["nomUsuario"] = "";
            Session["urlUsuario"] = "";
            Session["usuUsuario"] = "";
            Session["rolUsuario"] = "";

            Response.Write("<script languaje= 'javascript'>" +
                            "alert ('Sesión cerrada exitosamente !'); " +
                           "</script>");

            Response.Write("<script language='javascript'>" +
                            "document.location.href='wpAcceso.aspx';" +
                            "</script");
        }
    }
}