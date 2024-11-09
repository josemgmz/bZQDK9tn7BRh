using System.Collections.Generic;
using Game.Entities.Card;

namespace Game.Entities.CardGrid
{
    public class CardGridSetupRequest
    {
        public List<CardSetupRequest> Cards { get; set; }
        public int Columns { get; set; }
        public bool Shuffle { get; set; }
    }
}