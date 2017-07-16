using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLauncher : MonoBehaviour {

	// TODO
	public GameObject groundEnemy;
	public GameObject airEnemy;
	public GameObject mine;
	public GameObject player;
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
		// The game is divided into stages, which are divided into screens, which essentially represent 50 unit blocks.
		// Each screen should be the size of one BG image, which are then looped to fill the stage.
		// Each stage may have differing properties.

		public const int GAME_LENGTH = 10; // Length in stages
		public const int STAGE_DEFAULT_LENGTH = 40; // Length in stages
		public int[] STAGE_LENGTHS = new int[GAME_LENGTH];
		private int GAME_LENGTH_SCREENS = 0;
		public int GetGameLengthInScreens() {
			return GAME_LENGTH_SCREENS;
		}

		public StageGeneratorConstants() {
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

	public void GenerateStage(){

		// Ingame screen size - UI exlusive - expected to be around 50x20 units
		// Initialize constants for stage generation.
		StageGeneratorConstants sg = new StageGeneratorConstants ();

		// Arrays for holding the objects, sorted by initial X coordinate.
		List<GameObject> floors = new List<GameObject>();
		List<GameObject> ramps = new List<GameObject>();
		List<GameObject> airEnemies = new List<GameObject>();
		List<GameObject> groundEnemies = new List<GameObject>();
		List<GameObject> mines = new List<GameObject>();
		List<GameObject> backgrounds = new List<GameObject>();
		List<GameObject> powerups = new List<GameObject>();

		//GameObject instantiatedObject = Instantiate(groundEnemy, new Vector3(1,1,1), Quaternion.identity);
		//instantiatedObject.transform.localScale = new Vector3 (5, 1, 1);



		int totalScreens = sg.GetGameLengthInScreens ();
		for (int i = 0; i < totalScreens; i++) {
			backgrounds.Add (Instantiate(background, new Vector3(80 * i - 80, 0, 20), Quaternion.identity));
			backgrounds[i].transform.localScale = normalizeToSize(backgrounds[i], 80, 40, 0);
		}
	}

}
