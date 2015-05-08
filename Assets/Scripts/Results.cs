using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Results : MonoBehaviour {
	public Text days;
	public Text food;
	public Text money;
	public int sheepScore = 1;
	public int foxScore = 2;
	public int wolfScore = 10;
	public int dayScore = 2800;
	private int foxes = 0;
	private int wolves = 0;
	private int sheep = 0;
	private int daysLived = 0;

	// Use this for initialization
	void Start () {
		int total = addupscore();
		string survived = "Days survived: " + PlayerPrefs.GetInt("Days");
		days.text = survived;
		string eating = "Cooked " + "\t" + PlayerPrefs.GetInt("Sheeps") + "\t" + "sheep" + "\n" + "Baked " + "\t" + PlayerPrefs.GetInt("Foxes") + "\t" + "foxes" + "\n" + "Roasted " + "\t" + PlayerPrefs.GetInt("Wolves") + "\t" + "wolves";
		food.text = eating;
		char tugrik = '\u20AE';
		string tug = tugrik.ToString();
		money.text = total + " " + tug;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	int addupscore() {
		int score = 0;
		Debug.Log (PlayerPrefs.GetInt("Foxes"));
		int days_survived = PlayerPrefs.GetInt("Days");
		int sheep = PlayerPrefs.GetInt("Sheeps") * sheepScore;
		int fox = PlayerPrefs.GetInt("Foxes") * foxScore;
		int wolves = PlayerPrefs.GetInt("Wolves") * wolfScore;


		score = sheep + fox + wolves;
		Debug.Log (score);
		score = (score * dayScore) + (days_survived * dayScore);
		Debug.Log (score);
		return score;
	}

	public void menu () {
		Application.LoadLevel("TitleScreen");

	}
}
