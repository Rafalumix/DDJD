using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WindPush : MonoBehaviour
{
    private GameObject player;
    private List<Collider2D> collisions = new List<Collider2D>();
    public LayerMask obstacleMask;

    public float cooldownTime;
    private float nextFireTime;
    private bool isCooldown = false;
    public Image imageCooldown;

    public bool pickedUp = false;

    public AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindWithTag("Player");
        // imageCooldown.enabled = false;
        GameObject.Find("WindPush").GetComponent<Image>().enabled = false; 
    }

    void Update()
    {
        if (pickedUp && Input.GetKeyDown(KeyCode.F) && Time.time >= nextFireTime) 
        {
            sound.Play();
            if (Push() > 0) 
            {
                nextFireTime = Time.time + cooldownTime;
                isCooldown = true;
            }
        }

        if(isCooldown)
        {
            ApplyCooldown();
        }
    }

    void ApplyCooldown() 
    {
        if(Time.time >= nextFireTime) 
        {
            isCooldown = false;
            imageCooldown.fillAmount = 0.0f;
        }
        else 
        {
            imageCooldown.fillAmount = (nextFireTime - Time.time) / cooldownTime;
        }
    }

    int Push()
    {   
        Vector2 position = this.player.transform.position;
        Collider2D[] results = Physics2D.OverlapCircleAll(position,5, obstacleMask);
        
        foreach (var result in results) {
            Vector2 direction = result.transform.position - transform.position;
            result.gameObject.tag = "Projectile";
            result.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            result.gameObject.GetComponent<Rigidbody2D>().AddForce(200*direction);
        }
        return results.Length;
    }
}
