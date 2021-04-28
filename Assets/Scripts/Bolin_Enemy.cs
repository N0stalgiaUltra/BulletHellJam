using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolin_Enemy : MonoBehaviour
{
    // Atributos basicos de inimigos, movimentação, colisores, comportamento
    CursorMove cursor;   
    public GameObject deathParticle;
    GameObject player;
    MainCamera mainCam;
    Rigidbody2D rb;
    [SerializeField] Transform spawnPoint;
    SpriteRenderer sr;
    Animator anim;

    Vector3 bolinPos;

    public bool isLooking;
    public bool isAttacking;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        mainCam = GameObject.FindWithTag("MainCamera").GetComponent<MainCamera>();
        cursor = GameObject.Find("Cursor").GetComponent<CursorMove>();
        spawnPoint = this.gameObject.transform.GetChild(1);
        rb = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponentInChildren<SpriteRenderer>();
        anim = this.GetComponentInChildren<Animator>();

        bolinPos = this.transform.position;
        isLooking = false;
        isAttacking = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        float distancia = Vector3.Distance(this.transform.position, player.transform.position);
        if(distancia <= 5f)
        {
            BolinAttack();
        }
        else
        {
            isAttacking = false;
            anim.SetBool("Attack", isAttacking);
        }


       if(this.rb.rotation >= 90f || this.rb.rotation <= -90f)
           sr.flipY = true;
       else
           sr.flipY = false;    
    }

    public void BolinAttack()
    { 
        isAttacking = true;
        anim.SetBool("Attack", isAttacking);
    }
    public void Move()
    {
        if(isLooking == false)
       {
            Vector3 direction = player.transform.position - this.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            isLooking = true;
       }
       else
       {
           transform.Translate(Vector3.right * 10f * Time.deltaTime);      
       }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Limite")
        {
            isLooking = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Katana")
        {
            GameManager.Instance.count++;
            mainCam.CameraShake();
            Instantiate(deathParticle, this.transform.position, Quaternion.identity);
            cursor.HitmarkEnable();
            Destroy(this.gameObject);
        }
    }
}
