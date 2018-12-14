using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

    public static List<string> messages = new List<string>();

    public bool messageRunning;
    public Text messageText;

    private void Start()
    {
        //messages.Add("Testing the update");
        //messages.Add("Testing the update again");
    }

    // Update is called once per frame
    void Update () {
		if (messages.Count > 0 && messageRunning == false)
        {
            StartCoroutine(runMessage());
        }
	}

    IEnumerator runMessage()
    {
        messageRunning = true;
        messageText.gameObject.SetActive(true);
        messageText.text = messages[0];
        messageText.rectTransform.localPosition = Vector3.zero;
        yield return new WaitForSeconds(1f);
        messageText.text = null;
        messages.RemoveAt(0);
        
        yield return new WaitForSeconds(0.5f);
        messageText.gameObject.SetActive(false);
        messageRunning = false;
    }
}
