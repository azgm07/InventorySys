using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderController : MonoBehaviour
{
    public float distance = 1;
    public float scaleFactor = 1;
    public Camera renderCamera;

    private void Awake() {
        GameController.Instance.COLLECT += OnCollect; 

        if(renderCamera == null)
        {
            renderCamera = GetComponentInChildren<Camera>();
        }   
    }


    //Render the object image and store it
    private void OnCollect(Transform transform) 
    {
        GameObject renderObject = transform.gameObject;

        GameObject gameObject = GameObject.Instantiate(renderObject, renderCamera.transform.position + (Vector3.forward*distance), Quaternion.identity) as GameObject;
        gameObject.transform.localScale = Vector3.one*scaleFactor;
        gameObject.transform.parent = this.transform;
        SetLayerRecursively(gameObject, 8);

        renderCamera.targetTexture = RenderTexture.GetTemporary(128, 128, 16);
        renderCamera.Render();

        RenderTexture saveActive = RenderTexture.active;
        RenderTexture.active = renderCamera.targetTexture;
        float width = renderCamera.targetTexture.width;
        float height = renderCamera.targetTexture.height;

        Texture2D texture = new Texture2D(128, 128);
        texture.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);

        texture.Apply();
        
        //Apply to inventory variable
        GameController.Instance.inventory.textureImage.Add(texture);

        //Release textures and destroy the object
        RenderTexture.active = saveActive;
        RenderTexture.ReleaseTemporary(renderCamera.targetTexture);
        GameObject.DestroyImmediate(gameObject);
    }

    void SetLayerRecursively(GameObject o, int layer)
    {
        foreach (Transform t in o.GetComponentsInChildren<Transform>(true))
        t.gameObject.layer = layer;
    }
}
