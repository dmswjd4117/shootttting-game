using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://velog.io/@kjms830/%EC%9C%A0%EB%8B%88%ED%8B%B0-%EC%94%AC-%EB%B3%80%EA%B2%BD%EC%8B%9C-%EB%8D%B0%EC%9D%B4%ED%84%B0-%EC%9C%A0%EC%A7%80

public class DataMangerScript : MonoBehaviour
{
    public static DataMangerScript instance;

    public float totalCoin;
    public int stage;

    private string TOTAL_COIN = "TotalCoin";
    private string STAGE = "Stage";
    private int FINAL_STAGE = 4;

    private int[] clearKillPerStage = new int[5] { 0, 1, 2, 4, 5 };

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

    public void resetGame() {
        PlayerPrefs.SetFloat(TOTAL_COIN, 0);
        PlayerPrefs.SetInt(STAGE, 1);
        this.stage = 1;
        this.totalCoin = 0;
    }
}
