---
description: CursedMod provides a rich and easy to use API for dummies or NPCs.
---

# 🤖 Dummies

## CursedDummy

Cursed implements dummies the same way as local players. Indeed dummies are a single `CursedPlayer`. To create a dummy you may follow the next example:

```csharp
CursedPlayer dummy = CursedDummy.Create("MyDummyNickname")
```

As now **dummy** is a **CursedPlayer** you can now use any property of it. Including role change, position change or even giving items.

