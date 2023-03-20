using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private SpriteRenderer spriteRenderer;

    public Rigidbody myBD;
    
    [SerializeField] Vector3 moveDir;

    private Vector3 moveDirection;
    void Start()
    {
        moveSpeed = 10;
        myBD= GetComponent<Rigidbody>();  
        hp=maxHP; stamina=maxStamina;
        spriteRenderer=GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        Attack();
    }
    private void FixedUpdate()
    {
        //Move();
        Run();
        HealthConsume();
        Movement();
        
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
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            moveDir.x = Input.GetAxisRaw("Horizontal");
            moveDir.z = Input.GetAxisRaw("Vertical");

            myBD.velocity = moveDir * moveSpeed;
        }
        else
        {
            moveDir = Vector3.zero;
            myBD.velocity = moveDir;
        }
        if (myBD.velocity.x < 0)
        {

            this.transform.rotation = Quaternion.Euler(-90, 180, 0);
            // anim.Play("Walk");
        }
        else if (myBD.velocity.x > 0)
        {

            this.transform.rotation = Quaternion.Euler(90, 0, 0);
            //   anim.Play("Walk");
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
            hp -= 0.05f;
        }
        if(Input.GetKeyDown(KeyCode.Space))
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

}
