using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Movimento é responsavel por mover o AGENTE pelo mapa
/// Ele determina as interações com base no <see cref="modo"/>
/// </summary>
public class Movimento : MonoBehaviour
{
    /// <summary>
    /// MODOS:  olha - Anda aleatóriamente sorteando valores.
    ///         come - persegue a comida a partir de uma direção.
    ///         fuga - NÃO IMPLEMENTADO
    /// </summary>
    public string modo = "olha";
    /// <summary>
    /// Transforme do agente
    /// </summary>
    public Transform me;
    // Transforme da comida alvo
    public Transform food;
    /// <summary>
    /// Transforme dos raycasts, sistema de rastreamento do chão
    /// </summary>
    public Transform f, b, l, r;
    /// <summary>
    /// Contador de tempo, intervalo entre passos
    /// </summary>
    float timer = 0f;
    /// <summary>
    /// tempo entre cada passo do intervalo <seealso cref="timer"/>
    /// </summary>
    public float speed = 2;
    /// <summary>
    /// direção do movimento determinada a partir da mecanica do <see cref="modo"/>
    /// </summary>
    public Vector3 direcao;
    /// <summary>
    /// distancia da comida em relação a sua posição
    /// </summary>
    public Vector2 foodDistance;
    /// <summary>
    /// saida do raycast, guarda as informações da direção onde o objeto vai se mover.
    /// </summary>
    RaycastHit hit;
    /// <summary>
    /// INICIALIZADOR
    /// </summary>
    private void Start()
    {
        //Seta posição inicial
        direcao.x = Random.Range(0, TerrainGenerator.instance.tamanhoDoCenarioX);
        direcao.z = Random.Range(0, TerrainGenerator.instance.tamanhoDoCenarioY);
        //Usa a direction para carregar a posição inicial
        me.position = direcao;
    }
    /// <summary>
    /// Atualizações do sistema de movimentação e comportamento do agente.
    /// </summary>
    private void FixedUpdate()
    {
        // controla o tempo entre cada passo d agente, levando em consideração o MODO
        if (timer < 0)
        {
            // reset do Raycast hit
            hit = new RaycastHit();
            // reset da direção
            direcao = Vector3.zero;
            // - - - - - - - - - - - - - - - - - - - - -
            // Configurações do MODO de ação OLHA
            // - - - - - - - - - - - - - - - - - - - - -
            if (modo == "olha")
                switch (Random.RandomRange(0, 5))   //  aleatorisa 5 ações
                {
                    case 0://anda para frente
                        if (Physics.Raycast(f.position, f.TransformDirection(Vector3.forward), out hit, 2f))
                        {
                            if (hit.collider.gameObject.GetComponent<Ground>().tipo != "agua")
                            {
                                direcao.z = 1;
                                Debug.Log(hit.collider.gameObject.GetComponent<Ground>().tipo + " - " + direcao);
                            }
                        }
                        break;
                    case 1://anda para traz
                        if (Physics.Raycast(b.position, b.TransformDirection(Vector3.forward), out hit, 2f))
                        {
                            if (hit.collider.gameObject.GetComponent<Ground>().tipo != "agua")
                            {
                                direcao.z = -1;
                                Debug.Log(hit.collider.gameObject.GetComponent<Ground>().tipo + " - " + direcao);
                            }
                        }
                        break;
                    case 2://anda para direita
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
                    case 3://anda para esquerda
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
                        // opção invalida, não afetam o comportamento
                }
            // - - - - - - - - - - - - - - - - - - - - -
            // Configurações do MODO de ação COME
            // - - - - - - - - - - - - - - - - - - - - -
            if ((modo == "come") && (food != null))
            {
                //calcula a distancia em unitys a partir da posição do agente 
                foodDistance = new Vector2(Mathf.Abs(me.position.x - food.position.x), Mathf.Abs(me.position.z + food.position.z));
                //determina se sera feita uma compensesão em horizontal ou vertical para atingir a posição do alvo.
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
            //cobra o tempo da ação 
                if (direcao != Vector3.zero) timer = speed;
                // aplica o movimento 
                me.Translate(direcao);

            }
            else
            {
            //desconta o tempo decorrido para rodar a ação.
                timer -= Time.deltaTime;
            }
            // - - - - - - - - - - - - - - - - - - - - -
    }

}
