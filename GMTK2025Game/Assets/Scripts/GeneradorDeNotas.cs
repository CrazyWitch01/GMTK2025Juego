using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GeneradorDeNotas : MonoBehaviour
{
    public PuntoSpawn[] SpawnPoints;
    public float intervaloEntreNotas = 0.5f;

    public List<PatronDeNotas> patronesDisponibles;

    private KeyCode teclaAnterior = KeyCode.None;
    private bool esperandoEntrada = true;
    private bool ejecutandoPatron = false;

    private bool esperandoInicio = true;

    void Update()
    {
        if (esperandoEntrada && !ejecutandoPatron)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) ProcesarInput(KeyCode.UpArrow);
            if (Input.GetKeyDown(KeyCode.DownArrow)) ProcesarInput(KeyCode.DownArrow);
            if (Input.GetKeyDown(KeyCode.LeftArrow)) ProcesarInput(KeyCode.LeftArrow);
            if (Input.GetKeyDown(KeyCode.RightArrow)) ProcesarInput(KeyCode.RightArrow);
        }
    }

    void ProcesarInput(KeyCode tecla)
    {
        if (tecla == teclaAnterior)
        {
            Debug.Log("¡Fallaste! No puedes repetir la misma tecla.");
            return;
        }

        PatronDeNotas patron = patronesDisponibles.Find(p => p.tecla == tecla);

        if (patron != null)
        {
            esperandoEntrada = false;
            ejecutandoPatron = true;
            teclaAnterior = tecla;
            StartCoroutine(EjecutarPatron(patron));
        }
        else
        {
            Debug.LogWarning("No hay patrón asignado a la tecla " + tecla);
        }
    }

    IEnumerator EjecutarPatron(PatronDeNotas patron)
    {
        foreach (KeyCode nota in patron.notas)
        {
            GenerarNota(nota);
            yield return new WaitForSeconds(intervaloEntreNotas);
        }

        yield return new WaitForSeconds(patron.pausaDespues);

        ejecutandoPatron = false;
        esperandoEntrada = true;
        Debug.Log("Patrón finalizado. Presiona otra tecla diferente.");
    }

    void GenerarNota(KeyCode tecla)
    {
        GameObject nota = NotasPool.Instancia.ObtenerNota(tecla);

        Transform punto = ObtenerPuntoDeSpawn(tecla);
        nota.transform.position = punto.position;
        nota.transform.rotation = Quaternion.identity;
    }

    Transform ObtenerPuntoDeSpawn(KeyCode tecla)
    {
        foreach (PuntoSpawn p in SpawnPoints)
        {
            if (p.tecla == tecla)
                return p.punto;
        }
        Debug.LogWarning("No se encontró punto de spawn para: " + tecla);
        return transform;
    }
}
[System.Serializable]
public class PuntoSpawn
{
    public KeyCode tecla;
    public Transform punto;
}
