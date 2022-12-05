using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.OleDb;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetAll(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.HsilvaProgramacionNcapasContext context = new DL.HsilvaProgramacionNcapasContext())
                {
                    var usuarios = context.Usuarios.FromSqlRaw($"UsuarioGetAll '{usuario.Nombre}','{usuario.ApellidoPaterno}'").ToList();
                    result.Objects = new List<object>();
                    if (usuarios != null)
                    {
                        foreach (var row in usuarios)
                        {
                            ML.Usuario usuariobj = new ML.Usuario();
                            usuariobj.IdUsuario = row.IdUsuario;
                            usuariobj.Nombre = row.Nombre;
                            usuariobj.ApellidoPaterno = row.ApellidoPaterno;
                            usuariobj.ApellidoMaterno = row.ApellidoMaterno;
                            usuariobj.FechaNacimiento = row.FechaNacimiento.ToString("dd-MM-yyyy");
                            usuariobj.Genero = row.Genero;
                            usuariobj.UserName = row.UserName;
                            usuariobj.Email = row.Email;
                            usuariobj.Password = row.Password;
                            usuariobj.Telefono = row.Telefono;
                            usuariobj.Celular = row.Celular;
                            usuariobj.CURP = row.Curp;

                            usuariobj.Rol = new ML.Rol();
                            usuariobj.Rol.IdRol = row.IdRol.Value;
                            usuariobj.Rol.Nombre = row.NombreRol;

                            usuariobj.Imagen = row.Imagen;

                            //direccion
                            usuariobj.Direccion = new ML.Direccion();
                            usuariobj.Direccion.IdDireccion = row.IdDireccion;
                            usuariobj.Direccion.Calle = row.Calle;
                            usuariobj.Direccion.NumeroExterior = row.NumeroExterior;
                            usuariobj.Direccion.NumeroInterior = row.NumeroIxterior;

                            //colonia
                            usuariobj.Direccion.Colonia = new ML.Colonia();
                            usuariobj.Direccion.Colonia.IdColonia = row.IdColonia;
                            usuariobj.Direccion.Colonia.Nombre = row.NombreColonia;
                            usuariobj.Direccion.Colonia.CodigoPostal = row.CodigoPostal;

                            //municipio
                            usuariobj.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuariobj.Direccion.Colonia.Municipio.IdMunicipio = row.IdMunicipio;
                            usuariobj.Direccion.Colonia.Municipio.Nombre = row.NombreMunicipio;

                            //estado
                            usuariobj.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuariobj.Direccion.Colonia.Municipio.Estado.IdEstado = row.IdEstado;
                            usuariobj.Direccion.Colonia.Municipio.Estado.Nombre = row.NumbreEstado;
                            
                            //pais
                            usuariobj.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            usuariobj.Direccion.Colonia.Municipio.Estado.Pais.IdPais = row.IdPais;
                            usuariobj.Direccion.Colonia.Municipio.Estado.Pais.Nombre = row.PaisNombre;
       
                            result.Objects.Add(usuariobj);

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

        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.HsilvaProgramacionNcapasContext context = new DL.HsilvaProgramacionNcapasContext())
                {
                    //var usuarios = context.AlumnoUpdate(alumno.IdAlumno, alumno.Nombre, alumno.ApellidoPaterno, alumno.ApellidoMaterno, alumno.FechaNacimiento, alumno.Sexo, alumno.Semestre.IdSemestre, alumno.Horario.Nombre, alumno.Horario.Grupo.IdGrupo);
                    var usuarios = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.FechaNacimiento}', '{usuario.Genero}', '{usuario.UserName}', '{usuario.Email}', '{usuario.Password}' ,'{usuario.Telefono}' ,'{usuario.Celular}' ,'{usuario.CURP}' ,{usuario.Rol.IdRol}, '{usuario.Imagen}' ,'{usuario.Direccion.Calle}' ,'{usuario.Direccion.NumeroExterior}' ,'{usuario.Direccion.NumeroInterior}' ,{usuario.Direccion.Colonia.IdColonia}");

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

        public static ML.Result GetById(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.HsilvaProgramacionNcapasContext context = new DL.HsilvaProgramacionNcapasContext())
                {

                  
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetById {idUsuario}").AsEnumerable().SingleOrDefault();

                    if (query != null)
                    {

                        ML.Usuario usuario = new ML.Usuario();

                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.FechaNacimiento = query.FechaNacimiento.ToString("dd-MM-yyyy");
                        usuario.Genero = query.Genero;
                        usuario.UserName = query.UserName;
                        usuario.Email = query.Email;
                        usuario.Password = query.Password;
                        usuario.Telefono = query.Telefono;
                        usuario.Celular = query.Celular;
                        usuario.CURP = query.Curp;
                      //  usuario.Imagen = query.Imagen;

                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = query.IdRol.Value;

                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = query.IdDireccion;
                        usuario.Direccion.Calle = query.Calle;
                        usuario.Direccion.NumeroExterior = query.NumeroExterior;
                        usuario.Direccion.NumeroInterior = query.NumeroIxterior;

                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = query.IdColonia;
                        usuario.Direccion.Colonia.Nombre = query.NombreColonia;

                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = query.IdMunicipio;
                        usuario.Direccion.Colonia.Municipio.Nombre = query.NombreMunicipio;

                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = query.IdEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = query.NumbreEstado;

                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = query.IdPais;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = query.PaisNombre;

                        result.Object = usuario; //boxing y unboxing
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

        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.HsilvaProgramacionNcapasContext context = new DL.HsilvaProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.IdUsuario}, '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.FechaNacimiento}', '{usuario.Genero}', '{usuario.UserName}', '{usuario.Email}', '{usuario.Password}' ,'{usuario.Telefono}' ,'{usuario.Celular}' ,'{usuario.CURP}' ,{usuario.Rol.IdRol}, '{usuario.Imagen}' ,'{usuario.Direccion.Calle}' ,'{usuario.Direccion.NumeroExterior}' ,'{usuario.Direccion.NumeroInterior}' ,{usuario.Direccion.Colonia.IdColonia}");

                    if (query > 0)
                    {
                        result.Message = "Se actualizo al usuario correctamente.";
                    }

                }
                result.Correct = true;

            }//codigo que puede causar una excepcion 
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "No se pudo actualizar al usuario." + result.Ex;

                throw;
            }//manejo de excepciones 
            return result;

        }

        public static ML.Result Delete(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.HsilvaProgramacionNcapasContext context = new DL.HsilvaProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"UsuarioDelete {usuario.IdUsuario}");

                    if (query > 0)
                    {
                        result.Message = "Se elimino al Usuario Correctamente.";
                    }

                }
                result.Correct = true;
            }//codigo que puede causar una excepcion 
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "No se pudo eliminar al usuario." + result.Ex;

                throw;
            }//manejo de excepciones 
            return result;
        }
    
       
        //carga de excel
        public static ML.Result ConvertirExceltoDataTable(string connstring) //convertir un excel en una tabla
        {
            ML.Result result = new ML.Result(); //instancia de result

            try
            {
                using (OleDbConnection context = new OleDbConnection(connstring)) //cadena de conexion
                {
                    string query = "SELECT * FROM [Sheet1$]"; //comando del query
                    using (OleDbCommand cmd = new OleDbCommand())
                    { 
                        cmd.CommandText = query;
                        cmd.Connection = context;

                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tableusuario = new DataTable(); //creacion de la tabla virutal

                        da.Fill(tableusuario); //se llena la tabla virutal

                        if (tableusuario.Rows.Count > 0) //validamos que la tabla tenga informacion
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tableusuario.Rows) //pasamos la inf de la tabla usuario a objeto row
                            {
                                ML.Usuario usuario = new ML.Usuario();

                               // usuario.IdUsuario = int.Parse(row[0].ToString());
                                usuario.Nombre = row[0].ToString();
                                usuario.ApellidoPaterno = row[1].ToString();
                                usuario.ApellidoMaterno = row[2].ToString();
                                usuario.FechaNacimiento = row[3].ToString();
                                usuario.Genero = row[4].ToString();
                                usuario.UserName = row[5].ToString();
                                usuario.Email = row[6].ToString();
                                usuario.Password = row[7].ToString();
                                usuario.Telefono = row[8].ToString();
                                usuario.Celular = row[9].ToString();
                                usuario.CURP = row[10].ToString();

                                usuario.Rol = new ML.Rol();
                                usuario.Rol.IdRol = int.Parse(row[11].ToString());

                               // usuario.Imagen = null;

                                usuario.Direccion = new ML.Direccion();
                                usuario.Direccion.Calle = row[12].ToString();
                                usuario.Direccion.NumeroExterior = row[13].ToString();
                                usuario.Direccion.NumeroInterior = row[14].ToString();

                                usuario.Direccion.Colonia = new ML.Colonia();
                                usuario.Direccion.Colonia.IdColonia = int.Parse(row[15].ToString());
                            

                            result.Objects.Add(usuario);
                        }

                        result.Correct = true;

                    }

                    if (tableusuario.Rows.Count > 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No existen registros en el excel";
                    }
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
       
        public static ML.Result ValidarExcel(List<object> Object)
        {
        ML.Result result = new ML.Result();

        try
    {
        result.Objects = new List<object>();
        //DataTable  //Rows //Columns
        int i = 1;
        foreach (ML.Usuario usuario in Object)
        {
            ML.ErrorExcel error = new ML.ErrorExcel();
            error.IdRegistro = i++;


            usuario.Nombre = (usuario.Nombre == "") ? error.Mensaje += "Ingresar el nombre  " : usuario.Nombre; //operador ternario

            if (usuario.ApellidoPaterno == "")
            {
                error.Mensaje += "Ingresar el Apellido Paterno ";
            }
            if (usuario.ApellidoMaterno == "")
            {
                error.Mensaje += "Ingresar el Apellido Materno ";
            }
            if (usuario.Genero == "")
                    {
                        error.Mensaje += "Ingresar el genero ";
                    }
            if (usuario.UserName == "")
            {
                error.Mensaje += "Ingresar el Nombre de Usuario";
            }
                    if (usuario.Email == "")
                    {
                        error.Mensaje += "Ingresar el email";
                    }
                    if (usuario.Password == "")
                    {
                        error.Mensaje += "Ingresar la contraseña";
                    }
                    if (usuario.Telefono == "")
                    {
                        error.Mensaje += "Ingresar el telefono";
                    }
                    if (usuario.Celular == "")
                    {
                        error.Mensaje += "Ingresar el celular";
                    }
                    if (usuario.CURP == "")
                    {
                        error.Mensaje += "Ingresar el CURP";
                    }
                    if (usuario.Rol.IdRol.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el semestre ";
                    }
                    if (usuario.Direccion.Calle == "")
                    {
                        error.Mensaje += "Ingresar la calle";
                    }
                    if (usuario.Direccion.NumeroExterior == "")
                    {
                        error.Mensaje += "Ingresar el numero exterior";
                    }
                    if (usuario.Direccion.NumeroInterior == "")
                    {
                        error.Mensaje += "Ingresar el numero interior";
                    }
                    if (usuario.Direccion.Colonia.IdColonia.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el idColonia";
                    }

                    if (error.Mensaje != null)
            {
                result.Objects.Add(error);
            }


        }
        result.Correct = true;
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