using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAnimation : MonoBehaviour
{
    public void DestryParent()
    {
        GameObject parent = gameObject.transform.parent.gameObject;
        Destroy(parent);
        
    }

}
