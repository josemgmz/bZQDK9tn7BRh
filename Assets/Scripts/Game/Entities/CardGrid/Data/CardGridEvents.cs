﻿using System.Collections.Generic;
using Game.Entities.Card.Data;

namespace Game.Entities.CardGrid.Data
{
    public class OnCardGridSetupEvent
    {
        public List<OnCardSetupEvent> Cards { get; set; }
        public int Columns { get; set; }
        public bool Shuffle { get; set; }
    }
}