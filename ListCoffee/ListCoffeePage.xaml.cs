using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListCoffee
{
	public partial class ListCoffeePage : ContentPage
	{
		ObservableCollection<Coffee> CoffeeItems;
		bool isScrolling = false;
		double currentWidth = 0;
		public ListCoffeePage()
		{
			InitializeComponent();
			BindingContext = this;
			CoffeeItems = new ObservableCollection<Coffee>()
			{
				new Coffee(){
					Title = "Espresse",
					SmallPrice = "2.30",
					LargePrice = "3.90"
				},
				new Coffee()
				{
					Title = "Espresse",
					SmallPrice = "2.30",
					LargePrice = "3.90"
				},
				new Coffee()
				{
					Title = "Espresse",
					SmallPrice = "2.30",
					LargePrice = "3.90"
				},
				new Coffee()
				{
					Title = "Espresse",
					SmallPrice = "2.30",
					LargePrice = "3.90"
				}
			};
			coffeeListView.ItemsSource = CoffeeItems;
			donutListView.ItemsSource = CoffeeItems;
			setWidth();
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			setWidth();
		}


		async void Handle_Scrolled(object sender, Xamarin.Forms.ScrolledEventArgs e)
		{
			//System.Diagnostics.Debug.WriteLine("X: " + e.ScrollX);
			var first = currentWidth - 10;
			var second = currentWidth - 40;
			var third =  currentWidth /3;
			if (e.ScrollX > 30 && e.ScrollX < second && !isScrolling)
			{
				 await SetButton("Donuts");

			}

			else if (e.ScrollX < first && e.ScrollX > third && !isScrolling)
			{
                await SetButton("Coffee");
			}
			else if (e.ScrollX < 25 || e.ScrollX > currentWidth)
			{
				isScrolling = false;
				return;
			}

		}
		protected override void OnSizeAllocated(double width, double height)
		{
			base.OnSizeAllocated(width, height);
			setWidth();
		}
		async void Coffee_Clicked(object sender, System.EventArgs e)
		{
			await SetButton("Coffee");
		}
		async void Donuts_Clicked(object sender, System.EventArgs e)
		{
           await SetButton("Donuts");
		}

		async Task SetButton(string Button)
		{
			if (Button == "Donuts")
			{
				await scrollView.ScrollToAsync(currentWidth + 5, 0, true);
				isScrolling = true;
				CoffeeButton.TextColor = Color.FromHex("#CCCCCC");
				CoffeeLine.HeightRequest = 0;
				DonutsButton.TextColor = Color.FromHex("#33384A");
				DonutsLine.HeightRequest = 2;
			}
			else if (Button == "Coffee")
			{
				await scrollView.ScrollToAsync(0, 0, true);
				isScrolling = true;
				DonutsButton.TextColor = Color.FromHex("#CCCCCC");
				DonutsLine.HeightRequest = 0;
				CoffeeButton.TextColor = Color.FromHex("#33384A");
				CoffeeLine.HeightRequest = 2;
			}
		}



		void setWidth()
		{
			currentWidth = headBox.Width;
			Frame1.WidthRequest = headBox.Width - 80;
			Frame2.WidthRequest = headBox.Width - 80;
		}

	}
}
