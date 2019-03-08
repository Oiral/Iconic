using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour {

    #region Singleton

    public static MenuFunctions instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Debug.Log("An Extra menu functions script, Deleting", gameObject);
            Destroy(this);
        }
    }

    #endregion

    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Quit()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

    public void MainMenu()
    {
        //SceneManager.LoadScene(0);
        Debug.Log("Load menu");
        animator.SetTrigger("Open Menu");
    }

    public void StartGame()
    {
        //SceneManager.LoadScene(1);
        Debug.Log("Start Game");
        animator.SetTrigger("Close Menu");
        StartCoroutine(GameManager.instance.StartGame());
    }

	public void Toggle_Changed(bool MuteValue)
	{
		//Toggle switch on menu to mute/ unmute music
		Debug.Log ("Music Toggle");
		if (MuteValue) {
			//Mute Music Audio bus
			//NOT public AK.Wwise.Event Mute;
		} else {
			//Unmute Music audio bus
			//NOT public AK.Wwise.Event Unmute;
		}
	}
}
