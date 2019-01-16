# MonoGame-Sprites
Simple Sprites &amp; bone-like Transformations for MonoGame projects.

## What is it
This mini-lib implements basic sprite class with transformations, that also support hierarchy and bone-like transformation inheritance. 

In simple words, this provide the very basics needed to create bone-based 2d animations.

## Live example
To see a live example, open and execute the solution in this repo.

## Using MonoGame-Sprites

### Install

To install the lib you can use NuGet:

```
Install-Package MonoGame.Sprites
```

Or you can manually copy the source files from ```MonoSprites/Source/``` into your project.

### Main objects
This lib contains 4 main classes you should know:

#### Renderable
Provide the basic API and functionality of any renderable entity. If you want to create your own custom sprites, inherit from it.

#### Container
An entity that doesn't have any graphic representation of its own, but hold transformations to inherit to its children. Containers are good method to group together multiple sprites. For example, you can use it to scale or move a group of renderables evenly.

#### Sprite
A simple renderable image with transformation and optional children.

#### Transformations
A set of transformation properties, such as position, scale, rotation, zindex, color, etc.. 

Transformations class also implements the functionality to inherit transformations from parent to child instances.
Normally you don't need to use this class directly, as it is used internally.

### How to use
To create a new sprite:

```cs
MonoSprites.Sprite sprite = new MonoSprites.Sprite(Content.Load<Texture2D>("sprite_texture"));
```

And to draw it:

```cs
spriteBatch.Begin(SpriteSortMode.FrontToBack);
sprite.Draw(spriteBatch);
spriteBatch.End();
```

To create a container and use it:

```cs
MonoSprites.Container container = new MonoSprites.Container();
MonoSprites.Sprite sprite = new MonoSprites.Sprite(Content.Load<Texture2D>("sprite_texture"), parent: container);
// or: container.AddChild(Sprite);
```

Now every transformation you apply on the container will be inherited to the child sprite.

For more info you can check out the code (with focus on the Renderable and Sprite public API), or read the automatically generated doc file in ```MonoSprites/Help/```.

## Lisence

MonoGame-Sprites is distributed with the permissive MIT License. For more info, check out the ```LICENSE``` file in this repo.