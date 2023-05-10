using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class climbingCollision : MonoBehaviour {
    [Header("References")]
    public Rigidbody rb;
    Animator anim;

    [Header("Climbing")]
    public float climbSpeed;
    private float climbTimer;
    public float maxClimbTime;
    private bool climbing = false;

    void Start(){
        rb = this.GetComponent<Rigidbody>();
        anim = this.GetComponent<Animator>();
        
    }

    void Update(){
        ClimbAction();
        if (climbing == true) WallCrawling();
       
    }

    private void ClimbAction(){
        if (!climbing && climbTimer > 0 && Input.GetKey("c")) StartClimbing();
        
        //Timer
        if(climbTimer > 0) climbTimer -= Time.deltaTime;
        if(climbTimer < 0) StopClimbing();

    }

    private void OnTriggerEnter(Collider collider){
        if (collider.gameObject.tag == "Building"){
            ClimbAction();
            climbTimer = maxClimbTime;
                     
        }
    }

    private void OnTriggerExit(Collider collider){
        if (collider.gameObject.tag == "Building"){
            climbTimer = -1;
           
        }
    }
    
    private void StartClimbing(){
        climbing = true;
        
    }

    private void WallCrawling(){
        anim.SetBool("Climbing", true);
        rb.velocity = new Vector3(rb.velocity.x, climbSpeed, rb.velocity.z);

    }

    private void StopClimbing(){
        anim.SetBool("Climbing", false);
        climbing = false;
        
    }

}