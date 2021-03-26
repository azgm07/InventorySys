using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    //Attributes
    [HideInInspector] public List<GameObject> collected;
    [HideInInspector] public List<Texture2D> textureImage;


    //Constructor
    public Inventory()
    {
        collected = new List<GameObject>();
        textureImage = new List<Texture2D>();
    }
}
