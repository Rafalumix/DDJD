using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobAnimations : MonoBehaviour
{

    private Rigidbody2D playerRigidbody;
    private Animator anim;
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        //parent = transform.parent; 
        //player = parent.Rigidbody2D; 
        anim = GetComponent<Animator>();
        playerRigidbody = parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerRigidbody.velocity.y < -0.1)
        {
            anim.SetBool("isFalling", true);
        }
        else
        {
            anim.SetBool("isFalling", false);
        }
    }
}
