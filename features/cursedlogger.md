---
description: >-
  CursedMod provides a custom console printer. Use it if you need to print
  anything inside the console.
---

# 📄 CursedLogger

## Log Types:

{% tabs %}
{% tab title="Information" %}
```csharp
// CursedLogger.LogInfo(object message, string prefix = null)

CursedLogger.LogInfo("Test message");
CursedLogger.LogInfo("Test message", "CursedMod")
```
{% endtab %}

{% tab title="Debug" %}
```csharp
// CursedLogger.LogDebug(object message, bool show = true, string prefix = null)

CursedLogger.LogDebug("Test message");
CursedLogger.LogDebug("Test message", true, "CursedMod")
```
{% endtab %}

{% tab title="Warning" %}
```csharp
// CursedLogger.LogWarning(object message, string prefix = null)

CursedLogger.LogWarning("Test message");
CursedLogger.LogWarning("Test message", "CursedMod")
```
{% endtab %}

{% tab title="Error" %}
```csharp
// CursedLogger.LogError(object message, string prefix = null)

CursedLogger.LogError("Test message");
CursedLogger.LogError("Test message", "CursedMod")
```
{% endtab %}

{% tab title="Critical" %}
```csharp
// CursedLogger.LogCritical(object message, string prefix = null)

CursedLogger.LogCritical("Test message");
CursedLogger.LogCritical("Test message", "CursedMod")
```
{% endtab %}
{% endtabs %}

<figure><img src="../.gitbook/assets/image (4).png" alt=""><figcaption><p>The different types of logs.</p></figcaption></figure>

## Where to use

Logs are a useful way of showing information inside the console to the end user. You can use them to debug the plugin.

&#x20;Each module or plugin has a property called `ShowDebug` inside its base class. To use the **CursedLogger::LogDebug(object, bool, string)** method please follow the next example:

```csharp
public class MyModule : CursedModule
{
    public override void OnLoaded()
    {
        CursedLogger.LogDebug("good implemented debug log", ModuleProperties.ShowDebug);
        base.OnLoaded();
    }
}
```
