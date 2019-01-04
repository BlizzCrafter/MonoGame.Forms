![Banner](Logos/Logo_Banner_800.png)

# Welcome to MonoGame.Forms!
[![Twitter Follow](https://img.shields.io/twitter/follow/SandboxBlizz.svg?style=flat-square&label=Follow&logo=twitter)](https://twitter.com/SandboxBlizz)
[![Wiki](https://img.shields.io/badge/Wiki-Online!-orange.svg?style=flat-square&logo=github&colorA=ba51ff&colorB=77c433)](https://github.com/sqrMin1/MonoGame.Forms/wiki)
[![License](https://img.shields.io/badge/License-MIT!-blue.svg?style=flat-square&colorA=bc9621&colorB=77c433)](https://github.com/sqrMin1/MonoGame.Forms/blob/master/LICENSE)

[![NuGet](https://img.shields.io/badge/NuGet-MonoGame.Forms.DX-blue.svg?style=flat-square&colorA=3260c4&colorB=77c433)](https://www.nuget.org/packages/MonoGame.Forms.DX)
[![NuGet](https://img.shields.io/badge/NuGet-MonoGame.Forms.GL-blue.svg?style=flat-square&colorA=3260c4&colorB=77c433)](https://www.nuget.org/packages/MonoGame.Forms.GL)
[![NuGet](https://img.shields.io/badge/NuGet-MonoGame.Forms_(OUTDATED)-red.svg?style=flat-square&colorA=3260c4&colorB=FF0000)](https://www.nuget.org/packages/MonoGame.Forms/)

MonoGame.Forms is the easiest way of integrating a MonoGame render window into your Windows Forms project. It should make your life much easier, when you want to create your own editor environment. 

#### Now with OpenGL support!

### Building

* The **MonoGame.Forms.DX** project uses the MonoGame.Framwork 3.7.0.1114, which is already precompiled and included in this repo.
  * It's possible to use older **MonoGame.Frameworks** like the **3.6** iteration, but you will then lose the **Mouse.GetState()** functionallity. You would then rely on the **WindowsForms Event System** for mouse input, which can be slower in some cases.
* The **MonoGame.Forms.GL** project uses a modified version of the MonoGame.Framework based on the development build from 16th of JULY 2018.
  * It's **not** possible to exchange the MonoGame.Framework easily. If you want to use a newer build, it would be neccessary to modify it to suit the needs of MonoGame.Forms!

#### Tips & Tricks / Dos & Don'ts

The OpenGL version of the library currently renders differently than the DirectX one, which makes it generally slower. If you need as much perormance as possible, you should definitley use the DirectX library.

* Boost performance of a custom OpenGL control by raising its **DrawIntervall** with the property window during design time (try 50ms or 100ms. 1ms aims to 60 fps)
* **Never** use **DoubleBuffering** on a custom control. This counts for the OpenGL library **as well as** for the DirectX library! It will cause flickering and slow downs.
* If you need to have the **MonoGame-PipelineTool** integrated into your **MonoGame.Forms** project, you should check out the **[PipelineTool-Tutorial](https://github.com/sqrMin1/MonoGame.Forms/blob/master/PipelineTool-Tutorial.md)!**

# How-To
#### Setup MonoGame.Forms

First you need to make your MonoGame.Forms library ready to use. This step is very easy; you just need to compile either the MonoGame.Forms.DX or the MonoGame.Forms.GL PCL from source and then just add the compiled DLL's to your project.

↳ _This is the prefered route, when you want to make you own custom changes to the library or extend it_.

Another option is to install the library with the NuGet package manager:

![NuGet](doc/NuGet_Manager.PNG)

↳ _This is the prefered and easiest route to be automatically up to date_.

> Note: You need to set the .Net Framework version to 4.5.1 to install from NuGet!

***

### Tutorial

The following tutorial is working exactly the same on both libraries (DX and GL).

#### Creating a simple [InvalidationControl](https://github.com/sqrMin1/MonoGame.Forms/wiki/1802024)

Let's start using the MonoGame.Forms library by creating a simple control to render stuff! 

_(it's assumed that you already have created a new **Windows Forms** project with the installed library)_

1. Create a new class and name it **DrawTest**
2. Inherit from **MonoGame.Forms.Controls.InvalidationControl**
3. Override its **Initialize()** method
4. Override its **Draw()** method
5. **Save & Build** your solution
6. **Double Click** on **Form1.cs** so that the Designer opens
7. Open the **Toolbox**
8. **Drag & Drop** the newly created control onto the Form1 control
9. Open the **Properties** of the new control and set the **Dock** option to **Fill**

This is how it should look now:

![Tutorial](https://github.com/sqrMin1/MonoGame.Forms/blob/master/doc/tut_00.PNG)

10. Now run the solution and see the classical **CornflowerBlue-Screen** you are (surly) familiar with! ;-)

![Tutorial](https://github.com/sqrMin1/MonoGame.Forms/blob/master/doc/tut_00a.PNG)

And yes, as you can see: it is realy **THAT EASY**!

Now I bet you wonder how to draw something to this control, right? I bet you think that this is now the difficult part, right?
Well... it's not!

More than that it's basically the same like you are used to do in the **MonoGame.Framework**. 
Just with a small difference (no it's still not difficult!)

In MonoGame you could draw someting to the screen with the [SpriteBatch](https://msdn.microsoft.com/de-de/library/microsoft.xna.framework.graphics.spritebatch(v=xnagamestudio.40).aspx).
In **MonoGame.Forms** you will do the same but you need to use [GFXService](https://github.com/sqrMin1/MonoGame.Forms/wiki/3A4C800C) for this.

In the **InvalidationControl** and **MonoGameControl** class this service is called **Editor**. To draw something to the **SpriteBatch** you need to do this:

```c
Editor.spriteBatch.DrawString();
```

Do you see? Easy! :)

The **GFXService** class contains some MonoGame specific stuff like a [ContentManager](https://github.com/sqrMin1/MonoGame.Forms/wiki/A72EF9E7).
Examine everything calmly. I just want to explain a little how **MonoGame.Forms** works under the hood!

To sum things up, let's take a look at the final **DrawTest** class:

```c
using Microsoft.Xna.Framework;
using MonoGame.Forms.Controls;

namespace nugetTest
{
    public class DrawTest : InvalidationControl
    {
        string WelcomeMessage = "Hello MonoGame.Forms!";

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void Draw()
        {
            base.Draw();

            Editor.spriteBatch.Begin();

            Editor.spriteBatch.DrawString(Editor.Font, WelcomeMessage, new Vector2(
                (Editor.graphics.Viewport.Width / 2) - (Editor.Font.MeasureString(WelcomeMessage).X / 2),
                (Editor.graphics.Viewport.Height / 2) - (Editor.FontHeight / 2)),
                Color.White);

            Editor.spriteBatch.End();
        }
    }
}
```
Result:

![Tutorial](https://github.com/sqrMin1/MonoGame.Forms/blob/master/doc/tut_00b.PNG)

It's pretty much like in the **MonoGame.Framework!**

> Note: If you are changing the drawn contents of the SpriteBatch when your editor project is running (not during design time), then you simply need to call **Invalidate()** on a custom control for every change you want to see on your control. This command commits those changes and after that your control does not consume CPU power anymore. This process is great when creating preview controls for textures and similar things!

For realtime interactions (game loop) please read the next topic about the **MonoGameControl**!

***

#### Creating a simple [MonoGameControl](https://github.com/sqrMin1/MonoGame.Forms/wiki/C5EB9086)

As you might though right: Yes, this is also very easy. It's the same like for the **InvalidationControl**. 
I just want to show you the only difference:

```c
protected override void Update(GameTime gameTime)
{
    base.Update(gameTime);
}
```

The update method makes realtime interactions with your game- / editor environment possible. Even complex mechanics are doable.

I just want to refer to the nice [MonoGame.Forms.Test](https://github.com/sqrMin1/MonoGame.Forms/tree/master/MonoGame.Forms.Tests)-Project,
which is part of this repo. Take a look at it and learn from its samples.

> Note: To raise the performance of an OpenGL MonoGameControl, it's recommended to change the **DrawIntervall** from 1ms to 50ms or 100ms!

![DrawIntervall](https://github.com/sqrMin1/MonoGame.Forms/blob/master/doc/intervall.PNG)

Did you notice the **BackColor** and **ForeColor** property? 
Changing these values makes it possible to style your controls to something like this:

![Style](https://github.com/sqrMin1/MonoGame.Forms/blob/master/doc/style.PNG)

Do it to keep the overview and feel of your custom editor project!
> Note: The MonoGame logo is placed automatically inside a newly created control to make it clear, that it is a render control with MonoGame functionality!

### Sample Pics

Here are some pics of some samples included with the repo:

### DX (Windows 10 Pro 64 bit)
![Sample](https://github.com/sqrMin1/MonoGame.Forms/blob/master/doc/UpdateSample.png)

### GL (Ubuntu 18.04 LTS [Bionic Beaver] 64 bit)
![Sample](https://github.com/sqrMin1/MonoGame.Forms/blob/master/doc/gl_Test.png)

# Projects using MonoGame.Forms!

Please watch the following YouTube videos in *1080p @ 60fps* to see what is possible with MonoGame.Forms!

###### This project is called: "**Rogue Engine Editor**" and it's possible to create Rogue Adventures with it:

[![Rogue Engine Editor](https://img.youtube.com/vi/6fyQ64O9HME/0.jpg)](https://youtu.be/6fyQ64O9HME)

Twitter: [#RogueEngineEditor](https://twitter.com/hashtag/RogueEngineEditor?src=hash)

***

###### This project is called: "**PenumbraPhysics.Editor**" and it was the prototype for the Rogue Engine Editor and MonoGame.Forms:

[![YouTube Video](https://github.com/sqrMin1/PenumbraPhysics.Editor/blob/master/Documentation/Thumbnail.png)](https://youtu.be/vQAxXN_V3X4)

GitHub: [PenumbraPhysics.Editor](https://github.com/sqrMin1/PenumbraPhysics.Editor)

***

### Now Have Fun with MonoGame.Forms!
[![Twitter Follow](https://img.shields.io/twitter/follow/SandboxBlizz.svg?style=flat-square&label=Follow&logo=twitter)](https://twitter.com/SandboxBlizz)
[![Wiki](https://img.shields.io/badge/Wiki-Online!-orange.svg?style=flat-square&logo=github&colorA=ba51ff&colorB=77c433)](https://github.com/sqrMin1/MonoGame.Forms/wiki)
[![License](https://img.shields.io/badge/License-MIT!-blue.svg?style=flat-square&colorA=bc9621&colorB=77c433)](https://github.com/sqrMin1/MonoGame.Forms/blob/master/LICENSE)

[![NuGet](https://img.shields.io/badge/NuGet-MonoGame.Forms.DX-blue.svg?style=flat-square&colorA=3260c4&colorB=77c433)](https://www.nuget.org/packages/MonoGame.Forms.DX)
[![NuGet](https://img.shields.io/badge/NuGet-MonoGame.Forms.GL-blue.svg?style=flat-square&colorA=3260c4&colorB=77c433)](https://www.nuget.org/packages/MonoGame.Forms.GL)
[![NuGet](https://img.shields.io/badge/NuGet-MonoGame.Forms_(OUTDATED)-red.svg?style=flat-square&colorA=3260c4&colorB=FF0000)](https://www.nuget.org/packages/MonoGame.Forms/)

![Logo](https://raw.githubusercontent.com/sqrMin1/MonoGame.Forms/master/Logos/Logo_Shadow_256.png)

### Special Thanks
- [nkast](https://github.com/nkast) and [SpiceyWolf](https://github.com/SpiceyWolf) from the MonoGame community
- everyone else from the official [MonoGame.Forms_Thread](http://community.monogame.net/t/monogame-forms-create-your-editor-environment/9954)
- the awesome MonoGame community itself :)
