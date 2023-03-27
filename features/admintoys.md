---
description: CursedMod has a rich API to interact with Admin Toys.
---

# 💡 AdminToys

## Primitives

Primitives are shapes such as cubes, spheres or even cylinders that you can spawn in clients. CursedMod lets you easily spawn primitives without any type of issue.

```csharp
CursedPrimitiveObject primitive = CursedPrimitiveObject.Create(PrimitiveType.Cube);
```

#### A rich constructor.

The **Create** method allows you to pass a lot of information in order to create your own primitives in one single method:

```csharp
static CursedPrimitiveObject Create(PrimitiveType? type = null, Vector3? position = null, Vector3? scale = null, Vector3? rotation = null, Color? color = null, bool spawn = false)
```

#### Properties

* PrimitiveType
* Position
* Scale
* Rotation
* Movement Smoothing
* Color
* Spawn on creation

#### Non collidable primitives

To create a non collidable primitive you just have to use a negative scale vector:

```csharp
primitive.Scale = new Vector3(1f, 1f, 1f); // Collidable
primitive.Scale = new Vector3(-1f, -1f, -1f); // Non collidable
```

## Light Sources

Light sources are simple light points that you can spawn on clients. CursedMod lets you easily spawn light sources without any type of issue.

#### A rich constructor.

The **Create** method allows you to pass a lot of information in order to create your own light sources in one single method:

```csharp
static CursedLightSource Create(Vector3? position = null, Vector3? rotation = null, Vector3? scale = null, float? lightIntensity = null, float? lightRange = null, Color? lightColor = null, bool? lightShadows = null, bool spawn = false)
```

#### Properties

* Position
* Scale
* Movement Smoothing
* Light Intensity
* Light Range
* Light Color
* Light Shadows

## Shooting Targets

Shooting targets are admin toys with the shape and functionality of targets that you can shoot and play with that you can spawn on clients. CursedMod lets you easily spawn light sources without any type of issue.

#### A rich constructor.

The **Create** method allows you to pass a lot of information in order to create your own light sources in one single method:

```csharp
static CursedShootingTarget Create(ShootingTargetType type, Vector3? position = null, Vector3? scale = null, Vector3? rotation = null, bool spawn = false)
```

#### Properties

* Position
* Scale
* Movement Smoothing
* Health
* Max Health
* Auto Reset Time
* Server Sync

## Movement Smoothing

In all admin toys you will find a property called `MovementSmoothing`, this property allows you to setup a configurable byte used to lerp positions in clients when they change in order to make them more smooth. An ideal value is equal to your server framerate, by default 60.

<figure><img src="../.gitbook/assets/image (5).png" alt=""><figcaption></figcaption></figure>
