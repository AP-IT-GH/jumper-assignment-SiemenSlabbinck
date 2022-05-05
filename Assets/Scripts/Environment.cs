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
