using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Rol
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result(); //inicializamos 
            try
            {
                using (DL.HsilvaProgramacionNcapasContext context = new DL.HsilvaProgramacionNcapasContext())
                {
                    var query = context.Rols.FromSqlRaw("RolGetAll").ToList();

                    result.Objects = new List<object>(); //inicializamos la lista

                    if (query != null)
                    {    

                        foreach (var row in query) //se pasa la lista al objeto row
                        {
                            ML.Rol rol = new ML.Rol(); //instanciamos

                            rol.IdRol = row.IdRol;
                            rol.Nombre = row.Nombre;

                            result.Objects.Add(rol);
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se ha podido realizar la consulta";

                    }
                }

                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al consultar la tabla de rol" + result.Ex;
                throw;
            }
            return result; //regresa el valor del metodo
        }
    }
}
