using CorectMyQuran.Application.Variables;
using CorectMyQuran.DateBase.Common.Models;
using CorectMyQuran.DateBase.Interceptors;
using CorectMyQuran.Features.Admins.Domain;
using CorectMyQuran.Features.Auth.Domain.RefreshTokens;
using CorectMyQuran.Features.Sheikh.Domain;
using CorectMyQuran.Features.Student.Domain;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CorectMyQuran.DateBase;

public class MainContext :DbContext
{

    private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;
    public MainContext(DbContextOptions< MainContext> options, PublishDomainEventsInterceptor publishDomainEventsInterceptor) : base(options)
    {
        _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
    }

    public DbSet<Admin> Admins { get; set; }
    public DbSet<Sheikh> Sheikhs { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder
            .Ignore<List<DomainEvent>>()
            .ApplyConfigurationsFromAssembly(typeof(MainContext).Assembly);


        modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetProperties())
            .Where(p => p.IsPrimaryKey())
            .ToList()
            .ForEach(p => p.ValueGenerated = ValueGenerated.Never);
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {


        optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
        base.OnConfiguring(optionsBuilder);
    }


}