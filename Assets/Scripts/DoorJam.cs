using UnityEngine;
using System.Collections;

public class DoorJam : MonoBehaviour {

	public Rigidbody door;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collision) {
		string collider = collision.gameObject.tag;
		if (collider == "Player") {
			//unlock door
			door.isKinematic = false;


		}
		
	}

}
