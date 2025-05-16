using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Required for using Text
public class PlayerController : MonoBehaviour
{
	public float speed = 5f;
	public int health = 5;
	private int score = 0;

	public Text scoreText;   // Link to Score UI
	public Text healthText;  // Link to Health UI

	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		if (rb == null)
		{
			Debug.LogError("PlayerController requires a Rigidbody component.");
		}

		SetScoreText();  // Initialize score UI
		SetHealthText(); // Initialize health UI
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
			SetScoreText();
			// Debug.Log("Score: " + score); // Commented out
			other.gameObject.SetActive(false);
		}

		if (other.CompareTag("Trap"))
		{
			health--;
			SetHealthText();
			// Debug.Log("Health: " + health); // Commented out
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

	void SetScoreText()
	{
		if (scoreText != null)
		{
			scoreText.text = "Score: " + score.ToString();
		}
	}

	// NEW: Update health UI text
	void SetHealthText()
	{
		if (healthText != null)
		{
			healthText.text = "Health: " + health.ToString();
		}
	}
}
