using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UsuariosRegistrados.Alumno
{
    public partial class VerTareasEstudiante : System.Web.UI.Page
    {
        private GestorBBDD gestorBBDD;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorBBDD = GestorBBDD.Instancia;

            if (!IsPostBack)
            {
                CargarAsignaturas();
                CargarTareas();
            }
        }

        // Resto del código...

        private void CargarAsignaturas()
        {
            try
            {
                gestorBBDD.Conectar();

                SqlDataReader reader = gestorBBDD.ObtenerAsignaturas(Session["Email"].ToString());

                ddlAsignaturas.DataSource = reader;
                ddlAsignaturas.DataTextField = "nombre";
                ddlAsignaturas.DataValueField = "codigo";
                ddlAsignaturas.DataBind();

                gestorBBDD.CerrarConexion();
                reader.Close();
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra durante la carga de asignaturas
                // Puedes mostrar un mensaje de error o realizar cualquier otra acción necesaria
                // Aquí simplemente lanzamos la excepción nuevamente
                throw ex;
            }
        }

        private void CargarTareas()
        {
            try
            {
                gestorBBDD.Conectar();
                SqlDataReader reader = gestorBBDD.ObtenerTareas(Session["Email"].ToString(), ddlAsignaturas.SelectedValue);

                gvTareas.DataSource = reader;
                gvTareas.DataBind();

                gestorBBDD.CerrarConexion();
                reader.Close();

                if (gvTareas.Rows.Count == 0)
                {
                    lblMensaje.Text = "No te quedan tareas pendientes.";
                    lblMensaje.Visible = true;
                }
                else
                {
                    lblMensaje.Visible = false;
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra durante la carga de tareas
                // Puedes mostrar un mensaje de error o realizar cualquier otra acción necesaria
                // Aquí simplemente lanzamos la excepción nuevamente
                throw ex;
            }
        }

        protected void ddlAsignaturas_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarTareas();
        }

        protected void gvTareas_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvTareas.SelectedRow;
            string codigo = row.Cells[0].Text;
            string tarea = row.Cells[1].Text;
            string hEstimadas = row.Cells[2].Text;

            Session["code"] = codigo;
            Session["tarea"] = tarea;
            Session["horasEstimadas"] = hEstimadas;

            // Redireccionar a InstanciarTarea.aspx pasando los parámetros necesarios
            Response.Redirect(string.Format("InstanciarTarea.aspx?codigo={0}", row.RowIndex));
        }

        protected void hrefLogout_OnClick(object sender, EventArgs e)
        {
            // Limpiar las variables de sesión
            Session["Email"] = null;
        }
    }
}