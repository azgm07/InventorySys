using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    public RawImage rawImage;
    public Text text;

    private void Awake() {
        if(rawImage == null)
        {
            rawImage = GetComponentInChildren<RawImage>();
            if(rawImage == null)
            {
                Debug.LogWarning("Error: InventoryButton "+ this.name +" don't have a RawImage component");
            }
        }
        if(text == null)
        {
            text = GetComponentInChildren<Text>();
            if(text == null)
            {
                Debug.LogWarning("Error: InventoryButton "+ this.name +" don't have a Text component");
            }
        }
    }
}
