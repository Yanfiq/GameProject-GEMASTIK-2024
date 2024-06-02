using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    bool isPause;
    [SerializeField] GameObject pausePanel;

    bool isLevelComplete;
    public bool IsLevelComplete { set { isLevelComplete = value; } }
    [SerializeField] GameObject levelCompletePanel;
    
    bool isLevelFailed;
    public bool IsLevelFailed { set { isLevelFailed = value; } }
    [SerializeField] GameObject levelFailedPanel;

    void Start()
    {
        isPause = false;
        isLevelComplete = false;
        isLevelFailed = false;
    }

    public void TogglePause() { isPause = !isPause; }
    public void LevelComplete() { isLevelComplete = true;  }
    public void LevelFailed() { isLevelFailed = true;  }

    void Update()
    {
        
    }
}
