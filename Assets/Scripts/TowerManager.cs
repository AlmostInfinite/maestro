using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class TowerManager : MonoBehaviour
{


    public static TileType selectedType;

    public static TowerManager instance = null;

    public int selectedTowerType;

    public GameObject percussionPrefab, stringsPrefab, brassPrefab, windPrefab, keyboardPrefab;

    public Transform instrumentSpawn;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        //DontDestroyOnLoad(gameObject);


    }

    public void HandleTowerPlacement(GameObject selectedTower, GameObject targetTowerNode)
    {

        selectedTowerType = selectedTower.GetComponent<Tower>().towerType;
        instrumentSpawn = targetTowerNode.transform.Find("Spawn");

        Debug.Log (instrumentSpawn);

        Color selectedColor;

        switch (selectedTowerType)
        {
            case 0:
                selectedColor = Color.red;
                Instantiate(percussionPrefab, instrumentSpawn.position, instrumentSpawn.rotation);
                break;
            case 1:
                selectedColor = Color.blue;
                break;
            case 2:
                selectedColor = Color.yellow;
                break;
            case 3:
                selectedColor = Color.green;
                break;
            case 4:
                selectedColor = Color.cyan;
                break;
            default:
                Debug.Log("Error");
                return;
        }

        targetTowerNode.GetComponentInChildren<MeshRenderer>().material.SetColor("_Color", selectedColor);
        targetTowerNode.GetComponent<Tower>().towerType = selectedTowerType;
        targetTowerNode.GetComponent<Tower>().bulletPrefab = selectedTower.GetComponent<Tower>().bulletPrefab;




    }

}
