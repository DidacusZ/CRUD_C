using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdbcConexionPostgresql.Servicios
{
    internal class MenuImplenentacion:MenuInterfaz
    {
        /// <summary>
        /// muestra el menu
        /// </summary>
        /// <returns></returns>
        public int Menu()
        {
            Console.WriteLine("\n\t1. Ver todos los libros");
            Console.WriteLine("\t2. Ver un libro");
            Console.WriteLine("\t3. Borrar un libro");
            Console.WriteLine("\t4. Añadir un libro");
            Console.WriteLine("\t0. Salir");
            Console.Write("\n\tElige una opcion: ");
            return Console.ReadKey(true).KeyChar - '0';
        }

    }
}
