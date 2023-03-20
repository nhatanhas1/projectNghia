using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Neko2 : MonoBehaviour , IDamageable
{
    [SerializeField] PlayerAttack attackRadius;

    public int tempSpeed;
    public int moveSpeed;
    public int runSpeed;
    public float hp;
    public float stamina;
    public float maxHP;
    public float maxStamina;
    public bool isStaminaLow = false;

    public float timeBtwAttack;
    public float startTimeBtwAttack;

    Animator animator;
    private string currentState;

    bool isDead;
    bool isAttackPressed;
    bool isAttacking = false;
    public Transform attackPos;
    public float attackRange;
    public LayerMask enemiesLayer;
    public int damage;

    //Animation State
    const string NEKO_IDLE = "idle";
    const string NEKO_WALK = "walk";
    const string NEKO_RUN = "run";
    const string NEKO_ATTACK = "attack";
    const string NEKO_DEAD = "dead";

    private SpriteRenderer spriteRenderer;

    public Rigidbody myBD;
    
    [SerializeField] Vector3 moveDir;

    private Vector3 moveDirection;

    private void Awake()
    {
        myBD = GetComponent<Rigidbody>();
        hp = maxHP; stamina = maxStamina;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        moveSpeed = 10;
        //myBD= GetComponent<Rigidbody>();  
        //hp=maxHP; stamina=maxStamina;
        //spriteRenderer=GetComponent<SpriteRenderer>();
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        Attack();
        CheckRotation();
        NekoDead();
        HealthConsume();
        StaminaCheck();
    }
    private void FixedUpdate()
    {
        //Move();
        Movement();
        if (isStaminaLow == false)
        {
            Run();
        }
        else if (isStaminaLow)
        {
            tempSpeed= moveSpeed;
        }
        
        
        
    }
    void CheckInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(moveX,0, moveY).normalized;

        //Debug.Log("Input X la : " + moveX + " Input Y la" + moveY);
    }

    void Movement()
    {
        if (isDead == false)
        {
            myBD.velocity = new Vector3(moveDirection.x * tempSpeed, 0, moveDirection.y * tempSpeed);
            myBD.velocity = moveDirection * tempSpeed;
            Debug.Log(myBD.velocity);

            //if(Input.anyKeyDown==false)
            //{
            //    moveDir = Vector3.zero;
            //    myBD.velocity = moveDir;

            //}
            if (myBD.velocity.x < 0 && myBD.velocity.x >= -moveSpeed)
            {
                ChangeAnimationState(NEKO_WALK);
                //this.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (myBD.velocity.x > 0 && myBD.velocity.x <= moveSpeed)
            {
                ChangeAnimationState(NEKO_WALK);
                //this.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (myBD.velocity.z > 0 && myBD.velocity.z <= moveSpeed)
            {
                ChangeAnimationState(NEKO_WALK);
            }
            if (myBD.velocity.z < 0 && myBD.velocity.z >= -moveSpeed)
            {
                ChangeAnimationState(NEKO_WALK);
            }
            else if (myBD.velocity.x == 0 && myBD.velocity.z == 0)
            {
                ChangeAnimationState(NEKO_IDLE);
            }
            if (myBD.velocity.x > moveSpeed || myBD.velocity.x < -moveSpeed || myBD.velocity.z > moveSpeed || myBD.velocity.z < -moveSpeed)
            {
                ChangeAnimationState(NEKO_RUN);
            }
            if (myBD.velocity.x < 0 && myBD.velocity.x >= -moveSpeed)
            {

                // this.transform.rotation = Quaternion.Euler(-90, 180, 0);
                // anim.Play("Walk");
            }
            else if (myBD.velocity.x > 0 && myBD.velocity.x <= moveSpeed)
            {

                //  this.transform.rotation = Quaternion.Euler(90, 0, 0);
                //   anim.Play("Walk");
            }
        }
    }

    //void Move()
    //{
    //    myBD.velocity = new Vector3(moveDirection.x * tempSpeed, 0f ,moveDirection.y * tempSpeed);
    //    //Debug.Log(myBD.velocity);
    //    if (myBD.velocity.x < 0)
    //    {

    //        this.transform.rotation = Quaternion.Euler(-90, 180, 0);
    //        // anim.Play("Walk");
    //    }
    //    else if (myBD.velocity.x > 0)
    //    {
       
    //        this.transform.rotation = Quaternion.Euler(90, 0, 0);
    //        //   anim.Play("Walk");
    //    }
    //}
    void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            tempSpeed = runSpeed;
            //Debug.Log("Running");
        }
        else
        {
            tempSpeed = moveSpeed;
        }
    }
    void HealthConsume()
    {
        if (hp >= 0)
        {
            hp -= 4f * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            hp += 5;
            hp = Mathf.Clamp(hp, 0, maxHP);
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            attackRadius.AttackInRadius();
            Debug.Log("Get Key downd");
        }
        
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Player Take Damage");
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;//tránh Animation tự phá chính nó
        animator.Play(newState);
        currentState = newState;//thay newState vào 
    }

    void CheckRotation()
    {
        if (myBD.velocity.x > 0)
        {
            transform.rotation = Quaternion.Euler(90, 0, 0);
        }
        if (myBD.velocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(-90, 180, 0);
        }
        if(myBD.velocity.x==0)
        {
            return;
        }
    }
    void StaminaCheck()
    {
        if (currentState == NEKO_WALK || currentState == NEKO_IDLE)
        {
            stamina += 20 * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0, maxStamina);
        }
        if (currentState == NEKO_RUN)
        {
            stamina -= 40 * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0, maxStamina);
        }
        if (stamina <= 10)
        {
            isStaminaLow = true;
        }
        else if (stamina >= 30)
        {
            isStaminaLow = false;
        }
    }
    void NekoDead()
    {
        if (hp <= 0)
        {
            myBD.constraints = RigidbodyConstraints.FreezePosition;
            isDead = true;
            ChangeAnimationState(NEKO_DEAD);
        }
    }
}
