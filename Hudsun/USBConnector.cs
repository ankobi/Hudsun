using System;
using System.Threading;
using HIDLibrary;

namespace Hudsun
{
    public class UsbConnector
    {
        public event EventHandler OnDisconnected;
        public event EventHandler OnConnected;
        private HidDevice hidDevice;
        private bool connected;
        private Boolean dreamCheeky = true;
        private bool abort;
        private readonly Semaphore waitSemaphore;
        public bool pulsing;
        

        public UsbConnector()
        {
            waitSemaphore = new Semaphore(0, 1);
            Connect(false);
            SetRGB(0x00, 0x00, 0x00);

        }

        private void Connect(bool fireEvent)
        {
            HidDevice[] hidDeviceList = HidDevices.Enumerate(0x1D34, 0x0004);
            if (hidDeviceList.Length == 0)
            {
                hidDeviceList = HidDevices.Enumerate(0x1294, 0x1320);
                if (hidDeviceList.Length > 0)
                {
                    dreamCheeky = false;
                }
            }

            if (hidDeviceList.Length > 0)
            {
                hidDevice = hidDeviceList[0];
                hidDevice.Open();
                while (!hidDevice.IsConnected || !hidDevice.IsOpen) { }
                if (dreamCheeky)
                {
                    hidDevice.Write(new byte[] { 0x00, 0x1F, 0x01, 0x29, 0x00, 0xB8, 0x54, 0x2C, 0x03 });
                    hidDevice.Write(new byte[] { 0x00, 0x00, 0x01, 0x29, 0x00, 0xB8, 0x54, 0x2C, 0x04 });
                }

                connected = true;

                if (fireEvent)
                {
                    OnConnected.Invoke(this, null);
                }
            }

            
        }

        public void Abort()
        {
            if (pulsing)
            {
                abort = true;
                waitSemaphore.Release();
            }
        }

        public bool CheckConnectionStatus()
        {
            if (hidDevice != null)
            {
                if (!hidDevice.IsOpen)
                {
                    if (connected)
                    {
                        hidDevice.Close();
                        connected = false;
                        OnDisconnected.Invoke(this, null);
                    }
                    Connect(true);
                }
            }

            return connected;
        }

        public void SetRGB(RgbValue color)
        {
            SetRGB((byte)color.R, (byte)color.G, (byte)color.B);
        }

        public void SetRGB(byte r, byte g, byte b)
        {
            if (hidDevice != null)
            {
                if (!hidDevice.IsOpen)
                {
                    if (connected)
                    {
                        hidDevice.Close();
                        connected = false;
                        OnDisconnected.Invoke(this, null);
                    }
                    Connect(true);
                }
                if (dreamCheeky)
                {
                    hidDevice.Write(new byte[] { 0x00, r, g, b, 0x00, 0x00, 0x54, 0x2C, 0x05 });
                }
                else
                {
                    if (r == 0x00 && g == 0x00 && b == 0x00)
                    {
                        hidDevice.Write(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
                    }
                    if (r > 0x00 && g == 0x00 && b == 0x00)
                    {
                        hidDevice.Write(new byte[] { 0x00, 0x02, 0x00, 0x00, 0x00, 0x00 });
                    }
                    if (r == 0x00 && g > 0x00 && b == 0x00)
                    {
                        hidDevice.Write(new byte[] { 0x00, 0x01, 0x00, 0x00, 0x00, 0x00 });
                    }
                    if (r == 0x00 && g == 0x00 && b > 0x00)
                    {
                        hidDevice.Write(new byte[] { 0x00, 0x03, 0x00, 0x00, 0x00, 0x00 });
                    }
                    if (r > 0x00 && g > 0x00 && b == 0x00)
                    {
                        hidDevice.Write(new byte[] { 0x00, 0x05, 0x00, 0x00, 0x00, 0x00 });
                    }
                    if (r > 0x00 && g == 0x00 && b > 0x00)
                    {
                        hidDevice.Write(new byte[] { 0x00, 0x06, 0x00, 0x00, 0x00, 0x00 });
                    }
                    if (r == 0x00 && g > 0x00 && b > 0x00)
                    {
                        hidDevice.Write(new byte[] { 0x00, 0x04, 0x00, 0x00, 0x00, 0x00 });
                    }
                    if (r > 0x00 && g > 0x00 && b > 0x00)
                    {
                        hidDevice.Write(new byte[] { 0x00, 0x07, 0x00, 0x00, 0x00, 0x00 });
                    }
                }
            }            
        }

        public void PulseRgb(RgbValue color)
        {
            PulseRgb((byte)color.R, (byte)color.G, (byte)color.B);
        }

        public void PulseRgb(byte r, byte g, byte b)
        {
            if (dreamCheeky)
            {
                abort = false;
                pulsing = true;

                int greatest = r;
                float internalR = 0;
                float internalG = 0;
                float internalB = 0;

                if (g > greatest)
                {
                    greatest = g;
                }

                if (b > greatest)
                {
                    greatest = b;
                }

                for (int i = 10; i < greatest; i++)
                {
                    if (abort)
                    {
                        break;
                    }

                    SetRGB((byte)Math.Round(internalR), (byte)Math.Round(internalG), (byte)Math.Round(internalB));

                    if (internalR < r)
                    {
                        internalR += (r / (float)greatest);
                    }

                    if (internalG < g)
                    {
                        internalG += (g / (float)greatest);
                    }

                    if (internalB < b)
                    {
                        internalB += (b / (float)greatest);
                    }

                    waitSemaphore.WaitOne(12);
                }

                Thread.Sleep(100);

                for (int i = 64; i > 11; i--)
                {
                    if (abort)
                    {
                        break;
                    }

                    if (internalR > 0)
                    {
                        internalR -= (r / (float)greatest);
                    }

                    if (internalG > 0)
                    {
                        internalG -= (g / (float)greatest);
                    }

                    if (internalB > 0)
                    {
                        internalB -= (b / (float)greatest);
                    }

                    SetRGB((byte)Math.Round(internalR), (byte)Math.Round(internalG), (byte)Math.Round(internalB));
                    waitSemaphore.WaitOne(20);
                }

                if (!abort)
                {
                    //waitSemaphore.WaitOne(200);
                }

                pulsing = false;
            }
        }
    }

}
