using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WindPush : MonoBehaviour
{
    private GameObject player;
    private List<Collider2D> collisions = new List<Collider2D>();

    public float cooldownTime;
    private float nextFireTime;
    private bool isCooldown = false;
    public Image imageCooldown;

    public bool pickedUp = false;

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
        Collider2D[] results = Physics2D.OverlapCircleAll(position,5,(1<<8));
        foreach (var result in results) {
            if(result.CompareTag("Obstacle")){
                Vector2 direction = result.transform.position - transform.position;
                result.gameObject.tag = "Projectile";
                result.gameObject.GetComponent<Rigidbody2D>().AddForce(500*direction);
            }
        }
        return results.Length;
    }
}
