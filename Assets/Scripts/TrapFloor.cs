using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFloor : MonoBehaviour
{
    public Transform respawn;
    public Animator animator;
    Player player;
    RollerBall ball;
    private void Start()
    {
        int numero = Random.Range(0, 3);
        animator = transform.parent.GetComponent<Animator>();
        //ball = FindObjectOfType<RollerBall>();
        player = FindObjectOfType<Player>();
        respawn = GameObject.FindGameObjectWithTag("Res").transform;
        if (numero==1 && animator)
        {
            animator.SetTrigger("CanBee");
        }
        respawn.position = player.transform.position;
        //respawn.position = ball.transform.position;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Jogador") && !GameManager.main.doOnce)
        {
            GameManager.main.doOnce = true;
            Destroy(animator);
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
