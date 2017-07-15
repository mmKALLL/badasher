using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AirDashIndicator : MonoBehaviour {

	//TODO
	// Yellow button that is up when Airdash is available

	Player player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
