using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Codigo base para movimento do Player
    
    public float velocidade;
    
    #region Variaveis Privadas
    Camera mainCam;
    Rigidbody2D rb;
    Vector2 mov;
    SpriteRenderer sr;
    
    [SerializeField] BoxCollider2D katanaBC;
    [SerializeField]BoxCollider2D playerBC;
    GameObject cursor;
    #endregion 

    float nextDash;
    float dashRate;
    float nextAtt;
    float attRate;
    [HideInInspector]public bool dash;
    public Animator dashAnim;
    public Animator runAnim;
    [HideInInspector]public bool isRunning;

    [SerializeField] GameObject deathParticle;

    [SerializeField] GameObject Katana;
    Transform transformKatana;
    SpriteRenderer srKatana;

    void Start()
    {
        playerBC = GetComponent<BoxCollider2D>(); 
        katanaBC = Katana.GetComponent<BoxCollider2D>();
        transformKatana = Katana.GetComponent<Transform>();
        srKatana = Katana.GetComponent<SpriteRenderer>();
        
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        cursor = GameObject.Find("Cursor");
        mainCam = Camera.main;
        
        
        playerBC.enabled = true;
        katanaBC.enabled = false;
        dash = true;

        velocidade = 6.5f;
        dashRate = 5f;
        attRate = .4f;

        isRunning = false;
    }

    void Update()
    {
        
        #region Movimentação
            mov.x = Input.GetAxisRaw("Horizontal");   
            mov.y = Input.GetAxisRaw("Vertical");        
        #endregion
        
        #region Rotação com Mouse
            Vector3 mousePos = Input.mousePosition;
            Vector3 alvoPos = mainCam.WorldToScreenPoint(transform.localPosition);
            Vector2 diff = new Vector2(mousePos.x - alvoPos.x, mousePos.y - alvoPos.y);
            float angulo = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transformKatana.rotation = Quaternion.Euler(0f, 0f, angulo);
           
        #endregion 
        #region Animação do icone de Dash
            if(Time.time > nextDash)
            {
                dash = true;
                dashAnim.SetBool("canDash", dash);
            }
        #endregion
        #region  Rotaciona os braços
        if(transformKatana.rotation.w > 0.7f)
        {
            srKatana.flipY = false;
            sr.flipX = false;

        }
        else if(transformKatana.rotation.w < 0.7f)
        {
            srKatana.flipY = true;
            sr.flipX = true;
        }
        #endregion 
        
        
        if(Input.GetButtonDown("Fire1") && Time.time > nextAtt)
        {
            Attack();
        }       
        else
            katanaBC.enabled = false;

        if(Input.GetButtonDown("Dash") && Time.time > nextDash)
        {
           Dash();
           Invoke("NormalizeVel", .5f);
        }
        
        if(mov != Vector2.zero)
        {
            isRunning = true;
            runAnim.SetBool("Running", isRunning);
        }
        else
        {
            isRunning = false;
            runAnim.SetBool("Running", isRunning);
        }
    }

    private void FixedUpdate() 
    {
        rb.MovePosition(rb.position + mov * velocidade * Time.fixedDeltaTime);
    }

    private void Attack(){
        nextAtt = Time.time + attRate;
        katanaBC.enabled = true;
    }
    
    private void Dash(){
        nextDash = Time.time + dashRate;
        playerBC.enabled = false;
        velocidade *= 3f;
        dash = false;
        dashAnim.SetBool("canDash", dash);
    }
    
    private void NormalizeVel()
    {
        playerBC.enabled = true;
        velocidade = 5f;
        
    } 

    private void Death()
    {
        deathParticle.SetActive(true);
        this.gameObject.SetActive(false);  
    }
      
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Enemy")
        {
            Death();
            GameManager.Instance.DeathScreen();
        }
        if(other.gameObject.tag == "Enemy2")
        {
            Death();
            GameManager.Instance.DeathScreen();
        }
        if(other.gameObject.tag == "Enemy3")
        {
            Death();
            GameManager.Instance.DeathScreen();
        }   
        if(other.gameObject.tag == "Bolin")
        {
            Death();
            GameManager.Instance.DeathScreen();
        }
        
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            Death();
            GameManager.Instance.DeathScreen();
        }

      
    }
}
