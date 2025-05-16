using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
	public float speed = 5f;      // Controls movement speed in Inspector
	public int health = 5;        // Starting health
	private int score = 0;        // Starting score

	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		if (rb == null)
		{
			Debug.LogError("PlayerController requires a Rigidbody component.");
		}
	}

	void FixedUpdate()
	{
		// Get movement input
		float moveHorizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows
		float moveVertical = Input.GetAxis("Vertical");     // W/S or Up/Down arrows

		// Only move on X and Z (no Y)
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		// Move the player using physics
		rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		// Pickup logic
		if (other.CompareTag("Pickup"))
		{
			score++;
			Debug.Log("Score: " + score);
			other.gameObject.SetActive(false); // or Destroy(other.gameObject);
		}

		// Trap logic
		if (other.CompareTag("Trap"))
		{
			health--;
			Debug.Log("Health: " + health);
		}

		// Goal logic
		if (other.CompareTag("Goal"))
		{
			Debug.Log("You win!");
		}
	}

	void Update()
	{
		if (health <= 0)
		{
			Debug.Log("Game Over!");

			// Reload the current scene
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

			// Reset values — optional if you're using Start() to initialize them
			health = 5;
			score = 0;
		}
	}

}
