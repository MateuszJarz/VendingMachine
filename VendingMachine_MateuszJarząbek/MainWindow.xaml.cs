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

namespace VendingMachine_MateuszJarząbek
{
	/// <summary>
	/// Logika interakcji dla klasy MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		
		
		//deklaracje


		//deklaracja tablicy dla przechowania nominałów monet
		float[] ValueOfCoins = { 5, 2, 1, 0.5F };
		float[] ValueOfCoinsEUR = {2, 1, 0.5F, 0.2F };
		float[] ValueOfCoinsUS = { 5, 2, 1, 0.5F };

	
		

		//deklaracja zmiennych globalnychh na potrzebe przechowania kwot 
		float ThrownCoins = 0.0F; //wrzuceone monety
		float AmountToPay = 0.0F; // kwota do zapłaty
		float AmountToPayOut = 0.0F; // kwota do wypłaty
		ushort IndexContainerOfCoins = 0;
		// ustalenie przez programistę ilosc monet 
		const ushort CardinalityOfCoins = 50;

		//deklaracja struktury dla tablicy pobierajacej naraz wartosci float oraz int
		struct Coins
		{
			public int Cardinality;
			public float Value;
		}

		// deklaracja tablicy
		Coins[] ContainerOfCoins;

		// metoda sprawdza czy możemy dokonać wypłaty
		static bool CanThePaymentMade(Coins[] ContainerOfCoins, float AmountToPayOut)
		{	// deklaracja  zmienej kapitału 
			float VendingMachinesCapital = 0.0F;

			for (ushort i = 0; i < ContainerOfCoins.Length; i++)
			{
				if (ContainerOfCoins[i].Cardinality > 0)
				{

					VendingMachinesCapital += ContainerOfCoins[i].Cardinality * ContainerOfCoins[i].Value;
				}
			}
			return VendingMachinesCapital > AmountToPayOut;
		}


		public MainWindow()
		{
			
			

			InitializeComponent();
			//zablokowanie przycisków

			mj_btn_05.IsEnabled = false;
			mj_btn_1.IsEnabled = false;
			mj_btn_2.IsEnabled = false;
			mj_btn_5.IsEnabled = false;
			mj_btn_Accept.IsEnabled = false;
			mj_btn_reset.IsEnabled = false;

			ContainerOfCoins = new Coins[ValueOfCoins.Length];
			//blokada wysztkich wyborów  do momentu wybrania waluty
			mj_btn_ChipsBag.IsEnabled = false;
			mj_btn_CocaCola.IsEnabled = false;
			mj_btn_ColaLite.IsEnabled = false;
			mj_btn_Fanta.IsEnabled = false;
			mj_btn_LaysP.IsEnabled = false;
			mj_btn_LaysR.IsEnabled = false;
			mj_btn_MilkiWay.IsEnabled = false;
			mj_btn_Mw.IsEnabled = false;
			mj_btn_Pepsi.IsEnabled = false;
			mj_btn_Snickers.IsEnabled = false;
			mj_btn_SnickersWhite.IsEnabled = false;
			mj_btn_Sprite.IsEnabled = false;

			//aktywacja przycisku reset

			mj_btn_reset.IsEnabled = false;

		}

		

