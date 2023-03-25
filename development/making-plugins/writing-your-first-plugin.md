---
description: Writing a plugin may seem difficult, but it is pretty straightforward!
---

# 👀 Writing Your First Plugin

## First Lines of Code

CursedMod implements a Module Pattern to load plugins. Because of this plugins should have a main class that implements the `CursedModule` abstract class.

```csharp
public class MyFirstModule : CursedModule
{
    public override string ModuleName => "MyFirstModule";
    public override string ModuleAuthor => "Me";
    public override string ModuleVersion => "1.0.0.0";
    public override byte LoadPriority => (byte)ModulePriority.Medium;
    public override string CursedModVersion => CursedModInformation.Version;
}
```

### Entry point

You can override `CursedModule::OnLoaded()` method to make your entry point:

```csharp
public class MyFirstModule : CursedModule
{
    public override string ModuleName => "MyFirstModule";
    public override string ModuleAuthor => "Me";
    public override string ModuleVersion => "1.0.0.0";
    public override byte LoadPriority => (byte)ModulePriority.Medium;
    public override string CursedModVersion => CursedModInformation.Version;
    
    public override void OnLoaded()
    {
        // All the code
        
        base.OnLoaded(); // Logs that the plugin has been loaded
    }
}
```

## Exit Point

You can override `CursedModule::OnUnloaded()` method to make your entry point:

```csharp
// Simple example of a case where OnUnloaded is useful.
public class MyFirstModule : CursedModule
{
    // public override string ModuleName .....
    
    public static ConnectionProvider Connector = new ConnectionProvider();
    
    public override void OnLoaded()
    {
        Connector.Start();
        base.OnLoaded();
    }
    
    public override void OnUnloaded()
    {
        Connector.Stop();
        base.OnUnloaded();
    }
}
```
