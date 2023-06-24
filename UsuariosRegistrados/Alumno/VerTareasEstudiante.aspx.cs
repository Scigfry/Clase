using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace UsuariosRegistrados.Alumno
{
    public partial class VerTareasEstudiante : System.Web.UI.Page
    {
        private string connectionString = "Server=tcp:hads2023serv.database.windows.net,1433;Initial Catalog=HADS2023;Persist Security Info=False;User ID=iayestaran009@ikasle.ehu.eus@hads2023serv;Password=Temporal#23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarAsignaturas();
                CargarTareas();
            }
        }

        private void CargarAsignaturas()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT A.codigo, A.nombre FROM Asignatura A INNER JOIN GrupoClase GC ON A.codigo = GC.codigoAsig " +
                "INNER JOIN EstudianteGrupo EG ON GC.codigo = EG.grupo WHERE EG.email = @Email", connection);
                    command.Parameters.AddWithValue("@Email", Session["Email"].ToString());
                    SqlDataReader reader = command.ExecuteReader();
                    ddlAsignaturas.DataSource = reader;
                    ddlAsignaturas.DataTextField = "nombre";
                    ddlAsignaturas.DataValueField = "codigo";
                    ddlAsignaturas.DataBind();
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra durante la carga de asignaturas
                throw ex;
            }
        }

        private void CargarTareas()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT codigo, descripcion, hEstimadas FROM TareaGenerica WHERE codAsig = @CodigoAsignatura " +
                "AND explotacion = 1 AND codigo NOT IN(SELECT codTarea FROM EstudianteTarea WHERE email = @Email)", connection);
                    command.Parameters.AddWithValue("@Email", Session["Email"].ToString());
                    command.Parameters.AddWithValue("@CodigoAsignatura", ddlAsignaturas.SelectedValue);
                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    gvTareas.DataSource = dataTable;
                    gvTareas.DataBind();
                    reader.Close();
                }

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
