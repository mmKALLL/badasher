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
		public float GROUND_OBJECT_SPAWN_DISTANCE;

		public StageGeneratorConstants() {
			GAME_LENGTH = 10;
			STAGE_DEFAULT_LENGTH = 20;
			SCREEN_DEFAULT_LENGTH = 100;
			GAME_LENGTH_IN_SCREENS = 0;
			STAGE_LENGTHS = new int[GAME_LENGTH];
			for (int i = 0; i < STAGE_LENGTHS.Length; i++) {
				STAGE_LENGTHS[i] = STAGE_DEFAULT_LENGTH;		// It is expected in GenerateStage that each stage is equal in length.
				GAME_LENGTH_IN_SCREENS += STAGE_DEFAULT_LENGTH;
			}

			DEATH_ZONE_HEIGHT_FROM_BOTTOM = 7; // Unused apart from generation, for now.
			TUTORIAL_FLOOR_LENGTH = 40;
			FLOOR_BASE_LENGTH = 14; // Average length, some variation included.

			AIR_ENEMY_SPAWN_CHANCE = 0.12f;
			MINE_SPAWN_CHANCE = 0.6f;
			GROUND_OBJECT_SPAWN_DISTANCE = 2.5f; // The smaller this is, the more objects (and challenge) you get. TODO: Could be scaled by difficulty?
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
		Vector3 returnee = new Vector3 (heightScale, heightScale, 1);
		return returnee;
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
		//player.transform.localScale = normalizeToSize(player, 2 * 400 / 572, 2, 0);
		player.transform.localScale = ScaleToUnit (2, player.GetComponent<SpriteRenderer>().sprite);

		floors.Add(Instantiate(floor, new Vector3(-10,0,3), Quaternion.identity));
		//floors[0].transform.localScale = normalizeToSize(floor, 120.0f, 0.6f, 0.0f);
		floors[0].transform.localScale = FloorScale(floors[0].GetComponent<SpriteRenderer>(), 100f);

		groundEnemies.Add(Instantiate(groundEnemy, new Vector3(40,0,3), Quaternion.identity));
		groundEnemies[0].transform.localScale = ScaleToUnit (2f, groundEnemies[0].GetComponent<SpriteRenderer> ().sprite);

		powerups.Add(Instantiate(powerup, new Vector3(70, 0 ,3), Quaternion.identity));
		powerups[0].transform.localScale = ScaleToUnit (1.4f, powerups [0].GetComponent<SpriteRenderer> ().sprite);

		GameObject tutorialEndRamp = Instantiate (ramp, new Vector3 (97-10, 0, 3), Quaternion.identity);
		tutorialEndRamp.transform.localScale = ScaleToUnit (0.6f, tutorialEndRamp.GetComponent<SpriteRenderer> ().sprite);
		ramps.Add (tutorialEndRamp);
		// FIXME: Tutorial end ramp does not get generated or added properly?
		// TODO: Add more objects for a tutorial-ish start





		// Generate floors and stuff on them.
		float x = 90.0f;
		float y = 0.0f;
		float diff; // Difficulty which increases over time to make the gaps wider and more challenging.
		i = 0;

		// Each loop generates a single floor tile. The while keeps generating them until the game's very end.
		while (x < sg.GetGameLengthInScreens() * 80 - 200) {
			// Raise difficulty based on a reverse exponential function. Speed expected to increase (very) gradually as well.
			// https://i.gyazo.com/956ebcd135567279bb4a00d01e312ca1.png
			diff = (Random.value * 0.5f + 0.75f) * 
				(4 + (Mathf.Pow(x, 0.71f) * 0.030f));
			float angle = (Random.value - 0.5f) * (24 + diff * 2) - 1.8f;

			// Check if floor would be too high or low; reverse direction if so
			if (y > 20 && angle >= 0)
				angle = -angle;
			else if (y < -(20-sg.DEATH_ZONE_HEIGHT_FROM_BOTTOM) && angle < 0)
				angle = -angle;
			x += Mathf.Cos (angle / 180 * Mathf.PI) * diff;
			y += Mathf.Sin (angle / 180 * Mathf.PI) * diff;

			if (Random.value < sg.AIR_ENEMY_SPAWN_CHANCE) {
				GameObject newAirEnemy = Instantiate (airEnemy, new Vector3 (x - 2, y + Random.Range (1, 6), 2), Quaternion.identity);
				newAirEnemy.transform.localScale = ScaleToUnit (0.8f, newAirEnemy.GetComponent<SpriteRenderer> ().sprite);
				airEnemies.Add(newAirEnemy);
			}
			if (Random.value < sg.MINE_SPAWN_CHANCE) {
				GameObject newMine = Instantiate (mine, new Vector3 (x - 2, y + Random.Range (1, 6), 2), Quaternion.identity);
				newMine.transform.localScale = ScaleToUnit (1.2f, newMine.GetComponent<SpriteRenderer> ().sprite);
				mines.Add(newMine);
			}
			floors.Add(Instantiate(floor, new Vector3(x, y, 3), Quaternion.identity));
			float floorLen = (Random.value * 2.0f + 2.0f) / 3 * sg.FLOOR_BASE_LENGTH; // This should scale by difficulty; increased game speed will compensate by reducing the time spent per platform.
			if (i != 0) {
				//floors[i].transform.localScale = normalizeToSize(floor, floorLen, 0.6f, 0.0f);
				floors [i].transform.localScale = FloorScale (floors [0].GetComponent<SpriteRenderer> (), floorLen);
			}


			// Place objects and enemies.
			float leftEdge = x;
			x += 2;
			while (x < leftEdge + floorLen - 4) {
				switch (Random.Range(0, 7)) {
				case 0:
					GameObject newGroundEnemy = Instantiate (groundEnemy, new Vector3 (x, y, 2), Quaternion.identity);
					newGroundEnemy.transform.localScale = ScaleToUnit (1f, newGroundEnemy.GetComponent<SpriteRenderer> ().sprite);
					groundEnemies.Add (newGroundEnemy);
					break;
				case 1:
				case 2:
				case 3:
					break; // generate nothing
				case 4:
				case 5:
					GameObject newPowerup = Instantiate (powerup, new Vector3 (x, y+0.5f, 1), Quaternion.identity);
					newPowerup.transform.localScale = ScaleToUnit (0.5f, newPowerup.GetComponent<SpriteRenderer> ().sprite);
					powerups.Add (newPowerup);
					break;
				case 6:
					GameObject newRamp = Instantiate (ramp, new Vector3 (x, y, 3), Quaternion.identity);
					newRamp.transform.localScale = ScaleToUnit (0.6f, newRamp.GetComponent<SpriteRenderer> ().sprite);
					ramps.Add (newRamp);
					break;
				}

				// increment x
				x += sg.GROUND_OBJECT_SPAWN_DISTANCE;
			}


			// Some cleanup; shift indexes. x has been set to match the end of the platform.
			i++;
			x = leftEdge + floorLen - 1.8f;

			// Finally place a ramp near the end.
			//GameObject floorEndRamp = Instantiate (ramp, new Vector3 (x + Random.value * 0.8f, y, 3), Quaternion.identity);
			GameObject floorEndRamp = Instantiate (ramp, new Vector3 (x + Random.value * 0.2f-0.2f, y, 3), Quaternion.identity);
			floorEndRamp.transform.localScale = ScaleToUnit (0.6f, floorEndRamp.GetComponent<SpriteRenderer> ().sprite);
			ramps.Add (floorEndRamp);
			x = leftEdge + floorLen;
		}


	}

}
