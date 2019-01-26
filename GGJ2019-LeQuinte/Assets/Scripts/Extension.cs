using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

static class Extension
{

    public static int Next(this int previous)
    {
        int max = GameManager.Instance.Vertexes.Count;

        return (previous + 1 >= max) ? 0 : previous + 1;
    }
    public static int Previous(this int next)
    {
        int max = GameManager.Instance.Vertexes.Count;

        return (next - 1 < 0) ? max - 1 : next - 1;
    }
}