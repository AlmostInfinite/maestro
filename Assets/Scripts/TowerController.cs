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
    public Rigidbody bulletPrefab;

    public Transform bulletSpawn;
    public Tower tower;

    

    
    // TODO Move mouseup to manager and create functions for selecting and deselecting a tower.
    void OnMouseUp()
    {
        if (tower.isNode) // and a tower is placed and not on cooldown
        {
            if (tower.isVacant)
            {
                Debug.Log("Y no Tower?!?!");
                // if tower type selected is not nothing set tower/tile type on node
            }
            else
            {
                Fire();
            }
        }
        else
        {
            if (tower.isSelected)
            {
                tower.isSelected = false;
                GetComponent<MeshRenderer>().material.SetTexture("_MainTex", null);
                Debug.Log("Not Selected");
                //set type selcted variable to nothing(create one)
                //TowerManager.selectedType = LevelMapper.instance.tileTypes[0];
            }
            else
            {
                tower.isSelected = true;
                GetComponent<MeshRenderer>().material.SetTexture("_MainTex", tower.textureSelected);
                Debug.Log("Selected");
                //set type selcted variable tower/tile type(create one)
                //TowerManager.selectedType = LevelMapper.instance.tileTypes[1];
            }
        }
    }


    void Fire()
    {

        //checked tower type, set bulletPrefab to approbraite prefab for that tower before shooting.

        Rigidbody bulletClone = (Rigidbody)Instantiate(bulletPrefab, bulletSpawn.position, transform.rotation);
        bulletClone.velocity = bulletSpawn.forward * bulletSpeed;
    }

}



