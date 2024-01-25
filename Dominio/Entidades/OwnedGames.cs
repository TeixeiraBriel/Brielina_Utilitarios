using System.Numerics;

namespace Dominio.Entidades
{
    public class OwnedGames
    {
        public Jogos response { get; set; }
    }

    public class Jogos
    {
        public int game_count { get; set; }
        public List<Game> games { get; set; }
    }
}
