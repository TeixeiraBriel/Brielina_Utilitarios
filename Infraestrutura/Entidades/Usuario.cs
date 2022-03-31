using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Entidades
{
    public class Usuario
    {
        public string user { get; set; }
        public string pass { get; set; }
        public string nickname { get; set; }
        public string FuncionalidadesPermitidas { get; set; }
    }
}
