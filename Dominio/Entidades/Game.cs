using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Game
    {
        public int appid { get; set; }
        public string name { get; set; }
        public string tempo_jogado { get; set; }
        public string caminho_imagem { get; set; }

        private string _img_icon_url;
        public string img_icon_url
        {
            get { return _img_icon_url; }
            set { _img_icon_url = value; caminho_imagem = CriarUrl(); }
        }

        private int _playtime_forever;
        public int playtime_forever
        {
            get { return _playtime_forever; }
            set { _playtime_forever = value; tempo_jogado = converterMinutosHoras(value); }
        }



        public static string converterMinutosHoras(int minutos)
        {
            if (minutos < 60)
            {
                if (minutos < 1)
                {
                    return $"{minutos} Minutos";
                }
                return $"{minutos} Minutos";
            }

            TimeSpan spWorkMin = TimeSpan.FromMinutes(minutos);
            return string.Format("{0} Horas {1} Minutos", (int)spWorkMin.TotalHours, (int)spWorkMin.Minutes);
        }

        private string CriarUrl()
        {
            return $"https://media.steampowered.com/steamcommunity/public/images/apps/{appid}/{_img_icon_url}.jpg";
        }
    }
}
