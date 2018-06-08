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
namespace derp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int updateTimer;
        private piGetter pigetter;
        private rtuSender rtusender;
        private String mehwhatever;
        private DateTime EndTime;
        private String StartDateTime;
        private String EndDateTime;


        public MainWindow()
        {

            InitializeComponent();

            //Initialize the classes
            this.pigetter = new piGetter();
            this.rtusender = new rtuSender(getUpdateTime());

            //initialize parameters here
            DatePicker startDatePicker = new DatePicker();
            DatePicker endDatePicker = new DatePicker();
            TimePicker startTimePicker = new TimePicker();
            TimePicker endTimePicker = new TimePicker();

            //Threading setup. The threads are use for concurrency...and I don't really want these data gattering and sending operaitons on the main thread 
            //var pitask = Task.Run(() => this.pigetter);
            //var rtuTask = Task.Run(() => this.rtusender);
        }

        private void enable(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("program enabled");
            Console.WriteLine("Start Time: "+this.StartDateTime);
            Console.WriteLine("End Time: "+this.EndDateTime);
            this.pigetter.isActive(true);
        }
        private void disable(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("program disabled");
            this.pigetter.isActive(false);
        }

       private void getTimeStamp()
        {

        }

        private void updateTextbox(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key==Key.Escape || e.Key == Key.Tab)
            {
                rtusender.setUpdateTime(getUpdateTime());
            }

        }

        private void updateTextbox(object sender, RoutedEventArgs e)
        {
            rtusender.setUpdateTime(getUpdateTime());
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
        private void startDateListener(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            pigetter.setStartDateTime(getDateTime(e.NewValue));
        }
        private void endDateListener(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            pigetter.setEndDateTime(getDateTime(e.NewValue));
        }



    }
}
