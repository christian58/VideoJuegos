using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("BALASSSSS  BLASSSS");
        Debug.Log((string)collision.gameObject.name);
        /*if (collision.gameObject.name == "BalasF(Clone)")
        {
            Debug.Log("BLAS FFFFFF");
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
