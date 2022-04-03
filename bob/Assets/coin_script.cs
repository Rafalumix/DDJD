using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin_script : MonoBehaviour
{
    private Animator anim; 
    private AudioSource pickupSound; 
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); 
        pickupSound = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DestroyObject(){
    Destroy(gameObject);
}

    public void CoinTaken(){
        anim.SetBool("coinTaken", true);  
    }

    public bool IsTaken(){
        return anim.GetBool("coinTaken"); 
    }

    public void coinPickupSound(){
        pickupSound.Play();
    }

}
