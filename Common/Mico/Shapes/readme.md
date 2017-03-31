# Mico.Shapes

A base object in `Micos`.

## A Base Object

The "Micos" can have many kinds object.

Shape is the very basic object.

## Introduction

Shape should be a interface,you should use it by inheriting.

```C#
public class Life : Shape
{
	...
}
```

### Event

On some events be occurred,some funciton will be run.

For example,the `Update` will be run when `Micos` update itself.

You can override this function.

```C#
public class Life : Shape
{
	public override void OnUpdate(object Unknown = null) 
	{
		Console.WriteLine("updating...");
	}
}
``` 

### Export

You can render or write your shape.

### Transform

A transofrm can describe a shape in the `Micos`.

#### Forward:
default is `(0,0,1)`.

If you change forward,the rotate will be changed.
#### Rotate: 

default is `(0,0,0)`.

If you change rotate,the forward will be changed.

We use `heading-pitch-bank`,and the value's range is `(0,360)`.

First,we rotate `Y-axis`,then `X-axis`,last `Z-axis`.

### Collider

Now Unavailable.
