using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droplet : MonoBehaviour
{


    public GameObject particle_system;
    private Vector3 position;


    void Awake(){
         //Debug.Log("Assigned particle system");
        //particle_system = GetComponent<ParticleSystem>();
        position = gameObject.transform.position;
    }

    private void FixedUpdate()
    {
        transform.SetPositionAndRotation(new Vector3(transform.position.x,transform.position.y - Time.deltaTime*10, transform.position.z),Quaternion.identity);
    }



    void OnCollisionEnter2D(Collision2D col)  
    {
        //Debug.Log("Collided here");
        //particle_system.Play();
       
        Instantiate(particle_system,transform.position,Quaternion.EulerAngles(-90,0,0));
        //gameObject.transform.position = position;
        Destroy(gameObject);
    }

}