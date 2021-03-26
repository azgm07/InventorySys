using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    //Singleton
    private static GameController _instance;
    public static GameController Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameController>();
            }

            if(_instance == null)
            {
                Instantiate(new GameObject("GameController")).AddComponent<GameController>();
            }

            return _instance;
        }
    }

    //Event actions
    public UnityAction<Transform> COLLECT;
    public UnityAction<Transform> RAYCAST_PLAYER_HIT;

    //Public variables
    public Inventory inventory;

    private void Awake() {
        DontDestroyOnLoad(this);
        inventory = new Inventory();

        //Add Methods to UnityActions
        COLLECT += OnCollect;
        RAYCAST_PLAYER_HIT += OnRaycastPlayerHit;
    }

    //UnityAction Methods
    private void OnCollect(Transform transform)
    {
        Debug.Log("UnityAction COLLECT Invoked to: " + transform.name);
        inventory.collected.Add(transform.gameObject);
        transform.gameObject.SetActive(false);
    }

    private void OnRaycastPlayerHit(Transform transform)
    {
        //Debug.Log("UnityAction RAYCAST_PLAYER_HIT Invoked to: " + transform.name);
    }
}
