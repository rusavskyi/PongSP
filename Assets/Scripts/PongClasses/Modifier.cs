using System.Collections.Generic;

namespace PongClasses
{
    public abstract class Modifier
    {
        protected List<Effect> _effects;
        protected int _id;

        public Modifier()
        {
            _effects = new List<Effect>();
        }

        public abstract void Initiation(PongObject target);

        public int GetID()
        {
            return _id;
        }

    }
}