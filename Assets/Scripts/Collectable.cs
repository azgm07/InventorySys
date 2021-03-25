using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Collectable : MonoBehaviour
{
    [Header("Options")]
    //Timer to turn off the highlight
    public float timer = 0.5f;

    //Private variables
    private bool highlightEnabled = false;
    private Outline outline;

    private void Start() 
    {
        //Listener
        GameController.Instance.RAYCAST_PLAYER_HIT += OnRaycastPlayerHit;
    }

    private void OnDestroy() 
    {
        //Remove Listener
        GameController.Instance.RAYCAST_PLAYER_HIT -= OnRaycastPlayerHit;
    }

    private void OnEnable() 
    {
        //Get the outline script
        outline = this.GetComponent<Outline>();
        if(outline == null)
        {
            outline = this.gameObject.AddComponent<Outline>();
        }
    }

    //Getter and Setter that handles timer property
    public bool HighlightEnabled
    {
        get 
        {
            return highlightEnabled ;
        }

        set
        {
            if(value == highlightEnabled)
            {
                return;
            }

            highlightEnabled = value;

            if(highlightEnabled)
            {
                TurnHighlightOn();
            }
            else
            {
                TurnHighlightOff();
            }
        }
    }

    //Coroutine that starts timer
    private IEnumerator SetTimerToOff()
    {
        yield return new WaitForSeconds(timer);
        HighlightEnabled = false;
    }

    //Turn on the highlight
    private void TurnHighlightOn()
    {
        //Turn highlight on
        outline.enabled = true;
    }
    private void TurnHighlightOff()
    {
        //Turn highlight off
        outline.enabled = false;
    }

    //Event receiver
    private void OnRaycastPlayerHit(Transform hit)
    {
        if(hit == this.transform)
        {
            StopAllCoroutines();
            HighlightEnabled = true;
        }
        else
        {
            StartCoroutine(SetTimerToOff());
        }
    }
}
