###### Siemen Slabbinck

# Jumper oefening

![2022-05-05-19-32-38-image](https://user-images.githubusercontent.com/25724406/166981997-a98e401e-1015-4e2e-9bd1-44da9312d10f.png)

## Inleiding

De speler staat in het midden van een kruispunt, langs beide kanten komen auto's aangereden die de speler moet ontwijken.

## Hierarchy

![2022-05-05-19-34-50-image](https://user-images.githubusercontent.com/25724406/166982013-71097f76-4442-4ba1-a62f-5cf6a85ad7d6.png)

In het GameObject Environment hebben volgende objecten

- Road is het kruispunt en heeft de tag 'Road'.

- Grass is het grasveld waarop de road zich bevind.

- Player is de agent. (Componenten worden hieronder getoond)

- Tunnel zijn de tunnels op het einde van de road, deze hebben de tag Tunnel.

### Player

De player heeft een rigidbody component
![2022-05-05-19-40-26-image](https://user-images.githubusercontent.com/25724406/166982052-2a10fd19-5996-462f-a063-52457c2dc726.png)

en het behavior parameters, player en decision requester component.

![image](https://user-images.githubusercontent.com/25724406/166982246-30298786-cc7e-4968-a14d-64e2e219b6bf.png)

Het player script:

```c#
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
```

Als er geen car's in het environment object zijn betekent dit dat ze succesvol de tunnel hebben bereikt en geven we een reward van 1f.

Indien een collision met een car geven we een reward van -1f.

### Environment

Het environment gameobject heeft het Environment script.

![image](https://user-images.githubusercontent.com/25724406/166982320-8bd37089-8660-4900-8b60-4ea5dbc8ccc3.png)

Het environment script gaat bij elke update kijken of er een gameObject met de tag 'Car' bestaat. Als deze niet bestaat gaat hij er een initieren.

Aan de hand van een random getal wordt de rij richting bepaald.

```c#
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public class Environment : MonoBehaviour

{

    public Car carPrefab;

    public Player player;

    private void FixedUpdate() {

        if (GameObject.FindWithTag("Car") == null)

        {

            //Spawn new car

            Car newCar = Instantiate(carPrefab);

            newCar.transform.parent = this.transform;

            int rnd = Random.Range(0, 2);

            if (rnd == 0){

                newCar.transform.localPosition = new Vector3(22.18f, 1.52f, 0);

                newCar.direction = new Vector3((Random.Range(-15f, -10f)) * Time.deltaTime,0,0);

            } else {

                newCar.transform.localPosition = new Vector3(0, 1.52f, 22.18f);

                newCar.transform.localRotation = Quaternion.Euler(0, -90, 0);

                newCar.direction = new Vector3(0,0,(Random.Range(-15f, -10f)) * Time.deltaTime);

            }

        }

    }

}
```

### Car

Het car script dat aan de car prefab hangt:

```c#
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

```

Dit zal elke update de auto verplaatsen in de richting waarmee hij geinitieerd werd.

Als hij een collision heeft met een Tunnel wordt dit car object verwijdert.
