using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class Player : Agent
{
    public float jumpSpeed = 50;

    private Rigidbody body;
    private Environment environment;

    public override void Initialize(){
        base.Initialize();
        body = GetComponent<Rigidbody>();
        environment = GetComponentInParent<Environment>();
    }

    public override void OnEpisodeBegin(){
        Debug.Log("OnEpisodeBegin");
        transform.localPosition = new Vector3(0f, 1.5f, 0f);
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

        body.angularVelocity = Vector3.zero;
        body.velocity = Vector3.zero;
        
        environment.SpawnEnemy();
    }

    public override void CollectObservations(VectorSensor sensor){
        
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;

        if (Input.GetKey(KeyCode.UpArrow)) // Moving fwd
        {
            continuousActionsOut[0] = 2f;
        }
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        if (actionBuffers.ContinuousActions[0] == 0)
        {
            AddReward(-0.001f);
            return;
        }

        if (actionBuffers.ContinuousActions[0] != 0)
        {
            Vector3 translation = transform.forward * jumpSpeed * (actionBuffers.ContinuousActions[0] * 2 - 3) * Time.deltaTime;
            transform.Translate(translation, Space.World);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            AddReward(1f);
            Destroy(collision.gameObject);
            EndEpisode();
        }
    }
}
