using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager main;
    public int moedas, nivel, maxNivel;
    public Coin[] coins;
    public bool doOnce;
    private void Awake()
    {
        if (main != null && main != this)
        {
            Destroy(gameObject);
        }
        else
        {
            main = this;
            DontDestroyOnLoad(main);
        }
    }
    private void Start()
    {
        AtualizarDados();
    }
    public void CapturarReferencia()
    {
        coins = FindObjectsOfType<Coin>();
    }
    public void AtualizarDados()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Valores");
        temp[0].GetComponent<TextMeshProUGUI>().text = nivel.ToString();
        temp[1].GetComponent<TextMeshProUGUI>().text = moedas.ToString();
        temp[2].GetComponent<Button>().onClick.AddListener(() => ReiniciarNivel());
    }
    public void MudarNivel()
    {
        if (nivel < maxNivel)
        {
            moedas = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            nivel++;
        }
        else
        {
            Recomecar();
        }
        AtualizarDados();
    }
    public void Recomecar()
    {
        nivel = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
