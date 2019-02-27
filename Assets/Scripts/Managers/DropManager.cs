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

    public GameObject guaranteedDrop;

    [Range(0, 1)]
    public float dropChance;

    public void DropPowerUp(Vector3 pos)
    {
        if (Random.Range(0f, 1f) < dropChance)
        {
            Instantiate(drops[Random.Range(0, drops.Count)], transform.position, Quaternion.identity, null);
        }
    }

    public void DropGuaranteed(Vector3 pos)
    {
        Instantiate(guaranteedDrop, transform.position, Quaternion.identity, null);
    }

    public GameObject PickDrop()
    {
        return (drops[Random.Range(0, drops.Count)]);
    }
}
