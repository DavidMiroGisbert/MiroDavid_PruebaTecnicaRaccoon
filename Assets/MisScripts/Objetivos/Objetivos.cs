using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objetivos : MonoBehaviour
{
    //------------Herencias del script objetivo------------//
    public static ObjetivosParent objetivo1;
    public static ObjetivosParent objetivo2;
    public static ObjetivosParent objetivo3;

    
    private ScriptManager valorSliders; 

    void Start()
    {
        objetivo1 = new ObjetivosParent(100);
        objetivo2 = new ObjetivosParent(200);
        objetivo3 = new ObjetivosParent(300);

        //------------Llamada a la función del manager una vez se han seteado las vidas------------//
        valorSliders = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScriptManager>();
        valorSliders.AsignarValorSliders();
    }
}
