using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour {

	//Codigo para executar musica de fundo
    private void Awake()
    {
		//Checa se musica já nao foi instanciada. Se já houver qualquer instancia da musica, destroi novas execuções caso cena seja recarregada
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Music");
        if (obj.Length > 1)
            Destroy(this.gameObject);
		
		
		//Mantem a musica rodando mesmo que outra cena seja carregada
        DontDestroyOnLoad(this.gameObject);
    }
}
