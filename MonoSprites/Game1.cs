using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoSprites
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // test sprite parts
        MonoSprites.Container container;
        MonoSprites.Sprite spriteHead;
        MonoSprites.Sprite spriteBody;
        MonoSprites.Sprite spriteFrontHand;
        MonoSprites.Sprite spriteFrontPalm;
        MonoSprites.Sprite spriteBackHand;
        MonoSprites.Sprite spriteBackPalm;
        MonoSprites.Sprite spriteFrontLeg;
        MonoSprites.Sprite spriteFrontFoot;
        MonoSprites.Sprite spriteBackLeg;
        MonoSprites.Sprite spriteBackFoot;

        /// <summary>
        /// Create the game instance.
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content/bin";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            // init sprite parts.
            // note: normally you would load this part from a json or xml file.
            container = new MonoSprites.Container();
            spriteBody = new MonoSprites.Sprite(Content.Load<Texture2D>("sprite/body"), zindex: 0.1f, parent: container);
            spriteHead = new MonoSprites.Sprite(Content.Load<Texture2D>("sprite/head"), position: new Vector2(-10, -120), zindex: 0.2f, parent: spriteBody);
            spriteFrontHand = new MonoSprites.Sprite(Content.Load<Texture2D>("sprite/front_hand"), position: new Vector2(36, -55), origin: new Vector2(0.3f, 0.28f), zindex: 0.25f, parent: spriteBody);
            spriteFrontPalm = new MonoSprites.Sprite(Content.Load<Texture2D>("sprite/front_palm"), position: new Vector2(34, 50), origin: new Vector2(0.7f, 0.07f), zindex: 0.01f, parent: spriteFrontHand);
            spriteBackHand = new MonoSprites.Sprite(Content.Load<Texture2D>("sprite/back_hand"), position: new Vector2(-45, -70), origin: new Vector2(0.6f, 0.12f), zindex: -0.1f, parent: spriteBody);
            spriteBackPalm = new MonoSprites.Sprite(Content.Load<Texture2D>("sprite/back_palm"), position: new Vector2(-14, 72), origin: new Vector2(0.8f, 0.08f), zindex: -0.01f, parent: spriteBackHand);
            spriteFrontLeg = new MonoSprites.Sprite(Content.Load<Texture2D>("sprite/front_leg"), position: new Vector2(15, 75), origin: new Vector2(0.27f, 0.16f), zindex: 0.15f, parent: spriteBody);
            spriteFrontFoot = new MonoSprites.Sprite(Content.Load<Texture2D>("sprite/front_foot"), position: new Vector2(50, 165), origin: new Vector2(0.67f, 0.36f), zindex: -0.01f, parent: spriteFrontLeg);
            spriteBackLeg = new MonoSprites.Sprite(Content.Load<Texture2D>("sprite/back_leg"), position: new Vector2(-34, 82), origin: new Vector2(0.56f, 0.16f), zindex: -0.15f, parent: spriteBody);
            spriteBackFoot = new MonoSprites.Sprite(Content.Load<Texture2D>("sprite/back_foot"), position: new Vector2(0, 140), origin: new Vector2(0.8f, 0.18f), zindex: -0.01f, parent: spriteBackLeg);
            
            // set sprite position and general scale
            container.ScaleScalar = 0.7f;
            container.Position = new Vector2(400, 200);
            container.Zindex = 0.5f;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // rotate parts
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                spriteFrontHand.Rotation = spriteFrontHand.Rotation + 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                spriteBackHand.Rotation = spriteBackHand.Rotation + 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                spriteFrontLeg.Rotation = spriteFrontLeg.Rotation + 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                spriteBackLeg.Rotation = spriteBackLeg.Rotation + 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                container.Color = Color.White;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                container.Color = Color.Red;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                container.Color = Color.Blue;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D4))
            {
                container.Color = Color.Green;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D5))
            {
                container.Color = Color.HotPink;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D6))
            {
                container.Color = Color.Gold;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D7))
            {
                container.Color = Color.Brown;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D8))
            {
                container.Color = Color.DarkGray;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D9))
            {
                container.Color = Color.DarkTurquoise;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D0))
            {
                container.Color = new Color(1, 1, 1, 0.5f);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // rotate the whole guy
            container.Rotation += 0.001f;

            // draw the sprite
            RasterizerState rasterStrate = new RasterizerState();
            rasterStrate.CullMode = CullMode.None;
            spriteBatch.Begin(SpriteSortMode.FrontToBack, rasterizerState: rasterStrate);
            container.Draw(spriteBatch);
            spriteBatch.End();

            // draw instructions
            SpriteFont font = Content.Load<SpriteFont>("font");
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Use arrows to rotate parts, numbers to switch colors. Sprite from: 'SCRAP PIRATES'.", new Vector2(5, 5), Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
