using Dominio.InjecaoDependencias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Excecoes
{
    public class ResolvedorNaoConfiguradoException : Exception
    {
        public ResolvedorNaoConfiguradoException() : base(
            string.Format("Nenhuma instancia de \"{0}\" foi configurada no domínio", typeof(IResolvedor).Name)) 
        {
            
        }
    }
}
