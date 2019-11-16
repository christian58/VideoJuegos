using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject objectEnemy;

    
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = objectEnemy.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        if(rb.isKinematic)
        {
            //Debug.Log("KINEMATIC TRUE");
        }
        else
        {
            Destroy(gameObject);
            //Debug.Log("Kienmanit false");
        }
    }
}
