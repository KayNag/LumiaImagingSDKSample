/*
 * Copyright (c) 2014 Microsoft Mobile
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */


using NISDKExtendedEffects.ImageEffects;
using Nokia.Graphics.Imaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Phone.Media.Capture;
using Windows.Storage.Streams;
namespace PerfectCamera
{
    public class EffectIndex
    {
        public int EffectIdx { get; set; }
        public int FilterIdx { get; set; }

        public EffectIndex()
        {
            EffectIdx = 0;
            FilterIdx = 0;
        }

        public EffectIndex(int eIdx, int fIdx)
        {
            EffectIdx = eIdx;
            FilterIdx = fIdx;
        }
    }

    public class Effects : ICameraEffect
    {
        private PhotoCaptureDevice PhotoCaptureDevice = null;
        private CameraPreviewImageSource m_StreamImageSource = null;
        private FilterEffect m_FilterEffect = null;
        private CustomEffectBase m_CustomEffect = null;
        private int m_EffectIndex = 1;
        private Semaphore m_Semaphore = new Semaphore(1, 1);
        private Size m_FrameSize;

        public String EffectName { get; private set; }

        public PhotoCaptureDevice m_PhotoCaptureDevice
        {
            set
            {
                if (PhotoCaptureDevice != value)
                {
                    while (!m_Semaphore.WaitOne(100)) ;

                    PhotoCaptureDevice = value;

                    Initialize();

                    m_Semaphore.Release();
                }
            }
        }

        public string DebugTag { get; private set; }

        ~Effects()
        {
            while (!m_Semaphore.WaitOne(100)) ;

            Uninitialize();

            m_Semaphore.Release();
        }

        public async Task GetNewFrameAndApplyEffect(IBuffer frameBuffer, Size frameSize)
        {
            if (m_Semaphore.WaitOne(500))
            {
                m_FrameSize = frameSize;

                var scanlineByteSize = (uint)frameSize.Width * 4; // 4 bytes per pixel in BGRA888 mode
                var bitmap = new Bitmap(frameSize, ColorMode.Bgra8888, scanlineByteSize, frameBuffer);

                if (m_FilterEffect != null)
                {
                    var renderer = new BitmapRenderer(m_FilterEffect, bitmap);
                    await renderer.RenderAsync();
                }
                else if (m_CustomEffect != null)
                {
                    var renderer = new BitmapRenderer(m_CustomEffect, bitmap);
                    await renderer.RenderAsync();
                }
                else
                {
                    var renderer = new BitmapRenderer(m_StreamImageSource, bitmap);
                    await renderer.RenderAsync();
                }

                m_Semaphore.Release();
            }
        }

        public void NextEffect()
        {
            if (m_Semaphore.WaitOne(500))
            {
                Uninitialize();

                m_EffectIndex++;

                if (m_EffectIndex >= m_EffectCount)
                {
                    m_EffectIndex = 0;
                }

                Initialize();

                m_Semaphore.Release();
            }
        }

        public void PreviousEffect()
        {
            if (m_Semaphore.WaitOne(500))
            {
                Uninitialize();

                m_EffectIndex--;

                if (m_EffectIndex < 0)
                {
                    m_EffectIndex = m_EffectCount - 1;
                }

                Initialize();

                m_Semaphore.Release();
            }
        }

        private void Uninitialize()
        {
            if (m_StreamImageSource != null)
            {
                m_StreamImageSource.Dispose();
                m_StreamImageSource = null;
            }

            if (m_FilterEffect != null)
            {
                m_FilterEffect.Dispose();
                m_FilterEffect = null;
            }

            if (m_CustomEffect != null)
            {
                m_CustomEffect.Dispose();
                m_CustomEffect = null;
            }
        }

        private void Initialize()
        {
           
            var filters = new List<IFilter>();
            var nameFormat = "{0}/" + m_EffectCount + " - {1}";

            App.AssignedColorCache = new Dictionary<uint, uint>(); // Reset
            m_StreamImageSource = new CameraPreviewImageSource(PhotoCaptureDevice);

            switch (m_EffectIndex)
            {

                case 0:
                    {
                        EffectName =  "Cell size : 10";
                        PerfectCamera.DataContext.Instance.Pixels = PixelSize.ten;
                        m_CustomEffect = new PixelationEffect(m_StreamImageSource,10);
                    }
                    break;

                case 1:
                    {
                        EffectName = "Cell size : 15";
                        PerfectCamera.DataContext.Instance.Pixels = PixelSize.fifteen;
                        m_CustomEffect = new PixelationEffect(m_StreamImageSource,15);
                    }
                    break;

                case 2:
                    {
                        EffectName = "Cell size : 35";
                        PerfectCamera.DataContext.Instance.Pixels = PixelSize.thirty;
                        m_CustomEffect = new PixelationEffect(m_StreamImageSource,30);
                    }
                    break;

               
            }

           
        }

        private int m_EffectCount = 3;  // Remember to increment by one with each case added above.

        public async Task<Stream> ApplyEffect(MemoryStream inputStream)
        {

            IBuffer outputBuffer = null;

            if (m_Semaphore.WaitOne(500))
            {

                outputBuffer = await RenderJpegAsync(inputStream.GetWindowsRuntimeBuffer());
                m_Semaphore.Release();
            }

            if (outputBuffer != null)
            {
                return outputBuffer.AsStream();
            }

            return null;
        }
        public virtual async Task<IBuffer> RenderJpegAsync(IBuffer buffer)
        {
            if (buffer == null || buffer.Length == 0)
            {
                Debug.WriteLine(DebugTag + ": RenderJpegAsync(): The given buffer is null or empty!");
                return null;
            }


            IBuffer outputBuffer;

            using (var source = new BufferImageSource(buffer))
            {
                var effect = new FilterEffect(source);


                using (var renderer = new JpegRenderer(effect))
                {
                    outputBuffer = await renderer.RenderAsync();
                }

                effect.Dispose();
            }

            return outputBuffer;
        }

    }
}