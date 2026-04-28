using System.Collections;
using UnityEngine;

public class Move : MonoBehaviour
{
    public int health = 100;
    public float movespeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private Animator anim;
    
    //圖片渲染
    private SpriteRenderer _renderer;

    private int jumpCount = 0;
    public int maxJump = 2;
    bool canJump = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 左右移動
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * movespeed, rb.linearVelocity.y);

        // 跳躍（按空白鍵）
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount++;
        }

        // 動畫控制
        HandleAnimation(moveInput);
    }


    void HandleAnimation(float moveInput)
    {
        if (canJump)
        {
            // 左右移動 = Walk
            if (moveInput == 0f)
            {
                anim.Play("idle");
            }
            // 靜止 = Idle
            else
            {anim.Play("run"); }            
        }
        else
        { // 上升 = Jump
                if (rb.linearVelocity.y > 0f)
                {
                    anim.Play("k");
                }
                // 下降 = Fall
                else
                {
                    anim.Play("k2");
                } 
        }
        
        
    }

    // 碰到地面重置跳躍次數
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Damage")
        {
            health -= 25;
            Debug.Log($"現在血量是{health}/100");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            StartCoroutine(Blinkred());
            if (health <= 0)
            {
                Die();
            }

        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
            jumpCount = 0;
            
        }
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
            jumpCount = 0;
            
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }

    private IEnumerator Blinkred()
    {
        _renderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _renderer.color = Color.white;
        
    }

    private void Die()
    {
        UnityEngine. SceneManagement. SceneManager. LoadScene("SampleScene");
    }
}