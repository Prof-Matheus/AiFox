using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniffer : MonoBehaviour
{
    public static List<Transform> bunny = new List<Transform>();
    public static List<Transform> grass = new List<Transform>();
    public enum Type { fox, bunny, grass };

    public float rangeSnif = 3f;

    public float refresh = 1f;

    public Type meuTipo;

    private void Start()
    {
        if (meuTipo == Type.bunny)
        {
            bunny.Add(transform);
        }
        else if (meuTipo == Type.grass)
        {
            grass.Add(transform);
        }
    }

    void Update()
    {
        refresh -= Time.deltaTime;
        if (refresh<0)
        {
            refresh = 1;
            
            switch (meuTipo)
            {
                case Type.fox:
                        foreach (Transform r in bunny)
                        {
                            if ( Vector3.Distance(transform.position, r.position) < rangeSnif)
                            {
                                transform.GetComponent<Movimento>().food = r;
                                                                                                                                                                            transform.GetComponent<Movimento>().modo = "come";
                            }
                        }
                    break;
                case Type.bunny:
                    foreach (Transform g in grass)
                    {
                        if (Vector3.Distance(transform.position, g.position) < rangeSnif)
                        {
                            transform.GetComponent<Movimento>().food = g;
                            transform.GetComponent<Movimento>().modo = "come";
                        }
                    }
                    break;
                case Type.grass:
                    this.enabled = false;
                    break;
            }

        }
    }
}
