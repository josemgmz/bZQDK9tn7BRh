using System;
using System.Collections.Generic;
using Game.Entities.Card;
using Game.Entities.CardGrid;

namespace Game.Services.Impl
{
    public class LevelServiceImpl : ILevelService
    {
        #region Variables

        private List<OnCardGridSetupEvent> _levels = new ();
        private Random _random = new Random((int)DateTime.Now.Ticks);
        
        #endregion
        
        #region Lifecycle

        public void Start()
        {
            var levelConfigs = new (int columns, int rows)[]
            {
                (2, 2), (2, 3), (3, 4), (4, 4), (4, 5), (5, 6), (6, 6), (6, 7), (7, 8), (8, 8)
            };

            foreach (var (columns, rows) in levelConfigs)
            {
                _levels.Add(CreateLevel(columns, rows));
            }
        }

        public void Stop()
        {
        }

        #endregion

        #region Public Methods
        

        public OnCardGridSetupEvent GetLevel(int level)
        {
            return level >= _levels.Count ? CreateLevel(8, 8) : _levels[level];
        }
        

        #endregion

        #region Private Methods

        private OnCardGridSetupEvent CreateLevel(int columns, int rows)
        {
            var pairs = (columns * rows) / 2;
            var cards = new List<OnCardSetupEvent>();
            var usedPairs = new List<Tuple<CardType, CardShape>>();

            for (var j = 0; j < pairs; j++)
            {
                CardType cardType;
                CardShape cardShape;
                Tuple<CardType, CardShape> tuple;
                do
                {
                    cardType =  (CardType) _random.Next(2, 15);
                    cardShape = (CardShape) _random.Next(0, 4);
                    tuple = new Tuple<CardType, CardShape>(cardType, cardShape);
                } while (usedPairs.Contains(tuple));
                    
                usedPairs.Add(new Tuple<CardType, CardShape>(cardType, cardShape));
                cards.Add(new OnCardSetupEvent { CardShape = cardShape, CardType = cardType });
            }

            var level = new OnCardGridSetupEvent
            {
                Columns = columns,
                Shuffle = true,
                Cards = cards
            };

            return level;
        }

        #endregion
    }
}