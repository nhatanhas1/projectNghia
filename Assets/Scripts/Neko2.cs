using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Cinemachine;

public class Neko2 : MonoBehaviour, IDamageable
{



    [SerializeField] PlayerAttack attackRadius;

    public GameObject deadSound;
    public SFXPlay sfx;
    public CameraShake _VCam;
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

    public int tempScore;
    public int score;
    public Animator animator;

    public string currentState;

    bool isDead;
    bool isAttackPressed;
    public bool isAttacking;
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


    public GameOver gameOver;

    private void Awake() {

        sfx = FindAnyObjectByType<SFXPlay>();
        _VCam = FindAnyObjectByType<CameraShake>();
       
        tempSpeed = moveSpeed;
        myBD = GetComponent<Rigidbody>();
        hp = maxHP; stamina = maxStamina;
        spriteRenderer = GetComponent<SpriteRenderer>();
        //animator = GetComponent<Animator>();
        isAttacking = animator.GetBool("IsAttack");
        startTimeBtwAttack = timeBtwAttack;
    }

    void Start()
    {
        isDead = false;
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
        //NekoDead();
        HealthConsume();
        StaminaCheck();

    }
    private void FixedUpdate()
    {
        //Move();
        startTimeBtwAttack -= 1 * Time.deltaTime;
        Movement();
        if (isStaminaLow == false)
        {
            Run();
        }
        else if (isStaminaLow)
        {
            tempSpeed = moveSpeed;
        }
        if (isDead == false) { ScoreUp(); }


    }

    private void LateUpdate()
    {

    }
    void CheckInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(moveX, 0, moveY).normalized;

        //Debug.Log("Input X la : " + moveX + " Input Y la" + moveY);
    }

    void Movement()
    {
        {
            if (isAttacking == false) {

                if (isDead == false)
                {
                    //  myBD.velocity = new Vector3(moveDirection.x * tempSpeed, 0, moveDirection.y * tempSpeed);
                    myBD.velocity = moveDirection * tempSpeed;
                    //Debug.Log(myBD.velocity);

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
        if (!animator.GetBool("IsAttack"))
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

    }
    void HealthConsume()
    {
        if (hp >= 0)
        {
            hp -= 2.5f * Time.deltaTime;
        }
        else
        {
            NekoDead();
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            hp += 100;
            hp = Mathf.Clamp(hp, 0, maxHP);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            hp -= 50;
        }
    }

    void Attack()
    {
        if (!isStaminaLow && startTimeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                attackRadius.AttackInRadius();
                animator.SetBool("IsAttack", true);
                tempSpeed = 0;
                startTimeBtwAttack = timeBtwAttack;
                //_VCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1;
                _VCam.ShakeCamera();
                //ChangeAnimationState(NEKO_ATTACK);
                //Debug.Log("Get Key downd");
                stamina -= 10;
                sfx.PlaySlap();
            }
        }

    }

    public void EndAT() // event trong anim.clip
    {
        Debug.Log("EndAT");

        animator.SetBool("IsAttack", false);
        // ChangeAnimationState(NEKO_ATTACK);
        tempSpeed = moveSpeed;
        // _VCam.StopShake();
        // _VCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        //ChangeAnimationState(NEKO_IDLE);

    }
    public void TakeDamage(float damage)
    {
        if (isDead) { return; }
        Debug.Log("Player Take Damage");

        hp -= damage;
        sfx.PlayHit();
        if (hp <= 0)
        {
            NekoDead();
        }

    }

    //void ConsumeItem()
    //{

    //}

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
        if (myBD.velocity.x == 0)
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
        if (stamina <= 1)
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
        //if (hp <= 0)
        //{
        //    myBD.constraints = RigidbodyConstraints.FreezePosition;
        //    isDead = true;
        //    ChangeAnimationState(NEKO_DEAD);
        //    StartCoroutine(GameOver());

        //}       
        isDead = true;
        deadSound.SetActive(true);
        myBD.constraints = RigidbodyConstraints.FreezePosition;        
        ChangeAnimationState(NEKO_DEAD);
        StartCoroutine(GameOver());
        

    }
    void ScoreUp()
    {
        score = score + 1;
        if (isDead) { return; }
    }
    IEnumerator  GameOver()
    {
        
        yield return new WaitForSeconds(2.5f);
        gameOver.Setup(score);
        
    }
}
