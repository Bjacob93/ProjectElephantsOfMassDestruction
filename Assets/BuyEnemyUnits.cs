using UnityEngine;
using System.Collections;

public class BuyEnemyUnits : MonoBehaviour {

    public float restartDelay = 5f;         // Time to wait before restarting the level
    float restartTimer;                     // Timer to count up to restarting the level

    public GameObject enemyUnits;
    int enemymoney = 500;

    void start() {
   
}

void update() {
        restartTimer += Time.deltaTime;

        if (gameObject.tag == "EnemyBase")
        {
            ScoreManager sm = GameObject.FindObjectOfType<ScoreManager>();
            if (enemymoney <=0)
            {
                Debug.Log("Not enought money");
                return;
            }

            if (enemymoney > 0)
            {
                Debug.Log("her");
                if (restartTimer >= restartDelay)
                {
                    enemymoney -= GameObject.FindObjectOfType<AlliedMelee_AI_Health>().cost;
                    Instantiate(enemyUnits, new Vector3(-45, 0, -45), Quaternion.Euler(0, 0, 0));
                }
               
            }

            //Debug.Log("fuck");
        }
    }

    void OnMouseDown()
    {
        
    }
}
