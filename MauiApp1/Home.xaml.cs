namespace MauiApp1;

public partial class Home : ContentPage
{
    int TotalScore = 0; // zmieniono nazwę, aby nie kolidowała z kontrolką x:Name="TotalSum" z XAML
    int GameSum = 0;
    int DiceNum = 5;
    int[] DiceNumbers;
    Random RandomNum = new Random();
    string DefultDicePath = "question.jpg";
    public Home()
    {
        InitializeComponent();
        DiceNumbers = new int[DiceNum];
        CurentSum.Text = "Wynik tego losowania: " + GameSum;
        TotalSum.Text = "Wynik gry: " + TotalScore;
    }

    private void ThrowDice(object? sender, EventArgs e)
    {
        // Pobieramy referencje do kontrolek Image z XAML (muszą mieć x:Name="Die1", ..., "Die5")
        Image?[] diceImages = new Image?[]
        {
            (Image?)FindByName("Die1"),
            (Image?)FindByName("Die2"),
            (Image?)FindByName("Die3"),
            (Image?)FindByName("Die4"),
            (Image?)FindByName("Die5")
        };

        for (int i = 0; i < DiceNum; i++)
        {
            // losujemy wynik 1..6
            DiceNumbers[i] = RandomNum.Next(1, 7);

            // nazwa pliku odpowiadająca wynikowi np. "k1.jpg", "k6.jpg"
            string fileName = $"k{DiceNumbers[i]}.jpg";

            // Bezpieczne przypisanie — jeśli kontrolka nie istnieje, pomijamy
            if (diceImages[i] != null)
            {
                // MAUI rozpoznaje string jako źródło zasobu z Resources/Images ustawionych jako MauiImage
                diceImages[i]!.Source = fileName;
                // alternatywnie: diceImages[i].Source = ImageSource.FromFile(fileName);
            }
        }
        var groups = DiceNumbers.GroupBy(x => x).Where(g => g.Count() > 1).Select(g => new { Value = g.Key, Pairs = g.Count() / 2, Sum = g.Key * 2 * (g.Count() / 2) });
        if (!groups.Any())
        {
            GameSum = 0;
            TotalScore = TotalScore + 0;
            for (int i = 0; i < DiceNum; i++)
            {
                diceImages[i]!.Source = DefultDicePath;
            }
            CurentSum.Text = "Wynik tego losowania: " + GameSum;
            TotalSum.Text = "Wynik gry: " + TotalScore;
        }
        else
        {
            GameSum = groups.Sum(g => g.Sum);
            TotalScore = TotalScore + GameSum;
            CurentSum.Text = "Wynik tego losowania: " + GameSum;
            TotalSum.Text = "Wynik gry: " + TotalScore;
        }
    }
    private void ResetGame(object? sender, EventArgs e)
    {
        GameSum = 0;
        TotalScore = 0;
        CurentSum.Text = "Wynik tego losowania: " + GameSum;
        TotalSum.Text = "Wynik gry: " + TotalScore;
        Image?[] diceImages = new Image?[]
        {
            (Image?)FindByName("Die1"),
            (Image?)FindByName("Die2"),
            (Image?)FindByName("Die3"),
            (Image?)FindByName("Die4"),
            (Image?)FindByName("Die5")
        };
        for(int i = 0; i < DiceNum;i++)
        {
            diceImages[i]!.Source = DefultDicePath;
        }
    }

}
