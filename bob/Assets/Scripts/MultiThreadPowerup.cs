using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MultiThreadPowerup : MonoBehaviour
{
    
    public bool isActive = false;
    public float effectDuration;
    private float endTime;
    public Image imageCooldown;

    public AudioSource pickSound;
    public Text popUp;


    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (isActive) 
        {
            ApplyCooldown();
        }
    }

    public void Activate() 
    {
        GameObject canvas = GameObject.Find("Canvas");
        Text obj = Instantiate(popUp, canvas.transform);
        Destroy(obj, 2.5f);
        pickSound.Play();
        endTime = Time.time + effectDuration;
        isActive = true;
    }

    void ApplyCooldown() 
    {
        if(Time.time >= endTime) 
        {
            isActive = false;
            GameObject.Find("Player").GetComponent<Weapon>().powerup = false;
            imageCooldown.fillAmount = 1.0f;
        }
        else 
        {
            imageCooldown.fillAmount = 1 - (endTime - Time.time) / effectDuration;
        }
    }
}
