using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShieldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private PlayerControl player;
    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("down working");
        StartCoroutine(Protection());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("exit working");
        player.isProtected = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("up working");
        player.isProtected = false;
    }

    private IEnumerator Protection()
    {
        player.isProtected = true;
        yield return new WaitForSeconds(3);
        player.isProtected = false;
    }

}
