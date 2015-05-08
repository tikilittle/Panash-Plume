using UnityEngine;
using System.Collections;

public class Animal : MonoBehaviour { 
	public int maxHealth = 1;
	public float speed = 2f;
	public float distance = 10f;
	public float farawaydistance = 100f;
	private GameObject space;
	private GameObject mountains;
	private int health;
	private Vector3 player1; 
	private Vector3 player2; 
	private Vector3 player3; 

	private bool captured = false;

	private Animator animate;
	private Vector3 runawayLocation = Vector3.forward;
	public AudioClip animalsound;
	public AudioClip eagleCall;
	public AudioClip fireSound;

	public enum AnimalClass {
			Sheep,
			Fox,
			Wolf
	}

	public AnimalClass animal;

	// Use this for initialization
	void Start () {
		health = maxHealth;
		animate = GetComponent<Animator>();
		space = GameObject.Find ("Space");
		if (animal == AnimalClass.Wolf) {
		mountains = GameObject.Find ("Wolf Den");
		} else if (animal == AnimalClass.Fox) {
		mountains = GameObject.Find ("Fox Den");
		}

	}
	
	// Update is called once per frame
	void Update () {
		//MoveTowardsPlayer();

		player1 = findPlayer("Plume");
		player2 = findPlayer("Panache");
	//	player3 = findPlayer("horse");
		Debug.Log (space.transform.eulerAngles.x);
		keepAnimalAbove();
		if ((space.transform.eulerAngles.x < 180)) {
	
			daytimeActivity();
		} else {
			nighttimeActivity();
		}
	}

	void 		keepAnimalAbove() {
			transform.position = new Vector3(transform.position.x,0f,transform.position.z);
	}


	Vector3 findPlayer (string playerName) {
		GameObject player = GameObject.Find(playerName);
		Vector3 returnVector;
		if (player) {
			returnVector = player.transform.position;
		} else {
			returnVector = new Vector3(transform.position.x+farawaydistance, transform.position.y + farawaydistance, transform.position.z + farawaydistance);
		}
		return returnVector;

	}


	void OnTriggerEnter(Collider collision) {
		string collider = collision.gameObject.tag;
		Debug.Log (collider);
		if (collider == "Player") {
			bool animalCheck = checkForCapturedAnimals(collision.gameObject);
			if (animalCheck == false) {
				transform.parent = collision.gameObject.transform;
				transform.localPosition = new Vector3(0f,0,1f);
				gameObject.GetComponent<Rigidbody>().isKinematic = true;
				playEagleCall(collision.gameObject.transform.position);
				enabled = false;
			}

//			if (collision.gameObject.name == "Plume") {
//				collision.gameObject.GetComponent<CharacterMotor>().canControl = false;
//
//			}
			//unlock door
			//damage();
			//checkHealth();
			
			
		} else if (collider == "Fire") {
			//Add to Health
			AddPelt(animal);
			if (animal != AnimalClass.Sheep) {
				AddNewAnimal();
			}
			Destroy(gameObject);
			GameObject.Find ("Space").GetComponent<DayAndNight>().healthUI.value = GameObject.Find ("Space").GetComponent<DayAndNight>().healthUI.maxValue;
			playEagleCall(collision.gameObject.transform.position);
			playFireSound(collision.gameObject.transform.position);
		} else if ((animal == AnimalClass.Sheep) && (collider == "Prey") && (captured == false)) {
			playAnimalDistressSound();
			Destroy (gameObject);

		}
		
	}

	void AddNewAnimal () {
		GameObject[] predators = GameObject.FindGameObjectsWithTag("Prey");
		if ((predators.Length +1) < 7) {
			GameObject newPredator1 = Instantiate(gameObject,mountains.transform.position,Quaternion.identity) as GameObject;
			GameObject newPredator2 = Instantiate(gameObject,mountains.transform.position,Quaternion.identity) as GameObject;
			newPredator1.GetComponent<Animal>().enabled = true;
			newPredator2.GetComponent<Animal>().enabled = true;

		}

	}


