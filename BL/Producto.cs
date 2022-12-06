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
                    
                    var usuarios = context.Database.ExecuteSqlRaw($"ProductoAdd '{producto.Nombre}', {producto.PrecioUnitario}, {producto.Stock}, {producto.Proveedor.IdProveedor}, {producto.Departamento.IdDepartamento}, '{producto.Descripcion}', '{producto.Imagen}'");

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

        public static ML.Result GetById(int idProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.HsilvaProgramacionNcapasContext context = new DL.HsilvaProgramacionNcapasContext())
                {


                    var query = context.Productos.FromSqlRaw($"ProductoGetById {idProducto}").AsEnumerable().SingleOrDefault();

                    if (query != null)
                    {

                        ML.Producto producto = new ML.Producto();

                        producto.IdProducto = query.IdProducto;
                        producto.Nombre = query.Nombre;
                        producto.PrecioUnitario = query.PrecioUnitario;
                        producto.Stock = query.Stock;

                        producto.Proveedor = new ML.Proveedor();
                        producto.Proveedor.IdProveedor = query.IdProveedor.Value;
                        producto.Proveedor.Nombre = query.NombreProveedor;
                        producto.Proveedor.Telefono = query.Telefono;

                        producto.Departamento = new ML.Departamento();
                        producto.Departamento.IdDepartamento = query.IdDepartamento.Value;
                        producto.Departamento.Nombre = query.NombreDepartamento;

                        producto.Departamento.Area = new ML.Area();
                        producto.Departamento.Area.IdArea = query.IdArea;
                        producto.Departamento.Area.Nombre = query.NombreArea;

                        producto.Descripcion = query.Descripcion;
                        producto.Imagen = query.Imagen;

   

                        result.Object = producto; //boxing y unboxing
                    }

                }
                result.Correct = true;

            }//codigo que puede causar una excepcion 
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "No se pudo mostar al usuario selecionado. " + result.Ex;

                throw;
            }//manejo de excepciones 

            return result;
        }

        public static ML.Result Update(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.HsilvaProgramacionNcapasContext context = new DL.HsilvaProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"ProductoUpdate {producto.IdProducto}, '{producto.Nombre}', '{producto.PrecioUnitario}', '{producto.Stock}', '{producto.Descripcion}', '{producto.Imagen}'");

                    if (query > 0)
                    {
                        result.Message = "Se actualizo el producto correctamente.";
                    }

                }
                result.Correct = true;

            }//codigo que puede causar una excepcion 
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "No se pudo actualizar el producto." + result.Ex;

                throw;
            }//manejo de excepciones 
            return result;

        }

        public static ML.Result Delete(int IdProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.HsilvaProgramacionNcapasContext context = new DL.HsilvaProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"UsuarioDelete {IdProducto}");

                    if (query > 0)
                    {
                        result.Message = "Se elimino el productoCorrectamente.";
                    }

                }
                result.Correct = true;
            }//codigo que puede causar una excepcion 
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "No se pudo eliminar el producto" + result.Ex;

                throw;
            }//manejo de excepciones 
            return result;
        }
    }
}
