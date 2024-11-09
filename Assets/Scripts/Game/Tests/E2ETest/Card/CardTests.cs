using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Game.Tests.E2ETest.Card
{
    [TestFixture]
    public class CardTests : TestFramework.TestFramework
    {
        #region Variables

        private const string CARD_OBJECT = "Card";

        #endregion

        #region Getters
        
        protected override string GetTestDirectoryName()
        {
            return CARD_OBJECT;
        }

        #endregion

        #region Test

        [UnityTest]
        public IEnumerator FlipCardTest()
        {
            var card = GameObject.Find(CARD_OBJECT);
            var transform = card.transform;
            var startRotation = transform.rotation;
            var endRotation = transform.rotation * Quaternion.Euler(0, -180, 0);
            yield return new WaitForSeconds(2.0f);
            yield return PerformDrag(transform.position, transform.position + Vector3.down, 0.25f, RenderMode.ScreenSpaceCamera);
            yield return new WaitForSeconds(1.0f);
            //Compare the rotation of the card to the original rotation it should be flipped 180 degrees
            Assert.AreEqual(endRotation.eulerAngles.y, transform.rotation.eulerAngles.y);
            Debug.Log("Card Flipped");
            
            
            yield return PerformDrag(transform.position, transform.position + Vector3.down, 0.25f, RenderMode.ScreenSpaceCamera);
            yield return new WaitForSeconds(1.0f);
            Assert.AreEqual(startRotation.eulerAngles.y, transform.rotation.eulerAngles.y);
            Debug.Log("Card Flipped Back");
            yield return null;
            Debug.Log("Test Complete");
        }

        #endregion
    }
}
