using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public float spawnCD = 0.25f;
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
				Destroy(gameObject);
			}
		
		}
	}
}
