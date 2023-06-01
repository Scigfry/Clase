using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;

namespace ParaProbarAccesoDatos
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                AccesoDatos.GestorBBDD gestor = AccesoDatos.GestorBBDD.Instancia;

                // Conectar a la base de datos
                gestor.Conectar();

                // Insertar un usuario
                int registrosAfectados = gestor.InsertarUsuario("correo@ejemplo.com", "Jon", "Bravo", "Pregunta secreta", "Respuesta secreta", 123, "contraseña");
                Console.WriteLine("Registros afectados: " + registrosAfectados);

                // Obtener un usuario
                string email = "correo@ejemplo.com";
                var reader = gestor.ObtenerUsuario(email);
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Email: " + reader["email"] + ", Nombre: " + reader["izena"]);
                    }
                    reader.Close();
                }

                // Comprobar un usuario
                registrosAfectados = gestor.ComprobarUsuario(email, "123");
                Console.WriteLine("Registros afectados: " + registrosAfectados);

                // Modificar la contraseña de un usuario
                string nuevaContraseña = "nueva_contraseña";
                registrosAfectados = gestor.ModificarContraseñaUsuario(email, nuevaContraseña);
                Console.WriteLine("Registros afectados: " + registrosAfectados);

                // Cerrar la conexión
                gestor.CerrarConexion();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.WriteLine("Presione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
