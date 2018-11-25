using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class PauseScript : MonoBehaviour {

    public static UnityEvent OnPauseEvent = new UnityEvent();

    public static bool paused = false;

    public GameObject pauseMenu;

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        paused = !paused;
        OnPauseEvent.Invoke();
        pauseMenu.SetActive(paused);
    }
}
