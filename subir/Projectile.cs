using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody bulletPrefabs;
    public Transform shootPoint;
    public LayerMask layer;

    public GameObject personaje;

    public Vector3 pos;

    public int destroyTime = 2;

    bool Disparar = true;
    bool flagg = true;
    int contt = 0;
    // Use this for initialization
    void Start()
    {
        Disparar = true;
        pos = new Vector3(10f,0f,10f);
    }

    // Update is called once per frame
    void Update()
    {
        //yield return new WaitForSeconds(15f);
        LaunchProjectile();


}

    void LaunchProjectile()
    {
        //Vector3 V0 = CalculateVelocity(personaje.transform.position, shootPoint.position, 1f);
        Vector3 V0 = CalculateVelocity(personaje.transform.position, shootPoint.position, 1f);

        V0 = V0 * 4;


        //transform.rotation = Quaternion.LookRotation(V0);

        //if (Input.GetMouseButtonDown(0))
        //{
        //yield return new WaitForSeconds(1f);


        if (Input.GetMouseButtonDown(0) && flagg)
        {
            Debug.Log("DISPARARRRR");
            flagg = false;
            Disparar = false;
            Rigidbody obj = Instantiate(bulletPrefabs, shootPoint.position, Quaternion.identity);
            obj.velocity = V0;

            Destroy(obj.gameObject, destroyTime);
            StartCoroutine(Esperar());
        }
            





        /*
        int r = Random.Range(1, 3000);
        if (r < 10)
        {
            //yield return new WaitForSeconds(1f);
            Rigidbody obj = Instantiate(bulletPrefabs, shootPoint.position, Quaternion.identity);
            obj.velocity = V0;
        }
        */
        //}
    }

   



    Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        //define the distance x and y first
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0f;

        //create a float - represent our distance
        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }


    IEnumerator Esperar()
    {
        flagg = false;
        Debug.Log("INIIII ESPERA");
  
          yield return new WaitForSeconds(0.5f);
        flagg = true;
        Debug.Log("END ESPERAA");
        
    }

}