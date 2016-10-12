using UnityEngine;
using System.Collections;

public class EnemyMelee_AI_Movement : MonoBehaviour {

    public float speed = 3f;
    float MeleeRange = 3f;

    // Use this for initialization
    GameObject pathGO;
    Transform targetPathNode;
    Transform unitTransform;
    int enemyPathNodeIndex = 0;
    bool isInMeleeRange;
    public GameObject nearestPlayer;
    float dist = Mathf.Infinity;
    GameObject unitManager;

    // Use this for initialization
    void Start() {

        if (Random.Range(0, 100) < 50) {
            pathGO = GameObject.Find("EnemyPathA");
            Debug.Log("A");
        } else {
            pathGO = GameObject.Find("EnemyPathB");
            Debug.Log("A");
        }
        isInMeleeRange = false;
    }

    void GetNextPathNode() {
        targetPathNode = pathGO.transform.GetChild(enemyPathNodeIndex);
        enemyPathNodeIndex++;
    }

    // Update is called once per frame
    void Update() {

        unitManager = GameObject.Find("UnitManager");
        nearestPlayer = unitManager.GetComponent<UnitArrays>().scan(this.gameObject, "Ally");

        if (nearestPlayer != null) {

            dist = Vector3.Distance(transform.position, nearestPlayer.transform.position);

            if (dist < 50 && dist > 1)
            {
                transform.position = Vector3.MoveTowards(this.transform.position, nearestPlayer.transform.position, speed * Time.deltaTime);

                if (nearestPlayer == null)
                {
                    //no players?
                    Debug.Log("No enemies?");
                }

                //Vector3 Lookdir = nearestPlayer.transform.position - transform.position;
                //Quaternion lookRot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Lookdir), 360 * Time.deltaTime);
                //transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

                if (dist < MeleeRange)
                {
                    isInMeleeRange = true;
                }

                //Debug.Log (isInMeleeRange);
            }
       } else if (isInMeleeRange == false)
            {
                if (targetPathNode == null) {
                    GetNextPathNode();
                    if (targetPathNode == null) {
                        //at player base
                        ReachedPlayerBase();
                    }
                }
                Vector3 dir = targetPathNode.position - this.transform.localPosition;

                float distThisFrame = speed * Time.deltaTime;

                if (dir.magnitude <= distThisFrame) {
                    // we reached the node
                    targetPathNode = null;
                } else {
                    //TODO: add the A* pathfinding instead
                    //move towards node
                    transform.Translate(dir.normalized * distThisFrame, Space.World);
                    Quaternion targetRotation = Quaternion.LookRotation(dir);
                    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * 5);
                }

            }

        }

        void ReachedPlayerBase(){
            GameObject.FindObjectOfType<ScoreManager>().LoseLife();
            Destroy(gameObject);
        }
    }