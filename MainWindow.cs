/*
 * Author: Satyam Khanna
 * Email: satyamkhanna66@gmail.com
 */


//Include Section Starts
using System;
using System.Text;
using System.Windows;
using MySqlConnector;
using PasswordHasher;
//Include Section Ends


namespace ExpenseIt
{
    public partial class MainWindow : Window
    {
        //Class Field Section Starts
        private string Username = "";
        private string Password = "";
        StringBuilder query = new StringBuilder();
        //Class Field Section Ends


        //Class Constructor Section Starts
        public MainWindow()
        {
            InitializeComponent();
        }
        //Class Constructor Section Ends


        //Class Member Section Starts
        private void AuthUserButton(object sender, RoutedEventArgs e)
        {
            HashPassword hashpassword = new HashPassword();

            //Fetch Input Data
            Username = usernameBox.Text;
            Password = hashpassword.Crypto(passwordBox.Password);

            //Clear Input Boxes
            usernameBox.Clear();
            passwordBox.Clear();

            //Disable Start Button
            submitButton.IsEnabled = false;

            //Instantiate Mysql Class With Connection Settings
            MySqlLibrary mySqlCommands = new MySqlLibrary("localhost", "root", "", "food-order");

            //Execute Commands
            query.Append("SELECT * FROM tbl_admin WHERE username='purujain099'");
            string recievedData = mySqlCommands.ExecuteSqlQuery(query);
            if (recievedData != null)
            {
                usernameBox.Text = recievedData;
            }
        }
    }
    //Class Member Section Ends
}


