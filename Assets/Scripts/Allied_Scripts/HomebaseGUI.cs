using UnityEngine;
using System.Collections;

public class HomebaseGUI : MonoBehaviour {

	//unit prduction management vars
	public float unitSpawnCoolDown = 5f;
	float unitSpawnCoolDownLeft = 0f;

	//unit which is spawned
	public GameObject playerUnits;

	//cache scripts for the unit price
	GameObject unitManager;
	UnitPrices unitPrices;

	//cache scripts for ScoreManager to keep trak of money spend and money left
	GameObject scoreHolder;
	ScoreManager scoreManager;

	//if unit is within controlRange it can recive the commands, controlRange is set here
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
		//Find and cache unit manager script
		unitManager = GameObject.Find("UnitManager");
		unitPrices = unitManager.GetComponent<UnitPrices>();

		//Find and cache the score manager
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


		//bool to pause and unpause the script
        gameIsPaused = true;
    }
	
	// Update is called once per frame
	void Update () {
		//array containing each playerUnits which can be affected by the script
		GameObject[] units = GameObject.FindGameObjectsWithTag("playerUnits");

		//if the bool is set to true skip the update and check bool again
        if (gameIsPaused)
        {
            return;
        }

		//check if the slot is to the left in the editor
		for (int i = 0; i < listComponent.slots.Count; i++) {
			//check to see if the slot is empty
			if (i % 2 == 0 && listComponent.slots[i].commandName != "") {
				//crate a sring to keep track of the slots commandId
				string id = listComponent.slots [i].commandId;
				//cache the target
				Vector3 target;

				//determine the corrent ation based in the commandId
				switch (id) {
				case "A01":
					//if attack command is initiated set target to the slot var to the left of the attack command
					target = listComponent.slots [i + 1].locationOfTarget;
					// run the attack command
					Attack (units, target);
					break;

				case "M01":
					target = listComponent.slots [i + 1].locationOfTarget;
					//run the move command
					Move (units, target);
					break;

				case "D01":
					target = listComponent.slots [i + 1].locationOfTarget;
					//run the defend command
					Defend (units, target);
					break;

				case "P01":
					//if this is run set the target to the gameObject and run produce unit
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
		//if the game is paused skip this command
		if (gameIsPaused)
		{
			return;
		}
		//count down to the spawner, this is to make sure all the player units arent initiate too quickly
		unitSpawnCoolDownLeft -= Time.deltaTime;
		//when the count down reaches 0 proceed to produce a unit
		if (unitSpawnCoolDownLeft <= 0)
		{
			//reset the Cooldown
			unitSpawnCoolDown = unitSpawnCoolDown;

			//if you do ot have the money to buy/produce a unit give a feedback
			if (scoreManager.money < unitPrices.alliedMeleeCost)
			{
				//TODO: Make "Not Enough Money" message appear in-game.
				Debug.Log("Not enought money");
				return;
			}

			//remove the unit prie from the score manager to make sure you cannot spawn infinite units
			scoreManager.money -= unitPrices.alliedMeleeCost;

			//initiate unit at the targeted location - 5 in x and - 5 in z, as long as the base is placed in (+,+) coordinates, the units will spawn in the mape.
			Instantiate(playerUnits, targetLocation + new Vector3(-5,0,-5), Quaternion.Euler(0, 0, 0));

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
