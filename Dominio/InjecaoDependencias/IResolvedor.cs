using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.InjecaoDependencias
{
    public interface IResolvedor
    {
        T Resolve<T>();

        void Singleton<T>(T obj);
    }
}
