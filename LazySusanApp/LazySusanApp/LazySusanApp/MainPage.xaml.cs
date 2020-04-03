using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace LazySusanApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        SKPaint blackFilledPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColors.Black
        };

        SKPaint whiteStrokePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.White,
            StrokeWidth = 2,
            StrokeCap = SKStrokeCap.Round,
            IsAntialias = true
        };

        public MainPage()
        {
            InitializeComponent();
        }

        private void canvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear(SKColors.CornflowerBlue);

            int width = e.Info.Width;
            int height = e.Info.Height;

            //Set transforms
            canvas.Translate(width / 2, height / 2);
            canvas.Scale(width / 200f);

            //Get DateTime
            DateTime dateTime = DateTime.Now;

            //Clock background
            canvas.DrawCircle(0, 0, 100, blackFilledPaint);

            //Hour hand
            canvas.RotateDegrees(30 * dateTime.Hour + dateTime.Minute / 2f);
            whiteStrokePaint.StrokeWidth = 15;
            canvas.DrawLine(0, 0, 0, -50, whiteStrokePaint);

            //dimensions for lazy susan
            //canvas.Translate(width / 2, height / 5);
            //canvas.Scale(width / 160f);
        }
    }
}
