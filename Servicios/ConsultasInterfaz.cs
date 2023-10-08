using JdbcConexionPostgresql.Dtos;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdbcConexionPostgresql.Servicios
{
    /// <summary>
    /// Interfaz que define las consultas a postgresql
    /// dpm
    /// </summary>
    internal interface ConsultasInterfaz
    {
        /// <summary>
        /// Métdo que lee todos los registros de la bas ede datos de Postgresql
        /// </summary>
        /// <param name="conexion"></param>
        /// <returns></returns>
        public List<LibroDto> seleccionarTodosLibros(NpgsqlConnection conexion);

        /// <summary>
        /// muestra el libro elegido por el titulo
        /// </summary>
        /// <param name="conexion"></param>
        /// <returns></returns>
        public List<LibroDto> seleccionarLibro(NpgsqlConnection conexion);

        /// <summary>
        /// borra el libro elegido por el titulo
        /// </summary>
        /// <param name="conexion"></param>
        public void borrarLibro(NpgsqlConnection conexion);

        /// <summary>
        /// añade un libro
        /// </summary>
        /// <param name="conexion"></param>
        public void anyadirLibro(NpgsqlConnection conexion);

        /// <summary>
        /// booleano de si o no  (tendria que ir en la carpeta util junto a CapturaEnetros pero solo lo uso una vez)
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        bool PreguntaSiNo(string texto);
    }
}
