using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : Component 
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject go = new GameObject(typeof(T).Name);
                    instance = go.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    public virtual void Awake()
    {
        if ( instance == null)
        {
            instance = this as T;
            if (Application.isPlaying)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
