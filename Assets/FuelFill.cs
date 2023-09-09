using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;

public class FuelFill : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("OnCollisionEnter");
        //if (col.gameObject.name == "Player")
        {
            ReturnToSpaceShipEvent evt = Events.ReturnToSpaceShipEvent;
            EventManager.Broadcast(evt);
        }
    }

    void OnCollisionStay(Collision col)
    {
        ReturnToSpaceShipEvent evt = Events.ReturnToSpaceShipEvent;
        EventManager.Broadcast(evt);
    }
}
