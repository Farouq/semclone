using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

class Person
{
    private string m_first;
    private string m_last;
    private int m_age;

    public Person(string fn, string ln, int a)
    {
        FirstName = fn;
        LastName = ln;
        Age = a;
    }
    public string FirstName
    {
        get { return m_first; }
        set { m_first = value; }
    }
    public string LastName
    {
        get { return m_last; }
        set { m_last = value; }
    }
    public int Age
    {
        get { return m_age; }
        set { m_age = value; }
    }
}

delegate bool MajorApproval(Class cl);
class Student : Person
{
    private string m_major;
    private MajorApproval m_majorApproval;
    
    public Student(string fn, string ln, int a, string maj)
        : base(fn, ln, a)
    {
        Major = maj;
    }
    public string Major
    {
        get { return m_major; }
        set { m_major = value; }
    }

    public MajorApproval CanTake
    {
        get { return m_majorApproval; }
    }
}

class Instructor : Person
{
    private string m_dept;
    
    public Instructor(string fn, string ln, int a, string dept)
        : base(fn, ln, a)
    {
        Department = dept;
    }
    public string Department
    {
        get { return m_dept; }
        set { m_dept = value; }
    }
}

class Class
{
    private string m_name;
    private string m_field;
    private List<Student> m_students = new List<Student>();
    private Instructor m_instructor;
    
    public Class(string n, string f)
    {
        Name = n;
        Field = f;
    }
    public string Name
    {
        get { return m_name; }
        set { m_name = value; }
    }
    public string Field
    {
        get { return m_field; }
        set { m_field = value; }
    }
    public Instructor Instructor
    {
        get { return m_instructor; }
        set { m_instructor = value; }
    }
    public List<Student> Students
    {
        get { return m_students; }
    }
    public bool Assign(Student s)
    {
        if (s.CanTake(this))
        {
            Students.Add(s);
            return true;
        }
        return false;
    }
}

delegate U Accumulator<T, U>(T src, U rslt);
class Database<T> where T : class
{
    private List<T> data = new List<T>();
    public Database() { }

    public List<T> AsQuerayable { get { return data; } }

    public void Add(T i) { data.Add(i); }

    public IEnumerable<T> Filter(Predicate<T> pred)
    {
        List<T> results = new List<T>();
        foreach (T it in data)
            if (pred(it))
                results.Add(it);
        return results;
    }
    public IEnumerable<T> Filter2(Predicate<T> pred)
    {
        foreach (T it in data)
            if (pred(it))
                yield return it;
    }
    public IEnumerable<U> Map<U>(Func<T, U> transform)
    {
        List<U> results = new List<U>();
        foreach (T it in data)
            results.Add(transform(it));
        return results;
    }
    public IEnumerable<U> Map2<U>(Func<T, U> transform)
    {
        foreach (T it in data)
            yield return (transform(it));
    }
    public U Reduce<U>(U startValue, Accumulator<T, U> accum)
    {
        U result = startValue;
        foreach (T it in data)
            result = accum(it, result);
        return result;
    }

    public T Find(Predicate<T> algorithm)
    {
        return Filter(algorithm).GetEnumerator().Current;
    }
    public T[] FindAll(Predicate<T> algorithm)
    {
        return new List<T>(Filter(algorithm)).ToArray();
    }
}


class Program
{
    static Database<Student> Students = new Database<Student>();
    static Database<Instructor> Instructors = new Database<Instructor>();
    static Dictionary<string, MajorApproval> ProgramValidation =
        new Dictionary<string, MajorApproval>();

    static Program()
    {
        Instructors.Add(new Instructor("Al", "Scherer", 38, "Computer Science"));
        Instructors.Add(new Instructor("Albert", "Einstein", 50, "Physics"));
        Instructors.Add(new Instructor("Sigmund", "Freud", 50, "Psychology"));
        Instructors.Add(new Instructor("Aaron", "Erickson", 35, "Underwater Basketweaving"));
        Instructors.Add(new Instructor("Stuart", "Halloway", 41, "Computer Science"));

        Students.Add(new Student("Matthew", "Neward", 10, "Grade school"));
        Students.Add(new Student("Michael", "Neward", 16, "Video game design"));
        Students.Add(new Student("Charlotte", "Neward", 38, "Psychology"));
        Students.Add(new Student("Ted", "Neward", 39, "Computer Science"));
        Students.Add(new Student("Erin", "Erickson", 34, "Psychology"));

        ProgramValidation["ComputerScience"] = 
            c => c.Field == "Computer Science";
        ProgramValidation["Physics"] =
            c => c.Field == "Computer Science" || c.Field == "Physics";
        ProgramValidation["Psychology"] =
            c => c.Field == "Psychology" || c.Field == "Grade school";
        ProgramValidation["Grade school"] =
            c => false;
        ProgramValidation["Video game design"] =
            c => c.Field != "Underwater Basketweaving";
    }

