using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {

        [HttpGet]
        public ActionResult GetAll()
        {
           
            ML.Result result = new ML.Result(); //instanceamos la clase result
            ML.Usuario usuario = new ML.Usuario();                                   
            result = BL.Usuario.GetAll(usuario); //llamamos el metodo

            if (result.Correct) //validacion 
            {
               // ML.Usuario usuario = new ML.Usuario();
                usuario.Usuarios = result.Objects;
                return View(usuario);
            }
            else
            {
                ViewBag.Massage = "Ocurrio un error al consultar los usuarios. ";
                return View();
            }
        }
        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {

            ML.Result result = new ML.Result(); //instanceamos la clase result 
            result = BL.Usuario.GetAll(usuario); //llamamos el metodo

            if (result.Correct) //validacion 
            {
               // ML.Usuario usuario = new ML.Usuario();
                usuario.Usuarios = result.Objects;
                return View(usuario);
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
        public ActionResult Form(ML.Usuario usuario)
        {
            IFormFile image = Request.Form.Files["IFImagen"];
            if (image != null)
            {
                byte[] ImagenBytes = ConvertToBytes(image);
                usuario.Imagen = Convert.ToBase64String(ImagenBytes);
            }
            ML.Result result = new ML.Result();

            //validaciones backend
            if (!ModelState.IsValid)
            {
                usuario.Rol = new ML.Rol();
                ML.Result resultRoles = BL.Rol.GetAll();
                usuario.Rol.Roles = resultRoles.Objects;

                
                ML.Result resultPaises = BL.Pais.GetAll();
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;

                ML.Result resultEstados = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;

                ML.Result resultMunicipios = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Objects;

                ML.Result resultColonias = BL.Colonia.GetbyIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                usuario.Direccion.Colonia.Colonias = resultColonias.Objects;

                return View(usuario);
            }
            else { 

                if (usuario.IdUsuario == 0)
                {
                    //ADD
                    result = BL.Usuario.Add(usuario);
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
                    result = BL.Usuario.Update(usuario);

                    if (result.Correct)
                    {
                        ViewBag.Massage = "Se actualizo correctamente el usuario. " + result.Message;
                    }
                    else
                    {
                        ViewBag.Massage = "Error: " + result.Message;
                    }
                }
                return PartialView("Modal");
            }
            return View(usuario);
        }

        public ActionResult Delete(int idUsuario)
        {
            ML.Result result = BL.Usuario.Delete(idUsuario);

            if (idUsuario != null)
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
                return Redirect("/Usuario/GetAll");
            }
            return PartialView("Modal");
        }

        //cambio status
        public JsonResult CambiarStatus(int idUsuario, bool status)
        {
            ML.Result result = BL.Usuario.ChangeStatus(idUsuario, status);

            return Json(result);
        }

        //imagen

        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }

        //JSON  DROP DOWN LIST EN CASCADA 

        public JsonResult GetEstado(int IdPais)
        {
            var result = BL.Estado.GetByIdPais(IdPais);

            return Json(result.Objects);
        }

        public JsonResult GetMunicipio(int IdEstado)
        {
            var result = BL.Municipio.GetByIdEstado(IdEstado);

            return Json(result.Objects);
        }

        public JsonResult GetColonia(int IdMunicipio)
        {
            var result = BL.Colonia.GetbyIdMunicipio(IdMunicipio);

            return Json(result.Objects);
        }
    }
}


