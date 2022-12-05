using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {

        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = new ML.Result(); //instanceamos la clase result 
            ML.Producto producto = new ML.Producto(); 
            producto.Departamento = new ML.Departamento();

            ML.Result resultDepartamento = BL.Departamento.GetAll();
            result = BL.Producto.GetAll(producto); //llamamos el metodo

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

        [HttpPost]
        public ActionResult GetAll(ML.Producto producto)
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

        [HttpGet]//muestra las vistas
        public ActionResult Form(int? idProducto)
        {
            ML.Producto producto = new ML.Producto();
            producto.Proveedor = new ML.Proveedor();
            producto.Departamento = new ML.Departamento();
            producto.Departamento.Area = new ML.Area();


            // ML.Result resultRol = BL.Rol.GetAll();
            // ML.Result resultPaises = BL.Pais.GetAll();

            if (idProducto == null)
            {
                //usuario.Rol.Roles = resultRol.Objects;
                //usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;
                return View(producto);
            }
            return View(producto);
        }

            [HttpPost]
        public ActionResult Form(ML.Producto producto)
        {

            ML.Result result = new ML.Result();


            if (producto.IdProducto == 0)
            {
                //ADD
                result = BL.Producto.Add(producto);
                if (result.Correct)
                {
                    ViewBag.Message = result.Message;
                }
                else
                {
                    ViewBag.Message = "Error" + result.Message;
                }
            }
            else
            {
                //Update
               // result = BL.Usuario.Update(producto);

                if (result.Correct)
                {
                    ViewBag.Massage = "Se actualizo correctamente el producto. " + result.Message;
                }
                else
                {
                    ViewBag.Massage = "Error: " + result.Message;
                }
            }

            return PartialView("Modal");
        }
    }
}
