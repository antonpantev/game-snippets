using UnityEngine;

namespace Toolbox
{
    /// <summary>
    /// This is a manager class that can be used for game objects that need to be
    /// singletons. Often used to hold game data or to control interactions
    /// across multiple game objects.
    /// </summary>
    public abstract class Manager<T> : MonoBehaviour where T : Manager<T>
    {
        public static T Instance { get; private set; }

        public virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = FindObjectOfType<T>();
            }
        }
    }
}