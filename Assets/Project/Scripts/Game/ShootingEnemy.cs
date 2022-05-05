using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootingEnemy : Enemy
{
    public float shootingInterval = 4f;
    public float shootingDistance = 3f;
    public float chasingInterval = 2f;
    public float chasingDistance = 12f;
    
    
    private Player player;
    private float shootingTimer;
    private float chasingtimer;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.Find("Player").GetComponent<Player>();
        agent = GetComponent<NavMeshAgent>();
        shootingTimer = Random.Range(0, shootingInterval);

        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.Killed == true)
        {
            agent.enabled = false;
            this.enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }
        //Shooting logic
        shootingTimer -= Time.deltaTime;
        if(shootingTimer <= 0 && Vector3.Distance(transform.position, player.transform.position) <= shootingDistance)
        {
            shootingTimer = shootingInterval;
            GameObject bullet = ObjectPoolingManager.Instance.GetBullet(false);
            bullet.transform.position = transform.position;
            bullet.transform.forward = (player.transform.position - transform.position).normalized;// 20
        }

        //chasing logic
        chasingtimer -= Time.deltaTime;
        if(chasingtimer <= 0 && Vector3.Distance(transform.position, player.transform.position) <= chasingDistance)
        {
            chasingtimer = chasingInterval;
            agent.SetDestination(player.transform.position);
        }    
    }

    protected override void OnKill()
    {
        base.OnKill();
        agent.enabled = false;
        this.enabled = false;
        //tips the enemy over when shot
        transform.localEulerAngles = new Vector3(10, transform.localEulerAngles.y, transform.eulerAngles.z);
    }
}
