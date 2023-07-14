## Implementation / Design-Philosophy

In MonoGame.Forms you will not work with a classical XNA/MonoGame **Game.cs** class. It's a "game-class-independent-implementation" if you want to call it like that.

The core class is the [GraphicsDeviceControl](https://github.com/BlizzCrafter/MonoGame.Forms/blob/master/MonoGame.Forms.DX/Controls/GraphicsDeviceControl.cs) which inherits from a regular System.Windows.Forms.Control and has the following core purpose:

- in [OnCreateControl()](https://github.com/BlizzCrafter/MonoGame.Forms/blob/master/MonoGame.Forms.DX/Controls/GraphicsDeviceControl.cs#L240) it creates the [GraphicsDevice](https://github.com/BlizzCrafter/MonoGame.Forms/blob/master/MonoGame.Forms.DX/Services/GraphicsDeviceService.cs) and a SwapChainRenderTarget
- in [BeginDraw()](https://github.com/BlizzCrafter/MonoGame.Forms/blob/master/MonoGame.Forms.DX/Controls/GraphicsDeviceControl.cs#L308) it sets the RenderTarget of the GraphicsDevice to the SwapChainRenderTarget
- in [EndDraw()](https://github.com/BlizzCrafter/MonoGame.Forms/blob/master/MonoGame.Forms.DX/Controls/GraphicsDeviceControl.cs#L338) the SwapChain presents the rendered image

It also handles client resizing and user input by converting the pressed keys to the XNA/MonoGame equivalents.

The general idea was to extend the library functionality later by [service classes](https://github.com/BlizzCrafter/MonoGame.Forms/tree/master/MonoGame.Forms.DX/Services) so that users would have a nice and easy time to start their own editor projects.

So if the user wants to have an updateable render surface, then the only thing he needs to do is to inherit from [MonoGameControl](https://github.com/BlizzCrafter/MonoGame.Forms/blob/master/MonoGame.Forms.DX/Controls/MonoGameControl.cs) like this:

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
            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
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

**"Editor"** is the underlying [GFXService](https://github.com/BlizzCrafter/MonoGame.Forms/blob/master/MonoGame.Forms.DX/Services/GFXService.cs) class, which contains basic XNA/MonoGame functionality as well as helper classes like a [RenderTargetManager](https://github.com/BlizzCrafter/MonoGame.Forms/blob/master/MonoGame.Forms.DX/Services/GFXService.cs#L27) which makes working with render targets easier (client resizing etc.). Generally you will find additional useful stuff to jumpstart your editor creation process. Fell free to take a look at the [Wiki](https://github.com/BlizzCrafter/MonoGame.Forms/wiki) to discover more of them.

Have fun and a nice day :sunglasses:

:: BlizzCrafter

---

https://github.com/BlizzCrafter/MonoGame.Forms
