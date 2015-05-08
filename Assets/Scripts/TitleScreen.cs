using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {
	public AudioClip music;
	private AudioSource mPlayer;
	private float musicTime;
	// Use this for initialization

	void Start () {
		//AudioSource.PlayClipAtPoint(music,transform.position);
		mPlayer = gameObject.GetComponent<AudioSource>();
		musicTime = PlayerPrefs.GetFloat("music_time");
		mPlayer.time = musicTime;
		mPlayer.Play();

	}
	
	// Update is called once per frame
	void Update () {
		updateMusicTime();
	}

	public void loadScene(string scene) {
//		AsyncOperation async = Application.LoadLevelAsync("MainGame");
//		yield return async;
//		Debug.Log("Loading complete");
		//Application.LoadLevel ("MainGame");
		Application.LoadLevel(scene);
	}

	void updateMusicTime() {
		musicTime = mPlayer.time;
		PlayerPrefs.SetFloat("music_time",musicTime);

	}



	public void StartGame() {
		//StartCoroutine("loadLevel");
		PlayerPrefs.SetInt("Foxes",0);
		PlayerPrefs.SetInt("Sheeps",0);
		PlayerPrefs.SetInt("Wolves",0);	
		PlayerPrefs.SetInt("Days",0);	
		Application.LoadLevel("MainGame");
	}
}
