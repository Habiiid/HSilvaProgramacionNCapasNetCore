using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Estado
    {

        public static ML.Result GetByIdPais(int idPais) //creacion del metodo
        {
            ML.Result result = new ML.Result(); //instancia
            try
            {
                using (DL.HsilvaProgramacionNcapasContext context = new DL.HsilvaProgramacionNcapasContext())
                {
                    var query = context.Estados.FromSqlRaw($"EstadoGetByIdPais {idPais}").ToList();
                    result.Objects = new List<object>();
                    if (query != null) //validacion
                    {
                        foreach (var row in query)
                        {
                            ML.Estado estado = new ML.Estado(); //creacion de objeto estado
                            estado.IdEstado = row.IdEstado;
                            estado.Nombre = row.Nombre;

                            estado.Pais = new ML.Pais();
                            estado.Pais.IdPais = idPais;


                            result.Objects.Add(estado);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Ocurrio un error al mostrar los estados";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
