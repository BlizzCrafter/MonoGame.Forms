![Banner](https://raw.githubusercontent.com/BlizzCrafter/MonoGame.Forms/master/Logos/Logo_Banner_Social_800.png)

# Welcome to MonoGame.Forms!
[![Wiki](https://img.shields.io/badge/Wiki-Online!-orange.svg?style=flat-square&logo=github&colorA=ba51ff&colorB=51ff63)](https://github.com/BlizzCrafter/MonoGame.Forms/wiki) [![NuGet](https://img.shields.io/badge/NuGet-MonoGame.Forms-blue.svg?style=flat-square&logo=NuGet&colorA=5196ff&colorB=51edff)](https://www.nuget.org/packages/MonoGame.Forms.DX/) [![NuGet](https://img.shields.io/badge/NuGet-MonoGame.Forms.Templates-blue.svg?style=flat-square&logo=NuGet&colorA=5196ff&colorB=51edff)](https://www.nuget.org/packages/MonoGame.Forms.Templates)

MonoGame.Forms is the easiest way of integrating a MonoGame render window into your Windows Forms project. It should make your life much easier, when you want to create your own editor environment. 

# Info

* The **MonoGame.Forms** project uses a modified version of the MonoGame.Framework. It's called [MonoGame.Framework.WindowsDX.9000](https://www.nuget.org/packages/MonoGame.Framework.WindowsDX.9000/) (created by [nkast](https://github.com/nkast)), which is faster, memory optimized, bugfixed and supports full mouse & keyboard input within WindowsForms. You can also update MonoGame.Forms to a new MonoGame version very easily - just by updating the MonoGame.Framework.WindowsDX.9000 nuget package!
* **MonoGame.Forms.GL**  - DEPRECATED! - faster alternative: [MonoGame.Template.Gtk.CSharp](https://www.nuget.org/packages/MonoGame.Template.Gtk.CSharp/) (created by [harry-cpp](https://github.com/harry-cpp)).

# Tips & Tricks / Dos & Don'ts

* **Never** use **DoubleBuffering** on a custom control. It will cause flickering and slow downs.
* If you experience scaling issues with your drawn content, then you might want to set the right **AutoScaleMode** of a Form containing a MonoGameControl:
**`AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;`**. If you want to turn off scaling of your whole application, then you need to add a **[Manifest-File](https://github.com/BlizzCrafter/MonoGame.Forms/blob/master/DPI_Aware_Application.md)**.

# Setup MonoGame.Forms
### Options:
- Build from source (this repo)
- Install Package:
  - ```dotnet add package MonoGame.Forms.DX``` or use the *Package Manager*
- Install Templates:
  - ```dotnet new install MonoGame.Forms.Templates```

***

# Tutorials
### Creating MonoGame.Forms Projects:
- Automagical via Terminal (if templates installed):
  - .NET 6.0: ```dotnet new mgf -n MyMonoGameFormsProject```
  - .Net-Framework: ```dotnet new mgfn -n MyMonoGameFormsProject```
- Manual:
1. Create a new WindowsForms project
2. Install the [nuget](https://www.nuget.org/packages/MonoGame.Forms.DX/3.1.0) package
3. Build the solution
  
### Creating MonoGame.Forms Controls:
- Automagical via Terminal (if templates installed):
  - [MonoGameControl](https://github.com/BlizzCrafter/MonoGame.Forms/wiki/FDEF831D): ```dotnet new mgc -na MyMonoGameControl```
  - [InvalidationControl](https://github.com/BlizzCrafter/MonoGame.Forms/wiki/62B63ABB): ```dotnet new mgic -na MyInvalidationControl```
- Manual:
1. Create a new class and name it **DrawTest**
2. Inherit from **MonoGame.Forms.Controls.MonoGameControl**
3. Override its **Initialize()**, **Update()** and **Draw()** method
4. **Save** your solution
5. **Build** your solution
6. **Double Click** on **Form1.cs** so that the designer opens
7. Open the **Toolbox**
8. **Drag & Drop** the newly created control onto the Form1 control
9. Open the **Properties** of the new control and set the **Dock** option to **Fill**

![Tutorial](https://github.com/BlizzCrafter/MonoGame.Forms/blob/master/doc/tut_00.PNG)

10. Profit ???

![Tutorial](https://github.com/BlizzCrafter/MonoGame.Forms/blob/master/doc/tut_00a.PNG)

### Drawing
In MonoGame you could draw someting to the screen with the **spriteBatch**.
In **MonoGame.Forms** you will do the same but you need to use the [EditorService](https://github.com/BlizzCrafter/MonoGame.Forms/wiki/29FDD2C0) for this.

In the MonoGame.Forms.Control classes this service is called **Editor**. So, in order to draw something to the **spriteBatch** you need to do this:

```c
Editor.spriteBatch.DrawString();
```

Let's take a look at the final **DrawTest** class:

```c
using Microsoft.Xna.Framework;
using MonoGame.Forms.Controls;

namespace nugetTest
{
    public class DrawTest : MonoGameControl
    {
        string WelcomeMessage = "Hello MonoGame.Forms!";

        protected override void Initialize()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
        }

        protected override void Draw()
        {
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

![Tutorial](https://github.com/BlizzCrafter/MonoGame.Forms/blob/master/doc/tut_00b.PNG)

It's pretty much like in the **MonoGame.Framework!**

# Samples

Take a look at the [MonoGame.Forms.Samples](https://github.com/BlizzCrafter/MonoGame.Forms/tree/master/MonoGame.Forms.NET.Samples/Tests)-Project,
which is part of this repo, to learn more about how MonoGame.Forms works.

***

# WTF is this **InvalidationControl**?

This specific control class doesn't need to override the **Update()** method, because it gets manually updated (by you!).

You simply need to call **Invalidate()** on a custom InvalidationControl for every change you want to see on it. After calling this, your control does not consume CPU power anymore. This is great when creating preview controls for textures and similar things!

# Pics or It Didn't Happen

Here are some sample pics from the [Blazor](https://github.com/BlizzCrafter/MonoGame.Forms/tree/Blazor) branch:

![MonoGameControl](https://raw.githubusercontent.com/BlizzCrafter/MonoGame.Forms/master/doc/sample_00.png)
![InvalidationControl](https://raw.githubusercontent.com/BlizzCrafter/MonoGame.Forms/master/doc/sample_01.png)
![MultipleControls](https://raw.githubusercontent.com/BlizzCrafter/MonoGame.Forms/master/doc/sample_02.png)
![AdvancedInput](https://raw.githubusercontent.com/BlizzCrafter/MonoGame.Forms/master/doc/sample_03.png)

# Projects using MonoGame.Forms

### **Rogue Engine Editor**:

[![Rogue Engine Editor](https://raw.githubusercontent.com/BlizzCrafter/MonoGame.Forms/master/doc/ree.png)](https://youtu.be/6fyQ64O9HME)

### **Mercury Particle Sandbox**:

[![Rogue Engine Editor](https://raw.githubusercontent.com/BlizzCrafter/MonoGame.Forms/master/doc/mps.png)](https://youtu.be/7Xds-q5tm8E)

### **PenumbraPhysics.Editor**:

[![YouTube Video](https://raw.githubusercontent.com/BlizzCrafter/MonoGame.Forms/master/doc/ppe.png)](https://youtu.be/vQAxXN_V3X4)

***

# Now Have Fun with MonoGame.Forms!

![Logo](https://raw.githubusercontent.com/BlizzCrafter/MonoGame.Forms/master/Logos/Logo_Shadow_256.png)

### Special Thanks
- [nkast](https://github.com/nkast) and [SpiceyWolf](https://github.com/SpiceyWolf) from the MonoGame community
- everyone else from the official [MonoGame.Forms_Thread](http://community.monogame.net/t/monogame-forms-create-your-editor-environment/9954)
- the awesome MonoGame community itself :)
