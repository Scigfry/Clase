using AccesoDatos;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UsuariosRegistrados.Alumno
{
    public partial class VerPass : Page
    {
        private GestorBBDD gestorBBDD;
        protected void Page_Load(object sender, EventArgs e)
        {
            gestorBBDD = GestorBBDD.Instancia;

            if (!IsPostBack)
            {
                CargarAsignaturas();
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

        private void CargarAsignaturas()
        {
            try
            {
                string email = Session["Email"].ToString();
                gestorBBDD.Conectar();
                string query = @"SELECT A.codigo, A.nombre FROM Asignatura A INNER JOIN GrupoClase GC ON A.codigo = GC.codigoAsig " +
                "INNER JOIN EstudianteGrupo EG ON GC.codigo = EG.grupo WHERE EG.email = @Email";
                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@Email", email)
                };
                DataTable asignaturasData = gestorBBDD.ObtenerDatosTabla(query, parametros);
                gestorBBDD.CerrarConexion();
                rptAsignaturas.DataSource = asignaturasData;
                rptAsignaturas.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable ObtenerTareas(string codigoAsignatura)
        {
            try
            {
                gestorBBDD.Conectar();
                string query = "SELECT T.codigo AS Codigo, T.descripcion AS Tarea, T.hEstimadas AS HorasEstimadas, ET.hReales AS HorasReales " +
                    "FROM TareaGenerica T INNER JOIN EstudianteTarea ET ON T.codigo = ET.codTarea WHERE ET.email = @email AND T.codAsig = @CodigoAsignatura";
                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@CodigoAsignatura", codigoAsignatura),
                    new SqlParameter("@Email", Session["Email"].ToString())
            };
                DataTable tareasData = gestorBBDD.ObtenerDatosTabla(query, parametros);
                gestorBBDD.CerrarConexion();
                return tareasData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void rptAsignaturas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView asignaturaData = (DataRowView)e.Item.DataItem;
                string codigoAsignatura = asignaturaData["Codigo"].ToString();
                Repeater rptTareas = (Repeater)e.Item.FindControl("rptTareas");
                DataTable tareasData = ObtenerTareas(codigoAsignatura);
                rptTareas.DataSource = tareasData;
                rptTareas.DataBind();
            }
        }
    }
}
