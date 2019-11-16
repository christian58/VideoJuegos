using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impacto : MonoBehaviour
{

    public GameObject Player;
    public GameObject Acutal;

    private int Salud = 100;
    //int Vida = 3;
    float y1,y2;

    string namee;

    public int kills;

   

    private GameObject objectPlayer;

    Player player_aux;
    // Start is called before the first frame update
    void Start()
    {
        objectPlayer = GameObject.Find("FPSController");
        player_aux = objectPlayer.GetComponent<Player>();
        kills = 0;

        //Debug.Log("OBJECT NAME");
        //Debug.Log(gameObject.name);
        Salud = 100;
        //Vida = 3;
        namee = " ";
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("COLLIDEERRRRRRRRR");
        Debug.Log(gameObject.name);
        Debug.Log((string)collision.gameObject.name);
        if(collision.gameObject.name == "BalasF(Clone)")
        {
            //Debug.Log
            //gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
            //gameObject.renderer.material.color = new Color(1, 1, 1);

            Salud -= 70;
            //Debug.Log("BLAS FFFFFF");
            namee = gameObject.name;
        }
    }


    // Update is called once per frame
    void Update()
    {
        //player_aux.kills = 100000;
        //Debug.Log("SALUDDD   REAL");
        //Debug.Log(Salud);

        if (Salud <= 0)
        {
            if(namee.Length > 3)
            {
                kills++;
                //GetComponent<Player>().kills++;
                //myPlayer.kills++;
                //objectPlayer.GetComponent<Player>().kills = 10;
                player_aux.kills++;
                //Debug.Log("KILLLLL NAME");
                //Debug.Log(gameObject.name);
                Destroy(gameObject);
            }

        }

        //Debug.Log("POSICIOENS");
        //Debug.Log(Player.transform.position);
        //Debug.Log(gameObject.transform.position);

    }
    int getSalud()
    {

        return Salud;
    }

}
