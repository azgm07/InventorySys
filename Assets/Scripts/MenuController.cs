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

    public float spawnDistance = 1f;
    public float spawnHeight = 2f;

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
        foreach (Transform child in buttonArea) {
            GameObject.Destroy(child.gameObject);
        }
        
        for (int i = 0; i < GameController.Instance.inventory.collectables.Count; i++)
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

            CollectableItem item = GameController.Instance.inventory.collectables[i];
            
            text.text = item.collected.name;
            rawImage.texture = item.textureImage;

            button.GetComponent<Button>().onClick.AddListener(delegate{OnClickButton(button, item);});
        }
    }

    private void OnClickButton(GameObject button, CollectableItem item)
    {
        Destroy(button);
        GameController.Instance.inventory.collectables.Remove(item);

        Transform cameraTransform = GameController.Instance.player.cameraPlayer.transform;

        Rigidbody itemRb = item.collected.GetComponent<Rigidbody>();
        itemRb.isKinematic = false;
        itemRb.useGravity = true;

        Vector3 pos = new Vector3(cameraTransform.position.x, spawnHeight, cameraTransform.position.z);
        
        item.collected.transform.position = cameraTransform.forward * spawnDistance + pos;
        item.collected.transform.rotation = cameraTransform.rotation;

        item.collected.SetActive(true);
    }
}
