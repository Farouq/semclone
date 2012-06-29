module XmlHelpers
open System
open System.Xml.Linq

let getAttr (elem:XElement) attrString = elem.Attribute(XName.Get(attrString)).Value
