using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool isPause;
    [SerializeField] GameObject pausePanel;

    bool isLevelComplete;
    [SerializeField] GameObject levelCompletePanel;
    
    bool isLevelFailed;
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
