using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class moving : MonoBehaviour {
    [Header("References")]
    public Rigidbody rb;
    Animator anim;
    public Transform playerCameraRoot;
    private ParticleSystem part;
    public TMP_Text t1;

    [Header("Movement")]
    public float speed;
    public float maxSpeed;
    public Vector3 movement;
 
    [Header("Jumping")]
    public float jumpTimer;
    public float maxJumpTime;
    public float jumpPower;

    [Header("Web-Swinging")]
    public float webTimer;
     public float maxWebTimer;
    public float webCartidges;
    public float maxWebCartidges;
   
    [Header("LayerMask")]
    public LayerMask Ground;

    void Start(){
        rb = this.GetComponent<Rigidbody>();
        anim = this.GetComponent<Animator>();
        part = this.GetComponent<ParticleSystem>();

    }

    void Update(){
        jumpTimer -= Time.deltaTime;
        webTimer -= Time.deltaTime;
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        /** Hee Hee **/
        if (Input.GetKey("e")){
            anim.Play("Thriller Part 3");

        } else {
            anim.SetBool("Hee Hee", false);
            
        }

        if (webTimer >= 0){
             t1.text = ("Web Cooldown = " + webTimer.ToString("F2"));

        } else {
            t1.text = ("");
        }

       
     
    }

    void FixedUpdate(){
        Moving(movement);
        JumpMovement();

        if (Input.GetKey("l")){
            WebSwing();

        } else {
            anim.SetBool("Swinging", false);

        }

    }

    void Moving(Vector3 direction){
        /** Setting camera direction **/
        if (Input.GetKey("a")){
            playerCameraRoot.transform.localRotation = Quaternion.Euler(0, 90, 0);
            rb.MoveRotation(Quaternion.Euler(0f, -90f, 0f));
            
        } else if (Input.GetKey("d")){
            playerCameraRoot.transform.localRotation = Quaternion.Euler(0, -90, 0);
            rb.MoveRotation(Quaternion.Euler(0f, 90f, 0f));
           

        } else if (Input.GetKey("s")){
            playerCameraRoot.transform.localRotation = Quaternion.Euler(0, 180, 0);
            rb.MoveRotation(Quaternion.Euler(0f, 180f, 0f));
          
        } else if(Input.GetKey("w")){
            playerCameraRoot.transform.localRotation = Quaternion.Euler(0, 0, 0);
            rb.MoveRotation(Quaternion.Euler(0f, 0f, 0f));
  
        }

        /** Player movement **/
        if (movement != Vector3.zero){
            anim.SetBool("Running", true);
            if (rb.velocity.magnitude < maxSpeed){
                rb.velocity += direction * speed * Time.fixedDeltaTime;

            }
        
        } else {
            anim.SetBool("Running", false);

        }
      
    }

    private void JumpMovement(){
        if (Input.GetKey("space") && jumpTimer < 0){
            anim.Play("Jumping");
            rb.velocity = new Vector3(rb.velocity.x, jumpPower, rb.velocity.z);
            jumpTimer = maxJumpTime;

        }
        
    }

    private void WebSwing(){
        if (webTimer < 0 && webCartidges > 0){
            anim.Play("Swing To Land");
            part.Play();
            
            rb.velocity = (Vector3.up * 500 * Time.fixedDeltaTime);
            print("Thwip");

            webCartidges = webCartidges - 1;
            webTimer = maxWebTimer;
        
        } 
        
        if (webTimer < 0 && webCartidges <= 0){
            anim.Play("Look Over Shoulder");
            print("Out of Webs!");
            webTimer = 15f;
            webCartidges = maxWebCartidges;

        }
        
    }     

}