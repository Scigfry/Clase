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
    public partial class InsertarTarea : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Configurar el parámetro de consulta del SqlDataSource de Asignaturas
                string emailProfesor = Session["Email"].ToString();
                SqlDataSourceAsignaturas.SelectParameters["Email"].DefaultValue = emailProfesor;
            }
        }

        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            // Obtener los valores de los controles de la página
            string codAsig = ddlAsignaturas.SelectedValue;
            string descripcion = txtDescripcion.Text;
            string horasEstimadasStr = txtHorasEstimadas.Text.Trim();
            int horasEstimadas;
            string tipoTarea = txtTipoTarea.Text;
            string codigoTarea = txtCodigo.Text;

            
            if (string.IsNullOrEmpty(codAsig) || string.IsNullOrEmpty(descripcion) || string.IsNullOrEmpty(codigoTarea) || string.IsNullOrEmpty(horasEstimadasStr))
            {
                lblError.Text = "Todos los campos son obligatorios. Por favor, complete todos los campos.";
                return;
            }

            if (!int.TryParse(horasEstimadasStr, out horasEstimadas))
            {
                lblError.Text = "El campo de horas estimadas debe ser un valor numérico.";
                return;
            }
            // Crear una nueva fila en un DataTable y asignar los valores
            DataTable dtTareas = new DataTable();
            dtTareas.Columns.Add("codigo", typeof(string));
            dtTareas.Columns.Add("descripcion", typeof(string));
            dtTareas.Columns.Add("codAsig", typeof(string));
            dtTareas.Columns.Add("hEstimadas", typeof(int));
            dtTareas.Columns.Add("tipoTarea", typeof(string));
            DataRow newRow = dtTareas.NewRow();
            newRow["codigo"] = codigoTarea;
            newRow["descripcion"] = descripcion;
            newRow["codAsig"] = codAsig;
            newRow["hEstimadas"] = horasEstimadas;
            newRow["tipoTarea"] = tipoTarea;
            dtTareas.Rows.Add(newRow);

            // Crear un objeto DataAdapter y establecer la consulta de inserción
            string connectionString = "Server=tcp:hads2023serv.database.windows.net,1433;Initial Catalog=HADS2023;Persist Security Info=False;User ID=iayestaran009@ikasle.ehu.eus@hads2023serv;Password=Temporal#23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            string insertQuery = "INSERT INTO TareaGenerica (codigo, descripcion, codAsig, hEstimadas, explotacion, tipoTarea) VALUES (@codigo, @descripcion, @codAsig, @hEstimadas, 0, @tipoTarea)";
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.InsertCommand = new SqlCommand(insertQuery, new SqlConnection(connectionString));
            dataAdapter.InsertCommand.Parameters.Add("@codigo", SqlDbType.VarChar, 50, "codigo");
            dataAdapter.InsertCommand.Parameters.Add("@descripcion", SqlDbType.VarChar, 255, "descripcion");
            dataAdapter.InsertCommand.Parameters.Add("@codAsig", SqlDbType.VarChar, 50, "codAsig");
            dataAdapter.InsertCommand.Parameters.Add("@hEstimadas", SqlDbType.Int, 0, "hEstimadas");
            dataAdapter.InsertCommand.Parameters.Add("@tipoTarea", SqlDbType.VarChar, 50, "tipoTarea");
            
            try
            {
                dataAdapter.Update(dtTareas);

                lblFeedback.Text = "La tarea ha sido registrada correctamente.";

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LimpiarCampos()
        {
            txtDescripcion.Text = string.Empty;
            txtHorasEstimadas.Text = string.Empty;
            txtCodigo.Text = string.Empty;
            txtTipoTarea.Text = string.Empty;
            lblError.Text = string.Empty;
        }
    }
}