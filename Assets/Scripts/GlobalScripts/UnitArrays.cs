using UnityEngine;
using System.Collections;

public class UnitArrays : MonoBehaviour {

    GameObject[] allies = new GameObject[50];
    GameObject[] enemies = new GameObject[50];
	
    public void add(GameObject unit, string s)
    {
        if (s == "playerUnit"){
            for (int i = 0; i < allies.Length; i++) {
                if(allies[i] == null){
                    allies[i] = unit;
                    return; 
                }
            }
        }
		else if (s == "enemyUnit") {
            for (int i = 0; i < enemies.Length; i++) {
                if (enemies[i] == null) {
                    enemies[i] = unit;
                    return;
                }
            }
        }
    }

    public void remove(GameObject unit, string s)
    {
        if (s == "playerUnit") {
            for (int i = 0; i < allies.Length; i++) {
                if (allies[i] == unit){
                    allies[i] = null;
                }
            }
        }
        else if (s == "enemyUnit") {
            for (int i = 0; i < enemies.Length; i++) {
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
            for (int i = 0; i < allies.Length; i++) {
                if (allies[i] != null){
                    GameObject o = allies[i];
                    Vector3 enemyDir = o.transform.position - scanner.transform.position;
                    float enemyDist = enemyDir.sqrMagnitude;
                    if (enemyDist <= distance || target == null) {
                        distance = enemyDist;
                        target = o;
                    }
                }
            }
        }
        else if (s == "Enemy") {
            for (int i = 0; i < enemies.Length; i++){
                if(enemies[i] != null) {
                    GameObject o = enemies[i];
                    Vector3 enemyDir = o.transform.position - scanner.transform.position;
                    float enemyDist = enemyDir.sqrMagnitude;
                    if (enemyDist <= distance || target == null){
                        distance = enemyDist;
                        target = o;
                    }
                }  
            }
        }
        return target;
    }
}
