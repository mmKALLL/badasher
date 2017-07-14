using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {


	public void Start(){
	
	}

	private IEnumerator WaitForDashButton(){
		while (!Input.GetKey (KeyCode.D)) {
			break;
		}
		yield return null;
	}
}
