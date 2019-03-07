using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class PauseScript : MonoBehaviour {

    #region Singleton

    public static PauseScript instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("An Extra pause script, Deleting", gameObject);
            Destroy(this);
        }
    }

    #endregion

    public static UnityEvent OnPauseEvent = new UnityEvent();

    public static bool paused = false;

    public GameObject pauseMenu;

    private void Update()
    {
        if (Input.GetButtonDown("Pause") && EnemyManager.instance.gameOver == false)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        paused = !paused;
        OnPauseEvent.Invoke();
        pauseMenu.SetActive(paused);
        if (paused == true)
        {
            Debug.Log("Test Pause");
            GetComponentInChildren<Animator>().SetTrigger("Play");
        }
    }
}
