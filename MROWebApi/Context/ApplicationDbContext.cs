using Microsoft.EntityFrameworkCore;
using MROWebApi.Context;
using MROWebAPI.Context;

namespace CodeFirstMigration.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }


        public virtual DbSet<lnkROIFacilityFieldMaps> lnkROIFacilityFieldMaps { get; set; }
        public virtual DbSet<lnkROIFacilityPrimaryReasons> lnkROIFacilityPrimaryReasons { get; set; }
        public virtual DbSet<lnkROIFacilityRecordTypes> lnkROIFacilityRecordTypes { get; set; }
        public virtual DbSet<lnkROIFacilitySensitiveInfo> lnkROIFacilitySensitiveInfo { get; set; }
        public virtual DbSet<lstFields> lstFields { get; set; }
        public virtual DbSet<lstPrimaryReasons> lstPrimaryReasons { get; set; }
        public virtual DbSet<lstRecordTypes> lstRecordTypes { get; set; }
        public virtual DbSet<lstSensitiveInfo> lstSensitiveInfo { get; set; }
        public virtual DbSet<lstWizards> lstWizards { get; set; }
        public virtual DbSet<tblROIFacilities> tblROIFacilities { get; set; }
        public virtual DbSet<tblROILocations> tblROILocations { get; set; }
        public virtual DbSet<tblRequestors> tblRequestors { get; set; }
        public virtual DbSet<tblTempRequestors> tblTempRequestors { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<lnkROIFacilityFieldMaps>(entity =>
            {
                entity.HasKey(e => new { e.nROIFacilityID, e.nFieldID })
                    .HasName("PK_lnkROIFacilityFieldMaps_1");

                entity.Property(e => e.nROIFacilityFieldMapID).ValueGeneratedOnAdd();

                entity.HasOne(d => d.nField)
                    .WithMany(p => p.lnkROIFacilityFieldMaps)
                    .HasForeignKey(d => d.nFieldID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lnkROIFacilityFieldMaps_nFieldID");

                entity.HasOne(d => d.nROIFacility)
                    .WithMany(p => p.lnkROIFacilityFieldMaps)
                    .HasForeignKey(d => d.nROIFacilityID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lnkROIFacilityFieldMaps_tblROIFacilities");
            });

            modelBuilder.Entity<lnkROIFacilityPrimaryReasons>(entity =>
            {
                entity.HasKey(e => new { e.nPrimaryReasonID, e.nROIFacilityID });

                entity.Property(e => e.nROIFacilityPrimaryReasonID).ValueGeneratedOnAdd();

                entity.Property(e => e.sPrimaryReasonName).HasMaxLength(50);

                entity.HasOne(d => d.nPrimaryReason)
                    .WithMany(p => p.lnkROIFacilityPrimaryReasons)
                    .HasForeignKey(d => d.nPrimaryReasonID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lnkROIFacilityPrimaryReasons_nPrimaryReasonID");

                entity.HasOne(d => d.nROIFacility)
                    .WithMany(p => p.lnkROIFacilityPrimaryReasons)
                    .HasForeignKey(d => d.nROIFacilityID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lnkROIFacilityPrimaryReasons_nROIFacilityID");
            });

            modelBuilder.Entity<lnkROIFacilityRecordTypes>(entity =>
            {
                entity.HasKey(e => new { e.nROIFacilityID, e.nRecordTypeID });

                entity.Property(e => e.sRecordTypeName).HasMaxLength(50);

                entity.Property(e => e.nROIFacilityRecordTypeID).ValueGeneratedOnAdd();

                entity.HasOne(d => d.nROIFacility)
                    .WithMany(p => p.lnkROIFacilityRecordTypes)
                    .HasForeignKey(d => d.nROIFacilityID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lnkROIFacilityRecordTypes_nROIFacilityID");

                entity.HasOne(d => d.nRecordType)
                    .WithMany(p => p.lnkROIFacilityRecordTypes)
                    .HasForeignKey(d => d.nRecordTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lnkROIFacilityRecordTypes_nRecordTypeID");
            });

            modelBuilder.Entity<lnkROIFacilitySensitiveInfo>(entity =>
            {
                entity.HasKey(e => new { e.nROIFacilityID, e.nSensitiveInfoID });

                entity.Property(e => e.nROIFacilitySensitiveInfoID).ValueGeneratedOnAdd();

                entity.Property(e => e.sSensitiveInfoName).HasMaxLength(50);

                entity.HasOne(d => d.nROIFacility)
                    .WithMany(p => p.lnkROIFacilitySensitiveInfo)
                    .HasForeignKey(d => d.nROIFacilityID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lnkROIFacilitySensitiveInfo_nROIFacilityID");

                entity.HasOne(d => d.nSensitiveInfo)
                    .WithMany(p => p.lnkROIFacilitySensitiveInfo)
                    .HasForeignKey(d => d.nSensitiveInfoID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_lnkROIFacilitySensitiveInfo_nSensitiveInfoID");
            });

            modelBuilder.Entity<lstFields>(entity =>
            {
                entity.HasKey(e => e.nFieldID)
                    .HasName("PK_Field");

                entity.HasOne(d => d.nWizard)
                    .WithMany(p => p.lstFields)
                    .HasForeignKey(d => d.nWizardID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Field_Wizard");
            });

            modelBuilder.Entity<lstPrimaryReasons>(entity =>
            {
                entity.HasKey(e => e.nPrimaryReasonID)
                    .HasName("PK_lst_PrimaryReason");

                entity.Property(e => e.sPrimaryReasonName).HasMaxLength(50);
            });

            modelBuilder.Entity<lstRecordTypes>(entity =>
            {
                entity.HasKey(e => e.nRecordTypeID)
                    .HasName("PK_lst_RecordType");

                entity.Property(e => e.sRecordTypeName).HasMaxLength(50);
            });

            modelBuilder.Entity<lstSensitiveInfo>(entity =>
            {
                entity.HasKey(e => e.nSensitiveInfoID)
                    .HasName("PK_lst_SensitiveInfor");

                entity.Property(e => e.sSensitiveInfoName).HasMaxLength(50);
            });

            modelBuilder.Entity<lstWizards>(entity =>
            {
                entity.HasKey(e => e.nWizardID)
                    .HasName("PK_Wizard");
            });

            modelBuilder.Entity<tblROIFacilities>(entity =>
            {
                entity.HasKey(e => e.nROIFacilityID)
                    .HasName("PK_Customer");

                entity.Property(e => e.sConfigShowFields).IsRequired();
            });

            modelBuilder.Entity<tblROILocations>(entity =>
            {
                entity.HasKey(e => new { e.nROIFacilityID, e.nLocationID })
                    .HasName("PK_Location");

                entity.Property(e => e.sLocationAddress)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.sLocationName).HasMaxLength(30);

                entity.HasOne(d => d.nROIFacility)
                    .WithMany(p => p.tblROILocations)
                    .HasForeignKey(d => d.nROIFacilityID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Location_Facility");
            });

            modelBuilder.Entity<tblRequestors>(entity =>
            {
                entity.HasKey(e => e.nRequestorID);

                entity.Property(e => e.dtAuthExpireDate).HasColumnType("date");

                entity.Property(e => e.dtPatientDOB).HasColumnType("date");

                entity.Property(e => e.dtRecordTimeFrameEnd).HasColumnType("date");

                entity.Property(e => e.dtRecordTimeFrameStart).HasColumnType("date");

                entity.Property(e => e.dtRequestDeadlineDate).HasColumnType("date");

                entity.Property(e => e.sEmailId).HasMaxLength(30);

                entity.Property(e => e.sPatientFirstName).HasMaxLength(20);

                entity.Property(e => e.sPatientLastName).HasMaxLength(50);

                entity.Property(e => e.sPatientMiddleInitial).HasMaxLength(50);

                entity.Property(e => e.sPatientStreetAddress).HasMaxLength(20);

                entity.Property(e => e.sPatientZip)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.sPhoneNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.sRelationship).HasMaxLength(20);

                entity.Property(e => e.sRelativeName).HasMaxLength(30);

                entity.Property(e => e.sWayOfSendRecord).HasMaxLength(30);

                entity.Property(e => e.sWhomReleaseRecord).HasMaxLength(30);

                entity.Property(e => e.sWhomReleaseRecordName).HasMaxLength(30);

                entity.Property(e => e.sWhomReleaseRecordStreetAdd).HasMaxLength(30);

                entity.Property(e => e.sWhomReleaseRecordZip)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.nROIFacility)
                    .WithMany(p => p.tblRequestors)
                    .HasForeignKey(d => d.nROIFacilityID)
                    .HasConstraintName("FK_tblRequestors_nROIFacilityID");

                entity.HasOne(d => d.n)
                    .WithMany(p => p.tblRequestors)
                    .HasForeignKey(d => new { d.nROIFacilityID, d.nLocationID })
                    .HasConstraintName("FK_tblRequestors_nLocationID");
            });

            modelBuilder.Entity<tblTempRequestors>(entity =>
            {
                entity.HasKey(e => e.nRequestorID);

                entity.Property(e => e.dtAuthExpireDate).HasColumnType("date");

                entity.Property(e => e.dtPatientDOB).HasColumnType("date");

                entity.Property(e => e.dtRecordTimeFrameEnd).HasColumnType("date");

                entity.Property(e => e.dtRecordTimeFrameStart).HasColumnType("date");

                entity.Property(e => e.dtRequestDeadlineDate).HasColumnType("date");

                entity.Property(e => e.sEmailId).HasMaxLength(30);

                entity.Property(e => e.sPatientFirstName).HasMaxLength(20);

                entity.Property(e => e.sPatientLastName).HasMaxLength(50);

                entity.Property(e => e.sPatientMiddleInitial).HasMaxLength(50);

                entity.Property(e => e.sPatientStreetAddress).HasMaxLength(20);

                entity.Property(e => e.sPatientZip)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.sPhoneNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.sRelationship).HasMaxLength(20);

                entity.Property(e => e.sRelativeName).HasMaxLength(30);

                entity.Property(e => e.sWayOfSendRecord).HasMaxLength(30);

                entity.Property(e => e.sWhomReleaseRecord).HasMaxLength(30);

                entity.Property(e => e.sWhomReleaseRecordName).HasMaxLength(30);

                entity.Property(e => e.sWhomReleaseRecordStreetAdd).HasMaxLength(30);

                entity.Property(e => e.sWhomReleaseRecordZip)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.nROIFacility)
                    .WithMany(p => p.tblTempRequestors)
                    .HasForeignKey(d => d.nROIFacilityID)
                    .HasConstraintName("FK_tblTempRequestors_nROIFacilityID");
            });

   
        

        
        modelBuilder.seed();
        }
   
    }
}