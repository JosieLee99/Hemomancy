using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveWallNode : MonoBehaviour
{
    public GameObject rigidbodyGO;
    public void OncollisionStay2D(Collider2D collider)
    {
        if(collider.tag == "WallCollider")
        {
            Destroy(collider.gameObject);
        }
    }
}
