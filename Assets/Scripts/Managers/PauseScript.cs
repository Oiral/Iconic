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
