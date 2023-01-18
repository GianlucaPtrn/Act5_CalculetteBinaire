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

namespace Act5_Calculatrice_Binaire
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            textBox1.PreviewTextInput += new TextCompositionEventHandler(OnlyOneAndZero);
            textBox2.PreviewTextInput += new TextCompositionEventHandler(OnlyOneAndZero);
            rationBtn1.Checked += new RoutedEventHandler(AdditionIsChecked);
            rationBtn2.Checked += new RoutedEventHandler(SoustractionIsChecked);
            BtnCalculer.Click += new RoutedEventHandler(BtnClickCalc);
        }

        String SelectOperation;

        private void OnlyOneAndZero(object sender, TextCompositionEventArgs e)
        {
            if (e.Text != "0" && e.Text != "1")
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void AdditionIsChecked(object sender, RoutedEventArgs e)
        {
            SelectOperation = "addition";
        }

        private void SoustractionIsChecked(object sender, RoutedEventArgs e)
        {
            SelectOperation = "soustraction";
        }

        private void BtnClickCalc(object sender, RoutedEventArgs e)
        {
            NbrBinaire nombre1 = new NbrBinaire(textBox1.Text);
            NbrBinaire nombre2 = new NbrBinaire(textBox2.Text);
            NbrBinaire tRes;
            bool ok;

            if (SelectOperation == "addition")
            {
                nombre1.Additionne(nombre2, out tRes, out ok);
                if (ok)
                {
                    MessageBox.Show(tRes.AffichageResultat());
                }
            }
            else if (SelectOperation == "soustraction")
            {
                ok = nombre1.Soustrait(nombre2, out tRes);
                if (ok)
                {
                    MessageBox.Show(tRes.AffichageResultat());
                }
            }
        }
    }
}
