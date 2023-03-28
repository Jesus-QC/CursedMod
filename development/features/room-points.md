---
description: >-
  Do you want to save a specific point of a room between map generations? We
  provide an API for doing so!
---

# 📍 Room Points

## Creating a room point

To create a room point just use **CursedRoom::GetLocalPoint(Vector3)** being the vector the global point in the scene.

```csharp
CursedRoom room;
Vector3 myGlobalPoint = new Vector3(-1150f, 245.24f, 1402f);
Vector3 roomPoint = room.GetLocalPoint(myLocalPoint);
```

Room points are useful for configurable positions inside plugins.

## Converting back a room point

To convert a room point back to global you can call **CursedRoom::GetGlobalPoint(Vector3)** with the vector as the room point.

```csharp
CursedRoom room;
Vector3 myRoomPoint = new Vector3(11.2f, 2.2f, 3.4f);
Vector3 globalPoint = room.GetGlobalPoint(myRoomPoint);
```
