using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameMangerScript : MonoBehaviour
{
    public static GameMangerScript instance;
    public GameObject pausePanel;
    public GameObject retryPanel;
    public GameObject clearPanel;
    public GameObject FinalClearPanel;


    public Text RetryPanel_coinText;
    public Text RetryPanel_stageText;
    public Button RetryPanel_retryBtn;
    public Button RetryPanel_MainBtn;


    public Text ClearPanel_coinText;
    public Text ClearPanel_stageText;
    public Button ClearPanel_nextStepBtn;
    public Button ClearPanel_MainBtn;

    public Text FinalClearPanel_coinText;
    public Text FinalClearPanel_stageText;
    public Button FinalClearPanel_MainBtn;


    public Text GameState_curStage;
    public Text GameState_remainEnemy;


    public Text coinText;
    public GameObject asteroid;
    public List<GameObject> enimies;
    public float time = 0;
    public float MAX_TIME;
    public float tempCoin;

    public int kill;

    private void Awake()
    {
        instance = this;
    }
 
    void Start()
    {
        tempCoin = 0;
        kill = 0;
        MAX_TIME = 2;
        UpdateCoinText();
        UpdateGameStateText();
    }

 
    void Update()
    {
        time += Time.deltaTime;
        if(time > DataMangerScript.instance.getEnemyMaxTime())
        {
            int temp = Random.Range(0, 2);
            if(temp == 0)
            {
                Instantiate(asteroid, new Vector3(10, Random.Range(-4.0f, 4.0f), 0), Quaternion.identity);
            }
            else if(temp == 1)
            {
                int type = Random.Range(0, 3);

                Instantiate(DataMangerScript.instance.getEnimies()[type], new Vector3(10, Random.Range(-4.0f, 4.0f), 0), Quaternion.identity);
            }
            time = 0;
        }
    }


    public void KillEnemy()
    {
        this.kill += 1;
        UpdateCurRemainEnemyText();
        if (this.kill >= DataMangerScript.instance.getClearKillPerStage()) 
        {
            if (DataMangerScript.instance.isLastStage())
            {
                ActiveFinalClearPanel();
                return;
            }

            ActiveClearPanel();
        }
    }

    // coin text
    private void AddCoin(float coin)
    {
        tempCoin += coin;
        DataMangerScript.instance.AddCoin(coin);
    }

    private void UpdateCoinText()
    {
        coinText.text = DataMangerScript.instance.GetCoin().ToString();
    }


    public void AddandUpdateCoinText(float coin)
    {
        AddCoin(coin);
        UpdateCoinText();
    }


    // Resume panel 
    public void ResumeEventListener()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void MainListener()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        SceneManager.LoadScene("MainScene");
    }

    public void PausePanelEventListener()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }


    // Retry panel

    public void RetryEventListener()
    {
        retryPanel.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

    public void RetryMainListener()
    {
        retryPanel.SetActive(false);
        SceneManager.LoadScene("MainScene");
    }

    public void ActiveRetryPanel()
    {
        RetryPanel_coinText.text = tempCoin.ToString();
        RetryPanel_stageText.text = DataMangerScript.instance.GetStage().ToString();
        Time.timeScale = 0;
        retryPanel.SetActive(true);
    }


    // Retry panel

    public void ClearEventListener()
    {
        clearPanel.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

    public void ClearMainListener()
    {
        clearPanel.SetActive(false);
        SceneManager.LoadScene("MainScene");
    }

    public void ActiveClearPanel()
    {        
        int curStage = DataMangerScript.instance.GetStage();
        ClearPanel_coinText.text = tempCoin.ToString();
        ClearPanel_stageText.text = curStage.ToString();
        Time.timeScale = 0;
        clearPanel.SetActive(true);

        DataMangerScript.instance.AddStage();
    }


    // final clear panel

    public void ActiveFinalClearPanel()
    {
        int curStage = DataMangerScript.instance.GetStage();
        FinalClearPanel_coinText.text = tempCoin.ToString();
        FinalClearPanel_stageText.text = curStage.ToString();
        FinalClearPanel.SetActive(true);
        Time.timeScale = 0;
        DataMangerScript.instance.resetGame();
    }

    public void FinalClearMainListener()
    {
        FinalClearPanel.SetActive(false);
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1;
    }



    // 게임화면에 표시되는 현재 스테이지와 남은적 텍스트 표시하기
    public void UpdateGameStateText()
    {
        UpdateCurStageText();
        UpdateCurRemainEnemyText();
    }

    public void UpdateCurStageText()
    {
        GameState_curStage.text = DataMangerScript.instance.GetStage().ToString();
    }

    public void UpdateCurRemainEnemyText()
    {
        int remain = DataMangerScript.instance.getClearKillPerStage() - kill;
        GameState_remainEnemy.text = remain.ToString();
    }
}
