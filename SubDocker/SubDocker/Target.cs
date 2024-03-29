﻿using BEPUphysics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace SpaceDocker
{
    public class Target : DrawableGameComponent
    {
        private Model model;
        private BEPUphysics.Entities.Prefabs.Sphere physicsObject;

        Random rnd = new Random();

        public Vector3 modelPosition
        {
            get { return ConversionHelper.MathConverter.Convert(physicsObject.Position); }
            set { physicsObject.Position = ConversionHelper.MathConverter.Convert(value); }
        }

        public Target(Game game) : base(game)
        {
            game.Components.Add(this);
        }

        public Target(Game game, string id) : this(game)
        {
            Vector3 pos = GetRandomPosition();

            physicsObject = new BEPUphysics.Entities.Prefabs.Sphere(ConversionHelper.MathConverter.Convert(pos), 1);
            physicsObject.AngularDamping = 0f;
            physicsObject.LinearDamping = 0f;
            physicsObject.Tag = id;

            Game.Services.GetService<Space>().Add(physicsObject);
        }

        public Target(Game game, string id, Vector3 linMomentum, Vector3 angMomentum) : this(game, id)
        {
            // TODO - doesn't change the linear or angular momentum
            physicsObject.LinearMomentum = ConversionHelper.MathConverter.Convert(linMomentum);
            physicsObject.AngularMomentum = ConversionHelper.MathConverter.Convert(angMomentum);
        }
        
        /// <summary>
        /// Randomly returns a location of the target within a specific range from 0,0,0
        /// </summary>
        /// <returns></returns>
        private Vector3 GetRandomPosition()
        {
            int scaleValue = 100;
            Vector3 distanceMultipler = new Vector3(200, 200, 200);
            Vector3 distanceBetween = (new Vector3(
                (float)rnd.Next(-(int)(distanceMultipler.X), (int)(distanceMultipler.X)) + scaleValue,
                (float)rnd.Next(-(int)(distanceMultipler.Y), (int)(distanceMultipler.Y)) + scaleValue,
                (float)rnd.Next(-(int)(distanceMultipler.Z), (int)distanceMultipler.Z) + scaleValue)
                );
            return distanceBetween;
        }

        // resetting moves the target to a different random position
        public void Reset()
        {
            modelPosition = GetRandomPosition();
        }

        protected override void LoadContent()
        {
            model = Game.Content.Load<Model>("turtle");
            physicsObject.Radius = model.Meshes[0].BoundingSphere.Radius;

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.PreferPerPixelLighting = true;
                    effect.EnableDefaultLighting();
                    effect.World = ConversionHelper.MathConverter.Convert(physicsObject.WorldTransform);
                    effect.View = Main.camera.View;
                    effect.Projection = Main.camera.Projection;
                }
                mesh.Draw();
            }
            base.Draw(gameTime);
        }
    }
}
