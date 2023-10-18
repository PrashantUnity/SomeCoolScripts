using Xamarin.Essentials;
 
private void OnButtonClicked(object sender, EventArgs e)
{
    try
    {
        // Specify the phone number with the country code (e.g., +1 for the United States)
        string phoneNumber = "+1234567890";

        // Open WhatsApp with the specified phone number
        Launcher.OpenAsync($"https://api.whatsapp.com/send?phone={phoneNumber}");
    }
    catch (Exception ex)
    {
        // Handle any exceptions
        Console.WriteLine(ex.Message);
    }
}


// from nuget package manager install Xamarin Essential
