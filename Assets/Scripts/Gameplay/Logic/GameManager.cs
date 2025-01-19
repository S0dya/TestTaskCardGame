using Game.Card;
using Game.Combat;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [Header("Test")]
        [SerializeField] private LevelInfo[] levelInfos;

        [Space(10)]
        [SerializeField] private CardsBundleInfo startingCardsBundle;

        [Space(10)]
        [SerializeField] private CombatManager combatManager;
        [SerializeField] private CardsManager cardsManager;

        private int _levelIndex;

        private void Start()
        {
            combatManager.StartLevel(levelInfos[_levelIndex].EnemyCharacters);

            foreach (var card in startingCardsBundle.CardInfos)
            {
                cardsManager.AddCard(card);
            }

            _levelIndex++;
        }
    }
}
