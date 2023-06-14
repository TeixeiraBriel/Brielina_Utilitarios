using Dominio.Excecoes;
using Dominio.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.InjecaoDependencias
{
    public static class Dependencias
    {
        #region Resolvedor
        private static IResolvedor _resolvedor;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design","CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification ="É necessário por ser o resolvedor")]
        public static IResolvedor Resolvedor
        {
            get 
            {
                if (_resolvedor == null)
                    throw new ResolvedorNaoConfiguradoException();

                return _resolvedor; 
            }

            set 
            {
                _resolvedor = value; 
            }
        }
        #endregion

        public static ISteamService SteamService
        {
            get { return Resolvedor.Resolve<ISteamService>(); }
        }
    }
}
