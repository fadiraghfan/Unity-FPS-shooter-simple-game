using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float lifeDuration = 2f;
    public float speed = 8f;
    public int damage = 5;


    private float lifeTimer;

    private bool shotByPlayer;
    public bool ShotByPlayer { get { return shotByPlayer; } set { shotByPlayer = value; } }



    // Start is called before the first frame update
    void OnEnable()
    {
        lifeTimer = lifeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        //MAKE THE BULLET MOVE
        transform.position += transform.forward * speed * Time.deltaTime;

        //check if bullet needs to be destroyed
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
