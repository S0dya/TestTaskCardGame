using System;
using TMPro;
using UnityEngine;

namespace Game.Card
{
    public class UIDeckView : MonoBehaviour
    {
        [SerializeField] private UICardView[] cardViews;

        [Space(10)]
        [SerializeField] private TextMeshProUGUI drawPileAmount;
        [SerializeField] private TextMeshProUGUI discardPileAmount;

        [Space(20)]
        [SerializeField] private RectTransform handCardsParent;
        [SerializeField] private RectTransform pileCardsParent;

        private void Awake()
        {
            foreach (var cardView in cardViews)
            {
                cardView.ResetCard();
                cardView.MoveToParent(pileCardsParent);
            }
        }

        public int AddCard(string name, Sprite sprite, string description, string energyNeeded)
        {
            int cardViewIndex = GetCardViewIndex(false);

            cardViews[cardViewIndex].SetCard(name, sprite, description, energyNeeded);
            cardViews[cardViewIndex].MoveToParent(handCardsParent);

            return cardViewIndex;
        }

        public void DiscardFreeCard()
        {
            DiscardCard(GetCardViewIndex(true));
        }
        public void DiscardCard(int index)
        {
            cardViews[index].ResetCard();
            cardViews[index].MoveToParent(pileCardsParent);
            cardViews[index].MoveLocalPosition(Vector2.zero);
        }

        public void ToggleCardsRaycast(bool toggle)
        {
            foreach (var cardView in cardViews)
            {
                cardView.ToggleRaycast(toggle);
            }
        }

        public void SetDrawPile(int value)
        {
            Helper.SetText(drawPileAmount, value.ToString());
        }
        public void SetDiscardPile(int value)
        {
            Helper.SetText(discardPileAmount, value.ToString());
        }

        public bool HasFreeCardView() => GetCardViewIndex(false) > -1;
        public bool HasHandCardView() => GetCardViewIndex(true) > -1;

        private int GetCardViewIndex(bool isSet)
        {
            for (int i = 0; i < cardViews.Length; i++)
            {
                if (cardViews[i].IsActive == isSet)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}