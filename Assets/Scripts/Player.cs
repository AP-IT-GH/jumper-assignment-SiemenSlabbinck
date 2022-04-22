using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class Player : Agent
{
    public float jumpSpeed = 10;

    private bool onGround;
    private Rigidbody body;
    private Environment environment;

    public override void Initialize(){
        base.Initialize();
        body = GetComponent<Rigidbody>();
        environment = GetComponentInParent<Environment>();
    }

    private void FixedUpdate() {
        if (environment != null){
            if (environment.GetPosition()){
                Debug.Log("Success");
                AddReward(1f);
                EndEpisode();
            }
        }

        if (transform.localPosition.y < 0){
            AddReward(-1f);
            EndEpisode();
        }
    }

    public override void CollectObservations(VectorSensor sensor){
        sensor.AddObservation(onGround);
    }

    public override void OnEpisodeBegin(){
        //transform.localPosition = new Vector3(-6f, 1.5f, 0f);
        //transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

        //body.angularVelocity = Vector3.zero;
        //body.velocity = Vector3.zero;
        
        environment.SpawnEnemy();
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;

        if (Input.GetKey(KeyCode.UpArrow)) // Moving fwd
        {
            continuousActionsOut[0] = 1f;
        }
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        if (actionBuffers.ContinuousActions[0] != 0)
        {
            if (onGround == true){
                body.velocity = new Vector3(0,jumpSpeed,0);
                onGround = false;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            Debug.Log("Collision");
            AddReward(-1f);
            Destroy(collision.gameObject);
            EndEpisode();
        } else if (collision.transform.CompareTag("Floor")){
            onGround = true;
        }
    }
}