		private void mj_btn_Accept_Click(object sender, RoutedEventArgs e)
		{
			
		   // zmienne lokalne 
			AmountToPayOut = ThrownCoins - AmountToPay;

			float RestToPayout = AmountToPayOut;
			float RestToPayout2 = AmountToPayOut;
			
			ushort NumberOfCoins;
			//ushort IndexDGV = 0;

			//sprawdzenie czy w automacie znajduje sie odpowiedznia liczba monet do wydania reszty
			if (!CanThePaymentMade(ContainerOfCoins, AmountToPayOut))
			{

				mj_lbl_price.Content = "PRZEPRASZAMY: automat nie może wydać reszty";
				//er.SetError(mj_btnZaplacMonetami, "PRZEPRASZAMY: automat nie może wydać reszty");
			}



		 	if (RestToPayout == 0.0F)
			{
				mj_lbl_price.Content = "Dziękujemy za zapłatę";
			}
			else
			{

				while ((RestToPayout > 0.0F) && (IndexContainerOfCoins < ContainerOfCoins.Length))
				{
					// obliczenie liczby monet, które mogą być użyte do wypłaty RestToPayout
					NumberOfCoins = (ushort)(RestToPayout / ContainerOfCoins[IndexContainerOfCoins].Value);

					//sprawdzenie czy pojemnik monet o indexie IndexContainerOfCoins ma odpowiedznią liczbę monet do wypłaty

					if (NumberOfCoins > ContainerOfCoins[IndexContainerOfCoins].Cardinality)
					{
						// pobieramy wszytkie nominały monet z pojemnika na pozycje 

						NumberOfCoins = (ushort)ContainerOfCoins[IndexContainerOfCoins].Cardinality;

						//wyzerowanie licznośći nominałów
						ContainerOfCoins[IndexContainerOfCoins].Cardinality = 0;

					}
					else
					{
						// pobieramy tylko wymaganą cześć nominałów z pojeminka 

						ContainerOfCoins[IndexContainerOfCoins].Cardinality = (ushort)
						(ContainerOfCoins[IndexContainerOfCoins].Cardinality - NumberOfCoins);

						if (NumberOfCoins > 0)
						{
							/*mj_dgv.Rows.Add();
							mj_dgv.Rows[IndexDGV].Cells[0].Value = NumberOfCoins;
							mj_dgv.Rows[IndexDGV].Cells[1].Value = ContainerOfCoins[IndexContainerOfCoins].Value;
							mj_dgv.Rows[IndexDGV].Cells[2].Value = ContainerOfCoins[IndexContainerOfCoins].Cardinality;
							*/
							//IndexDGV++;



							mj_lbl_info.Content = "Wydano : " + (CardinalityOfCoins - ContainerOfCoins[0].Cardinality) + " monet, o nominale : " + ContainerOfCoins[0].Value + " pozostało w automacie : " + ContainerOfCoins[0].Cardinality + " monet";
							mj_lbl_info_Copy.Content = "Wydano : " + (CardinalityOfCoins - ContainerOfCoins[1].Cardinality) + " monet, o nominale : " + ContainerOfCoins[1].Value + " pozostało w automacie : " + ContainerOfCoins[1].Cardinality + " monet";
							mj_lbl_info_Copy1.Content = "Wydano : " + (CardinalityOfCoins - ContainerOfCoins[2].Cardinality) + " monet, o nominale : " + ContainerOfCoins[2].Value + " pozostało w automacie : " + ContainerOfCoins[2].Cardinality + " monet";
							mj_lbl_info_Copy2.Content = "Wydano : " + (CardinalityOfCoins - ContainerOfCoins[3].Cardinality) + " monet, o nominale : " + ContainerOfCoins[3].Value + " pozostało w automacie : " + ContainerOfCoins[3].Cardinality + " monet";




						}




					}


					//akutalizacvja wartosci Reszty do wypłaty 


					RestToPayout = RestToPayout - NumberOfCoins * ContainerOfCoins[IndexContainerOfCoins].Value;

					IndexContainerOfCoins++;


					// mj_lbl_monitor.Text = "Reszta:" + RestToPayout2;
					mj_lbl_price.Content = "Reszta:" + RestToPayout2;
					//end while

				}
			}
			
		}

		private void mj_btn_CocaCola_Click(object sender, RoutedEventArgs e)
		{
			if (mj_cb_waluty.SelectedItem.Equals(EUR))
			{

				AmountToPay = 1.20F;
				mj_lbl_price.Content = "CENA: 1.20 EUR";


				mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
			}
			else
			{


				if (mj_cb_waluty.SelectedItem.Equals(US))
				{
					AmountToPay = 1.50F;
					mj_lbl_price.Content = "CENA: 1.50 DOL";


					mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
				}
				else
				{
					AmountToPay = 3.50F;
					mj_lbl_price.Content = "CENA: 3.50";
				}
			}
			mj_btn_ChipsBag.IsEnabled = false;
			mj_btn_CocaCola.IsEnabled = false;
			mj_btn_ColaLite.IsEnabled = false;
			mj_btn_Fanta.IsEnabled = false;
			mj_btn_LaysP.IsEnabled = false;
			mj_btn_LaysR.IsEnabled = false;
			mj_btn_MilkiWay.IsEnabled = false;
			mj_btn_Mw.IsEnabled = false;
			mj_btn_Pepsi.IsEnabled = false;
			mj_btn_Snickers.IsEnabled = false;
			mj_btn_SnickersWhite.IsEnabled = false;
			mj_btn_Sprite.IsEnabled = false;


			// aktywacja wyboru monet 

			mj_btn_05.IsEnabled = true;
			mj_btn_1.IsEnabled = true;
			mj_btn_2.IsEnabled = true;
			mj_btn_5.IsEnabled = true;

			//aktywacja przycisku reset

			mj_btn_reset.IsEnabled = true;




		}

