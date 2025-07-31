using UnityEngine;
using System.Collections;


public class Nota : MonoBehaviour
{
    public KeyCode teclasAsignada;
    public bool interactiva = false;

    public float velocidad = 2f; // Ajusta la velocidad a lo que prefieras
    public AudioClip SonidoPresionar;

    private void OnEnable()
    {
        interactiva = false;
        StartCoroutine(HabilitarInteraccion());
    }

    private void Update()
    {
        transform.Translate(Vector3.down * velocidad * Time.deltaTime);
    }

    private IEnumerator HabilitarInteraccion()
    {
        yield return new WaitForSeconds(0.1f);
        interactiva = true;
    }
}
