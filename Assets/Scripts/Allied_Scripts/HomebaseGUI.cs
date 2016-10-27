using UnityEngine;
using System.Collections;

public class HomebaseGUI : MonoBehaviour {

	//unit prduction management vars
	public float unitSpawnCoolDown = 5f;
	float unitSpawnCoolDownLeft = 0f;

	//unit which is spawned
	public GameObject playerUnits;

	//unit price
	GameObject unitManager;
	UnitPrices unitPrices;

	//score manager to keep track of money
	GameObject scoreHolder;
	ScoreManager scoreManager;


	public float controlRange = 10f;

    //Initialise variables to give the base it's own sequence editor.
    SequenceManager sequenceManager;
    string baseName;
    EditorList listComponent;

    //The base's location
    public Vector3 location;

    //Boolean for checking if the game is paused
    public bool gameIsPaused;

    // Use this for initialization
    void Start () {
		//find and cache unit manager script
		unitManager = GameObject.Find("UnitManager");
		unitPrices = unitManager.GetComponent<UnitPrices>();

		//find and cache the score manager
		scoreHolder = GameObject.Find("ScoreManager");
		scoreManager = scoreHolder.GetComponent<ScoreManager>();


        //Find the SequenceManager, and same the name of the checkpoint to a string.
        sequenceManager = GameObject.Find("UIManager").GetComponent<SequenceManager>();
        baseName = this.gameObject.name;


		// Find and Save the position of this gameObjects location for future reference.
		location = gameObject.transform.position;

        //Add the sequenceEditor component to the list in SequenceManager.
        listComponent = gameObject.AddComponent<EditorList>();
        listComponent.listID = baseName;
        listComponent.belongsToCheckpoint = false;
        sequenceManager.editorlistGO.Add(listComponent);

        gameIsPaused = true;
    }
	
	// Update is called once per frame
	void Update () {

		GameObject[] units = GameObject.FindGameObjectsWithTag("playerUnits");

        if (gameIsPaused)
        {
            return;
        }


		for (int i = 0; i < listComponent.slots.Count; i++) {
			if (i % 2 == 0) {
				string id = listComponent.slots [i].commandId;
				Vector3 target;

				switch (id) {
				case "A01":
					target = listComponent.slots [i + 1].locationOfTarget;
					Attack (units, target);
					break;

				case "M01":
					target = listComponent.slots [i + 1].locationOfTarget;
					Move (units, target);
					break;

				case "D01":
					target = listComponent.slots [i + 1].locationOfTarget;
					Defend (units, target);
					break;

				case "P01":
					target = gameObject.transform.position;
					ProduceUnit (target);
					break;

				default:
					
					break;
				}
			}
		}


	
	}

	void Move(GameObject[] units, Vector3 targetLocation)
	{
		//For each unit that exist belonging to the player, check if it is within control range, and give it a new destination if it is
		foreach (GameObject e in units)
		{
			float distance = Vector3.Distance(transform.position, e.transform.position);
			if (distance < controlRange)
			{
				Astar aStar = e.GetComponent<Astar>();
				aStar.targetPosition = targetLocation;
				aStar.receivedNewDestination = true;
			}
		}
	}

	void Attack(GameObject[] units, Vector3 targetLocation)
	{
		//For each unit that exist belonging to the player, check if it is within control range, and give it a new destination if it is
		foreach (GameObject e in units)
		{
			float distance = Vector3.Distance(transform.position, e.transform.position);
			if (distance < controlRange)
			{
				//TODO: Check melee or ranged
				Astar aStar = e.GetComponent<Astar>();
				aStar.targetPosition = targetLocation;
				aStar.receivedNewDestination = true;
			}
		}
	}

	void Defend(GameObject[] units, Vector3 targetLocation)
	{
		//For each unit that exist belonging to the player, check if it is within control range, and give it a new destination if it is
		foreach (GameObject e in units)
		{
			float distance = Vector3.Distance(transform.position, e.transform.position);
			if (distance < controlRange)
			{
				//TODO: Check melee or ranged
				Astar aStar = e.GetComponent<Astar>();
				aStar.targetPosition = targetLocation;
				aStar.receivedNewDestination = true;
				aStar.receivedDefenceOrder = true;
			}
		}
	}

	void ProduceUnit(Vector3 targetLocation){
	//Check if the game is paused
		if (gameIsPaused)
		{
			return;
		}

		unitSpawnCoolDownLeft -= Time.deltaTime;
		if (unitSpawnCoolDownLeft <= 0)
		{
			unitSpawnCoolDown = unitSpawnCoolDown;

			if (scoreManager.money < unitPrices.alliedMeleeCost)
			{
				//TODO: Make "Not Enough Money" message appear in-game.
				Debug.Log("Not enought money");
				return;
			}

			scoreManager.money -= unitPrices.alliedMeleeCost;

			Instantiate(playerUnits, targetLocation + new Vector3(-5,0,-5), Quaternion.Euler(0, 0, 0));

		}
	}


		}
	}


    void OnMouseDown()
    {
        //If the mouse is clicked on the object, go through the list of SequenceEditors.

        for (int i = 0; i < sequenceManager.editorlistGO.Count; i++)
        {
            //If a list has an id equal to the checkpoint name, draw that editor window in the UI.
            if (sequenceManager.editorlistGO[i].listID == baseName)
            {
                sequenceManager.editorlistGO[i].drawEditorWindow = true;
            }
            //Every list which id is different from the checkpoint name should not be drawn.
            else
            {
                sequenceManager.editorlistGO[i].drawEditorWindow = false;
            }
        }
    }

}
