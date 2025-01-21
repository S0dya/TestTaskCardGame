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

        private DeckModel _deckModel;

        private int _currentDraggedCardIndex;

        private Coroutine _drawCardsCoroutine;
        private Coroutine _discardCardsCoroutine;

        private void Awake()
        {
            AddEventActions(new Dictionary<EventEnum, Action>
            {
                { EventEnum.CombatPlayerTurnStarted, OnPlayerTurnStarted},
                { EventEnum.CombatPlayerTurnEnded, OnPlayerTurnEnded},
            });
        }

        private void Start()
        {
            charactersManager.InitActions(OnPointerUpActionPlayer, OnPointerUpActionEnemy);

            _deckModel = new DeckModel(OnDiscardCardsDrawwed);
        }

        public void OnCardPointerDown(int i)
        {
            DebugManager.Log(DebugCategory.UI, "pointer down on card");

            _currentDraggedCardIndex = i;

            CardModel card = _deckModel.GetCard(_currentDraggedCardIndex);

            charactersManager.EnableActionForCard(card.ActionEffectData.ActionEffect);
        }
        public void OnCardPointerUp()
        {
            DebugManager.Log(DebugCategory.UI, "pointer up on card");

            charactersManager.DisableAction();

            //_currentDraggedCardIndex = -1;
        }

        public void AddCard(CardInfo cardInfo)
        {
            _deckModel.AddCardDraw(
                cardInfo.CardName,
                cardInfo.EnergyNeeded,
                cardInfo.ActionEffectData.ActionEffect,
                cardInfo.ActionEffectData.Sprite,
                cardInfo.ActionEffectData.Value);


            deckView.SetDrawPile(_deckModel.DrawPileCardsCount);
        }

        public void DrawCard()
        {
            if (deckView.HasFreeCardView())
            {
                CardModel card = _deckModel.DrawCard();

                var actionEffectData = card.ActionEffectData;

                int cardIndex = deckView.AddCard(
                    card.CardName,
                    actionEffectData.Sprite,
                    $"<color=yellow>{actionEffectData.ActionEffect}</color> {actionEffectData.Value}",
                    card.EnergyNeeded.ToString());

                _deckModel.AddCardHand(card, cardIndex);

                deckView.SetDrawPile(_deckModel.DrawPileCardsCount);
            }
        }

        private void OnPointerUpActionPlayer()
        {
            GetDraggedCard(out CardModel card);
            if (card == null) return;

            DebugManager.Log(DebugCategory.UI, "used card on player");

            charactersManager.UseCard(card.EnergyNeeded, card.ActionEffectData.ActionEffect, card.ActionEffectData.Value);
        }
        private void OnPointerUpActionEnemy(int i)
        {
            GetDraggedCard(out CardModel card);
            if (card == null) return;

            DebugManager.Log(DebugCategory.UI, "used card on enemy");

            charactersManager.UseCard(card.EnergyNeeded, card.ActionEffectData.ActionEffect, card.ActionEffectData.Value, i);
        }

        private void GetDraggedCard(out CardModel card)
        {
            card = _deckModel.GetCard(_currentDraggedCardIndex);

            if (charactersManager.GetPlayerEnergy() < card.EnergyNeeded) return;

            _deckModel.DiscardCard(card);
            deckView.SetDiscardPile(_deckModel.DiscardPileCardsCount);

            deckView.DiscardCard(_currentDraggedCardIndex);
        }

        private void OnDiscardCardsDrawwed()
        {
            DebugManager.Log(DebugCategory.Points, "Took cards from discard");

            deckView.SetDiscardPile(_deckModel.DiscardPileCardsCount);
            deckView.SetDrawPile(_deckModel.DrawPileCardsCount);
        }

        private void OnPlayerTurnStarted()
        {
            _deckModel.ShuffleDrawPile();

            if (_drawCardsCoroutine != null) StopCoroutine(_drawCardsCoroutine);
            _drawCardsCoroutine = StartCoroutine(DrawCardsCoroutine());
        }
        private void OnPlayerTurnEnded()
        {
            _deckModel.DiscardCards();
            deckView.ToggleCardsRaycast(false);

            if (_discardCardsCoroutine != null) StopCoroutine(_discardCardsCoroutine);
            _discardCardsCoroutine = StartCoroutine(DiscardCardsCoroutine());
        }

        private IEnumerator DrawCardsCoroutine()
        {
            for (int i = 0; i < 5; i++)
            {
                DrawCard();
             
                yield return new WaitForSeconds(0.1f);
            }

            deckView.ToggleCardsRaycast(true);
        }
        private IEnumerator DiscardCardsCoroutine()
        {
            while (deckView.HasHandCardView())
            {
                deckView.DiscardFreeCard();
                deckView.SetDiscardPile(_deckModel.DiscardPileCardsCount);

                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}