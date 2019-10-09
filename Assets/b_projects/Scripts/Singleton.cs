using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{

    public static bool applicationIsQuitting = false;

    private static T _Instance;
    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                return null;
            }
            if (_Instance == null)
            {
                GameObject container = new GameObject(typeof(T).ToString());
                _Instance = container.AddComponent<T>();
            }
            return _Instance;
        }
    }

    virtual public void Awake()
    {
        if (_Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            _Instance = this as T;
        }
        else if (_Instance != this)
        {
            Destroy(gameObject);
        }
    }

    virtual public void OnDestroy()
    {
        applicationIsQuitting = true;
    }


}