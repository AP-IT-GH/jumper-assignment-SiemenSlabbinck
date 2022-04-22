using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public Enemy enemyPrefab;
    public GameObject newEnemy;

    private Rigidbody body;

    List<GameObject> enemyList = new List<GameObject>();


    public void SpawnEnemy(){
        ClearEnvironment();
        newEnemy = Instantiate(enemyPrefab.gameObject);
        newEnemy.transform.parent = this.transform;
        newEnemy.transform.localPosition = new Vector3(8, 1.8f, 0);

        enemyList.Add(newEnemy);
    }

    private void ClearEnvironment(){
        foreach (GameObject enemy in enemyList)
        {
            Destroy(enemy);
        }
        enemyList.Clear();
    }

    public bool GetPosition(){
        Vector3 pos = newEnemy.transform.localPosition;
        int posx = (int)pos.x;
        if (posx < -15){
            return true;
        } else {
            return false;
        }
    }
}