		private void mj_btn_Fanta_Click(object sender, RoutedEventArgs e)
		{
			
			if (mj_cb_waluty.SelectedItem.Equals(EUR))
			{

				AmountToPay = 2.70F;
				mj_lbl_price.Content = "CENA: 2.70 EUR";


				mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
			}
			else
			{


				if (mj_cb_waluty.SelectedItem.Equals(US))
				{
					AmountToPay = 3.50F;
					mj_lbl_price.Content = "CENA: 3.50 DOL";


					mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
				}
				else
				{
					AmountToPay = 2.50F;
					mj_lbl_price.Content = "CENA: 2.50";
				}
			}




			mj_btn_ChipsBag.IsEnabled = false;
			mj_btn_CocaCola.IsEnabled = false;
			mj_btn_ColaLite.IsEnabled = false;
			mj_btn_Fanta.IsEnabled = false;
			mj_btn_LaysP.IsEnabled = false;
			mj_btn_LaysR.IsEnabled = false;
			mj_btn_MilkiWay.IsEnabled = false;
			mj_btn_Mw.IsEnabled = false;
			mj_btn_Pepsi.IsEnabled = false;
			mj_btn_Snickers.IsEnabled = false;
			mj_btn_SnickersWhite.IsEnabled = false;
			mj_btn_Sprite.IsEnabled = false;

			// aktywacja wyboru monet 

			mj_btn_05.IsEnabled = true;
			mj_btn_1.IsEnabled = true;
			mj_btn_2.IsEnabled = true;
			mj_btn_5.IsEnabled = true;

			//aktywacja przycisku reset

			mj_btn_reset.IsEnabled = true;


		}

		private void mj_btn_Sprite_Click(object sender, RoutedEventArgs e)
		{

			if (mj_cb_waluty.SelectedItem.Equals(EUR))
			{

				AmountToPay = 1.40F;
				mj_lbl_price.Content = "CENA: 1.40 EUR";


				mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
			}
			else
			{


				if (mj_cb_waluty.SelectedItem.Equals(US))
				{
					AmountToPay = 2.50F;
					mj_lbl_price.Content = "CENA: 2.50 DOL";


					mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
				}
				else
				{
					AmountToPay = 2.50F;
					mj_lbl_price.Content = "CENA: 2.50";
				}
			}
			mj_btn_ChipsBag.IsEnabled = false;
			mj_btn_CocaCola.IsEnabled = false;
			mj_btn_ColaLite.IsEnabled = false;
			mj_btn_Fanta.IsEnabled = false;
			mj_btn_LaysP.IsEnabled = false;
			mj_btn_LaysR.IsEnabled = false;
			mj_btn_MilkiWay.IsEnabled = false;
			mj_btn_Mw.IsEnabled = false;
			mj_btn_Pepsi.IsEnabled = false;
			mj_btn_Snickers.IsEnabled = false;
			mj_btn_SnickersWhite.IsEnabled = false;
			mj_btn_Sprite.IsEnabled = false;

			// aktywacja wyboru monet 

			mj_btn_05.IsEnabled = true;
			mj_btn_1.IsEnabled = true;
			mj_btn_2.IsEnabled = true;
			mj_btn_5.IsEnabled = true;

			//aktywacja przycisku reset

			mj_btn_reset.IsEnabled = true;


		}

		// DOKONCZYĆ I ZROBIĆ DOKUMENTACJNE I KONIEC

