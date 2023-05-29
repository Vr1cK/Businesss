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

namespace Business
{
    internal static class ButtonsClass  
    {
        public static void SetWorker(DataGrid WorkerTable)
        {
            HideWorkerTable(WorkerTable);

        }
        public static void ShowOrHideWorkerTable(DataGrid WorkerTable)
        {
            if(WorkerTable.Visibility == Visibility.Hidden)
                WorkerTable.Visibility = Visibility.Visible;
            else
                WorkerTable.Visibility = Visibility.Hidden;
        }
        public static void HideWorkerTable(DataGrid WorkerTable)
        {
            if (WorkerTable.Visibility == Visibility.Visible)
                WorkerTable.Visibility = Visibility.Hidden;
        }
        public static void ShowWorkerTable(DataGrid WorkerTable)
        {
            if (WorkerTable.Visibility != Visibility.Visible)
                WorkerTable.Visibility = Visibility.Visible;
        }
    }
}
