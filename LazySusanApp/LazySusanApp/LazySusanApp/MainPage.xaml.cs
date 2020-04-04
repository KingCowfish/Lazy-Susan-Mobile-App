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

        SKPaint whiteFillPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColors.White

        };

        SKPaint greenFilledPaint = new SKPaint()
        {
            Style = SKPaintStyle.Fill,
            Color = SKColors.PaleGreen
        };

        SKPaint blackStrokePaint = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Black,
            StrokeWidth = 20,
            StrokeCap = SKStrokeCap.Round

        };

        SKPath catEarPath = new SKPath();
        SKPath catEyePath = new SKPath();
        SKPath catPupilPath = new SKPath();
        SKPath catTailPath = new SKPath();

        public MainPage()
        {
            InitializeComponent();

            //Make cat ear path
            catEarPath.MoveTo(0, 0);
            catEarPath.LineTo(0, 75);
            catEarPath.LineTo(100, 75);
            catEarPath.Close();

            //Make cat eye path
            catEyePath.MoveTo(0, 0);
            catEyePath.ArcTo(50, 50, 0, SKPathArcSize.Small, SKPathDirection.Clockwise, 50, 0);
            catEyePath.ArcTo(50, 50, 0, SKPathArcSize.Small, SKPathDirection.Clockwise, 0, 0);
            catEyePath.Close();

            //Make eye pupil
            catPupilPath.MoveTo(25, -5);
            catEyePath.ArcTo(6, 6, 0, SKPathArcSize.Small, SKPathDirection.Clockwise, 25, 5);
            catEyePath.ArcTo(6, 6, 0, SKPathArcSize.Small, SKPathDirection.Clockwise, 25, -5);
            catPupilPath.Close();

            //Make cat tail path
            catTailPath.MoveTo(0, 100);
            catTailPath.CubicTo(50, 200, 0, 250, -50, 200);



            Device.StartTimer(TimeSpan.FromSeconds(1f / 60), () =>
            {
                canvasView.InvalidateSurface();
                return true;
            });
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
            canvas.Scale(Math.Min(width / 210f, height / 520f));

            //Get DateTime
            DateTime dateTime = DateTime.Now;

            //Head
            canvas.DrawCircle(0, -160, 75, blackFilledPaint);

            //Draw ears and eyes
            for (int i = 0; i < 2; i++)
            {
                canvas.Save();
                canvas.Scale(2 * i - 1, 1);

                canvas.Save();
                canvas.Translate(-65, -255);
                canvas.DrawPath(catEarPath, blackFilledPaint);
                canvas.Restore();

                canvas.Save();
                canvas.Translate(10, -170);
                canvas.DrawPath(catEyePath, greenFilledPaint);
                canvas.DrawPath(catPupilPath, blackFilledPaint);
                canvas.Restore();

                //Draw whiskers
                canvas.DrawLine(10, -120, 100, -100, whiteStrokePaint);
                canvas.DrawLine(10, -125, 100, -120, whiteStrokePaint);
                canvas.DrawLine(10, -130, 100, -140, whiteStrokePaint);
                canvas.DrawLine(10, -135, 100, -160, whiteStrokePaint);


                canvas.Restore();
            }

            //Draw Tail
            canvas.DrawPath(catTailPath, blackStrokePaint);

            //Clock background
            canvas.DrawCircle(0, 0, 100, blackFilledPaint);

            //Hour and minute marks --------------------important----------------------------
            for (int angle = 0; angle < 360 ; angle += 6)
            {
                canvas.DrawCircle(0, -90, angle % 30 == 0 ? 4 : 2, whiteFillPaint);
                canvas.RotateDegrees(6);
            }


            //Hour hand
            canvas.Save();
            canvas.RotateDegrees(30 * dateTime.Hour + dateTime.Minute / 2f);
            whiteStrokePaint.StrokeWidth = 15;
            canvas.DrawLine(0, 0, 0, -50, whiteStrokePaint);
            canvas.Restore();

            //Minute hand
            canvas.Save();
            canvas.RotateDegrees(6 * dateTime.Minute + dateTime.Second / 10f);
            whiteStrokePaint.StrokeWidth = 10;
            canvas.DrawLine(0, 0, 0, -70, whiteStrokePaint);
            canvas.Restore();

            //Second hand
            canvas.Save();
            float seconds = dateTime.Second + dateTime.Millisecond / 1000f;
            canvas.RotateDegrees(6 * seconds);
            whiteStrokePaint.StrokeWidth = 2;
            canvas.DrawLine(0, 10, 0, -80, whiteStrokePaint);
            canvas.Restore();

            //dimensions for lazy susan
            //canvas.Translate(width / 2, height / 5);
            //canvas.Scale(width / 160f);
        }
    }
}
