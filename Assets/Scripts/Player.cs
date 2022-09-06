using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Camera mainCam;
    public Slider slider;
    [SerializeField] private LayerMask platformLayerMask;
    public float health;
    private Rigidbody2D playerRB;
    private Animator playerAnimator;
    private BoxCollider2D boxCollider2d;
    public float moveSpeed;
    public float jumpSpeed;
    public bool facingRight = true;
    private bool isGrounded=false;
    Transform muzzle;
    public Transform bullet;
    public float bulletSpeed;
    bool dead=false;

    // Start is called before the first frame update
    private void Start()
    {
        slider.maxValue = health;
        slider.value = health;
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        muzzle = transform.GetChild(0);
    }

    // Update is called once per frame
    private void Update()
    {
        HorizontalMove();
        FlipFaceCheck();
        SetAnimatorParameters();
        Jump();
        IsGrounded();
        Shoot();
    }

    private void HorizontalMove()
    {
        playerRB.velocity= new Vector2 (Input.GetAxis("Horizontal")*moveSpeed,playerRB.velocity.y);
    }


    private void SetAnimatorParameters()
    {
        playerAnimator.SetFloat("playerSpeed", Mathf.Abs(playerRB.velocity.x));
        playerAnimator.SetBool("playerIsGrounded",isGrounded);
    }

    private void FlipFaceCheck()
    {
        if(playerRB.velocity.x<0 && facingRight)
        {
            FlipFace();
        }

        else if(playerRB.velocity.x>0 && !facingRight)
        {
            FlipFace();
        }

    }

    private void FlipFace()
    {
        facingRight = !facingRight;
        Vector3 tempLocalScale = transform.localScale;
        tempLocalScale.x *= -1;
        transform.localScale = tempLocalScale;
    }

    private void IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.1f, platformLayerMask);
        if (raycastHit.collider != null)
            isGrounded = true;
        else
            isGrounded = false;
    }

    private void Jump()
    {
        if (Input.GetAxis("Vertical") > 0 && isGrounded)
            playerRB.velocity=Vector2.up*jumpSpeed;
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ShootBullet();
    }

    void ShootBullet()
    {
        Transform tempBullet;
        tempBullet= Instantiate(bullet,muzzle.position,Quaternion.identity);
        tempBullet.GetComponent<Rigidbody2D>().AddForce(muzzle.forward * bulletSpeed);
        
        if(!facingRight)
        {
            tempBullet.Rotate(0f, 0f, 180f);
        }
        


    }


    public void GetDamage(float damage)
    {
        if((health-damage)>=0)
        {
            health-=damage;
        }
        else
        {
            health=0;
        }
        slider.value=health;
        mainCam.DOShakeRotation(1, 1);
        AmIDead();
    }

    void AmIDead()
    {
        if(health<=0)
        {
            dead = true;
        }
    }
}
