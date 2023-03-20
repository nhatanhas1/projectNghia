using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour ,IDamageable
{
    public bool isDead;
    public float currentHp;
    public float maxHp;   
    public float moveSpeed;

    [SerializeField] GameObject dropItem;

    public Transform target;
    NavMeshAgent navMeshAgent;
    public float range = 10.0f;
    public bool isTargeted;
    Vector3 point;
    Vector3 runAwayDir;

    bool switchState;
    int timState;

    public enum EnemyStyle
    {
        tim,
        red,
        pink,
    }   

    [SerializeField] EnemyStyle enemyStyle;

    public enum State
    {
        Roaming,
        ChaseTarget,
        AttackTarget,
        Runaway,
    }

    public State enemyState;
    // Start is called before the first frame update
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<Neko2>().transform;
    }
    void Start()
    {
        isDead = false;
        maxHp = 20;
        currentHp = maxHp;

        timState = Random.Range(0, 2);
        StartCoroutine(UpdatePath());
        enemyState = State.Roaming;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator UpdatePath()
    {
        while (target != null)
        {
            if (navMeshAgent.isOnNavMesh)
            {
                //checkOnMesh = true;


                //MoveToPosition();
                //MoveRandomPosition();
                if (isTargeted)
                {
                    
                    switch (enemyStyle)
                    {
                        case EnemyStyle.red:
                            enemyState = State.ChaseTarget;
                            //Debug.Log("DO");
                            break;
                        case EnemyStyle.pink:
                            enemyState = State.Runaway;
                            break;
                        case EnemyStyle.tim:
                            //Debug.Log("Tim");
                            if (timState > 0)
                            {
                                enemyState = State.ChaseTarget;
                                //Debug.Log("Tim tan cong" + timState);
                            }
                            else
                            {
                                //Debug.Log("Tim Bo chay" + timState);
                                enemyState = State.Runaway;
                            }
                            break;
                    }

                }
                else
                {

                    enemyState = State.Roaming;
                }
                EnemyState();
                //Debug.Log("Run");
            }
            yield return new WaitForSeconds(.5f);
        }



    }

    void EnemyState()
    {
        switch (enemyState)
        {
            case State.Roaming:
                //Debug.Log("Roaming");
                //point = transform.position;
                MoveRandomPosition();
                
                break;
            case State.ChaseTarget:
                //switchState = true;
                //Debug.Log("Chasing");
                navMeshAgent.stoppingDistance = target.GetComponent<CapsuleCollider>().radius / 2;
                navMeshAgent.destination = target.position;
                //if (navMeshAgent.hasPath)
                //{
                //    //Debug.Log("tim duoc duong");
                //}
                break;

            case State.Runaway:
                //switchState = true;
                //Debug.Log("RunAway");
                runAwayDir = (target.transform.position - transform.position).normalized;
                MoveAway(transform.position - runAwayDir * 3);
                
                break;

        }
    }

    void MoveAway(Vector3 pos)
    {
        navMeshAgent.SetDestination(pos);
        navMeshAgent.isStopped = false;
    }

    public void MoveToPosition()
    {
        Vector3 randomDir = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)).normalized;
        navMeshAgent.destination =  transform.position + (randomDir) * Random.Range(1f, 3f);


    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                //Debug.Log("tim thay");
                return true;
                
            }
        }
        result = Vector3.zero;
        //Debug.Log("Khong tim thay");
        return false;
    }
    public void MoveRandomPosition()
    {
        if (navMeshAgent.remainingDistance < 0.5f || switchState == true)
        {
            //Debug.Log("Da den noi");


            if (RandomPoint(transform.position, range, out point))
            {
                //Debug.Log("aasd");
                navMeshAgent.destination = point;
                switchState = false;
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
            }
        }
        else
        {
            //Debug.Log("Dang tren duong di");
        }



        //if (RandomPoint(transform.position, range, out point))
        //{
        //    if (navMeshAgent.remainingDistance > 0.5f)
        //    {
        //        return;
        //    }
        //        //Debug.Log("aasd");
        //        navMeshAgent.destination = point;
        //    //switchState = false;
        //    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
        //}

    }

    public void TakeDamage(float damage)
    {        
        if (isDead) { return; }
        Debug.Log("Enemy Take Daamage");
        currentHp -= damage;
        if(currentHp <= 0)
        {
            
            Dead();
        }
        
    }

    void Dead()
    {
        isDead = true;
        GameObject dropitem =  Instantiate(dropItem,transform.position, Quaternion.Euler(90, 0, 0));
        ItemController itemController = dropitem.GetComponent<ItemController>();
        int tmp = Random.Range(0, itemController.itemDatas.Count - 1);
        itemController.itemData = itemController.itemDatas[1];
        itemController.SetUp();
        Destroy(gameObject);
    }
}
