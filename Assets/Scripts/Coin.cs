using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Jogador"))
        {
            GameManager.main.moedas++;
            if(GameManager.main.moedas==GameManager.main.coins.Length)
            {
                GameManager.main.MudarNivel();
                GameManager.main.AtualizarDados();
            }
            else
            {
                Destroy(gameObject);
                GameManager.main.AtualizarDados();
            }
        }
    }
}
