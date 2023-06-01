    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    namespace UsuariosRegistrados.Profesor
    {
        public partial class GestionarTareas : System.Web.UI.Page
        {
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    // Obtener el email del profesor de la sesión
                    string emailProfesor = Session["Email"].ToString();

                    // Configurar el parámetro de consulta del SqlDataSource de Asignaturas
                    SqlDataSourceAsignaturas.SelectParameters["Email"].DefaultValue = emailProfesor;

                    // Configurar el parámetro de consulta del SqlDataSource de Tareas]
                    string codigoAsignatura = ObtenerCodigoAsignatura(emailProfesor);
                    if (codigoAsignatura != null)
                    {
                        SqlDataSourceTareas.SelectParameters["CodigoAsignatura"].DefaultValue = codigoAsignatura;
                    }
                    else
                    {
                        // Manejo del escenario cuando no se puede obtener el código de asignatura
                    }
                }
            }

            protected string ObtenerCodigoAsignatura(string emailProfesor)
            {
                string codigoAsignatura = string.Empty;

                // Obtener el código de asignatura correspondiente al email del profesor
                string query = "SELECT TOP 1 codigo FROM Asignatura " +
                               "WHERE codigo IN (SELECT codigoAsig FROM GrupoClase WHERE codigo IN (SELECT codigoGrupo FROM ProfesorGrupo WHERE email = @Email))";
                using (SqlConnection connection = new SqlConnection(SqlDataSourceAsignaturas.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", emailProfesor);
                        connection.Open();
                        codigoAsignatura = (string)command.ExecuteScalar();
                    }
                }

                return codigoAsignatura;
            }

            protected void GridViewTareas_RowUpdating(object sender, GridViewUpdateEventArgs e)
            {
                GridViewRow row = GridViewTareas.Rows[e.RowIndex];

                // Obtener los nuevos valores de los BoundFields a través de la propiedad NewValues
                string descripcion = e.NewValues["descripcion"].ToString();
                int horasEstimadas = Convert.ToInt32(e.NewValues["hEstimadas"]);
                bool activa = Convert.ToBoolean(e.NewValues["explotacion"]);
                string tipoTarea = e.NewValues["tipoTarea"].ToString();

                Label lblCodigo = row.FindControl("lblEditCodigo") as Label;
                string codigo = lblCodigo.Text;

                // Actualizar los valores en el SqlDataSource
                SqlDataSourceTareas.UpdateParameters["Descripcion"].DefaultValue = descripcion;
                SqlDataSourceTareas.UpdateParameters["HorasEstimadas"].DefaultValue = horasEstimadas.ToString();
                SqlDataSourceTareas.UpdateParameters["Activa"].DefaultValue = activa.ToString();
                SqlDataSourceTareas.UpdateParameters["TipoTarea"].DefaultValue = tipoTarea;
                SqlDataSourceTareas.UpdateParameters["CodigoTarea"].DefaultValue = codigo;

                // Actualizar la fila
                SqlDataSourceTareas.Update();

                // Finalizar el modo de edición y actualizar el GridView
                GridViewTareas.EditIndex = -1;
                GridViewTareas.DataBind();
            }

            protected void btnNuevaTarea_Click(object sender, EventArgs e)
            {
                // Redireccionar a la página de creación de nueva tarea
                try
                {
                    Response.Redirect("InsertarTarea.aspx");
                }
                catch (System.Threading.ThreadAbortException i)
                {
                    //Es una respuesta normal por lo que 0 problemas
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message, ex);
                }
            }

            protected void hrefLogout_OnClick(object sender, EventArgs e)
            {
                // Redireccionar a la página de creación de nueva tarea
                Session["Email"] = null;
                Session["Tipo"] = null;
            }
        }
    }