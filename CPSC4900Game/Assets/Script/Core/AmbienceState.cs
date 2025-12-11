using System.Collections;
using System.Collections.Generic;
// Scripts/Core/AmbienceState.cs
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName="AmbienceState", menuName="Liminal/Ambience State")]
public class AmbienceState : ScriptableObject {
    public bool Hum;      // true when fluorescent hum is on
    public bool Static;   // true when static is audible
    public bool Followed; // true when Echo is present
    public UnityEvent OnChange;

    public void SetHum(bool v){ if (Hum!=v){ Hum=v; OnChange?.Invoke(); } }
    public void SetStatic(bool v){ if (Static!=v){ Static=v; OnChange?.Invoke(); } }
    public void SetFollowed(bool v){ if (Followed!=v){ Followed=v; OnChange?.Invoke(); } }
}
