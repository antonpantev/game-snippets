using UnityEngine;

namespace Toolbox
{
    public class State : MonoBehaviour
    {
        public virtual void StateStart() { }
        public virtual void StateUpdate() { }
        public virtual void StateEnd() { }
    }
}