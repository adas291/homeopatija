using homeopatija.Entities;
using Microsoft.EntityFrameworkCore;

public class HomeopatijaContext : DbContext
{
    public HomeopatijaContext(DbContextOptions<HomeopatijaContext> options) : base(options)
    {
        Orders = Set<Order>();
        Users = Set<User>();
        Drugs = Set<Drug>();
        Diseases = Set<Disease>();
        CartDrugs = Set<CartDrug>();
        MandatorDiseaseSymptoms = Set<MandatorDiseaseSymptom>();
        PossibleDiseaseSymptoms = Set<PossibleDiseaseSymptom>();
        Faqs = Set<Faq>();
        Reports = Set<Report>();
        Comments = Set<Comment>();
        Symptoms = Set<Symptom>();
        PaymentMethods = Set<PaymentMethod>();
        Payments = Set<Payment>();
        Questionnaires = Set<Questionnaire>();
        DiagnosisSymptoms = Set<DiagnosisSymptom>();
        Diagnosis = Set<Diagnosis>();
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Drug> Drugs { get; set; }
    public DbSet<Disease> Diseases { get; set; }
    public DbSet<DrugCompatibility> DrugCompatibilities { get; set; }
    public DbSet<OrderDrug> OrderDrugs { get; set; }
    public DbSet<CartDrug> CartDrugs { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Symptom> Symptoms { get; set; }
    public DbSet<MandatorDiseaseSymptom> MandatorDiseaseSymptoms { get; set; }
    public DbSet<PossibleDiseaseSymptom> PossibleDiseaseSymptoms { get; set; }
    public DbSet<Faq> Faqs { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<Questionnaire> Questionnaires { get; set; }
    public DbSet<DiagnosisSymptom> DiagnosisSymptoms { get; set; }
    public DbSet<Diagnosis> Diagnosis { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();
    }
}