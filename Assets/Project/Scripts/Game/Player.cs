using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Visuals")]
    public Camera PlayerCamera;

    [Header("Gameplay")]
    public int initialHealth = 100;
    public int initialAmmo = 12;
    public float knockbackForce = 10;
    public float hurtDuration = 0.1f;

    private int health;
    public int Health { get { return health; } }

    private int ammo;
    public int Ammo { get { return ammo; } }

    private bool killed;
    public bool Killed { get { return killed; } }

    private bool isHurt;

    // Start is called before the first frame update
    void Start()
    {
        health = initialHealth;
        ammo = initialAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
           
            if(ammo > 0 && Killed == false)
            {
                ammo--;
                GameObject bulletObject = ObjectPoolingManager.Instance.GetBullet(true);
                bulletObject.transform.position = PlayerCamera.transform.position + PlayerCamera.transform.forward;
                bulletObject.transform.forward = PlayerCamera.transform.forward;
            }

        }
    }

    //Check for collisions
    private void OnTriggerEnter(Collider otherCollider)
    {
        
        if(otherCollider.GetComponent<AmmoCrate>() != null)
        {
            //Collect ammo crate;
            AmmoCrate ammoCrate = otherCollider.GetComponent<AmmoCrate>();
            ammo += ammoCrate.ammo;
            Destroy(ammoCrate.gameObject);
        }

        else if (otherCollider.GetComponent<HealthCrate>() != null)
        {
            //Collect health crate;
            HealthCrate healthCrate = otherCollider.GetComponent<HealthCrate>();
            health += healthCrate.health;
            Destroy(healthCrate.gameObject);
        }


        if (isHurt == false)
        {
            GameObject hazzard = null;
            if(otherCollider.GetComponent<Enemy>() != null)
            {
                //Touching enemies
                Enemy enemy = otherCollider.GetComponent<Enemy>();
                if (enemy.Killed == false)
                {
                    hazzard = enemy.gameObject;
                    health -= enemy.damage;
                } 
            } 
            
            else if(otherCollider.GetComponent<Bullets>() != null)
            {
                Bullets bullet = (otherCollider.GetComponent<Bullets>());
                if (bullet.ShotByPlayer == false)
                {
                    hazzard = bullet.gameObject;
                    health -= bullet.damage;
                    bullet.gameObject.SetActive(false);
                }
            }

            if(hazzard != null)
            {
                isHurt = true;


                //perform the knockback
                Vector3 hurtDirection = (transform.position - hazzard.transform.position).normalized;
                Vector3 knockbackDirection = (hurtDirection + Vector3.up).normalized;
                GetComponent<ForceReceiver>().AddForce(knockbackDirection, knockbackForce);
                StartCoroutine(HurtRoutine());
            }

            if(health <= 0)
            {
                if(killed == false)
                {
                    killed = true;
                    OnKill();
                }
            }

        }




    }
    IEnumerator HurtRoutine()
    {
        yield return new WaitForSeconds(hurtDuration);
        isHurt = false;

    }

    private void OnKill()
    {
        GetComponent<CharacterController>().enabled = false;
        GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
    }

}
