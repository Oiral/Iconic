using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftManager : MonoBehaviour {

    #region singleton

    public static ShiftManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public Material shiftMaterial;

    public float currentShift;
    public float maxShift;

    public void UpdateShift(float num)
    {
        //Debug.Log(num);
        float tempNum = maxShift * num;
        shiftMaterial.SetFloat("_Refraction",tempNum);
    }
}
