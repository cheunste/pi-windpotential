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
using MaterialDesignThemes.Wpf;
namespace piWindPotential
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private piGetter pigetter;
        private rtuSender rtusender;
        private InterruptManager im;
        private DateTime startDateTime;
        private DateTime endDateTime;
        private TimeSpan samplingInterval;
        private List<String> bigHornOutput;
        private List<String> jonesOutput;
        private List<String> juniperOutput;
        private List<String> klondikeOutput;
        private csvOuptut csvOutput;

        public MainWindow()
        {

            InitializeComponent();

            //Initialize the classes
            this.pigetter = new piGetter();
            this.rtusender = new rtuSender();
            this.im = new InterruptManager();

            //Replace this with the value retrieved from the Sampling Time textbox
            this.samplingInterval = new TimeSpan(0, 5, 0);
            //What is the current start time
            //initialize parameters here

            this.startDateTime = DateTime.Now.AddHours(-1.00) ;
            this.endDateTime = DateTime.Now;

            StartTimePicker.DefaultValue = startDateTime;
            EndTimePicker.DefaultValue = endDateTime;

            pigetter.setStartDateTime(this.startDateTime.ToString());
            pigetter.setEndDateTime(this.endDateTime.ToString());

            //reference to the csvOutuput
            this.csvOutput = new csvOuptut();

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
            this.csvOutput.closeFile();
            //close the csv file by killing the office process
            this.rtusender.setState(false);
            this.rtusender.cancelRTUCalls();
            disable();

        }

        private void enable()
        {

            //Check if startDateTime or EndDateTime is empty or not. If it is empty, then throw an error to the user...or just not run
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
                TimeSpan samplingTime = getSamplingTime();
                //This is update time
                TimeSpan updateTime = getUpdateTime();

                this.samplingInterval = getSamplingTime(); 
                //Set the sampling interval for PI
                this.pigetter.setSamplingInterval(this.samplingInterval);
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
            this.pigetter.clearList();
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
                this.samplingInterval = getSamplingTime();
            }
        }

        private void samplingTimeTextBox(object sender, RoutedEventArgs e)
        {
                this.samplingInterval = getSamplingTime();
        }

        private TimeSpan getSamplingTime()
        {
            TextBox t= (TextBox)samplingTextbox;
            String samplingTextboxValue = t.Text;
            Console.WriteLine("On Change detected: " +samplingTextboxValue);
            double samplingTime = checkNegative(double.Parse(samplingTextboxValue));
            TimeSpan samplingTimeSpan = TimeSpan.FromMinutes(samplingTime);

            return  samplingTimeSpan;

           
        }
        private TimeSpan getUpdateTime()
        {
            TextBox t= (TextBox)rtuUpdateTextbox;
            String updateTextboxValue = t.Text;
            Console.WriteLine("On Change detected: " +updateTextboxValue);
            double updateTime = checkNegative(double.Parse(updateTextboxValue));

            TimeSpan updateTimeSpan = TimeSpan.FromMinutes(updateTime);
            return updateTimeSpan;
        }

        //Method to check if the time is negative. Returns a 1 if it is and the value itself if not
        private double checkNegative(double time){
            return (time < 0.00)? 1.00 : time;
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
