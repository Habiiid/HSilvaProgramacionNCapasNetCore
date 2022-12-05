﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Departamento
    {
        public int IdDepartamento { get; set; }

        public string Nombre { get; set; }

        public ML.Area Area { get; set; } //es la propiedad de navegacion

        public List<Object> Departamentos { get; set; }
    }
}