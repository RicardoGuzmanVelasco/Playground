﻿using UnityEngine;

public static class Bridge
{
    public static (int x, int y) ToTuple(this Vector2Int vector) => (vector.x, vector.y);
}