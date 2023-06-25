using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UsuariosRegistrados.Profesor
{
    public partial class Profesor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar datos de dedicación media en asignaturas
                CargarDedicacionMediaAsignaturas();

                // Cargar datos de alumnos en el DropDownList
                CargarAlumnos();

                // Cargar datos de dedicación de alumnos en tareas instanciadas para el alumno seleccionado
                CargarDedicacionAlumnos(ddlAlumnos.SelectedValue);
            }
        }

        protected void CargarAlumnos()
        {
            // Obtener los alumnos disponibles desde tu fuente de datos (base de datos, lista, etc.)
            List<string> alumnos = new List<string>();

            string connectionString = "Server=tcp:hads2023serv.database.windows.net,1433;Initial Catalog=HADS2023;Persist Security Info=False;User ID=iayestaran009@ikasle.ehu.eus@hads2023serv;Password=Temporal#23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT DISTINCT(EG.email) " +
                               "FROM EstudianteGrupo EG " +
                               "JOIN ProfesorGrupo PG ON EG.grupo = PG.codigoGrupo " +
                               "WHERE PG.email = @EmailProfesor";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmailProfesor", Session["Email"].ToString());

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string emailAlumno = reader.GetString(0);
                        alumnos.Add(emailAlumno);
                    }
                }
            }

            // Asignar los alumnos al DropDownList
            ddlAlumnos.DataSource = alumnos;
            ddlAlumnos.DataBind();
        }
        protected void ddlAlumnos_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el alumno seleccionado
            string alumnoSeleccionado = ddlAlumnos.SelectedValue;

            // Cargar nuevamente los datos de dedicación de alumnos en tareas instanciadas para el alumno seleccionado
            CargarDedicacionAlumnos(alumnoSeleccionado);
        }

        protected void CargarDedicacionAlumnos(string alumnoSeleccionado)
        {
            // Obtener el email del profesor de la sesión
            string email = Session["Email"].ToString();

            // Crear la conexión a la base de datos
            string connectionString = "Server=tcp:hads2023serv.database.windows.net,1433;Initial Catalog=HADS2023;Persist Security Info=False;User ID=iayestaran009@ikasle.ehu.eus@hads2023serv;Password=Temporal#23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Crear el comando SQL
                string query = "SELECT et.CodTarea AS Codigo, tg.Descripcion AS Tarea, et.HReales AS HorasReales " +
                               "FROM EstudianteTarea et " +
                               "JOIN TareaGenerica tg ON et.CodTarea = tg.Codigo " +
                               "WHERE tg.Explotacion = 1 AND et.email = @EmailAlumno";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmailAlumno", alumnoSeleccionado);

                // Crear el adaptador de datos
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                // Crear el DataTable para almacenar los resultados
                DataTable dataTable = new DataTable();

                // Llenar el DataTable con los resultados de la consulta
                adapter.Fill(dataTable);

                // Asignar los resultados al control Repeater
                rptDedicacionTareas.DataSource = dataTable;
                rptDedicacionTareas.DataBind();

                // Calcular la media de las horas reales si hay elementos en el DataTable
                double mediaHorasReales = 0;
                if (dataTable.Rows.Count > 0)
                {
                    mediaHorasReales = dataTable.AsEnumerable().Average(row => row.Field<int>("HorasReales"));
                }

                // Asignar la media al control Label
                lblMediaHorasReales.Text = "Media de horas reales: " + mediaHorasReales.ToString("0.00");

            }
        }

        protected void CargarDedicacionMediaAsignaturas()
        {
            // Crear la conexión a la base de datos
            string connectionString = "Server=tcp:hads2023serv.database.windows.net,1433;Initial Catalog=HADS2023;Persist Security Info=False;User ID=iayestaran009@ikasle.ehu.eus@hads2023serv;Password=Temporal#23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Crear el comando SQL
                string query = "SELECT ag.Nombre AS Asignatura, AVG(tg.HEstimadas) AS DedicacionMedia " +
                               "FROM TareaGenerica tg " +
                               "JOIN Asignatura ag ON tg.CodAsig = ag.Codigo " +
                               "WHERE tg.Explotacion = 1 " +
                               "GROUP BY ag.Nombre";
                SqlCommand command = new SqlCommand(query, connection);

                // Crear el adaptador de datos
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                // Crear el DataTable para almacenar los resultados
                DataTable dataTable = new DataTable();

                // Llenar el DataTable con los resultados de la consulta
                adapter.Fill(dataTable);

                // Asignar los resultados al control Repeater
                rptDedicacionAsignaturas.DataSource = dataTable;
                rptDedicacionAsignaturas.DataBind();
            }
        }


        protected void btnMostrarContraseña_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=tcp:hads2023serv.database.windows.net,1433;Initial Catalog=HADS2023;Persist Security Info=False;User ID=iayestaran009@ikasle.ehu.eus@hads2023serv;Password=Temporal#23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            string email = Session["Email"].ToString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetPass", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", email);

                    connection.Open();
                    string password = command.ExecuteScalar()?.ToString();
                    connection.Close();

                    lblResultado.Text = $"Contraseña: {password}";
                }
            }
        }
    }
}