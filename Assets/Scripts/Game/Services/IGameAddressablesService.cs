using Game.Entities.Card.Data;
using Game.Services.Lifetime;
using UnityEngine;

namespace Game.Services
{
    /// <summary>
    /// Provides functionalities to retrieve card sprites based on card shape and type.
    /// </summary>
    public interface IGameAddressablesService : IStoppableService
    {
        /// <summary>
        /// Gets the sprite for the specified card shape and type.
        /// </summary>
        /// <param name="cardShape">The shape of the card.</param>
        /// <param name="cardType">The type of the card.</param>
        /// <returns>The sprite for the specified card.</returns>
        Sprite GetCardSprite(CardShape cardShape, CardType cardType = CardType.None);
    }
}