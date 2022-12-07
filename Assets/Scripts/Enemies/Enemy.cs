using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //Enemy speed
    public float speed;

    //Enemy line of sight
    public float lineOfSight;
    private Transform player;

    //Faceplayer
    private Vector3 tempScale;

    //Health
    public int maxHP;
    public int currentHP;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHP = maxHP;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
