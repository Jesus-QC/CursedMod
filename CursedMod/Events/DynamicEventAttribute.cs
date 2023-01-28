/*using System;
using System.Reflection;

namespace CursedMod.Events;

public class DynamicEventAttribute : Attribute
{
    public EventInfo Method;

    public DynamicEventAttribute(Type type, string name)
    {
        Method = type.GetEvent(name);
    }
}*/