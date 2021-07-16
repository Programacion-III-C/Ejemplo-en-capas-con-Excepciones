using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using CapaDeDatos;

namespace CapaLogica
{
    public static class PersonaController{
        
        public static void Guardar(string nombre, string apellido, int edad, string email){
            PersonaModelo p = new PersonaModelo();

            p.nombre = nombre;
            p.apellido = apellido;
            p.edad = edad;
            p.email = email;

            p.Guardar();
        }


        public static void Eliminar(int id)
        {
            PersonaModelo p = new PersonaModelo();
            p.Obtener(id);
            p.Eliminar();
        }


        public static void Eliminar()
        {
            PersonaModelo p = new PersonaModelo();
            List<PersonaModelo> personas = new List<PersonaModelo>();

            foreach (PersonaModelo persona in personas)
            {
                persona.Eliminar();
            }
        }

        public static DataTable Obtener(int id)
        {
            
            PersonaModelo p = new PersonaModelo();
            List<PersonaModelo> persona = new List<PersonaModelo>();

            p.Obtener(id);
            persona.Add(p);

            return prepararDataTable(persona);
        }

        public static DataTable Obtener()
        {
            try
            {
                PersonaModelo p = new PersonaModelo();
                List<PersonaModelo> personas = p.Obtener();

                if (personas == null) throw new Exception("ResultadoVacio");
                return prepararDataTable(personas);

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.SqlState + " - " + ex.Message);
                if (ex.SqlState == "28000")
                    throw new Exception("AccesoNegado");
                else
                    throw new Exception("ErroDesconocido");


            }
            
        }

        private static DataTable prepararDataTable(List<PersonaModelo> personas)
        {
            DataTable tabla = new DataTable();
            tabla.Columns.Add("id");
            tabla.Columns.Add("nombre");
            tabla.Columns.Add("apellido");
            tabla.Columns.Add("edad");
            tabla.Columns.Add("email");

            foreach (PersonaModelo persona in personas)
                tabla.Rows.Add(persona.id, persona.nombre, persona.apellido, persona.edad, persona.email);

            return tabla;

        }



    }
}
