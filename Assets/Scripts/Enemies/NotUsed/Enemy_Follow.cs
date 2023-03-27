using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Follow : MonoBehaviour
{
    public float speed;
    public float lineOfSight;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if(distanceFromPlayer < lineOfSight)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        */

        //Vector2.Distance
        float distanceFromPlayer = Approach(player.position, transform.position);
        if (distanceFromPlayer < lineOfSight)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }



    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    }

    private float Approach(Vector2 v1, Vector2 v2)
    {
        /*
         * What I'm trying to achieve:
         * Root(1.x - 2.x)^2 + (1.y - 2.y)^2
         */

        //1.x - 2.x, 1.y - 2.y
        float x = v1.x - v2.x;
        float y = v1.y - v2.y;

        //Square x y
        x = Mathf.Pow(x, 2);
        y = Mathf.Pow(y, 2);

        //Add x y
        float sqXY = x + y;

        //Square root
        return Mathf.Sqrt(sqXY);
    }
}
