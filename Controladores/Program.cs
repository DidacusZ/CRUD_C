using JdbcConexionPostgresql.Dtos;
using JdbcConexionPostgresql.Servicios;
using Npgsql;

namespace JdbcConexionPostgresql
{
    /// <summary>
    /// Clase principal de la aplicación
    /// dpm
    /// </summary>
    class Program
    {
        /// <summary>
        /// Método main de la aplicación, puerta de entrada
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try {

                ConexionInterfaz conexionImpl = new ConexionImplementacion();
                ConsultasInterfaz consultas = new ConsultasImplementacion();
                MenuInterfaz menu = new MenuImplenentacion();
                NpgsqlConnection conexion = null;
                conexion = conexionImpl.generarConexionPostgresql();

                int opcion = 0;
                do
                {
                    //Console.Clear();
                    opcion = menu.Menu();
                    //Console.Clear();

                    switch (opcion)
                    {
                        //ver todos los libros
                        case 1:
                            if (conexion != null)
                            {
                                foreach (LibroDto libro in consultas.seleccionarTodosLibros(conexion))
                                    Console.WriteLine(libro.ToString());
                                Console.WriteLine("Pulsa para volver al menu...");
                            }
                            Console.WriteLine("Pulsa para volver al menu...");
                            break;

                        //ver un libro
                        case 2:
                            //do
                            //{
                                if (conexion != null)
                                {
                                    foreach (LibroDto libro in consultas.seleccionarLibro(conexion))
                                        Console.WriteLine(libro.ToString());
                                }
                            //} while (consultas.PreguntaSiNo("Quieres ver otro libro"));
                            break;

                        //borrar libro
                        case 3:
                            consultas.borrarLibro(conexion);
                            break;

                        //nuevo libro
                        case 4:
                            consultas.anyadirLibro(conexion);
                            break;
                    }

                } while (opcion != 0);

            }
            catch(Exception ex) 
            {
                Console.WriteLine("ERROR GENERAL");
            }
            

        }
    }
}