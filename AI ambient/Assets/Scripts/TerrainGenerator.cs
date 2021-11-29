using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int tamanhoDoCenarioX, tamanhoDoCenarioY;

    Ground[,] cenario;

    public Ground PlateModel;

    string[] tipos = { "grama", "terra", "agua" };

    public Material[] materiais;

    public static TerrainGenerator instance;

    public void Awake()
    {
        instance = FindObjectOfType<TerrainGenerator>();
    }

    void Start()
    {
        cenario = new Ground[tamanhoDoCenarioX, tamanhoDoCenarioY];

        Vector2 r = Vector2.zero;

        for (int x = 0 ; x < cenario.GetLength(0) ; x++)
        {
            for (int z = 0 ; z < cenario.GetLength(1) ; z++)
            {
                Ground g = Instantiate(PlateModel, Vector3.zero, Quaternion.identity);

                if (r.y == 0)
                {
                    switch (Random.Range(0, tipos.Length))
                    {
                        case 0:
                            r.x = 0;
                            r.y = 9;
                            break;
                        case 1:
                            r.x = 1;
                            r.y = 4;
                            break;
                        case 2:
                            r.x = 2;
                            r.y = 2;
                            break;
                    }
                }

                g.Construtor( gameObject.transform, tipos[(int)r.x], new Vector3(x, 0, z), materiais[(int)r.x]);

                r.y--;

            }
        }
    }

}
