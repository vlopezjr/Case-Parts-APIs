using System.Data.Entity;

namespace CreateCustomer.API.Entities
{
    public class CustomerContext : DbContext
    {
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustClass> CustClasses { get; set; }
        public virtual DbSet<PaymentTerms> PaymentTerms { get; set; }
        public virtual DbSet<StatementCycle> StatementCycles { get; set; }
        public virtual DbSet<CustAddress> CustAddresses { get; set; }
        public virtual DbSet<ShipMethod> ShipMethods { get; set; }
        public virtual DbSet<BusinessForm> BusinessForms { get; set; }
        public virtual DbSet<SalesSource> SalesSources { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<SalesTerritory> SalesTerritories { get; set; }
        public virtual DbSet<CustStatus> CustStatuses { get; set; }
        public virtual DbSet<Territory> Territories { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<NationalAccount> NationalAccounts { get; set; }
        public virtual DbSet<NationalAccountLevel> NationalAccountLevels { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<TaxExemptionCPC> TaxExemptionsCPC { get; set; }
        public virtual DbSet<TaxExemptionAcuity> TaxExemptionsAcuity { get; set; }
        public virtual DbSet<TaxSchedule> TaxSchedules { get; set; }
        public virtual DbSet<TaxCode> TaxCodes { get; set; }
        public virtual DbSet<CreditCardType> CreditCardTypes { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<TaxSubjClass> TaxSubjClasses { get; set; }
        public virtual DbSet<CustHold> CustHolds { get; set; }
        public virtual DbSet<CustPayment> CustPayments { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<CPSalesOrder> CPSalesOrders { get; set; }
        public virtual DbSet<SalesOrder> SalesOrders { get; set; }
        public virtual DbSet<Shipment> Shipments { get; set; }
        public virtual DbSet<CCTransaction> CCTransactions { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<VendorAddress> VendorAddresses { get; set; }
        public virtual DbSet<MemoRemark> MemoRemarks { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<Student>()
            //            .HasMany<Course>(s => s.Courses)
            //            .WithMany(c => c.Students)
            //            .Map(cs =>
            //            {
            //                cs.MapLeftKey("StudentRefId");
            //                cs.MapRightKey("CourseRefId");
            //                cs.ToTable("StudentCourse");
            //            });



            modelBuilder.Entity<TaxSchedule>()
            .HasMany(schd => schd.TaxCodes)
            .WithMany(code => code.TaxSchedules)
            .Map(ug =>
            {
                ug.MapLeftKey("STaxSchdKey");
                ug.MapRightKey("STaxCodeKey");
                ug.ToTable("tciSTaxSchdCodes");
            });

            modelBuilder.Entity<User>()
                .HasMany(user => user.Groups)
                .WithMany(grp => grp.Users)
                .Map(ug =>
                {
                    ug.MapLeftKey("UserKey");
                    ug.MapRightKey("GroupKey");
                    ug.ToTable("tcpGroupMember");
                });


            modelBuilder.Entity<Customer>()
                .HasMany(s => s.Branches)
                .WithMany(c => c.Parents)
                .Map(cs =>
                {
                    cs.MapLeftKey("ParentCustKey");
                    cs.MapRightKey("ChildCustKey");
                    cs.ToTable("tarNationalAcctMember");
                });

            modelBuilder.Entity<Customer>()
                    .HasMany(c => c.Contacts)
                    .WithRequired(c => c.Customer)
                    .HasForeignKey(c => c.CntctOwnerKey);

            modelBuilder.Entity<CustAddress>()
                    .HasRequired(a => a.Customer)
                    .WithMany(r => r.CustAddresses)
                    .HasForeignKey(c => c.CustKey);

            modelBuilder.Entity<Address>()
                    .HasRequired(c => c.CustAddress)
                    .WithRequiredPrincipal(c => c.Address);

            modelBuilder.Entity<Customer>()
                .HasOptional(s => s.CustStatus)
                .WithRequired(s => s.Customer);


            modelBuilder.Entity<Customer>()
                .HasOptional(s => s.CustHold)
                .WithRequired(s => s.Customer);

            modelBuilder.Entity<CustHold>()
                .HasRequired(s => s.HoldStatus);
        }
    }
}
