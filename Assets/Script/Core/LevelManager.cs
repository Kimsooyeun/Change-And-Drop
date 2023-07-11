using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> Level;

    [SerializeField]
    private GameObject GameClearPanel;

    [SerializeField]
    private GameObject GameFailedPanel;

    [SerializeField]
    private TextMeshProUGUI score;

    private int finalStage = 3;

    private void OnEnable()
    {
        Init();
        EndTrigger.onGameClearEvent += ActivationInfoPanel;
        GameManager.Instance.onGameOverEvent += ActivationGameFailedPanel;
    }

    public void Init()
    {
        Instantiate(Level[GameManager.Instance.currentLevel - 1], transform.position, transform.rotation);
    }

    public void LoadScene_NextStage()
    {
        if (GameManager.Instance.currentLevel == finalStage)
        {
            Debug.Log("마지막 스테이지 입니다.");
            return;
        }

        GameManager.Instance.StageLoad(GameManager.Instance.currentLevel + 1);
    }

    public void LoadeScene_RetryStage()
    {
        GameManager.Instance.StageLoad(GameManager.Instance.currentLevel);
    }

    public void ActivationInfoPanel()
    {

        score.text = GameManager.Instance.CurrentBallCount.ToString();
        GameClearPanel.SetActive(true);
    }

    public void DeActivationInfoPanel()
    {
        GameClearPanel.SetActive(false);
    }

    public void ActivationGameFailedPanel()
    {
        if (GameManager.Instance.currentBallCount <= 0)
        {
            Debug.Log("dddddd");
            GameFailedPanel.SetActive(true);
        }
    }

    public void DeActivationGameFailedPanel()
    {
        GameFailedPanel.SetActive(false);
    }

    private void OnDisable()
    {
        EndTrigger.onGameClearEvent -= ActivationInfoPanel;

        if (GameManager.Instance == null)
        {
            return;
        }

        GameManager.Instance.onGameOverEvent -= ActivationGameFailedPanel;
    }
}
