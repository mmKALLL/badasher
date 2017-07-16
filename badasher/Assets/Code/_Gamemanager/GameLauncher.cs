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

	void Awake (){
		GenerateStage ();
	}


	protected internal class StageGeneratorConstants {

		public int GAME_LENGTH; // Length in stages
		public int STAGE_DEFAULT_LENGTH; // Length in stages
		public int[] STAGE_LENGTHS;
		private int GAME_LENGTH_IN_SCREENS;
		public int GetGameLengthInScreens() {
			return GAME_LENGTH_IN_SCREENS;
		}
		public float DEATH_ZONE_HEIGHT_FROM_BOTTOM;
		public float FLOOR_BASE_LENGTH;

		public StageGeneratorConstants() {
			GAME_LENGTH = 10;
			STAGE_DEFAULT_LENGTH = 20;
			GAME_LENGTH_IN_SCREENS = 0;
			STAGE_LENGTHS = new int[GAME_LENGTH];
			for (int i = 0; i < STAGE_LENGTHS.Length; i++) {
				STAGE_LENGTHS[i] = STAGE_DEFAULT_LENGTH;		// It is expected in GenerateStage that each stage is equal in length.
				GAME_LENGTH_IN_SCREENS += STAGE_DEFAULT_LENGTH;
			}

			DEATH_ZONE_HEIGHT_FROM_BOTTOM = 7; // Unused apart from generation, for now.
			FLOOR_BASE_LENGTH = 10; // Minimum at least twice as much as this.
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
	 * Viewport size assumed to be around 23-30 units wide and 15 units tall. y = 0 is the middle part of stage height, and the game
	 * starts from x,y = (0,0). The y value can be negative, but player should be killed at around y = -17. Camera should never reveal area
	 * below or above the background, which has y values in the range [-25, 25].
	 * 
	 * The player height is fixed to 2 units, so one player width is around 1.4 units.
	 * 
	 * Depth was used; player is at z = 0, pickups at z = 1, enemies at z = 2, ramps and floor at z = 3, background at z = 20.
	 * Possible particles should be placed in the foreground, at z = -1 for minor particles, z = -2 for major particles and
	 * z = -3 for foreground supporting particles.
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
		int i;
		for (i = 0; i < totalScreens; i++) {
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
		player = Instantiate(playerPrefab, new Vector3(0,0,0), Quaternion.identity);
		player.transform.localScale = normalizeToSize(player, 2 * 400 / 572, 2, 0);

		floors.Add(Instantiate(floor, new Vector3(-10,-1,3), Quaternion.identity));
		floors[0].transform.localScale = normalizeToSize(floor, 120.0f, 0.6f, 0.0f);

		// Generate floors and stuff on them
		float x = 120.0f;
		float y = 0.0f;
		float diff;//iculty
		i = 0;

		while (x < sg.GetGameLengthInScreens() * 80 - 200) {
			// Raise difficulty based on a reverse exponential function. Speed expected to increase (very) gradually as well.
			// https://i.gyazo.com/956ebcd135567279bb4a00d01e312ca1.png
			diff = (Random.value * 0.5f + 0.75f) * 
				(2 + (Mathf.Pow(x, 0.7f) * 0.028f));
			float angle = (Random.value - 0.5f) * (24 + diff * 2) - 1.8f;

			// Check if floor would be too high or low; reverse direction if so
			if (y > 20 && angle >= 0)
				angle = -angle;
			else if (y < -(20-sg.DEATH_ZONE_HEIGHT_FROM_BOTTOM) && angle < 0)
				angle = -angle;
			x += Mathf.Cos (angle / 180 * Mathf.PI) * diff;
			y += Mathf.Sin (angle / 180 * Mathf.PI) * diff;

			floors.Add(Instantiate(floor, new Vector3(x, y, 3), Quaternion.identity));
			float floorLen = (Random.value + 0.4f) * (diff * 0.1f) * sg.FLOOR_BASE_LENGTH;
			floors[i].transform.localScale = normalizeToSize(floor, floorLen, 0.6f, 0.0f);

			// Place objects and enemies.


			// Finally some cleanup; shift indexes and set x to match the end of the platform.
			i++;
		}


	}

}
