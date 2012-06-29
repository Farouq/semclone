#light

open System
open System.Xml

let xmlDoc = new XmlDocument()

// This is our first XML document; notice that it's fairly
// straightforward, just <person> elements, albeit with a
// little unnecesary structure around them
Console.WriteLine("===========> Loading XML v1...")
xmlDoc.LoadXml("<data>
	<item>
		<person gender=\"male\">
			<name>Ted Neward</name>
			<age>38</age>
			<languages>
				<language>English</language>
				<language>French</language>
				<language>C#</language>
				<language>Java</language>
				<language>F#</language>
				<language>Scala</language>
			</languages>
		</person>
	</item>
	<item>
		<person gender=\"male\">
			<name>Han Solo</name>
			<age>35</age>
			<languages>
				<language>Imperial Standard</language>
				<language>Wookiee</language>
			</languages>
		</person>
	</item>
	<item>
		<person gender=\"male\">
			<name>Gaius Baltar</name>
			<age>35</age>
			<languages>
				<language>English</language>
				<language>Cylon</language>
			</languages>
		</person>
	</item>
</data>")

/// This form just breaks the XML document down into Nodes
/// and Leafs and prints them out to console
let nodesleafs_print xmlDoc =
    let (|Node|Leaf|) (node : #System.Xml.XmlNode) =
        if node.HasChildNodes then
            Node (node.Name, seq { for x in node.ChildNodes -> x })
        else
            Leaf (node.InnerText)        

    let printXml node =
        let rec printXml indent node =
            match node with
            | Leaf (text) -> 
                printfn "%s%s" indent text
            | Node (name, nodes) ->
                printfn "%s%s:" indent name
                nodes |> Seq.iter (printXml (indent+"    "))
        printXml "" node
    printXml xmlDoc

Console.WriteLine("===========> nodesleafs_print xmlDoc:")
nodesleafs_print xmlDoc



/// This form breaks the XML down into Persons, Nodes or Leafs
/// and prints them to the console
let personnodesleafs_print xmlDoc =
    let (|Node|Leaf|Person|) (node : #System.Xml.XmlNode) =
        if node.Name = "person" then
            let pGender = node.Attributes.ItemOf("gender").Value
            let pName = node.Item("name").InnerText
            let pAge = Int32.Parse(node.Item("age").InnerText)
            let pLangNode = node.Item("languages")
            let pLangs = seq { for l in pLangNode.ChildNodes -> l.InnerText }
            Person (pName, pGender, pAge, pLangs)
        else if node.HasChildNodes then
            Node (node.Name, seq { for x in node.ChildNodes -> x })
        else
            Leaf (node.InnerText)        

    let printXml node =
        let rec printXml indent node =
            match node with
            | Person (n, g, a, ls) -> 
                printfn "%sPerson: %s is %s, %d, and speaks %d languages" 
                    indent n g a (Seq.length ls)
            | Leaf (text) -> 
                printfn "%s%s" indent text
            | Node (name, nodes) ->
                printfn "%s%s:" indent name
                nodes |> Seq.iter (printXml (indent+"    "))
        printXml "" node
    printXml xmlDoc

Console.WriteLine("===========> personnodesleafs_print xmlDoc:")
personnodesleafs_print xmlDoc



/// This form uses the multi-case active pattern to print
/// the contents of the XML document
let person_print xmlDoc =
    let (|Node|Person|) (node : #System.Xml.XmlNode) =
        if node.Name = "person" then
            let pGender = node.Attributes.ItemOf("gender").Value
            let pName = node.Item("name").InnerText
            let pAge = Int32.Parse(node.Item("age").InnerText)
            let pLangNode = node.Item("languages")
            let pLangs = seq { for l in pLangNode.ChildNodes -> l.InnerText }
            Person (pName, pGender, pAge, pLangs)
        else if (node.HasChildNodes) then
            Node (seq { for x in node.ChildNodes -> x})
        else
            failwith ("Unexpected data: " + node.ToString())
    let printXml node =
        let rec printXml node =
            match node with
            | Node (nodes) -> 
                nodes |> Seq.iter printXml
            | Person (n, g, a, ls) -> 
                printfn "Person: %s is %s, %d, and speaks %d languages" 
                    n g a (Seq.length ls)
        printXml node
    printXml xmlDoc

Console.WriteLine("===========> person_print xmlDoc:")
person_print xmlDoc



/// This form uses multi-case active patterns to extract the
/// data from the XML document into tuples of data
let person_tuples xmlDoc =
    let (|Node|Person|) (node : #System.Xml.XmlNode) =
        if node.Name = "person" then
            let pGender = node.Attributes.ItemOf("gender").Value
            let pName = node.Item("name").InnerText
            let pAge = Int32.Parse(node.Item("age").InnerText)
            let pLangNode = node.Item("languages")
            let pLangs = seq { for l in pLangNode.ChildNodes -> l.InnerText }
            Person (pName, pGender, pAge, pLangs)
        else if (node.HasChildNodes) then
            Node (seq { for x in node.ChildNodes -> x})
        else
            failwith ("Unexpected data: " + node.ToString())
    let extract node =
        let rec extract node =
            match node with
            | Person (n, g, a, ls) ->
                Seq.singleton (n, g, a, ls)
            | Node (nodes) ->
                Seq.collect (fun (n) -> extract n) nodes
        extract node
    let results = extract xmlDoc
    for r in results do
        Console.WriteLine("Result: {0}", r)

Console.WriteLine("===========> person_tuples xmlDoc:")
person_tuples xmlDoc



/// Person is a class type representing a Person from the
/// XML document
type Person(name : string, gender : string, 
            age : int, langs : seq<string>) =
    member p.Name with get() = name
    member p.Gender with get() = gender
    member p.Age with get() = age.ToString()
    member p.Languages with get() = langs
    override p.ToString() =
        String.Format("[Person: {0} is {1}, {2}," + 
            " and speaks {3}]",
            name, gender, age.ToString(),
                (Seq.reduce (fun (l) (s) ->
                    l + ", and " + s) langs) )
/// This form uses multi-case active patterns to extract
/// Person objects from the XML document
let person_objs xmlDoc =
    let (|Node|Person|) (node : #System.Xml.XmlNode) =
        if node.Name = "person" then
            let pGender = node.Attributes.ItemOf("gender").Value
            let pName = node.Item("name").InnerText
            let pAge = Int32.Parse(node.Item("age").InnerText)
            let pLangNode = node.Item("languages")
            let pLangs = seq { for l in pLangNode.ChildNodes -> l.InnerText }
            Person (new Person(pName, pGender, pAge, pLangs))
        else if (node.HasChildNodes) then
            Node (seq { for x in node.ChildNodes -> x})
        else
            failwith ("Unexpected data: " + node.ToString())
    let extract node =
        let rec extract node =
            match node with
            | Person (p) ->
                Seq.singleton p
            | Node (nodes) ->
                Seq.collect (fun (n) -> extract n) nodes
        extract node
    let results = extract xmlDoc
    for r in results do
        Console.WriteLine("Result: {0}", r)

Console.WriteLine("===========> person_objs xmlDoc:")
person_objs xmlDoc



// Whoops! New data! <ship> elements added to the document.
// Notice, just for effect, the <ship> is either at the
// top-level (next to <item> elements), or buried inside
// of an <item> element.
Console.WriteLine("===========> Loading XML v2 (with ships)...")
xmlDoc.LoadXml("<data>
	<item>
		<person gender=\"male\">
			<name>Ted Neward</name>
			<age>38</age>
			<languages>
				<language>English</language>
				<language>French</language>
				<language>C#</language>
				<language>Java</language>
				<language>F#</language>
				<language>Scala</language>
			</languages>
		</person>
	</item>
	<item>
		<person gender=\"male\">
			<name>Han Solo</name>
			<age>35</age>
			<languages>
				<language>Imperial Standard</language>
				<language>Wookiee</language>
			</languages>
		</person>
	</item>
	<ship jump=\"true\">
	    <name>Millenium Falcon</name>
	</ship>
	<item>
		<person gender=\"male\">
			<name>Gaius Baltar</name>
			<age>35</age>
			<languages>
				<language>English</language>
				<language>Cylon</language>
			</languages>
		</person>
	</item>
	<item>
    	<ship jump=\"true\">
	        <name>Galactica</name>
	    </ship>
	</item>
</data>")

/// Ship is another domain type from the XML document
type Ship(name : string, jumpCapable : bool) =
    member s.Name with get() = name
    member s.Jump with get() = jumpCapable
    override s.ToString() =
        String.Format("[Ship: {0}, jump={1}]", 
            name, jumpCapable.ToString())
/// This form uses multi-case active patterns to extract
/// either Persons or Ships from the XML document and
/// returns a sequence containing either/both for processing
let personship_objs xmlDoc =
    let (|Node|Person|Ship|) (node : #System.Xml.XmlNode) =
        if node.Name = "person" then
            let pGender = node.Attributes.ItemOf("gender").Value
            let pName = node.Item("name").InnerText
            let pAge = Int32.Parse(node.Item("age").InnerText)
            let pLangNode = node.Item("languages")
            let pLangs = seq { for l in pLangNode.ChildNodes -> l.InnerText }
            Person (new Person(pName, pGender, pAge, pLangs))
        else if (node.Name = "ship") then
            let sName = node.Item("name").InnerText
            let sJump = node.Attributes.ItemOf("jump").Value
            Ship (new Ship(sName, if sJump="true" then true else false))
        else if (node.HasChildNodes) then
            Node (seq { for x in node.ChildNodes -> x})
        else
            failwith ("Unexpected data: " + node.ToString())
    let extract node : seq<obj> =
        let rec extract node : seq<obj> =
            match node with
            | Ship (s) ->
                Seq.singleton (s :> obj)
            | Person (p) ->
                Seq.singleton (p :> obj)
            | Node (nodes) ->
                Seq.collect (fun (n) -> extract n) nodes
        extract node
    let results = extract xmlDoc
    for r in results do
        Console.WriteLine("Result: {0}", r)

Console.WriteLine("===========> personship_objs xmlDoc:")
personship_objs xmlDoc

                    

// Whoops! New, unknown data! Where'd those <fairyTalePrincess>
// elements come from? Who knows what else might show up?
// Ack!
// What's worse, notice how now there's a <ship> element buried
// inside the <item> element containing Gaius Baltar. Do these
// clients know no shame?
Console.WriteLine("===========> Loading XML v3 (with unknowns)...")
xmlDoc.LoadXml("<data>
	<item>
		<person gender=\"male\">
			<name>Ted Neward</name>
			<age>38</age>
			<languages>
				<language>English</language>
				<language>French</language>
				<language>C#</language>
				<language>Java</language>
				<language>F#</language>
				<language>Scala</language>
			</languages>
		</person>
	</item>
	<item>
		<person gender=\"male\">
			<name>Han Solo</name>
			<age>35</age>
			<languages>
				<language>Imperial Standard</language>
				<language>Wookiee</language>
			</languages>
		</person>
	</item>
	<ship jump=\"true\">
	    <name>Millenium Falcon</name>
	</ship>
	<fairyTalePrincess>
	    <name>Sleeping Beauty</name>
	    <ending>Happy</ending>
	</fairyTalePrincess>
	<fairyTalePrincess>
	    <name>Cinderella</name>
	    <ending>Happy</ending>
	</fairyTalePrincess>
	<item>
		<person gender=\"male\">
			<name>Gaius Baltar</name>
			<age>35</age>
			<languages>
				<language>English</language>
				<language>Cylon</language>
			</languages>
		</person>
	    <ship jump=\"true\">
	        <name>Galactica</name>
	    </ship>
	</item>
</data>")

/// This form uses partial-match active patterns to allow for
/// unknown elements in the XML to simply "fall out" through
/// the process, while still extracting the relevant bits back
/// into a sequence of domain objects.
let partialmatch_personship_objs xmlDoc =
    let (|Person|_|) (node : #System.Xml.XmlNode) =
        if node.Name = "person" then
            let pGender = node.Attributes.ItemOf("gender").Value
            let pName = node.Item("name").InnerText
            let pAge = Int32.Parse(node.Item("age").InnerText)
            let pLangNode = node.Item("languages")
            let pLangs = seq { for l in pLangNode.ChildNodes -> l.InnerText }
            Some(new Person(pName, pGender, pAge, pLangs))
        else
            None
    let (|Ship|_|) (node : #System.Xml.XmlNode) =
        if (node.Name = "ship") then
            let sName = node.Item("name").InnerText
            let sJump = node.Attributes.ItemOf("jump").Value
            Some (new Ship(sName, if sJump="true" then true else false))
        else
            None

    let extract node : seq<obj> =
        let rec extract node : seq<obj> =
            match node with
            | Ship (s) ->
                Seq.singleton (s :> obj)
            | Person (p) ->
                Seq.singleton (p :> obj)
            | node when node.HasChildNodes ->
                let children = seq { for n in node.ChildNodes -> n }
                Seq.collect (fun (n) -> extract n) children
            | node when node.Name = "fairyTalePrincess" ->
                Seq.empty
            | _ ->
                Seq.empty
        extract node
    let results = extract xmlDoc
    for r in results do
        match r with
        | :? Ship as s ->
            Console.WriteLine("The ship {0} {1}",
                s.Name,
                if s.Jump = true 
                    then "is jump-capable"
                    else "is slower-than-light")
        | :? Person as p ->
            Console.WriteLine("Found {0}", p.Name)
        | _ ->
            ()

Console.WriteLine("===========> partialmatch_personship_objs xmlDoc:")
partialmatch_personship_objs xmlDoc


(*
    let extract node : obj list =
        let rec extract node : obj list =
            match node with
            | Ship (s) ->
                [s :> obj]
            | Person (p) ->
                [p :> obj]
            | node when node.HasChildNodes ->
                let children = [ for n in node.ChildNodes -> n ]
                List.collect (fun (n) -> extract n) children
                //Seq.collect (fun (n) -> extract n) children
            | _ ->
                []
        extract node
    let results = extract xmlDoc
    for r in results do
        Console.WriteLine("Result: {0}", r)
*)
