# MonoGame-Sprites
Simple Sprites &amp; bone-like Transformations for MonoGame projects.

## What is it
This mini-lib implements basic sprite types with transformations, that also support hierarchy and bone-like transformation inheritance. 
In simple words, this provide the basics needed to load bone-based 2d animations and sprites.

## Live example
Open and execute the solution in this repo to see a basic example.

## Using MonoGame-Sprites

### Install
Just copy the files inside ```/Source/``` into your project and you're good to go.

### Main objects
This lib contains 4 simple classes and is very easy to use:

#### Renderable
Provide the basic API and functionality of any renderable entity. If you want to create your own custom sprite, inherit from it.

#### Container
An entity that doesn't have any graphic representation for itself, but have transformation which it can pass to its children.

#### Sprite
A simple renderable image with transformation and children.

#### Transformations
A collection of renderable transformation (position, scale, rotation, zindex, color..) with functionality to inherit and clone transformations.
Normally you don't need to use this class.

### How to use
To create a new sprite:
```cs
MonoSprites.Sprite sprite = new MonoSprites.Sprite(Content.Load<Texture2D>("sprite_texture"));
```

And later to draw it:
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

Note that in the drawing code above I set CullMode to None - this is important if you want to use flipping and negative scale.
In addition, if you want z-index support you need to set SpriteSortMode to one of the ordered deffered modes (like ```FrontToBack```).


To create a container and use it:
```cs
MonoSprites.Container container = new MonoSprites.Container();
MonoSprites.Sprite sprite = new MonoSprites.Sprite(Content.Load<Texture2D>("sprite_texture"), parent: container);
```

Now every transformation you apply on the container will also affect the child sprite.

For more info you can check out the code and public Renderable API (its quite short), or read the automatically generated help file in ```Help/```.
