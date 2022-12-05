using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Departamento
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.HsilvaProgramacionNcapasContext context = new DL.HsilvaProgramacionNcapasContext())
                {
                    var departamentos = context.Departamentos.FromSqlRaw("DepartamentoGetAll").ToList();
                    result.Objects = new List<object>();
                    if (departamentos != null)
                    {
                        foreach (var row in departamentos)
                        {
                            ML.Departamento departamento = new ML.Departamento();
                            departamento.IdDepartamento = row.IdDepartamento;
                            departamento.Nombre = row.Nombre;
                            departamento.Area = new ML.Area();
                            departamento.Area.IdArea = row.IdArea.Value;
                            departamento.Area.Nombre = row.NombreArea;

                            result.Objects.Add(departamento);

                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se ha podido realizar la consulta";

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
