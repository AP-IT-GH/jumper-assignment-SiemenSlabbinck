using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Environment : MonoBehaviour
{
    public Enemy enemyPrefab;
    public GameObject newEnemy;
    public Player player;
    public Text scoreBoard;

    private Rigidbody body;

    List<GameObject> enemyList = new List<GameObject>();

    private void FixedUpdate()
    {
        scoreBoard.text = player.GetCumulativeReward().ToString("f2");
    }  

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
        Vector3 posEnemy = newEnemy.transform.localPosition;
        Vector3 posPlayer = player.transform.localPosition;
        int posxEnemy = (int)posEnemy.x;
        int posxPlayer = (int)posPlayer.x;
        if (posxEnemy < posxPlayer){
            return true;
        } else {
            return false;
        }

    }
}
