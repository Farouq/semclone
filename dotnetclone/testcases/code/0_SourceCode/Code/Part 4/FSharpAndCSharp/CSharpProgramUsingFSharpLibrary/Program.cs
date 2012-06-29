using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FSharpLibraryUsedWithCSharp;
using Microsoft.FSharp.Core;

namespace CSharpProgramUsingFSharpLibrary
{
    class Program
    {
        public enum EnumColors { Red, Green, Blue}
        static void Main(string[] args)
        {
            var someFSharpObject = new FSharpLibrary();

            var simpleFunction = someFSharpObject.MethodCallNoParameters();
            var withoutParmsIAmAProperty = someFSharpObject.NoParamsMakesMeAReadOnlyProperty;
            
            //this wont work, as I am a setter
            //someFSharpObject.NoParamsMakesMeAReadOnlyProperty = "23";

            //note tuple form and non-tuple form both become simple parameters in C#
            var withParameters = someFSharpObject.MethodCallWithParameters(1, 2);
            var withParametersTupleForm = someFSharpObject.MethodCallWithTupleFormParameters(1, 2);
            //worth mentioning that this does *not* work
            //var passingATupleAsParameters = someFSharpObject.MethodCallWithTupleFormParameters(new Tuple<int, int>(1, 2));
            var someTuple = someFSharpObject.MemberThatReturnsATuple();
            var firstValue = someTuple.Item1;
            var secondValue = someTuple.Item2;
            var thirdValue = someTuple.Item3;
            var lastValue = someTuple.Item4;

            //return of a record
            var someRecord = someFSharpObject.MemberReturnsARecord();
            var firstName = someRecord.FirstName;
            var lastName = someRecord.LastName;
            var age = someRecord.Age;

            //creation of a record (notice the generated ctor)
            var aPerson = new PersonRecord("Matthew", "Erickson", 8);

            //calling a member that takes a function as a parameter
            var uglyFunc = FuncConvert.ToFSharpFunc<int, FSharpFunc<int, int>>(
                a => FuncConvert.ToFSharpFunc<int,int>(b => a + b)
            );
            var operationResult = someFSharpObject.MemberThatTakesAFunction(2, 2, uglyFunc);

            var betterResult = someFSharpObject.IsBetterForCSharpInterop(2, 2, (a, b) => a + b);

            var shouldBeBlue = someFSharpObject.ReturnADiscriminatedUnion();
            var test = shouldBeBlue == CoolColors.Blue;

            //getting back an option
            var mightHaveSomething = someFSharpObject.ReturnAnOption();
            var someNullable = 
                mightHaveSomething == FSharpOption<int>.None 
                  ? new int?() 
                  : mightHaveSomething.Value;
        }
    }
}
