using System.Collections.Generic;
using UnityEngine;

public class SS_SchilderMesh : MonoBehaviour
{
    //Anzahl an generierten Schildern
    public int Anzahl = 20;

    //Arrays mit Objekt und Mesh Sammlung
    GameObject[] schildSammlung;
    Mesh[] schildMeshSammlung;

    //Farbe
    public Color[] coli = new Color[5];
    int randomColor = 0;

    //Listen für Mesh
    List<Vector3> vertices = new List<Vector3>();
    List<int> faces = new List<int>();
    List<Vector3> normals = new List<Vector3>();

    //Maße der Schilder
    float minHoehe = 0.5f; float maxHoehe = 3.0f;
    float minBreite = 0.5f; float maxBreite = 3.0f;
    float randomHoehe; float randomBreite; float tiefe = 0.3f;

    //Vektoren für Rahmen Verticies
    Vector3 untenLinks = new Vector3(0.05f, 0.05f, 0);
    Vector3 obenLinks = new Vector3(0.05f, -0.05f, 0);
    Vector3 untenRechts = new Vector3(-0.05f, 0.05f, 0);
    Vector3 obenRechts = new Vector3(-0.05f, -0.05f, 0);

    void Start()
    {   
        //Liste an Farben
        coli[0] = new Color(0.9f, 0.1f, 0.4f, 0);
        coli[1] = new Color(0.1f, 0.4f, 0.9f, 0);
        coli[2] = new Color(1.0f, 0, 0, 1.0f);
        coli[3] = new Color(1.0f, 0, 0.62f, 1.0f);
        coli[4] = new Color(0, 0.84f, 1.0f, 1.0f);

        SchildGenerieren(Anzahl);
    }

    void SchildGenerieren(int Anzahl)
    {
        for (int i = 0; i < Anzahl; i++)
        {
            //Random generierte Werte
            randomColor = (int)Random.Range(0, 5);
            Vector3 randomPosition = new Vector3 (Random.Range(-10, 10), Random.Range(5, 15), Random.Range(-50, 50));

            schildMeshSammlung = new Mesh[i+1];
            schildSammlung = new GameObject[i+1];

            schildMeshSammlung[i] = new Mesh();
            schildSammlung[i] = new GameObject("Neon_Schild" + i);

            schildSammlung[i].transform.Translate(this.transform.position + randomPosition);
            schildSammlung[i].transform.parent = this.transform;

            //Meshfilter
            schildSammlung[i].AddComponent<MeshFilter>();
            schildSammlung[i].AddComponent<MeshRenderer>();
            schildMeshSammlung[i] = schildSammlung[i].GetComponent<MeshFilter>().mesh;

            //Mesh zu Beginn clearen
            schildMeshSammlung[i].Clear();

            VerticiesGenerator();
            MeshGenerator();

            //Punkte und Flächen dem Mesh übergeben
            schildMeshSammlung[i].vertices = vertices.ToArray();
            schildMeshSammlung[i].triangles = faces.ToArray();

            schildMeshSammlung[i].RecalculateNormals();
            
            //Größen clearen um neu zu generieren
            vertices.Clear();
        	faces.Clear();

            Renderer rend = schildSammlung[i].GetComponent<Renderer>();
            rend.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            rend.material.SetColor("_EmissionColor", coli[randomColor]);
            rend.material.EnableKeyword("_EMISSION");            
        }
        
    }

