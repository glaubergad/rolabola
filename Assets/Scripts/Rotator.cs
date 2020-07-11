using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {


	void Update () {
		//comando faz a rotação dos PickUps para torna-los mais destacados em tela
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);

    }
}
