using UnityEngine;
using Game.Character;
using ObserverPattern;
using System.Collections.Generic;
using System;
using System.Collections;

namespace Game.Card
{
    public class CardsManager : SubjectMonoBehaviour
    {
        [SerializeField] private CharactersManager charactersManager;

        [Space(10)]
        [SerializeField] private UIDeckView deckView;

        private DeckModel _deckModel = new DeckModel();

        private int _currentDraggedCardIndex;

        private Coroutine _drawCardsCoroutine;

        private void Awake()
        {
            AddEventActions(new Dictionary<EventEnum, Action>
            {
                { EventEnum.CombatPlayerTurnStarted, OnPlayerTurnStarted},
            });
        }

        private void Start()
        {
            charactersManager.InitActions(OnPointerUpActionPlayer, OnPointerUpActionEnemy);
        }

        public void OnCardPointerDown(int i)
        {
            DebugManager.Log(DebugCategory.Points, "pointer down on card");

            _currentDraggedCardIndex = i;

            CardModel card = _deckModel.GetCard(_currentDraggedCardIndex);

            charactersManager.OnCardDragged(card.ActionEffect); //change later
        }
        public void OnCardPointerUp()
        {
            DebugManager.Log(DebugCategory.Points, "pointer up on card");

            _currentDraggedCardIndex = -1;
        }

        public void AddCard(CardInfo cardInfo)
        {
            _deckModel.AddCardDraw(
                cardInfo.CardName,
                cardInfo.EnergyNeeded,
                cardInfo.ActionEffect,
                cardInfo.Sprite,
                cardInfo.Value);


            deckView.SetDrawPile(_deckModel.DrawPileCardsCount);
        }

        public void DrawCard()
        {
            if (deckView.HasFreeCardView())
            {
                CardModel card = _deckModel.DrawCard();

                int cardIndex = deckView.AddCard(
                    card.CardName,
                    card.Sprite,
                    $"<color=yellow>{card.ActionEffect}</color> {card.Value}",
                    card.EnergyNeeded.ToString());

                _deckModel.AddCardHand(card, cardIndex);

                deckView.SetDrawPile(_deckModel.DrawPileCardsCount);
            }
        }

        private void OnPointerUpActionPlayer()
        {
            CardModel card = _deckModel.GetCard(_currentDraggedCardIndex);

            if (charactersManager.GetPlayerEnergy() < card.EnergyNeeded) return;

            DebugManager.Log(DebugCategory.Points, "used card on player");

            _deckModel.DiscardCard(card);

            //charactersManager.UseCard(card.);
        }
        private void OnPointerUpActionEnemy(int i)
        {
            DebugManager.Log(DebugCategory.Points, "used card on enemy");
        }

        private void OnPlayerTurnStarted()
        {
            if (_drawCardsCoroutine != null) StopCoroutine(_drawCardsCoroutine);
            _drawCardsCoroutine = StartCoroutine(DrawCardsCoroutine());
        }

        private IEnumerator DrawCardsCoroutine()
        {
            for (int i = 0; i < 5; i++)
            {
                DrawCard();
             
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}