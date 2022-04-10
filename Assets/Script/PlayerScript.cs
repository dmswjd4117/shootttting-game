using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5f;
    public float shotDelay = 0;
    public float SHOT_MAX_TIME = 1f;

    public GameObject explosionObj;
    public GameObject shot;
    public Transform shotPointTf;
    Vector3 min, max;
    Vector2 chrSize;


    // Start is called before the first frame update
    void Start()
    {
        min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        Vector2 colliderSize = GetComponent<BoxCollider2D>().size;
        chrSize = new Vector2(colliderSize.x / 2, colliderSize.y / 2);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        PlayerShot();
    }

    private void PlayerShot()
    {
        shotDelay += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            if(shotDelay > SHOT_MAX_TIME)
            {
                shotDelay = 0;
                Instantiate(shot, shotPointTf.position, Quaternion.identity);
            }
        }
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(x, y, 0);

        transform.position += move * Time.deltaTime * speed;

        float newX = transform.position.x;
        float newY = transform.position.y;

        if (newX < min.x + chrSize.x)
        {
            newX = min.x + chrSize.x;
        }
        if (newX > max.x - chrSize.x)
        {
            newX = max.x - chrSize.x;
        }


        if (newY < min.y + chrSize.y)
        {
            newY = min.y + chrSize.y;
        }
        if (newY > max.y - chrSize.y)
        {
            newY = max.y - chrSize.y;
        }

        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Asteroid" ||
            collision.gameObject.tag == "Enemy" ||
            collision.gameObject.tag == "EnemyShot")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Instantiate(explosionObj, transform.position, Quaternion.identity);

            // retry panel
            GameMangerScript.instance.ActiveRetryPanel();
        }

        else if(collision.gameObject.tag == "Item")
        {
            CoinScript coinScript = collision.gameObject.GetComponent<CoinScript>();

            GameMangerScript.instance.AddandUpdateCoinText(coinScript.coinValue);

            Destroy(collision.gameObject);
        }
    }
}
