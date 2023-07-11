# Change And Drop
### Change And Drop 을 모방한 게임 입니다.
- 레벨은 총 3 레벨이 있으며 상자에 공이 담기면 다음 레벨로 올라갑니다.
- 클릭 시 공이 떨어지게 되어 있으며 게임이 시작 한 후 클릭으로 공의 색을 변경 할 수 있습니다.

### 게임 매니저 소스 코드
- 게임 매니저로 씬 이동과 게임오버 델리게이트 그리고 공의 수에 대한 프로펕티를 설정했습니다. 
```using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene
{
    Start,
    Game
}
public class GameManager : Singleton<GameManager>
{

    public Action onGameOverEvent;
    public Scene currentScene;
    public int currentLevel;
    private GameManager() { }

    public int currentBallCount;
    private int deletBallCount;

    public int CurrentBallCount
    {
        get => currentBallCount;
        set
        {
            currentBallCount = value;
            if (currentBallCount <= 0)
            {
                onGameOverEvent?.Invoke();
            }
        }
    }

    public int DeletBallCount
    {
        get => deletBallCount;
        set => deletBallCount = value;
    }

    private void Start()
    {
        currentScene = Scene.Start;
        CurrentBallCount = 5;
    }

    public void GameOver()
    {
        onGameOverEvent?.Invoke();
    }

    public void StageLoad(int level)
    {
        currentLevel = level;
        currentScene = Scene.Game;
        SceneManager.LoadScene((int)Scene.Game);
        currentBallCount = 5;
        deletBallCount = 0;
    }

    private void OnApplicationQuit()
    {
        Time.timeScale = 0;
    }
}
```

### 블록에 닿을 시 공의 수를 증가 하거나 감소하는 부분

```
private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball"))
        {
            if(!onSplinter)
            {
                return;
            }

            Ball ball = other.GetComponent<Ball>();
            ball.Init();
            BallPool.ReturnObject(ball);
            --GameManager.Instance.CurrentBallCount;
            ++GameManager.Instance.DeletBallCount;
        }
    }
```
