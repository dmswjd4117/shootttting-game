using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 유니티 씬 변경시 데이터와 gameObject 유지
// https://velog.io/@kjms830/%EC%9C%A0%EB%8B%88%ED%8B%B0-%EC%94%AC-%EB%B3%80%EA%B2%BD%EC%8B%9C-%EB%8D%B0%EC%9D%B4%ED%84%B0-%EC%9C%A0%EC%A7%80


public class GameMangerScript : MonoBehaviour
{
    public static GameMangerScript instance;
    public GameObject pausePanel;
    public Text coinText;
    public GameObject asteroid;
    public List<GameObject> enimies;
    public float time = 0;
    public float MAX_TIME = 2;
    public float coinSum;

    public void UpdateCoinText()
    {
        coinText.text = coinSum.ToString();
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        coinSum = 0;
        UpdateCoinText();
    }

    // Update is called once per frame
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

    public void ResumeEventListener()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void MainListener()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void PauseEventListener()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
}
