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

namespace Act5_Calculatrice_Procedural
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window{
        public MainWindow(){
            InitializeComponent();
            textBox1.PreviewTextInput += new TextCompositionEventHandler(OnlyOneAndZero);
            textBox2.PreviewTextInput += new TextCompositionEventHandler(OnlyOneAndZero);
            rationBtn1.Checked += new RoutedEventHandler(AdditionIsChecked);
            rationBtn2.Checked += new RoutedEventHandler(SoustractionIsChecked);
            BtnCalculer.Click += new RoutedEventHandler(BtnClickCalc);
        }

        String SelectOperation;

        private ushort[] RemplirTableau(string nbrBinaire){
            ushort[] tabBin = new ushort[8];
            for (int i = 0; i <= 7; i++){
                tabBin[i] = 0;
            }

            for (int i = 0; i <= nbrBinaire.Length - 1; i++){
                tabBin[7 - i] = ushort.Parse(nbrBinaire[nbrBinaire.Length - 1 - i].ToString());
            }
            return tabBin;
        }

        private void Additionner(ushort[] t1, ushort[] t2, out ushort[] tRes, out bool ok){
            ushort report = 0;
            ok = true;
            tRes = new ushort[8];
            for (int i = 7; i >= 0; i--){
                ushort res = (ushort)((t1[i] + t2[i]) + report);
                if (res / 2 == 0){
                    report = 0;
                }
                else{
                    report = 1;
                }
                if (res == 1){
                    tRes[i] = 1;
                }
                else{
                    if (res % 2 == 1)
                    {
                        tRes[i] = 1;
                    }
                    else{
                        tRes[i] = 0;
                    }
                }
            }
            if (report == 1){
                ok = false;
            }
        }

        private bool Soustrait(ushort[] t1, ushort[] t2, out ushort[] tRes){
            int[] tTemp = new int[8];
            tRes = new ushort[8];
            bool ok = true;

            for (int i = 0; i < 8; i++){
                tTemp[i] = t1[i] - t2[i];
            }

            for (int i = 7; i >= 1; i--){
                if (tTemp[i] == -1){
                    t2[i - 1] = (ushort)(t2[i - 1] + 1);
                    t1[i] = (ushort)(t1[i] + 2);
                }
                if (t1[i] < t2[i]){
                    t2[i - 1] = (ushort)(t2[i - 1] + 1);
                    t1[i] = (ushort)(t1[i] + 2);
                }
                tRes[i] = (ushort)(t1[i] - t2[i]);
            }

            if (t1[0] >= t2[0]){
                tRes[0] = (ushort)(t1[0] - t2[0]);
            }
            else{
                ok = false;
            }
            return ok;
        }

        public string AffichageResultat(ushort[] tRes){
            string Reponse = "Le résultat est ";

            for (int i = 0; i < tRes.Length; i++){
                Reponse = Reponse + tRes[i];
            }
            return Reponse;
        }

        private void OnlyOneAndZero(object sender, TextCompositionEventArgs e){
            if (e.Text != "0" && e.Text != "1"){
                e.Handled = true;
            }
            else{
                e.Handled = false;
            }
        }

        private void AdditionIsChecked(object sender, RoutedEventArgs e){
            SelectOperation = "addition";
        }

        private void SoustractionIsChecked(object sender, RoutedEventArgs e){
            SelectOperation = "soustraction";
        }

        private void BtnClickCalc(object sender, RoutedEventArgs e){
            ushort[] t1 = RemplirTableau(textBox1.Text);
            ushort[] t2 = RemplirTableau(textBox2.Text);
            ushort[] tRes = new ushort[8];
            bool ok;

            if (SelectOperation == "addition"){
                Additionner(t1, t2, out tRes, out ok);
                if (ok){
                    MessageBox.Show(AffichageResultat(tRes));
                }
            }
            else if (SelectOperation == "soustraction"){ 
                ok = Soustrait(t1, t2, out tRes);
                if(ok){
                    MessageBox.Show(AffichageResultat(tRes));
                }
            }
        }
    }
}