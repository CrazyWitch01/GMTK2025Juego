using UnityEngine;

public class HitZoneManager : MonoBehaviour
{
    public AudioSource audioSource; // Asegúrate de asignarlo desde el Inspector

    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.UpArrow)) VerificarNota(KeyCode.UpArrow);
        if (Input.GetKeyDown(KeyCode.DownArrow)) VerificarNota(KeyCode.DownArrow);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) VerificarNota(KeyCode.LeftArrow);
        if (Input.GetKeyDown(KeyCode.RightArrow)) VerificarNota(KeyCode.RightArrow);
    }

    void VerificarNota(KeyCode tecla)
    {
        GameObject[] notas = GameObject.FindGameObjectsWithTag("Nota");

        foreach (GameObject nota in notas)
        {
            Nota n = nota.GetComponent<Nota>();
            float distancia = Mathf.Abs(nota.transform.position.y - transform.position.y);

            if (n.teclasAsignada == tecla && distancia < 0.5f && n.interactiva)
            {
                Debug.Log("Perfect");

                if (n.SonidoPresionar != null && audioSource != null)
                    audioSource.PlayOneShot(n.SonidoPresionar);

                Destroy(nota);
                return;
            }
        }

        Debug.Log("Missed");
    }
}