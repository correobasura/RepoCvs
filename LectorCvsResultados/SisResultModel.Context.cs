﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace LectorCvsResultados
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class SisResultEntities : DbContext
{
    public SisResultEntities()
        : base("name=SisResultEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<ANALISTINDEXUNG> ANALISTINDEXUNG { get; set; }

    public virtual DbSet<USERRESULTTABLESFS> USERRESULTTABLESFS { get; set; }

    public virtual DbSet<FLASHORDERED> FLASHORDERED { get; set; }

}

}

