using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.IO;
using System.Reflection;
using TouchTracking;

namespace LazySusanApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        SKPaint backgroundFillPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill
        };

        public MainPage()
        {
            InitializeComponent();

            shaderFill("LazySusanApp.Images.Table_Cloth_Red.jpg", backgroundFillPaint);
        }

        void sliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            if (canvasView != null)
            {
                canvasView.InvalidateSurface();
            }
        }

        private void canvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;


            // Background Color
            canvas.DrawPaint(backgroundFillPaint);

            // Find width and height of screen in pixels
            int width = e.Info.Width;
            int height = e.Info.Height;

            //Set transforms to change object to specified size and place
            canvas.Translate(width / 2, height);
            canvas.Scale(Math.Min(width / 240f, height / 410f));

            //Lazy Susan
            using (SKPaint lazySusanFillPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill
            })
            {
                canvas.RotateDegrees((float)rotateSlider.Value);
                shaderFill("LazySusanApp.Images.Cherry_WG.jpg", lazySusanFillPaint);
                canvas.DrawCircle(0, 0, 180, lazySusanFillPaint);   
            }           
        }

        //Shader that fills in SKPaint objects with embedded images
        public void shaderFill(string link, SKPaint x)
        {
            Assembly assembly1 = GetType().GetTypeInfo().Assembly;
            using (Stream stream1 = assembly1.GetManifestResourceStream(link))
            using (SKManagedStream skStream1 = new SKManagedStream(stream1))
            using (SKBitmap bitmap1 = SKBitmap.Decode(stream1))
            using (SKShader shader1 = SKShader.CreateBitmap(bitmap1, SKShaderTileMode.Mirror, SKShaderTileMode.Mirror))
            {
                x.Shader = shader1;
            }
        }

        
        private void TouchEffect_TouchAction(object sender, TouchTracking.TouchActionEventArgs args)
        {
            switch (args.Type)
            {
                case TouchActionType.Pressed:
                    break;

                case TouchActionType.Moved:
                    break;

                case TouchActionType.Released:
                    break;

                case TouchActionType.Cancelled:
                    break;
            }
        }
    }
}

