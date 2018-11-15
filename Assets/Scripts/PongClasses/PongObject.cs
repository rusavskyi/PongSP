using System.Collections.Generic;
using UnityEngine;

namespace PongClasses
{
    public class PongObject
    {
        public List<Effect> _effects;
        protected List<Modifier> _modifiers;

        public PongObject()
        {
            _effects = new List<Effect>();
            _modifiers = new List<Modifier>();
        }

        public virtual void AddEffect(Effect income)
        {
            bool triger = false;
            foreach (Effect e in _effects)
            {
                if (income.GetID() == e.GetID())
                {
                    //e.Renew();
                    triger = true;
                    break;
                }
            }
            if (!triger)
                _effects.Add(income);
        }

        public virtual void AddModifier(Modifier income)
        {
            bool triger = false;
            foreach (Modifier m in _modifiers)
            {
                if (income.GetID() == m.GetID())
                    triger = true;
            }
            if (!triger)
                _modifiers.Add(income);
        }

        public virtual void AddModifiers(params Modifier[] income)
        {
            for (int i = 0; i < income.Length; i++)
            {
                AddModifier(income[i]);
            }
        }

        public void CheckEffects() //Cheking and removing effects
        {
            int size = _effects.Count;
            for (int i = 0; i < size; i++)
            {
                while (i < _effects.Count && !_effects[i].IsActive())
                {
                    _effects.RemoveAt(i);
                    size = _effects.Count;
                }
            }
        }

        public void InitiateAllMods()
        {
            foreach (Modifier m in _modifiers)
            {
                m.Initiation(this);
            }
        }
    }

}

