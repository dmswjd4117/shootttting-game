using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBackgroundScript : MonoBehaviour
{
    float speed = 2;
    SpriteRenderer spr;
    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * speed;
        Vector3 pos = transform.position;

        if(pos.x + spr.bounds.size.x / 2 < -20)
        {
            float size = spr.bounds.size.x * 3;
            pos.x += size;
            transform.position = pos;
        }
    }
}
