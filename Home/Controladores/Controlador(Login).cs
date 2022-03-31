using Infraestrutura.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrielinaUtilitarios.Controladores
{
    public partial class ControladorLogin : Controlador
    {
        public Usuario usuarioValido(Usuario usuario)
        {
            var file = @"Dados\Usuarios.json";
            List<Usuario> Usuarios = JsonConvert.DeserializeObject<List<Usuario>>(File.ReadAllText(file, Encoding.UTF8));

            return Usuarios.Find(i => i.user == usuario.user && i.pass == usuario.pass);
        }
    }
}
