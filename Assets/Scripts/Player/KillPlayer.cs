using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{

    private Health checkHealth;

    //Where to respawn the player
    public int Respawn;

    // Start is called before the first frame update
    void Start()
    {
        checkHealth = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (checkHealth.currentHealth <= 0)
        {
            SceneManager.LoadScene(Respawn);
        }
    }
}