		private void mj_btn_Mw_Click(object sender, RoutedEventArgs e)
		{

			if (mj_cb_waluty.SelectedItem.Equals(EUR))
			{

				AmountToPay = 2.40F;
				mj_lbl_price.Content = "CENA: 2.40 EUR";


				mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
			}
			else
			{


				if (mj_cb_waluty.SelectedItem.Equals(US))
				{
					AmountToPay = 1.50F;
					mj_lbl_price.Content = "CENA: 1.50 DOL";


					mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
				}
				else
				{
					AmountToPay = 3.50F;
					mj_lbl_price.Content = "CENA: 5.50";
				}
			}
			mj_btn_ChipsBag.IsEnabled = false;
			mj_btn_CocaCola.IsEnabled = false;
			mj_btn_ColaLite.IsEnabled = false;
			mj_btn_Fanta.IsEnabled = false;
			mj_btn_LaysP.IsEnabled = false;
			mj_btn_LaysR.IsEnabled = false;
			mj_btn_MilkiWay.IsEnabled = false;
			mj_btn_Mw.IsEnabled = false;
			mj_btn_Pepsi.IsEnabled = false;
			mj_btn_Snickers.IsEnabled = false;
			mj_btn_SnickersWhite.IsEnabled = false;
			mj_btn_Sprite.IsEnabled = false;

			// aktywacja wyboru monet 

			mj_btn_05.IsEnabled = true;
			mj_btn_1.IsEnabled = true;
			mj_btn_2.IsEnabled = true;
			mj_btn_5.IsEnabled = true;

			//aktywacja przycisku reset

			mj_btn_reset.IsEnabled = true;
		}


		private void mj_btn_Pepsi_Click(object sender, RoutedEventArgs e)
		{

			if (mj_cb_waluty.SelectedItem.Equals(EUR))
			{

				AmountToPay = 1.20F;
				mj_lbl_price.Content = "CENA: 1.20 EUR";


				mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
			}
			else
			{


				if (mj_cb_waluty.SelectedItem.Equals(US))
				{
					AmountToPay = 1.50F;
					mj_lbl_price.Content = "CENA: 1.50 DOL";


					mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
				}
				else
				{
					AmountToPay = 3.50F;
					mj_lbl_price.Content = "CENA: 3.50";
				}
			}
			mj_btn_ChipsBag.IsEnabled = false;
			mj_btn_CocaCola.IsEnabled = false;
			mj_btn_ColaLite.IsEnabled = false;
			mj_btn_Fanta.IsEnabled = false;
			mj_btn_LaysP.IsEnabled = false;
			mj_btn_LaysR.IsEnabled = false;
			mj_btn_MilkiWay.IsEnabled = false;
			mj_btn_Mw.IsEnabled = false;
			mj_btn_Pepsi.IsEnabled = false;
			mj_btn_Snickers.IsEnabled = false;
			mj_btn_SnickersWhite.IsEnabled = false;
			mj_btn_Sprite.IsEnabled = false;

			// aktywacja wyboru monet 

			mj_btn_05.IsEnabled = true;
			mj_btn_1.IsEnabled = true;
			mj_btn_2.IsEnabled = true;
			mj_btn_5.IsEnabled = true;

			//aktywacja przycisku reset

			mj_btn_reset.IsEnabled = true;
		}

		private void mj_btn_ColaLite_Click(object sender, RoutedEventArgs e)
		{

			if (mj_cb_waluty.SelectedItem.Equals(EUR))
			{

				AmountToPay = 3.20F;
				mj_lbl_price.Content = "CENA: 3.20 EUR";


				mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
			}
			else
			{


				if (mj_cb_waluty.SelectedItem.Equals(US))
				{
					AmountToPay = 2.50F;
					mj_lbl_price.Content = "CENA: 2.50 DOL";


					mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
				}
				else
				{
					AmountToPay = 4.50F;
					mj_lbl_price.Content = "CENA: 4.50";
				}
			}
			mj_btn_ChipsBag.IsEnabled = false;
			mj_btn_CocaCola.IsEnabled = false;
			mj_btn_ColaLite.IsEnabled = false;
			mj_btn_Fanta.IsEnabled = false;
			mj_btn_LaysP.IsEnabled = false;
			mj_btn_LaysR.IsEnabled = false;
			mj_btn_MilkiWay.IsEnabled = false;
			mj_btn_Mw.IsEnabled = false;
			mj_btn_Pepsi.IsEnabled = false;
			mj_btn_Snickers.IsEnabled = false;
			mj_btn_SnickersWhite.IsEnabled = false;
			mj_btn_Sprite.IsEnabled = false;

			// aktywacja wyboru monet 

			mj_btn_05.IsEnabled = true;
			mj_btn_1.IsEnabled = true;
			mj_btn_2.IsEnabled = true;
			mj_btn_5.IsEnabled = true;

			//aktywacja przycisku reset

			mj_btn_reset.IsEnabled = true;
		}

