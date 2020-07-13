using UnityEngine;

namespace Toolbox
{
    public class StateMachineManager<S, T> : StateMachine<T> where S : StateMachineManager<S, T> where T : State
    {
        public static S Instance { get; private set; }

        public virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = FindObjectOfType<S>();
            }
        }
    }

    public class StateMachineManager : StateMachineManager<StateMachineManager, State> { }
}