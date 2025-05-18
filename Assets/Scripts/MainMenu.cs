using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void PlayMaze()
	{
		SceneManager.LoadScene("maze"); // Replace "maze" with your actual scene name
	}

	public void QuitMaze()
	{
		Debug.Log("Quit Game");

		// This only works in a built application (not in the editor)
		Application.Quit();
	}
}
