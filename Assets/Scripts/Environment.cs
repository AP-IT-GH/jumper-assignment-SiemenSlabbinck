using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public Enemy enemyPrefab;
    List<GameObject> enemyList = new List<GameObject>();


    public void SpawnEnemy(){
        ClearEnvironment();
        GameObject newEnemy = Instantiate(enemyPrefab.gameObject);
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
}
