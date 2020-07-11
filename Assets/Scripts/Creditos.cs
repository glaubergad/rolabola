using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour {

    public Button resetButton;


	// Use this for initialization
	void Start () {
		//Listener de ação de botão para chamada do Reset
        resetButton.onClick.AddListener(ResetGame);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ResetGame()
    {
		//Reinicia o jogo, recarregando a cena inicial MainGame
        SceneManager.LoadScene("MainGame");
    }
}
