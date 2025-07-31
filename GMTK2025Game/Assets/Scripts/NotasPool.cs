using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class NotasPool : MonoBehaviour
{
    public static NotasPool Instancia;

    [Header("Prefabs de Notas en orden")]
    public GameObject[] notasPrefabs;

    [Header("Cantidad inicial de cada tipo")]
    public int cantidadInicial = 10;

    private Dictionary<KeyCode, Queue<GameObject>> pools = new Dictionary<KeyCode, Queue<GameObject>>();

    private KeyCode[] teclas = { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow };

    void Awake()
    {
        Instancia = this;

       
        for (int i = 0; i < teclas.Length; i++)
        {
            Queue<GameObject> cola = new Queue<GameObject>();

            for (int j = 0; j < cantidadInicial; j++)
            {
                GameObject obj = Instantiate(notasPrefabs[i]);
                obj.SetActive(false);
                obj.GetComponent<Nota>().teclasAsignada = teclas[i];
                cola.Enqueue(obj);
            }

            pools.Add(teclas[i], cola); 
        }
    }


    public GameObject ObtenerNota(KeyCode tecla)
    {
        if (pools.ContainsKey(tecla))
        {
            
            if (pools[tecla].Count > 0)
            {
                GameObject nota = pools[tecla].Dequeue();
                nota.SetActive(true);
                return nota;
            }
            else
            {
                
                int index = tecla == KeyCode.UpArrow ? 0 :
                            tecla == KeyCode.DownArrow ? 1 :
                            tecla == KeyCode.LeftArrow ? 2 : 3;

                GameObject nuevaNota = Instantiate(notasPrefabs[index]);
                nuevaNota.GetComponent<Nota>().teclasAsignada = tecla;
                return nuevaNota;
            }
        }

        Debug.LogWarning("No hay pool para la tecla: " + tecla);
        return null;
    }

    
    public void RetornarNota(GameObject nota, KeyCode tecla)
    {
        nota.SetActive(false);
        if (!pools.ContainsKey(tecla))
            pools[tecla] = new Queue<GameObject>();

        pools[tecla].Enqueue(nota);
    }
}
