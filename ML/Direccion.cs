using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
     public class Direccion
    {
        public int IdDireccion { get; set; }

        public string Calle { get; set; }

        public string NumeroExterior { get; set; }

        public string NumeroInterior { get; set; }
        
        public ML.Colonia Colonia { get; set; } //clase colonia

        //public ML.Usuario Usuario { get; set; } //clase usuario

        public List<Object> Direcciones { get; set; } //lista de objetos 
    }
}
