using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace PerfectCamera
{
    public static class extension
    {
        private const float PreMultiplyFactor = 1 / 255f;

          /// <summary>
      /// Sets the color of the pixel using a precalculated index (faster).
      /// </summary>
      /// <param name="bmp">The WriteableBitmap</param>
      /// <param name="index">The coordinate index.</param>
      /// <param name="r">The red value of the color.</param>
      /// <param name="g">The green value of the color.</param>
      /// <param name="b">The blue value of the color.</param>
      public static void SetPixeli(this WriteableBitmap bmp, int index, byte r, byte g, byte b)
      {
         bmp.Pixels[index] = (255 << 24) | (r << 16) | (g << 8) | b;
      }

      /// <summary>
      /// Sets the color of the pixel.
      /// Inspired by Bill Reiss (http://blogs.silverarcade.com/silverlight-games-101/15/silverlight-writeablebitmap-pixel-format-in-silverlight-3/)
      /// </summary>
      /// <param name="bmp">The WriteableBitmap</param>
      /// <param name="x">The x coordinate (row).</param>
      /// <param name="y">The y coordinate (column).</param>
      /// <param name="r">The red value of the color.</param>
      /// <param name="g">The green value of the color.</param>
      /// <param name="b">The blue value of the color.</param>
      public static void SetPixel(this WriteableBitmap bmp, int x, int y, byte r, byte g, byte b)
      {
         bmp.Pixels[x * bmp.PixelWidth + y] = (255 << 24) | (r << 16) | (g << 8) | b;
      }


      #region With alpha

      /// <summary>
      /// Sets the color of the pixel including the alpha value and using a precalculated index (faster).
      /// </summary>
      /// <param name="bmp">The WriteableBitmap</param>
      /// <param name="index">The coordinate index.</param>
      /// <param name="a">The alpha value of the color.</param>
      /// <param name="r">The red value of the color.</param>
      /// <param name="g">The green value of the color.</param>
      /// <param name="b">The blue value of the color.</param>
      public static void SetPixeli(this WriteableBitmap bmp, int index, byte a, byte r, byte g, byte b)
      {
         float ai = a * PreMultiplyFactor;
         bmp.Pixels[index] = (a << 24) | ((byte)(r * ai) << 16) | ((byte)(g * ai) << 8) | (byte)(b * ai);
      }

      /// <summary>
      /// Sets the color of the pixel including the alpha value.
      /// Inspired by Bill Reiss (http://blogs.silverarcade.com/silverlight-games-101/15/silverlight-writeablebitmap-pixel-format-in-silverlight-3/)
      /// </summary>
      /// <param name="bmp">The WriteableBitmap</param>
      /// <param name="x">The x coordinate (row).</param>
      /// <param name="y">The y coordinate (column).</param>
      /// <param name="a">The alpha value of the color.</param>
      /// <param name="r">The red value of the color.</param>
      /// <param name="g">The green value of the color.</param>
      /// <param name="b">The blue value of the color.</param>
      public static void SetPixel(this WriteableBitmap bmp, int x, int y, byte a, byte r, byte g, byte b)
      {
         float ai = a * PreMultiplyFactor;
         bmp.Pixels[x * bmp.PixelWidth + y] = (a << 24) | ((byte)(r * ai) << 16) | ((byte)(g * ai) << 8) | (byte)(b * ai);
      }

      #endregion

      #region With System.Windows.Media.Color

      /// <summary>
      /// Sets the color of the pixel using a precalculated index (faster).
      /// </summary>
      /// <param name="bmp">The WriteableBitmap</param>
      /// <param name="index">The coordinate index.</param>
      /// <param name="color">The color.</param>
      public static void SetPixeli(this WriteableBitmap bmp, int index, Color color)
      {
         float ai = color.A * PreMultiplyFactor;
         bmp.Pixels[index] = (color.A << 24) | ((byte)(color.R * ai) << 16) | ((byte)(color.G * ai) << 8) | (byte)(color.B * ai);
      }

      /// <summary>
      /// Sets the color of the pixel.
      /// </summary>
      /// <param name="bmp">The WriteableBitmap</param>
      /// <param name="x">The x coordinate (row).</param>
      /// <param name="y">The y coordinate (column).</param>
      /// <param name="color">The color.</param>
      public static void SetPixel(this WriteableBitmap bmp, int x, int y, Color color)
      {
         float ai = color.A * PreMultiplyFactor;
         bmp.Pixels[x * bmp.PixelWidth + y] = (color.A << 24) | ((byte)(color.R * ai) << 16) | ((byte)(color.G * ai) << 8) | (byte)(color.B * ai);
      }

      /// <summary>
      /// Sets the color of the pixel using an extra alpha value and a precalculated index (faster).
      /// </summary>
      /// <param name="bmp">The WriteableBitmap</param>
      /// <param name="index">The coordinate index.</param>
      /// <param name="color">The color.</param>
      public static void SetPixeli(this WriteableBitmap bmp, int index, byte a, Color color)
      {
         float ai = a * PreMultiplyFactor;
         bmp.Pixels[index] = (a << 24) | ((byte)(color.R * ai) << 16) | ((byte)(color.G * ai) << 8) | (byte)(color.B * ai);
      }

      /// <summary>
      /// Sets the color of the pixel using an extra alpha value.
      /// </summary>
      /// <param name="bmp">The WriteableBitmap</param>
      /// <param name="x">The x coordinate (row).</param>
      /// <param name="y">The y coordinate (column).</param>
      /// <param name="a">The alpha value of the color.</param>
      /// <param name="color">The color.</param>
      public static void SetPixel(this WriteableBitmap bmp, int x, int y, byte a, Color color)
      {
         float ai = a * PreMultiplyFactor;
         bmp.Pixels[x * bmp.PixelWidth + y] = (a << 24) | ((byte)(color.R * ai) << 16) | ((byte)(color.G * ai) << 8) | (byte)(color.B * ai);
      }

      #endregion

    }
}
