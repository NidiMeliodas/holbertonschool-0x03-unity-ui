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
	public Text winLoseText;
	public Image winLoseBG;

	private Rigidbody rb;
	private bool gameEnded = false;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		if (rb == null)
		{
			Debug.LogError("PlayerController requires a Rigidbody component.");
		}

		SetScoreText();
		SetHealthText();

		if (winLoseText != null)
			winLoseText.text = "";

		if (winLoseBG != null)
			winLoseBG.color = Color.clear;
	}

	void FixedUpdate()
	{
		if (gameEnded) return;

		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		if (gameEnded) return;

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
			gameEnded = true;

			if (winLoseText != null)
			{
				winLoseText.text = "You Win!";
				winLoseText.color = Color.black;
			}

			if (winLoseBG != null)
			{
				winLoseBG.color = Color.green;
			}

			StartCoroutine(LoadScene(3)); // 👈 Restart the game after 3 seconds
		}
	}

	void Update()
	{
		if (!gameEnded && health <= 0)
		{
			gameEnded = true;

			// Debug.Log("Game Over!");
			if (winLoseText != null)
			{
				winLoseText.text = "Game Over!";
				winLoseText.color = Color.white;
			}

			if (winLoseBG != null)
			{
				winLoseBG.color = Color.red;
			}

			StartCoroutine(LoadScene(3)); // Wait 3 seconds then reload
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

	// ✅ Coroutine to reload scene after delay
	IEnumerator LoadScene(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
