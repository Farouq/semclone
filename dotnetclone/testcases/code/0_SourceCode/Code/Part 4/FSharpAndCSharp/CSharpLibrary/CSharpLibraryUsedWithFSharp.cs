using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpLibrary
{
    public class CSharpLibraryUsedWithFSharp
    {
        public int SomeMutableProperty { get; set; }
        public int AnotherMutableProperty { get; set; }

        public delegate int BinaryIntegerMathOperation(int op1, int op2);



        public CSharpLibraryUsedWithFSharp()
        {
            
        }

        public CSharpLibraryUsedWithFSharp(int someProperty, int anotherProperty)
        {
            SomeMutableProperty = someProperty;
            AnotherMutableProperty = anotherProperty;
        }

        public bool SomeFunction(int firstParam, int secondParam)
        {
            return true;
        }

        public int PerformAMathOperationOnMyProperties(BinaryIntegerMathOperation operation)
        {
            return operation(SomeMutableProperty, AnotherMutableProperty);
        }

        public bool FunctionWithOptionalParameters(int firstParam, string secondParam, string thirdParam = "I am optional")
        {
            return true;
        }

        public delegate void BeerFinishingHandler(string nameOfBeer);
        public event BeerFinishingHandler FinishedABeer;

        public void HaveADrinkingBinge()
        {
            if (FinishedABeer != null)
            {
                FinishedABeer("Belgian Trappist");
                FinishedABeer("Three Philosophers");
                FinishedABeer("Red Hook");
                FinishedABeer("Stella");
                FinishedABeer("Another Stella");
                //as time goes by, standards get lower
                FinishedABeer("Pabst Blue Ribbon");
                FinishedABeer("Schlitz");
                //throw a type mismatch exception?
                FinishedABeer("Zima");
            }
        }
    }
}
