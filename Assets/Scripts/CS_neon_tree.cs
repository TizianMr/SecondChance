using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CS_neon_tree : MonoBehaviour
{
    // Vorinitalisierungen
    GameObject tubecollect, tube, tube_light;
    Light b_licht;
    Renderer tuberend;
    Mesh tubeface;
    Material mag;
    bool t_adder = true;
    int color_randy = 0;

    // Meshlisten
    List<int> tpoints = new List<int>();
    List<Vector3> tverts = new List<Vector3>();
    List<Vector3> tnormals = new List<Vector3>();
    List<Vector2> tuvs = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {

        // GO erstellen und an Skript vererben
        tube = new GameObject("Neon_Tube");
        tube.transform.parent = this.transform;

        // Bäume auf Bürgersteige setzen
        tree_planter();

        // ganzes Script-Gameobject auf 0-Position verschieben
        this.transform.position = new Vector3(42, 1.3f, -105.99f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void tree_sprayer(int groesse, int ic, float min_bx, float max_bx, float min_bz, float max_bz)
    {
        // TubeMesh erstellen
        tube_generator();

        GameObject[] tree_pl = new GameObject[groesse];

        // Bäume zusammenbauen und verteilen
        for (int i = 1; i < groesse; i++)
        {
            tree_pl[i] = new GameObject("tree_" + i+ic);
            tree_pl[i].transform.parent = this.transform;

            create_tree(Random.Range(2, 14), Random.Range(4, 9), -10, 10).transform.parent = tree_pl[i].transform;


            if (i == 1 && ic == 0)
            {
                tree_pl[i].transform.localScale = Quaternion.identity * new Vector3(0.01f, 0.01f, 0.01f);
            }

        }
        Destroy(tree_pl[1]);
        tree_pl[2].transform.position = new Vector3(Random.Range(min_bx, max_bx), 0.3f, Random.Range(min_bz, max_bz));
    }


    void tube_generator()
    {

        // GO verschieben
        tube.transform.position = new Vector3(0,0,0);

        // GO Meshfilter
        if (t_adder == true) {
            tube.AddComponent<MeshFilter>();
            t_adder = false;
        }
        tube.GetComponent<MeshFilter>().mesh = tubeface;
        tubeface = tube.GetComponent<MeshFilter>().mesh;

        // Mesh vor Erstellung clearen
        tubeface.Clear();

        // Mesh erzeugen
        tubeMeshGenerator();

        // Normals neu berechnen
        tubeface.RecalculateNormals();

    }

    Vector3 getNormal(Vector3 a, Vector3 b, Vector3 c)
    {
        Vector3 side1 = new Vector3(b.x, b.y, b.z);
        Vector3 side2 = new Vector3(c.x, c.y, c.z);
        side1 = b - a;
        side2 = c - a;

        return Vector3.Cross(side1, side2).normalized;
    }

    GameObject create_tree(int zweig, int ast, int r_min, int r_max)
    {
        GameObject tree = new GameObject();
        tube.transform.parent = tree.transform;
        tree.transform.parent = this.transform;
        tree.transform.position = new Vector3(0, 0, 0);
        tree.name = "tree";

        GameObject[,] ast_go = new GameObject[zweig,ast];
        Renderer[,] rend_go = new Renderer[zweig, ast];

        Color[] coli = new Color[5];
        coli[0] = new Color(0.9f, 0.1f, 0.4f, 0);
        coli[1] = new Color(0.1f, 0.4f, 0.9f, 0);
        coli[2] = new Color(0.2f, 0.9f, 0.1f, 0);
        coli[3] = new Color(0.9f, 0.9f, 0.1f, 0);
        coli[4] = new Color(0.9f, 0.1f, 0.0f, 0);

        color_randy = (int)Random.Range(0, 3);
        // Höhe
        for (int j = 0; j < zweig; j++)
        {
            // Äste
            for (int i = 0; i < ast; i++)
            {
                //
                if(i == 0)
                {
                    ast_go[j, i] = Instantiate(tube, tube.transform.position, Quaternion.Euler(0, 0, 0), tree.transform) as GameObject;
                    ast_go[j, i].AddComponent<MeshRenderer>();

                    rend_go[j, i] = ast_go[j, i].GetComponent<Renderer>();
                    rend_go[j, i].material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                    rend_go[j, i].material.SetColor("_EmissionColor", coli[color_randy]);
                    rend_go[j, i].material.EnableKeyword("_EMISSION");

                    color_randy = (int)Random.Range(0f, 3.99f);

                    if(j == 0) {
                        GameObject t_light = new GameObject("The Light");
                        b_licht = t_light.AddComponent<Light>();
                        t_light.transform.parent = ast_go[j, i].transform;
                        t_light.transform.position = ast_go[j, i].transform.position + new Vector3(0, 2, 0);
                        b_licht.range = 100;
                        b_licht.intensity = 0.1f;
                    }
                }
                else
                {
                    ast_go[j, i] = Instantiate(tube, tube.transform.position, Quaternion.Euler(50, 360 / (ast-1) * (i-1), 0), tree.transform) as GameObject;
                    ast_go[j, i].AddComponent<MeshRenderer>();

                    rend_go[j, i] = ast_go[j, i].GetComponent<Renderer>();
                    rend_go[j, i].material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                    rend_go[j, i].material.SetColor("_EmissionColor", coli[color_randy]);
                    rend_go[j, i].material.EnableKeyword("_EMISSION");

                    color_randy = (int)Random.Range(0f, 4.99f);
                }

                ast_go[j,i].name = "ast" + j + i;

                if (j > 0)
                {
                    ast_go[j, i].transform.position = ast_go[j - 1, i].transform.position + new Vector3(0, 0.5f, 0);
                }
                else
                {
                    ast_go[j, i].transform.position = tube.transform.position + new Vector3(0, 0.5f, 0);
                }

                ast_go[j,i].transform.localScale = tube.transform.localScale;
            }

        }

        float rand_x = Random.Range(r_min, r_max);
        float rand_y = Random.Range(r_min, r_max);

        rand_x = Random.Range(-10, 10);
        rand_y = Random.Range(-10, 10);

        return tree;

    }

    void tubeMeshGenerator()
    {
        // Verts definieren
        Vector3 b0 = new Vector3(0, 0, 0);
        Vector3 b1 = new Vector3(2, 0, 4);
        Vector3 b2 = new Vector3(4, 0, 2);
        Vector3 b3 = new Vector3(4, 0, -2);
        Vector3 b4 = new Vector3(2, 0, -4);
        Vector3 b5 = new Vector3(-2, 0, -4);
        Vector3 b6 = new Vector3(-4, 0, -2);
        Vector3 b7 = new Vector3(-4, 0, 2);
        Vector3 b8 = new Vector3(-2, 0, 4);

        Vector3 t0 = new Vector3(0, 80, 0);
        Vector3 t1 = new Vector3(2, 80, 4);
        Vector3 t2 = new Vector3(4, 80, 2);
        Vector3 t3 = new Vector3(4, 80, -2);
        Vector3 t4 = new Vector3(2, 80, -4);
        Vector3 t5 = new Vector3(-2, 80, -4);
        Vector3 t6 = new Vector3(-4, 80, -2);
        Vector3 t7 = new Vector3(-4, 80, 2);
        Vector3 t8 = new Vector3(-2, 80, 4);

        // Verts Array hinzufügen
        tverts.Add(b0);
        tverts.Add(b1);
        tverts.Add(b2);
        tverts.Add(b3);
        tverts.Add(b4);
        tverts.Add(b5);
        tverts.Add(b6);
        tverts.Add(b7);
        tverts.Add(b8);

        tverts.Add(t0);
        tverts.Add(t1);
        tverts.Add(t2);
        tverts.Add(t3);
        tverts.Add(t4);
        tverts.Add(t5);
        tverts.Add(t6);
        tverts.Add(t7);
        tverts.Add(t8);

        // Triangles aus Verts erzeugen

        // BOTTOM ------------------------------

        // B-N
        tpoints.Add(0);
        tpoints.Add(1);
        tpoints.Add(8);

        // B-NO
        tpoints.Add(0);
        tpoints.Add(2);
        tpoints.Add(1);

        // B-O
        tpoints.Add(0);
        tpoints.Add(3);
        tpoints.Add(2);

        // B-SO
        tpoints.Add(0);
        tpoints.Add(4);
        tpoints.Add(3);

        // B-S
        tpoints.Add(0);
        tpoints.Add(5);
        tpoints.Add(4);

        // B-SW
        tpoints.Add(0);
        tpoints.Add(6);
        tpoints.Add(5);

        // B-W
        tpoints.Add(0);
        tpoints.Add(7);
        tpoints.Add(6);

        // B-NW
        tpoints.Add(0);
        tpoints.Add(8);
        tpoints.Add(7);

        // TOP ------------------------------

        // T-N
        tpoints.Add(9);
        tpoints.Add(17);
        tpoints.Add(10);

        // T-NO
        tpoints.Add(9);
        tpoints.Add(10);
        tpoints.Add(11);

        // T-O
        tpoints.Add(9);
        tpoints.Add(11);
        tpoints.Add(12);

        // T-SO
        tpoints.Add(9);
        tpoints.Add(12);
        tpoints.Add(13);

        // T-S
        tpoints.Add(9);
        tpoints.Add(13);
        tpoints.Add(14);

        // T-SW
        tpoints.Add(9);
        tpoints.Add(14);
        tpoints.Add(15);

        // T-W
        tpoints.Add(9);
        tpoints.Add(15);
        tpoints.Add(16);

        // T-NW
        tpoints.Add(9);
        tpoints.Add(16);
        tpoints.Add(17);

        // SEITEN ------------------------------

        // S-N
        tpoints.Add(8);
        tpoints.Add(10);
        tpoints.Add(17);

        tpoints.Add(8);
        tpoints.Add(1);
        tpoints.Add(10);

        // S-NO
        tpoints.Add(10);
        tpoints.Add(1);
        tpoints.Add(11);

        tpoints.Add(1);
        tpoints.Add(2);
        tpoints.Add(11);

        // S-O
        tpoints.Add(11);
        tpoints.Add(2);
        tpoints.Add(12);

        tpoints.Add(2);
        tpoints.Add(3);
        tpoints.Add(12);

        // T-SO
        tpoints.Add(12);
        tpoints.Add(3);
        tpoints.Add(13);

        tpoints.Add(3);
        tpoints.Add(4);
        tpoints.Add(13);

        // T-S
        tpoints.Add(13);
        tpoints.Add(4);
        tpoints.Add(14);

        tpoints.Add(4);
        tpoints.Add(5);
        tpoints.Add(14);

        // T-SW
        tpoints.Add(14);
        tpoints.Add(5);
        tpoints.Add(15);

        tpoints.Add(5);
        tpoints.Add(6);
        tpoints.Add(15);

        // T-W
        tpoints.Add(15);
        tpoints.Add(6);
        tpoints.Add(16);

        tpoints.Add(6);
        tpoints.Add(7);
        tpoints.Add(16);

        // T-NW
        tpoints.Add(16);
        tpoints.Add(7);
        tpoints.Add(17);

        tpoints.Add(7);
        tpoints.Add(8);
        tpoints.Add(17);

        // Punkte dem Mesh übergeben
        tubeface.vertices = tverts.ToArray();
        tubeface.triangles = tpoints.ToArray();
        tubeface.normals = tnormals.ToArray();
        tubeface.uv = tuvs.ToArray();
    }


    void tree_planter()
    {
        // -----------
        // Bäume erstellen und verteilen
        // -----------

        // Bürgersteig 1
        for (int i = 0; i < 6; i++)
        {
            tree_sprayer(3, i, 1.63f, 0f, -7.11f, 12.51f);

        }
        // Bürgersteig 2
        for (int i = 6; i < 12; i++)
        {
            tree_sprayer(3, i, -12.22f, -15.34f, -7.11f, 12.51f);
        }
        // Bürgersteig 3
        for (int i = 12; i < 18; i++)
        {
            tree_sprayer(3, i, 1.63f, 0f, 32f, 55f);

        }
        // Bürgersteig 4
        for (int i = 18; i < 24; i++)
        {
            tree_sprayer(3, i, -12.22f, -15.34f, 32f, 53f);
        }
        // Bürgersteig 5
        for (int i = 12; i < 18; i++)
        {
            tree_sprayer(3, i, 1.63f, 0f, 93f, 113f);

        }
        // Bürgersteig 6
        for (int i = 18; i < 24; i++)
        {
            tree_sprayer(3, i, -12.22f, -15.34f, 93f, 113f);
        }

        // ----
        // ----

        // Bürgersteig 7
        for (int i = 24; i < 30; i++)
        {
            tree_sprayer(3, i, -65f, -22.5f, 56.5f, 58.38f);
        }
        // Bürgersteig 8
        for (int i = 30; i < 36; i++)
        {
            tree_sprayer(3, i, -65f, -22.5f, 68f, 71f);
        }

        // ----
        // ----

        // Parkhaus
        for (int i = 36; i < 42; i++)
        {
            tree_sprayer(3, i, 19f, 32f, -15f, -9f);
        }

    }


}

