using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Required for using Text

public class PlayerController : MonoBehaviour
{
	public float speed = 5f;       // Controls movement speed in Inspector
	public int health = 5;         // Starting health
	private int score = 0;         // Starting score

	public Text scoreText;         // UI Text to show score

	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		if (rb == null)
		{
			Debug.LogError("PlayerController requires a Rigidbody component.");
		}

		SetScoreText(); // Initialize UI with starting score
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Pickup"))
		{
			score++;
			SetScoreText(); // Update the UI
							// Debug.Log("Score: " + score); // Commented out as requested
			other.gameObject.SetActive(false);
		}

		if (other.CompareTag("Trap"))
		{
			health--;
			Debug.Log("Health: " + health);
		}

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
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			health = 5;
			score = 0;
		}
	}

	// NEW: Updates score UI text
	void SetScoreText()
	{
		if (scoreText != null)
		{
			scoreText.text = "Score: " + score.ToString();
		}
	}
}
