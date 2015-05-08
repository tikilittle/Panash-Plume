using UnityEngine;
using System.Collections;

public class GameMenu : MonoBehaviour {
	public GameObject menu;
	public GameObject sheepIcon;
	public GameObject healthSlider;
	// Use this for initialization
	void Start () {
		turnCursor(false);

	}
	
	// Update is called once per frame
	void Update () {
		checkPlayerInput();
	}

	void checkPlayerInput() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (menu.activeSelf == true) {
				turnMenuActive(false);
				turnTime(true);
				turnUI(true);
			} else {
				turnMenuActive(true);
				turnTime(false);
				turnCursor(true);

			}

		}
	}

	public void turnCursor(bool active) {
		if (active) {
			Cursor.visible = true;
		} else {
			Cursor.visible = false;
		}

	}



	public void turnMenuActive(bool option) {
			menu.SetActive(option);
	}

	public void turnTime(bool active) {
		if (active) {
			Time.timeScale = 1f;
		} else {
			Time.timeScale = 0f;
		}
	}

	public void turnUI(bool active) {
		if (active) {
			sheepIcon.SetActive(true);
			healthSlider.SetActive(true);
			turnCursor(false);

		} else {
			sheepIcon.SetActive(false);
			healthSlider.SetActive(false);

		}
	}

	public  void endLevel() {
		Application.LoadLevel("EndScreen");
		Cursor.visible = true;

	}

	public  void endGame() {
		Application.Quit();
	}


}
