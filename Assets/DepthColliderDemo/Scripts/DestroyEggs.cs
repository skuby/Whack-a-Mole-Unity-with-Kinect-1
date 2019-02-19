using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyEggs : MonoBehaviour {
    
	//int counter;
	public static int counter;
	public GameObject explosion;
	public AudioClip clip;
    AudioSource audiosource;
	//public bool check;
	//public Text scoretext;
	// Use this for initialization
	void Start () {
		
		counter = 0;
		//GetComponent <AudioSource> ().playOnAwake = false;
		audiosource = GetComponent <AudioSource> ();

		//check = false;
	}
	
	// Update is called once per frame
	void Update () 
	{


	}

	void OnTriggerEnter (Collider other)
	{
		
		if (gameObject.name == "EggPrefab7(Clone)") 
		{
			DepthImageViewer.score += 1;
			Debug.Log ("Score = " + DepthImageViewer.score);
			Instantiate (explosion, transform.position, transform.rotation);
			audiosource.PlayOneShot (clip, 0.7f);
			Destroy (gameObject, 0.2f);


		}
	}



//	void calculateScore()
//	{
//		if (explosion == true) {
//			count += 10;
//			//Debug.Log (explosion);
//
//			Debug.Log (count);
//		}
//
//		//counter = count + 1;
//
//
//	}

//	void Check()
//	{
//		if (audiosource.isPlaying) 
//		{
//			//check = true;
//			counter = counter + 1;
//			Debug.Log (counter);
//			Debug.Log ("audio played");
//		}

		//counter = count + 1;
	//}

//	void Count()
//	{
//		if (check == true) {
//			
//			counter = counter + 1;
//	
//		//	check = true;
//
//			Debug.Log (counter);
//		}

}
