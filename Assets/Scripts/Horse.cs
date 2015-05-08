using UnityEngine;
using System.Collections;

public class Horse : MonoBehaviour {
	public GameObject horseCamera;
	public GameObject panache;
	public CharacterMotor horseMotor;
	public CharacterController horseController;
	public FPSInputController horseFPS;
	public MouseLook mLook;
	public bool horseMode = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		checkPlayerInput();
	}


	void OnTriggerEnter(Collider collision) {
		string collider = collision.gameObject.name;
		Debug.Log (collider);
		if (collider == "Panache") {
			//Set Panache's transform to the horses saddle
			//disable control of Panache - by turning it off
			//turn on Horse Camera
			panache = collision.gameObject;
			collision.gameObject.SetActive(false);
			horseCamera.SetActive(true);
			horseMode = true;
			if (horseMotor)
				horseMotor.enabled = true;
			if (horseController)
				horseController.enabled = true;
			mLook.enabled = true;
			if(horseFPS)
				horseFPS.enabled = true;

			
		}
		
	}


	void checkPlayerInput() {
		if (Input.GetKeyDown(KeyCode.Space) && horseMode == true)  {
			//switch back to human player
			panache.SetActive(true);
			//get off on the left side of the horse
			panache.transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z - 1f);
			horseCamera.SetActive(false);
			horseMode = false;
			if (horseMotor)
				horseMotor.enabled = false;
			if (horseController)
				horseController.enabled = false;
			mLook.enabled = false;
			if(horseFPS)
				horseFPS.enabled = false;
			transform.position = new Vector3(transform.position.x,0f,transform.position.z);                                                                                             


		} else if (Input.GetAxis("Vertical") > 0 && horseMode == true){
			transform.position = new Vector3(transform.position.x,0f, transform.position.z);                                                                                             
			horseCamera.transform.localPosition = new Vector3(horseCamera.transform.localPosition.x,0.71f + (0.03f * (Mathf.Sin(Time.time*4f))), horseCamera.transform.localPosition.z);                                                                                             

		}


	}





}