		private void mj_btn_Snickers_Click(object sender, RoutedEventArgs e)
		{

			if (mj_cb_waluty.SelectedItem.Equals(EUR))
			{

				AmountToPay = 1.20F;
				mj_lbl_price.Content = "CENA: 1.20 EUR";


				mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
			}
			else
			{


				if (mj_cb_waluty.SelectedItem.Equals(US))
				{
					AmountToPay = 3.50F;
					mj_lbl_price.Content = "CENA: 3.50 DOL";


					mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
				}
				else
				{
					AmountToPay = 4.50F;
					mj_lbl_price.Content = "CENA: 4.50";
				}
			}
			mj_btn_ChipsBag.IsEnabled = false;
			mj_btn_CocaCola.IsEnabled = false;
			mj_btn_ColaLite.IsEnabled = false;
			mj_btn_Fanta.IsEnabled = false;
			mj_btn_LaysP.IsEnabled = false;
			mj_btn_LaysR.IsEnabled = false;
			mj_btn_MilkiWay.IsEnabled = false;
			mj_btn_Mw.IsEnabled = false;
			mj_btn_Pepsi.IsEnabled = false;
			mj_btn_Snickers.IsEnabled = false;
			mj_btn_SnickersWhite.IsEnabled = false;
			mj_btn_Sprite.IsEnabled = false;

			// aktywacja wyboru monet 

			mj_btn_05.IsEnabled = true;
			mj_btn_1.IsEnabled = true;
			mj_btn_2.IsEnabled = true;
			mj_btn_5.IsEnabled = true;

			//aktywacja przycisku reset

			mj_btn_reset.IsEnabled = true;
		}

		private void mj_btn_SnickersWhite_Click(object sender, RoutedEventArgs e)
		{

			if (mj_cb_waluty.SelectedItem.Equals(EUR))
			{

				AmountToPay = 0.80F;
				mj_lbl_price.Content = "CENA: 0.80 EUR";


				mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
			}
			else
			{


				if (mj_cb_waluty.SelectedItem.Equals(US))
				{
					AmountToPay = 2.50F;
					mj_lbl_price.Content = "CENA: 2.50 DOL";


					mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
				}
				else
				{
					AmountToPay = 1.50F;
					mj_lbl_price.Content = "CENA: 1.50";
				}
			}
			mj_btn_ChipsBag.IsEnabled = false;
			mj_btn_CocaCola.IsEnabled = false;
			mj_btn_ColaLite.IsEnabled = false;
			mj_btn_Fanta.IsEnabled = false;
			mj_btn_LaysP.IsEnabled = false;
			mj_btn_LaysR.IsEnabled = false;
			mj_btn_MilkiWay.IsEnabled = false;
			mj_btn_Mw.IsEnabled = false;
			mj_btn_Pepsi.IsEnabled = false;
			mj_btn_Snickers.IsEnabled = false;
			mj_btn_SnickersWhite.IsEnabled = false;
			mj_btn_Sprite.IsEnabled = false;

			// aktywacja wyboru monet 

			mj_btn_05.IsEnabled = true;
			mj_btn_1.IsEnabled = true;
			mj_btn_2.IsEnabled = true;
			mj_btn_5.IsEnabled = true;

			//aktywacja przycisku reset

			mj_btn_reset.IsEnabled = true;
		}

