using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JackyFinalProject
{
    public class Helper
    {
        public static void ShowMessage1()
        {
            MessageBox.Show("Saved Successfully!");
        }

        public static void ShowMessage2()
        {
            MessageBox.Show("Please select an item first!");
        }

        public static void ShowMessage3()
        {
            MessageBox.Show("Please select an employee first!");
        }

        public static void ShowMessage4()
        {
            MessageBox.Show("Please select an order first!");
        }

        public static void ShowMessage5()
        {
            MessageBox.Show(@"Action Failed! Essential information is missing. 
                Please check all required information, for instance, 
                Order ID, Customer ID, Address ID, Invoice Information and so forth
                are all completed and try again.");
        }

        public static void ShowMessage6()
        {
            MessageBox.Show("The selected order was already completed previously. Please choose an incompleted order and try again.");
        }

        public static void ShowMessage7()
        {
            MessageBox.Show("Success! The selected order is completed!");
        }

        public static void ShowMessage8()
        {
            MessageBox.Show("Invalid characters detected, please verify all fields and try again.");
        }
    }
}
