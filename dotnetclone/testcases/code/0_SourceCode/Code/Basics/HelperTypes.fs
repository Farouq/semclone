#light

module HelperTypes

open System

type public Person(firstName : string, lastName : string, age : int) =
    interface IComparable with
        member p.CompareTo(other : obj) : int =
            match other with
            | :? Person as otherP ->
                let result = lastName.CompareTo(otherP.LastName)
                if result = 0 then
                    firstName.CompareTo(otherP.FirstName)
                else
                    result
            | _ ->
                raise (new System.ArgumentException("Not a Person"))
    member p.FirstName = firstName
    member p.LastName = lastName
    member p.Age = age
    
    override p.Equals(obj : obj) =
        (p :> IComparable).CompareTo(obj) = 0
    override p.ToString() = 
        System.String.Format("[Person: FirstName={0}, LastName={1}, Age={2}]",
            firstName, lastName, age)
    override p.GetHashCode() =
        (p.FirstName.GetHashCode() * 10) + p.LastName.GetHashCode()
