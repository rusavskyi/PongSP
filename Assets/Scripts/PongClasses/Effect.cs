using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PongClasses
{
    public abstract class Effect
    {
        protected int _id;
        protected int _counter; //for effects whith limeted times of ativation
        protected int _type; // 0 - time limited effect, 1 - action limited effect, 2 - other
        public Effect() { }

        //public abstract void Timer(); //for effects whith time limit
        //public abstract void Renew();

        public virtual void Action(GameObject target)
        {
            
        }

        public int GetID()
        {
            return _id;
        }

        public int GetCounter()
        {
            return _counter;
        }

        public virtual bool IsActive()
        {
            bool res = true;
            switch (_type)
            {
                case 0:
                    res = false;
                    break;
                case 1:
                    if (_counter < 0) res = false;
                    break;
                default:
                    res = false;
                    break;
            }
            return res;
        }
    }
}
