using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    private static ObjectPoolingManager instance;
    public static ObjectPoolingManager Instance { get { return instance; } }
    public GameObject bulletPrefab;
    public int bulletAmmount = 20;

    private List<GameObject> bullets;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        //Preload bullets
        bullets = new List<GameObject>(bulletAmmount);



        for (int i = 0; i < bulletAmmount; i++)
        {
            

            GameObject prefabInstance = Instantiate(bulletPrefab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);

            bullets.Add(prefabInstance);
        }
    }

    // Update is called once per frame
    public GameObject GetBullet(bool shotByPlayer)
    {
       foreach(GameObject bullet in bullets)
        {
            if(!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                bullet.GetComponent<Bullets>().ShotByPlayer = shotByPlayer;
                return bullet;
            }    
        }

        GameObject prefabInstance = Instantiate(bulletPrefab);
        prefabInstance.transform.SetParent(transform);
        prefabInstance.GetComponent<Bullets>().ShotByPlayer = shotByPlayer;
        bullets.Add(prefabInstance);

        return prefabInstance;
    }
}
