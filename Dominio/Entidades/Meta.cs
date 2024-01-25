namespace Dominio.Entidades
{
    public class Meta
    {
        public string Nome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public List<DateTime> DiasExecutados { get; set; }
    }
}
