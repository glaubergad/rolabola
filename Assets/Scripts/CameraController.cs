using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;

    private Vector3 offset;

	// funcionalidade para manter a camera "presa ao player e se movendo acompanhando a Esfera"
	void Start () {
        offset = transform.position - player.transform.position;
	}
	
	// Reposiciona a camera frame a frame baseada na posição da Esfera
	void LateUpdate () {
        transform.position = player.transform.position + offset;
	}
}
