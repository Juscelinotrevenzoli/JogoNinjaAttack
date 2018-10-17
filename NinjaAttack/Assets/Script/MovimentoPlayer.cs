using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPlayer : MonoBehaviour {
	
	private Rigidbody2D rb;
	private Transform tr;
	private Animator an;
	public Transform verificaChao;
	public Transform verificaParede;
	private bool estaAndando;
	private bool estaNoChao;
	private bool estaNaParede;
	private bool estaVivo;
	
	
	
	public float velocidade;
	public float forcaPulo;
	public float raioValidaChao;
	public float raioValidaParede;
	public LayerMask solido;
	
	
   
    
	int numeroToques;
	float direita = 1;
	float esquerda = -1;
	float metadeTela = Screen.width/2;
	
	float maxTimeWait = 0.1f;
	
	float contadorTempo;
	int contadorToque;
	float newTime;
	bool viradoParaDireita;


	void Start () {

		numeroToques = 0;

		rb = GetComponent<Rigidbody2D> ();
		tr = GetComponent<Transform> ();
		an = GetComponent<Animator> ();

		estaVivo = true;
		numeroToques = 0;
		contadorToque = 0;
		

	}
	
	
	void Update () {

		

		estaNoChao = Physics2D.OverlapCircle (verificaChao.position, raioValidaChao, solido);
		estaNaParede = Physics2D.OverlapCircle (verificaParede.position, raioValidaParede, solido);

		

		  //movimentação do personagem direita ou esquerda.
		  //se estiver em alguma direção e der dois toques na tela ele faz o pulo
	if(estaVivo){
		Animacao();
		
		if(Input.touchCount == 1){
			Touch toque = Input.touches[0];
			
			
			if(toque.phase == TouchPhase.Stationary){
				
				if(toque.position.x > metadeTela){      //indentifica o toque de alguma dos dois lado da tela
					
					 Mover(direita);

					if (direita>0){
						
						Touch doisToque = Input.touches[1];
						

						if(doisToque.position.x < metadeTela){
							
							if (doisToque.phase == TouchPhase.Began) {
                 				contadorToque += 1;
             				}

			 				if (contadorToque == 1){
				 
				 					newTime = Time.time + maxTimeWait;

							}else if(contadorToque == 2 && contadorTempo < maxTimeWait){

									Pular();
				
									contadorToque = 0;

							}

						}

					}

			
				}else if(toque.position.x < metadeTela){
					Mover(esquerda);

					if(esquerda <0){
						
						
						Touch doisToque = Input.touches[1];

						if(doisToque.position.x > metadeTela){
							
							if (doisToque.phase == TouchPhase.Began) {
                 				contadorToque += 1;
             				}

			 				if (contadorToque == 1){
				 
				 					newTime = Time.time + maxTimeWait;

							}else if(contadorToque == 2 && contadorTempo < maxTimeWait){

									Pular();
				
									contadorToque = 0;

							}

						}
						
					}
				}
				
				
		
			}

		}

		

			//parte do codigo que faz o personagem pular

			if (Time.time > newTime) {			//e o contador de tempo, conta o tempo des do ultimo toque
             contadorToque = 0;
        	 }
		}
		
	}

	
	void FixedUpdate(){
		//parte do codigo que faz o personagem pular
		//devido a recomendações quando for trabalhar com componentes de fisica utilize fixedupdate.

		if (Time.time > newTime) {			//e o contador de tempo, conta o tempo des do ultimo toque
             contadorToque = 0;
         }

		if (Input.touchCount == 1){
			
			Touch touch = Input.touches[0];			//primeiro toque
			
			

			if (touch.phase == TouchPhase.Began) { 
                 contadorToque += 1;
             }

			 if (contadorToque == 1){
				 
				 newTime = Time.time + maxTimeWait;

			}else if(contadorToque == 2 && contadorTempo < maxTimeWait){

				Pular();
				
				contadorToque = 0;

			}
		}

	}

	void Animacao(){
		
		an.SetBool ("Pulando", !estaNoChao);
		an.SetFloat ("VelVertical", rb.velocity.y);
	}
	
	
	
	void Pular(){
			if(estaNoChao){
				rb.AddForce (tr.up * forcaPulo);
			}
	}

	

	void Mover(float movimento)
	{
		if(movimento < 0 || movimento >0 ){
			estaAndando = true; 
	
		}else if( movimento == 0){
			estaAndando = false;
		}

		

		if(estaAndando == true){
			
			GetComponent<Animator>().SetBool("Correndo", true);

		}else if(estaAndando == false){
			GetComponent<Animator>().SetBool("Correndo", false);

		}

	
	
		 	Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

			rigidbody.velocity = new Vector2(movimento*velocidade, rigidbody.velocity.y);

		
		
		//tr.Translate(velocidade * Time.deltaTime*movimento,0,0);   //outra forma de fazer o movimento

	
		if(movimento < 0){
			GetComponent<SpriteRenderer>().flipX = true;
		}else if(movimento > 0){
			GetComponent<SpriteRenderer>().flipX = false;
		}
		
	
	}

	

	//função para debug do solo e parede

	void OnDrawGizmosSelected(){

		Gizmos.color = Color.red;

		Gizmos.DrawWireSphere(verificaChao.position, raioValidaChao);
		Gizmos.DrawWireSphere(verificaParede.position, raioValidaParede);

	}

	


}
