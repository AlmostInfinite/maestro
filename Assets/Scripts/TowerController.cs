using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class TowerController : MonoBehaviour
{

    // TODO manages tower firing/sounds/visual changes??


    public float bulletSpeed = 10;

    public Transform bulletSpawn;
    public Tower tower;
    

    public Material[] materials;


    // TODO Move mouseup to manager and create functions for selecting and deselecting a tower.
    void OnMouseUp()
    {
        if (tower.isNode && tower.towerType != 0) //and not on cooldown??
        {
            Fire();
            // Set cooldown??
        }            
    }

    void Fire()
    {
        //checked tower type, set bulletPrefab to approbraite prefab for that tower before shooting.

        Rigidbody bulletClone = (Rigidbody)Instantiate(tower.bulletPrefab, bulletSpawn.position, transform.rotation, this.transform);
        bulletClone.velocity = bulletSpawn.forward * bulletSpeed;
		Destroy (bulletClone.gameObject, 1.1f);
    }

}



