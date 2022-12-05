using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Producto
    {
        public static ML.Result GetAll(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.HsilvaProgramacionNcapasContext context = new DL.HsilvaProgramacionNcapasContext())
                {
                    producto.Departamento.IdDepartamento = (producto.Departamento.IdDepartamento == null) ? 0 : producto.Departamento.IdDepartamento; //operador ternario
                    var productos = context.Productos.FromSqlRaw($"ProductoGetAll '{producto.Nombre}', {producto.Departamento.IdDepartamento}").ToList();
                    result.Objects = new List<object>();
                    if (productos != null)
                    {
                        foreach (var row in productos)
                        {
                            ML.Producto productobj = new ML.Producto();
                            productobj.IdProducto = row.IdProducto;
                            productobj.Nombre = row.Nombre;
                            productobj.PrecioUnitario= row.PrecioUnitario;
                            productobj.Stock = row.Stock;
      
                            productobj.Proveedor = new ML.Proveedor();
                            productobj.Proveedor.IdProveedor = row.IdProveedor.Value;
                            productobj.Proveedor.Nombre = row.NombreProveedor;
                            productobj.Proveedor.Telefono = row.Telefono;

                            productobj.Departamento = new ML.Departamento();
                            productobj.Departamento.IdDepartamento = row.IdDepartamento.Value;
                            productobj.Departamento.Nombre = row.NombreDepartamento;

                            productobj.Departamento.Area = new ML.Area();
                            productobj.Departamento.Area.IdArea = row.IdArea;
                            productobj.Departamento.Area.Nombre= row.NombreArea;
                            
                            productobj.Descripcion = row.Descripcion;

                            result.Objects.Add(productobj);

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


        public static ML.Result Add(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.HsilvaProgramacionNcapasContext context = new DL.HsilvaProgramacionNcapasContext())
                {
                    
                    var usuarios = context.Database.ExecuteSqlRaw($"ProductoAdd '{producto.Nombre}', {producto.PrecioUnitario}, {producto.Stock}, ,{producto.Proveedor.IdProveedor}, {producto.Departamento.IdDepartamento}, '{producto.Descripcion}'");

                    if (usuarios > 0)
                    {
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
