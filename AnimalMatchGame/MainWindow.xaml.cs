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
using System.Windows.Threading;

namespace AnimalMatchGame {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    DispatcherTimer timer = new DispatcherTimer();
    int tenthsOfSecondsElapsed;
    int matchesFound;

    public MainWindow() {
      InitializeComponent();

      timer.Interval = TimeSpan.FromSeconds(.1);
      timer.Tick += Timer_Tick;
      SetUpGame();
    }

    private void Timer_Tick(object sender, EventArgs e) {
      tenthsOfSecondsElapsed++;
      timeTextBlock.Text = (tenthsOfSecondsElapsed / 10f).ToString();
      if (GameCleard()) {
        timer.Stop();
        timeTextBlock.Text += " - Play again?";
      }
    }

    private void SetUpGame() {
      var animalEmoji = new List<string>() {
        "🐵", "🐶", "🐺", "🐱", "🦁", "🐯", "🦒", "🦊"
      };
      animalEmoji.AddRange(animalEmoji);

      var random = new Random();
      foreach (var textBlock in mainGrid.Children.OfType<TextBlock>()) {
        if (animalEmoji.Count <= 0) break;

        var index = random.Next(animalEmoji.Count);
        var nextEmoji = animalEmoji[index];
        animalEmoji.RemoveAt(index);

        textBlock.Text = nextEmoji;
        textBlock.Visibility = Visibility.Visible;
      }

      tenthsOfSecondsElapsed = 0;
      matchesFound = 0;

      timer.Start();
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
          OnMatched(textBlock);
        else
          lastTextBlockClicked.Visibility = Visibility.Visible;
        lastTextBlockClicked = null;
      }
    }

    private bool FindingMatch() {
      return lastTextBlockClicked != null;
    }

    private void OnMatched(TextBlock textBlock) {
      textBlock.Visibility = Visibility.Hidden;
      matchesFound++;
    }

    private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e) {
      if (GameCleard()) {
        SetUpGame();
      }
    }

    private bool GameCleard() {
      return matchesFound >= 8;
    }
  }
}
