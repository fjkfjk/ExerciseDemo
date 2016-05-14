using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Drawing;

namespace WpfCSharpDirectX
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Form dxForm;
        Device dxDevice;
        Surface dxSurface;

        TimeSpan _lastRender;
        Int32Rect mImageRect;

        public MainWindow()
        {
            InitializeComponent();

            mImageRect.X = mImageRect.Y = 0;
            mImageRect.Width = 500;
            mImageRect.Height = 500;

            InitializeDirect3D();

            CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
        }
        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            RenderingEventArgs args = (RenderingEventArgs)e;

            // It's possible for Rendering to call back twice in the same frame 
            // so only render when we haven't already rendered in this frame.
            if (_lastRender == args.RenderingTime)
                return;

            _lastRender = args.RenderingTime;

            if (d3dimg.IsFrontBufferAvailable)
            {
                d3dimg.Lock();

                // Repeatedly calling SetBackBuffer with the same IntPtr is 
                // a no-op. There is no performance penalty.
                unsafe { d3dimg.SetBackBuffer(D3DResourceType.IDirect3DSurface9, (IntPtr)dxSurface.UnmanagedComPointer); };

                Render();

                d3dimg.AddDirtyRect(mImageRect);

                d3dimg.Unlock();
            }
        }
        public bool InitializeDirect3D()
        {
            try
            {
                PresentParameters presentParams = new PresentParameters();
                presentParams.Windowed = true; //指定以Windows窗体形式显示
                presentParams.SwapEffect = SwapEffect.Discard; //当前屏幕绘制后它将自动从内存中删除
                presentParams.BackBufferFormat = Format.Unknown;
                presentParams.BackBufferHeight = mImageRect.Height;
                presentParams.BackBufferWidth = mImageRect.Width;
                presentParams.BackBufferCount = 1;
                presentParams.PresentFlag = PresentFlag.LockableBackBuffer;

                dxForm = new Form();   //仅供创建device用，不会在其表面绘图
                dxDevice = new Device(0, DeviceType.Hardware, dxForm,
                    CreateFlags.HardwareVertexProcessing | CreateFlags.MultiThreaded | CreateFlags.FpuPreserve, presentParams); //实例化device对象
                
                dxSurface = dxDevice.GetBackBuffer(0, 0, BackBufferType.Mono);

                return true;
            }
            catch (DirectXException )
            {
                return false;
            }
        }
        public void Render()
        {
            if (dxDevice == null) //如果device为空则不渲染
            {
                return;
            }

            dxDevice.Clear(ClearFlags.Target, ColorTranslator.ToWin32(System.Drawing.Color.Red), 1.0f, 0);
            dxDevice.BeginScene();

            //在此添加渲染图形代码
            byte[] data = new byte[mImageRect.Width * mImageRect.Height * 4];

            for (int i = 0; i < data.Length/4/2; i++)
            {
                byte[] b = BitConverter.GetBytes(ColorTranslator.ToWin32(System.Drawing.Color.Green));
                Array.Copy(b, 0, data, i * 4, 4);
            }

            GraphicsStream gs = dxSurface.LockRectangle(LockFlags.Discard);
            gs.Write(data, 0, data.Length);
            dxSurface.UnlockRectangle();

            dxDevice.EndScene();
        }
    }
}
