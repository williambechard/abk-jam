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
    private static int _score;
    private static bool _isGoalMet;


    public void init()
    {
        _isPaused = false;
        _score = 0;
        _isGoalMet = false;
    }

    public static bool IsGoalMet
    {
        get { return _isGoalMet; }
        set { _isGoalMet = value; }
    }

    public static int Score
    {
        get { return _score; }
        set { _score = value; }
    }


    public static bool IsPaused
    {
        get { return _isPaused; }
        set { _isPaused = value; }
    }

    public void Reset()
    {
        init();
        //unload the scene
        //LevelManager.Instance.UnloadScene("Game");
        //LevelManager.Instance.UnloadScene("GameOverlay");
        //Invoke("LoadGameScene", 2f);

    }

    void LoadGameScene()
    {
        //Debug.Log("Loading Game Scene from invoke");
        //LevelManager.Instance.LoadSceneAdditive("Game");
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

        init();
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
