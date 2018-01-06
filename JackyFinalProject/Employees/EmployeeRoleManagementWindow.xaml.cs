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
using System.Windows.Shapes;

namespace JackyFinalProject.Employees
{
    /// <summary>
    /// Interaction logic for EmployeeRoleManagementWindow.xaml
    /// </summary>
    public partial class EmployeeRoleManagementWindow : Window
    {
        JackyDigitalXDBEntities jackyDigitalXDBEntities;

        public bool NewAdminNeeded { get; set; } = false;
        public EmployeeRoleManagementWindow()
        {
            jackyDigitalXDBEntities = new JackyDigitalXDBEntities();
            InitializeComponent();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (rightLB.SelectedItem != null)
            {
                Role role = rightLB.SelectedItem as Role;
                Verification verification = new Verification();

                if (role.Role1 == verification.Administrator)
                {
                    NewAdminNeeded = true;
                    MessageBox.Show("A new Administrator is required because the system must have ONE (and only one) Administrator at any time. Please choose a new Administrator in the new dialog box.");
                    this.Close();
                }
                else
                {
                    rightLB.ItemsSource = null;
                    EmployeesPage.Emp.Roles.Remove(role);
                    jackyDigitalXDBEntities.SaveChanges();
                    rightLB.ItemsSource = EmployeesPage.Emp.Roles.ToList();
                }
            }
            else
            {
                MessageBox.Show("Please choose a role first!");
                
            }
        }

        private void leftLB_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataObject dragItem = new DataObject(DataFormats.StringFormat, sender);
            DragDrop.DoDragDrop((ListBox)sender, dragItem, DragDropEffects.Move);
        }

        private void rightLB_PreviewDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                e.Effects = DragDropEffects.Move;
                e.Handled = true;
            }
            else
            {
                e.Effects = DragDropEffects.None;
                e.Handled = false;
            }
        }

        private void rightLB_PreviewDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                if (leftLB.SelectedIndex >= 0)
                {
                    Role role = leftLB.SelectedItem as Role;
                    Verification verification = new Verification();
                    var existingRoles = (from er in EmployeesPage.Emp.Roles
                                         where er.Role1 == role.Role1
                                         select er);
                    if (existingRoles.Count() != 0)
                    {
                        MessageBox.Show("This role is already assigned to this employee. Please choose another role and try again.");
                    }
                    else if (existingRoles.Count() == 0 && role.Role1 == verification.Administrator)
                    {
                        MessageBox.Show("A user with administrator role already exists. The system can have ONE (and only one) Administrator at any time.");
                    }
                    else
                    {
                        rightLB.ItemsSource = null;
                        EmployeesPage.Emp.Roles.Add(role);
                        jackyDigitalXDBEntities.SaveChanges();
                        rightLB.ItemsSource = EmployeesPage.Emp.Roles.ToList();
                    }
                }
            }
            else
            {
                e.Effects = DragDropEffects.None;
                e.Handled = false;
            }
        }
    }
}
