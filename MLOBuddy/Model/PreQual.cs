using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

public partial class PreQual : ObservableObject
{
    public string _id { get; set; }
    public bool favorite { get; set; }
    public bool hidden { get; set; }

    [ObservableProperty]
    public ObservableCollection<Client> clients;
    public DateTime date { get; set; }
    public bool VA { get; set; }
    public bool privacy { get; set; }
    public bool terms { get; set; }
    public string purpose { get; set; }
    public float maxRHSLAMT { get; set; }
    public float maxFHALAMT { get; set; }
    public float maxRHSPITI { get; set; }
    public float maxFHAPITI { get; set; }
    public float totalDebt { get; set; }
    public float totalIncome { get; set; }
    public object originator { get; set; }
    public string id { get; set; }
    public string preq_string { get; set; }
    public string favoriteString { get; set; }
    public override string ToString()
    {
        string RHS = string.Empty;
        if (maxRHSLAMT > 0) { RHS = $"\nRHS: {Math.Round(maxRHSLAMT, 2)}"; }
        if (favorite) { favoriteString = "Remove from favorites"; }
        else { favoriteString = "Add to favorites"; }
        preq_string = $"FHA = {Math.Round(maxFHALAMT, 2)} {RHS} \nVA Eligible: {VA} \nFHA DTI:{Math.Round(((totalDebt + maxFHAPITI) / totalIncome) * 100),2}%\nTotal Income: {Math.Round(totalIncome, 2)}\nTotal Debt: {Math.Round(totalDebt, 2)}\nID: {id}";
        string display = $"{clients[0].firstName } {clients[0].lastName} \nFHA: {maxFHALAMT}";
        return display;
    }
    public string FavoriteString()
    {
        if (favorite)
        {
            favoriteString = "Remove from favorites";
            return "Remove from favorites";

        }
        favoriteString = "Add to favorites";
        return "Add to favorites";
    }
    public string PreQualString()
    {
        string RHS = string.Empty;
        if (maxRHSLAMT>0) { RHS = $"\nRHS: {Math.Round(maxRHSLAMT, 2)}"; }
        if (favorite) { favoriteString = "Remove from favorites"; } 
        else { favoriteString =  "Add to favorites"; }
        preq_string = $"FHA = {Math.Round(maxFHALAMT, 2)} {RHS} \nVA Eligible: {VA} \nFHA DTI:{Math.Round(((totalDebt+maxFHAPITI)/totalIncome)*100),2}%\nTotal Income: {Math.Round(totalIncome,2)}\nTotal Debt: {Math.Round(totalDebt,2)}\nID: {id}";
        return preq_string;
    }
}

public partial class Client : ObservableObject
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string phoneNumber { get; set; }
    public string email { get; set; }
    public float id { get; set; }

    [ObservableProperty]
    public ObservableCollection<Debt> debts;
    [ObservableProperty]
    public ObservableCollection<Job> jobs;

    public float income { get; set; }
    public decimal debt { get; set; }
    public float creditScore { get; set; }
    public string maritalStatus { get; set; }
    public object preNup { get; set; }
    public bool noDebt { get; set; }
    public bool noMoreDebt { get; set; }
    public bool noIncome { get; set; }
    public string _id { get; set; }
    public override string ToString()
    {
        return $"Client: {firstName} {lastName} \nEmail: {email}\nPhone Number: {phoneNumber}\n Income: {Math.Round(income,2)} Debts:{Math.Round(debt,2)}";
    }
}

public class Job
{
    public string title { get; set; }
    public float salary { get; set; }
    public float? yearsOfExperience { get; set; }
    public string type { get; set; }
    public string paymentFrequency { get; set; }
    public float id { get; set; }
    public float? firstReturn { get; set; }
    public float? secondReturn { get; set; }
    public float? returnAverage { get; set; }
    public string pensionType { get; set; }
    public string _id { get; set; }
    public override string ToString()
    {
        return $"{title}\nIncome: {salary}\nType:{type}\nYOE:{yearsOfExperience}";
    }
}

public class Debt
{
    public decimal payment { get; set; }
    public string type { get; set; }
    public decimal balance { get; set; }
    public decimal id { get; set; }
    public string deferred { get; set; }
    public bool isDeferred { get; set; }
    public string _id { get; set; }
    public override string ToString()
    {
        return $"{type}\nPayment: {payment}";
    }
}