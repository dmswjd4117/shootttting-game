using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    public float speed = 10;
    private const int ATTACK = 1;
    public GameObject coinObj;
    public GameObject BurstObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Asteroid")
        {
            AsteroidScript asteroidScript = collision.gameObject.GetComponent<AsteroidScript>();
            asteroidScript.hp -= ATTACK;
            Instantiate(BurstObj, transform.position, Quaternion.identity);
            if (asteroidScript.hp <= 0)
            {
                Destroy(collision.gameObject);
                CoinScript coinScript = coinObj.GetComponent<CoinScript>();
                coinScript.coinValue = asteroidScript.coinValue;
                Instantiate(coinObj, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
        else if (collision.tag == "Enemy")
        {
            EnemyScript enemyScript = collision.gameObject.GetComponent<EnemyScript>();
            enemyScript.hp -= ATTACK;
            Instantiate(BurstObj, transform.position, Quaternion.identity);
            if (enemyScript.hp <= 0)
            {
                Destroy(collision.gameObject);
                CoinScript coinScript = coinObj.GetComponent<CoinScript>();
                coinScript.coinValue = enemyScript .coinValue;
                Instantiate(coinObj, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

}
