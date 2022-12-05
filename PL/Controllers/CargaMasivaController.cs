using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace PL.Controllers
{
    public class CargaMasivaController : Controller
    {
        //para el excel, se agregaron
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public CargaMasivaController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        //cargar vista 
        [HttpGet]
        public ActionResult CargaMasiva()
        {
            ML.Result result = new ML.Result();
            return View(result);
        }

        //metodo para insertar TXT
        [HttpPost]
        public ActionResult CargaTXT()
        {
            IFormFile fileTXT = Request.Form.Files["archivoTXT"];


            if (fileTXT != null)
            {

                StreamReader Textfile = new StreamReader(fileTXT.OpenReadStream());
                string line;
                line = Textfile.ReadLine(); //se salta el enabezado del txt

                ML.Result resultErrores = new ML.Result();
                resultErrores.Objects = new List<object>();

                while ((line = Textfile.ReadLine()) != null) //se ejecuta hasta que una fila este vacia
                {
                    string[] parts = line.Split('|'); //elimina el paid de las filas

                    ML.Usuario usuario = new ML.Usuario();

                    usuario.Nombre = parts[0];
                    usuario.ApellidoPaterno = parts[1];
                    usuario.ApellidoMaterno = parts[2];
                    usuario.FechaNacimiento = parts[3];
                    usuario.Genero = parts[4];
                    usuario.UserName = parts[5];
                    usuario.Email = parts[6];
                    usuario.Password = parts[7];
                    usuario.Telefono = parts[8];
                    usuario.Celular = parts[9];
                    usuario.CURP = parts[10];

                    usuario.Rol = new ML.Rol();
                    usuario.Rol.IdRol = int.Parse(parts[11]);
                    usuario.Imagen = null;

                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Calle = parts[12];
                    usuario.Direccion.NumeroExterior = parts[13];
                    usuario.Direccion.NumeroInterior = parts[14];

                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.IdColonia = int.Parse(parts[15]);

                    ML.Result result = BL.Usuario.Add(usuario); //hacemos el boxing

                    if (!result.Correct)
                    {
                        resultErrores.Objects.Add("Error al insertar el Nombre" + usuario.Nombre +
                                                  "Error al insertar el Apellido Paterno " + usuario.ApellidoPaterno +
                                                  "Error al insertar el Apellido Materno " + usuario.ApellidoMaterno +
                                                  "Error al insertar la Fecha de Nacimiento " + usuario.FechaNacimiento +
                                                  "Error al insertar el Genero " + usuario.Genero +
                                                  "Error al insertar el UserName" + usuario.UserName +
                                                  "Error al insertar el Email" + usuario.Email +
                                                  "Error al insertar el Password" + usuario.Password +
                                                  "Error al insertar el Telefono" + usuario.Telefono +
                                                  "Error al insertar el Celular" + usuario.Celular +
                                                  "Error al insertar el CURP" + usuario.CURP +
                                                  "Error al insertar la Calle " + usuario.Direccion.Calle +
                                                  "Error al insertar el Numero Extereor " + usuario.Direccion.NumeroExterior +
                                                  "Error al insertar el Numero Interior " + usuario.Direccion.NumeroInterior +
                                                  "Error al insertar el IdColina " + usuario.Direccion.Colonia.IdColonia);
                    }
                    if (result.Correct) //validacion del objeto 
                    {
                        result.Message = "Registro Agregado";
                    }
                    else
                    {
                        result.Message = "No se pudo agregar linea";
                    }
                    if (resultErrores.Objects != null)
                    {

                        TextWriter tx = new StreamWriter(@"C:\Users\digis\Documents\Habid Alejandro Silva Cruz\ErroresLayoutUsuarios.txt");
                        foreach (string error in resultErrores.Objects)
                        {
                            tx.WriteLine(error);
                        }
                        tx.Close();
                    }

                }
            }
            return PartialView("Modal");
        }


        //metodo para insertar EXCEL
        [HttpPost]
    
        public ActionResult UsuarioCargaMasiva(ML.Usuario usuario)
        {

            IFormFile excelCargaMasiva = Request.Form.Files["FileExcel"];
            //Session 

            if (HttpContext.Session.GetString("PathArchivo") == null) //sesion nula o que no exista 
            {
                //validar el excel

                if (excelCargaMasiva.Length > 0)
                {
                    //que sea .xlsx
                    string fileName = Path.GetFileName(excelCargaMasiva.FileName);
                    string folderPath = _configuration["PathFolder:value"];
                    string extensionArchivo = Path.GetExtension(excelCargaMasiva.FileName).ToLower();
                    string extensionModulo = _configuration["TipoExcel"];

                    if (extensionArchivo == extensionModulo)
                    {
                        //crear copia
                        string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, folderPath, Path.GetFileNameWithoutExtension(fileName)) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                        if (!System.IO.File.Exists(filePath))
                        {
                            using (FileStream stream = new FileStream(filePath, FileMode.Create))
                            {
                                excelCargaMasiva.CopyTo(stream); //se crea la copia
                            }
                            //leer
                            string connectionString = _configuration["ConnectionStringExcel:value"] + filePath;
                            //convertExceltodatatable

                            ML.Result resultConvertExcel = BL.Usuario.ConvertirExceltoDataTable(connectionString);

                            if (resultConvertExcel.Correct)
                            {
                                ML.Result resultValidacion = BL.Usuario.ValidarExcel(resultConvertExcel.Objects);
                                if (resultValidacion.Objects.Count == 0)
                                {
                                    resultValidacion.Correct = true;
                                    HttpContext.Session.SetString("PathArchivo", filePath);
                                }

                                return View("CargaMasiva", resultValidacion);
                            }
                            else
                            {
                                //error al leer el archivo
                                ViewBag.Message = "Ocurrio un error al leer el arhivo";
                                return View("Modal");
                            }

                        }
                    }
                    else
                    {
                        ViewBag.Message ="El archivo no es aceptado, por favor selecciones el tipo correcto.";
                        return View("Modal");
                    }

                }



                //verificar que no tenga errores 
                //le avisamos al usuario que el excel esta mal ML.ErrorExcel 
                //crea la sesion 
            }
            else
            {
                string rutaArchivoExcel = HttpContext.Session.GetString("PathArchivo");
                string connectionString = _configuration["ConnectionStringExcel:value"] + rutaArchivoExcel;

                ML.Result resultData = BL.Usuario.ConvertirExceltoDataTable(connectionString);
                if (resultData.Correct)
                {
                    ML.Result resultErrores = new ML.Result();
                    resultErrores.Objects = new List<object>();

                    foreach (ML.Usuario usuarioItem in resultData.Objects)
                    {

                        ML.Result resultAdd = BL.Usuario.Add(usuarioItem);
                        if (!resultAdd.Correct)
                        {
                            resultErrores.Objects.Add("No se insertó el Usuario con nombre: " + usuarioItem.Nombre + " Error: " + resultAdd.Message);
                        }
                    }
                    if (resultErrores.Objects.Count > 0)
                    {

                    //  string fileError = Path.Combine(_hostingEnvironment.WebRootPath, @"\Files\logErrores.txt");
                        string fileError = _hostingEnvironment.WebRootPath + @"\Files\logErrores.txt";
                    
                        using (StreamWriter writer = new StreamWriter(fileError))
                        {
                            foreach (string ln in resultErrores.Objects)
                            {
                                writer.WriteLine(ln);
                            }
                        }
                        ViewBag.Message = "Los usuarios no han sido registrados correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "Los usuarios han sido registrados correctamente";

                    }

                }

            }
            return PartialView("Modal");

        }
   
    }
}
