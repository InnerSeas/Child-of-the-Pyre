using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour {

    private PlayerController heroinePos;


	// Use this for initialization
	void Start () {
        //on déplace l'héroine sur le point de départ;
        heroinePos = FindObjectOfType<PlayerController>();
        heroinePos.transform.position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
