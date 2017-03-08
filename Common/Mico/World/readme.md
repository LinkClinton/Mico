# Mico.World

The world called "Micos".

## A Simple Framework

When we make game,we need to manager characters,events and so on.

"Micos" is a simple framework to help you manager this.

## Introduction

"Micos" expects we can make game more easier.

### Add Shape

You can add [shape](https://github.com/Link-Arthur/Mico/tree/master/Common/Mico/Shapes)
and delete it.

```C#
Mico.World.Micos.Add(Shape shape);

Mico.World.Micos.Delete(Shape shape);
```

If you want a shape be updated and render,you should add it in "Micos".

### Update Micos

"Micos" manager some shapes' information,so you need to update "Micos' to
update shapes in the "Micos".

```C#
Mico.World.Micos.Update();
```

### Export Micos

A world need to output itself.

You can render this world or write it to a file and so on.

```C#
Mico.World.Micos.Export();
```