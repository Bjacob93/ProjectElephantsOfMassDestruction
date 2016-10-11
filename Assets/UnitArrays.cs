using UnityEngine;
using System.Collections;

public class UnitArrays : MonoBehaviour {

    GameObject[] allies = new GameObject[50];
    GameObject[] enemies = new GameObject[50];

	// Use this for initialization
	void Start () {
	
	}
	
    public void add(GameObject unit)
    {
        if (unit.tag == "playerUnits"){
            for (int i = 0; i<allies.Length; i++) {
                if(allies[i] == null){
                    allies[i] = unit;
                }
            }
        }
		else if (unit.tag == "enemyUnits") {
            for (int i = 0; i < allies.Length; i++) {
                if (enemies[i] == null) {
                    enemies[i] = unit;
                }
            }
        }
        else return;
    }

    public void remove(GameObject unit)
    {
        if (unit.tag == "playerUnits") {
            for (int i = 0; i < allies.Length; i++) {
                if (allies[i] == unit){
                    allies[i] = null;
                }
            }
        }
        else if (unit.tag == "enemyUnits") {
            for (int i = 0; i < allies.Length; i++) {
                if (enemies[i] == unit) {
                    enemies[i] = null;
                }
            }
        }
        else return;
    }

    public GameObject scan(GameObject scanner, string s) {

        GameObject target = null;
        float distance = Mathf.Infinity;

        if (s == "Ally") {
            foreach (GameObject o in allies) {
                float enemyDist = Vector3.Distance(scanner.transform.position, o.transform.position);
                if (enemyDist <= distance || target == null) {
                    distance = enemyDist;
                    target = o;

                   
                }
            }
            if (target == null) Debug.Log("No target");
        }
        else if (s == "Enemy") {
            foreach (GameObject o in enemies){
                float enemyDist = Vector3.Distance(scanner.transform.position, o.transform.position);
                if (enemyDist <= distance || target == null){
                    distance = enemyDist;
                    target = o;
                }
            }
        }

        return target;
    }

	// Update is called once per frame
	void Update () {
	
	}
}
