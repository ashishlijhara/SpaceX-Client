using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Vector3Double
{
    public double x;
    public double y;
    public double z;

    public Vector3Double(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3 ToFLoat()
    {
        return new Vector3((float)(x), (float)(y), (float)(z));
    }

    public static Vector3Double operator*(Vector3Double vec, double d)
    {
        return new Vector3Double(vec.x * d, vec.y * d, vec.z * d);
    }

    public static Vector3Double operator/(Vector3Double vec, double d)
    {
        return new Vector3Double(vec.x / d, vec.y / d, vec.z / d);
    }
}
