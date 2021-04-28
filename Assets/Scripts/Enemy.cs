using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Atributos basicos de inimigos, movimentação, colisores, comportamento
    CursorMove cursor;   
    public GameObject bulletPrefab;
    public GameObject deathParticle;
    MainCamera mainCam;
    
    GameObject player;
    Rigidbody2D rb;
    [SerializeField] Transform spawnPoint;
    SpriteRenderer sr;
    public float fireRate;
    float nextFire;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        mainCam = GameObject.FindWithTag("MainCamera").GetComponent<MainCamera>();
        cursor = GameObject.Find("Cursor").GetComponent<CursorMove>();
        spawnPoint = this.gameObject.transform.GetChild(0);
        rb = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        if(this.gameObject.tag == "Enemy")
           fireRate = 1.5f;
        if(this.gameObject.tag == "Enemy2")
           fireRate = 0.85f;
        if(this.gameObject.tag == "Enemy3")
           fireRate = 0.75f;
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 direction = player.transform.position - this.transform.position;
       float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
       rb.rotation = angle; 
       if(Time.time > nextFire)
        {
            Attack();
        }

        if(this.rb.rotation >= 90f || this.rb.rotation  <= -90)
            sr.flipY = true;
        else
            sr.flipY = false;    
    }
    public void Attack()
    {
        nextFire = Time.time + fireRate;
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        Rigidbody2D rbbullet = bullet.GetComponent<Rigidbody2D>();
        rbbullet.AddForce(spawnPoint.right * 10f, ForceMode2D.Impulse);
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Katana")
        {
            mainCam.CameraShake();
            GameManager.Instance.count++;
            Instantiate(deathParticle, this.transform.position, Quaternion.identity);
            cursor.HitmarkEnable();
            Destroy(this.gameObject);
        }
    }
}
