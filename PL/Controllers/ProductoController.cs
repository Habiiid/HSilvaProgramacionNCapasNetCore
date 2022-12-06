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
        public ActionResult Form(int? idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

            ML.Result resultRol = BL.Rol.GetAll();
            ML.Result resultPaises = BL.Pais.GetAll();

            if (idUsuario == null)
            {
                usuario.Rol.Roles = resultRol.Objects;
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;
                return View(usuario);
            }
            else
            {
                //GetById
                ML.Result result = BL.Usuario.GetById(idUsuario.Value);

                if (result.Correct)
                {
                    usuario = (ML.Usuario)result.Object;

                    usuario.Rol.Roles = resultRol.Objects;

                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;

                    ML.Result resultEstados = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;

                    ML.Result resultMunicipios = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                    usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Objects;

                    ML.Result resultColonias = BL.Colonia.GetbyIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                    usuario.Direccion.Colonia.Colonias = resultColonias.Objects;
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error";
                }
                return View(usuario);
            }
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
                result = BL.Producto.Update(producto);

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

        public ActionResult Delete(int idProducto)
        {
            ML.Result result = BL.Usuario.Delete(idProducto);

            if (idProducto != null)
            {
                if (result.Correct)
                {
                    ViewBag.Massage = result.Message;
                }
                else
                {
                    ViewBag.Massage = "Error: " + result.Message;
                }
            }
            else
            {
                return Redirect("/Producto/GetAll");
            }
            return PartialView("Modal");
        }

        //imagen
        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
    }
}
