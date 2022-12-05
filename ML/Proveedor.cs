using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Proveedor
    {

        public int IdProveedor { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }

        //lista
        public List<Proveedor> Proveedors { get; set; }
    }
}
