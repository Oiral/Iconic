using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DestroySphere : MonoBehaviour {

    public float growRate = 2;

    public float maxSize = 200;
    public float currentSize = 1;

    public GameObject collder;

    public LineRenderer line;

    public MeshFilter mesh;

    private void Start()
    {
        line = GetComponentInChildren<LineRenderer>();
        mesh = GetComponentInChildren<MeshFilter>();
    }

    // Update is called once per frame
    void Update () {
        UpdateVisuals();

        collder.transform.localScale = Vector3.one * currentSize;

        if (Application.isPlaying == true)
        {
            currentSize += (Time.deltaTime * growRate);

            if (transform.localScale.x > maxSize)
            {
                Destroy(gameObject);
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.RemoveHealth(enemyHealth.health);
            }
        }
        else if (collision.tag == "Bullet")
        {
            Destroy(collision.gameObject);
        }else if (collision.tag == "Power Up")
        {
            Destroy(collision.gameObject);
        }
    }

    void UpdateVisuals()
    {
        Vector3[] verts = mesh.sharedMesh.vertices;


        List<Vector3> odd = new List<Vector3>();
        List<Vector3> even = new List<Vector3>();

        for (int i = 0; i < verts.Length; i++)
        {
            //flip the circle vertical
            verts[i].y = verts[i].z;
            verts[i].z = 0;

            //Make it the size of the current size
            verts[i] = verts[i] * currentSize;

            //Make the points relative to the position of the object
            verts[i] = verts[i] + transform.position;
            if (i % 2 == 1)
            {
                odd.Add(verts[i]);
            }
            else
            {
                even.Add(verts[i]);
            }

        }
        odd.Reverse();
        even.AddRange(odd);
        

        Vector3[] reorderedVerts = even.ToArray();
        
        line.positionCount = reorderedVerts.Length;
        line.SetPositions(reorderedVerts);
    }
}
