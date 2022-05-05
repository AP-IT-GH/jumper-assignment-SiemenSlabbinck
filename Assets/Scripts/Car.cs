using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Vector3 direction;

    private void FixedUpdate() {
        transform.localPosition += direction;

        if (transform.localPosition.y < 0){
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Tunnel")){
            Destroy(gameObject);
            Debug.Log("Success");
        }
    }
}
