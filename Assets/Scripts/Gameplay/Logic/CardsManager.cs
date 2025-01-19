using UnityEngine;
using Game.Character;
using System.Collections.Generic;

namespace Game.Card
{
    public class CardsManager : MonoBehaviour
    {
        [SerializeField] private UIDeckView deckView;

        private DeckModel _deckModel = new DeckModel();

        public void AddCard(CardInfo cardInfo)
        {

        }

        public void DrawCard()
        {
            if (deckView.HasFreeCardView())
            {
                CardModel card = _deckModel.DrawCard();

                deckView.AddCard(card);
            }
        }
    }
}