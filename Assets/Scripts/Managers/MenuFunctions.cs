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
}
