using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceController : MonoBehaviour
{

    public Scoring ScoreScript;


    void start ()
    {
        ScoreScript = GetComponent<Scoring>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Dead");

        if (collision.GetComponent<TowerProjectile>().projectileType == this.GetComponentInParent<AudienceUnit>().instrumentType)
        {
            
            Vector3 seatPos = LevelMapper.instance.GetRandomSeatPos();

			Poof ();// Insert spawn particle effect here @ seatpos
            Instantiate(LevelMapper.instance.seatedUnitToSpawn, seatPos, LevelMapper.instance.seatedUnitToSpawn.transform.rotation);
			Instantiate(GameManager.instance.poofPrefab, seatPos, LevelMapper.instance.seatedUnitToSpawn.transform.rotation);
            ScoreScript.playerScore++;
            Debug.Log("score");

			Poof();// Insert spawn particle effect here @ transform.position
			Destroy(transform.parent.gameObject);


        }

    }

	void Poof()
	{
		Instantiate (GameManager.instance.poofPrefab,transform.position,transform.rotation);
	}

    //TODO - Move to seat?? - destroy object?? - (move "pawn" from unit make unit spawn from a pos instead.)

}