	void AddPelt(AnimalClass a) {
		Debug.Log ("Just caught an " + a);
		if (a == AnimalClass.Fox) {
			int f =	PlayerPrefs.GetInt("Foxes");
			f++;
			PlayerPrefs.SetInt("Foxes",f);
		} else if(a == AnimalClass.Sheep) {
			int s =	PlayerPrefs.GetInt("Sheeps");
			s++;
			PlayerPrefs.SetInt("Sheeps",s);
		} else if (a == AnimalClass.Wolf) {
			int w =	PlayerPrefs.GetInt("Wolves");
			w++;
			PlayerPrefs.SetInt("Wolves",w);
		}

	}

	void MoveTowards(Vector3 player) {
		transform.LookAt(player);
		transform.position += transform.forward * speed * Time.deltaTime;
		playAnimalDistressSound();
	}

	void MoveAwayFrom(Vector3 player) {
		runawayLocation = new Vector3(((transform.position.x-player.x))+transform.position.x,transform.position.y,((transform.position.z-player.z))+transform.position.z);
		transform.LookAt(runawayLocation);
		transform.position += transform.forward * speed * Time.deltaTime;
		playAnimalDistressSound();
	}


	void CircleAround(Vector3 player) {
		transform.RotateAround(player, Vector3.up, 20 * Time.deltaTime);
		transform.eulerAngles = new Vector3(0f,transform.eulerAngles.y,0f);
		transform.LookAt (player);
//		runawayLocation = new Vector3(((transform.position.x-player.x))+transform.position.x,transform.position.y,((transform.position.z-player.z))+transform.position.z);
//		transform.LookAt(runawayLocation);
//		transform.position += transform.forward * speed * Time.deltaTime;
		playAnimalDistressSound();
	}




	void daytimeActivity() {
		float player1distance = Vector3.Distance(transform.position,player1);
		float player2distance = Vector3.Distance(transform.position,player2);
		float player3distance = Vector3.Distance(transform.position,player3);

		if (player1distance < distance) {
		//run from the Eagle
			MoveAwayFrom(player1);
		} else if (player2distance < distance) {
			//runaway from the Human if you're a Fox or Wolf
			if (animal == AnimalClass.Fox) {
				MoveAwayFrom(player2);
			 } else if (animal == AnimalClass.Wolf) {
				MoveAwayFrom(player2);
			} else if (animal == AnimalClass.Sheep) {
				// if sheep, follow Human
				MoveTowards(player2);
			}
//		} else if (player3distance < distance) {
//			if (animal == AnimalClass.Fox || animal == AnimalClass.Wolf) {
//				//runaway from the Horse if you're a Fox or Wolf
//				MoveAwayFrom(player3);
//			} else if (animal == AnimalClass.Sheep) {
//				// if sheep, follow Human
//				MoveTowards(player3);
//			}
		
		} else {
			if (animal == AnimalClass.Fox) {
				GameObject[] sheepies = GameObject.FindGameObjectsWithTag("Sheep");
				if (sheepies.Length > 0) {
					Vector3 nearbySheep = checkForNearest("Sheep");
					MoveTowards(nearbySheep);
					
				}
			 } else if (animal == AnimalClass.Wolf) {
				//Go to the mountains
				MoveTowards(mountains.transform.position);

			} else if (animal == AnimalClass.Sheep) {
				//do nothing
				transform.eulerAngles = new Vector3(0f,transform.eulerAngles.y,0f);

			}

		}
//		animate.SetFloat ("speed",1.1f);
	}


