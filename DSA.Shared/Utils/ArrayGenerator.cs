using System;

namespace DSA.Shared.Utils;

public static class ArrayGenerator
{
    private static readonly Random _random = new ();

    public static int[] Generate(int minSize = 5, int maxSize = 15)
    {
        int size = _random.Next(minSize, maxSize + 1);
        int[] array = new int[size];

        int minValue, maxValueExclusive;

        minValue = -10;
        maxValueExclusive = 11;

        for (int i = 0; i < size; i++)
            array[i] = _random.Next(minValue, maxValueExclusive);

        return array;
    }
}
