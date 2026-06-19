using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hitSfx;
    
    public float maxjumpForce = 100f;
    public float minjumpForce = 10f;
    public float maxChargeTime = 1f; //最常蓄力時間
    
    public bool infiniteJump = false;//無限跳躍
    
    public float reboundForce = 50f;
    

    private Rigidbody2D rb2D;
    private Animator anim;
    private SpriteRenderer _renderer; //圖片渲染顏色

    private float buttonDowntime;
    private float lastHitTime;
    
    bool canJump = true;
    
    //取得滑鼠座標
    private Vector2 mousePos;
    float dirX;


    private void Awake()
    {
        //初始化自動抓元件
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        
    }

    private void OnEnable()
    {
        gameUIMgr.OnInfiniteJumpChanged += SetInfiniteJump;
    }

    private void OnDisable()
    {
        gameUIMgr.OnInfiniteJumpChanged -= SetInfiniteJump;
    }
    
    void SetInfiniteJump(bool value)
    {
        infiniteJump = value;
    }
    
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousepos();
        LookAtMouse();
        
        if (Input.GetMouseButtonDown(0))
        {
            buttonDowntime = Time.time;
        }

        if (Input.GetMouseButtonUp(0)&& canJump)
        {
            Jump();
        }
    }

    private void mousepos()
    {
        if (mousePos.x > transform.position.x)
        {
            dirX = 1f;   //右
        }
        else
        {
            dirX = -1f;  //左
        }
    }

    private void LookAtMouse()
    {
        if (dirX > 0)
        {
            _renderer.flipX = false; // 面向右
        }
        else
        {
            _renderer.flipX = true;  // 面向左
        }
    }

    private void Jump()
    {
        //計算按住時間
        float holdTime = Time.time - buttonDowntime;

        //限制最大蓄力時間
        holdTime = Mathf.Clamp(holdTime, 0, maxChargeTime);

        //換算力量
        float force = Mathf.Lerp(
            minjumpForce,
            maxjumpForce,
            holdTime / maxChargeTime
        );
        
        rb2D.linearVelocity = Vector2.zero;
        
        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
        
        //施加力量
        rb2D.AddForce(direction * force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (audioSource != null && hitSfx != null)
        {
            if (Time.time - lastHitTime > 0.1f)
            {
                audioSource.PlayOneShot(hitSfx);
                lastHitTime = Time.time;
            }
        }
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;//可跳躍 "開啟"
            
        }

        if (collision.gameObject.TryGetComponent(out BounceCollision bounce))
        {
            float finalForce = 0f;
            
            switch (bounce.強度)
            {
                case level.Weak:
                    finalForce = reboundForce / 10f;
                    break;
                case level.Normal:
                    finalForce = reboundForce / 5f;
                    break;
                case level.Strong:
                    finalForce = reboundForce;
                    break;
            }
           
            Vector2 bounceDir = (transform.position - collision.transform.position).normalized;
            bounceDir.y = -Mathf.Abs(bounceDir.y);

            rb2D.AddForce(bounceDir * finalForce, ForceMode2D.Impulse);
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(infiniteJump)return;
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = false;//可跳躍 "關閉"
        }
    }
}
