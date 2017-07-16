using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLauncher : MonoBehaviour {

	// TODO
	public GameObject groundEnemy;
	public GameObject airEnemy;
	public GameObject player;
	public GameObject floor;
	public GameObject background;
	public GameObject powerup;

	// boots up the stuff
	void Start () {
		// TODO
	}


	protected internal static class StageGeneratorConstants {
		// The game is divided into stages, which are divided into screens, which essentially represent 50 unit blocks.
		// Each screen should be the size of one BG image, which are then looped to fill the stage.
		// Each stage may have differing properties, for example.

		public const int GAME_LENGTH = 10; // Length in stages
		public const int STAGE_DEFAULT_LENGTH = 40; // Length in stages
		public const int[] STAGE_LENGTHS = new int[GAME_LENGTH];
		private int GAME_LENGTH_SCREENS = 0;
		public int getGameLengthInScreens(){
			return GAME_LENGTH_SCREENS;
		}

		public StageGeneratorConstants() {

			for (int i = 0; i < STAGE_LENGTHS.Length; i++) {
				STAGE_LENGTHS[i] = STAGE_DEFAULT_LENGTH;
				GAME_LENGTH_SCREENS += STAGE_DEFAULT_LENGTH;
			}

		}
	}

	public void GenerateStage(){
		// TODO ESA

		// Ingame screen size UI exlusive expected to be around 50x20 units

		// Arrays for holding the objects, sorted by initial X coordinate.
		GameObject[] floors = new GameObject[];

		GameObject instantiatedObject = Instantiate(enemy_ground, new Vector3(), Quaternion.identity);
		GameObject instantiatedObject = Instantiate(enemy_ground, new Vector3(), Quaternion.identity);
		instantiatedObject.transform.localScale = new Vector3 (1, 1, 1);



	}

}
