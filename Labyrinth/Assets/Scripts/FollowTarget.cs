using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log(transform.position);
    }
    private void Update()
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }

        StartCoroutine(WaitUntilDead());
        
    }

    private IEnumerator WaitUntilDead()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
