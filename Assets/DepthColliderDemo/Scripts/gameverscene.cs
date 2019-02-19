using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class gameverscene : MonoBehaviour {

	// Use this for initialization

	//public Text countdowntimer;
	public Text totalscore;
	public AudioClip clip;
	AudioSource audiosource;
	public float time = 12f;
	void Start () {
		
		audiosource = GetComponent <AudioSource> ();
		//audiosource.loop = true;

		audiosource.Play();
		//audiosource.loop = true;

	}
	// Update is called once per frame
	void Update () {
		
		totalscore.text = "YOU WHACKED " + DepthImageViewer.score.ToString("F0") + " TRUMPS";
		time = time - Time.deltaTime;
		if (Input.GetKeyDown (KeyCode.Space) || (time <= 0)) {
			Debug.Log (time);
			Debug.Log ("Space key was pressed.");
			Application.LoadLevel (2);
		
		}
		//countdowntimer.text = "Game Restarting In: " + time.ToString("F0");

		//time = time - Time.deltaTime;

		//if (time <= 0)
		//{
		//	Application.LoadLevel (0);
		//}

	}
}
