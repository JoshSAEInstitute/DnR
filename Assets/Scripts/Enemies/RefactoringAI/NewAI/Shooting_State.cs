using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting_State : MonoBehaviour
{
    //Sight
    private Transform player;

    //Animation
    private Animator anim;

    //Shooting
    public float shootingRange;
    public float fireRate = 1;
    private float nextFire;

    public GameObject bullet;
    public GameObject bulletParent;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }
}
