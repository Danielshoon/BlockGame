﻿///Wrappers for custom HLSL shaders
///© 2013 Spine Games

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Block_Game.Render
{
    public class PointLightEffect
    {
        public readonly Effect BaseEffect;

        const int MAX_LIGHTS = 50;
        
        #region Transform
        Matrix world;
        public Matrix World
        {
            get { return world; }
            set
            {
                world = value;
                BaseEffect.Parameters["World"].SetValue(world);
            }
        }
        Matrix view;
        public Matrix View
        {
            get { return view; }
            set
            {
                view = value;
                BaseEffect.Parameters["View"].SetValue(view);
            }
        }
        Matrix projection;
        public Matrix Projection
        {
            get { return projection; }
            set
            {
                projection = value;
                BaseEffect.Parameters["Projection"].SetValue(projection);
            }
        }

        //Matrix worldInverseTranspose;
        ///// <summary>
        ///// The 
        ///// </summary>
        //public Matrix WorldInverseTranspose
        //{
        //    get { return worldInverseTranspose; }
        //    set
        //    {
        //        worldInverseTranspose = value;
        //        BaseEffect.Parameters["WorldInverseTranspose"].SetValue(worldInverseTranspose);
        //    }
        //}
        #endregion

        #region DefaultLighting
        Vector4 ambientLightColor = new Vector4(1, 1, 1, 1);
        /// <summary>
        /// The color of the ambient lighting
        /// </summary>
        public Vector4 AmbientLightColor
        {
            get { return ambientLightColor; }
            set
            {
                ambientLightColor = value;
                BaseEffect.Parameters["AmbientColor"].SetValue(ambientLightColor);
            }
        }

        float ambientLightIntesity = 1.0F;
        /// <summary>
        /// The intensity for the ambient lighting. Default is 0.1
        /// </summary>
        public float AmbientLightIntensity
        {
            get { return ambientLightIntesity; }
            set
            {
                ambientLightIntesity = value;
                BaseEffect.Parameters["AmbientIntensity"].SetValue(ambientLightIntesity);
            }
        }

        /// <summary>
        /// Represents the normal for the diffuse light
        /// </summary>
        public Vector3 DiffuseDirection
        {
            get { return BaseEffect.Parameters["DiffuseLightDirection"].GetValueVector3(); }
            set { BaseEffect.Parameters["DiffuseLightDirection"].SetValue(value); }
        }

        /// <summary>
        /// The color for the diffuse light
        /// </summary>
        public Vector4 DiffuseColor
        {
            get { return BaseEffect.Parameters["DiffuseColor"].GetValueVector4(); }
            set { BaseEffect.Parameters["DiffuseColor"].SetValue(value); }
        }

        /// <summary>
        /// The intensity of the diffuse
        /// </summary>
        public float DiffuseIntensity
        {
            get { return BaseEffect.Parameters["DiffuseIntensity"].GetValueSingle(); }
            set { BaseEffect.Parameters["DiffuseIntensity"].SetValue(value); }
        }

        #region Point Lights
        /// <summary>
        /// The array of all positions for point lights
        /// </summary>
        Vector3[] LightPositions = new Vector3[MAX_LIGHTS];

        /// <summary>
        /// The array of all Colors for point lights
        /// </summary>
        public Vector4[] LightDiffuseColors = new Vector4[MAX_LIGHTS];

        /// <summary>
        /// The array of all the light radui for point lights
        /// </summary>
        public float[] LightDistances = new float[MAX_LIGHTS];

        /// <summary>
        /// The number of point lights currently used
        /// </summary>
        int lightCount = 0;
        #endregion
        #endregion

        Texture2D texture;
        public Texture2D Texture
        {
            get { return texture; }
            set {
                texture = value;
                BaseEffect.Parameters["Texture"].SetValue(texture);
            }
        }

        public PointLightEffect(ContentManager content)
        {
            BaseEffect = content.Load<Effect>("Shaders/PointLight");
        }

        public PointLightEffect(Effect BaseEffect)
        {
            this.BaseEffect = BaseEffect;
        }

        public PointLightEffect Clone()
        {
            PointLightEffect temp = new PointLightEffect(BaseEffect.Clone());

            temp.World = World;
            temp.View = View;
            temp.Projection = Projection;

            temp.AmbientLightColor = AmbientLightColor;
            temp.AmbientLightIntensity = AmbientLightIntensity;
            
            //temp.WorldInverseTranspose = WorldInverseTranspose;

            return temp;
        }

        public int AddPointLight(PointLight light)
        {
            if (lightCount + 1 < MAX_LIGHTS)
            {
                LightPositions[lightCount] = light.LightPos;

                LightDiffuseColors[lightCount] = light.DiffuseColor;
                LightDistances[lightCount] = light.Radius;

                lightCount++;

                UpdatePointLightLists();

                return lightCount - 1;
            }
            else
                return -1;
        }

        public void SetLightPos(int lightID, Vector3 pos)
        {
            if (lightID >= 0 & lightID < lightCount)
            {
                LightPositions[lightID] = pos;
                
                UpdatePointLightLists();
            }
        }

        public void SetLightColor(int lightID, Vector4 color)
        {
            if (lightID >= 0 & lightID < lightCount)
            {
                LightDiffuseColors[lightID] = color;

                UpdatePointLightLists();
            }
        }

        public void SetLightRadius(int lightID, float radius)
        {
            if (lightID >= 0 & lightID < lightCount)
            {
                LightDistances[lightID] = radius;

                UpdatePointLightLists();
            }
        }

        private void UpdatePointLightLists()
        {
            BaseEffect.Parameters["LightPositions"].SetValue(LightPositions);
            BaseEffect.Parameters["LightDiffuseColors"].SetValue(LightDiffuseColors);
            BaseEffect.Parameters["LightDistances"].SetValue(LightDistances);

            BaseEffect.Parameters["lightCount"].SetValue(lightCount);
        }
    }

    public class NormalMapEffect
    {
        public readonly Effect BaseEffect;

        const int MAX_LIGHTS = 50;

        #region Transform
        Matrix world;
        public Matrix World
        {
            get { return world; }
            set
            {
                world = value;
                BaseEffect.Parameters["World"].SetValue(world);
            }
        }
        Matrix view;
        public Matrix View
        {
            get { return view; }
            set
            {
                view = value;
                BaseEffect.Parameters["View"].SetValue(view);
            }
        }
        Matrix projection;
        public Matrix Projection
        {
            get { return projection; }
            set
            {
                projection = value;
                BaseEffect.Parameters["Projection"].SetValue(projection);
            }
        }

        //Matrix worldInverseTranspose;
        ///// <summary>
        ///// The 
        ///// </summary>
        //public Matrix WorldInverseTranspose
        //{
        //    get { return worldInverseTranspose; }
        //    set
        //    {
        //        worldInverseTranspose = value;
        //        BaseEffect.Parameters["WorldInverseTranspose"].SetValue(worldInverseTranspose);
        //    }
        //}
        #endregion

        #region DefaultLighting
        Vector4 ambientLightColor = new Vector4(1, 1, 1, 1);
        /// <summary>
        /// The color of the ambient lighting
        /// </summary>
        public Vector4 AmbientLightColor
        {
            get { return ambientLightColor; }
            set
            {
                ambientLightColor = value;
                BaseEffect.Parameters["AmbientColor"].SetValue(ambientLightColor);
            }
        }

        float ambientLightIntesity = 1.0F;
        /// <summary>
        /// The intensity for the ambient lighting. Default is 0.1
        /// </summary>
        public float AmbientLightIntensity
        {
            get { return ambientLightIntesity; }
            set
            {
                ambientLightIntesity = value;
                BaseEffect.Parameters["AmbientIntensity"].SetValue(ambientLightIntesity);
            }
        }

        /// <summary>
        /// Represents the normal for the diffuse light
        /// </summary>
        public Vector3 DiffuseDirection
        {
            get { return BaseEffect.Parameters["lightDirection"].GetValueVector3(); }
            set { BaseEffect.Parameters["lightDirection"].SetValue(value); }
        }

        /// <summary>
        /// The color for the diffuse light
        /// </summary>
        public Vector4 DiffuseColor
        {
            get { return BaseEffect.Parameters["diffuseColor"].GetValueVector4(); }
            set { BaseEffect.Parameters["diffuseColor"].SetValue(value); }
        }
        #endregion

        Texture2D texture;
        public Texture2D Texture
        {
            get { return texture; }
            set
            {
                texture = value;
                BaseEffect.Parameters["Texture"].SetValue(texture);
            }
        }
        
        Texture2D normalMap;
        public Texture2D NormalMap
        {
            get { return normalMap; }
            set
            {
                normalMap = value;
                BaseEffect.Parameters["NormalMap"].SetValue(normalMap);
            }
        }

        public NormalMapEffect(ContentManager content)
        {
            BaseEffect = content.Load<Effect>("Shaders/bumpmap");
        }

        public NormalMapEffect(Effect BaseEffect)
        {
            this.BaseEffect = BaseEffect;
        }

        public NormalMapEffect Clone()
        {
            NormalMapEffect temp = new NormalMapEffect(BaseEffect.Clone());

            temp.World = World;
            temp.View = View;
            temp.Projection = Projection;

            temp.AmbientLightColor = AmbientLightColor;
            temp.AmbientLightIntensity = AmbientLightIntensity;

            temp.DiffuseColor = DiffuseColor;
            temp.DiffuseDirection = DiffuseDirection;

            temp.Texture = Texture;
            temp.NormalMap = NormalMap;

            return temp;
        }
    }

    public class StandardEffect
    {
        public readonly Effect BaseEffect;

        const int MAX_LIGHTS = 50;

        #region Transform
        Matrix world;
        public Matrix World
        {
            get { return world; }
            set
            {
                world = value;
                BaseEffect.Parameters["World"].SetValue(world);
            }
        }
        Matrix view;
        public Matrix View
        {
            get { return view; }
            set
            {
                view = value;
                BaseEffect.Parameters["View"].SetValue(view);
            }
        }
        Matrix projection;
        public Matrix Projection
        {
            get { return projection; }
            set
            {
                projection = value;
                BaseEffect.Parameters["Projection"].SetValue(projection);
            }
        }
        #endregion

        #region DefaultLighting
        Vector4 ambientLightColor = new Vector4(1, 1, 1, 1);
        /// <summary>
        /// The color of the ambient lighting
        /// </summary>
        public Vector4 AmbientLightColor
        {
            get { return ambientLightColor; }
            set
            {
                ambientLightColor = value;
                BaseEffect.Parameters["AmbientColor"].SetValue(ambientLightColor);
            }
        }

        float ambientLightIntesity = 1.0F;
        /// <summary>
        /// The intensity for the ambient lighting. Default is 0.1
        /// </summary>
        public float AmbientLightIntensity
        {
            get { return ambientLightIntesity; }
            set
            {
                ambientLightIntesity = value;
                BaseEffect.Parameters["AmbientIntensity"].SetValue(ambientLightIntesity);
            }
        }

        /// <summary>
        /// Represents the normal for the diffuse light
        /// </summary>
        public Vector3 DiffuseDirection
        {
            get { return BaseEffect.Parameters["DiffuseLightDirection"].GetValueVector3(); }
            set { BaseEffect.Parameters["DiffuseLightDirection"].SetValue(value); }
        }

        /// <summary>
        /// The color for the diffuse light
        /// </summary>
        public Vector4 DiffuseColor
        {
            get { return BaseEffect.Parameters["DiffuseColor"].GetValueVector4(); }
            set { BaseEffect.Parameters["DiffuseColor"].SetValue(value); }
        }

        /// <summary>
        /// The color for the diffuse light
        /// </summary>
        public float DiffuseIntensity
        {
            get { return BaseEffect.Parameters["DiffuseIntensity"].GetValueSingle(); }
            set { BaseEffect.Parameters["DiffuseIntensity"].SetValue(value); }
        }
        #endregion

        Texture2D texture;
        public Texture2D Texture
        {
            get { return texture; }
            set
            {
                texture = value;
                BaseEffect.Parameters["Texture"].SetValue(texture);
            }
        }

        public StandardEffect(ContentManager content)
        {
            BaseEffect = content.Load<Effect>("Shaders/StandardShader");
        }

        public StandardEffect(Effect BaseEffect)
        {
            this.BaseEffect = BaseEffect;
        }

        public StandardEffect Clone()
        {
            StandardEffect temp = new StandardEffect(BaseEffect.Clone());

            temp.World = World;
            temp.View = View;
            temp.Projection = Projection;

            temp.AmbientLightColor = AmbientLightColor;
            temp.AmbientLightIntensity = AmbientLightIntensity;

            temp.DiffuseColor = DiffuseColor;
            temp.DiffuseDirection = DiffuseDirection;
            temp.DiffuseIntensity = DiffuseIntensity;

            temp.Texture = Texture;

            return temp;
        }
    }

    public struct PointLight
    {
        public Vector4 DiffuseColor;
        public float Radius;
        public Vector3 LightPos;
    };
}
