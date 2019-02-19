using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DepthImageViewer : MonoBehaviour 
{
	// the KinectManager instance
	//DestroyEggs counting = new DestroyEggs();
	private KinectManager manager;

	// the foreground texture
	private Texture2D foregroundTex;
	
	// rectangle taken by the foreground texture (in pixels)
	private Rect foregroundRect;
	private Vector2 foregroundOfs;

	// game objects to contain the joint colliders
	private GameObject[] jointColliders = null;
	public float timer;
	public Text timertext;
	public Text scoretext;
	public static int score;
	void Start () 
	{
		score = 0;

		//counter = 1;
		// calculate the foreground rectangle


		Rect cameraRect = Camera.main.pixelRect;
		float rectHeight = cameraRect.height;
		float rectWidth = cameraRect.width;
		
		if(rectWidth > rectHeight)
			rectWidth = rectHeight * KinectWrapper.Constants.DepthImageWidth / KinectWrapper.Constants.DepthImageHeight;
		else
			rectHeight = rectWidth * KinectWrapper.Constants.DepthImageHeight / KinectWrapper.Constants.DepthImageWidth;

		foregroundOfs = new Vector2((cameraRect.width - rectWidth) / 2, (cameraRect.height - rectHeight) / 2);
		foregroundRect = new Rect(foregroundOfs.x, cameraRect.height - foregroundOfs.y, rectWidth, -rectHeight);
	
		// create joint colliders
		int numColliders = (int)KinectWrapper.NuiSkeletonPositionIndex.Count;
		jointColliders = new GameObject[numColliders];
		
		for(int i = 0; i < numColliders; i++)
		{
			string sColObjectName = ((KinectWrapper.NuiSkeletonPositionIndex)i).ToString() + "Collider";
			jointColliders[i] = new GameObject(sColObjectName);
			jointColliders[i].transform.parent = transform;
			
			SphereCollider collider = jointColliders[i].AddComponent<SphereCollider>();
		    //collider.isTrigger = true;
			//Debug.Log("Trigger On : " + collider.isTrigger);
			 collider.radius = 1f;
			//Debug.Log (collider);
			//collider.

		}
			
	}
//	void OnTriggerEnter (Collider other)
//	{
//		Destroy (other.gameObject);
//		Debug.Log ("Destroyed");
//	}
//	Destroy other sphere collider here
//	void OnCollisionEnter (Collision col)
//	{
//		//if(col.gameObject.name == "EggSpawner")
//		//{
//		Destroy(gameObject);
//		Debug.Log ("Destroyed");
//		Debug.Log (gameObject);
//		//}
//	}
//
//	void OnTriggerEnter (Collider other)
//	{
//		
//			//count = count + 1;
//			Destroy (other.gameObject);
//			Debug.Log (gameObject);
//			//counter = counter + 1;
//			//Debug.Log (counter);
//
//	}



	void Update () 
	{
		if(manager == null)
		{
			manager = KinectManager.Instance;

		}




		// get the users texture
		if(manager && manager.IsInitialized())
		{
			foregroundTex = manager.GetUsersLblTex();

		}

		if(manager.IsUserDetected())
		{
			uint userId = manager.GetPlayer1ID();
			timer -= Time.deltaTime;
			timertext.text = "Time: " + timer.ToString("F0");
			//DestroyEggs.counter++;
			scoretext.text = "Score: " + score.ToString("F0");
			// update colliders


			int numColliders = (int)KinectWrapper.NuiSkeletonPositionIndex.Count;
			//Debug.Log (numColliders);
			for(int i = 0; i < numColliders; i++)
			{
				if(manager.IsJointTracked(userId, i))
				{
					Vector3 posJoint = manager.GetRawSkeletonJointPos(userId, i);

					if(posJoint != Vector3.zero)
					{
						// convert the joint 3d position to depth 2d coordinates
						Vector2 posDepth = manager.GetDepthMapPosForJointPos(posJoint);
						
						float scaledX = posDepth.x * foregroundRect.width / KinectWrapper.Constants.DepthImageWidth;
						float scaledY = posDepth.y * -foregroundRect.height / KinectWrapper.Constants.DepthImageHeight;

						float screenX = foregroundOfs.x + scaledX;
						float screenY = Camera.main.pixelHeight - (foregroundOfs.y + scaledY);
						float zDistance = posJoint.z - Camera.main.transform.position.z;
						
						Vector3 posScreen = new Vector3(screenX, screenY, zDistance);
						Vector3 posCollider = Camera.main.ScreenToWorldPoint(posScreen);

						jointColliders[i].transform.position = posCollider;
					}
				}
			}
		}

		//timer -= Time.deltaTime;

		if (timer < 0)
		{
			DestroyImmediate (Camera.main.gameObject);
			//DestroyImmediate (manager);
			Debug.Log ("Game Over");
			Application.LoadLevel (1);

		}

		if (Input.GetKeyDown (KeyCode.Space)) {

			Debug.Log ("Space key was pressed.");
			//DestroyImmediate (Camera.main.gameObject);
			DestroyImmediate (manager);
			Application.LoadLevel (2);

		}


	}

	void OnGUI()
	{
		if(foregroundTex)
		{
			GUI.DrawTexture(foregroundRect, foregroundTex);
		}
	}

}
