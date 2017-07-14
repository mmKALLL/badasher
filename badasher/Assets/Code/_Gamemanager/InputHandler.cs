using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {



	private float timeFromLastDash(){
		
	}

	public void Start(){
		
	}

	private IEnumerator WaitForDashButton(){
		while (!Input.GetKeyDown (KeyCode.D)) {
			yield return null;
		}
		yield return new WaitForFixedUpdate ();
	}

	private IEnumerator CountTimeFromLastDash(){
		while (timeFromLastDash < PlayerConstants.BOOST_POWER_INPUT_BUFFER) {
			this.timeFromLastDash += Time.deltaTime;
			yield return null;
		}
		yield return new WaitForFixedUpdate ();
	}
}
