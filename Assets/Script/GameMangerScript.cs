using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMangerScript : MonoBehaviour
{
    public static GameMangerScript instance;
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

    public void PauseEventListener()
    {
        print("pause");
    }
}
