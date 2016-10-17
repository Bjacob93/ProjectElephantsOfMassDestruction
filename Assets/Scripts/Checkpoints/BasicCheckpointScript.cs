using UnityEngine;
using System.Collections;

public class BasicCheckpointScript : MonoBehaviour {

    public Vector3 meleeDistination;
    public Vector3 rangedDistination;

    public float controlRange = 10f;

    //Bool that determines what kind of order a checkpoint should issue units.
    bool giveMoveOrder = false;
    bool giveDefenceOrder = false;
    bool giveAttackOrder = false;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame 
	void Update () {
        //Find all player units.
        GameObject[] units = GameObject.FindGameObjectsWithTag("playerUnits");

        //Issue the correct order
        if (giveMoveOrder)
        {
            Move(units);
        }else if (giveAttackOrder)
        {
            Attack(units);
        }else if (giveDefenceOrder)
        {
            Defend(units);
        }

        
	}

    void Move(GameObject[] units)
    {
        foreach (GameObject e in units)
        {
            float distance = Vector3.Distance(transform.position, e.transform.position);
            if (distance < controlRange)
            {
                //TODO: Check melee or ranged
                Astar aStar = e.GetComponent<Astar>();
                aStar.targetPosition = meleeDistination;
                aStar.receivedNewDestination = true;
            }
        }
    }

    void Attack(GameObject[] units)
    {
        foreach (GameObject e in units)
        {
            float distance = Vector3.Distance(transform.position, e.transform.position);
            if (distance < controlRange)
            {
                //TODO: Check melee or ranged
                Astar aStar = e.GetComponent<Astar>();
                aStar.targetPosition = meleeDistination;
                aStar.receivedNewDestination = true;
            }
        }
    }

    void Defend(GameObject[] units)
    {
        foreach (GameObject e in units)
        {
            float distance = Vector3.Distance(transform.position, e.transform.position);
            if (distance < controlRange)
            {
                //TODO: Check melee or ranged
                Astar aStar = e.GetComponent<Astar>();
                aStar.targetPosition = meleeDistination;
                aStar.receivedNewDestination = true;
                aStar.receivedDefenceOrder = true;
            }
        }
    }
}
