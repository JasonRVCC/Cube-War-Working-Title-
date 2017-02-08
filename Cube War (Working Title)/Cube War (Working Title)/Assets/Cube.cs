using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Just a basic bit of code to launch a cube
 * (pulled some code from Mission Demolition)
 */

//Enum for the current play state
public enum PlayState{
	Idle, //The player is idle
	Aiming, //The player has selected a cude and is currently aiming
	Launch}; //The player has launched the cube, and the cube is in motion

public class Cube : MonoBehaviour {

	private PlayState playState; //The current play state
	private Vector3 velocity; //The cube's velocity

	private Vector3 mousePos2D;
	private Vector3 mousePos3D;
	private Vector3 cubePos;
	private float maxMagnitude;

	public float velocityMulti = 4f; //Multiplyer for velocity


	// Use this for initialization
	void Start () {
		playState = PlayState.Idle; 
		//cubePos = Camera.main.ScreenToWorldPoint (this.gameObject.GetComponent<Collider>().bounds.center);
		//cubePos = this.gameObject.GetComponent<Collider>().bounds.center;
		mousePos2D = Input.mousePosition;
		mousePos3D = Vector3.zero;
		//maxMagnitude = this.gameObject.GetComponent<Collider> ().bounds.size.x * 3f;
		velocity = Vector3.zero; //The player only controls X and Z velocity
	}
	
	// Update is called once per frame
	void Update () {
		switch (playState) {
		case PlayState.Idle:
			break;
		case PlayState.Aiming:
			UpdateLaunchVelocity ();
			if (Input.GetMouseButtonUp (0)) {
				this.gameObject.GetComponent<Rigidbody> ().velocity = -velocity * velocityMulti;
				playState = PlayState.Launch;
			}
			break;
		case PlayState.Launch:
			print (velocity);
			if (this.gameObject.GetComponent<Rigidbody> ().velocity == Vector3.zero) {
				cubePos = this.gameObject.GetComponent<Collider>().bounds.center;
				playState = PlayState.Idle;
			}
			break;
		}
	}

	void OnMouseDown(){
		if (playState == PlayState.Idle) {
			//TEMP
			//this.gameObject.GetComponent<Rigidbody> ().velocity = new Vector3(30f,4f,30f);
			playState = PlayState.Aiming;
		}
	}

	private void UpdateLaunchVelocity(){
		mousePos2D = Input.mousePosition;
		mousePos2D.z = Camera.main.transform.position.z - cubePos.z;
		mousePos3D = Camera.main.ScreenToWorldPoint (mousePos2D);
		velocity = mousePos3D - cubePos;
		//since the camera has been rotated, we need to change the vector values
		/*float z = velocity.z;
		velocity.z = velocity.y;
		velocity.y = z;*/
	}
}
