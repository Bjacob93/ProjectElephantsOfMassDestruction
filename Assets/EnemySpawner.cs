using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemiesArray;
    int index;

    public int enemyCount; //How many enemies each wave have
    public float spawnWait; //How long to wait in each wave between the spawn of an enemy
    public float startWait; //How long to wait in the beginning before a wave spawns
    public float waveWait; //How long to wait between each wave


    void Start()
    {
        StartCoroutine(SpawnWaves());
        enemiesArray = GameObject.FindGameObjectsWithTag("enemyUnits");
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                Instantiate(enemiesArray[Random.Range(0, enemiesArray.Length)], this.transform.position, this.transform.rotation); //instantiate a random game object
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }
}

    //OLD SPAWN WAVE CODE
    /*	public float spawnCD = 0.25f;
	public float spawnCDremaning = 0;

	[System.Serializable]
	public class WaveComponent {
		public GameObject enemyPrefab;
		public int num;
		[System.NonSerialized]
		public int spawned = 0;
	}

	public WaveComponent[] waveComps;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {		
		spawnCDremaning -= Time.deltaTime;
		if (spawnCDremaning <= 0) {
			spawnCDremaning = spawnCD;

			bool enemySpawn = false;

			//go throug the waveComps until there is something to spawn
			foreach(WaveComponent wc in waveComps){
				if (wc.spawned < wc.num) {
				//spawn enemy
					//make spawner face the first target
					wc.spawned++;
					Instantiate (wc.enemyPrefab, this.transform.position, this.transform.rotation);

					enemySpawn = true;
					break;
				}
			}
			if (enemySpawn == false) {
				//waveComps is hopefully over
				//TODO: Instantiate next wave
				if (transform.parent.childCount > 1) {
					transform.parent.GetChild (1).gameObject.SetActive (true);
				} else {
					//what can be done then
					//what if instad of destorying they were made inactive
					//and when all waves are finished
					//we restard the first one but double
					//all enemy health or something
				
				}
				Destroy(gameObject);
			}
		
		}
	} */
