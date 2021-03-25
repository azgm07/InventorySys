using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    //Attributes
    [HideInInspector] public List<GameObject> collected;


    //Constructor
    public Inventory()
    {
        collected = new List<GameObject>();
    }
}
