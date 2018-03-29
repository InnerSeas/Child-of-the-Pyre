using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    private bool playerMoving;
    private float playerDirX;
    private float playerDirZ;
    private Vector3 lastMove;

    public GameObject heroine;
    private Animator heroineAnim;

    // Use this for initialization
    void Start () {
        //le script de mouvement appartien a l'objet "Heroine Position", qui ne contient pas d'animator
        //On cherche donc la réference a l'objet fils "Heroine" qui le contient affin de pouvoir 
        //donner les information necessaire a l'animator.
        heroine = GameObject.Find("Heroine");
        heroineAnim = heroine.GetComponent<Animator>();
        
    }
	
	// Update is called once per frame
	void Update () {

        playerMoving = false; // si le joueur bouge, ce sera mis à TRUE, mais par défaut c'est FALSE


        // MOUVEMENT VIA LES COMMANDES AXIALES (JOYSTICK, CLAVIER)
        // Si un des axes est différent de 0, il y a mouvement !
        if (Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f)
        {
            // On corrige les axes des boutons pour que ça corresponde à ce que voit le joueur, tout en restant sur l'axe
            // On utilise 0.7 pour les diagonales car 0.7² = 0.49, et 0.98 est suffisamment proche de 1 pour que ça n'ait pas d'importance.
            // Si on utilisait 1 le personnage irait super vite en diagonale. (on utilise des VECTEURS)
            if (Input.GetAxisRaw("Horizontal") > 0f) {

                if (Input.GetAxisRaw("Vertical") > 0f) {
                    playerDirZ = 0f;
                    playerDirX = 1f;
                }
                else if (Input.GetAxisRaw("Vertical") < 0f) {
                    playerDirZ = -1f;
                    playerDirX = 0f;
                }
                
                else
                {
                    playerDirZ = -0.7f;
                    playerDirX = 0.7f;
                }
            }
            else if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                if (Input.GetAxisRaw("Vertical") > 0f)
                {
                    playerDirZ = 1f;
                    playerDirX = 0f;
                }
                else if (Input.GetAxisRaw("Vertical") < 0f)
                {
                    playerDirZ = 0f;
                    playerDirX = -1f;
                }
                
                else
                {
                    playerDirZ = 0.7f;
                    playerDirX = -0.7f;
                }
            }
            else {
                //Monter consiste en une translation X/Z
                if (Input.GetAxisRaw("Vertical") > 0f)
                {
                    playerDirZ = 0.7f;
                    playerDirX = 0.7f;
                }
                //descendre consiste en une translation -X/-Z
                else if (Input.GetAxisRaw("Vertical") < 0f)
                {
                    playerDirZ = -0.7f;
                    playerDirX = -0.7f;
                }
            }


            // On applique un vecteur de translation correspondant exactement à la direction, multiplié par la vitesse. 
            transform.Translate(new Vector3(playerDirX * moveSpeed * Time.deltaTime, 0, playerDirZ * moveSpeed * Time.deltaTime));
            // Note : Comme update est appelé toutes les frames, et non toutes les X millisecondes, et que les frames peuvent varier en temps, 
            // on multiplie par le temps de la dernière frame pour déterminer de combien il faut se déplacer. (si la dernière frame a mis du temps, on se déplacera davantage)
            // cela normalise la vitesse/seconde. 

            playerMoving = true; // on indique qu'à cette frame, le joueur bouge
            lastMove = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); // on enregistre le dernier mouvement effectué, on en aura besoin !


        }
        heroineAnim.SetFloat("PlayerDirX", playerDirX);
        heroineAnim.SetFloat("PlayerDirZ", playerDirZ);
        heroineAnim.SetBool("PlayerMoving", playerMoving);
        heroineAnim.SetFloat("LastMoveX", lastMove.x);
        heroineAnim.SetFloat("LastMoveY", lastMove.z);
    }
}
