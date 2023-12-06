using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Now_You_See_Me
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            System.Windows.Forms.Timer t = null;
            t = new System.Windows.Forms.Timer();
            t.Interval = 1000;
            t.Tick += new EventHandler(t_Tick);
            t.Enabled = true;
        }

        

        void t_Tick(object sender, EventArgs e)
        {
            currT.Text = DateTime.Now.ToString();
        }
       
        private string GetFormattedTime()
        {
            DateTime now = DateTime.Now;
            string formattedTime = now.ToString("HHmm");
            return formattedTime;
        }
        private bool VerifyPassword()
        {
            string currentTime = GetFormattedTime();
            PasswordInputDialog passwordDialog = new PasswordInputDialog();
            if (passwordDialog.ShowDialog() == true)
            {
                return passwordDialog.Password == currentTime;
            }

            return false;
        }
        private void SetHiddenAttributeRecursively(string path, bool hide)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    foreach (var file in Directory.GetFiles(path))
                    {
                        File.SetAttributes(file, hide
                            ? File.GetAttributes(file) | FileAttributes.Hidden
                            : File.GetAttributes(file) & ~FileAttributes.Hidden);
                    }

                    foreach (var subDir in Directory.GetDirectories(path))
                    {
                        SetHiddenAttributeRecursively(subDir, hide);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ButtHide_Click(object sender, RoutedEventArgs e)
        {
            if (VerifyPassword())
            {
                using (var folderBrowserDialog = new FolderBrowserDialog())
                {
                    DialogResult result = folderBrowserDialog.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                    {
                        string folderPath = folderBrowserDialog.SelectedPath;

                        try
                        {
                            SetHiddenAttributeRecursively(folderPath, true);
                            System.Windows.Forms.MessageBox.Show("The files are hidden and secure !!");
                        }
                        catch (Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show("Error hiding the files: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Access denied. Invalid password.");
            }
        }







        private void ButtShowFile_Click(object sender, RoutedEventArgs e)
        {
            {
                if (VerifyPassword())
                {
                    using (var folderBrowserDialog = new FolderBrowserDialog())
                    {
                        DialogResult result = folderBrowserDialog.ShowDialog();
                        if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                        {
                            string folderPath = folderBrowserDialog.SelectedPath;

                            try
                            {
                                SetHiddenAttributeRecursively(folderPath, false);
                                System.Windows.Forms.MessageBox.Show("The files are visible be aware !!");
                            }
                            catch (Exception ex)
                            {
                                System.Windows.Forms.MessageBox.Show("Error showing the folder: " + ex.Message);
                            }
                        }
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Access denied. Invalid password.");
                }
            }
        }
        private void UnhideFolderAndFiles(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    // Set the hidden attribute to false for the directory itself
                    FileAttributes attributes = File.GetAttributes(path);
                    attributes &= ~FileAttributes.Hidden;
                    File.SetAttributes(path, attributes);

                    // Recursively unhide subdirectories and files
                    foreach (var subDir in Directory.GetDirectories(path))
                    {
                        UnhideFolderAndFiles(subDir);
                    }

                    foreach (var file in Directory.GetFiles(path))
                    {
                        attributes = File.GetAttributes(file);
                        attributes &= ~FileAttributes.Hidden;
                        File.SetAttributes(file, attributes);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ButtShowFolder_Click(object sender, RoutedEventArgs e)
        {

            if (VerifyPassword())
            {
                using (var folderBrowserDialog = new FolderBrowserDialog())
                {
                    DialogResult result = folderBrowserDialog.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                    {
                        string folderPath = folderBrowserDialog.SelectedPath;

                        try
                        {
                            UnhideFolderAndFiles(folderPath);
                            System.Windows.Forms.MessageBox.Show("The folder is visible be save !!");
                        }
                        catch (Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show("Error showing the folder: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Access denied. Invalid password.");
            }

        }
        private void SetHiddenFolderRecursively(string path, bool hide)
        {

            try
            {
                if (Directory.Exists(path))
                {
                    // Set the hidden attribute for the directory itself
                    FileAttributes attributes = File.GetAttributes(path);
                    if (hide)
                    {
                        attributes |= FileAttributes.Hidden;
                    }
                    else
                    {
                        attributes &= ~FileAttributes.Hidden;
                    }
                    File.SetAttributes(path, attributes);

                    // Recursively set the hidden attribute for subdirectories and files
                    foreach (var subDir in Directory.GetDirectories(path))
                    {
                        SetHiddenFolderRecursively(subDir, hide);
                    }

                    foreach (var file in Directory.GetFiles(path))
                    {
                        attributes = File.GetAttributes(file);
                        if (hide)
                        {
                            attributes |= FileAttributes.Hidden;
                        }
                        else
                        {
                            attributes &= ~FileAttributes.Hidden;
                        }
                        File.SetAttributes(file, attributes);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: " + ex.Message);
            }

        }
        private void ButtUnHide_Click(object sender, RoutedEventArgs e)
        {
            if (VerifyPassword())
            {
                using (var folderBrowserDialog = new FolderBrowserDialog())
                {
                    DialogResult result = folderBrowserDialog.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                    {
                        string folderPath = folderBrowserDialog.SelectedPath;

                        try
                        {
                            SetHiddenFolderRecursively(folderPath, true);
                            System.Windows.Forms.MessageBox.Show("The folder is hidden and save and secure  !!");
                        }
                        catch (Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show("Error hiding the folder: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Access denied. Invalid password.");
            }
        }



        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

}

