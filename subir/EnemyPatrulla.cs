using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyPatrulla : MonoBehaviour
{
    protected NavMeshAgent Agent;
    protected StateEnum State;
    protected Target[] PotentialTargets;
    [HideInInspector]

    public Target target;
    protected float NextState;

    //Transform targett;


    public GameObject playerr;

    public static bool estadoo = false;

    Vector3 temp;

    //Impacto impacto;

    // Start is called before the first frame update
    void Awake()
    {
        //impacto = GetComponent<Impacto>();

        //targett = PlayerManager.instance.player.transform;
        estadoo = false;
        Agent = GetComponent<NavMeshAgent>();
        PotentialTargets = FindObjectsOfType<Target>();
        target = PotentialTargets[Random.Range(0, PotentialTargets.Length)];
        Agent.SetDestination(target.transform.position); 
        State = StateEnum.RUN;
        temp = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Agent.updatePosition = false;
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;

        NextState -= Time.deltaTime;

        //if(impacto)


        switch (State)
        {
            case StateEnum.RUN:
                //if(Vector3.Distance(transform.position, target.transform.position) < 0.02f)
                Vector3 direction = (transform.position - temp).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
                if (Agent.desiredVelocity.magnitude < 0.1f)
                {
                    State = StateEnum.SHOOT;
                    NextState = Random.RandomRange(1f, 7f);
                }
                temp = transform.position;
                break;
            case StateEnum.SHOOT:

                if (Vector3.Distance(playerr.transform.position, transform.position) > 20)
                {
                    State = StateEnum.RUN;
                    estadoo = false;
                    var targetIndex = Random.Range(0, PotentialTargets.Length);
                    for (var i = 0; i < PotentialTargets.Length && PotentialTargets[targetIndex].Ocupied; i++)
                        targetIndex = (targetIndex + 1) % PotentialTargets.Length;
                    target = PotentialTargets[targetIndex];
                    target.EnemyGoal = this;

                    //target = PotentialTargets[Random.Range(0, PotentialTargets.Length)];
                    Agent.SetDestination(target.transform.position);

                    Vector3 directionn = (transform.position - temp).normalized;
                    Quaternion lookRotationn = Quaternion.LookRotation(new Vector3(directionn.x, 0, directionn.z));
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotationn, Time.deltaTime * 5f);

                    temp = transform.position;

                }
                else
                {
                    estadoo = true;
                    Vector3 directiona = (playerr.transform.position - transform.position).normalized;
                    Quaternion lookRotationa = Quaternion.LookRotation(new Vector3(directiona.x, 0, directiona.z));
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotationa, Time.deltaTime * 5f);
                    temp = transform.position;
                }
                //Debug.Log("PARADOOO");
                /*
                if (NextState < 0)
                {



                    State = StateEnum.RUN;
                    var targetIndex = Random.Range(0, PotentialTargets.Length);
                    for (var i = 0; i < PotentialTargets.Length && PotentialTargets[targetIndex].Ocupied; i++)
                        targetIndex = (targetIndex + 1) % PotentialTargets.Length;
                    target = PotentialTargets[targetIndex];
                    target.EnemyGoal = this;

                    //target = PotentialTargets[Random.Range(0, PotentialTargets.Length)];
                    Agent.SetDestination(target.transform.position);

                }
                */
                break;
        }

        transform.position += Agent.desiredVelocity * Time.deltaTime;

    }

    public enum StateEnum
    {
        RUN,
        SHOOT
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("COLICIONO");
        Destroy(other.gameObject);
    }

}
