
public class PreQual
{
    public string _id { get; set; }
    public bool favorite { get; set; }
    public bool hidden { get; set; }
    public Client[] clients { get; set; }
    public DateTime date { get; set; }
    public bool privacy { get; set; }
    public bool terms { get; set; }
    public string purpose { get; set; }
    public int maxRHSLAMT { get; set; }
    public int maxFHALAMT { get; set; }
    public float maxRHSPITI { get; set; }
    public float maxFHAPITI { get; set; }
    public int totalDebt { get; set; }
    public float totalIncome { get; set; }
    public object originator { get; set; }
    public string id { get; set; }
}

public class Client
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string phoneNumber { get; set; }
    public string email { get; set; }
    public int id { get; set; }
    public object[] debts { get; set; }
    public Job[] jobs { get; set; }
    public int income { get; set; }
    public int debt { get; set; }
    public int creditScore { get; set; }
    public string maritalStatus { get; set; }
    public object preNup { get; set; }
    public bool noDebt { get; set; }
    public bool noMoreDebt { get; set; }
    public bool noIncome { get; set; }
    public string _id { get; set; }
}

public class Job
{
    public string title { get; set; }
    public float salary { get; set; }
    public int yearsOfExperience { get; set; }
    public string type { get; set; }
    public string paymentFrequency { get; set; }
    public float id { get; set; }
    public int firstReturn { get; set; }
    public int secondReturn { get; set; }
    public float returnAverage { get; set; }
    public string pensionType { get; set; }
    public string _id { get; set; }
}