    void VerticiesGenerator()
    {
        //Random Werte generieren
        randomHoehe = Random.Range(minHoehe, maxHoehe);
        randomBreite = Random.Range(minBreite, maxBreite);
        //Debug.Log("Höhe: " + randomHoehe + " Breite: " + randomBreite);

        //Vertices Bottom--------------------------------
        Vector3 a = new Vector3(0, 0, 0);
        Vector3 b = new Vector3(0 + randomBreite, 0, 0);
        Vector3 c = new Vector3(0, 0, 0 - tiefe);
        Vector3 d = new Vector3(0 + randomBreite, 0, 0 - tiefe);

        //Vertices Top
        Vector3 e = new Vector3(0, randomHoehe, 0);
        Vector3 f = new Vector3(0 + randomBreite, randomHoehe, 0);
        Vector3 g = new Vector3(0, randomHoehe, 0 - tiefe);
        Vector3 h = new Vector3(0 + randomBreite, randomHoehe, 0 - tiefe);

        //Vertices Front
        Vector3 i = g + obenLinks;
        Vector3 j = c + untenLinks;
        Vector3 k = h + obenRechts;
        Vector3 l = d + untenRechts;

        //Vertices Back
        Vector3 m = e + obenLinks;
        Vector3 n = a + untenLinks;
        Vector3 o = f + obenRechts;
        Vector3 p = b + untenRechts;

        //Vertices hinzufügen-----------------------------
        vertices.Add(a); vertices.Add(b); vertices.Add(c); vertices.Add(d);
        vertices.Add(e); vertices.Add(f); vertices.Add(g); vertices.Add(h);
        vertices.Add(i); vertices.Add(j); vertices.Add(k); vertices.Add(l);
        vertices.Add(m); vertices.Add(n); vertices.Add(o); vertices.Add(p);
    }

    void MeshGenerator()
    {   
        /*Faces Front
        faces.Add(6); faces.Add(3); faces.Add(2);
        faces.Add(6); faces.Add(7); faces.Add(3);
        
        //Faces Back
        faces.Add(5); faces.Add(0); faces.Add(1);
        faces.Add(5); faces.Add(4); faces.Add(0);*/
        
        //Faces North-------------------------------------
        faces.Add(4); faces.Add(5); faces.Add(6);
        faces.Add(5); faces.Add(7); faces.Add(6);

        //Faces South
        faces.Add(0); faces.Add(2); faces.Add(1);
        faces.Add(1); faces.Add(2); faces.Add(3);

        //Faces West
        faces.Add(4); faces.Add(2); faces.Add(0);
        faces.Add(4); faces.Add(6); faces.Add(2);

        //Faces East
        faces.Add(7); faces.Add(1); faces.Add(3);
        faces.Add(7); faces.Add(5); faces.Add(1);

        //Westfront---------------------------------------
        faces.Add(8); faces.Add(2); faces.Add(6);
        faces.Add(8); faces.Add(9); faces.Add(2);

        //Northfront
        faces.Add(7); faces.Add(8); faces.Add(6);
        faces.Add(7); faces.Add(10); faces.Add(8);

        //Eastfront
        faces.Add(3); faces.Add(10); faces.Add(7);
        faces.Add(3); faces.Add(11); faces.Add(10);

        //Southfront
        faces.Add(2); faces.Add(11); faces.Add(3);
        faces.Add(2); faces.Add(9); faces.Add(11);

        //Westback-----------------------------------------
        faces.Add(0); faces.Add(12); faces.Add(4);
        faces.Add(0); faces.Add(13); faces.Add(12);

        //Northback
        faces.Add(4); faces.Add(14); faces.Add(5);
        faces.Add(4); faces.Add(12); faces.Add(14);

        //Eastback
        faces.Add(15); faces.Add(5); faces.Add(14);
        faces.Add(15); faces.Add(1); faces.Add(5);

        //Southback
        faces.Add(13); faces.Add(1); faces.Add(15);
        faces.Add(13); faces.Add(0); faces.Add(1);

        //Westinside----------------------------------------
        faces.Add(13); faces.Add(8); faces.Add(12);
        faces.Add(13); faces.Add(9); faces.Add(8);

        //Northinside
        faces.Add(12); faces.Add(10); faces.Add(14);
        faces.Add(12); faces.Add(8); faces.Add(10);

        //Eastinside
        faces.Add(15); faces.Add(10); faces.Add(11);
        faces.Add(15); faces.Add(14); faces.Add(10);

        //Southinside
        faces.Add(9); faces.Add(15); faces.Add(11);
        faces.Add(9); faces.Add(13); faces.Add(15);
    }

    void OnDrawGizmosSelected()
    {
        //Gizmo zeigt Bereich in dem Schilder generiert werden an
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(17, 10, 100));
    }
}
