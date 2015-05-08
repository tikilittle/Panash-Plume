using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class DayAndNight : MonoBehaviour {
	public AudioSource music;
	public Transform space;
	public float speed;
	public Material sun;
	public Material moon;
	public MeshRenderer cloudSystem;
	public Slider healthUI;
	public Text sheepCounter;
	public Sprite sheep_alive;
	public Sprite sheep_dead;
	// Use this for initialization
	void Start () {
		StartCoroutine(checkHealth());
		StartCoroutine(checkDays());

		music = gameObject.GetComponent<AudioSource>();
		music.time = 91.5f;
		Time.timeScale = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 1.0f) {
			checkRotation();
			Rotate();
			checkHealth();
			checkSheep();
		}
		updateMusicTime();

	}

	void checkRotation() {
		if ((space.rotation.x > 0) && (space.rotation.x <= 270)) {
			//cloudSystem.material = moon;
		} else if (space.rotation.x < 360) {
			//cloudSystem.material = sun;
		}

//		if (space.rotation.x > 0 && space.rotation.x < 3f) {
//			
//		}
	}

	void updateMusicTime() {
			float musicTime = music.time;
			PlayerPrefs.SetFloat("music_time",musicTime);
	}

	void Rotate() {
		space.Rotate(new Vector3(speed,0f,0f));
		
	}

	void checkSheep() {
		GameObject[] sheepies = GameObject.FindGameObjectsWithTag("Sheep");
		//sheepCounter.text = "" + sheepies.Length;
		for (int i = 1; i <= 5; i++) {
			if (i <= sheepies.Length) {
				GameObject.Find("SHP" + i).GetComponent<Image>().sprite = sheep_alive;

			} else {
				GameObject.Find("SHP" + i).GetComponent<Image>().sprite = sheep_dead;

			}

		}
		if (sheepies.Length == 0) {
			Application.LoadLevel("EndScreen");

		}
	}

	IEnumerator checkHealth() {
		while (true) {
			yield return new WaitForSeconds(3f);
			healthUI.value = healthUI.value -= .01f;
			if (healthUI.value <= healthUI.minValue) {
			//Game Over
			Application.LoadLevel("EndScreen");
			}
		}
	}

	IEnumerator checkDays() {
		while (true) {
			yield return new WaitForSeconds(60f);
			int days = PlayerPrefs.GetInt("Days");	
						days++;
						PlayerPrefs.SetInt ("Days",days);
			Debug.Log (days);
		}

	}
}
