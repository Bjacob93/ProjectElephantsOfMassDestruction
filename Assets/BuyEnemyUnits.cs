using UnityEngine;
using System.Collections;

public class BuyEnemyUnits : MonoBehaviour
{

    public float restartDelay = 10f;         // Time to wait before restarting the level
    float restartTimer;                     // Timer to count up to restarting the level

    public GameObject enemyUnits;
    int enemymoney = 500;

    void start()
    {
        
    }

    void Update()
    {
        
        Debug.Log("her");
        restartTimer += Time.deltaTime;
        ScoreManager sm = GameObject.FindObjectOfType<ScoreManager>();
        if (enemymoney <= 0)
        {
            Debug.Log("Not enought money");
            return;
        }



        if (restartTimer >= restartDelay)
        {
            if (enemymoney > 0)
            {
                enemymoney -= GameObject.FindObjectOfType<AlliedMelee_AI_Health>().cost;
                Instantiate(enemyUnits, new Vector3(-45, 0, -45), Quaternion.Euler(0, 0, 0));
                restartTimer = 0;
            }

        }

        //Debug.Log("fuck");
    }

    void OnMouseDown()
    {

    }
}
