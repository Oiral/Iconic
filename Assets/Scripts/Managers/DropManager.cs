using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour {

    #region Singleton
    public static DropManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Debug.LogError("Multiple of Drop Manager in the scene - Deleting Script", gameObject);
            Destroy(this);
        }
    }

    #endregion

    public List<GameObject> drops = new List<GameObject>();

    public void DropPowerUp(Vector3 pos)
    {

    }

    public GameObject PickDrop()
    {
        return (drops[Random.Range(0, drops.Count)]);
    }
}
