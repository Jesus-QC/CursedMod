---
description: Writing a plugin may seem difficult, but it is pretty straightforward!
---

# 👀 Writing Your First Plugin

## First Lines of Code

CursedMod implements a Module Pattern to load plugins. Because of this plugins should have a main class that implements the `CursedModule` abstract class.

<pre class="language-csharp" data-title="MyFirstModule.cs"><code class="lang-csharp">public class <a data-footnote-ref href="#user-content-fn-1">MyFirstModule</a> : CursedModule
{
    public override string ModuleName => "<a data-footnote-ref href="#user-content-fn-2">MyFirstModule</a>";
    public override string ModuleAuthor => "Me";
    public override string ModuleVersion => "<a data-footnote-ref href="#user-content-fn-3">1.0.0.0</a>";
    public override byte LoadPriority => (byte)ModulePriority.Medium;
    <a data-footnote-ref href="#user-content-fn-4">public override string CursedModVersion => CursedModInformation.Version;</a>
}
</code></pre>

## Entry point

You can override `CursedModule::OnLoaded()` method to make your entry point:

<pre class="language-csharp"><code class="lang-csharp">public class MyFirstModule : CursedModule
{
    <a data-footnote-ref href="#user-content-fn-5">// public override string ModuleName .....</a>
    
    public override void OnLoaded()
    {
        // All the code
        
        base.OnLoaded(); // Logs that the plugin has been loaded
    }
}
</code></pre>

## Exit Point

You can override `CursedModule::OnUnloaded()` method to make your exit point:

<pre class="language-csharp"><code class="lang-csharp">// Simple example of a case where OnUnloaded is useful.
public class MyFirstModule : CursedModule
{
    // public override string ModuleName .....
    
    public static <a data-footnote-ref href="#user-content-fn-6">ConnectionProvider</a> Connector = new ConnectionProvider();
    
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
</code></pre>

[^1]: It can have any name you want it to have

[^2]: Don't use characters not allowed in file names

[^3]: Use semantic versioning

[^4]: Don't change this line. It works perfectly fine like this. It allows the loader to know in which version was the plugin compiled.

[^5]: Here goes the properties that we just saw.

[^6]: This doesn't exist, it is just an example where OnUnloaded may result useful.
