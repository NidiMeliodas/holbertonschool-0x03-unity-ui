using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
	void Update()
	{
		// Rotate 45 degrees per second on the X-axis
		transform.Rotate(45 * Time.deltaTime, 0, 0);
	}
}
