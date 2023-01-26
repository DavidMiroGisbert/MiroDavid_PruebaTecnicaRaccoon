using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptManager : MonoBehaviour
{
    public List<GameObject> salidaRay; //Lista que almacena la punta de las pistolas desde donde sale el raycast
    public List<Slider> vidasObjetivosUI; //Lista que almacena todas las barras de vida que se ven en la UI
    bool cooldownArmaCorta, cooldownArmaLarga; //Bools para comprobar si se pueden disparar cada una de las armas o no
    public List<GameObject> dianas; //Lista que almacena los objetivos

    //Variables para el sonido del disparo
    public AudioSource source;
    public AudioClip sonidoDisparo;

    public void Start()
    {
        cooldownArmaCorta = true;
        cooldownArmaLarga = true;
    }
    //------------Asignación del valor máximo de los sliders así como la asignación de su valor al máximo------------//
    public void AsignarValorSliders()
    {
        vidasObjetivosUI[0].maxValue = Objetivos.objetivo1.vida;
        vidasObjetivosUI[1].maxValue = Objetivos.objetivo2.vida;
        vidasObjetivosUI[2].maxValue = Objetivos.objetivo3.vida;
        vidasObjetivosUI[0].value = Objetivos.objetivo1.vida;
        vidasObjetivosUI[1].value = Objetivos.objetivo2.vida;
        vidasObjetivosUI[2].value = Objetivos.objetivo3.vida;
    }

    //------------Función para el disparo------------//
    public void DisparoPorRayCast()
    {
        /*Disparo mediante Raycast que tiene en cuenta el lugar de origen (punta del cañón), la dirección, el hit (si golpea en algo),
        la distancia hasta la que llega que en este caso viene dado por el arma y la layer a la que afecta*/


        RaycastHit hit;

                            //------------Disparo con arma larga------------//

        if (salidaRay[0].tag == "ArmaLarga" && cooldownArmaLarga == true) //Comprobación de si puede disparar y con qué arma lo hace
        {
            source.PlayOneShot(sonidoDisparo);
            StartCoroutine(Cooldown());//Inicio del courutine para que no se pueda spamear el disparo

            if (Physics.Raycast(salidaRay[0].transform.position, salidaRay[0].transform.forward, out hit, Armas.armaLargoAlcance.distancia, 1<<3))
            {
                //------------Comprobaciones de a qué objetivo ha dado------------//

                if (hit.transform.tag == "Objetivo1")
                {
                    Objetivos.objetivo1.vida = Objetivos.objetivo1.vida - Armas.armaLargoAlcance.danyo;
                }

                if (hit.transform.tag == "Objetivo2")
                {
                    Objetivos.objetivo2.vida = Objetivos.objetivo2.vida - Armas.armaLargoAlcance.danyo;
                }

                if (hit.transform.tag == "Objetivo3")
                {
                    Objetivos.objetivo3.vida = Objetivos.objetivo3.vida - Armas.armaLargoAlcance.danyo;
                }

                //------------Llamada a funciones para actualizar la vida o si ha muerto------------//

                Muerte();
                RestarVidaUI();
            }

        }

                        //------------Disparo con arma corta------------//

        if (salidaRay[1].tag == "ArmaCorta" && cooldownArmaCorta == true) //Comprobación de si puede disparar y con qué arma lo hace
        {
            source.PlayOneShot(sonidoDisparo);
            StartCoroutine(Cooldown()); //Inicio del courutine para que no se pueda spamear el disparo

            if (Physics.Raycast(salidaRay[1].transform.position, salidaRay[1].transform.forward, out hit, Armas.armaCortoAlcance.distancia, 1 << 3)) 
            {
                //------------Comprobaciones de a qué objetivo ha dado------------//

                if (hit.transform.tag == "Objetivo1")
                {
                    Objetivos.objetivo1.vida = Objetivos.objetivo1.vida - Armas.armaCortoAlcance.danyo;
                }

                if (hit.transform.tag == "Objetivo2")
                {
                    Objetivos.objetivo2.vida = Objetivos.objetivo2.vida - Armas.armaCortoAlcance.danyo;
                }

                if (hit.transform.tag == "Objetivo3")
                {
                    Objetivos.objetivo3.vida = Objetivos.objetivo3.vida - Armas.armaCortoAlcance.danyo;
                }

                //------------Llamada a funciones para actualizar la vida o si ha muerto------------//

                Muerte();
                RestarVidaUI();
            }
        }


    }

    //------------Courutine para el cooldown que cambia el tiempo según el cooldown del arma------------//
    IEnumerator Cooldown()
    {
        if (cooldownArmaLarga == true)
        {
            cooldownArmaLarga = false;
            yield return new WaitForSeconds(Armas.armaLargoAlcance.cooldown);
            cooldownArmaLarga = true;
        }
        if (cooldownArmaCorta == true)
        {
            cooldownArmaCorta = false;
            yield return new WaitForSeconds(Armas.armaCortoAlcance.cooldown);
            cooldownArmaCorta = true;
        }
    }

    //------------Función para comprobar si a los objetivos les queda vida y en caso contrario destruirlos------------//
    void Muerte()
    {
        if (Objetivos.objetivo1.vida <= 0)
        {
            Destroy(dianas[0]);
        }

        if (Objetivos.objetivo2.vida <= 0)
        {
            Destroy(dianas[1]);
        }

        if (Objetivos.objetivo3.vida <= 0)
        {
            Destroy(dianas[2]);
        }
    }

    //------------Función para actualizar la vida que se muestra por UI------------//
    void RestarVidaUI()
    {
        vidasObjetivosUI[0].value = Objetivos.objetivo1.vida;
        vidasObjetivosUI[1].value = Objetivos.objetivo2.vida;
        vidasObjetivosUI[2].value = Objetivos.objetivo3.vida;
    }
}
