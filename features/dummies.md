---
description: CursedMod provides a rich and easy to use API for dummies or NPCs.
---

# 🤖 Dummies

## CursedDummy

Cursed implements dummies the same way as local players. Indeed dummies are a single `CursedPlayer`. To create a dummy you may follow the next example:

```csharp
CursedPlayer dummy = CursedDummy.Create("MyDummyNickname")
```

As now **dummy** is a **CursedPlayer** you can now use any property of it. Including changin its role, position even items and ammo, you could even attach a small unity component to control it and make it follow a specific behaviour like following a player.

<figure><img src="../.gitbook/assets/image (2).png" alt=""><figcaption><p>A simple dummy with the Tutorial role</p></figcaption></figure>
