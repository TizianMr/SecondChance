using System.Collections.Generic;
using UnityEngine;

public class TM_Mesh : MonoBehaviour
{
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private int randomColor;
    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Unterschiedliche Farben
        Color[] coli = new Color[5];
        coli[0] = new Color(0.9f, 0.1f, 0.4f, 0);
        coli[1] = new Color(0.1f, 0.4f, 0.9f, 0);
        coli[2] = new Color(0.2f, 0.9f, 0.1f, 0);
        coli[3] = new Color(0.9f, 0.9f, 0.1f, 0);
        coli[4] = new Color(0.9f, 0.1f, 0.0f, 0);

        //Meshes und Gameobjects werden in einer Schleife erzeugt
        for(int i = 0; i < 350; i++)
        {
            Mesh[] meshObject = new Mesh[i+1];

            GameObject[] starObject = new GameObject[i+1];

            meshObject[i] = new Mesh();
            starObject[i] = new GameObject("Star" + i);

            randomColor = (int)Random.Range(0, 3);


            //star = new GameObject("Star"+i);
            starObject[i].transform.Translate(Random.Range(-300, 400), 100, Random.Range(-400, 300));
            starObject[i].transform.Rotate(Random.Range(-90, 90), 0.0f, 0.0f, Space.Self);
            starObject[i].transform.parent = this.transform;

            starObject[i].AddComponent<MeshFilter>();
            starObject[i].AddComponent<MeshRenderer>();
            meshObject[i] = starObject[i].GetComponent<MeshFilter>().mesh;

            Renderer rend = starObject[i].GetComponent<Renderer>();
            rend.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            rend.material.SetColor("_EmissionColor", coli[randomColor]);
            rend.material.EnableKeyword("_EMISSION");

            VerticesGenerieren();
            TrianglesHinzufuegen();

            meshObject[i].Clear();

            meshObject[i].vertices = vertices.ToArray();
            meshObject[i].triangles = triangles.ToArray();

            meshObject[i].RecalculateNormals();

            vertices.Clear();
            triangles.Clear();
        }

    }

    void VerticesGenerieren()
    {
        //Vertices werden der Vertices Liste hinzugefügt

        //Zufällige Höhe und Breite
        float randomHoehe = Random.Range(0.25f, 1);
        float randomBreite = Random.Range(.1f, 1);
        vertices.Add(new Vector3(0, randomHoehe * (-1), 0));     //0
        vertices.Add(new Vector3(0, randomHoehe * 2, 0));     //1
        vertices.Add(new Vector3(randomBreite, randomHoehe / 2, randomBreite));     //2
        vertices.Add(new Vector3(randomBreite, randomHoehe / 2, -randomBreite));    //3
        vertices.Add(new Vector3(-randomBreite, randomHoehe / 2, randomBreite));     //4
        vertices.Add(new Vector3(-randomBreite, randomHoehe / 2, -randomBreite));   //5
    }

    void TrianglesHinzufuegen()
    {
        //Punkte werden der triangles Liste hinzugefügt

        //Unten
        triangles.Add(3 + counter);
        triangles.Add(2 + counter);
        triangles.Add(0 + counter);

        triangles.Add(2 + counter);
        triangles.Add(4 + counter);
        triangles.Add(0 + counter);

        triangles.Add(5 + counter);
        triangles.Add(3 + counter);
        triangles.Add(0 + counter);

        triangles.Add(0 + counter);
        triangles.Add(4 + counter);
        triangles.Add(5 + counter);


        //Oben
        triangles.Add(1 + counter);
        triangles.Add(2 + counter);
        triangles.Add(3 + counter);

        triangles.Add(1 + counter);
        triangles.Add(4 + counter);
        triangles.Add(2 + counter);

        triangles.Add(1 + counter);
        triangles.Add(3 + counter);
        triangles.Add(5 + counter);

        triangles.Add(5 + counter);
        triangles.Add(4 + counter);
        triangles.Add(1 + counter);
    }
}
