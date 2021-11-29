using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    public GameObject       self        ;
    public Transform        me          ;

    public string           tipo        ;

    public Renderer         render      ;

    public void Construtor( Transform d, string t, Vector3 p, Material m )
    {
        tipo            = t;
        render.material = m;
        me.position     = p;

        me.parent       = d;
        self.name       = "obj_Tile_"+t;
    }

}
