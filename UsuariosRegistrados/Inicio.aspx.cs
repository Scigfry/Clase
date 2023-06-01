using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UsuariosRegistrados
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        GestorBBDD gestor;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                DataView dv = (DataView)SqlDataSourceLogin.Select(DataSourceSelectArguments.Empty);
                if (dv != null && dv.Count > 0)
                {
                    string userType = dv[0]["Tipo"].ToString();
                    Session["Email"] = txtEmail.Text;
                    Session["Tipo"] = userType;
                    if (userType == "Alumno")
                    {
                        Response.Redirect("~/Alumno/alumno.aspx"); // Redirige a la página del Alumno
                    }
                    else if (userType == "Profesor")
                    {
                        Response.Redirect("~/Profesor/Profesor.aspx"); // Redirige a la página del Profesor
                    }
                }
                else
                {
                    lblMensaje.Text = "Credenciales inválidas. Por favor, inténtalo de nuevo.";
                }
            }
            catch (System.Threading.ThreadAbortException i)
            {
                //Es una respuesta normal por lo que 0 problemas
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message, ex);
            } 
        }
    }
}