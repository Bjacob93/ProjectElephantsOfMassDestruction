using UnityEngine;
using System.Collections;

public class EnemyOrders : MonoBehaviour {

    float controlRange;
    public Vector3 targetLocation1;
    public Vector3 targetLocation2;
    public bool hasMultipleTargets;
    public int split;
    int splitIndex;

	// Use this for initialization
	void Start () {

        controlRange = 5f;
        splitIndex = 1;
	}
	
	// Update is called once per frame
	void Update () {
        GameObject[] units = GameObject.FindGameObjectsWithTag("enemyUnits");

        if (!hasMultipleTargets && targetLocation2 != new Vector3(0,0,0))
        {
            targetLocation1 = targetLocation2;
        }

        foreach (GameObject e in units)
        {
            float distance = Vector3.Distance(transform.position, e.transform.position);
            if (distance < controlRange)
            {
                AstarEnemy aStarEnemy = e.GetComponent<AstarEnemy>();
                if(aStarEnemy.commanderID != gameObject.name)
                {
                    if (hasMultipleTargets)
                    {
                        if (splitIndex % split == 0)
                        {
                            aStarEnemy.targetPosition = targetLocation1;
                            aStarEnemy.receivedNewDestination = true;
                            aStarEnemy.commanderID = gameObject.name;
                        }
                        else
                        {
                            aStarEnemy.targetPosition = targetLocation2;
                            aStarEnemy.receivedNewDestination = true;
                            aStarEnemy.commanderID = gameObject.name;
                        }
                        splitIndex++;
                    }
                    else
                    {
                        aStarEnemy.targetPosition = targetLocation1;
                        aStarEnemy.receivedNewDestination = true;
                        aStarEnemy.commanderID = gameObject.name;
                    }
                }
            }
        }
	}
}
