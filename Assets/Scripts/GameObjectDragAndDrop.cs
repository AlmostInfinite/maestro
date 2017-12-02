using System.Collections;
using UnityEngine;

public class GameObjectDragAndDrop : MonoBehaviour {

    private bool up;
    private Vector3 startPosition;
    protected GameObject item;
    public GameObject targetItem;

    public LayerMask myLayerMask;

    //int towerType;


    void Awake()
    {
        item = this.gameObject;
        startPosition = item.transform.position;
    }

    private void Start()
    {
        //towerType = this.GetComponent<Tower>().towerType;
    }


    IEnumerator OnMouseDown()
    {
        up = false;
        while (up == false)
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Vector3 hitOffset = new Vector3(0, -0.5f, 0);
            if (Physics.Raycast(ray, out hit, 100, myLayerMask))
            {
                Debug.DrawLine(ray.origin, hit.point - hitOffset, Color.red);
                Vector3 pos = hit.point - hitOffset;
                item.transform.position = pos;
                if (hit.collider.gameObject.CompareTag("TowerNode")) //and not current object
                {
                    
                    targetItem = hit.collider.gameObject;
                }
                else
                {
                    targetItem = null;
                }
                print(targetItem);
            }

            yield return new WaitForEndOfFrame();
        }
    }

    void OnMouseUp()
    {
        up = true;
        item.transform.position = startPosition;
        if (targetItem != null)
        TowerManager.instance.HandleTowerPlacement(this.gameObject, targetItem);
    }

    public void Reset()
    {
        item.transform.position = startPosition;
    }
}

