using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whackatrump : MonoBehaviour {

	// Use this for initialization
	AudioSource audiosource;
	public AudioClip trump;

	void Start () {
		audiosource = GetComponent<AudioSource> ();
		audiosource.Play();
		audiosource.loop = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {

			Debug.Log ("Space key was pressed.");
			Application.LoadLevel (0);
		}
	}
}
