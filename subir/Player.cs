using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using UnityEngine.UI;


public class Player : MonoBehaviour
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

    public float Speed = 10f;
    public const float JumpForce = 7f;

    protected Rigidbody Rigidbody;
    protected Quaternion LookRotation;

    //public GameObject AKBack;
    //public GameObject AKHand;
    //public GameObject PistolBack;
    //public GameObject PistolHand;

    //public AudioClip AudioClipAK;
    //public AudioClip AudioClipShot;

    //protected Animator Animator;
    protected float Cooldown;
    //protected AudioSource AudioSourcePlayer;
    //protected ParticleSystem ParticleSystem;

    public int HP;
    public bool IsDead { get { return HP <= 0; } }
    [HideInInspector]
    public bool Debugg = false;
    public const int AllButIgnoreLayer = 0b11111011;


    public int kills = 0;

    private void Awake()
    {
        kills = 0;

        HP = 100;
        //Rigidbody = GetComponent<Rigidbody>();
        //Animator = GetComponent<Animator>();
        //AudioSourcePlayer = GetComponent<AudioSource>();
        //ParticleSystem = GetComponentInChildren<ParticleSystem>();
        //AKBack.SetActive(true);
        //AKHand.SetActive(false);
        //PistolBack.SetActive(true);
        //PistolHand.SetActive(false);
        //GetComponent<PlayerRagdoll>().RadollSetActive(false);

        //Input.Shoot = EnemyPatrulla.estadoo;
        Input.Shoot = IAEnemy.estadoo;
    }


    void FixedUpdate()
    {

        if (IsDead)
            return;

        var inputRun = Vector3.ClampMagnitude(new Vector3(Input.RunX, 0, Input.RunZ), 1);
        var inputLook = Vector3.ClampMagnitude(new Vector3(Input.LookX, 0, Input.LookZ), 1);

        //Rigidbody.velocity = new Vector3(inputRun.x * Speed, Rigidbody.velocity.y, inputRun.z * Speed);

        //rotation to go target
        if (inputLook.magnitude > 0.01f)
            LookRotation = Quaternion.AngleAxis(Vector3.SignedAngle(Vector3.forward, inputLook, Vector3.up), Vector3.up);

        //transform.rotation = LookRotation;
    }

    void Update()
    {
        Debug.Log("KILSSSS");
        Debug.Log(kills);
        //Input.Shoot = EnemyPatrulla.estadoo;

        //Input.Shoot = IAEnemy.estadoo;

        Debug.Log("ESTADOOOOOOOOO");
        Debug.Log(Input.Shoot);

        if (IsDead)
            return;

        Cooldown -= Time.deltaTime;

        //Debug.Log("GAAAA");
        if (Input.Shoot)
        {
            //Debug.Log("Shoooottt22");
            if (Cooldown <= 0 || Debugg)
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
                var shootOrigin = transform.position + Vector3.up * 1.5f;
                var shootDirection = (Input.ShootTarget - shootOrigin).normalized;
                var shootRay = new Ray(shootOrigin, shootDirection + shootVariation);


                //do we hit anybody?
                var hitInfo = new RaycastHit();
                gameObject.layer = Physics.IgnoreRaycastLayer;
                //Debug.Log("GARRR");
                if (Physics.SphereCast(shootRay, 0.1f, out hitInfo, Player.AllButIgnoreLayer))
                {
                    //Debug.Log("EDED");
                    UnityEngine.Debug.DrawLine(shootRay.origin, hitInfo.point, Color.red);

                    var player = hitInfo.collider.GetComponent<Player>();
                    if (player != null && !Debugg)
                    {
                        //Debug.Log("AAA");
                        player.OnHit();
                    }
                }
                gameObject.layer = 0;


            }
        }


        //var charVelo = Quaternion.Inverse(transform.rotation) * Rigidbody.velocity;
        //Animator.SetFloat("SpeedForward", charVelo.z);
        //Animator.SetFloat("SpeedSideward", charVelo.x * Mathf.Sign(charVelo.z + 0.1f));
        /*
        if (Input.SwitchToAK)
        {
            Input.SwitchToAK = false;
            Animator.SetBool("AK", true);
            Animator.SetBool("Pistol", false);
        }

        if (Input.SwitchToPistol)
        {
            Input.SwitchToPistol = false;
            Animator.SetBool("AK", false);
            Animator.SetBool("Pistol", true);
        }
        */
    }

    private void OnHit()
    {
        //Debug.Log("FINNN");
        Debug.Log(HP);
        if (IsDead)
            return;

        HP = HP - 1;
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
}