		private void mj_btn_MilkiWay_Click(object sender, RoutedEventArgs e)
		{

			if (mj_cb_waluty.SelectedItem.Equals(EUR))
			{

				AmountToPay = 0.60F;
				mj_lbl_price.Content = "CENA: = 0.60 EUR";


				mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
			}
			else
			{


				if (mj_cb_waluty.SelectedItem.Equals(US))
				{
					AmountToPay = 0.50F;
					mj_lbl_price.Content = "CENA: 0.50 DOL";


					mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
				}
				else
				{
					AmountToPay = 1.50F;
					mj_lbl_price.Content = "CENA: 1.50";
				}
			}
			mj_btn_ChipsBag.IsEnabled = false;
			mj_btn_CocaCola.IsEnabled = false;
			mj_btn_ColaLite.IsEnabled = false;
			mj_btn_Fanta.IsEnabled = false;
			mj_btn_LaysP.IsEnabled = false;
			mj_btn_LaysR.IsEnabled = false;
			mj_btn_MilkiWay.IsEnabled = false;
			mj_btn_Mw.IsEnabled = false;
			mj_btn_Pepsi.IsEnabled = false;
			mj_btn_Snickers.IsEnabled = false;
			mj_btn_SnickersWhite.IsEnabled = false;
			mj_btn_Sprite.IsEnabled = false;

			// aktywacja wyboru monet 

			mj_btn_05.IsEnabled = true;
			mj_btn_1.IsEnabled = true;
			mj_btn_2.IsEnabled = true;
			mj_btn_5.IsEnabled = true;

			//aktywacja przycisku reset

			mj_btn_reset.IsEnabled = true;
		}

		private void mj_btn_ChipsBag_Click(object sender, RoutedEventArgs e)
		{

			if (mj_cb_waluty.SelectedItem.Equals(EUR))
			{

				AmountToPay = 3.60F;
				mj_lbl_price.Content = "CENA: 3.60 EUR";


				mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
			}
			else
			{


				if (mj_cb_waluty.SelectedItem.Equals(US))
				{
					AmountToPay = 4.50F;
					mj_lbl_price.Content = "CENA: 4.50 DOL";


					mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
				}
				else
				{
					AmountToPay = 5.50F;
					mj_lbl_price.Content = "CENA: 5.50";
				}
			}
			mj_btn_ChipsBag.IsEnabled = false;
			mj_btn_CocaCola.IsEnabled = false;
			mj_btn_ColaLite.IsEnabled = false;
			mj_btn_Fanta.IsEnabled = false;
			mj_btn_LaysP.IsEnabled = false;
			mj_btn_LaysR.IsEnabled = false;
			mj_btn_MilkiWay.IsEnabled = false;
			mj_btn_Mw.IsEnabled = false;
			mj_btn_Pepsi.IsEnabled = false;
			mj_btn_Snickers.IsEnabled = false;
			mj_btn_SnickersWhite.IsEnabled = false;
			mj_btn_Sprite.IsEnabled = false;

			// aktywacja wyboru monet 

			mj_btn_05.IsEnabled = true;
			mj_btn_1.IsEnabled = true;
			mj_btn_2.IsEnabled = true;
			mj_btn_5.IsEnabled = true;

			//aktywacja przycisku reset

			mj_btn_reset.IsEnabled = true;
		}

		private void mj_btn_LaysP_Click(object sender, RoutedEventArgs e)
		{

			if (mj_cb_waluty.SelectedItem.Equals(EUR))
			{

				AmountToPay = 3F;
				mj_lbl_price.Content = "CENA: 3 EUR";


				mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
			}
			else
			{


				if (mj_cb_waluty.SelectedItem.Equals(US))
				{
					AmountToPay = 4.50F;
					mj_lbl_price.Content = "CENA: 4.50 DOL";


					mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
				}
				else
				{
					AmountToPay = 6.50F;
					mj_lbl_price.Content = "CENA: 6.50";
				}
			}
			mj_btn_ChipsBag.IsEnabled = false;
			mj_btn_CocaCola.IsEnabled = false;
			mj_btn_ColaLite.IsEnabled = false;
			mj_btn_Fanta.IsEnabled = false;
			mj_btn_LaysP.IsEnabled = false;
			mj_btn_LaysR.IsEnabled = false;
			mj_btn_MilkiWay.IsEnabled = false;
			mj_btn_Mw.IsEnabled = false;
			mj_btn_Pepsi.IsEnabled = false;
			mj_btn_Snickers.IsEnabled = false;
			mj_btn_SnickersWhite.IsEnabled = false;
			mj_btn_Sprite.IsEnabled = false;

			// aktywacja wyboru monet 

			mj_btn_05.IsEnabled = true;
			mj_btn_1.IsEnabled = true;
			mj_btn_2.IsEnabled = true;
			mj_btn_5.IsEnabled = true;

			//aktywacja przycisku reset

			mj_btn_reset.IsEnabled = true;
		}

