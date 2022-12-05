using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }   
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string CURP { get; set; }
        public string Imagen { get; set; }
        public List<Object> Usuarios { get; set; } //lista de objetos 


        public ML.Rol Rol { get; set; } //es la propiedad de navegacion
        public ML.Direccion Direccion { get; set; } //propiedad para la tabla direccion

       
    }
}