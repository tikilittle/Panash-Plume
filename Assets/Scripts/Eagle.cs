using UnityEngine;
using System.Collections;

public class Eagle : MonoBehaviour {
	public Hunter hunter;
	private Animator animate;
	// Use this for initialization
	void Start () {
		animate = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		checkForPrey();

		if (hunter.eagleMode == true) {
//			switchCameras();
		} else {
			//checkToPreen();
		}
	}




	void checkToPreen() {
		//if on ground then random chance to preen
		int rand = Random.Range (0,10);
		if (rand > 8) {
			animate.SetInteger("AnimState",1);
		} else {
			animate.SetInteger("AnimState",0);
		}

	}

	void switchCameras() {
		//if player presses E for Eagle, they will fly transfer Camera to Eagle
		if (Input.GetKeyDown(KeyCode.E) & hunter.eagleMode == true) {
			hunter.eagleMode = false;
				//switch to Huntress Persepective
			StartCoroutine(activateHunter());

		}
	}



	public float pushPower = 2.0F;
	void OnControllerColliderHit(ControllerColliderHit hit) {
		Rigidbody body = hit.collider.attachedRigidbody;
		if (body == null || body.isKinematic)
			return;
		
		if (hit.moveDirection.y < -0.3F)
			return;
		
		Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
		body.velocity = pushDir * pushPower;
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
					gameObject.GetComponent<CharacterMotor>().canControl = true;
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

	void OnTriggerEnter(Collider collision) {
		string collider = collision.gameObject.tag;
		Debug.Log (collider);
		if (collider == "Player" && hunter.eagleMode == true) {
			transform.parent = hunter.huntressArm.transform;
			transform.localPosition = new Vector3(0.18f,0.393f,-0.046f);

		}
		
	}

	
	




	IEnumerator activateHunter() {
		yield return new WaitForSeconds(0.3f);
		hunter.huntressCam.SetActive(true);
		hunter.eagleCam.SetActive(false);
		hunter.eagleObj.transform.parent = hunter.huntressArm.transform;
		hunter.eagleMotor.enabled = false;
		hunter.eagleController.enabled = false;
		//.enabled = true;
		hunter.eagleFPS.enabled = false;
		//	hunter.eagleObj.GetComponent<MouseLook>().enabled = false;
		gameObject.transform.localPosition = hunter.eagleRestingPosition;
		//enabled = false;

	}

}
