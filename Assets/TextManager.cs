using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

    public static List<string> messages = new List<string>();

    bool messageRunning;
    public Text messageText;

    [Header("Power up messages")]
    public Image powerupImage;
    public List<PowerUpType> powerUpMessages;
    bool powerUpMessageRunning;

    public List<Sprite> powerUpSprites;

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
        if (powerUpMessages.Count > 0 && powerUpMessageRunning == false)
        {
            StartCoroutine(runPowerUp());
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

    IEnumerator runPowerUp()
    {
        powerUpMessageRunning = true;

        powerupImage.gameObject.SetActive(true);
        powerupImage.sprite = powerUpSprites[(int)powerUpMessages[0]];
        yield return StartCoroutine(ChangeTransparancyTo(powerupImage, 0, 1, 2f));
        //messageText.rectTransform.localPosition = Vector3.zero;

        yield return new WaitForSeconds(1f);
        //messageText.text = null;
        powerUpMessages.RemoveAt(0);

        yield return StartCoroutine(ChangeTransparancyTo(powerupImage, 1, 0, 2f));

        yield return new WaitForSeconds(0.5f);
        powerupImage.gameObject.SetActive(false);
        powerUpMessageRunning = false;
    }

    IEnumerator ChangeTransparancyTo(Text textToChange, float a, float b, float speed)
    {
        Color col = textToChange.color;
        float step = (speed / (a - b)) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step; // Goes from 0 to 1, incrementing by step each time
            col.a = Mathf.Lerp(a, b, t); // Move objectToMove closer to b
            textToChange.color = col;
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }
        col.a = b;
        textToChange.color = col;
    }

    IEnumerator ChangeTransparancyTo(Image imageToChange, float a, float b, float speed)
    {
        Color col = imageToChange.color;
        float step = (speed / Mathf.Abs(a - b)) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step; // Goes from 0 to 1, incrementing by step each time
            col.a = Mathf.Lerp(a, b, t); // Move objectToMove closer to b
            imageToChange.color = col;
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }
        col.a = b;
        imageToChange.color = col;
    }

}
