using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundObject : MonoBehaviour {

	AudioSource[] BGMlist;

	void Awake(){
		DontDestroyOnLoad (this.gameObject);
		BGMlist = transform.GetComponents<AudioSource> ();
		//SceneManager.sceneLoaded += 
		SceneManager.LoadScene(1);
		BGMlist[0].Play();
	}




	void PlayMusic(Scene scene, LoadSceneMode mode){
		
	
	}
}
