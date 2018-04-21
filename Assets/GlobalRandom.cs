using System;
using System.Collections.Generic;
using System.Linq;

public class GlobalRandom
{
    private static Random random = null;

    public static int GetRandomNumber(int min, int max) {
        if (random == null) {
            init();
        }

        return random.Next(min, max);
    }

    private static void init() {
        if (random == null) {
            random = new Random();
        }
    }
}