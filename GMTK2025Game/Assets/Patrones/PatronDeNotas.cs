using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevoPatronDeNotas", menuName = "Ritmo/PatronDeNotas")]
public class PatronDeNotas : ScriptableObject
{
    public KeyCode tecla;
    public List<KeyCode> notas;
    public float pausaDespues = 2f;
}
