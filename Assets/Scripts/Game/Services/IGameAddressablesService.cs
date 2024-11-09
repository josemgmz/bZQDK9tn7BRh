using Game.Entities.Card;
using Game.Services.Lifetime;
using UnityEngine;

namespace Game.Services
{
    public interface IGameAddressablesService : IStoppableService
    {
        Sprite GetCardSprite(CardShape cardShape, CardType cardType = CardType.None);
    }
}