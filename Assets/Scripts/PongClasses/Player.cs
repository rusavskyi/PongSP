using UnityEngine;
using System.Collections;

namespace PongClasses
{
    public class Player : PongObject
    {
        public Effect _storedEffect;
        protected int _hp;
        protected int maxHp = 20;
        public string Name { get; set; }

        // bonus fields for effects and modifiers 

        #region Get methods
        public int GetHeath()
        {
            return _hp;
        }
        #endregion


        public Player() : base()
        {
            Name = "PlayerName";
            _storedEffect = null;
            _hp = 20;
        }

        public bool StoreEffect(Effect income)
        {
            if (_storedEffect == null)
            {
                _storedEffect = income;
                return true;
            }
            return false;
        }

        public void ClearStoredEffect()
        {
            _storedEffect = null;
        }

        #region HP methods
        public int LoseHP()
        {
            if (_hp > 0) _hp--;
            return _hp;
        }

        public void AddHP()
        {
            if (_hp > 0 && _hp < maxHp)
                _hp++;
        }

        public void AddHP(int x)
        {
            if (_hp > 0)
                _hp += x;
            if (_hp > maxHp)
                _hp = maxHp;
        }
        #endregion
        /*
        UseStoredEffect();
        
        */
    }


}
