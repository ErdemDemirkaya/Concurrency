using System;

namespace Delegates
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<int, int> fun1 = x => {x++; return x; };

            int t1 = 5;
            int res1 = 0;
            res1 = fun1(t1);
            Console.WriteLine("[Test 1] result is {0} , local variable is {1}, function returns {2} ", res1, t1, fun1(t1));

            int t2 = 5;
            int res2 = 0;
            Func<int> fun2 = () => { t2++; return t2; };
            res2 = fun2();
            Console.WriteLine("[Test 2] result is {0} , local variable is {1}, function returns {2} ", res2, t2, fun2());


        }
    }
}