		private void mj_btn_LaysR_Click(object sender, RoutedEventArgs e)
		{

			if (mj_cb_waluty.SelectedItem.Equals(EUR))
			{

				AmountToPay = 4.20F;
				mj_lbl_price.Content = "CENA: 4.20 EUR";


				mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
			}
			else
			{


				if (mj_cb_waluty.SelectedItem.Equals(US))
				{
					AmountToPay = 6.50F;
					mj_lbl_price.Content = "CENA: 6.50 DOL";


					mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
				}
				else
				{
					AmountToPay = 6.50F;
					mj_lbl_price.Content = "CENA: 6.50";
				}
			}
			mj_btn_ChipsBag.IsEnabled = false;
			mj_btn_CocaCola.IsEnabled = false;
			mj_btn_ColaLite.IsEnabled = false;
			mj_btn_Fanta.IsEnabled = false;
			mj_btn_LaysP.IsEnabled = false;
			mj_btn_LaysR.IsEnabled = false;
			mj_btn_MilkiWay.IsEnabled = false;
			mj_btn_Mw.IsEnabled = false;
			mj_btn_Pepsi.IsEnabled = false;
			mj_btn_Snickers.IsEnabled = false;
			mj_btn_SnickersWhite.IsEnabled = false;
			mj_btn_Sprite.IsEnabled = false;

			// aktywacja wyboru monet 

			mj_btn_05.IsEnabled = true;
			mj_btn_1.IsEnabled = true;
			mj_btn_2.IsEnabled = true;
			mj_btn_5.IsEnabled = true;

			//aktywacja przycisku reset

			mj_btn_reset.IsEnabled = true;
		}
		//reset
		private void mj_btn_reset_Click(object sender, RoutedEventArgs e)
		{
			AmountToPay = 0.0F;
			ThrownCoins = 0.0F;
			AmountToPayOut = 0.0F;

			mj_lbl_price.Content = "Wybierz produkt :";
			mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;

			mj_btn_ChipsBag.IsEnabled = true;
			mj_btn_CocaCola.IsEnabled = true;
			mj_btn_ColaLite.IsEnabled = true;
			mj_btn_Fanta.IsEnabled = true;
			mj_btn_LaysP.IsEnabled = true;
			mj_btn_LaysR.IsEnabled = true;
			mj_btn_MilkiWay.IsEnabled = true;
			mj_btn_Mw.IsEnabled = true;
			mj_btn_Pepsi.IsEnabled = true;
			mj_btn_Snickers.IsEnabled = true;
			mj_btn_SnickersWhite.IsEnabled = true;
			mj_btn_Sprite.IsEnabled = true;
			mj_btn_Accept.IsEnabled = false;

			mj_cb_waluty.IsEnabled = true;





			System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
			Application.Current.Shutdown();



			// aktywacja wyboru monet 


		}
		// monety
		private void mj_btn_05_Click(object sender, RoutedEventArgs e)
		{

			if (mj_cb_waluty.SelectedItem.Equals(EUR))
			{

				ThrownCoins = ThrownCoins + 0.2F;


				mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
			}
			else
			{
				ThrownCoins = ThrownCoins + 0.5F;
				mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
			}


			mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;

			if (AmountToPay <= ThrownCoins)
			{
				mj_btn_Accept.IsEnabled = true;
			}

			
		}

		private void mj_btn_1_Click(object sender, RoutedEventArgs e)
		{
			if (mj_cb_waluty.SelectedItem.Equals(EUR))
			{

				ThrownCoins = ThrownCoins + 0.5F;


				mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
			}
			else
			{
				ThrownCoins = ThrownCoins + 1;
				mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
			}
			mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;

			if (AmountToPay <= ThrownCoins)
			{
				mj_btn_Accept.IsEnabled = true;
			}
			
		}

		private void mj_btn_2_Click(object sender, RoutedEventArgs e)
		{
			if (mj_cb_waluty.SelectedItem.Equals(EUR))
			{

				ThrownCoins = ThrownCoins + 1F;


				mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
			}
			else
			{
				ThrownCoins = ThrownCoins + 2;
				mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
			}



			mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;

			if (AmountToPay <= ThrownCoins)
			{
				mj_btn_Accept.IsEnabled = true;
			}
			
		}

