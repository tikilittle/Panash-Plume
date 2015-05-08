using UnityEngine;
using System.Collections;

public class Hunter : MonoBehaviour {
	public GameObject huntressCam;
	public GameObject eagleCam;
	public GameObject eagleFlying;
	public GameObject eagleRest;

	public Camera eCamera;
	public GameObject huntressArm;
	public GameObject eagleObj;
	public GameObject huntressLeftArm;
	public CharacterMotor eagleMotor;
	public CharacterController eagleController;
	public FPSInputController eagleFPS;
	public MouseLook mLook;
	private Animator animate;
	public bool eagleMode = false;
	public Vector3 eagleRestingPosition;
	public GameObject huntressController;
	// Use this for initialization
	void Start () {
		animate = gameObject.GetComponent<Animator>();
		huntressCam.SetActive(true);
		eagleCam.SetActive(false);
		eagleRestingPosition = eagleObj.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
			checkPlayerInput();
			checkForPrey();
			switchCameras();

	}

	void checkPlayerInput() {
//		if (Input.GetAxis("Vertical") != 0) {
//			//if player is moving forward, animate walking
//			animate.SetInteger("AnimState",1);
//		} else {
//			animate.SetInteger("AnimState",0);
//		}

//		float xdir = Input.GetAxis("Horizontal");
//		if (xdir != 0) {
//			if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.D)) {
//				//rotate the camera right 45
//				transform.parent.Rotate(new Vector3(0f,45f,0f));
//			} else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.A)) {
//				//rotate the camera left 45
//				transform.parent.Rotate(new Vector3(0f,-45f,0f));
//					                 
//			}
//		} else {
//		}
//		if (Input.GetKeyDown(KeyCode.C)) {
//			for(int i = 0; i < eagleObj.transform.childCount; i++) {
//				Transform child = eagleObj.transform.GetChild(i);
//				string tag = child.gameObject.tag;
//				if (tag == "Prey") {
//					Debug.Log ("I got some food");
//				}
//			}
//		}


//		//if player jumps, animate jump
//		if (Input.GetAxis("Jump") != 0) {
//			//if player is moving forward, animate walking
//			animate.SetInteger("AnimState",2);
//		}
	
	}

	void switchCameras() {
		//if player presses E for Eagle, they will fly transfer Camera to Eagle
		if (Input.GetKeyDown(KeyCode.LeftShift)) {


		if (huntressCam.activeSelf) {
			//switch to Eagle Persepective
				eagleMode = true;
				if (huntressCam)
				huntressCam.SetActive(false);
				if (eagleCam)
				eagleCam.SetActive(true);
				if (eagleObj)
				eagleObj.transform.parent = null;
				if (eagleMotor)
				eagleMotor.enabled = true;
				if (eagleController)
				eagleController.enabled = true;
				mLook.enabled = true;
				if(eagleFPS)
				eagleFPS.enabled = true;
				eagleFlying.SetActive(true);
				eagleRest.SetActive(false);

			//eagleObj.GetComponent<MouseLook>().enabled = true;

			//huntressController.SetActive(false);
		} else {
			//switch to Huntress Persepective
				eagleMode = false;
//				eagleFlying.enabled = false;
//				eagleRest.enabled = false;

//				eagleObj.transform.parent = huntressArm.transform;
//
//				//	hunter.eagleObj.GetComponent<MouseLook>().enabled = false;
				StartCoroutine(zoomToPlayer());
//				eagleObj.transform.localPosition = eagleRestingPosition;

		}

	}
	}

	IEnumerator zoomToPlayer() {
//
//		while (eagleObj.transform.localPosition != eagleRestingPosition) {
//		eagleObj.transform.localPosition = Vector3.Slerp(eagleObj.transform.localPosition,eagleRestingPosition,5f * Time.deltaTime);
//			yield return new WaitForSeconds(0.1f);
//		}

	float steps = 10;
//		for (int i = 0; i < steps; i++) {
//			Vector3 armLocation = new Vector3(eagleRestingPosition.x*i/steps,eagleRestingPosition.y*i/steps,eagleRestingPosition.z*i/steps);
//			eagleObj.transform.localPosition = Vector3.Slerp(eagleObj.transform.localPosition,armLocation,steps * Time.deltaTime);
//			//eagleObj.transform.LookAt(eagleRestingPosition);
//			yield return new WaitForSeconds(0.05f);
//		}
//		eagleObj.transform.Translate(   (eagleObj.transform.position,huntressArm.transform.position,steps * Time.deltaTime);
		huntressCam.SetActive(true);
		eagleCam.SetActive(false);
		eagleMotor.enabled = false;
		eagleController.enabled = false;
		mLook.enabled = false;
		eagleObj.transform.rotation = Quaternion.Euler (new Vector3(0f,0f,0f));
		eagleFPS.enabled = false;
		eagleObj.transform.parent = huntressArm.transform;
		eagleObj.transform.localPosition = eagleRestingPosition;
		eagleObj.transform.localEulerAngles = Vector3.zero;

		eagleFlying.SetActive(false);
		eagleRest.SetActive(true);


//		checkForPrey();
		yield return 0;
	}

	void checkForPrey() {
		if (Input.GetKeyDown(KeyCode.C)) {
			for(int i = 0; i < transform.childCount; i++) {
				Transform child = transform.GetChild(i);
				string tag = child.gameObject.tag;
				if (tag == "Prey" || tag == "Sheep") {
					child.parent = null;
					child.gameObject.GetComponent<Rigidbody>().isKinematic = false;
					child.gameObject.GetComponent<SphereCollider>().enabled = false;
					child.gameObject.GetComponent<CapsuleCollider>().enabled = false;

					StartCoroutine(reenableChildObj(child,0.2f));
					Debug.Log ("I've got some prey");
				}
			}
		}
	}

	IEnumerator reenableChildObj(Transform prey,float delay) {
		yield return new WaitForSeconds(delay);
		prey.gameObject.GetComponent<CapsuleCollider>().enabled = true;
		prey.gameObject.GetComponent<SphereCollider>().enabled = true;
		prey.gameObject.GetComponent<Animal>().enabled = true;
	}

	
}