	void nighttimeActivity() {
		float player1distance = Vector3.Distance(transform.position,player1);
		float player2distance = Vector3.Distance(transform.position,player2);
		float player3distance = Vector3.Distance(transform.position,player3);
		
		if (player1distance < distance) {
			//run from the Eagle
			MoveAwayFrom(player1);
		} else if (player2distance < distance) {
			//runaway from the Human if you're a Fox or Wolf
			if (animal == AnimalClass.Fox) {
				MoveAwayFrom(player2);
			} else if (animal == AnimalClass.Wolf) {
				CircleAround(player2);
			} else if (animal == AnimalClass.Sheep) {
				// if sheep, follow Human
				MoveTowards(player2);
			}
//		} else if (player3distance < distance) {
//			if (animal == AnimalClass.Fox || animal == AnimalClass.Wolf) {
//				//runaway from the Horse if you're a Fox or Wolf
//				CircleAround(player3);
//			} else if (animal == AnimalClass.Sheep) {
//				// if sheep, follow Human
//				MoveTowards(player3);
//			}
		} else {
			if (animal == AnimalClass.Fox) {
				MoveTowards(mountains.transform.position);
			 } else if (animal == AnimalClass.Wolf) {
				//Look For Closest Sheep
				GameObject[] sheepies = GameObject.FindGameObjectsWithTag("Sheep");
				if (sheepies.Length > 0) {
					Vector3 nearbySheep = checkForNearest("Sheep");
					MoveTowards(nearbySheep);
					
				}
				//If captured Sheep, try to get to the hills
			} else if (animal == AnimalClass.Sheep) {
				//if captured do nothing
				if (captured == false) {
					Vector3 nearybyPredator = checkForNearest("Prey");
					if (Vector3.Distance(nearybyPredator,transform.position) < distance) {
						MoveAwayFrom(nearybyPredator);
					} else {
						transform.eulerAngles = new Vector3(0f,transform.eulerAngles.y,0f);
					}
				}
			}
			
		}
		//		animate.SetFloat ("speed",1.1f);
	}

	public void playAnimalDistressSound(){
		float randomnumber = Random.Range (0,1000);
		if (randomnumber == 9) {
			AudioSource.PlayClipAtPoint(animalsound,transform.position);
		}

	}

	void playFireSound(Vector3 eagle){
		
		AudioSource.PlayClipAtPoint(fireSound,eagle);
		
	}


	void playEagleCall(Vector3 eagle){

		AudioSource.PlayClipAtPoint(eagleCall,eagle);
		
	}

	Vector3 checkForNearest (string _animal) { 
	GameObject[] prey = GameObject.FindGameObjectsWithTag(_animal);
	GameObject closestAnimal = null;
	Vector3 closeVector;
		for(int i = 0; i < prey.Length; i++) {
			GameObject _prey = prey[i];
//			AnimalClass animalTag = _prey.GetComponent<Animal>().animal;
//			if (animalTag == AnimalClass.Sheep) {
				float howfaraway = Vector3.Distance(gameObject.transform.position,_prey.transform.position);
				if (i == 0) {
					closestAnimal = _prey;
					closeVector = closestAnimal.transform.position;
				} else if (howfaraway < Vector3.Distance(gameObject.transform.position,closestAnimal.transform.position)) {
					closestAnimal = _prey;
					closeVector = closestAnimal.transform.position;
					if (i == prey.Length-1) {
						return closeVector;
					}
				} else {
					if (i == prey.Length-1) {
						closeVector = closestAnimal.transform.position;
						return closeVector;
					}

				}
		}
		closeVector = closestAnimal.transform.position;
		return closeVector;

	}





	bool checkForCapturedAnimals (GameObject hunter) { 

		for(int i = 0; i < hunter.transform.childCount; i++) {
			Transform child = hunter.transform.GetChild(i);
			string tag = child.gameObject.tag;
			if (tag == "Prey" || tag == "Sheep") {
				return true;
			}
		}
		return false;
	}



	void damage() {
		health--;

	}

	void checkHealth() {
		if (health < 0) {
			Destroy (gameObject);
		}
		
	}

}
