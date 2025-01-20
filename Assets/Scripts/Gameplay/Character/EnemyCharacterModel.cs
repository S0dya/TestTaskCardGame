using System;

namespace Game.Character
{
    public class EnemyCharacterModel : CharacterModel
    {
        public EnemyCharacterModel(int healthPoints, int shieldPoints, Action<EnemyCharacterModel> deathAction)
            : base(healthPoints, shieldPoints, null)
        {
            _deathAction = () => deathAction.Invoke(this);
        }
    }
}