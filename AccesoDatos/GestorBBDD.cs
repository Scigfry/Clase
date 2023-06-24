using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class GestorBBDD
    {
        private static GestorBBDD instancia;
        private SqlConnection connection;
        private string connectionString = "Server=tcp:hads2023serv.database.windows.net,1433;Initial Catalog=HADS2023;Persist Security Info=False;User ID=iayestaran009@ikasle.ehu.eus@hads2023serv;Password=Temporal#23;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


        private GestorBBDD()
        {
            connection = new SqlConnection(connectionString);
        }

        public static GestorBBDD Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new GestorBBDD();
                }
                return instancia;
            }
        }

        public void Conectar()
        {
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                throw new ConnectionException("Error al conectarse a la BBDD.", ex);
            }
        }

        public void CerrarConexion()
        {
            connection.Close();
        }

        public int InsertarUsuario(string email, string izena, string abizena, string galderaEzkutua, string erantzuna, int egiztatzekoZenbakia, string pasahitza)
        {
            string query = "INSERT INTO Erabiltzaileak (email, izena, abizena, galderaEzkutua, erantzuna, egiaztatzeZenbakia, egiaztatua, pasahitza) " +
                      "VALUES (@Email, @Izena, @Abizena, @GalderaEzkutua, @Erantzuna, @EgiaztatzeZenbakia, @Egiaztatua, @Pasahitza)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Izena", izena);
                command.Parameters.AddWithValue("@Abizena", abizena);
                command.Parameters.AddWithValue("@GalderaEzkutua", galderaEzkutua);
                command.Parameters.AddWithValue("@Erantzuna", erantzuna);
                command.Parameters.AddWithValue("@EgiaztatzeZenbakia", egiztatzekoZenbakia);
                command.Parameters.AddWithValue("@Egiaztatua", false);
                command.Parameters.AddWithValue("@Pasahitza", pasahitza);

                try
                {
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new DatabaseException("Error al insertar el usuario.", ex);
                }
            }
        }

        public SqlDataReader ObtenerUsuario(string email)
        {
            string query = "SELECT * FROM Erabiltzaileak WHERE email = @Email";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);

                try
                {
                    return command.ExecuteReader();
                }
                catch (Exception ex)
                {
                    throw new DatabaseException("Error al obtener el usuario.", ex);
                }
            }
        }

        public SqlDataReader ObtenerUsuarioVerificado(string email)
        {
            string query = "SELECT * FROM Erabiltzaileak WHERE email = @Email AND egiaztatua = 1";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);

                try
                {
                    return command.ExecuteReader();
                }
                catch (Exception ex)
                {
                    throw new DatabaseException("Error al obtener el usuario.", ex);
                }
            }
        }

        public SqlDataReader ObtenerAsignaturas(string email)
        {
            string query = "SELECT A.codigo, A.nombre FROM Asignatura A INNER JOIN GrupoClase GC ON A.codigo = GC.codigoAsig " +
                "INNER JOIN EstudianteGrupo EG ON GC.codigo = EG.grupo WHERE EG.email = @Email";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);

                try
                {
                    return command.ExecuteReader();
                }
                catch (Exception ex)
                {
                    throw new DatabaseException("Error al obtener el usuario.", ex);
                }
            }
        }

        public SqlDataReader ObtenerTareas(string email, String codigoAsignatura)
        {
            string query = "SELECT codigo, descripcion, hEstimadas FROM TareaGenerica WHERE codAsig = @CodigoAsignatura " +
                "AND explotacion = 1 AND codigo NOT IN(SELECT codTarea FROM EstudianteTarea WHERE email = @Email)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@CodigoAsignatura", codigoAsignatura);

                try
                {
                    return command.ExecuteReader();
                }
                catch (Exception ex)
                {
                    throw new DatabaseException("Error al obtener el usuario.", ex);
                }
            }
        }

        public int ComprobarUsuario(string email, string egiaztatzeZenbakia)
        {
            string query = "UPDATE Erabiltzaileak SET egiaztatua = 1 WHERE email = @Email AND egiaztatzeZenbakia = @egiaztatzeZenbakia";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@egiaztatzeZenbakia", egiaztatzeZenbakia);

                try
                {
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new DatabaseException("Error al comprobar el usuario.", ex);
                }
            }
        }

        public int ModificarContraseñaUsuario(string email, string nuevaContraseña)
        {
            string query = "UPDATE Erabiltzaileak SET pasahitza = @NuevaContraseña WHERE email = @Email";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@NuevaContraseña", nuevaContraseña);
                command.Parameters.AddWithValue("@Email", email);

                try
                {
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new DatabaseException("Error al modificar la contraseña del usuario.", ex);
                }
            }
        }


        public int Login(string email, string pwd)
        {
            int usuario = -1;
            string query = "SELECT * FROM Erabiltzaileak WHERE email = @Email AND pasahitza = @pwd";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@pwd", pwd);

                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        // El usuario existe
                        usuario = 1;
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new DatabaseException("Error al obtener el usuario.", ex);
                }
            }
            return usuario;
        }

        public string ObtenerPregunta(string email)
        {
            string pregunta = "";
            string query = "SELECT galderaEzkutua FROM Erabiltzaileak WHERE email = @Email";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);

                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pregunta = reader.GetString(0);
                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new DatabaseException("Error al obtener la pregunta oculta.", ex);
                }
            }

            return pregunta;
        }
        public string VerificarRespuesta(string email, string erantzuna)
        {
            string pwd = "";
            string query = "SELECT pasahitza FROM Erabiltzaileak WHERE email = @Email AND erantzuna = @Erantzuna";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Erantzuna", erantzuna);

                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        pwd = reader.GetString(0);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new DatabaseException("Error al obtener la contraseña.", ex);
                }
            }
            return pwd;
        }

        public bool ValidateOldPassword(string email, string oldPassword)
        {
            string query = "SELECT * FROM Erabiltzaileak WHERE Email = @Email AND pasahitza = @pwd";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@pwd", oldPassword);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    return true;
                }
            }
            return false;
        }

        public void UpdateUserPassword(string email, string newPassword)
        {
            string query = "UPDATE Erabiltzaileak SET pasahitza = @NewPassword WHERE Email = @Email";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@NewPassword", newPassword);
                command.Parameters.AddWithValue("@Email", email);
                command.ExecuteNonQuery();
            }
        }


        public SqlDataReader ObtenerDatos(string query, SqlParameter[] parametros)
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddRange(parametros);

            try
            {
                return command.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error al obtener los datos.", ex);
            }
        }

        public object ObtenerEscalar(string query, SqlParameter[] parametros)
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddRange(parametros);

            try
            {
                return command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error al obtener el valor escalar.", ex);
            }
        }

        public int EjecutarQuery(string query, SqlParameter[] parametros)
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddRange(parametros);

            try
            {
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error al ejecutar la consulta.", ex);
            }
        }

        public DataTable ObtenerDatosTabla(string query, SqlParameter[] parametros)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parametros != null)
                    {
                        command.Parameters.AddRange(parametros);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(tabla);
                    }
                }
            }

            return tabla;
        }


    }

    public class ConnectionException : Exception
    {
        public ConnectionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class DatabaseException : Exception
    {
        public DatabaseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}


