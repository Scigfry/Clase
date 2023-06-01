using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UsuariosRegistrados.Alumno
{
    public partial class InstanciarTarea : System.Web.UI.Page
    {
        private GestorBBDD gestorBBDD;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorBBDD = GestorBBDD.Instancia;

            if (!IsPostBack)
            {
                CargarTareaGenerica();
                CargarTareasRegistradas();
            }

            string notification = Session["Notification"] as string;
            if (!string.IsNullOrEmpty(notification))
            {
                lblMensaje.Text = notification;
                lblMensaje.Visible = true;
                btnAgregarHoras.Enabled = false;
                Session["Notification"] = null;
            }
        }

        private void CargarTareaGenerica()
        {
            // Obtener los valores de la sesión
            string codigo = Session["code"].ToString();
            string tarea = Session["tarea"].ToString();
            string horasEstimadas = Session["horasEstimadas"].ToString();
            string email = Session["Email"].ToString();

            // Mostrar los valores en los controles de la página
            txtUsuario.Text = email;
            txtTarea.Text = tarea;
            txtCodigo.Text = codigo;
            txtHorasEstimadas.Text = horasEstimadas;
        }

        private void CargarTareasRegistradas()
        {
            try
            {
                // Obtener el email de la sesión
                string email = Session["Email"].ToString();

                gestorBBDD.Conectar();

                // Consultar las tareas registradas por el estudiante para esta asignatura
                string query = @"SELECT T.codigo, T.descripcion AS Tarea, T.hEstimadas AS HorasEstimadas, ET.hReales AS HorasReales
                                FROM TareaGenerica T
                                INNER JOIN EstudianteTarea ET ON T.codigo = ET.codTarea
                                WHERE ET.email = @email";

                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@email", email)
                };

                SqlDataReader reader = gestorBBDD.ObtenerDatos(query, parametros);

                gvTareasRegistradas.DataSource = reader;
                gvTareasRegistradas.DataBind();

                gestorBBDD.CerrarConexion();
                reader.Close();
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra durante la carga de tareas registradas
                // Puedes mostrar un mensaje de error o realizar cualquier otra acción necesaria
                // Aquí simplemente lanzamos la excepción nuevamente
                throw ex;
            }
        }

        protected void hrefAlumno_Click(object sender, EventArgs e)
        {
            // Limpiar las variables de sesión
            Session["code"] = null;
            Session["tarea"] = null;
            Session["horasEstimadas"] = null;
        }

        protected void btnAgregarHoras_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener los valores de la sesión y el email del estudiante
                string codigo = Session["code"].ToString();
                string email = Session["Email"].ToString();
                string horasReales = txtHorasReales.Text;
                string horasEstimadas = Session["horasEstimadas"].ToString();

                gestorBBDD.Conectar();

                // Verificar si el estudiante ya ha registrado horas para esta tarea
                string verificarQuery = @"SELECT COUNT(*) FROM EstudianteTarea WHERE email = @email
                                          AND codTarea = @codigo";

                SqlParameter[] verificarParametros = new SqlParameter[]
                {
                    new SqlParameter("@email", email),
                    new SqlParameter("@codigo", codigo)
                };

                int registros = Convert.ToInt32(gestorBBDD.ObtenerEscalar(verificarQuery, verificarParametros));

                if (registros == 0)
                {
                    // Insertar el registro en EstudianteTarea
                    string insertQuery = @"INSERT INTO EstudianteTarea (email, codTarea, hReales, hEstimadas)
                                           VALUES (@email, @codigo, @horasReales, @hEstimadas)";

                    SqlParameter[] insertParametros = new SqlParameter[]
                    {
                        new SqlParameter("@email", email),
                        new SqlParameter("@codigo", codigo),
                        new SqlParameter("@horasReales", horasReales),
                        new SqlParameter("@hEstimadas", horasEstimadas)
                    };

                    gestorBBDD.EjecutarQuery(insertQuery, insertParametros);
                }
                /* Me di cuenta luego que no debería de poder actualizar, pero dejo esto comentado
                else
                {
                    // Actualizar las horas reales en el registro existente de EstudianteTarea
                    string updateQuery = @"UPDATE EstudianteTarea SET hReales = @horasReales
                                           WHERE email = @email
                                           AND codTarea = @codigo";

                    SqlParameter[] updateParametros = new SqlParameter[]
                    {
                        new SqlParameter("@horasReales", horasReales),
                        new SqlParameter("@email", email),
                        new SqlParameter("@codigo", codigo)
                    };

                    gestorBBDD.EjecutarQuery(updateQuery, updateParametros);
                }
                */
                //Actualizamos la página
                gestorBBDD.CerrarConexion();
                Session["Notification"] = "¡La actividad se ha registrado correctamente!";
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            catch (System.Threading.ThreadAbortException i)
            {
                //Es una respuesta normal por lo que 0 problemas
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Se ha producido un error" + ex.Message;
                throw ex;
            }
        }


    }
}