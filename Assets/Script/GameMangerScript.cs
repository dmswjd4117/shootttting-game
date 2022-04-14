using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 유니티 씬 변경시 데이터와 gameObject 유지
// https://velog.io/@kjms830/%EC%9C%A0%EB%8B%88%ED%8B%B0-%EC%94%AC-%EB%B3%80%EA%B2%BD%EC%8B%9C-%EB%8D%B0%EC%9D%B4%ED%84%B0-%EC%9C%A0%EC%A7%80


public class GameMangerScript : MonoBehaviour
{
    public static GameMangerScript instance;
    public GameObject pausePanel;
    public GameObject retryPanel;
    public GameObject clearPanel;

    public Text RetryPanel_coinText;
    public Text RetryPanel_stageText;
    public Button RetryPanel_retryBtn;
    public Button RetryPanel_MainBtn;


    public Text ClearPanel_coinText;
    public Text ClearPanel_stageText;
    public Button ClearPanel_nextStepBtn;
    public Button ClearPanel_MainBtn;


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
    }

 
    void Update()
    {
        time += Time.deltaTime;
        if(time > MAX_TIME)
        {
            int temp = Random.Range(0, 2);
            if(temp == 0)
            {
                Instantiate(asteroid, new Vector3(10, Random.Range(-4.0f, 4.0f), 0), Quaternion.identity);
            }
            else if(temp == 1)
            {
                int type = Random.Range(0, 3);
               
                Instantiate(enimies[type], new Vector3(10, Random.Range(-4.0f, 4.0f), 0), Quaternion.identity);
            }
            time = 0;
        }
    }


    public void KillEnemy()
    {
        this.kill += 1;
        if(this.kill >= DataMangerScript.instance.getClearKillPerStage()) 
        {
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
}
