using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLauncher : MonoBehaviour {

	// TODO
	public GameObject groundEnemy;
	public GameObject airEnemy;
	public GameObject mine;
	public GameObject playerPrefab;
	public GameObject floor;
	public GameObject ramp;
	public GameObject background;
	public GameObject powerup;

	// boots up the stuff
	void Start () {
		// TODO
		GenerateStage();
	}


	protected internal class StageGeneratorConstants {

		public int GAME_LENGTH; // Length in stages
		public int STAGE_DEFAULT_LENGTH; // Length in stages
		public int[] STAGE_LENGTHS;
		private int GAME_LENGTH_SCREENS;
		public int GetGameLengthInScreens() {
			return GAME_LENGTH_SCREENS;
		}

		public StageGeneratorConstants() {
			GAME_LENGTH = 10;
			STAGE_DEFAULT_LENGTH = 40;
			GAME_LENGTH_SCREENS = 0;
			STAGE_LENGTHS = new int[GAME_LENGTH];
			for (int i = 0; i < STAGE_LENGTHS.Length; i++) {
				STAGE_LENGTHS[i] = STAGE_DEFAULT_LENGTH;		// It is expected below that each stage is equal in length.
				GAME_LENGTH_SCREENS += STAGE_DEFAULT_LENGTH;
			}



		}
	}

	private Vector3 normalizeToSize(GameObject gameObject, float x, float y, float z) {
		return new Vector3 (
			x * gameObject.transform.localScale.x / gameObject.GetComponent<Renderer>().bounds.size.x, 
			y * gameObject.transform.localScale.y / gameObject.GetComponent<Renderer>().bounds.size.y, 
			z * gameObject.transform.localScale.z / gameObject.GetComponent<Renderer>().bounds.size.z);
	}

	/**
	 * GenerateStage - a random stage generator.
	 * 
	 * Starts with an initial fixed stage part, and creates floorings after that based on jump difficulty, which increases over time.
	 * 
	 * The game is divided into stages, which are divided into screens, which essentially represent 50 unit blocks.
	 * Each screen should be the size of one BG image, which are then looped to fill the stage.
	 * Each stage may have differing properties.
	 * 
	 * Viewport size assumed to be around 50-70 units wide and 30 units tall. y = 0 is the middle part of stage height, and the game
	 * starts from x = 0. The y value can be negative, but player should be killed at around y = -20. Camera should never reveal area
	 * below or above the background, which has y values in the range [-25, 25].
	 * 
	 * 
	 * 
	 */
	public void GenerateStage(){
		
		// Initialize constants for stage generation.
		StageGeneratorConstants sg = new StageGeneratorConstants ();

		// Arrays for holding the objects, sorted by initial X coordinate.
		GameObject player;
		List<GameObject> floors = new List<GameObject>();
		List<GameObject> ramps = new List<GameObject>();
		List<GameObject> airEnemies = new List<GameObject>();
		List<GameObject> groundEnemies = new List<GameObject>();
		List<GameObject> mines = new List<GameObject>();
		List<GameObject> backgrounds = new List<GameObject>();
		List<GameObject> powerups = new List<GameObject>();

		//GameObject instantiatedObject = Instantiate(groundEnemy, new Vector3(1,1,1), Quaternion.identity);
		//instantiatedObject.transform.localScale = new Vector3 (5, 1, 1);

		
		// Generate backgrounds
		int totalScreens = sg.GetGameLengthInScreens ();
		for (int i = 0; i < totalScreens; i++) {
			backgrounds.Add (Instantiate(background, new Vector3(100 * i - 100, 0, 20), Quaternion.identity));
			if (i > sg.STAGE_DEFAULT_LENGTH) {
				backgrounds [i].GetComponent<Renderer> ().material.color = Color.HSVToRGB (
					(i / sg.STAGE_DEFAULT_LENGTH) * 1.0f / sg.GAME_LENGTH,
					0.4f,
					1.0f);
			}
			backgrounds[i].transform.localScale = normalizeToSize(backgrounds[i], 100, 50, 0);
		}

		// Initial start area
		

		// Generate floors
		int x = 0;

	}

}
