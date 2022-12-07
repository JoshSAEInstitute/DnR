using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{

    private List<Key.KeyType> keyList;

    private void Awake()
    {
        keyList = new List<Key.KeyType>();
    }

    private void AddKey(Key.KeyType keyType)
    {
        //Add key in list
        Debug.Log("Added key: " + keyType);
        keyList.Add(keyType);
    }

    public void RemoveKey(Key.KeyType keyType)
    {
        //Remove key in list
        keyList.Remove(keyType);
    }

    public bool ContainsKey(Key.KeyType keyType)
    {
        //Returns the keys within the list
        return keyList.Contains(keyType);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //This checkes if the player has collided with an object with a "key" component in them, if they do, they key is saved within the list
        Key key = collider.GetComponent<Key>();
        if(key != null)
        {
            AddKey(key.GetKeyType());
            Destroy(key.gameObject);
        }

        DoorHinge keyDoor = collider.GetComponent<DoorHinge>();
        if(keyDoor != null)
        {
            //Check if player has the key to
            if(ContainsKey(keyDoor.GetKeyType()))
            {
                //Currently holding key to open this door
                keyDoor.OpenDoor();
            }
        }
    }

}
