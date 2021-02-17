using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject focus;

    public void LateUpdate() // <------ @owengretzinger, thanks for the suggestion.
    {
        /* Cool smoothing that ignores the z position. */
        Vector3 vec = Vector3.Lerp((Vector2)transform.position, (Vector2)focus.transform.position, 0.0075f);
        vec.z = -10;
        transform.position = vec;
    }
}
