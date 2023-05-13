using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


public partial class PreQual : ObservableObject
{
    double INTERESTRATE = 6.75;
    int FHALOANLIMIT = 470000;
    int RHSINCOMELIMIT = 103500;

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
    public double maxRHSLAMT { get; set; }
    public double maxFHALAMT { get; set; }
    public double maxRHSPITI { get; set; }
    public double maxFHAPITI { get; set; }
    public double totalDebt { get; set; }
    public double totalIncome { get; set; }
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

    public PreQual PreQualify() 
    {
        this.totalDebt = 0;
        this.totalIncome = 0;
        this.maxFHALAMT = 0;
        this.maxRHSLAMT = 0;
        foreach (Client client in this.Clients) 
        {
            this.totalDebt += client.debt;
            this.totalIncome += client.income;
        }
        if (this.totalIncome * 0.47 - this.totalDebt < this.totalIncome * 0.37)
        {
            this.maxFHAPITI = this.totalIncome * 0.47 - this.totalDebt;
        }
        else
        {
            this.maxFHAPITI = this.totalIncome * 0.37;
        }
        if (this.totalIncome * 0.41 - this.totalDebt < this.totalIncome * 0.31)
        {
            this.maxRHSPITI = this.totalIncome * 0.41 - this.totalDebt;
        }
        else
        {
            this.maxRHSPITI = this.totalIncome * 0.31;
        }

        if (this.totalIncome * 12 < RHSINCOMELIMIT)
        {
            while (
              this.PmtCalculator(this.maxRHSLAMT, 30, INTERESTRATE) +
                (this.maxRHSLAMT * 0.0035) / 12 +
                (this.maxRHSLAMT * 0.0035) / 12 <
              this.maxRHSPITI
            )
            {
                this.maxRHSLAMT += 5000;
            }
        }
        while (
          this.maxFHALAMT < FHALOANLIMIT &&
          PmtCalculator(this.maxFHALAMT * 0.965, 30, INTERESTRATE) +
            (this.maxFHALAMT * 0.0035) / 12 +
            (this.maxFHALAMT * 0.0085) / 12 <
            this.maxFHAPITI
        )
        {
            this.maxFHALAMT += 5000;
        }


        return this;
    }
    private double PmtCalculator(double loanamount, int term, double rate)
    {
        double irate = rate / 100;
        int months = term * 12;
        double monthlyirate = irate / 12;
        double payment = loanamount * ((monthlyirate * (1 + monthlyirate) * months) / ((1 + monthlyirate) * months - 1));
        return payment;
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

    public double income { get; set; }
    public double debt { get; set; }
    public int creditScore { get; set; }
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
    public double salary { get; set; }
    public double? yearsOfExperience { get; set; }
    public string type { get; set; }
    public string paymentFrequency { get; set; }
    public double id { get; set; }
    public double? firstReturn { get; set; }
    public double? secondReturn { get; set; }
    public double? returnAverage { get; set; }
    public string pensionType { get; set; }
    public string _id { get; set; }
    public override string ToString()
    {
        return $"{title}\nIncome: {salary}\nType:{type}\nYOE:{yearsOfExperience}";
    }
}

public class Debt
{
    public double payment { get; set; }
    public string type { get; set; }
    public double id { get; set; }
    public double balance { get; set; }
    public string deferred { get; set; }
    public bool isDeferred { get; set; }
    public string _id { get; set; }
    public override string ToString()
    {
        return $"{type}\nPayment: {payment}";
    }
    
}
