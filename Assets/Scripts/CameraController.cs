using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 pos;
    


    private void Awake()
    {
        if (!player)
            player = FindObjectOfType<Hero>().transform;
        
    }

    private void Update()
    {
        pos = player.position;
        pos.z = -10f;
        pos.y += 1f;

        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime*4);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