		private void mj_btn_5_Click(object sender, RoutedEventArgs e)
		{


			if (mj_cb_waluty.SelectedItem.Equals(EUR)) 
			{

				ThrownCoins = ThrownCoins + 2F;
				
				
				mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
			}
			else
			{
				ThrownCoins = ThrownCoins + 5;
				mj_lbl_throwcoins.Content = "Wrzuciłeś : " + ThrownCoins;
			}
			

			

			if (AmountToPay <= ThrownCoins)
			{
				mj_btn_Accept.IsEnabled = true;
			}

			
		}

		private void mj_cb_waluty_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			



			if (mj_cb_waluty.SelectedItem.Equals(PL))
			{
				for (ushort i = 0; i < ContainerOfCoins.Length; i++)
				{
					ContainerOfCoins[i].Cardinality = CardinalityOfCoins;
					ContainerOfCoins[i].Value = ValueOfCoins[i];

					//blokada wysztkich wyborów po klikniecu 
					mj_btn_ChipsBag.IsEnabled = true;
					mj_btn_CocaCola.IsEnabled = true;
					mj_btn_ColaLite.IsEnabled = true;
					mj_btn_Fanta.IsEnabled = true;
					mj_btn_LaysP.IsEnabled = true;
					mj_btn_LaysR.IsEnabled = true;
					mj_btn_MilkiWay.IsEnabled =true;
					mj_btn_Mw.IsEnabled = true;
					mj_btn_Pepsi.IsEnabled = true;
					mj_btn_Snickers.IsEnabled = true;
					mj_btn_SnickersWhite.IsEnabled = true;
					mj_btn_Sprite.IsEnabled = true;

					mj_cb_waluty.IsEnabled = false;





				}
			}

			if (mj_cb_waluty.SelectedItem.Equals(EUR))
			{
				for (ushort i = 0; i < ContainerOfCoins.Length; i++)
				{
					ContainerOfCoins[i].Cardinality = CardinalityOfCoins;
					ContainerOfCoins[i].Value = ValueOfCoinsEUR[i];
					mj_btn_5.Content = "2 EUR";
					mj_btn_2.Content = "1 EUR";
					mj_btn_1.Content = "0.5 EUR";
					mj_btn_05.Content = "0.2 EUR";

					////blokada wysztkich wyborów po klikniecu 
					mj_btn_ChipsBag.IsEnabled = true;
					mj_btn_CocaCola.IsEnabled = true;
					mj_btn_ColaLite.IsEnabled = true;
					mj_btn_Fanta.IsEnabled = true;
					mj_btn_LaysP.IsEnabled = true;
					mj_btn_LaysR.IsEnabled = true;
					mj_btn_MilkiWay.IsEnabled = true;
					mj_btn_Mw.IsEnabled = true;
					mj_btn_Pepsi.IsEnabled = true;
					mj_btn_Snickers.IsEnabled = true;
					mj_btn_SnickersWhite.IsEnabled = true;
					mj_btn_Sprite.IsEnabled = true;

					mj_cb_waluty.IsEnabled = false;
				}
			}
			if (mj_cb_waluty.SelectedItem.Equals(US))
			{
				for (ushort i = 0; i < ContainerOfCoins.Length; i++)
				{
					ContainerOfCoins[i].Cardinality = CardinalityOfCoins;
					ContainerOfCoins[i].Value = ValueOfCoinsUS[i];
					mj_btn_5.Content = "5 DOL";
					mj_btn_2.Content = "2 DOL";
					mj_btn_1.Content = "1 DOL";
					mj_btn_05.Content = "0.5CENT";

					//blokada wysztkich wyborów po klikniecu 
					mj_btn_ChipsBag.IsEnabled = true;
					mj_btn_CocaCola.IsEnabled = true;
					mj_btn_ColaLite.IsEnabled = true;
					mj_btn_Fanta.IsEnabled = true;
					mj_btn_LaysP.IsEnabled = true;
					mj_btn_LaysR.IsEnabled = true;
					mj_btn_MilkiWay.IsEnabled = true;
					mj_btn_Mw.IsEnabled = true;
					mj_btn_Pepsi.IsEnabled = true;
					mj_btn_Snickers.IsEnabled = true;
					mj_btn_SnickersWhite.IsEnabled = true;
					mj_btn_Sprite.IsEnabled = true;

					mj_cb_waluty.IsEnabled = false;
				}
			}
		}
	}
	
}
