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

using System.ComponentModel;    // required for SortDescription, etc


namespace HW3_Sorting_ListView_Columns
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // ===========================================================================
        //
        // Homework 3: Sorting ListView Columns
        //
        // Take the ListView topic and make the columns sortable.
        //
        // Use the following data:
        //
        //      var users = new List<Models.User>();
        //
        //      users.Add(new Models.User { Name = "Dave", Password = "1DavePwd" });
        //      users.Add(new Models.User { Name = "Steve", Password = "2StevePwd" });
        //      users.Add(new Models.User { Name = "Lisa", Password = "3LisaPwd" });
        //
        //      uxList.ItemsSource = users;
        //
        // And use the following XAML from the exercise:
        //
        //      <ListView x:Name="uxList">
        //          <ListView.View>
        //              <GridView>
        //                  <GridViewColumn DisplayMemberBinding = "{Binding Name}" >
        //                  </ GridViewColumn >
        //                  < GridViewColumn DisplayMemberBinding="{Binding Password}">
        //                  </GridViewColumn>
        //              </GridView>
        //          </ListView.View>
        //      </ListView>
        //
        // When you click on the Name column you should see the list in order(Dave, Lisa, Steve).
        //
        // When you click on the Password column you should see the list in order(1DavePwd, 2StevePwd, 3LisaPwd)
        //
        // Use the following links to help out:
        //
        // http://www.wpf-tutorial.com/listview-control/listview-sorting/ (Links to an external site.)
        //
        // http://stackoverflow.com/questions/31527455/how-do-i-get-a-click-event-from-a-gridviewcolumn-header (Links to an external site.)
        //
        // Hint: Look at the GridViewColumnHeader element which is under the GridViewColumn
        //
        // SecondWindow.xaml and SecondWindow.xaml.cs should be the only files that need to be modified.
        //
        // ===========================================================================

        public MainWindow()
        {
            InitializeComponent();
            var users = new List<Models.User>();

            users.Add(new Models.User { Name = "Dave", Password = "1DavePwd" });
            users.Add(new Models.User { Name = "Steve", Password = "2StevePwd" });
            users.Add(new Models.User { Name = "Lisa", Password = "3LisaPwd" });

            uxList.ItemsSource = users;

            //  done in MainWindow.xaml instead on <GridViewColumnHeader> 
            //AddHandler(GridViewColumnHeader.MouseLeftButtonUpEvent, new MouseButtonEventHandler(uxList_MouseLeftButtonUp));

        }

        private void uxList_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SortDescription match;

            GridViewColumnHeader columnClicked = e.OriginalSource as GridViewColumnHeader;
            string columnHeaderText = columnClicked.Content.ToString();

            // sort the ListView column based on this column data
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(uxList.ItemsSource);

            // Remove all SortDescriptions first
            while (view.SortDescriptions.Any<SortDescription>())
            {
                match = view.SortDescriptions.FirstOrDefault<SortDescription>();
                view.SortDescriptions.Remove(match);
            }
            view.SortDescriptions.Add(new SortDescription(columnHeaderText, ListSortDirection.Ascending));

        }

    }
}
