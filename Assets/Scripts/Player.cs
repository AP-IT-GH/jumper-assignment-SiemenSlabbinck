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

    public override void Initialize(){
        base.Initialize();
        body = GetComponent<Rigidbody>();
        onGround = true;
    }

    private void FixedUpdate() {
        if (GameObject.FindWithTag("Car") == null){
            AddReward(1f);
            EndEpisode();
            Debug.Log("Success");
        }

    }

    public override void OnEpisodeBegin(){
        transform.localPosition = new Vector3(0, 1.8f, 0);
        body.velocity = new Vector3(0,0,0);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            continuousActionsOut[0] = 1f;
        }
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        if (actionBuffers.ContinuousActions[0] == 1)
        {
            //AddReward(-0.2f);
            if (onGround == true){
                body.velocity = new Vector3(0,jumpSpeed,0);
                onGround = false;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road")){
            onGround = true;
        } else if (collision.gameObject.CompareTag("Car")){
            AddReward(-1.0f);
            Debug.Log("Fail");
            Destroy(collision.gameObject);
            EndEpisode();
        }
    }
}