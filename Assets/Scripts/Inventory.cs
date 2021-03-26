using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Custom type
public class CollectableItem
{
    public GameObject collected;
    public Texture2D textureImage;
}

public class Inventory
{
    //Attributes
    [HideInInspector] public List<CollectableItem> collectables;


    //Constructor
    public Inventory()
    {
        collectables = new List<CollectableItem>();
    }
}
