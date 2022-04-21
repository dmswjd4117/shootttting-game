using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://velog.io/@kjms830/%EC%9C%A0%EB%8B%88%ED%8B%B0-%EC%94%AC-%EB%B3%80%EA%B2%BD%EC%8B%9C-%EB%8D%B0%EC%9D%B4%ED%84%B0-%EC%9C%A0%EC%A7%80

public class DataMangerScript : MonoBehaviour
{
    public static DataMangerScript instance;
    public GameObject asteroidPref;
    public GameObject enemyA;
    public GameObject enemyB;
    public GameObject enemyC;
    

    public float totalCoin = 0;
    public int stage = 1;

    private string TOTAL_COIN = "TotalCoin";
    private string STAGE = "Stage";
    private int FINAL_STAGE = 4;

    private int[] clearKillPerStage = new int[5] { 0, 1, 2, 3, 5 };

    private void Start()
    {
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    
    public float GetCoin()
    {
        totalCoin = PlayerPrefs.GetFloat(TOTAL_COIN, 0);
        return totalCoin;
    }

    public void AddCoin(float coin)
    {
        this.totalCoin += coin;
        PlayerPrefs.SetFloat(TOTAL_COIN, this.totalCoin);
    }

    public int GetStage()
    {
        stage = PlayerPrefs.GetInt(STAGE, 1);
        return stage;
    }

    public void AddStage()
    {
        if(this.stage == FINAL_STAGE)
        {
            return;
        }
        this.stage += 1;
        PlayerPrefs.SetInt(STAGE, this.stage);
    }


    public int getClearKillPerStage()
    {
        return clearKillPerStage[stage];
    }


    public List<GameObject> getEnimies()
    {
        List<List<GameObject>> enimies = new List<List<GameObject>>();

        enimies.Add(new List<GameObject>());
        enimies.Add(new List<GameObject>());
        enimies.Add(new List<GameObject>());
        enimies.Add(new List<GameObject>());


        enimies[0].Add(enemyA);
        enimies[0].Add(enemyA);
        enimies[0].Add(enemyA);


        enimies[1].Add(enemyA);
        enimies[1].Add(enemyA);
        enimies[1].Add(enemyB);


        enimies[2].Add(enemyA);
        enimies[2].Add(enemyB);
        enimies[2].Add(enemyB);

        enimies[3].Add(enemyA);
        enimies[3].Add(enemyB);
        enimies[3].Add(enemyC);

        return enimies[GetStage() - 1];
    }

    public void resetGame() {
        PlayerPrefs.SetFloat(TOTAL_COIN, 0);
        PlayerPrefs.SetInt(STAGE, 1);
        this.stage = 1;
        this.totalCoin = 0;
    }

    public bool isLastStage()
    {
        return this.FINAL_STAGE == this.stage;
    }
}
