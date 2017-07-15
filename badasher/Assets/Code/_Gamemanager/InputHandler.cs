using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

	private Player player;

	private float timeAtLastDash;
	private Coroutine waitForInputsStorage;

	public void Start (){
		player = this.GetComponent<Player> ();
		waitForInputsStorage = StartCoroutine(WaitForDashButtons());
	}

	public void EndThis (){
		StopAllCoroutines ();
	}

	private IEnumerator WaitForDashButtons (){ // main ienumerator
		while (true){
			yield return WaitForDashButton();
			if (Time.realtimeSinceStartup - timeAtLastDash < PlayerConstants.BOOST_POWER_INPUT_BUFFER) {
				if (player.IsJumpDashing()) {
					player.PlayerDash ();
				} else {
					player.PlayerBoostPower ();
				}
			} else {
				player.PlayerDash ();
			}
			timeAtLastDash = Time.realtimeSinceStartup;
		}
	}

	private IEnumerator WaitForDashButton (){
		while (!Input.GetKeyDown (KeyCode.D)) {
			yield return null;
		}
		Debug.Log ("KEY GOT!");
		yield return new WaitForFixedUpdate ();
	}
}
