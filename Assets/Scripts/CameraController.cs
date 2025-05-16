using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public GameObject player;  // Assign via Inspector
	private Vector3 offset;    // Stores initial offset between camera and player

	void Start()
	{
		if (player == null)
		{
			Debug.LogError("CameraController: Aucun objet 'player' assigné.");
			return;
		}

		// Calculate the initial offset
		offset = transform.position - player.transform.position;
	}

	void LateUpdate()
	{
		if (player != null)
		{
			// Maintain the initial offset from the player
			transform.position = player.transform.position + offset;
		}
	}
}
