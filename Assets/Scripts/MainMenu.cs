using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public Material trapMat;
	public Material goalMat;
	public Toggle colorblindMode;

	private const string COLORBLIND_PREF_KEY = "ColorblindMode";

	void Start()
	{
		// Load saved toggle state or default to false
		bool isColorblind = PlayerPrefs.GetInt(COLORBLIND_PREF_KEY, 0) == 1;
		colorblindMode.isOn = isColorblind;

		// Reset materials to default on menu start
		ResetMaterials();

		// Add listener to toggle to save preference when changed
		colorblindMode.onValueChanged.AddListener(OnToggleValueChanged);
	}

	void OnToggleValueChanged(bool isOn)
	{
		PlayerPrefs.SetInt(COLORBLIND_PREF_KEY, isOn ? 1 : 0);
		PlayerPrefs.Save();
	}

	public void PlayMaze()
	{
		// Apply colors based on saved toggle value
		if (colorblindMode != null && colorblindMode.isOn)
		{
			if (trapMat != null)
				trapMat.color = new Color32(255, 112, 0, 255); // Orange

			if (goalMat != null)
				goalMat.color = Color.blue;
		}
		else
		{
			ResetMaterials();
		}

		SceneManager.LoadScene("maze");
	}

	void ResetMaterials()
	{
		if (trapMat != null)
			trapMat.color = Color.red;
		if (goalMat != null)
			goalMat.color = Color.green;
	}

	public void QuitMaze()
	{
		Debug.Log("Quit Game");
		Application.Quit();
	}
}
