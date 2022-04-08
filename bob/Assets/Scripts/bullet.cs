using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public float speed = 20f; 
    public int damage = 25; 
    private Rigidbody2D bulletRigidBody; 
    private Animator anim; 
    private AudioSource bulletSplash;
    private HardcoreMode hm;

    // Start is called before the first frame update
    void Start()
    {   
        bulletRigidBody = GetComponent<Rigidbody2D>();
        bulletSplash = GetComponent<AudioSource>(); 
        anim = GetComponent<Animator>(); 
        bulletRigidBody.velocity = transform.right * speed; 
        hm = GetComponent<HardcoreMode>(); 
        if (hm.IsHardcore()==true){
            damage = 100; 
        }
    }

    void FixedUpdate() 
    {
        float playerPos = GameObject.Find("Player").transform.position.x;
        if (bulletRigidBody.gameObject.transform.position.x - playerPos >= 15){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D (Collider2D hitInfo){
        if (hitInfo.gameObject.CompareTag("Enemy") || hitInfo.gameObject.CompareTag("Obstacle") )
        {
            stopBullet(); 
            bulletDestroyedSound(); 
            EnemyController enemy = hitInfo.GetComponent<EnemyController>(); 
            if (enemy!= null){
                   enemy.TakeDamage(damage);  
            }
            anim.SetBool("Explosion", true); 
            // Debug.Log(hitInfo.name); 
        }
    }

    void stopBullet(){
        bulletRigidBody.velocity = transform.right * 0f; 
    }

    void destroyGameObject(){
        Destroy(gameObject);
    }

    public void bulletDestroyedSound(){
        bulletSplash.Play();
    }
}
