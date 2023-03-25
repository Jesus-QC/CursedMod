---
description: Adding configurations to your plugin is pretty straightforward!
---

# ⚙ Adding Configurations

## Configuration Class

Any class that can be serialized is a class that can be used for configuration.

For example:

<pre class="language-csharp"><code class="lang-csharp">public class <a data-footnote-ref href="#user-content-fn-1">MyModuleConfigurationClass</a>
{
    public int MyConfigurableInt <a data-footnote-ref href="#user-content-fn-2">{ get; set; }</a> = 10; // 10 is the default value
}
</code></pre>

**Fields aren't serialized! You must use properties.**

## Getting a config

To get a config you just have to call the `GetConfig<T>(string)` method inside your module class:

<pre class="language-csharp"><code class="lang-csharp">public class MyFirstModule : CursedModule
{
    <a data-footnote-ref href="#user-content-fn-3">// public override string ModuleNam....</a>
    
    public MyModuleConfigurationClass Configuration;
    
    public override void OnLoaded()
    {
        Configuration = GetConfig&#x3C;MyModuleConfigurationClass>("File Name");  
        base.OnLoaded(); 
    }
}
</code></pre>

**"File Name"** will be the name of the configuration file inside the module folder!

## Saving a config

To save a config you just have to call the `SaveConfig<T>(T, string)` method inside your module class:

<pre class="language-csharp"><code class="lang-csharp">public class MyFirstModule : CursedModule
{
    // public override string ModuleNam....
    
    public MyModuleConfigurationClass Configuration;
    
    public override void OnLoaded()
    {
        Configuration = GetConfig&#x3C;MyModuleConfigurationClass>("File Name");
        <a data-footnote-ref href="#user-content-fn-4">Configuration.MyConfigurableInt = 5;</a>
        SaveConfig(Configuration, "File Name"); 
        
        base.OnLoaded(); 
    }
}
</code></pre>

In this case the configuration file would be overrided with the property `MyConfigurableInt` changed to 5.



[^1]: It can have any name you want it to have

[^2]: Don't miss this part, it is important!

[^3]: Add the missing properties

[^4]: Changing the value of the configuration we just got.
