using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//classe aplicada ao player Esfera, comum a todas as cenas do jogo

public class Comportamento : MonoBehaviour {
	
	//Instancia do som do click ao tocar um PickUp
    public AudioSource tickSound;
    
	//RB para aplicação de física
	public Rigidbody rb;
	
	//Fator de multiplicação da velocidade da Esfera
    private float speed = 20;
    
	//Contador auxiliar para Score
	private int contador;
	
	//Quantidade máxima de Pickups em cena
    public int maxPickups;
	
	//Especificação da próxima cena a ser carregada
    public string nextScene;
	
	//Tempo total para finalização do jogo em caso de não recolher todos os pickups
    private float totalTime = 65;
	
	//Boolean para identificar que o telefone está parado em posição flat
    private bool isFlat = true;
	
	//Variavel booleana de jogo para uso do scoreboard e aplicação de física
    private bool isPlaying;
	
	//Elementos do canvas
    public Text scoreBoard;
    public Text winText;
    public Text timer;
    public Button resetButton;
    public Button nextButton;

	
	
    private void Start()
    {
        
		//Inicialização de objetos a cada inicio de cena
		rb = GetComponent<Rigidbody>();
        tickSound = GetComponent<AudioSource>();
        isPlaying = true;
        contador = 0;
        AjustaScoreboard();
        winText.text = "";
        resetButton.onClick.AddListener(ResetLevel);
        nextButton.onClick.AddListener(NextLevel);
        resetButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);

    }


    private void FixedUpdate()
    {
        
		//Configurações relacionadas à movimentação do player Esfera usando Joystick ou o teclado
		float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal,0.0f,moveVertical);
		
		//caso o jogo esteja em curso, admite a aplicação de uma força para movimento da esfera
        if(isPlaying)
        rb.AddForce(movement*speed);
    }

    private void Update()
    {
        
		//Instruções relativas ao controle do player por acelerometros de celular
        Vector3 tilt = new Vector3(Input.acceleration.x,Input.acceleration.y,0.0f);
        
		//Se o telefone estiver "flat", gira eixo X, garantindo que telefone esta na posição inicial para aplicacao de força
		if (isFlat)
            tilt = Quaternion.Euler(90, 0, 0) * tilt;
        
		
		//caso jogo esteja em curso, admite controle da Esfera
		if(isPlaying)
        rb.AddForce(tilt * speed * 1.5f);
	
		//Se o tempo total estiver em Zero, para o jogo e carrega GameOver()
        if (totalTime < 0.01f)
        {
            isPlaying=false;
            GameOver();
        }
		
		//caso jogo esteja em curso, trabalha as variaveis para exibição de score e tempo
        if (isPlaying)
        {
            totalTime -= Time.deltaTime;
            timer.text = totalTime.ToString("f");
			
			//Roda metodo de ajuste do Score e Tempo
            AjustaScoreboard();
        }
        
 
    }

    private void OnTriggerEnter(Collider other)
    {
		//Caso jogo esteja em curso, admite a desativação do pickup em caso de colisão com o player
        if (isPlaying)
        {
			//Checa se colisão "sentida" foi com um PickUp
            if (other.gameObject.CompareTag("PickUp"))
            {
                
				//Toca som, desativa o pickup tocado, acrescenta 1 ao score e exibe em tela
				tickSound.Play();
                other.gameObject.SetActive(false);
                contador = contador + 1;
                AjustaScoreboard();
            }
        }
    }

    private void AjustaScoreboard()
    {
        scoreBoard.text = "Score: " + contador;
        

        if(contador >= maxPickups)
        {
            winText.text = "Você Venceu!!!";
            isPlaying = false;
            nextButton.gameObject.SetActive(true);
        }
    }

    private void GameOver()
    {
        winText.text = "Fim de Jogo!!!";
        timer.text = "0.00";
        isPlaying = false;
        resetButton.gameObject.SetActive(true);
        
    }


    private void NextLevel()
    {
        SceneManager.LoadScene(nextScene);
    }

    private void ResetLevel()
    {
        SceneManager.LoadScene("MainGame");
    }

}

