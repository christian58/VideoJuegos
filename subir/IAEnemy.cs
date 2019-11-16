using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAEnemy : MonoBehaviour
{
    [HideInInspector]
    public InputStr Input;
    public struct InputStr
    {
        public float LookX;
        public float LookZ;
        public float RunX;
        public float RunZ;
        public bool Jump;

        //public bool SwitchToAK;
        //public bool SwitchToPistol;

        public bool Shoot;
        public Vector3 ShootTarget;
    }
    protected float Cooldown;
    //protected AudioSource AudioSourcePlayer;
    //protected ParticleSystem ParticleSystem;

    public int HP;
    public bool IsDead { get { return HP <= 0; } }
    [HideInInspector]
    public bool Debugg = false;
    public const int AllButIgnoreLayer = 0b11111011;




    public GameObject playerr;

    public GameObject punto1;


    public float lookRadius = 10f;
    Transform target;
    NavMeshAgent agent;

    Vector3 p1 = new Vector3(23.5f, -6.5f, 3.0f);

    private readonly Vector3[] positionMap = { new Vector3(23.5f, -6.5f, 3.0f), new Vector3(-13.0f, 0.0f, 51f),
    new Vector3(-18.0f, -1.5f, 8.0f), new Vector3(-66.5f, -1.5f, -8.0f), new Vector3(-59.6f, -0.8f, -2.5f),
        new Vector3(12.2f, -0.2f, 62.6f), new Vector3(15.1f, -1.7f, 26.9f), new Vector3(21.2f, -0.8f, 14.8f),

        new Vector3(-40.5f, -1.7f, -10.0f), new Vector3(-17.6f, -0.9f, -6.7f), new Vector3(-21.3f, -1.7f, -10.5f),
        new Vector3(24.4f, -6.5f, -28.1f), new Vector3(30.2f, -1.7f, -64.7f), new Vector3(-1.7f, -1.8f, -60.5f),
        new Vector3(-41.8f, -4.9f, -59.6f)};




    bool llegada = false;
    bool ruta = true;
    bool ataque = false;
    bool ataque_aux = false;
    bool flagg = true;


    private int pos;
    private int pos_aux;


    public static bool estadoo = false;



    private GameObject objectPlayer;
    Player player_aux;
    // Start is called before the first frame update
    //Input.Shoot = IAEnemy.estadoo;
    void Start()
    {
        
        objectPlayer = GameObject.Find("FPSController");
        player_aux = objectPlayer.GetComponent<Player>();


        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();

        HP = player_aux.HP;
        Debug.Log("INIO VIDAAA");
        Debug.Log(HP);

        pos_aux = pos = Random.Range(0, positionMap.Length - 1);
        //Debug.Log("POSICIONESS");
        //Debug.Log(pos);
        agent.SetDestination(positionMap[pos]);

    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(punto1.transform.position);
        //Vector3.Distance(positionMap[pos],transform.position) < 2
        Cooldown -= Time.deltaTime;
        if (Vector3.Distance(transform.position, playerr.transform.position) < 20)
        {
            FaceTarget();

            if (flagg)
            {

                BajarVida();
                StartCoroutine(Esperar());
            }


            //Debug.Log("BAJANDO VIDAAA");
            //Debug.Log(HP);
            //player_aux.Input.Shoot = true;
            estadoo = true;
            //Debug.Log("Disparo");

            ataque_aux = true;
            if (!ataque)
            {
                agent.SetDestination(transform.position);
                ataque = true;
            }

        }
        else
        {
            //player_aux.Input.Shoot = false;

            estadoo = false;
            ataque = false;
            //Debug.Log("CAMINOOO");
            if (ataque_aux)
            {
                pos = Random.Range(0, positionMap.Length - 1);
                while (pos == pos_aux) pos = Random.Range(0, positionMap.Length - 1);
                pos_aux = pos;
                agent.SetDestination(positionMap[pos]);
                ataque_aux = false;
            }
        }



        if (Vector3.Distance(positionMap[pos],transform.position) < 2 && !llegada)
        {

            llegada = true;

            pos = Random.Range(0, positionMap.Length - 1);
            while(pos == pos_aux) pos = Random.Range(0, positionMap.Length - 1);
            pos_aux = pos;
            agent.SetDestination(positionMap[pos]);
        }
        if (Vector3.Distance(positionMap[pos], transform.position) > 2 )
        {
            llegada = false;
        }


        /*
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }

        }
        */

    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

   

    void BajarVida()
    {
        int vida = 0;

        if (Cooldown <= 0)
        {
            //Debug.Log("Shoooottt");
            var shootVariation = UnityEngine.Random.insideUnitSphere;

            //Animator.SetTrigger("Shoot");
            /*
            if (Animator.GetBool("AK") == true)
            {
                //AudioSourcePlayer.PlayOneShot(AudioClipAK);
                Cooldown = 0.2f;
                shootVariation *= 0.02f;
            }
            else
            {
                //AudioSourcePlayer.PlayOneShot(AudioClipShot);
                Cooldown = 1f;
                shootVariation *= 0.01f;
            }
            */
            var shootOrigin = objectPlayer.transform.position + Vector3.up * 1.5f;
            var shootDirection = (Input.ShootTarget - shootOrigin).normalized;
            var shootRay = new Ray(shootOrigin, shootDirection + shootVariation);


            //do we hit anybody?
            var hitInfo = new RaycastHit();
            objectPlayer.layer = Physics.IgnoreRaycastLayer;
            //Debug.Log("GARRR");
            if (Physics.SphereCast(shootRay, 0.1f, out hitInfo, IAEnemy.AllButIgnoreLayer))
            {
                //Debug.Log("EDED");
                UnityEngine.Debug.DrawLine(shootRay.origin, hitInfo.point, Color.red);

                /*
                var player = hitInfo.collider.GetComponent<Player>();

                if (player != null)
                {
                    //Debug.Log("AAA");
                    
                    player.OnHit();
                }
                */
                player_aux.HP -= 2;

                HP = HP - 10;
            }
            objectPlayer.layer = 0;

        }





        //return vida;
    }

    private void OnHit()
    {
        //Debug.Log("FINNN");
        Debug.Log(HP);
        if (IsDead)
            return;

        HP = HP - 10;
        //ParticleSystem.Stop();
        //ParticleSystem.Play();

        if (IsDead)
            Die();
        //Activate Particles
    }
    private void Die()
    {
        //Activate Ragdoll Mode
        //GetComponent<PlayerRagdoll>().RadollSetActive(true);
    }

    IEnumerator Esperar()
    {
        flagg = false;
        Debug.Log("INIIII ESPERA");

        yield return new WaitForSeconds(1.5f);
        flagg = true;
        Debug.Log("END ESPERAA");

    }
}
