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



    public Material[] materials;


    // TODO Move mouseup to manager and create functions for selecting and deselecting a tower.
    void OnMouseUp()
    {
        if (tower.isNode) // and a tower is placed and not on cooldown
        {
            if (tower.isVacant)
            {

                // TODO tidy up
                this.GetComponent<Renderer>().material = GameManager.instance.towerToPlace.GetComponent<Renderer>().material;

                // if tower type selected is not nothing set tower/tile type on node
                //switch (GameManager.instance.selectedTowerType)
                //{

                //    case 1:
                //        Debug.Log("Tower1");
                //        GetComponent<Renderer>().material = materials[1];
                //        break;
                //    case 2:
                //        Debug.Log("Tower2");
                //        GetComponent<Renderer>().material = materials[2];
                //        break;
                //    case 3:
                //        Debug.Log("Tower3");
                //        GetComponent<Renderer>().material = materials[3];
                //        break;
                //    case 4:
                //        Debug.Log("Tower4");
                //        GetComponent<Renderer>().material = materials[4];
                //        break;
                //    case 5:
                //        Debug.Log("Tower5");
                //        GetComponent<Renderer>().material = materials[5];
                //        break;

                //    default:
                //        Debug.Log("Y no Tower?!?!");
                //        GetComponent<Renderer>().material = materials[0]; //Needed?
                //        return;
                //}
                tower.isVacant = false;
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
                GameManager.instance.towerToPlace = null;
                //TowerManager.selectedType = LevelMapper.instance.tileTypes[0];

            }
            else
            {
                tower.isSelected = true;
                GetComponent<MeshRenderer>().material.SetTexture("_MainTex", tower.textureSelected);
                Debug.Log("Selected");

                GameManager.instance.towerToPlace = this.gameObject;

                ////set type selcted variable tower/tile type(create one)
                //switch (tag)
                //{
                //    case "Tower1":
                //        GameManager.instance.selectedTowerType = 1;
                //        break;
                //    case "Tower2":
                //        GameManager.instance.selectedTowerType = 2;
                //        break;
                //    case "Tower3":
                //        GameManager.instance.selectedTowerType = 3;
                //        break;
                //    case "Tower4":
                //        GameManager.instance.selectedTowerType = 4;
                //        break;
                //    case "Tower5":
                //        GameManager.instance.selectedTowerType = 5;
                //        break;
                //    default:
                //        GameManager.instance.selectedTowerType = 0;
                //        break;
                //}

                //TowerManager.selectedType = LevelMapper.instance.tileTypes[1];
            }
        }
    }


    void RemoveSelection()
    {
        //Hack
        
        // if tower type selected is not nothing set tower/tile type on node
        switch (GameManager.instance.selectedTowerType)
        {

            case 1:
                Debug.Log("Tower1");
                GetComponent<Renderer>().material = materials[1];
                break;
            case 2:
                Debug.Log("Tower2");
                GetComponent<Renderer>().material = materials[2];
                break;
            case 3:
                Debug.Log("Tower3");
                GetComponent<Renderer>().material = materials[3];
                break;
            case 4:
                Debug.Log("Tower4");
                GetComponent<Renderer>().material = materials[4];
                break;
            case 5:
                Debug.Log("Tower5");
                GetComponent<Renderer>().material = materials[5];
                break;

            default:
                Debug.Log("Y no Tower?!?!");
                GetComponent<Renderer>().material = materials[0]; //Needed?
                return;
        }
    }

    void Fire()
    {

        //checked tower type, set bulletPrefab to approbraite prefab for that tower before shooting.

        Rigidbody bulletClone = (Rigidbody)Instantiate(bulletPrefab, bulletSpawn.position, transform.rotation);
        bulletClone.velocity = bulletSpawn.forward * bulletSpeed;
    }

}



