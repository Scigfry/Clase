using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace UsuariosRegistrados.Alumno
{
    public partial class InstanciarTarea : Page
    {
        private const string ConnectionString = "Server=tcp:hads2023serv.database.windows.net,1433;Initial Catalog=HADS2023;Persist Security Info=False;User ID=iayestaran009@ikasle.ehu.eus@hads2023serv;Password=Temporal#23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        protected void Page_Load(object sender, EventArgs e)
        {
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
            string codigo = Session["code"].ToString();
            string tarea = Session["tarea"].ToString();
            string horasEstimadas = Session["horasEstimadas"].ToString();
            string email = Session["Email"].ToString();

            txtUsuario.Text = email;
            txtTarea.Text = tarea;
            txtCodigo.Text = codigo;
            txtHorasEstimadas.Text = horasEstimadas;
        }

        private void CargarTareasRegistradas()
        {
            try
            {
                string email = Session["Email"].ToString();

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = @"SELECT T.codigo, T.descripcion AS Tarea, T.hEstimadas AS HorasEstimadas, ET.hReales AS HorasReales
                                    FROM TareaGenerica T
                                    INNER JOIN EstudianteTarea ET ON T.codigo = ET.codTarea
                                    WHERE ET.email = @email";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@email", email);

                    DataTable dtTareasRegistradas = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dtTareasRegistradas);

                    gvTareasRegistradas.DataSource = dtTareasRegistradas;
                    gvTareasRegistradas.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void hrefAlumno_Click(object sender, EventArgs e)
        {
            Session["code"] = null;
            Session["tarea"] = null;
            Session["horasEstimadas"] = null;
        }

        protected void btnAgregarHoras_Click(object sender, EventArgs e)
        {
            if(txtHorasReales.Text.Length == 0)
            {
                lblMensaje.Text = "Introduce un valor.";
                lblMensaje.Visible = true;
            }
            else
            {
                try
                {
                    string codigo = Session["code"].ToString();
                    string email = Session["Email"].ToString();
                    string horasReales = txtHorasReales.Text;
                    string horasEstimadas = Session["horasEstimadas"].ToString();

                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();

                        string verificarQuery = @"SELECT COUNT(*) FROM EstudianteTarea WHERE email = @email
                                                  AND codTarea = @codigo";

                        SqlCommand verificarCommand = new SqlCommand(verificarQuery, connection);
                        verificarCommand.Parameters.AddWithValue("@email", email);
                        verificarCommand.Parameters.AddWithValue("@codigo", codigo);

                        int registros = Convert.ToInt32(verificarCommand.ExecuteScalar());

                        if (registros == 0)
                        {
                            string insertQuery = @"INSERT INTO EstudianteTarea (email, codTarea, hReales, hEstimadas)
                                                   VALUES (@email, @codigo, @horasReales, @hEstimadas)";

                            SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                            insertCommand.Parameters.AddWithValue("@email", email);
                            insertCommand.Parameters.AddWithValue("@codigo", codigo);
                            insertCommand.Parameters.AddWithValue("@horasReales", horasReales);
                            insertCommand.Parameters.AddWithValue("@hEstimadas", horasEstimadas);

                            insertCommand.ExecuteNonQuery();
                            CargarTareasRegistradas();
                            lblMensaje.Text = "La actividad se ha registrado correctamente.";
                            lblMensaje.Visible = true;
                        }
                        else
                        {
                            lblMensaje.Text = "Ya has registrado horas para esta tarea.";
                            lblMensaje.Visible = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Se ha producido un error: " + ex.Message;
                    lblMensaje.Visible = true;
                    throw ex;
                }
            }
        }

    }
}
