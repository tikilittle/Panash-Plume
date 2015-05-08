using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroText : MonoBehaviour {

	public Text t;
	public float delay;
	// Use this for initialization
	void Start () {
		StartCoroutine(DisplayText());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator DisplayText() {
		yield return new WaitForSeconds(delay);
		t.enabled = false;

	}
}
