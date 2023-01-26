using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armas : MonoBehaviour
{
    //Este script se utiliza para obtener las variables del script ArmaParent y asignarles variables a las herencias (Son static puesto que no van a cambiar)
    public static ArmaParent armaLargoAlcance;
    public static ArmaParent armaCortoAlcance;

    void Start()
    {
            armaLargoAlcance = new ArmaParent(10, 10, 0.5f);
        
            armaCortoAlcance = new ArmaParent(50, 4.5f, 1.5f);          
    }
}
