using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    /// <summary>
    /// MODOS:  olha - anda aleatóriamente 
    ///         come
    ///         fuga
    /// </summary>
    public string modo = "olha";

    public Transform me;

    public Transform food;

    public Transform f, b, l, r;

    float timer = 0f;

    public float speed = 2;

    public Vector3 direcao;

    public Vector2 foodDistance;

    RaycastHit hit;

    private void Start()
    {
        direcao.x = Random.Range(0, TerrainGenerator.instance.tamanhoDoCenarioX);
        direcao.z = Random.Range(0, TerrainGenerator.instance.tamanhoDoCenarioY);

        me.position = direcao;
    }

    private void FixedUpdate()
    {

        if (timer < 0)
        {
            hit = new RaycastHit();

            direcao = Vector3.zero;

            if (modo == "olha")
                switch (Random.RandomRange(0, 5))
                {
                    case 0:
                        if (Physics.Raycast(f.position, f.TransformDirection(Vector3.forward), out hit, 2f))
                        {
                            if (hit.collider.gameObject.GetComponent<Ground>().tipo != "agua")
                            {
                                direcao.z = 1;
                                Debug.Log(hit.collider.gameObject.GetComponent<Ground>().tipo + " - " + direcao);
                            }
                        }
                        break;
                    case 1:
                        if (Physics.Raycast(b.position, b.TransformDirection(Vector3.forward), out hit, 2f))
                        {
                            if (hit.collider.gameObject.GetComponent<Ground>().tipo != "agua")
                            {
                                direcao.z = -1;
                                Debug.Log(hit.collider.gameObject.GetComponent<Ground>().tipo + " - " + direcao);
                            }
                        }
                        break;
                    case 2:
                        if (Physics.Raycast(r.position, r.TransformDirection(Vector3.forward), out hit, 2f))
                        {
                            if (hit.collider.gameObject.GetComponent<Ground>().tipo != "agua")
                            {
                                Debug.Log("R-hit_come");
                                direcao.x = 1;
                                Debug.Log(hit.collider.gameObject.GetComponent<Ground>().tipo + " - " + direcao);
                            }
                        }
                        break;
                    case 3:
                        if (Physics.Raycast(l.position, l.TransformDirection(Vector3.forward), out hit, 2f))
                        {
                            if (hit.collider.gameObject.GetComponent<Ground>().tipo != "agua")
                            {
                                Debug.Log("L-hit_come");
                                direcao.x = -1;
                                Debug.Log(hit.collider.gameObject.GetComponent<Ground>().tipo + " - " + direcao);
                            }
                        }
                        break;

                }

            if ((modo == "come") && (food != null))
            {
                foodDistance = new Vector2(Mathf.Abs(me.position.x - food.position.x), Mathf.Abs(me.position.z + food.position.z));

                if (Random.Range(0, 2) == 0)
                {
                    Debug.Log("Horizontal");
                    //horizontal +
                    if ((me.position.x + .5f < food.position.x) && (Physics.Raycast(r.position, r.TransformDirection(Vector3.forward), out hit, 2f)))
                    {
                        if (hit.collider.gameObject.GetComponent<Ground>().tipo != "agua")
                        {
                            direcao.x = 1;
                        }
                    }
                    //horizontal -
                    if ((me.position.x - .5f > food.position.x) && (Physics.Raycast(r.position, r.TransformDirection(Vector3.forward), out hit, 2f)))
                    {
                        if (hit.collider.gameObject.GetComponent<Ground>().tipo != "agua")
                        {
                            direcao.x = -1;

                        }
                    }
                }
                else
                {
                    Debug.Log("Vertical");
                    //vertical +
                    if ((me.position.z + .5f < food.position.z) && (Physics.Raycast(f.position, f.TransformDirection(Vector3.forward), out hit, 2f)))
                    {
                        if (hit.collider.gameObject.GetComponent<Ground>().tipo != "agua")
                        {
                            direcao.z = 1;
                        }
                    }

                    //vertical -
                    if ((me.position.z - .5f > food.position.z) && (Physics.Raycast(b.position, b.TransformDirection(Vector3.forward), out hit, 2f)))
                    {
                        if (hit.collider.gameObject.GetComponent<Ground>().tipo != "agua")
                        {
                            direcao.z = -1;
                        }
                    }
                }
                }

                if (direcao != Vector3.zero) timer = speed;

                me.Translate(direcao);

            }
            else
            {
                timer -= Time.deltaTime;
            }
        }

    }
