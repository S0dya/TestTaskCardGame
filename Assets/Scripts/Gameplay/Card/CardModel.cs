using UnityEngine;

namespace Game.Card
{
    public class CardModel
    {
        public string CardName => _cardName;
        public int EnergyNeeded => _energyNeeded;
        public ActionEffectData ActionEffectData => _actionEffectData;

        public int CardIndex => _cardIndex;

        private string _cardName;
        private int _energyNeeded;
        private ActionEffectData _actionEffectData;

        private int _cardIndex;

        public CardModel(string cardName, int energyNeeded, ActionEffectEnum actionEffect, Sprite sprite, int value)
        {
            _cardName = cardName;
            _energyNeeded = energyNeeded;
            _actionEffectData = new ActionEffectData(actionEffect, sprite, value);
        }

        public void SetCardIndex(int cardIndex)
        {
            _cardIndex = cardIndex;
        }
    }
}
