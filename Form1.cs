using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CurrencyConvertorApp
{
    public partial class Form1 : Form
    {
        // Use a single static HttpClient instance for efficiency
        // Static means there's only one instance for the entire application. This is important because HttpClient is designed to be reused.
        private static readonly HttpClient httpClient = new HttpClient();
        // Store rates: Key = Currency Code (e.g., "USD"), Value = Rate relative to EUR
        // A Dictionary is like a real-world dictionary; you look up a word (the Key) to find its definition (the Value).
        private Dictionary<string, decimal> exchangeRates = new Dictionary<string, decimal>();

       
        public Form1()
        {
            InitializeComponent();
            this.Text = "Live Currency Convertor";
            lblResult.Text = "Loading Rates...";
        }
        //The 'async' keyword allows this method to use 'await' to perform long-running tasks (like web requests) without freezing the UI.
        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadCurrencyData();
            //'await' tells the program to continue with other tasks while it waits for the network response. The UI stays responsive.
        }

        private async Task LoadCurrencyData()
        {
            // Frankfurter API endpoints (Updated to use the correct .dev domain)
            
            const string baseUrl = "https://api.frankfurter.dev/v1";
            const string symbolsUrl = $"{baseUrl}/currencies";
            const string ratesUrl = $"{baseUrl}/latest";

            try
            {
                // 1. Fetch available currencies (symbols and names)
                HttpResponseMessage symbolsResponse = await httpClient.GetAsync(symbolsUrl);
                symbolsResponse.EnsureSuccessStatusCode();
                //The method EnsureSuccessStatusCode() is part of the HttpResponseMessage class in C# and
                //serves a specific, vital purpose: to check if the HTTP request was successful,
                //and if not, to stop execution immediately by throwing an exception.
                string symbolsJson = await symbolsResponse.Content.ReadAsStringAsync();
                //Deserialisation is the process of converting JSON text (from the API) into C# objects we can use in our code.
                SymbolsResponse symbols = JsonSerializer.Deserialize<SymbolsResponse>(symbolsJson);
                //symbols is of type SymbolsResponse, which is essentially a Dictionary<string, string>
                // it actually makes accessing currency names by their codes straightforward and simplifies access later in the code.
                // 2. Fetch latest rates (all rates relative to EUR)
                HttpResponseMessage ratesResponse = await httpClient.GetAsync(ratesUrl);
                ratesResponse.EnsureSuccessStatusCode();
                string ratesJson = await ratesResponse.Content.ReadAsStringAsync();
                var rateData = JsonSerializer.Deserialize<RateResponse>(ratesJson);

                // Process and populate data
                if (rateData?.Rates != null && symbols != null)
                    // the ? operator is a null-conditional operator in C#.
                    // It is used to perform a member access (like calling a method or accessing a property)
                    // only if the preceding object is not null.
                    // If the object is null, the entire expression evaluates to null instead of throwing a NullReferenceException.
                {
                    exchangeRates.Clear(); // Clear any existing data
                    cmbFrom.Items.Clear();
                    cmbTo.Items.Clear();

                    // Add the base currency (EUR) to the dictionary itself, rate is 1.0
                    exchangeRates.Add(rateData.Base, 1.0m);
                    // Create display string for base currency
                    
                    string displayString = "EUR - Euro";
                    // Populate ComboBoxes with base currency
                    cmbFrom.Items.Add(displayString);
                    cmbTo.Items.Add(displayString);
                    // Add all other fetched rates and populate ComboBoxes
                    foreach (var rate in rateData.Rates)
                    {
                        exchangeRates.Add(rate.Key, rate.Value);

                        // Create a user-friendly display string
                        string currencyName = symbols.GetValueOrDefault(rate.Key, "Unknown Currency");
                         displayString = $"{rate.Key} - {currencyName}";

                        cmbFrom.Items.Add(displayString);
                        cmbTo.Items.Add(displayString);
                    }

                    // Set initial selections
                    
                    cmbFrom.SelectedIndex = cmbFrom.FindString("GBP");
                    cmbTo.SelectedIndex = cmbTo.FindString("EUR");
                    
                    lblLastUpdated.Text = $"Rates updated on: {rateData.Date:yyyy-MM-dd} (Base: {rateData.Base})";
                    lblResult.Text = "Ready for conversion.";
                }
            }
            catch (HttpRequestException ex)
            {
                lblResult.Text = "ERROR: Failed to fetch rates.";
                MessageBox.Show($"Network Error: Could not fetch data. Check your connection or API status. Details: {ex.Message}", "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                lblResult.Text = "ERROR: See console.";
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            // 1. Validate inputs
            //TryParse is a safe way to check if the text can be converted to a number. It returns true if successful.
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid positive number for the amount.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbFrom.SelectedItem == null || cmbTo.SelectedItem == null)
            {
                MessageBox.Show("Please select both 'From' and 'To' currencies.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Extract currency codes (the first 3 characters)
            string fromCurrencyCode = cmbFrom.SelectedItem.ToString().Substring(0, 3);
            string toCurrencyCode = cmbTo.SelectedItem.ToString().Substring(0, 3);

            if (fromCurrencyCode == toCurrencyCode)
            {
                lblResult.Text = $"Result: {amount:N2} {fromCurrencyCode} (No conversion needed)";
                return;
            }

            // 3. Perform conversion calculation using the stored rates
            if (exchangeRates.ContainsKey(fromCurrencyCode) && exchangeRates.ContainsKey(toCurrencyCode))
            {
                decimal rateFrom = exchangeRates[fromCurrencyCode];
                decimal rateTo = exchangeRates[toCurrencyCode];

                // Formula: Convert FROM currency to the base currency (EUR), then to the TO currency.
                // (Amount / Rate_From_to_EUR) * Rate_To_to_EUR
                decimal convertedAmount = (amount / rateFrom) * rateTo;

                // 4. Display the result, formatted to two decimal places
                //The ":N2" is a format specifier that displays the number with thousand separators and 2 decimal places (e.g., 1,234.57).
                lblResult.Text = $"Result: {convertedAmount:N2} {toCurrencyCode}";
            }
            else
            {
                MessageBox.Show("Required exchange rates are missing.", "Internal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