    static void Main(string[] args)
    {
        List<Class> classesFor2010 = new List<Class>();
        classesFor2010.Add(new Class("Scala for .NET Developers", "Computer Science"));
        classesFor2010.Add(new Class("F# for .NET Developers", "Computer Science"));
        classesFor2010.Add(new Class("How to play pranks on teachers", "Grade school"));
        classesFor2010.Add(new Class("Baskets of the Lower Amazon", "Underwater basketweaving"));
        classesFor2010.Add(new Class("Child Psych", "Psychology"));
        classesFor2010.Add(new Class("Geek Psych", "Psychology"));

        Student s = null;
        while (s == null)
        {
            Console.Write("Please enter your first name:");
            string first = Console.ReadLine();
            Console.Write("\nPlease enter your last name:");
            string last = Console.ReadLine();
            s = Students.Find(c => c.FirstName == first && 
                                   c.LastName == last );
            if (s == null)
                Console.WriteLine("Sorry! Couldn't find you");
        }
        // Do something with s

        foreach (int a in 
            Students.Map(delegate(Student it) 
            { return it.Age; }))
        {
            Console.WriteLine(a);
        }

        int count =
            Students.Reduce(0, delegate(Student st, int acc)
                                   {
                                       return acc++;
                                   });
        int sumAges = 
            Students.Reduce(0, delegate(Student st, int acc)
                                   {
                                       return st.Age + acc;
                                   });
        float averageAge =
            (Students.Reduce(0, delegate(Student st, float acc)
                                    {
                                        return st.Age + acc;
                                    }))/
            (Students.Reduce(0, delegate(Student st, float acc)
                                    {
                                        return acc + 1;
                                    }));

        string studentXML =
            (Students.Reduce("<students>", delegate(Student st, string acc)
                                       {
                                           return acc +
                                                  "<student>" +
                                                  st.FirstName +
                                                  "</student>";
                                       })) + "</students>";

        count = 
            Students.AsQuerayable.Aggregate(0, (acc, st) => ++acc);
        sumAges = 
            Students.AsQuerayable.Aggregate(0, (acc, st) => st.Age + acc);
        averageAge =
            Students.AsQuerayable.Aggregate(0.0F, (acc, st) => ++acc)
            /
            Students.AsQuerayable.Aggregate(0.0F, (acc, st) => st.Age + acc);
    }

    static int Add(int x, int y) { return x+y; }
    static int Mult(int x, int y) { return x*y; }

    delegate int BinaryOp(int lhs, int rhs);
    static int Operate(int l, int r, BinaryOp op)
    {
        return op(l, r);
    }

    private delegate int Constant();
    private delegate int AddOperate(Constant c);

    static void MathExamples()
    {
        int x = 2;
        int y = 3;
        // using explicit anonymous methods
        int added = Operate(x, y, delegate(int l, int r) { return l+r;} );
        int multed = Operate(x, y, delegate(int l, int r) { return l*r; });
        // using lambda expressions
        int addedagain = Operate(x, y, (l, r) => l + r);
        int multedagain = Operate(x, y, (l, r) => l * r );
    }

    delegate int Operation(int l, int r);
    delegate InnerOp DelegateOp(int r);
    delegate int InnerOp(int l);
    delegate U Op<T1, T2, U>(T1 arg1, T2 arg2);
    delegate U Op<T1, U>(T1 arg1);
    static Op<T1, Op<T2, U>> Curry<T1, T2, U>(Op<T1, T2, U> fn)
    {
        return delegate(T1 arg1)
                   {
                       return delegate(T2 arg2)
                                  {
                                      return fn(arg1, arg2);
                                  };
                   };
    }
    static void MoreMathExamples()
    {
        int result = Add(2, 3);

        Operation add2 = delegate(int l, int r) { return l + r; };
        int result2 = add2(2, 3);

        DelegateOp add3 = delegate(int l)
                              {
                                  return delegate(int r)
                                             {
                                                 return l + r;
                                             };
                              };
        int result3 = add3(2)(3);

        Func<int,Func<int, int>> add4 = 
            delegate(int l)
                {
                    return delegate(int r)
                             {
                                 return l + r;
                             };
                };
        int result4 = add4(2)(3);

        Op<int, int, int> add5 = delegate(int l, int r) { return l + r; };
        int result5 = add5(2, 3);
        Op<int, Op<int, int>> curriedAdd = Curry(add5);
        int result6 = curriedAdd(2)(3);

        Op<int, int> increment = curriedAdd(1);
        int result7 = increment(increment(increment(2)));

        Op<int, int> constant = curriedAdd(0);
        int result8 = constant(5);

        var add9 = Curry(add5);
        int result9 = add9(2)(3);

        var x = 2;
        var y = 3;
        var add10 = add9(x)(y);

        var anotherResult = false;
        if (x == 2)
            anotherResult = true;
        else
            anotherResult = false;
        var yetAnotherResult = (x == 2) ? true : false;
        /*
        var thirdResult =
            switch (x)
            {
                case 0: "empty"; break;
                case 1: "one"; break;
                case 2: "two"; break;
                default: "many"; break;
            };

        var swap = delegate (l, r) {
            var temp = r; r = l; l = temp; 
        };
         */
    }

    /*
    class InferredPerson
    {
        public InferredPerson(var firstName, var lastName, var age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string FullName { get { return firstName + " " + lastName;  } }
    }
    Person talbott = new Person("Talbott", "Crowell", 29);
    Person olderTalbott = new Person(talbott.FirstName, talbott.LastName, talbott.Age + 1);
     */


}

