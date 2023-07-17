using Dominio.Entidades;
using Dominio.Servicos;

namespace Apresentacao.Views.Steam;

public partial class HomeSteam : ContentPage
{
    int totalJogado = 0;
    OwnedGames listaJogos;
    ISteamService _steamService;

    public HomeSteam(ISteamService steamService)
    {
        InitializeComponent();
        _steamService = steamService;
        atribuiListaJogos();
    }

    async void atribuiListaJogos()
    {
        int count = 1;
        listaJogos = await _steamService.minerarDados("76561198124348532");
        
        TabelaSection.Clear();
        foreach (var item in listaJogos.response.games.OrderByDescending(i => i.playtime_forever))
        {
            string linkImg = $"https://media.steampowered.com/steamcommunity/public/images/apps/{item.appid.ToString()}/{item.img_icon_url}.jpg";

            TabelaSection.Add(
            new ImageCell()
            {
                Text = count + " - " + item.name,
                Detail = converterMinutosHoras(item.playtime_forever),
                ImageSource = linkImg
            });

            count++;
            totalJogado = totalJogado + item.playtime_forever;
        }
    }

    public string converterMinutosHoras(int minutos)
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
        return string.Format("{0} Horas", (int)spWorkMin.TotalHours);
    }
}