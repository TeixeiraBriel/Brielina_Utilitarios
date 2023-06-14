using Dominio.InjecaoDependencias;
using InjecaoDependencias;

namespace Apresentacao.Controladores
{
    public class ControladorInicializa
    {
        public void Inicializa()
        {
            Dependencias.Resolvedor = new Resolvedor();
        }
    }
}
