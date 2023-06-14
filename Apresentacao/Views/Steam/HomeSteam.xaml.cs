using Dominio.Entidades;
using Dominio.InjecaoDependencias;
using Dominio.Servicos;
using Microsoft.Maui.Controls;

namespace Apresentacao.Views.Steam;

public partial class HomeSteam : ContentPage
{
    int totalJogado = 0;
    OwnedGames listaJogos;

    public HomeSteam()
    {
        InitializeComponent();
        atribuiListaJogos();
    }

    async void atribuiListaJogos()
    {
        listaJogos = await Dependencias.SteamService.minerarDados("76561198124348532");
        foreach (var item in listaJogos.response.games.OrderByDescending(i => i.playtime_forever))
        {
            TabelaSection.Add(
            new ImageCell()
            {
                Text = item.name,
                Detail = converterMinutosHoras(item.playtime_forever),
                ImageSource = item.img_icon_url
            });

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