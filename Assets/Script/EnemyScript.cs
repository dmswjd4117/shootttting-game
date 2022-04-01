using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject enemyShot;
    public int type = 0;
    public int hp = 1;
    public float speed = 1;
    public float maxShotTime = 1;
    public float shotSpeed = 1;
    public float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        switch (type)
        {
            case 0:
                hp = 1;
                speed = 1f;
                maxShotTime = 1.5f;
                shotSpeed = 2;
                break;
            case 1:
                hp = 2;
                speed = 1.3f;
                maxShotTime = 1.3f;
                shotSpeed = 3;
                break;
            case 2:
                hp = 3;
                speed = 1.5f;
                maxShotTime = 1;
                shotSpeed = 5;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > maxShotTime)
        {
            GameObject shotObject = Instantiate(enemyShot, transform.position, Quaternion.identity);
            EnemyShotScript shotScript = shotObject.GetComponent<EnemyShotScript>();
            shotScript.speed = shotSpeed;
            time = 0;
        }
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
