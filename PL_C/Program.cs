// See https://aka.ms/new-console-template for more information

ReadFile();

  static void ReadFile()
   {
    string file = @"C:\Users\digis\Documents\Habid Alejandro Silva Cruz\LayoutUsuarios.txt"; //ruta del .TXT

    if (File.Exists(file))
    {
        StreamReader Textfile = new StreamReader(file);
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
           
            if(!result.Correct)
            {
                resultErrores.Objects.Add("Error al insertar el genero " + usuario.Genero +
                                          "Error al insertar la fecha  " + usuario.FechaNacimiento +
                                          "Error al insertar el UserName" + usuario.UserName +
                                          "Error al insertar el Email"  + usuario.Email +
                                          "Error al insertar Password" + usuario.Password +
                                          "Error al insertar el IdColina" + usuario.Direccion.Colonia.IdColonia);
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
}
