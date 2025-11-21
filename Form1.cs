using System.Text.Json;

namespace CurrencyConvertorApp
{
    public partial class Form1 : Form
    {
        // Use a single static HttpClient instance for efficiency
        private static readonly HttpClient httpClient = new HttpClient();
        // Store rates: Key = Currency Code (e.g., "USD"), Value = Rate relative to EUR
        private Dictionary<string, decimal> exchangeRates = new Dictionary<string, decimal>();
        public Form1()
        {
            InitializeComponent();
            this.Text = "Live Currency Convertor";
            lblResult.Text = "Loading Rates...";
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadCurrencyData();
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
                string symbolsJson = await symbolsResponse.Content.ReadAsStringAsync();
                var symbols = JsonSerializer.Deserialize<SymbolsResponse>(symbolsJson);

                // 2. Fetch latest rates (all rates relative to EUR)
                HttpResponseMessage ratesResponse = await httpClient.GetAsync(ratesUrl);
                ratesResponse.EnsureSuccessStatusCode();
                string ratesJson = await ratesResponse.Content.ReadAsStringAsync();
                var rateData = JsonSerializer.Deserialize<RateResponse>(ratesJson);

                // Process and populate data
                if (rateData?.Rates != null && symbols != null)
                {
                    exchangeRates.Clear(); // Clear any existing data
                    cmbFrom.Items.Clear();
                    cmbTo.Items.Clear();

                    // Add the base currency (EUR) to the dictionary itself, rate is 1.0
                    exchangeRates.Add(rateData.Base, 1.0m);

                    // Add all other fetched rates and populate ComboBoxes
                    foreach (var rate in rateData.Rates)
                    {
                        exchangeRates.Add(rate.Key, rate.Value);

                        // Create a user-friendly display string
                        string currencyName = symbols.GetValueOrDefault(rate.Key, "Unknown Currency");
                        string displayString = $"{rate.Key} - {currencyName}";

                        cmbFrom.Items.Add(displayString);
                        cmbTo.Items.Add(displayString);
                    }

                    // Set initial selections
                    cmbFrom.SelectedIndex = cmbFrom.FindStringExact("EUR");
                    cmbTo.SelectedIndex = cmbTo.FindStringExact("USD");

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
                lblResult.Text = $"Result: {convertedAmount:N2} {toCurrencyCode}";
            }
            else
            {
                MessageBox.Show("Required exchange rates are missing.", "Internal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
