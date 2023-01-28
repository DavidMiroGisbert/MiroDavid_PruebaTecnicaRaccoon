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

    RaycastHit hit; //Sirve para detectar colisiones del raycast

    public List <LineRenderer> trayectoriaDisparo; //Lista para dibujar los rayos de las armas y as� ver d�nde hemos disparado y el alcance de las armas
    public void Start()
    {
        cooldownArmaCorta = true;
        cooldownArmaLarga = true;

    }

    //------------Asignaci�n del valor m�ximo de los sliders as� como la asignaci�n de su valor al m�ximo------------//
    public void AsignarValorSliders()
    {
        vidasObjetivosUI[0].maxValue = Objetivos.objetivo1.vida;
        vidasObjetivosUI[1].maxValue = Objetivos.objetivo2.vida;
        vidasObjetivosUI[2].maxValue = Objetivos.objetivo3.vida;
        vidasObjetivosUI[0].value = Objetivos.objetivo1.vida;
        vidasObjetivosUI[1].value = Objetivos.objetivo2.vida;
        vidasObjetivosUI[2].value = Objetivos.objetivo3.vida;
    }

    //------------Funciones para el disparo------------//

    /*Disparo mediante Raycast que tiene en cuenta el lugar de origen (punta del ca��n), la direcci�n, el hit (si golpea en algo),
      la distancia hasta la que llega que en este caso viene dado por el arma y la layer a la que afecta.
      En todo momento comprueba si ha dado al objetivo, a otro objeto o al aire para poder dibujar una l�nea que sirva de gu�a*/

    public void DisparoArmaLarga()
    {
                            //------------Disparo con arma larga------------//


        if (cooldownArmaLarga == true) //Comprobaci�n de si puede disparar
        {
                source.PlayOneShot(sonidoDisparo);

            cooldownArmaLarga = false;

            StartCoroutine(DibujarRayo());//Inicio del courutine para dibujar el disparo
            StartCoroutine(Cooldown());//Inicio del courutine para que no se pueda spamear el disparo

            //------------Comprobaci�n de si ha golpeado al objetivo------------

            if (Physics.Raycast(salidaRay[0].transform.position, salidaRay[0].transform.forward, out hit, Armas.armaLargoAlcance.distancia, 1<<3))
            {

                //------------Comprobaciones de a qu� objetivo ha dado------------//

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

    }
    public void DisparoArmaCorta()
    {

        //------------Disparo con arma corta------------//

        if (cooldownArmaCorta == true) //Comprobaci�n de si puede disparar 
        {

            source.PlayOneShot(sonidoDisparo);

            cooldownArmaCorta = false;

            StartCoroutine(DibujarRayo());//Inicio del courutine para dibujar el disparo
            StartCoroutine(Cooldown());//Inicio del courutine para que no se pueda spamear el disparo


            //------------Comprobaci�n de si ha golpeado al objetivo------------

            if (Physics.Raycast(salidaRay[1].transform.position, salidaRay[1].transform.forward, out hit, Armas.armaCortoAlcance.distancia, 1 << 3))
            {
                //------------Comprobaciones de a qu� objetivo ha dado------------//

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


    //------------Courutine para el cooldown que cambia el tiempo seg�n el cooldown del arma------------//
    IEnumerator Cooldown()
    {
        if (cooldownArmaLarga == false)
        {
            yield return new WaitForSeconds(Armas.armaLargoAlcance.cooldown);
            cooldownArmaLarga = true;
        }
        if (cooldownArmaCorta == false)
        {
            yield return new WaitForSeconds(Armas.armaCortoAlcance.cooldown);
            cooldownArmaCorta = true;
        }
    }

    //------------Courutine para dibujar un rayo que muestra la trayectoria del raycast------------//
    IEnumerator DibujarRayo()
    {
        if (cooldownArmaLarga == false)
        {
            trayectoriaDisparo[0].enabled = true; //Hace visible el rayo
            trayectoriaDisparo[0].SetPosition(0, salidaRay[0].transform.position); //Define el punto de origen de la l�nea
            trayectoriaDisparo[0].SetPosition(1, salidaRay[0].transform.forward * Armas.armaLargoAlcance.distancia); //Define el punto de destino de la l�nea

            yield return new WaitForSeconds(0.1f);

            trayectoriaDisparo[0].enabled = false; //Desactiva el rayo
        }
        if (cooldownArmaCorta == false)
        {
            trayectoriaDisparo[1].enabled = true;
            trayectoriaDisparo[1].SetPosition(0, salidaRay[1].transform.position);
            trayectoriaDisparo[1].SetPosition(1, salidaRay[1].transform.forward * Armas.armaCortoAlcance.distancia);

            yield return new WaitForSeconds(0.1f);

            trayectoriaDisparo[1].enabled = false;
        }
    }

    //------------Funci�n para comprobar si a los objetivos les queda vida y en caso contrario destruirlos------------//
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

    //------------Funci�n para actualizar la vida que se muestra por UI------------//
    void RestarVidaUI()
    {
        vidasObjetivosUI[0].value = Objetivos.objetivo1.vida;
        vidasObjetivosUI[1].value = Objetivos.objetivo2.vida;
        vidasObjetivosUI[2].value = Objetivos.objetivo3.vida;
    }
}
