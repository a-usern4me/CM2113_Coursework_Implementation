using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backwardsAndForwards : MonoBehaviour {
    [Header("References")]
    public Rigidbody van;
    public AudioSource horn;

    [Header("Movement")]
    public float timer;
    public float maxTimer;
    public float speed;

    void Start(){
        van = this.GetComponent<Rigidbody>();

    }

    void Update(){
        if (timer <= -maxTimer){
            timer = maxTimer;

        } else {
            timer -= Time.deltaTime;

        }
        //print(timer);
  
    }

    private void FixedUpdate(){
        moving();
       
    }

    private void moving(){
        if (timer >= 0){
            van.velocity = Vector3.forward * speed ;
            van.MoveRotation(Quaternion.Euler(-90f, 180f, 0f));
     

        } else if (timer < 0){
            van.velocity = Vector3.back * speed;
            van.MoveRotation(Quaternion.Euler(-90f, 0f, 0f));

        } 

    } 

    private void OnTriggerEnter(Collider collider){
        if (collider.gameObject.tag == "Player"){
            horn.Play();

        }
    }
}