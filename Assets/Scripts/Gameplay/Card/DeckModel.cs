using System.Collections.Generic;
using UnityEngine;

namespace Game.Card
{
    public class DeckModel
    {
        public int DrawPileCardsCount => _drawPileCards.Count;
        public int HandCardsCount => _handCards.Count;
        public int DiscardPileCardsCount => _discardPileCards.Count;

        private List<CardModel> _drawPileCards = new List<CardModel>();
        private List<CardModel> _handCards = new List<CardModel>();
        private List<CardModel> _discardPileCards = new List<CardModel>();


        public void AddCardDraw(string cardName, int energyNeeded, ActionEffectEnum actionEffect, Sprite sprite, int value)
        {
            _drawPileCards.Add(new CardModel(cardName, energyNeeded, actionEffect, sprite, value));
        }
        public void AddCardHand(CardModel card, int index)
        {
            card.SetCardIndex(index);

            _handCards.Add(card);
        }

        public void DiscardCard(CardModel card)
        {
            if (_handCards.Remove(card))
            {
                _discardPileCards.Add(card);
            }
        }

        public void ShuffleDrawPile()
        {
            Helper.Shuffle(_drawPileCards);
        }

        public CardModel DrawCard()
        {
            if (_drawPileCards.Count == 0)
            {
                ReshuffleDiscardPile();

                if (_drawPileCards.Count == 0)
                {
                    DebugManager.Log(DebugCategory.Gameplay, "no cards in piles", DebugStatus.Error);
                }
            }

            CardModel card = _drawPileCards[0];
            _drawPileCards.RemoveAt(0);

            return card;
        }

        public CardModel GetCard(int cardIndex)
        {
            return _handCards.Find(x => x.CardIndex == cardIndex);
        }

        private void ReshuffleDiscardPile()
        {
            _drawPileCards.AddRange(_discardPileCards);
            
            _discardPileCards.Clear();

            ShuffleDrawPile();
        }
    }
}