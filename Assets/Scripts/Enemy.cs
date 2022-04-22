using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    private float randomizeSpeed = 0f;
    private Rigidbody body;

    private void Start() {
        randomizeSpeed = speed * Random.Range(.5f, 1.5f);
        body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        body.velocity = new Vector3(-randomizeSpeed, 0, 0);
    }
}