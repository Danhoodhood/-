using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Monser
{
    [SerializeField] float speed = 3f; //�������� ��������
    
    [SerializeField] private float jumpForce = 15f;// ���� ������
    [SerializeField] private bool isGrounded = false;

    [SerializeField]private bool isAttacking = false; // ������� ��
    //[SerializeField] private bool isRecharged = false; // �������������� ��

    public Transform attackPos;//������� ����� 
    public float attackRange;//��������� �����
    public LayerMask enemy;//

    private float moveInput;//���������� ��������
    private bool factingRight = true;
    
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    public static Player Instance { get;  set; }//������ ����� ���������� � ������� ����� ������ �� ������ ������� �� �������� ���������� ����� ������ � ������
    public Joystick joystick;
    public AudioSource audioSourceJump;
    public AudioSource audioSourceDamagePlayer;
    //[SerializeField] private AudioSource audioSourceDamageMonster;

    [SerializeField]private Animator anim;

   /* private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }*/

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        Instance = this;
        //isRecharged=true;

        
        lives = 5;
    }
    
    private void Run()
    {
       // if (isGrounded) State = States.run;

        Vector3 dir = transform.right*joystick.Horizontal;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed*Time.deltaTime);

        sprite.flipX = dir.x < 0.0f;//  ������� ����-����� ���� ����������� ������ ���� flipX = true � �� �������������� �����
    }

    public void Jump1()
    {

        //rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        audioSourceJump.Play();
        anim.SetTrigger("jumpUp");
        if (!isGrounded)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    // ���������, ��� ����� ������� �� �����
                    if (transform.position.y > collider.transform.position.y)
                    {
                       
                       collider.GetComponent<Monser>().GetDamage();
                       //audioSourceDamageMonster.Play();
                    }
                }
            }
        }
        else  { 
        
            rb.velocity = Vector2.up*jumpForce;
        }
    }
    public void Jump()
    {
        if (isGrounded) { Jump1(); }
    }
    void Flip()
    {
        factingRight = !factingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *=-1;
        transform.localScale = Scaler;  
    }
    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;

        if (!isGrounded)
        {
           // State = States.jump;
        }

    }
    void Start()
    {
        //anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        CheckGround();
        moveInput = Input.GetAxis("Horizontal");
        if(factingRight == false && joystick.Horizontal >0)
        {
            Flip();
        }
        else if(factingRight == true && joystick.Horizontal < 0)
        {
            Flip();
        }
    }
    void Update()
    {

        /*if (moveInput== 0)
        { anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }*/
        if (isGrounded)
        {
            anim.SetBool("isJump", false);
            anim.ResetTrigger("jumpUp");
           
        }
        else
        {
            
            anim.SetBool("isJump", true);
            
        }

        if (joystick.Horizontal !=0 && !isAttacking)
        {
            Run();
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        if (isGrounded &&  joystick.Vertical>=0.55f)//����������� �� ���! ������� ��� �����(������)
        {
            Jump();
            //anim.SetTrigger("jumpUp");

        }//(�����)


    }
    public override void GetDamage()
    {
        HeartSystem.health--;
        
        lives  -= 1;

        Debug.Log(lives);
        audioSourceDamagePlayer.Play();
    }
    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }
    public void Attack()
    {
        if (isGrounded ) {
          //  State = States.attack;
            isAttacking = true;
            //isRecharged = false;

            StartCoroutine(AttackCoolDown());

            Debug.Log("����");

            Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
            

            for (int i = 0; i < colliders.Length; i++)
            {

                colliders[i].GetComponent<Monser>().GetDamage();
            }
        }
    }

    private void OnAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);

        for(int i = 0; i < colliders.Length; i++) {
            colliders[i].GetComponent<Monser>().GetDamage();
        }   
    }
    private void OnDrawGizmosSelected()
    {   
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)//�������� ��������� ������ � ���������� ����� ������������� �� ��� 
    {
        if (collision.gameObject.name.Equals("moving_platform"))
        {
            this.transform.parent = collision.transform;
        }

    }
    
    private void OnCollisionExit2D(Collision2D collision)//�������� �� ��������� ������ � ����������
    {
        if (collision.gameObject.name.Equals("moving_platform"))
        {
            this.transform.parent = null;
        }

    }

    
}
/*public enum States
{
    idle,
    run,
    jump
}
*/