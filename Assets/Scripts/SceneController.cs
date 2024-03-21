using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    private GameObject[] _enemies;
    public int enemiesCount;

    void Start(){
        _enemies=new GameObject[enemiesCount];
    }

    void Update(){
        for(int i=0;i<_enemies.Length;i++){
            if(_enemies[i]==null){
                _enemies[i]=Instantiate(enemyPrefab) as GameObject;
                _enemies[i].transform.position=new Vector3(Random.Range(-10,10),1.5f,Random.Range(-10,10));
                float angle=Random.Range(0,360);
                _enemies[i].transform.Rotate(0,angle,0);
            }
        }
    }
}
