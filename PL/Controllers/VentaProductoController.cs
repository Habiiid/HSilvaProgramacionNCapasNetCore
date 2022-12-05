using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class VentaProductoController : Controller
    {
        [HttpGet]
        public ActionResult VentaProducto()
        {

            ML.Producto producto = new ML.Producto();
            producto.Proveedor = new ML.Proveedor();
            producto.Departamento = new ML.Departamento();

            ML.Result result = new ML.Result(); //instanceamos la clase result 
            ML.Result resultDepartamento = BL.Departamento.GetAll();
            result = BL.Producto.GetAll(producto); //llamamos el metodo

            //ML.Result resultProveedor = BL.Proveedor.GetAll();
            result = BL.Producto.GetAll(producto);

            if (result.Correct) //validacion 
            {
                // ML.Producto producto = new ML.Producto();
                producto.Departamento.Departamentos = resultDepartamento.Objects;
                producto.Productos = result.Objects;

                return View(producto);
            }
            else
            {
                ViewBag.Massage = "Ocurrio un error al consultar los productos. ";
                return View();
            }
        }

        [HttpPost]   //filtro busqueda libre
        public ActionResult VentaProducto(ML.Producto producto)
        {

            ML.Result result = new ML.Result(); //instanceamos la clase result 
            // producto.Departamento = new ML.Departamento();

            ML.Result resultDepartamento = BL.Departamento.GetAll();
            result = BL.Producto.GetAll(producto); //llamamos el metodo

            if (result.Correct) //validacion 
            {
                // ML.Usuario usuario = new ML.Usuario();
                producto.Productos = result.Objects;
                producto.Departamento.Departamentos = resultDepartamento.Objects;
                return View(producto);
            }
            else
            {
                ViewBag.Massage = "Ocurrio un error al consultar los usuarios. ";
                return View();
            }


        }

        //vista carrito
        public ActionResult Cart()
        {

            return View();
        }

        [HttpGet]
        public ActionResult CartPost(ML.Producto producto)
        {
            ML.VentaProducto ventaProducto = new ML.VentaProducto();
            ventaProducto.VentasProductos = new List<object>();

            if (HttpContext.Session.GetString("Producto") == null)
            {
                ventaProducto.Cantidad = ventaProducto.Cantidad = 1;
                ventaProducto.VentasProductos.Add(producto);
                HttpContext.Session.SetString("Producto", Newtonsoft.Json.JsonConvert.SerializeObject(ventaProducto.VentasProductos));
                var session = HttpContext.Session.GetString("Producto");
            }
            else
            {
                var ventaSession = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("Producto"));

                foreach (var obj in ventaSession)
                {
                    ML.Producto objProducto = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(obj.ToString());
                    ventaProducto.VentasProductos.Add(objProducto);
                }
                //var indice = ventaMateria.VentaMaterias.IndexOf(materia);
                foreach (ML.Producto venta in ventaProducto.VentasProductos.ToList())
                {
                    if (ventaProducto.IdVentaproducto == ventaProducto.IdVentaproducto)
                    {
                        ventaProducto.Cantidad = ventaProducto.Cantidad + 1;   //True                
                    }
                    else
                    {
                        ventaProducto.Cantidad = ventaProducto.Cantidad = 1; //False
                        ventaProducto.VentasProductos.Add(venta);
                    }
                }
                HttpContext.Session.SetString("Producto", Newtonsoft.Json.JsonConvert.SerializeObject(ventaProducto.VentasProductos));
            }
            if (HttpContext.Session.GetString("Producto") != null)
            {
                ViewBag.Message = "Se ha agregado la materia a tu carrito!";
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Message = "No se pudo agregar tu producto ):";
                return PartialView("Modal");
            }

        }

        [HttpGet]
        public ActionResult Cart(ML.VentaProducto ventaProducto)
        {
            if (HttpContext.Session.GetString("Producto") == null)
            {
                return View();
            }
            else
            {
                var ventaSession = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("Producto"));
                ventaProducto.VentasProductos = new List<object>();
                foreach (var obj in ventaSession)
                {
                    ML.Producto objproducto = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(obj.ToString());
                    ventaProducto.VentasProductos.Add(objproducto);
                }

            }

            return View(ventaProducto);
        }
    }
}
