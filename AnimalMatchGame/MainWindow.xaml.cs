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

namespace AnimalMatchGame {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    public MainWindow() {
      InitializeComponent();
      SetUpGame();
    }

    private void SetUpGame() {
      var animalEmoji = new List<string>() {
        "🐵", "🐶", "🐺", "🐱", "🦁", "🐯", "🦒", "🦊"
      };
      animalEmoji.AddRange(animalEmoji);

      var random = new Random();
      foreach (var textBlock in mainGrid.Children.OfType<TextBlock>()) {
        var index = random.Next(animalEmoji.Count);
        var nextEmoji = animalEmoji[index];
        textBlock.Text = nextEmoji;
        animalEmoji.RemoveAt(index);
      }
    }

    TextBlock lastTextBlockClicked;
    private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e) {
      var textBlock = sender as TextBlock;
      if (!FindingMatch()) {
        textBlock.Visibility = Visibility.Hidden;
        lastTextBlockClicked = textBlock;
      }
      else {
        if (textBlock.Text == lastTextBlockClicked.Text)
          textBlock.Visibility = Visibility.Hidden;
        else
          lastTextBlockClicked.Visibility = Visibility.Visible;
        lastTextBlockClicked = null;
      }
    }

    private bool FindingMatch() {
      return lastTextBlockClicked != null;
    }
  }
}
