using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Card
{
    public class CardModel
    {
        public string CardName => _cardName;
        public int EnergyNeeded => _energyNeeded;
        public ActionEffectEnum ActionEffect => _actionEffect;
        public Sprite Sprite => _sprite;
        public int Value => _value;

        public int CardIndex => _cardIndex;

        private string _cardName;
        private int _energyNeeded;
        private ActionEffectEnum _actionEffect;
        private Sprite _sprite;
        private int _value;

        private int _cardIndex;

        public CardModel(string cardName, int energyNeeded, ActionEffectEnum actionEffect, Sprite sprite, int value)
        {
            _cardName = cardName;
            _energyNeeded = energyNeeded;
            _actionEffect = actionEffect;
            _sprite = sprite;
            _value = value;
        }

        public void SetCardIndex(int cardIndex)
        {
            _cardIndex = cardIndex;
        }
    }
}
