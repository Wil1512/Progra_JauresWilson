using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace Progra_JauresWilson
{
    /// <summary>
    /// Logique d'interaction pour TodoWindow.xaml
    /// </summary>
    public partial class TodoWindow : Window
    {
        private ObservableCollection<TaskItem> tasks = new ObservableCollection<TaskItem>();

        private readonly string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "todolist.json");
        public TodoWindow()
        {
            InitializeComponent();
            LoadTasks();
            lstTasks.ItemsSource = tasks;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNewTask.Text))
            {
                tasks.Add(new TaskItem { Title = txtNewTask.Text.Trim(), IsDone = false });
                txtNewTask.Clear();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstTasks.SelectedItem is TaskItem selectedTask)
            {
                tasks.Remove(selectedTask);
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une tâche à supprimer.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
                MessageBox.Show("To-Do List enregistrée avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la sauvegarde : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadTasks()
        {
            if (File.Exists(filePath))
            {
                try
                {
                    string json = File.ReadAllText(filePath);
                    var loadedTasks = JsonSerializer.Deserialize<ObservableCollection<TaskItem>>(json);
                    if (loadedTasks != null)
                    {
                        tasks = loadedTasks;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors du chargement : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
