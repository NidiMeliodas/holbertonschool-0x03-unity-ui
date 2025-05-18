using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void PlayMaze()
	{
		SceneManager.LoadScene("maze");
	}
}