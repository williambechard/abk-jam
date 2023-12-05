using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject singleton = new GameObject("GameManagerSingleton");
                    _instance = singleton.AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }

    private static bool _isPaused;

    public static bool IsPaused
    {
        get { return _isPaused; }
        set { _isPaused = value; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void PauseGame()
    {
        IsPaused = true;
        // Add any other pause-related logic here
    }

    public void ResumeGame()
    {
        IsPaused = false;
        // Add any other resume-related logic here
    }
}
