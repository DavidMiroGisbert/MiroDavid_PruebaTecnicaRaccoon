using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaParent
{
    public int danyo; //Se trata de un int porque quiero que quite la vida con números enteros aunque también podría ser un float
    public float distancia; //tanto la distancia como el cooldown son float para poder usar decimales
    public float cooldown; 

    public ArmaParent(int danyo, float distancia, float cooldown) //Constructor para pasar las variables en la herencia
    {
        this.danyo = danyo;
        this.distancia = distancia;
        this.cooldown = cooldown;
    }
}

