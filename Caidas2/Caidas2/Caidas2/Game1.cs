using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Caidas2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D[] textABC=new Texture2D[27];
        string[] abc = new string[27] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "ñ", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y","z" };
        Rectangle[] recABC = new Rectangle[27];
        
        
        Texture2D caja;
        Texture2D repeat;
        Texture2D letra;
        Texture2D bien;
        Texture2D otraCaja;
        Texture2D mal;
        Rectangle recCaja;
        Rectangle recCaja2;
        Rectangle recRepeat;
        Rectangle recPlayer;
        int cosa = 0;
        int flag = 0;
        int bandera = 0;
        int pressedX, pressedY;
        bool noDraw = false;
        bool malTocado = false;
        bool clicked = false;
        bool clicked2 = false;
        int cosa2 = 0;
        MouseState currentMouseState;
        MouseState previousMouseState;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            IsMouseVisible = true;
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

            caja = Content.Load<Texture2D>("caja");
            repeat= Content.Load<Texture2D>("repeat");
            otraCaja = Content.Load<Texture2D>("caja2");
            mal = Content.Load<Texture2D>("cancel");
            letra = Content.Load<Texture2D>("a");
            bien = Content.Load<Texture2D>("Bien");
            for (int i = 0; i < textABC.Length; i++)
            {
                textABC[i] = Content.Load<Texture2D>(abc[i]);
            }
            recPlayer = new Rectangle(300, cosa, letra.Width, letra.Height);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
        MouseState current, last;
        private float holdTimer;
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            

            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            if ((previousMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Pressed) && (recPlayer.Contains(currentMouseState.X, currentMouseState.Y) && (recPlayer.Contains(previousMouseState.X, previousMouseState.Y))))
            {
                
                clicked = true;
                clicked2 = true;
                flag = 1;
                bandera = 1;
                pressedX = currentMouseState.X-30;
                pressedY = currentMouseState.Y-30;
            }
            else if (previousMouseState.LeftButton == ButtonState.Released & currentMouseState.LeftButton == ButtonState.Pressed) 
            {
                clicked = false;
                clicked2 = false;
            }
            else if (previousMouseState.LeftButton == ButtonState.Released & currentMouseState.LeftButton == ButtonState.Released)
            {
                clicked = false;
                clicked2 = false;
                noDraw = false;
                malTocado = false;
            }
            if ((previousMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Pressed) && (recRepeat.Contains(currentMouseState.X, currentMouseState.Y)))
            {
                clicked = true;
                clicked2 = true;
                flag = 0;
                bandera = 0;
                pressedX = currentMouseState.X-30;
                pressedY = currentMouseState.Y-30;
                cosa = 0;
                cosa2 = 0;
                noDraw = false;
                malTocado = false;
                clicked = false;
                clicked2 = false;
            }
            // TODO: Add your update logic here
            cosa2++;
            recCaja = new Rectangle(50, 350, caja.Width, caja.Height);
            recCaja2 = new Rectangle(200, 350, otraCaja.Width, otraCaja.Height);
            recRepeat = new Rectangle(500, 10,repeat.Width,repeat.Height);
            if (clicked)
            {
                recPlayer = new Rectangle(currentMouseState.X-30, currentMouseState.Y-30, letra.Width, letra.Height);
               
            }
            else
            {
                if (flag==0)
                {
                    recPlayer = new Rectangle(100, cosa2, letra.Width, letra.Height);
                }
                else if (flag == 1)
                {
                    cosa2 = pressedY;
                    recPlayer = new Rectangle(pressedX, cosa2, letra.Width, letra.Height);
                }
               
            }
            if (bandera==1)
            {
                if (recCaja.Intersects(recPlayer)&&!clicked2)
                {
                    noDraw = true;
                }
                else if (recCaja2.Intersects(recPlayer) && !clicked2)
                {
                    malTocado = true;
                }
            }
            
            if (cosa == 400)
            {
                cosa = 0;
                bandera = 0;
                flag = 0;
                cosa2 = 0;
                GraphicsDevice.Clear(Color.CornflowerBlue);
                noDraw = false;
                malTocado = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
           //////////////////////////////////////////////////////////////////
                        
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            if (!noDraw && !malTocado)
            {
                if (clicked)
                {
                    spriteBatch.Draw(letra, new Vector2(currentMouseState.X-30, currentMouseState.Y-30), Color.White);
                }
                else
                {
                    if (flag == 0)
                    {
                        cosa++;
                        spriteBatch.Draw(letra, new Vector2(300, cosa), Color.White);
                        recPlayer = new Rectangle(300, cosa, letra.Width, letra.Height);
                    }
                    else if (flag == 1)
                    {
                        //cosa = pressedY;
                        //cosa++;
                        pressedY++;
                        spriteBatch.Draw(letra, new Vector2(pressedX, pressedY), Color.White);
                    }

                }

                spriteBatch.Draw(caja, new Vector2(50, 350), Color.White);
                spriteBatch.Draw(otraCaja, new Vector2(200, 350), Color.White);
            }
            else
            {
                if (noDraw)
                {
                    GraphicsDevice.Clear(Color.Orange);
                    spriteBatch.Draw(bien, new Vector2(200, 200), Color.White);
                }
                if (malTocado)
                {
                    GraphicsDevice.Clear(Color.Orange);
                    spriteBatch.Draw(mal, new Vector2(200, 200), Color.White);
                }
            }
           
            spriteBatch.Draw(repeat, new Vector2(500, 10), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
