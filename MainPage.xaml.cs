// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace Blinky
{
    public sealed partial class MainPage : Page
    {
        private const int RED_PIN = 5;
        private const int YELLOW_PIN = 6;
        private const int GREEN_PIN = 13;
        private GpioPin pinRed;
        private GpioPin pinYellow;
        private GpioPin pinGreen;
        private GpioPinValue pinValueRed;
        private GpioPinValue pinValueYellow;
        private GpioPinValue pinValueGreen;


        public MainPage()
        {
            InitializeComponent();

            InitGPIO();
            if (pinRed != null)
            {
                StartTrafficLight();
            }
        }

        private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();

            // Show an error if there is no GPIO controller
            if (gpio == null)
            {
                pinRed = null;
                pinYellow = null;
                pinGreen = null;
                GpioStatus.Text = "There is no GPIO controller on this device.";
                return;
            }

            pinRed = gpio.OpenPin(RED_PIN);
            pinValueRed = GpioPinValue.High;
            pinRed.Write(pinValueRed);
            pinRed.SetDriveMode(GpioPinDriveMode.Output);

            pinYellow = gpio.OpenPin(YELLOW_PIN);
            pinValueYellow = GpioPinValue.High;
            pinYellow.Write(pinValueYellow);
            pinYellow.SetDriveMode(GpioPinDriveMode.Output);

            pinGreen = gpio.OpenPin(GREEN_PIN);
            pinValueGreen = GpioPinValue.High;
            pinGreen.Write(pinValueGreen);
            pinGreen.SetDriveMode(GpioPinDriveMode.Output);
            GpioStatus.Text = "GPIO pin initialized correctly.";

        }





        private void StartTrafficLight()
        {

            pinValueGreen = GpioPinValue.Low;
            pinValueYellow = GpioPinValue.High;
            pinValueRed = GpioPinValue.High;
            pinGreen.Write(pinValueGreen);
            Task.Delay(10000).Wait();
            pinValueGreen = GpioPinValue.Low;


            pinValueGreen = GpioPinValue.High;
            pinValueYellow = GpioPinValue.Low;
            pinValueRed = GpioPinValue.High;
            pinYellow.Write(pinValueYellow);
            Task.Delay(3000).Wait();


            pinValueGreen = GpioPinValue.High;
            pinValueYellow = GpioPinValue.High;
            pinValueRed = GpioPinValue.Low;
            pinRed.Write(pinValueRed);
            Task.Delay(20000);

            StartTrafficLight();
        }


    }
}
