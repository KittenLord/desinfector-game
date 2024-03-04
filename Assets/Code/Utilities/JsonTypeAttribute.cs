using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class JsonTypeAttribute : Attribute
{
    public string Alias { get; private set; }
    public JsonTypeAttribute(string alias) { Alias = alias; }
}