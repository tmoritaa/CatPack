using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public static class Vector2Extension
{
    public static Vector2 Rotate(this Vector2 vec, float degAngle) {
        float radAngle = degAngle * Mathf.PI / 180f;
        float x = vec.x;
        float y = vec.y;

        return new Vector2(x * Mathf.Cos(radAngle) - y * Mathf.Sin(radAngle), x * Mathf.Sin(radAngle) + y * Mathf.Cos(radAngle));
    }
}