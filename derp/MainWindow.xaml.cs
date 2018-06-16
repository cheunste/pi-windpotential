﻿using System;
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
using MaterialDesignThemes.Wpf;
namespace derp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private piGetter pigetter;
        private rtuSender rtusender;
        private InterruptManager im;
        private String startDateTime;
        private String endDateTime;
        private TimeSpan interval;


        public MainWindow()
        {

            InitializeComponent();

            //Initialize the classes
            this.pigetter = new piGetter();
            this.rtusender = new rtuSender();
            this.im = new InterruptManager();

            //Replace this with the value retrieved from the Sampling Time textbox
            this.interval = new TimeSpan(0, 5, 0);
            //What is the current start time
            //initialize parameters here

            DateTime defaultStart = DateTime.Now.AddHours(-1.00) ;
            DateTime defaultEnd = DateTime.Now;

            this.startDateTime = defaultStart.ToString();
            this.endDateTime = defaultEnd.ToString();

            StartTimePicker.DefaultValue = defaultStart;
            EndTimePicker.DefaultValue = defaultEnd;



            pigetter.setStartDateTime(defaultStart.ToString());
            pigetter.setEndDateTime(defaultEnd.ToString());

            //Threading setup. The threads are use for concurrency...and I don't really want these data gattering and sending operaitons on the main thread 
            //var pitask = Task.Run(() => this.pigetter);
            //var rtuTask = Task.Run(() => this.rtusender);
        }

        private void enableState(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("program enabled");
            Console.WriteLine("Start Time: "+this.startDateTime);
            Console.WriteLine("End Time: "+this.endDateTime);

            this.rtusender.setState(true);
            enable();

        }
        private void disableState(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("program disabled");
            this.rtusender.setState(false);
            this.rtusender.cancelRTUCalls();
            disable();

        }

        private void enable()
        {

            //Check if startDateTime or EndDateTime is empty or not. If it is empty, then throw an error to the user
            if (this.startDateTime.Equals("") || this.endDateTime == null )
            { 

            }
            else if (this.endDateTime.Equals("") || this.endDateTime == null)
            {

            }
            else if (enableButton.IsChecked == true)
            {
                this.im.setprogramEnabled(true);
                //Set the wait interval
                TimeSpan samplingTime = new TimeSpan(0,getSamplingTime(),0);
                //This is update time
                TimeSpan updateTime = new TimeSpan(0, getUpdateTime(), 0);

                this.interval = new TimeSpan(0, getSamplingTime(), 0);
                //Set the sampling interval for PI
                this.pigetter.setSamplingInterval(this.interval);
                this.pigetter.isActive(true);

                this.rtusender.setUpdateInterval(updateTime);
                this.rtusender.setList(pigetter.getList());
                this.rtusender.sendToRTU();           
            }
            else
            {
                //Doesn't really do anything

            }

        }

        private void disable()
        {
            this.im.setprogramEnabled(false);
            this.pigetter.isActive(false);
            this.rtusender.deleteAllLists();
        }


        //Method to restart everything, meaning refetch the data from PI, rebuild the List, etc
        private void restart()
        {
            //First disable everything...but without triggerting the switch
            disable();
            enable();

        }

        private void updateTimeTextBox(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key==Key.Escape || e.Key == Key.Tab)
            {
                rtusender.setUpdateTime(getUpdateTime());
            }

        }

        private void updateTimeTextBox(object sender, RoutedEventArgs e)
        {
            rtusender.setUpdateTime(getUpdateTime());
        }

        private void samplingTimeTextBox(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key==Key.Escape || e.Key == Key.Tab)
            {
                this.interval = new TimeSpan(0, getSamplingTime(), 0);
            }
        }

        private void samplingTimeTextBox(object sender, RoutedEventArgs e)
        {
                this.interval = new TimeSpan(0,getSamplingTime(),0);
        }

        private int getSamplingTime()
        {
            TextBox t= (TextBox)samplingTextbox;
            String samplingTextboxValue = t.Text;
            Console.WriteLine("On Change detected: " +samplingTextboxValue);
            return int.Parse(samplingTextboxValue);
           
        }
        private int getUpdateTime()
        {
            TextBox t= (TextBox)rtuUpdateTextbox;
            String updateTextboxValue = t.Text;
            Console.WriteLine("On Change detected: " +updateTextboxValue);
            return (int.Parse(updateTextboxValue));
        }

        private String getDateTime(object sender)
        {
            return sender.ToString();

        }
        
        //Event method 
        private void startDateListener(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            pigetter.setStartDateTime(getDateTime(e.NewValue));
            pigetter.restart();
            restart();
        }
        private void endDateListener(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            pigetter.setEndDateTime(getDateTime(e.NewValue));
            pigetter.restart();
            restart();
        }



        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


    }
}
