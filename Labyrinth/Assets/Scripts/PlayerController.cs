using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private HintFinder hintFinder;

    private GameObject player;

    private static PlayerController _instance;

    private void Update()
    {
        if (player == null)
        {
            player = Instantiate(playerPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity);
            SetPlayersPath();
        }
    }

    public void SetPlayersPath()
    {
        if (player != null)
        {
            player.GetComponent<PlayerControl>().PlayerStart(hintFinder.FindPath());
        }
        
    }


}
