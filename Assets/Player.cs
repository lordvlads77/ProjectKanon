using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectSaga;
using UnityEngine.Android;

public class Player : MonoBehaviour
{
    // TODO Remove the line of code 11 in the future if there is no more use for this by the 7-12
    //public MouseItem mouseItem = new MouseItem();
    
    public PlayerActions _playerActions;
    public InventoryObject inventory;
    public InventoryObject equipment;

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            Item _item = new Item(item.item);
            if (inventory.AddItem(_item, 1))
            {
                Destroy(other.gameObject);
            }
        }
    }
    

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
}
