using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

	public GameObject painelPause;

	public bool estaPausado = false;

	
	public void QuitGame(){
		Application.Quit();
	}

	public void Carregador(string name){
		SceneManager.LoadScene (name);
	}


	public void Pause(){

		if(estaPausado){
		
		painelPause.SetActive(false);
		estaPausado = false;
		Time.timeScale = 1;
		
		}else{
			
			painelPause.SetActive(true);
			estaPausado = true;
			Time.timeScale = 0;
		}

	}

	
}
