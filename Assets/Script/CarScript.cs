using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
   
    public float speed;
    Vector3 startPos;
    bool clicked = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(!this.clicked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                this.startPos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Vector3 endPoint = Input.mousePosition;
                float len = endPoint.x - startPos.x;
                this.speed = len / 500.0f;
    
                this.clicked = true;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                this.speed = 0;
                transform.position = new Vector3(-7.16f, -3.12f, 0);
                //this.clicked = false;
            }

        }

        transform.Translate(this.speed, 0, 0);
        //print(this.speed);
        this.speed *= 0.98f;
    }
}
