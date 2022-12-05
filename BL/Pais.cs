using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BL
{
    public class Pais
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.HsilvaProgramacionNcapasContext context = new DL.HsilvaProgramacionNcapasContext())
                {

                    var query = context.Pais.FromSqlRaw("PaisGetAll").ToList();
                   
                    if (query != null)  //validamos
                    {
                        result.Objects = new List<object>(); //pasamos los datos de object al objeto result.Objects

                        foreach (var row in query)
                        {
                            ML.Pais pais = new ML.Pais(); //inicializamos la clase pais

                            pais.IdPais = row.IdPais;
                            pais.Nombre = row.Nombre;

                            result.Objects.Add(pais); //se hace el boxing

                        }
                    }

                }
                result.Correct = true;
            }//codigo que puede causar una excepcion 
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al mostrar los paises" + result.Ex;

                throw;
            }//manejo de excepciones 

            return result;
        }

    }
}
