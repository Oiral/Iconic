using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotCounter : MonoBehaviour {

    public GameObject image;
    public float numberOfIcons;

    public List<GameObject> blocks;

    private void OnGUI()
    {
        
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            Character player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();

            if (player != null)
            {
                if (blocks.Count < player.multiShot)
                {
                    blocks.Add(Instantiate(image, this.transform));
                    numberOfIcons += 1;
                }else if (blocks.Count > player.multiShot)
                {
                    Destroy(blocks[blocks.Count - 1]);
                    blocks.RemoveAt(blocks.Count - 1);
                    numberOfIcons -= 1;
                }
                
            }
        }
    }
}
