using JdbcConexionPostgresql.Dtos;
using JdbcConexionPostgresql.Util;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdbcConexionPostgresql.Servicios
{
    /// <summary>
    /// Implementación de la interfaz de consultas a postgresql
    /// dpm
    /// </summary>
    internal class ConsultasImplementacion : ConsultasInterfaz
    {
        public void anyadirLibro(NpgsqlConnection conexion)
        {
            //INSERT INTO gbp_almacen.gbp_alm_cat_libros (titulo, autor, isbn,edicion) VALUES (@titulo,@autor,@isbn,@edicion);

            try
            {
                //Se define y ejecuta la consulta Select
                NpgsqlCommand consulta = new NpgsqlCommand("INSERT INTO gbp_almacen.gbp_alm_cat_libros (titulo, autor, isbn,edicion) VALUES (@titulo,@autor,@isbn,@edicion);", conexion);

                consulta.Parameters.AddWithValue("@titulo", CapturaString("Introduce el titulo del libro"));//pasamos los valores
                consulta.Parameters.AddWithValue("@autor", CapturaString("Introduce el autor del libro"));
                consulta.Parameters.AddWithValue("@isbn", CapturaString("Introduce el isbn del libro"));
                consulta.Parameters.AddWithValue("@edicion", CapturaEnteros("Introduce la edicion del libro",1,999999999));

                NpgsqlDataReader resultadoConsulta = consulta.ExecuteReader();//ejecutamos la query
                Console.WriteLine("[INFO] El libro se a añadido correctamente [ConsultasImplementacion-anyadirLibro]");

                while (PreguntaSiNo("Quieres añadir otro libro"))
                    anyadirLibro(conexion);

                Console.WriteLine("[INFO]  Cierre conexión y conjunto de datos [ConsultasImplementacion-anyadirLibro]");
                conexion.Close();
                resultadoConsulta.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("[ERROR] [ConsultasImplementacion-anyadirLibro] Error al ejecutar consulta : " + e);
                conexion.Close();
            }
        }

        public void borrarLibro(NpgsqlConnection conexion)
        {
            try
            {
                //Se define y ejecuta la consulta delete
                NpgsqlCommand consulta = new NpgsqlCommand("DELETE FROM gbp_almacen.gbp_alm_cat_libros WHERE titulo=@titulo ", conexion);

                consulta.Parameters.AddWithValue("@titulo", CapturaString("Introduce el titulo del libro que quieres borrar"));//pasamos el valor

                NpgsqlDataReader resultadoConsulta = consulta.ExecuteReader();//ejecutamos la query
                Console.WriteLine("[INFO-ConsultasImplementacion-borrarLibro] Libro borrado correctamente");

                while (PreguntaSiNo("Quieres borrar otro libro"))
                    borrarLibro(conexion);

                Console.WriteLine("[INFO] Cierre conexión y conjunto de datos [ConsultasImplementacion-borrarLibro]");
                conexion.Close();
                resultadoConsulta.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("[ERROR] [ConsultasImplementacion-borrarLibro] Error al ejecutar consulta: " + e);
                conexion.Close();
            }
        }

        public List<LibroDto> seleccionarLibro(NpgsqlConnection conexion)
        {

            ADto aDto = new ADto();
            List<LibroDto> listaLibros = new List<LibroDto>();
            try
            {
                //Se define y ejecuta la consulta Select de una fila en concreto
                NpgsqlCommand consulta = new NpgsqlCommand("SELECT * FROM gbp_almacen.gbp_alm_cat_libros WHERE titulo=@titulo ", conexion);

                consulta.Parameters.AddWithValue("@titulo", CapturaString("Introduce el titulo del libro que quieres ver"));//pasamos el valor

                NpgsqlDataReader resultadoConsulta = consulta.ExecuteReader();  

                //Paso de DataReader a lista de alumnoDTO
                listaLibros = aDto.readerALibroDto(resultadoConsulta);

                Console.WriteLine("[INFO] Cierre conexión y conjunto de datos [ConsultasImplementacion-seleccionarLibro]");
                conexion.Close();
                resultadoConsulta.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("[ERROR] [ConsultasImplementacion-seleccionarLibro] Error al ejecutar consulta: " + e);
                conexion.Close();
            }
            return listaLibros;
        }

        public List<LibroDto> seleccionarTodosLibros(NpgsqlConnection conexion)
        {
            ADto aDto = new ADto();
            List<LibroDto> listaLibros = new List<LibroDto>();
            try
            {
                //Se define y ejecuta la consulta Select de toda la tabla
                NpgsqlCommand consulta = new NpgsqlCommand("SELECT * FROM gbp_almacen.gbp_alm_cat_libros", conexion);
                NpgsqlDataReader resultadoConsulta = consulta.ExecuteReader();

                //Paso de DataReader a lista de alumnoDTO
                listaLibros = aDto.readerALibroDto(resultadoConsulta);

                Console.WriteLine("[INFO] Número de libros: " + listaLibros.Count() + " [ConsultasImplementacion-seleccionarTodosLibros]");

                Console.WriteLine("[INFO] Cierre conexión y conjunto de datos [ConsultasImplementacion-seleccionarTodosLibros]");
                conexion.Close();
                resultadoConsulta.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("[ERROR] [ConsultasImplementacion-seleccionarTodosLibros] Error al ejecutar consulta: " + e);
                conexion.Close();
            }
            return listaLibros;
        }

        // estos metodos tendrian que ir en la carpeta util pero como solo los uso aqui (menos PreguntaSiNo)
        // los dejo como privados
        private string CapturaString(string texto)
        {
            Console.Write("\n\t{0}: ",texto);
            return Console.ReadLine();
        }

        private int CapturaEnteros(string texto, int min, int max)//
        {

            bool correcto;
            int numeroDevuelto;

            do
            {
                Console.Write("\n\t{0}: ", texto, min, max);

                correcto = Int32.TryParse(Console.ReadLine(), out numeroDevuelto);

                if (!correcto)
                    Console.WriteLine("\n\t** Error: Valor introducido no válido **");
                else if (numeroDevuelto < min || numeroDevuelto > max)
                    Console.WriteLine("\n\t** Error: El número no está en el rango pedido **");

            } while (!correcto || numeroDevuelto < min || numeroDevuelto > max);

            return numeroDevuelto;

        }

        public bool PreguntaSiNo(string texto)
        {
            char letra;

            do
            {
                Console.Write("\n\n\t¿{0}? [si=s/no=n]: ", texto);
                letra = Console.ReadKey().KeyChar;// capturamos una pulsacion

                if (letra == 's' || letra == 'S')
                    return true;


                if (letra == 'n' || letra == 'N')
                    return false;

                Console.Write("\n\n\t**Te has equivocado, porfavor introduce un valor correcto**");

            } while (true);

        }

    }
}
