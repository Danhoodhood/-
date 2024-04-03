using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class Player : Monser
{
    [SerializeField] float speed = 3f; //�������� ��������
    
    [SerializeField] private float jumpForce = 15f;// ���� ������
    [SerializeField] private bool isGrounded = false;

    [SerializeField]private bool isAttacking = false; // ������� ��
    [SerializeField] private bool isRecharged = false; // �������������� ��

    public Transform attackPos;//������� ����� 
    public float attackRange;//��������� �����
    public LayerMask enemy;//


    
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    public static Player Instance { get;  set; }//������ ����� ���������� � ������� ����� ������ �� ������ ������� �� �������� ���������� ����� ������ � ������
    public Joystick joystick;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        Instance = this;
        isRecharged=true;


        lives = 5;
    }

    private void Run()
    {
        Vector3 dir = transform.right*joystick.Horizontal;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed*Time.deltaTime);

        sprite.flipX = dir.x < 0.0f;//  ������� ����-����� ���� ����������� ������ ���� flipX = true � �� �������������� �����
    }

    private void Jump()
    {
         //rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        rb.velocity = Vector2.up*jumpForce;
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
        if (isGrounded &&  joystick.Vertical>=0.55f )
        {
            Jump();
            
        }
        
        
    }
    public override void GetDamage()
    {
        lives  -= 1;
        Debug.Log(lives);
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
    
}
