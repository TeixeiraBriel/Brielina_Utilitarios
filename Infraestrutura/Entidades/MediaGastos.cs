using BrielinaFinanceiro.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estrutura.Entidades
{
    public class MediaGastos
    {
        public string Tipo { get; set; }
        public string Nome { get; set; }
        public double ValorTotal { get; set; }
        public double ValorDaMedia { get; set; }
        public int qtdRegistros { get; set; }

        public List<MediaGastos> criarMedias(List<Registro> registros)
        {
            List<MediaGastos> Medias = new List<MediaGastos>();

            foreach (var registro in registros)
            {
                string tipoDele = registro.Tipo == 0 ? "Entrada" : "Saida";
                MediaGastos novamedia = new MediaGastos();
                var valida = Medias.FindLastIndex(x => x.Nome.ToLower() == registro.Grupo.ToLower() && x.Tipo == tipoDele);
                if (valida == -1 || Medias.Count == 0)
                {
                    novamedia = new MediaGastos { Tipo = tipoDele, Nome = registro.Grupo, ValorTotal = registro.Valor, ValorDaMedia = registro.Valor, qtdRegistros = 1 };
                    Medias.Add(novamedia);
                }
                else
                {
                    Medias[valida].qtdRegistros++;
                    Medias[valida].ValorTotal += registro.Valor;
                    Medias[valida].ValorDaMedia = Medias[valida].ValorTotal / Medias[valida].qtdRegistros;
                }
            }

            return Medias;
        }
    }
}
