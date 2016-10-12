using UnityEngine;
using System.Collections;

public class ThreeDRTSCamera : MonoBehaviour {

	public GameObject TopRight, BotLeft;

	public float scrollZone = 10.0f;
	public float scrollSpeed = 3.0f;

	//camera zoom variables
	public float cameraSpeed = 2.0f;
	public float cameraZoomSpeed = 15.0f;
	public float cameraZoomIn = 1.0f;
	public float cameraZoomOut = 2.0f;

	//camera orbit variables
	public float xMax;
	public float xMin;
	public float yMax = 40.0f;
	public float yMin = 5.0f;
	public float zMax;
	public float zMin;

	//camera rotation variables
	public float panSpeed = 0.2f;
	public float panAngleMin = 20;
	public float panAngleMax = 85;

	private Vector3 desiredPosition;

    Camera camera;

	// Use this for initialization
	void Start () {
        camera = GetComponent<Camera>();
	}


	// Update is called once per frame
	void Update () {
		//Camera movement

		float x = 0, y = 0, z = 0;
		float speed = scrollZone * 3 *  Time.deltaTime;
		//WASD movement
		if (Input.GetKey(KeyCode.W)){
			z += speed;
		}
		if (Input.GetKey(KeyCode.A)){
			x -= speed;
		}
		if (Input.GetKey(KeyCode.S)){
			z -= speed;
		}
		if (Input.GetKey(KeyCode.D)){
			x += speed;
		}

		//mouse movement
		if (Input.mousePosition.x < scrollZone)
			x -= speed;
		else if (Input.mousePosition.x > Screen.width - scrollZone)
			x += speed;

		if (Input.mousePosition.y < scrollZone)
			z -= speed;
		else if (Input.mousePosition.y > Screen.height - scrollZone)
			z += speed;

		//zoom camera
		y -= 40 * Input.GetAxis ("Mouse ScrollWheel");

		Vector3 move = new Vector3(x, y, z) + desiredPosition;
		move.x = Mathf.Clamp (move.x, xMin, xMax);
		move.y = Mathf.Clamp (move.y, 3, yMax);
		move.z = Mathf.Clamp (move.z, zMin, zMax);
		desiredPosition = move;
		transform.position = Vector3.Lerp(transform.position, desiredPosition, 0.2f);

		//panning when zooming not working
		var pan = camera.transform.eulerAngles.x - (y/3) * -1;
		pan = Mathf.Clamp (pan, panAngleMin, panAngleMax);
		if (y < 0 || camera.transform.position.y > (yMax / 3)){
			camera.transform.eulerAngles = new Vector3(pan,0,0);
		}


	}
}