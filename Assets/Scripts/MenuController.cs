using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public RectTransform inventoryMenu;
    public RectTransform crosshair;
    public RectTransform minimap;

    public RectTransform buttonArea;

    public GameObject buttonPrefab;

    private void Awake() {
        inventoryMenu.gameObject.SetActive(false);
        crosshair.gameObject.SetActive(true);
        minimap.gameObject.SetActive(true);
    }


    private void Update() {

        if(Input.GetKeyDown(KeyCode.E))
        {
            inventoryMenu.gameObject.SetActive(!inventoryMenu.gameObject.activeInHierarchy);
            crosshair.gameObject.SetActive(!crosshair.gameObject.activeInHierarchy);
            minimap.gameObject.SetActive(!minimap.gameObject.activeInHierarchy);

            if(inventoryMenu.gameObject.activeInHierarchy)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                SetInventory();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

    }

    private void SetInventory()
    {
        for (int i = 0; i < GameController.Instance.inventory.collected.Count; i++)
        {
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(buttonArea);
            InventoryButton invButton = button.GetComponent<InventoryButton>();

            RawImage rawImage;
            Text text;

            if(invButton == null)
            {
                Debug.LogError("Error on setting the Inventory: prefab loaded invalid.");
                return;
            }

            rawImage = invButton.rawImage;
            text = invButton.text;

            GameObject collected = GameController.Instance.inventory.collected[i];
            Texture2D texture = GameController.Instance.inventory.textureImage[i];
            
            text.text = collected.name;
            rawImage.texture = texture;

            button.GetComponent<Button>().onClick.AddListener(delegate{OnClickButton(button, collected, texture, i);});
        }
    }

    private void OnClickButton(GameObject button, GameObject collected, Texture2D texture , int indexCollectable)
    {
        Destroy(button);
        GameController.Instance.inventory.collected.RemoveAt(indexCollectable);
        GameController.Instance.inventory.textureImage.RemoveAt(indexCollectable);
    }
}
