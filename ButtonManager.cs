using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	public void GoToGame()
	{
		SceneManager.LoadScene("Main", LoadSceneMode.Single);
		Debug.Log("CLICK");
	}
}
