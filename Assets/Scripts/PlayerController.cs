using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public float speed = 5f;
	public int health = 5;
	private int score = 0;

	public Text scoreText;
	public Text healthText;
	public Text winLoseText;       // NEW: Reference to WinLoseText UI
	public Image winLoseBG;        // NEW: Reference to WinLoseBG UI

	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		if (rb == null)
		{
			Debug.LogError("PlayerController requires a Rigidbody component.");
		}

		SetScoreText();
		SetHealthText();

		// Optional: Hide win/lose message at start
		if (winLoseText != null)
			winLoseText.text = "";

		if (winLoseBG != null)
			winLoseBG.color = Color.clear;
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
			// Debug.Log("Score: " + score);
			other.gameObject.SetActive(false);
		}

		if (other.CompareTag("Trap"))
		{
			health--;
			SetHealthText();
			// Debug.Log("Health: " + health);
		}

		if (other.CompareTag("Goal"))
		{
			// Debug.Log("You win!"); // Commented out as requested

			if (winLoseText != null)
			{
				winLoseText.text = "You Win!";
				winLoseText.color = Color.black;
			}

			if (winLoseBG != null)
			{
				winLoseBG.color = Color.green;
			}
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

	void SetHealthText()
	{
		if (healthText != null)
		{
			healthText.text = "Health: " + health.ToString();
		}
	}
}
