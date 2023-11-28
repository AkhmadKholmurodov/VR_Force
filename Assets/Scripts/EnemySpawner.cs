using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemys;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            foreach(var enemy in enemys)
            {
                enemy.SetActive(true);
            }
        }
    }
}