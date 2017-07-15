using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DashCooldown : MonoBehaviour {

	//TODO
	// bar that depletes on dash use and fills according to DashCooldown

	Player player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
