using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Framework;
using Game.Entities.Card;
using Game.Services;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using VContainer;
using VContainer.Unity;

namespace Game.Tests.E2ETest.Levels
{
    [TestFixture]
    public class LevelTests : TestFramework.TestFramework
    {
        #region Variables

        private const string CARD_OBJECT = "Card";
        private const string PLAY_BUTTON_OBJECT = "Play";
        private const string CARD_GRID_OBJECT = "CardGrid";
        private static GameDependency _dependency = null;

        #endregion

        #region Getters
        
        protected override string GetTestDirectoryName()
        {
            return "Levels";
        }

        #endregion

        #region Test

        [UnityTest]
        public IEnumerator Level0()
        {
            yield return EvaluateLevel(0);
        }
        
        [UnityTest]
        public IEnumerator Level1()
        {
            yield return EvaluateLevel(1);
        }
        
        [UnityTest]
        public IEnumerator Level2()
        {
            yield return EvaluateLevel(2);
        }
        
        [UnityTest]
        public IEnumerator Level3()
        {
            yield return EvaluateLevel(3);
        }
        
        [UnityTest]
        public IEnumerator Level4()
        {
            yield return EvaluateLevel(4);
        }
        
        [UnityTest]
        public IEnumerator Level5()
        {
            yield return EvaluateLevel(5);
        }
        
        [UnityTest]
        public IEnumerator Level6()
        {
            yield return EvaluateLevel(6);
        }
        
        [UnityTest]
        public IEnumerator Level7()
        {
            yield return EvaluateLevel(7);
        }
        
        [UnityTest]
        public IEnumerator Level8()
        {
            yield return EvaluateLevel(8);
        }
        
        [UnityTest]
        public IEnumerator Level9()
        {
            yield return EvaluateLevel(9);
        }

        #endregion

        #region Private Methods
        
        public IEnumerator EvaluateLevel(int level)
        {
            yield return new WaitForSeconds(0.5f);
            GetInstance<IRoundService>().SetRound(level);
            GetInstance<IRoundService>().EndRound(false);
            
            yield return new WaitForSeconds(4.50f);
            var startRound = GetInstance<IRoundService>().GetRound();
            
            var play = GameObject.Find(PLAY_BUTTON_OBJECT);
            yield return PerformDrag(play.transform.position, play.transform.position + Vector3.down, 0.25f, RenderMode.ScreenSpaceCamera);

            yield return new WaitForSeconds(7.25f);
            var cardGrid = GameObject.Find(CARD_GRID_OBJECT);
            var cards = cardGrid.GetComponentsInChildren<CardView>();
            
            //Create a list of card pair by name pair
            var cardGroups = GetCardPairsByName(cards);

            foreach (var pair in cardGroups)
            {
                foreach (var card in pair)
                {
                    yield return PerformDrag(card.transform.position, card.transform.position + Vector3.down, 0.25f, RenderMode.ScreenSpaceCamera);
                    yield return new WaitForSeconds(0.75f);
                }
            }
            yield return new WaitForSeconds(2.0f);

            var endRound = GetInstance<IRoundService>().GetRound();
            Assert.AreEqual(startRound + 1, endRound);
        }

        List<List<CardView>> GetCardPairsByName(CardView[] cards)
        {
            return cards.GroupBy(card => card.name)
                .Where(group => group.Count() > 1)
                .Select(group => group.ToList())
                .ToList();;
        }
        
        TType GetInstance<TType>()
        {
            _dependency = _dependency == null ? LifetimeScope.Find<GameDependency>().GetComponent<GameDependency>() : _dependency;
            return _dependency.Container.Resolve<TType>();
        }

        #endregion
    }
}
