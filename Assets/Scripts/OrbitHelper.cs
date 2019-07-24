using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class OrbitHelper
{
    public static double Deg2Rad = Math.PI / 180.0;
    public static int AU2KM = 149597871;

    public static Vector3Double OrbitalStateVectors(
            double semiMajorAxis,
            double eccentricity,
            double inclination,
            double longitudeOfAscendingNode,
            double argumentOfPeriapsis,
            double trueAnomaly
        )
    {
        double num1 = semiMajorAxis * (double)OrbitHelper.AU2KM * Math.Pow(10.0, -3.0);
        double d1 = inclination * OrbitHelper.Deg2Rad;
        double num2 = longitudeOfAscendingNode * OrbitHelper.Deg2Rad;
        double num3 = argumentOfPeriapsis * OrbitHelper.Deg2Rad;
        double d2 = trueAnomaly * OrbitHelper.Deg2Rad;
        double num4 = 1.0 - eccentricity * eccentricity;
        double num5 = num1 * num4 / (1.0 + eccentricity * Math.Cos(d2));
        double num6 = Math.Acos((eccentricity + Math.Cos(d2)) / (1.0 + eccentricity * Math.Cos(d2)));
        Math.Atan2(Math.Sqrt(1.0 - eccentricity * eccentricity) * Math.Sin(num6), Math.Cos(num6) - eccentricity);
        return new Vector3Double(num5 * (Math.Cos(d2 + num3) * Math.Cos(num2) - Math.Sin(d2 + num3) * Math.Cos(d1) * Math.Sin(num2)), num5 * (Math.Cos(d2 + num3) * Math.Cos(num2) + Math.Sin(d2 + num3) * Math.Cos(d1) * Math.Sin(num2)), num5 * Math.Sin(d2 + num3) * Math.Cos(d1));
    }
}
