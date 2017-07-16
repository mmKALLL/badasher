using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLauncher : MonoBehaviour {

	// TODO - something missing?
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
		public int STAGE_DEFAULT_LENGTH; // length in screens
		public int SCREEN_DEFAULT_LENGTH;
		public int[] STAGE_LENGTHS;
		private int GAME_LENGTH_IN_SCREENS;
		public int GetGameLengthInScreens() {
			return GAME_LENGTH_IN_SCREENS;
		}
		public float DEATH_ZONE_HEIGHT_FROM_BOTTOM;

		public float TUTORIAL_FLOOR_LENGTH;
		public float FLOOR_BASE_LENGTH;
		public float AIR_ENEMY_SPAWN_CHANCE;
		public float MINE_SPAWN_CHANCE;
		public float AIR_POWERUP_SPAWN_CHANCE;
		public float GROUND_OBJECT_SPAWN_DISTANCE;

		public float PLAYER_SIZE;
		public float POWERUP_SIZE;
		public float GROUND_ENEMY_SIZE;
		public float AIR_ENEMY_SIZE;
		public float MINE_SIZE;
		public float RAMP_SIZE;

		public StageGeneratorConstants() {
			GAME_LENGTH = 10;
			STAGE_DEFAULT_LENGTH = 10;
			SCREEN_DEFAULT_LENGTH = 100;
			GAME_LENGTH_IN_SCREENS = 0;
			STAGE_LENGTHS = new int[GAME_LENGTH];
			for (int i = 0; i < STAGE_LENGTHS.Length; i++) {
				STAGE_LENGTHS[i] = STAGE_DEFAULT_LENGTH;		// It is expected in GenerateStage that each stage is equal in length.
				GAME_LENGTH_IN_SCREENS += STAGE_DEFAULT_LENGTH;
			}

			DEATH_ZONE_HEIGHT_FROM_BOTTOM = 7; // Unused apart from generation, for now.
			TUTORIAL_FLOOR_LENGTH = 60;
			FLOOR_BASE_LENGTH = 10; // Average length, some variation included.

			AIR_ENEMY_SPAWN_CHANCE = 0.12f;
			MINE_SPAWN_CHANCE = 0.6f;
			AIR_POWERUP_SPAWN_CHANCE = 0.22f;
			GROUND_OBJECT_SPAWN_DISTANCE = 2.7f; // The smaller this is, the more objects (and challenge) you get. TODO: Could be scaled by difficulty?
		
			PLAYER_SIZE = 2.0f;
			POWERUP_SIZE = 1.4f;
			GROUND_ENEMY_SIZE = 2.25f;
			AIR_ENEMY_SIZE = 1.7f;
			MINE_SIZE = 1.8f;
			RAMP_SIZE = 1.1f;
		}
	}


	// Helper functions for stage generation.
	private Vector3 normalizeToSize(GameObject gameObject, float x, float y, float z) {
		return new Vector3 (
			x * gameObject.transform.localScale.x / gameObject.GetComponent<Renderer>().bounds.size.x, 
			y * gameObject.transform.localScale.y / gameObject.GetComponent<Renderer>().bounds.size.y, 
			z * gameObject.transform.localScale.z / gameObject.GetComponent<Renderer>().bounds.size.z);
	}

	private Vector3 ScaleToUnit(float desiredUnitHeight, Sprite sprite){
		float heightScale = desiredUnitHeight / (sprite.rect.height / sprite.pixelsPerUnit);
		return new Vector3 (heightScale, heightScale, 1);
	}

	private Vector3 FloorScale(SpriteRenderer renderer, float desiredUnitLength){
		/*float lengthScale = desiredUnitLength / (8.5f);
		//Debug.Log (renderer.bounds.size.x);
		return new Vector3 (lengthScale, 0.105f, 1f);
		//(sprite.rect.width / sprite.pixelsPerUnit)*/

		return normalizeToSize (floor, desiredUnitLength, 0.6f, 0.0f);
	}

	/**
	 * GenerateStage - a random stage generator.
	 * 
	 * Starts with an initial fixed stage part, and creates floorings after that based on jump difficulty, which increases over time.
	 * 
	 * The game is divided into stages, which are divided into screens, which essentially represent SCREEN_DEFAULT_LENGTH unit blocks.
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
			backgrounds.Add (Instantiate(background, new Vector3(sg.SCREEN_DEFAULT_LENGTH * i - 100, 0, 20), Quaternion.identity));
			if (i > sg.STAGE_DEFAULT_LENGTH) {
				backgrounds [i].GetComponent<Renderer> ().material.color = Color.HSVToRGB (
					(i / sg.STAGE_DEFAULT_LENGTH) * 1.0f / sg.GAME_LENGTH,
					0.4f,
					1.0f);
			}
			backgrounds[i].transform.localScale = normalizeToSize(backgrounds[i], sg.SCREEN_DEFAULT_LENGTH, 50, 0);
		}

		// Initial start area
		player = Instantiate(playerPrefab, new Vector3(0,0,0), Quaternion.identity);
		//player.transform.localScale = normalizeToSize(player, 2 * 400 / 572, 2, 0);
		player.transform.localScale = ScaleToUnit (sg.PLAYER_SIZE, player.GetComponent<SpriteRenderer>().sprite);

		floors.Add(Instantiate(floor, new Vector3(-10,0,3), Quaternion.identity));
		//floors[0].transform.localScale = normalizeToSize(floor, 120.0f, 0.6f, 0.0f);
		floors[0].transform.localScale = FloorScale(floors[0].GetComponent<SpriteRenderer>(), sg.TUTORIAL_FLOOR_LENGTH + 10);

		groundEnemies.Add(Instantiate(groundEnemy, new Vector3(sg.TUTORIAL_FLOOR_LENGTH / 3.0f, 0, 3), Quaternion.identity));
		groundEnemies[0].transform.localScale = ScaleToUnit (sg.GROUND_ENEMY_SIZE, groundEnemies[0].GetComponent<SpriteRenderer> ().sprite);

		groundEnemies.Add(Instantiate(groundEnemy, new Vector3(sg.TUTORIAL_FLOOR_LENGTH / 1.8f,0,3), Quaternion.identity));
		groundEnemies[1].transform.localScale = ScaleToUnit (sg.GROUND_ENEMY_SIZE, groundEnemies[1].GetComponent<SpriteRenderer> ().sprite);

		powerups.Add(Instantiate(powerup, new Vector3(sg.TUTORIAL_FLOOR_LENGTH / 1.3f, 0, 3), Quaternion.identity));
		powerups[0].transform.localScale = ScaleToUnit (sg.POWERUP_SIZE, powerups [0].GetComponent<SpriteRenderer> ().sprite);

		GameObject tutorialEndRamp = Instantiate (ramp, new Vector3 (sg.TUTORIAL_FLOOR_LENGTH - 2, 0, 3), Quaternion.identity);
		tutorialEndRamp.transform.localScale = ScaleToUnit (sg.RAMP_SIZE, tutorialEndRamp.GetComponent<SpriteRenderer> ().sprite);
		ramps.Add (tutorialEndRamp);


		// Generate floors and stuff on them.
		float x = sg.TUTORIAL_FLOOR_LENGTH;
		float y = 0.0f;
		float diff; // Difficulty which increases over time to make the gaps wider and more challenging.
		i = 0;

		// Each loop generates a single floor tile. The while keeps generating them until the game's very end.
		while (x < sg.GetGameLengthInScreens() * sg.SCREEN_DEFAULT_LENGTH - 200) {
			// Raise difficulty based on a reverse exponential function. Speed expected to increase (very) gradually as well.
			// https://i.gyazo.com/956ebcd135567279bb4a00d01e312ca1.png
			diff = (Random.value * 0.5f + 0.75f) * 
				(8 + (Mathf.Pow(x, 0.71f) * 0.080f));
			float angle = (Random.value - 0.47f) * (24 + diff) - 1.4f; // degrees

			// Check if floor would be too high or low; reverse direction if so
			if (y > 20 && angle >= 0)
				angle = -angle;
			else if (y < -(20-sg.DEATH_ZONE_HEIGHT_FROM_BOTTOM) && angle < 0)
				angle = -angle;
			
			x += Mathf.Cos (angle / 180 * Mathf.PI) * diff;
			y += Mathf.Sin (angle / 180 * Mathf.PI) * diff;

			// Midair objects before the actual platform.
			if (Random.value < sg.AIR_ENEMY_SPAWN_CHANCE) {
				GameObject newAirEnemy = Instantiate (airEnemy, new Vector3 (x - 2, y + Random.Range (0, 6), 2), Quaternion.identity);
				newAirEnemy.transform.localScale = ScaleToUnit (sg.AIR_ENEMY_SIZE, newAirEnemy.GetComponent<SpriteRenderer> ().sprite);
				airEnemies.Add(newAirEnemy);
			}
			if (Random.value < sg.MINE_SPAWN_CHANCE) {
				GameObject newMine = Instantiate (mine, new Vector3 (x - 2, y + Random.Range (0, 6), 2), Quaternion.identity);
				newMine.transform.localScale = ScaleToUnit (sg.MINE_SIZE, newMine.GetComponent<SpriteRenderer> ().sprite);
				mines.Add(newMine);
			}
			if (Random.value < sg.AIR_POWERUP_SPAWN_CHANCE) {
				GameObject newPowerup = Instantiate (powerup, new Vector3 (x - (1.5f * Random.Range(-2.0f, 3.0f)), y + Random.Range (3, 8), 2), Quaternion.identity);
				newPowerup.transform.localScale = ScaleToUnit (sg.POWERUP_SIZE, newPowerup.GetComponent<SpriteRenderer> ().sprite);
				powerups.Add(newPowerup);
			}

			// Add the new floor.
			floors.Add(Instantiate(floor, new Vector3(x, y, 3), Quaternion.identity));
			float floorLen = (Random.value * 3.0f + 2.0f) * sg.FLOOR_BASE_LENGTH; // This should scale by difficulty; increased game speed will compensate by reducing the time spent per platform.
			if (i != 0) {
				//floors[i].transform.localScale = normalizeToSize(floor, floorLen, 0.6f, 0.0f);
				floors [i].transform.localScale = FloorScale (floors [i].GetComponent<SpriteRenderer> (), floorLen);
			}


			// Place objects and enemies.
			float leftEdge = x;
			x += sg.GROUND_OBJECT_SPAWN_DISTANCE * 1.4f; // Don't spawn things right on the edge.
			while (x < leftEdge + floorLen - 2) {
				switch (Random.Range(0, 7)) {
				case 0:
					GameObject newGroundEnemy = Instantiate (groundEnemy, new Vector3 (x, y, 2), Quaternion.identity);
					newGroundEnemy.transform.localScale = ScaleToUnit (sg.GROUND_ENEMY_SIZE, newGroundEnemy.GetComponent<SpriteRenderer> ().sprite);
					groundEnemies.Add (newGroundEnemy);
					break;
				case 1:
				case 2:
				case 3:
				case 4:
					break; // generate nothing
				case 5:
					GameObject newPowerup = Instantiate (powerup, new Vector3 (x, y, 1), Quaternion.identity);
					newPowerup.transform.localScale = ScaleToUnit (sg.POWERUP_SIZE, newPowerup.GetComponent<SpriteRenderer> ().sprite);
					powerups.Add (newPowerup);
					break;
				case 6:
					GameObject newRamp = Instantiate (ramp, new Vector3 (x, y, 3), Quaternion.identity);
					newRamp.transform.localScale = ScaleToUnit (sg.RAMP_SIZE, newRamp.GetComponent<SpriteRenderer> ().sprite);
					ramps.Add (newRamp);
					break;
				}

				// increment x
				x += sg.GROUND_OBJECT_SPAWN_DISTANCE * Random.Range(0.7f, 1.42f);
			}


			// Some cleanup; shift indexes. x has been set to match the end of the platform.
			i++;
			x = leftEdge + floorLen - 1.6f;

			// Finally place a ramp near the end.
			//GameObject floorEndRamp = Instantiate (ramp, new Vector3 (x + Random.value * 0.8f, y, 3), Quaternion.identity);
			GameObject floorEndRamp = Instantiate (ramp, new Vector3 (x, y, 3), Quaternion.identity);
			floorEndRamp.transform.localScale = ScaleToUnit (sg.RAMP_SIZE, floorEndRamp.GetComponent<SpriteRenderer> ().sprite);
			ramps.Add (floorEndRamp);
			x = leftEdge + floorLen;
		}


	}

}
