using UnityEngine;
using System.Collections;

public class Cape : MonoBehaviour {
//	public Transform collider;
	// Use this for initialization
	


	void FixedUpdate () {
		
		transform.Translate(Vector3.up * Input.GetAxis("Vertical") * Time.deltaTime * 100);
		transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * 100);
	}
}
