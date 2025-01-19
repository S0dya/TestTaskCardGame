using System.Linq;
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

        public void AddCard(CardModel cardModel)
        {
            GetFreeCardView().SetCard(cardModel);
        }

        public void RemoveCard(int index)
        {
            cardViews[index].ResetCard();
        }

        public void SetDrawPile(int value)
        {
            Helper.SetText(drawPileAmount, value.ToString());
        }
        public void SetDiscardPile(int value)
        {
            Helper.SetText(discardPileAmount, value.ToString());
        }

        public bool HasFreeCardView() => GetFreeCardView() != null;

        private UICardView GetFreeCardView() => cardViews.FirstOrDefault(x => !x.IsActive);
    }
}