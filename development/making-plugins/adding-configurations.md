---
description: Adding configurations to your plugin is pretty straightforward!
---

# ⚙ Adding Configurations

## Configuration Class

Any class that can be serialized is a class that can be used for configuration.

For example:

```csharp
public class MyModuleConfigurationClass
{
    public int MyConfigurableInt { get; set; } = 10; // 10 is the default value
}
```

**Fields aren't serialized! You must use properties.**

## Getting a config

To get a config you just have to call the `GetConfig<T>(string)` method inside your module class:

<pre class="language-csharp"><code class="lang-csharp"><strong>public class MyFirstModule : CursedModule
</strong><strong>{
</strong>    // public override string ModuleNam....
    
    public MyModuleConfigurationClass Configuration;
    
    public override void OnLoaded()
<strong>    {
</strong><strong>        Configuration = GetConfig&#x3C;MyModuleConfigurationClass>("File Name");  
</strong>        base.OnLoaded(); 
    }
}
</code></pre>

**"File Name"** will be the name of the configuration file inside the module folder!

## Saving a config

To save a config you just have to call the `SaveConfig<T>(T, string)` method inside your module class:

```csharp
public class MyFirstModule : CursedModule
{
    // public override string ModuleNam....
    
    public MyModuleConfigurationClass Configuration;
    
    public override void OnLoaded()
    {
        Configuration = GetConfig<MyModuleConfigurationClass>("File Name");
        Configuration.MyConfigurableInt = 5;
        SaveConfig(Configuration, "File Name"); 
        
        base.OnLoaded(); 
    }
}
```

In this case the configuration file would be overrided with the property `MyConfigurableInt` changed to 5.

