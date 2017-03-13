# Mico.Sample

这里有一个需要注意的地方。

```C#
 event WndProc WindowProc;

 WindowProc += Window_proc;

 Hwnd = IDevice.CreateWindow("Mico", "", 800, 600, WindowProc);
```

而不能这样使用：

```C#
  Hwnd = IDevice.CreateWindow("Mico", "", 800, 600, Window_proc);
```