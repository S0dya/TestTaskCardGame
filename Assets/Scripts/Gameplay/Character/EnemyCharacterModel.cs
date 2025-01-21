using System;
using System.Collections.Generic;

namespace Game.Character
{
    public class EnemyCharacterModel : CharacterModel
    {
        public ActionEffectData CurrentActionEffect => _currentActionEffect;
        public int EnemyIndex => _enemyIndex;

        private Queue<ActionEffectData> _strategy;

        private int _enemyIndex;

        private ActionEffectData _currentActionEffect;

        public EnemyCharacterModel(int healthPoints, int shieldPoints, 
            ActionEffectData[] strategy, int enemyIndex, Action<EnemyCharacterModel> deathAction)
            : base(healthPoints, shieldPoints, null)
        {
            _strategy = new Queue<ActionEffectData>(strategy);

            _enemyIndex = enemyIndex;

            _deathAction = () => deathAction.Invoke(this);
        }

        public void SetNextStrategyAction()
        {
            if (_strategy.Count == 0)
            {
                DebugManager.Log(DebugCategory.Errors, "No strategy", DebugStatus.Error);
                return;
            }

            _currentActionEffect = _strategy.Dequeue();
            _strategy.Enqueue(_currentActionEffect);
        }
    }
}