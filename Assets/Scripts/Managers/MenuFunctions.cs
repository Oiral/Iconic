using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour {

    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        //SceneManager.LoadScene(0);
        Debug.Log("Load menu");
    }

    public void StartGame()
    {
        //SceneManager.LoadScene(1);
        Debug.Log("Start Game");
        animator.SetTrigger("Close Menu");
        StartCoroutine(GameManager.instance.StartGame());
    }
}
