using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorCarreteras : MonoBehaviour {

    public GameObject contenedorCallesGO;
    public GameObject[] contenedorCallesArray;

    public GameObject calleNueva;
    public GameObject calleAnterior;

    public float velocidad;
    public float tamanyoCalle;
    public bool inicioJuego;
    public bool juegoTerminado;

    int contadorCalles = 0;
    int numeroSelectorCalles;

    public Vector3 medidaLimitePantalla;
    public bool salioDePantalla;

    public GameObject mCamGO;
    public Camera mCamComp;

	// Use this for initialization
	void Start () {
        InicioJuego();
		
	}
	
	// Update is called once per frame
	void Update () {
        if (inicioJuego  && !juegoTerminado) {
            transform.Translate(Vector3.down * velocidad * Time.deltaTime);

            if (calleAnterior.transform.position.y + tamanyoCalle < medidaLimitePantalla.y && !salioDePantalla)
            {
                salioDePantalla = true;
                DestruyoCalles();

            }
        }  
	}

    void InicioJuego() {
        contenedorCallesGO = GameObject.Find("ContenedorCalles");
        mCamGO = GameObject.Find("MainCamera");
        mCamComp = mCamGO.GetComponent<Camera>();
        VelocidadMotorCarretera();
        MedirPantalla();
        BuscoCalles();


    }

    void VelocidadMotorCarretera() {
        velocidad = 18;
    }

    void BuscoCalles() {
        contenedorCallesArray = GameObject.FindGameObjectsWithTag("Calle");

        for (int i = 0; i < contenedorCallesArray.Length; i++) {
            contenedorCallesArray[i].gameObject.transform.parent = contenedorCallesGO.transform;
            contenedorCallesArray[i].gameObject.SetActive(false);
            contenedorCallesArray[i].gameObject.name = "CalleOFF_" + i;
        }

        CrearCalles();
    }

    void CrearCalles() {

        contadorCalles++;
        numeroSelectorCalles = Random.Range(0, contenedorCallesArray.Length);
        GameObject Calle = Instantiate(contenedorCallesArray[numeroSelectorCalles]);
        Calle.SetActive(true);
        Calle.name = "Calle"+contadorCalles;
        Calle.transform.parent = gameObject.transform;
        PosicionoCalles();
        Debug.Log("Crear");

    }

    void PosicionoCalles() {
        calleAnterior = GameObject.Find("Calle"+(contadorCalles-1));
        calleNueva = GameObject.Find("Calle"+contadorCalles);
        MidoCalle();
        calleNueva.transform.position = new Vector3(calleAnterior.transform.position.x,calleAnterior.transform.position.y+tamanyoCalle, 0);
        salioDePantalla = false;
        Debug.Log("Posicionar");


    }

    void MidoCalle() {
        for (int i = 0; i < calleAnterior.transform.childCount; i++) {

            if (calleAnterior.transform.GetChild(i).gameObject.GetComponent<Pieza>() != null) {
                float tamanyoPieza = calleAnterior.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
                tamanyoCalle += tamanyoPieza;

            }
            
        }

    }

    void MedirPantalla() {

        medidaLimitePantalla = new Vector3(0, mCamComp.ScreenToWorldPoint(new Vector3(0, 0, 0)).y - 0.5f, 0);

    }

    void DestruyoCalles() {
        Destroy(calleAnterior);
        tamanyoCalle = 0;
        calleAnterior = null;
        CrearCalles();

    }
}
