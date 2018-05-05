using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cronometro : MonoBehaviour {

    public GameObject motorCarreterasGo;
    public MotorCarreteras motorCarreterasScript;

    public float tiempo;
    public float distancia;
    public Text txtTiempo;
    public Text txtDistancia;

	// Use this for initialization
	void Start () {
        motorCarreterasGo = GameObject.Find("MotorCarreteras");
        motorCarreterasScript = motorCarreterasGo.GetComponent<MotorCarreteras>();

        txtTiempo.text = "2:00";
        txtDistancia.text = "0";
        tiempo = 120;
		
	}
	
	// Update is called once per frame
	void Update () {

        if (motorCarreterasScript.inicioJuego && !motorCarreterasScript.juegoTerminado) {
            CalculoTiempoDistancia();
        }

        if (tiempo <= 0 && !motorCarreterasScript.juegoTerminado) {
            motorCarreterasScript.juegoTerminado = true;
        }

    }

    void CalculoTiempoDistancia()
    {
        distancia += Time.deltaTime * motorCarreterasScript.velocidad;
        txtDistancia.text = ((int)distancia).ToString();

        tiempo -= Time.deltaTime;
        int minutos = (int)tiempo / 60;
        int segundos = (int)tiempo % 60;
        txtTiempo.text = minutos.ToString() + ":" + segundos.ToString().PadLeft(2, '0');
    }
}
