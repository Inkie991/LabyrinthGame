using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private GameObject deadPlayersParticlesPrefab;
    [SerializeField] private ParticleSystem confettiPrefab;
    [SerializeField] private Material[] playerMaterials;
    private List<Vector3> myPath;
    private float speed = 7.0f;
    private float maxDistance = 0.25f;
    private bool isStarted = false;
    private int pathIndex = -1;
    public bool isProtected = false;
    public bool isFinished = false;
    MeshRenderer mesh;


    public void PlayerStart(List<Vector3> path)
    {
        myPath = path;
        pathIndex = path.Count - 2;
        mesh = gameObject.GetComponent<MeshRenderer>();
        StartCoroutine(Wait());
    }

    private void Update()
    {

        if (isStarted == true && pathIndex >= 0)
        {
            var point = myPath[pathIndex];
            transform.position = Vector3.MoveTowards(transform.position, point, speed * Time.deltaTime);
            float distanceSqr = (transform.position - point).sqrMagnitude;
            if (distanceSqr < maxDistance)
            {
                pathIndex -= 1;
            }
        }

        if(mesh == null)
        {
            mesh = gameObject.GetComponent<MeshRenderer>();
        }

        if (isProtected == true)
        {
            mesh.material = playerMaterials[1];
        } else
        {
            mesh.material = playerMaterials[0];
        }

        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.SphereCast (ray, 0.7f, out hit))
        {
            if (hit.transform.gameObject.CompareTag("Trap Zone") && !isProtected)
            {
                PlayerDeath();
            }
        }

        float distanceToFinish = (transform.position - myPath[0]).sqrMagnitude;
        if (distanceToFinish < maxDistance)
        {

            StartCoroutine(PlayerWin());
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerDeath();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(PlayerWin());
        }
    }

    /*private void PlayerWin()
    {
        var confetti = Instantiate(confettiPrefab, transform.position, Quaternion.Euler(-90, 0, 0));
        confetti.Play();
    }*/

    private void PlayerDeath()
    {
        speed = 0f;
        GameObject deadPlayer = Instantiate(deadPlayersParticlesPrefab, transform.position, transform.rotation);
        Debug.Log(transform.position);
        gameObject.SetActive(false);
        isStarted = false;
        if (deadPlayer != null)
        {
            Debug.Log(deadPlayer.name);
        }
        Destroy(this.gameObject);
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        isStarted = true;
    }

    private IEnumerator PlayerWin()
    {
        var confetti = Instantiate(confettiPrefab, transform.position, Quaternion.Euler(-90, 0, 0));
        confetti.Play();
        yield return new WaitForSeconds(3);
        isFinished = true;
    }
}
