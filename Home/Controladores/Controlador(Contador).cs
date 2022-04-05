namespace BrielinaUtilitarios.Controladores
{
    using static AutoIt.AutoItX;
    public partial class ControladorContador : Controlador
    {
        public string capturaJanelaAtiva()
        {
            return WinGetTitle("[active]");
        }
    }
}
