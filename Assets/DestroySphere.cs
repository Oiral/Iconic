using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DestroySphere : MonoBehaviour {

    public float growRate = 2;

    public float maxSize = 200;

    public LineRenderer line;

    public MeshFilter mesh;

    private void Start()
    {
        line = GetComponentInChildren<LineRenderer>();
        mesh = GetComponentInChildren<MeshFilter>();
    }

    // Update is called once per frame
    void Update () {
        Vector3[] verts = mesh.mesh.vertices;

        
        List<Vector3> odd = new List<Vector3>();
        List<Vector3> even = new List<Vector3>();

        for (int i = 0; i < verts.Length; i++)
        {
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

        if (Application.isPlaying == true)
        {
            transform.localScale = transform.localScale + (Vector3.one * Time.deltaTime * growRate);

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
}
