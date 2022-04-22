using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public Enemy enemyPrefab;

    public void SpawnEnemy(){
        Debug.Log("SpawnEnemy");
        GameObject newEnemy = Instantiate(enemyPrefab.gameObject);

        newEnemy.transform.localPosition = new Vector3(8, 1, 0);
    }
}
