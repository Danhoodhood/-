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


    
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    public static Player Instance { get;  set; }//������ ����� ���������� � ������� ����� ������ �� ������ ������� �� �������� ���������� ����� ������ � ������
    public Joystick joystick;
    public AudioSource audioSourceJump;
    public AudioSource audioSourceDamagePlayer;
    //[SerializeField] private AudioSource audioSourceDamageMonster;
    



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
        Vector3 dir = transform.right*joystick.Horizontal;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed*Time.deltaTime);

        sprite.flipX = dir.x < 0.0f;//  ������� ����-����� ���� ����������� ������ ���� flipX = true � �� �������������� �����
    }

    public void Jump()
    {
        //rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                    audioSourceJump.Play();
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
        else
        {
            rb.velocity = Vector2.up*jumpForce;
        }
    }


    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;

        if (!isGrounded)
        {
           
        }

    }
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        CheckGround();
    }
    void Update()
    {
        if (joystick.Horizontal !=0 && !isAttacking)
        {
            Run();
        }
        if (isGrounded &&  joystick.Vertical>=0.55f )//����������� ��� ������� ��� �����
        {
            Jump();

        }
        
        
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
