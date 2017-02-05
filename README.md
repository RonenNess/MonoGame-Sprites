# MonoGame-Sprites
Simple Sprites &amp; bone-like Transformations for MonoGame projects.

## What is it
This mini-lib implements basic sprite class with transformations, that also support hierarchy and bone-like transformation inheritance. 

In simple words, this provide the very basics needed to create bone-based 2d animations.

## Live example
To see a live example, open and execute the solution in this repo.

## Using MonoGame-Sprites

### Install
Just copy the files inside ```/Source/``` into your project and you're good to go.

### Main objects
This lib contains 4 main classes you should know:

#### Renderable
Provide the basic API and functionality of any renderable entity. If you want to create your own custom sprite, inherit from it.

#### Container
An entity that doesn't have any graphic representation for itself, but have transformation which it can pass to its children. Containers are good method to group together sprites and apply global transformations, like position or scale, on all of them.

#### Sprite
A simple renderable image with transformation and children.

#### Transformations
A set of transformation properties, such as position, scale, rotation, zindex, color, etc.. Transformations class also implements the functionality to inherit transformations from another Transformations instance.
Normally you don't need to use this class, it is used internally.

### How to use
To create a new sprite:
```cs
MonoSprites.Sprite sprite = new MonoSprites.Sprite(Content.Load<Texture2D>("sprite_texture"));
```

And to draw the new sprite:
```cs
// begin drawing
RasterizerState rasterStrate = new RasterizerState();
rasterStrate.CullMode = CullMode.None;
spriteBatch.Begin(SpriteSortMode.FrontToBack, rasterizerState: rasterStrate);

// draw the sprite
container.Draw(sprite);

// end drawing
spriteBatch.End();
```

Note that in the drawing code above I set ```CullMode``` to None - this is important if you want to use flipping and negative scale.
In addition, if you want z-index support you need to set ```SpriteSortMode``` to one of the ordered deffered modes (like ```FrontToBack```).


To create a container and use it:
```cs
MonoSprites.Container container = new MonoSprites.Container();
MonoSprites.Sprite sprite = new MonoSprites.Sprite(Content.Load<Texture2D>("sprite_texture"), parent: container);
```

Now every transformation you apply on the container will also affect the child sprite.

For more info you can check out the code (with focus on the Renderable and Sprite public API), or read the automatically generated doc file in ```Help/```.

## Lisence
MonoGame-Sprites is distributed with the permissive MIT License. For more info, check out the ```LICENSE``` file in this repo.