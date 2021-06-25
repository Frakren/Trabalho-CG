using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrigger : MonoBehaviour
{
    public Transform respawn;
    public GameObject tocha;
    Player player;
    RollerBall ball;
    private void Start()
    {
        int numero = Random.Range(0, 3);
        //ball = FindObjectOfType<RollerBall>();
        player = FindObjectOfType<Player>();
        respawn = GameObject.FindGameObjectWithTag("Res").transform;
        respawn.position = player.transform.position;
        //respawn.position = ball.transform.position;
        if (numero == 1 && tocha)
        {
            tocha.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jogador"))
        {
            player.transform.position = respawn.position;
        }
    }
}
