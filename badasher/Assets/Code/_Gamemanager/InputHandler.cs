using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

	private Player player;

	private float timeAtLastDash;
	private Coroutine waitForInputsStorage;

	public void Start (){
		// initialize player;
		waitForInputsStorage = StartCoroutine(WaitForDashButtons());
	}

	public void EndThis (){
		StopAllCoroutines ();
	}

	private IEnumerator WaitForDashButtons (){ // main ienumerator
		while (true){
			yield return WaitForDashButton();
			if (Time.realtimeSinceStartup - timeAtLastDash < PlayerConstants.BOOST_POWER_INPUT_BUFFER) {
				player.PlayerBoostPower();
			} else {
				player.PlayerDash();
			}
			timeAtLastDash = Time.realtimeSinceStartup;
		}
	}

	private IEnumerator WaitForDashButton (){
		while (!Input.GetKeyDown (KeyCode.D)) {
			yield return null;
		}
		yield return new WaitForFixedUpdate ();
	}
}
