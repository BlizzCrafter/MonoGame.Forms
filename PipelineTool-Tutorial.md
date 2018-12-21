# MonoGame PipelineTool Tutorial

This tutorial will guid you step by step through the path of integrating the **MonoGame-PipelineTool** into your **MonoGame.Forms** project.

With this tool it's possible to **directly compile your content files to the .xnb file format** - ready to be used by the MonoGame **ContentManager**!

As a starting point we will use a fresh **WindowsForms project** on a **Windows machine**, which has nothing - related to the MonoGame.Framework - referenced.

Let's start!

---

Part I:
## Preperation

1. Create a fresh WindowsForms project
2. Install the MonoGame.Forms.DX library
    - *PM> Install-Package MonoGame.Forms.DX -Version 2.1.0.2* (exchange with the version you want to use) **OR**
	- Browse through the NuGet Package-Manger from within Visual Studio
3. Create a new folder and name it **"Content"**
4. Add a **.txt** file to this folder and name it **"Content.mgcb"**
5. You will get an error saying that the project couldn't be opened, because of an unknown error 
    - Click **"Ok"** on this message box
6. The PipelineTool should now be opened with an empty project
7. Close the PipelineTool now
8. Open the **"Content.mgcb"** file with a regular **text editor** (like the windows Notepad or Notepad++)
9. **Paste** the following lines to the **.mgcb file** and **save it**:

```xml
#----------------------------- Global Properties ----------------------------#

/outputDir:bin/$(Platform)
/intermediateDir:obj/$(Platform)
/platform:Windows
/config:
/profile:Reach
/compress:False

#-------------------------------- References --------------------------------#


#---------------------------------- Content ---------------------------------#
```

10. When you now **double click** on the **Content.mgcb** file it sould open the **PipelineTool** without errors and it should display your content project

We are done with the preperation!

... but we are not finished yet. We need to make sure, that our content files correctly getting built and copied to the output directory.

---

Part II:
## Pipeline Magick

... if you want to call it like this ;)

1. Open your **WindowsForms .csproj** file with a regular **text editor** (like the windows Notepad or Notepad++)
2. **Paste** the following line directly to the first `<PropertyGroup>`:
    - `<MonoGamePlatform>Windows</MonoGamePlatform>`
3. Scroll right to the **end of this file** and **paste** the following line right behind the **Microsoft.CSharp.targets** line:
    - `<Import Project="$(MSBuildExtensionsPath)\MonoGame\v3.0\MonoGame.Content.Builder.targets" />`
4. Save your changes and open Visual Studio again
5. You will be greeted with a **prompt**. Just tell him to **reload everything**
6. Now **select** the **"Content.mgcb"** file and in the **Visual Studio property panel** under **Build Process** choose the **MonoGameContentReference** option
7. Add a **test file** by using the **PipelineTool** (e.g. TestFont.spritefont) and **save the content project**
8. If you now **rebuild** or **run** your WindowsForms project you will see a **"Content" folder** with the **compiled "TestFont.xnb"** file in your **output directory**
9. Use this file with your **ContentManager** (e.g. `Editor.Content.Load<SpriteFont>(@"TestFont");`
10. Be Happy!

This should accelerate your "content compiling workflow" by alot!

Feel free to ask questions if something is not clear enough.

You can reach me on **Twitter**:

[![Twitter](https://img.shields.io/twitter/follow/SandboxBlizz.svg?style=flat-square&label=Follow&logo=twitter)](https://twitter.com/SandboxBlizz)

or visit the official
**[MonoGame.Forms_Thread](http://community.monogame.net/t/monogame-forms-create-your-editor-environment/9954)**

Cheers,

Marcel "sqrMin1" Härtel
