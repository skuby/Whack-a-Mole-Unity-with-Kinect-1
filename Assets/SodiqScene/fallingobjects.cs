using UnityEngine;
using System.Collections;

public class fallingobjects : MonoBehaviour 
{
	public Transform eggPrefab;

	public float nextEggTime = 0.0f;
	public float spawnRate = 1.5f;

	void Update () 
	{
		if (nextEggTime < Time.time)
		{
			SpawnEgg();
			nextEggTime = Time.time + spawnRate;

			spawnRate = Mathf.Clamp(spawnRate, 0.3f, 99f);
		}
	}

	void SpawnEgg()
	{
		KinectManager manager = KinectManager.Instance;

		if(eggPrefab && manager && manager.IsInitialized() && manager.IsUserDetected())
		{
			uint userId = manager.GetPlayer1ID();
			Vector3 posUser = manager.GetUserPosition(userId);
			//Vector3 posUser = manager.GetRawSkeletonJointPos(userId,5);
			float addXPos = Random.Range(-1.3f, 15.5f);
			Vector3 spawnPos = new Vector3(addXPos, 10f, posUser.z);
			//Debug.Log (addXPos+ "10f" + posUser.z);
			//Debug.Log (addXPos);

			Transform eggTransform = Instantiate(eggPrefab, spawnPos, Quaternion.identity) as Transform;
			eggTransform.parent = transform;
		}
	}

}
