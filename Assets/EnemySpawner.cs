using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
    //public Vector3 spawnValues;
    public int enemyCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                //Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);

                //Quaternion spawnRotation = Quaternion.identity;
                Instantiate(enemy, this.transform.position, this.transform.rotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

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
}
