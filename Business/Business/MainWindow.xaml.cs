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
using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace Business
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetNameWorker.Visibility = Visibility.Collapsed;
            SetPhoneWorker.Visibility = Visibility.Collapsed;
            SetEmailWorker.Visibility = Visibility.Collapsed;
            SetPostWorker.Visibility = Visibility.Collapsed;
            
            WorkerTable.Visibility = Visibility.Collapsed;
            ScrollViewerWorkerTable.Visibility = Visibility.Collapsed;

            EndSetWorkerButton.Visibility = Visibility.Collapsed;
            StartSetWorkerButton.Visibility = Visibility.Visible;

            StartDeleteWorkerButton.Visibility = Visibility.Visible;
            EndDeleteWorkerButton.Visibility = Visibility.Collapsed;
            SetPhoneWorkerForDelete.Visibility = Visibility.Collapsed;

            SearchInTableButton.Visibility = Visibility.Collapsed;
            SearchInTableTextBox.Visibility = Visibility.Collapsed;

            CancelDeleteWorkerButton.Visibility = Visibility.Collapsed;

            CancelSetWorkerButton.Visibility = Visibility.Collapsed;
        }

        private void ShowWorkingTableButton_Click(object sender, RoutedEventArgs e)
        {
            if (ScrollViewerWorkerTable.Visibility == Visibility.Collapsed ||
            WorkerTable.Visibility == Visibility.Collapsed ||
            SearchInTableButton.Visibility == Visibility.Collapsed ||
            SearchInTableTextBox.Visibility == Visibility.Collapsed)
            {
                ScrollViewerWorkerTable.Visibility = Visibility.Visible;
                WorkerTable.Visibility = Visibility.Visible;
                SearchInTableTextBox.Visibility = Visibility.Visible;
                SearchInTableButton.Visibility = Visibility.Visible;
            }
            else
            {
                ScrollViewerWorkerTable.Visibility = Visibility.Collapsed;
                WorkerTable.Visibility = Visibility.Collapsed;
                SearchInTableTextBox.Visibility = Visibility.Collapsed;
                SearchInTableButton.Visibility = Visibility.Collapsed;
            }
            List<WorkerList> workerList = new List<WorkerList>();

            using(BusinessContext db = new BusinessContext())
            {
                var workers = db.Workers.Include(u => u.Post);
                foreach (Worker worker in workers)
                {   WorkerList copyWorker = new WorkerList();
                    copyWorker.Id = worker.Id;
                    copyWorker.Age = worker.Age;
                    copyWorker.FullName = worker.FullName;
                    copyWorker.PostName = worker?.Post?.PostName;
                    copyWorker.Phone = worker?.Phone;
                    workerList.Add(copyWorker);
                }
            }
            WorkerTable.ItemsSource = workerList;
        }

        private void StartSetWorkerButton_Click(object sender, RoutedEventArgs e)
        {

            SetNameWorker.Visibility = Visibility.Visible;
            SetPhoneWorker.Visibility = Visibility.Visible;
            SetEmailWorker.Visibility = Visibility.Visible;
            SetPostWorker.Visibility = Visibility.Visible;
            
            StartSetWorkerButton.Visibility = Visibility.Collapsed;
            EndSetWorkerButton.Visibility = Visibility.Visible;

            CancelSetWorkerButton.Visibility = Visibility.Visible;
        }

        private void EndSetWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            if (SetNameWorker.Text == "" ||
            SetPhoneWorker.Text == "" ||
            SetPostWorker.Text == "" || 
            SetNameWorker.Text.Contains("Введите") ||
            SetPhoneWorker.Text.Contains("Введите") ||
            SetPostWorker.Text.Contains("Введите") ||
            SetEmailWorker.Text.Contains("Введите"))
            {
                return;
            }
            else
            {
                Worker worker = new Worker();
                worker.FullName = SetNameWorker.Text;
                worker.Email = SetEmailWorker.Text;
                worker.Phone = SetPhoneWorker.Text;

                string workerPost = SetPostWorker.Text;

                using (BusinessContext db = new BusinessContext())
                {
                    var posts = db.Posts;
                    foreach (Post post in posts)
                    {
                        if (post.PostName.ToLower() == workerPost.ToLower()) {
                            worker.Post = post;
                            break;
                        }
                    }
                    if (worker.Post == null)
                    {
                        MessageBox.Show("Такой должности не существует. Пожалуйста, напишите полное название должности без ошибок.");
                        return;
                    }
                    else
                    {
                        db.Workers.Add(worker);
                        db.SaveChanges();
                    }
                }
                {
                    SetNameWorker.Visibility = Visibility.Collapsed;
                    SetPhoneWorker.Visibility = Visibility.Collapsed;
                    SetEmailWorker.Visibility = Visibility.Collapsed;
                    SetPostWorker.Visibility = Visibility.Collapsed;

                    StartSetWorkerButton.Visibility = Visibility.Visible;
                    EndSetWorkerButton.Visibility = Visibility.Collapsed;

                    SetNameWorker.Text = "Введите полное имя работника";
                    SetPostWorker.Text = "Введите должность работника";
                    SetPhoneWorker.Text = "Введите телефон работника";
                    SetEmailWorker.Text = "Введите почту работника";

                    ShowWorkingTableButton.Visibility = Visibility.Visible;
                    StartDeleteWorkerButton.Visibility = Visibility.Visible;
                }
            }
        }

        private void EndDeleteWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            if (SetPhoneWorkerForDelete.Text == "" ||
            SetPhoneWorkerForDelete.Text.Contains("Введите"))
            {
                return;
            }
            else
            {
                using(BusinessContext db = new BusinessContext())
                {
                    var workers = db.Workers;
                    foreach(var worker in workers)
                    {
                        if (worker.Phone == SetPhoneWorkerForDelete.Text)
                        {
                            db.Workers.Remove(worker);
                            break;
                        }
                    }
                    db.SaveChanges();
                }
                
            }
            StartDeleteWorkerButton.Visibility = Visibility.Visible;
            EndDeleteWorkerButton.Visibility = Visibility.Collapsed;
            SetPhoneWorkerForDelete.Visibility = Visibility.Collapsed;
            SetPhoneWorkerForDelete.Text = "Введите телефон работника для удаления по шаблону(8 - ***-***-**-**) без тире.";
        }

        private void StartDeleteWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            StartDeleteWorkerButton.Visibility = Visibility.Collapsed;
            EndDeleteWorkerButton.Visibility = Visibility.Visible;

            SetPhoneWorkerForDelete.Visibility = Visibility.Visible;
            CancelDeleteWorkerButton.Visibility = Visibility.Visible;
        }


        private void SetWorker_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (textBox.Text.Contains("Введите"))
                textBox.Text = null;
        }

        private void CancelDeleteWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            StartDeleteWorkerButton.Visibility = Visibility.Visible;
            EndDeleteWorkerButton.Visibility = Visibility.Collapsed;
            SetPhoneWorkerForDelete.Visibility = Visibility.Collapsed;
            SetPhoneWorkerForDelete.Text = "Введите телефон работника для удаления по шаблону(8 - ***-***-**-**) без тире.";
            CancelDeleteWorkerButton.Visibility = Visibility.Collapsed;
        }

        private void CancelSetWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            StartSetWorkerButton.Visibility = Visibility.Visible;
            EndSetWorkerButton.Visibility = Visibility.Collapsed;

            SetEmailWorker.Visibility = Visibility.Collapsed;
            SetPhoneWorker.Visibility = Visibility.Collapsed;
            SetNameWorker.Visibility = Visibility.Collapsed;
            SetPostWorker.Visibility = Visibility.Collapsed;

            CancelSetWorkerButton.Visibility = Visibility.Collapsed;
        }
    }
}
