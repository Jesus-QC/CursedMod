---
description: You can create commands inside your plugins for users to interact with.
---

# ❗ Commands

To create commands you can follow the vanilla command classes:

## Command Types

* ClientCommandHandler - Commands that clients can run on their console prefixed with a dot.
* RemoteAdminCommandHandler - Commands that players with access to the RA can run.
* GameConsoleCommandHandler - Commands that only the server can run on its console.

## Individual commands

To create an individual command you may follow the next example:

{% hint style="warning" %}
Commands need to inherit `ICommand` and have an attribute of the desired type**.**
{% endhint %}

```csharp
[CommandHandler(typeof(ClientCommandHandler))]
[CommandHandler(typeof(RemoteAdminCommandHandler))]
[CommandHandler(typeof(GameConsoleCommandHandler))]
public class TestCommand : ICommand
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        response = "This was a test."; // You need to return a response
        return true; // Return true if the command was executed or false if not (missing permissions...).
    }

    public string Command { get; } = "Test"; // The command used in the console.
    public string[] Aliases { get; } = Array.Empty<string>(); // The desired aliases.
    public string Description { get; } = "Test command"; // A small description.
}
```

## Parent Commands

You can group different Individual Commands inside a parent command, this will make the commands behave like this: `.parent child` being `parent` the parent command and `child` one of the child commands. You can have any amount of child commands you would like to have.

{% hint style="danger" %}
Child commands can't have the attributes with the command type or they will be registered as individual commands. The attribute/s must be on the parent.
{% endhint %}

```csharp
[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class TestParentCommand : ParentCommand
{
    public TestParentCommand() // We are subscribing our child when created an instance.
    {
        LoadGeneratedCommands();
    }
    
    public override void LoadGeneratedCommands()
    {
        // Here we register our child commands. (Classes inheriting ICommand).
        RegisterCommand(new TestChildCommand1());
        // RegisterCommand(new TestChildCommand2());
        // ...
    }

    protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        // In the case that child commands weren't found this will run.
        // It is a great moment to display the available subcommands.
        response = "Subcommands: test, test2, test3.";
        return false; // We are returning false because we are not handling any logic here.
    }

    public override string Command { get; } = "parent";
    public override string[] Aliases { get; } = Array.Empty<string>();
    public override string Description { get; } = "Test parent command.";
}
```

And like that you can easily create commands. Any commands in your module assembly will be registered automatically by CursedMod. If you want to prevent this you can inherit `ICursedModule::OnRegisteringCommands()` on your module.

<figure><img src="../../.gitbook/assets/image.png" alt=""><figcaption><p>Example of a RemoteAdmin command.</p></figcaption></figure>
