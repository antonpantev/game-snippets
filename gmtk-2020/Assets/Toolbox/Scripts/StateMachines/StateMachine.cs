using UnityEngine;

namespace Toolbox
{
    public class StateMachine<T> : State where T : State
    {
        [SerializeField]
        T active;

        public virtual T Active
        {
            get
            {
                return active;
            }

            set
            {
                if (active != null)
                {
                    active.StateEnd();
                }

                active = value;

                if (active != null)
                {
                    active.StateStart();
                }
            }
        }

        public virtual void Start()
        {
            if (Active != null)
            {
                Active.StateStart();
            }
        }

        public virtual void Update()
        {
            if (Active != null)
            {
                Active.StateUpdate();
            }
        }

        bool isQuitting;

        public virtual void OnApplicationQuit()
        {
            isQuitting = true;
        }

        public virtual void OnDestroy()
        {
            if (!isQuitting && Active != null)
            {
                Active.StateEnd();
            }
        }

        public virtual void Clear(State s)
        {
            if (Active == s)
            {
                Active = null;
            }
        }
    }

    public class StateMachine : StateMachine<State> { }
}