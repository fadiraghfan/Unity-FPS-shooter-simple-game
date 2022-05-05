using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Game")]
    public Player player;
    public GameObject enemyContainer;
    public GameObject Background;


    [Header("UI")]
    public Text healthText;
    public Text ammoText;
    public Text enemyText;
    public Text infoText;
   


    private bool gameOver = false;
    private float resetTimer = 5f;

    private int initialEnemyCount;

    private void Start()
    {
        initialEnemyCount = enemyContainer.GetComponentsInChildren<Enemy>().Length;
        infoText.gameObject.SetActive(false);
        Background.gameObject.SetActive(false);
        
       
    }

    // Update is called once per frame
    void Update()
    {
        ammoText.text = "Ammo: " + player.Ammo;
        healthText.text = "Health: " + player.Health;

        int killedEnemies = 0;
        foreach(Enemy enemy in enemyContainer.GetComponentsInChildren<Enemy>())
        {
            if(enemy.Killed == true)
            {
                killedEnemies++;
            }
        }

        enemyText.text = "Enemeies: " + (initialEnemyCount - killedEnemies);
        if(initialEnemyCount - killedEnemies == 0)
        {
            gameOver = true;
            infoText.gameObject.SetActive(true);
            infoText.text = "You Win! \n Good job!";
        }

        if(player.Killed == true)
        {
            gameOver = true;
            infoText.gameObject.SetActive(true);
            Background.gameObject.SetActive(true);
            infoText.text = "- We're finally safe...  -I'm so glad it's over";
        }

        if(gameOver == true)
        {
            resetTimer -= Time.deltaTime;
            if(resetTimer <= 0)
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
 
}
